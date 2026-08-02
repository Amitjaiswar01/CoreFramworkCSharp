using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using static System.String;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Enums;
using LampsPlus.AutomationFramework.Exceptions;
using OperatingSystem = LampsPlus.AutomationFramework.Utilities.TestConfiguration.OperatingSystem;

namespace LampsPlus.AutomationFramework.Utilities.Environment
{
    /// <summary>
    /// Responsible for configuring the proxy environment.
    /// </summary>
    public class EnvironmentResolver
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private static object _lock = new object();
        private static bool _isInitialized;
        private static string _nodeHost;
        private Log _log;
        private EnvironmentUnderTest _currentEnvironment;
        public static string TargetEnvironment;
        public static string BaselineEnvironment;

        private static Dictionary<string, string> Ports =>
            ConfigurationManager.AppSettings["ProxyPortMappings"]
                .Split(',')
                .Select(entry => entry.Split('-'))
                .ToDictionary(key => key[0], value => value[1]);

        private readonly bool _isNetworkLoggingTest;

        //app.config settings
        private static readonly NameValueCollection _applicationMobileGridSettings = ConfigurationManager.GetSection("GridGroup/MobileGrid") as NameValueCollection;
        private static readonly NameValueCollection _applicationDesktopGridSettings = ConfigurationManager.GetSection("GridGroup/DesktopGrid") as NameValueCollection;
        private readonly string _mobileGridHub = _applicationMobileGridSettings.GetValues("SeleniumMobileHubHost").First();
        private readonly string _mobileGridRemote = _applicationMobileGridSettings.GetValues("SeleniumMobileHubHostAlternative").First();
        private readonly string _defaultBmpPort = _applicationMobileGridSettings.GetValues("DefaultBmpProxyPort").First();
        private readonly string _bmpPortBaseline = _applicationMobileGridSettings.GetValues("MobileGridBmpPort").First();
        private readonly string _bmpPortTarget = _applicationMobileGridSettings.GetValues("MobileGridSecondBmpPort").First();
        private readonly string _bmpPortIpad = _applicationMobileGridSettings.GetValues("MobileGridThirdBmpPort").First();
        private readonly string _bmpDataCaptureInstanceHost = _applicationDesktopGridSettings.GetValues("DataCaptureBmpInstanceHost").First();

        private OperatingSystem _operatingSystem;

        public string ProxyIpAddress => GetProxyAddress(GetDesiredEnvironment());
        public string ProxyPort => GetProxyPort(GetDesiredEnvironment());

        // NOTE: For mobile devices, we will use the host that a device is connected to for the proxy server.
        // For example, we can use the mac mini as the proxy for the devices connected to it.
        private string _proxyHost;

        //Grid app.congig settings
        private NameValueCollection applicationDesktopGridSettings = ConfigurationManager.GetSection("GridGroup/DesktopGrid") as NameValueCollection;
        private NameValueCollection applicationMobileGridSettings = ConfigurationManager.GetSection("GridGroup/MobileGrid") as NameValueCollection;
        private string mobileCloudRunConfig = ConfigurationManager.AppSettings["MobileGridCloud"];
        private string desktopCloudRunConfig = ConfigurationManager.AppSettings["DesktopGridCloud"];

        //Property to get BMP Host for bamboo
        public string VisualProxyIpAddress => IsLocalEnvironment ? $"{applicationDesktopGridSettings.GetValues("ProxyHost").FirstOrDefault()}:{GetProxyPort(GetDesiredEnvironment())}" : $"{applicationDesktopGridSettings.GetValues("GridBmpHost").FirstOrDefault()}:{GetProxyPort(GetDesiredEnvironment())}";

        private string ProxyHost
        {
            get
            {
                string GetProxyHost()
                {
                    var proxyHost = applicationDesktopGridSettings.GetValues("SeleniumHubHost").FirstOrDefault();//Initial assignment is required for visual tests FixtureBase data (as FixtureBase initialized before tests run).

                    if (IsLocalEnvironment && _operatingSystem == OperatingSystem.Windows)
                    {
                        proxyHost = applicationDesktopGridSettings.GetValues("ProxyHost").FirstOrDefault();
                    }

                    if (!IsLocalEnvironment && _operatingSystem == OperatingSystem.Windows)
                    {
                        proxyHost = applicationDesktopGridSettings.GetValues("DockerProxyHost").FirstOrDefault();
                    }

                    var designatedProxyHost = ConfigurationManager.AppSettings["DesignatedProxyHost"];
                    return IsNullOrWhiteSpace(designatedProxyHost) ? proxyHost : designatedProxyHost;
                }

                return _proxyHost = _proxyHost ?? GetProxyHost();
            }
        }

        private int? _designatedProxyPort;
        private int DesignatedProxyPort
        {
            get
            {
                int GetDesignatedProxyPort()
                {
                    var configDesignatedProxyPort = ConfigurationManager.AppSettings["DesignatedProxyPort"];
                    int.TryParse(configDesignatedProxyPort, out var designatedProxyPort);
                    return designatedProxyPort;
                }

                return (_designatedProxyPort = _designatedProxyPort ?? GetDesignatedProxyPort()).Value;
            }
        }


        public string HubIpAddress { get; set; }
        public string HubPort { get; }

        public bool IsLocalEnvironment { get; }

        public EnvironmentResolver(EnvironmentUnderTest currentEnvironment, bool isNetworkLoggingTest, OperatingSystem operatingSystem, Log log)
        {
            _currentEnvironment = currentEnvironment;
            _isNetworkLoggingTest = isNetworkLoggingTest;
            _operatingSystem = operatingSystem;
            _log = log;

#if DebugLocal || ReleaseLocal
            IsLocalEnvironment = true;
#elif DebugGrid || ReleaseGrid
            IsLocalEnvironment = false;
#endif
            HubPort = applicationDesktopGridSettings.GetValues("SeleniumHubPort").FirstOrDefault();

            if (_operatingSystem == OperatingSystem.iPad || _operatingSystem == OperatingSystem.iPhone || _operatingSystem == OperatingSystem.Android)
            {
                if (!mobileCloudRunConfig.Equals("true"))
                {
                    HubIpAddress = applicationMobileGridSettings.GetValues("SeleniumMobileHubHost").FirstOrDefault();
                }
            }
            else
            {
                HubIpAddress = applicationDesktopGridSettings.GetValues("SeleniumHubHost").FirstOrDefault();
            }

            Initialize();
        }

        public EnvironmentResolver(EnvironmentUnderTest currentEnvironment, bool isNetworkLoggingTest)
        {
            _currentEnvironment = currentEnvironment;
            _isNetworkLoggingTest = isNetworkLoggingTest;
#if DebugLocal || ReleaseLocal
            IsLocalEnvironment = true;
#elif DebugGrid || ReleaseGrid
            IsLocalEnvironment = false;
#endif
            HubPort = applicationDesktopGridSettings.GetValues("SeleniumHubPort").FirstOrDefault();
            HubIpAddress = applicationDesktopGridSettings.GetValues("SeleniumHubHost").FirstOrDefault();

            Initialize();
        }

        public string GetDesiredEnvironment()
        {
            return _currentEnvironment == EnvironmentUnderTest.Baseline
                ? BaselineEnvironment
                : TargetEnvironment;
        }

        private string GetDnsRemappingByTestingInstance(string environment)
        {
            switch (environment)
            {
                case "A":
                case "A_Har":
                    return "172.24.104.20";
                case "B":
                case "B_Har":
                    return "172.24.105.20";
                case "C":
                case "C_Har":
                    return "172.24.106.20";
                case "D":
                case "D_Har":
                    return "172.24.107.20";
                case "E":
                case "E_Har":
                    return "172.24.108.20";
                case "F":
                case "F_Har":
                    return "172.24.109.20";
                case "G":
                case "G_Har":
                    return "172.24.104.239";
                // NOTE: I didn't consolidate H and I because they were different in the original version too.
                case "H":
                    return "172.24.110.20";
                case "I":
                    return "172.24.111.20";
                case "H_Har":
                    return "172.24.130.211";
                case "I_Har":
                    return "172.24.130.209";
                default:
                    Console.WriteLine("No port found");
                    return Empty;
            }
        }

        private void BrowserMobProxyDnsRemapping(string testingEnvironment, string hostComputerIpAddress, string defaultBrowserMobProxyPort, string browserMobProxyTargetPort, string bmpDnsRemapping)
        {
            var request = new RequestApi();

            request.GetResponse($"http://{hostComputerIpAddress}:{defaultBrowserMobProxyPort}/proxy/{browserMobProxyTargetPort}/hosts", HttpMethod.Post, bmpDnsRemapping); //Remap iOS mobile proxy port DNS 
            request.GetResponse($"http://{hostComputerIpAddress}:{defaultBrowserMobProxyPort}/proxy/{browserMobProxyTargetPort}/dns/cache", HttpMethod.Delete); //Flush iOS mobile proxy port DNS 
            _log.Message($"Instance was switched to {testingEnvironment} on BrowserMobProxy port http://{hostComputerIpAddress}:{defaultBrowserMobProxyPort}/proxy/{browserMobProxyTargetPort}");
        }

        private string GetDnsRemapping(string environment)
        {
            var getDnsRemappingByInstance = GetDnsRemappingByTestingInstance(environment);
            return $"{{\"www.lampsplus.com\" : \"{getDnsRemappingByInstance}\"}}";
        }

        public void SwitchLpInstanceIphoneVisual()
        {
            var currentEnvironment = GetDesiredEnvironment();

            if (_currentEnvironment == EnvironmentUnderTest.Baseline)
            {
                //Mobile Grid Hub; iPhone 12 Baseline
                BrowserMobProxyDnsRemapping(currentEnvironment, _mobileGridHub, _defaultBmpPort, _bmpPortBaseline, GetDnsRemapping(currentEnvironment));

                //Remote Mobile Grid; iPhone 12 Baseline
                BrowserMobProxyDnsRemapping(currentEnvironment, _mobileGridRemote, _defaultBmpPort, _bmpPortBaseline, GetDnsRemapping(currentEnvironment));
            }
            else
            {
                //Remote Mobile Grid; iPhone 12 Target
                BrowserMobProxyDnsRemapping(currentEnvironment, _mobileGridRemote, _defaultBmpPort, _bmpPortTarget, GetDnsRemapping(currentEnvironment));
            }
        }

        public void SwitchLpInstanceIpadVisual()
        {
            var currentEnvironment = GetDesiredEnvironment();

            BrowserMobProxyDnsRemapping(currentEnvironment, _currentEnvironment == EnvironmentUnderTest.Baseline ? _mobileGridHub :
                    _mobileGridRemote, _defaultBmpPort, _bmpPortIpad, GetDnsRemapping(currentEnvironment));
        }

        public void SwitchLpInstanceIphoneFunctional()
        {
            var currentEnvironment = GetDesiredEnvironment();

            //Mobile Grid Hub; iPhone 12
            BrowserMobProxyDnsRemapping(currentEnvironment, _mobileGridHub, _defaultBmpPort, _bmpPortBaseline, GetDnsRemapping(currentEnvironment));

            //Mobile Grid Hub; iPhone X
            BrowserMobProxyDnsRemapping(currentEnvironment, _mobileGridHub, _defaultBmpPort, _bmpPortTarget, GetDnsRemapping(currentEnvironment));

            //Remote Mobile Grid; iPhone 12 Baseline
            BrowserMobProxyDnsRemapping(currentEnvironment, _mobileGridRemote, _defaultBmpPort, _bmpPortBaseline, GetDnsRemapping(currentEnvironment));

            //Remote Mobile Grid; iPhone 12 Target
            BrowserMobProxyDnsRemapping(currentEnvironment, _mobileGridRemote, _defaultBmpPort, _bmpPortTarget, GetDnsRemapping(currentEnvironment));
        }

        public void SwitchLpInstanceMobile(NetworkLoggingUtility networkLoggingUtility, bool isVisualTest, bool isInstanceSwitchTest)
        {
            var currentEnvironment = GetDesiredEnvironment();

            var getDnsRemappingByInstance = GetDnsRemappingByTestingInstance(currentEnvironment);
            var dnsRemapping = $"{{\"www.lampsplus.com\" : \"{getDnsRemappingByInstance}\"}}";

            var request = new RequestApi();

            var isMobileProductionRegression = applicationMobileGridSettings.GetValues("IsProductionRegression").FirstOrDefault();

            if (!isMobileProductionRegression.Equals("true") || isInstanceSwitchTest)
            {
                var mobileGridHub = GetMobileGridHubHostAndBmpPort(networkLoggingUtility, isVisualTest);

                request.GetResponse($"http://{mobileGridHub.Host}:{_defaultBmpPort}/proxy/{mobileGridHub.BmpPort}/hosts", HttpMethod.Post, dnsRemapping); //Remap iOS mobile proxy port DNS 
                request.GetResponse($"http://{mobileGridHub.Host}:{_defaultBmpPort}/proxy/{mobileGridHub.BmpPort}/dns/cache", HttpMethod.Delete); //Flush iOS mobile proxy port DNS 
                _log.Message($"Instance was switched to {currentEnvironment} on BrowserMobProxy port http://{mobileGridHub.Host}:{_defaultBmpPort}/proxy/{mobileGridHub.BmpPort}");
            }
        }

        private (string Host, string BmpPort) GetMobileGridHubHostAndBmpPort(NetworkLoggingUtility networkLoggingUtility, bool isVisualTest)
        {
            var hubHost = string.Empty;
            var browserMobProxyPort = string.Empty;
            if (_operatingSystem == OperatingSystem.iPad)
            {
                //Selenium Hub host detection
                hubHost = networkLoggingUtility.GetGridNodeHost();

                if (networkLoggingUtility.GetMobileGridNodePort() == applicationMobileGridSettings.GetValues("MobileGridNodeIpadFunctionalSecondPort").FirstOrDefault()
                    || networkLoggingUtility.GetMobileGridNodePort() == applicationMobileGridSettings.GetValues("MobileGridNodeIpadFunctionalFourthPort").FirstOrDefault())
                {
                    hubHost = applicationMobileGridSettings.GetValues("SeleniumMobileHubHostAlternative").FirstOrDefault(); //Do BMP LP instance switch on Alternative Mac
                }

                //BrowserMobProxyPort detection
                browserMobProxyPort = applicationMobileGridSettings.GetValues("MobileGridThirdBmpPort").FirstOrDefault();

            }
            else //If not iPad
            {
                //Selenium Hub host detection
                if (!isVisualTest && (networkLoggingUtility.GetMobileGridNodePort() == applicationMobileGridSettings.GetValues("MobileGridNodeFunctionalThirdPort").FirstOrDefault()
                                      || networkLoggingUtility.GetMobileGridNodePort() == applicationMobileGridSettings.GetValues("MobileGridNodeFunctionalFourthPort").FirstOrDefault()))
                {
                    hubHost = applicationMobileGridSettings.GetValues("SeleniumMobileHubHostAlternative").FirstOrDefault();
                }
                else
                {
                    hubHost = networkLoggingUtility.GetGridNodeHost();
                }

                //BrowserMobProxyPort detection
                if (networkLoggingUtility.GetMobileGridNodePort() == applicationMobileGridSettings.GetValues("MobileGridNodeFirstPort").FirstOrDefault()
                    || networkLoggingUtility.GetMobileGridNodePort() == applicationMobileGridSettings.GetValues("MobileGridNodeFunctionalFirstPort").FirstOrDefault()
                    || networkLoggingUtility.GetMobileGridNodePort() == applicationMobileGridSettings.GetValues("MobileGridNodeFunctionalThirdPort").FirstOrDefault())
                {
                    browserMobProxyPort = applicationMobileGridSettings.GetValues("MobileGridBmpPort").FirstOrDefault();
                }
                else
                {
                    browserMobProxyPort = applicationMobileGridSettings.GetValues("MobileGridSecondBmpPort").FirstOrDefault();
                }
            }

            return (hubHost, browserMobProxyPort);
        }

        private void Initialize()
        {
            if (_isInitialized) { return; }
            lock (_lock)
            {
                if (_isInitialized) { return; }
                SetEnvironments();
                _isInitialized = true;
            }
        }

        private void SetEnvironments()
        {
            // content will be {targetEnvironment}.{baselineEnvironment}, but baselineEnvironment is optional e.g D.B
            var environments = ConfigurationManager.AppSettings["TargetInstance"].Split('.');

            if (_currentEnvironment == EnvironmentUnderTest.Baseline && environments.Length <= 1)
                throw new EnvironmentNotFoundException("Visualization Baseline environment is not provided");

            if (environments.Length > 1)
            {
                TargetEnvironment = environments[1].Trim();
                BaselineEnvironment = environments[0].Trim();
            }
            else
            {
                TargetEnvironment = environments[0];
            }
        }

        private string GetProxyPort(string testEnvironment) => DesignatedProxyPort != 0
                                                                ? DesignatedProxyPort.ToString()
                                                                : (_isNetworkLoggingTest
                                                                    ? Ports[$"{testEnvironment}_Har"]
                                                                    : Ports[testEnvironment]);

        private string GetProxyAddress(string testEnvironment) => _isNetworkLoggingTest && !IsLocalEnvironment ? $"{_bmpDataCaptureInstanceHost}:{GetProxyPort(testEnvironment)}" : $"{ProxyHost}:{GetProxyPort(testEnvironment)}";
    }
}
