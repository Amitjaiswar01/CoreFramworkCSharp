using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using Xunit.Abstractions;
using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Core;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Databases.Actions;
using LampsPlus.AutomationFramework.Pages.Refactored.Address;
using LampsPlus.AutomationFramework.Services;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Environment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Address = LampsPlus.AutomationFramework.Pages.Refactored.Address;
using OperatingSystem = LampsPlus.AutomationFramework.Utilities.TestConfiguration.OperatingSystem;

//#if DebugLocal || ReleaseLocal
//[assembly: CollectionBehavior(MaxParallelThreads = 8)]
//#endif

namespace LampsPlus.AutomationFramework.TestsBaseRefactored
{
    /// <summary>
    /// Base class for all XUnit tests.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class TestsBase : IDisposable
    {
        #region Private fields and properties

        /// <summary>
        /// A string that is persistent in the source throughout the entire site.
        /// Used for verifying server error pages.
        /// </summary>
        private string _persistentStringInSource = "lamps plus";
        private string _testTagName => TestSetup.TestTagName;
        private int _desiredViewPortWidth => TestSetup.DesiredViewPortWidth;

        /// <summary>
        /// Default seconds to wait for Selenium elements in production.
        /// </summary>
        public int ImplicitWaitTime = 70;

        /// <summary>
        /// Request API private class members
        /// </summary>
        private readonly string[] _localIpAddresses = ConfigurationManager.AppSettings["LocalIpAddresses"].Split(',');
        private RequestApi _requestApi;

        /// <summary>
        /// Member reference of the public Test property.
        /// </summary>
        private ITest _test;

        private CookieUtility _cookieUtility;
        private IAddress _address;
        private IIntAddress _intAddress;
        private IRandomAddressGenerator _randomAddressGenerator;

        #endregion

        #region Protected class fields and properties

        /// <summary>
        /// Name of the current method under test.
        /// </summary>
        protected string TestName => $"{GetType().FullName}.{TestCase?.TestCase.TestMethod.Method.Name}";
        protected bool IsVisualTest;
        protected bool IsLpInstanceSwitchForMobileTest { get; set; }

        #endregion

        #region Public fields and properties

        /// <summary>
        /// Fix version of target website
        /// </summary>
        public string TargetFixVersion { get; set; }
        /// <summary>
        /// Fix version of baseline website
        /// </summary>
        public string BaselineFixVersion { get; set; }
        /// <summary>
        /// Get the current time formatted yyyyMMddHHmmssffff.
        /// </summary>
        public string CurrentDateTime => Log.FormatDateTime(DateTime.Now);

        public string RecurringDataIssue => "Recurring Data Issue: ";

        #endregion

        #region Public class flags
        /// <summary>
        /// Flag to determine if the test configuration has been successfully ran.
        /// </summary>
        public bool IsTestConfigurationSet { get; private set; }

        /// <summary>
        /// Flag to determine if environment DbClust.
        /// </summary>
        public static bool IsDbClustRefactored { get; private set; }

        /// <summary>
        /// Should the driver be closed after the test has completed?
        /// </summary>
        public bool DisposeOfBrowserAfterTest { get; private set; }

        public bool EmptyCart { get; private set; }
        #endregion

        #region Public class instances (objects)

        /// <summary>
        /// Public Test properties (objects)
        /// </summary>
        public CookieUtility CookieUtility => _cookieUtility ?? (_cookieUtility = new CookieUtility(Browser,Assert));
        public RequestApi RequestApi => _requestApi ?? (_requestApi = new RequestApi(_localIpAddresses));
        public SessionSettings Settings { get; private set; }
        public IAddress Address => _address ?? (_address = new Address.Address());
        public IIntAddress IntAddress => _intAddress ?? (_intAddress = new Address.Address.IntAddress());
        public IRandomAddressGenerator RandomAddressGenerator => _randomAddressGenerator ?? (_randomAddressGenerator = new Address.Address.RandomAddressGenerator());

        /// <summary>
        /// Test ITestOutputHelper instance.
        /// </summary>
        public ITestOutputHelper OutputHelper { get; }

        /// <summary>
        /// Log class to provide common logging format.
        /// </summary>
        public Log Log { get; }

        /// <summary>
        /// Provides access to Selenium.
        /// </summary>
        public IBrowser Browser { get; set; }

        /// <summary>
        /// Provides access to ScreenCapturer.
        /// </summary>
        public IScreenCapturer ScreenCapturer { get; set; }

        /// <summary>
        /// Type of device used in the test.
        /// </summary>
        public OperatingSystem OperatingSystem => TestSetup.TestConfiguration.OperatingSystem;

        /// <summary>
        /// Browser used in the test.
        /// </summary>
        public WebBrowser WebBrowser => TestSetup.TestConfiguration.Browser;

        /// <summary>
        /// User role used in the test.
        /// </summary>
        public UserRole UserRole => TestSetup.TestConfiguration.UserRole;

        public IAssert Assert { get; private set; }

        /// <summary>
        /// Provides access to library of account actions that can be used to retrieve data from the database.
        /// </summary>
        public AccountActions AccountActions { get; private set; }

        /// <summary>
        /// Provides access to library of product actions that can be used to retrieve data from the database.
        /// </summary>
        public ProductActions ProductActions { get; private set; }

        /// <summary>
        /// Provides access to library of order actions that can be used to retrieve data from the database.
        /// </summary>
        public OrderActions OrderActions { get; private set; }

        /// <summary>
        /// Provides access to library of sort actions that can be used to retrieve data from the database.
        /// </summary>
        public SortActions SortActions { get; private set; }

        /// <summary>
        /// Provides access to library of shopping cart actions that can be used to retrieve data from the database.
        /// </summary>
        public ShoppingCartActions ShoppingCartActions { get; private set; }

        /// <summary>
        /// Provides advanced test setup and teardown capabilities. 
        /// </summary>
        public TestSetup TestSetup { get; set; }

        public EnvironmentResolver EnvironmentResolver { get; private set; }
        public DevEnvInformation DevEnvInformation { get; private set; }
        public NetworkLoggingUtility NetworkLoggingUtility { get; private set; }
        public DataCaptureUtility DataCaptureUtility { get; private set; }

        /// <summary>
        /// Get the ITest object for the current test.
        /// </summary>
        public ITest TestCase
        {
            get
            {
                if (_test == null)
                {
                    _test = (ITest)OutputHelper.GetType().GetField("test", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(OutputHelper);
                }

                return _test;
            }
        }

        #endregion

        #region Constructor

        public TestsBase(ITestOutputHelper output, bool enableRealTimeLogging = false)
        {
            OutputHelper = output;

            Log = new Log(OutputHelper, TestName, enableRealTimeLogging);
        }

        #endregion
        
        #region Private methods

        /// <summary>
        /// Move focus to the requested element and take a screenshot.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnReadyToMoveToEvent(object sender, AssertBase.WebElementEventArgs e)
        {
            Browser.MouseOverOnElement(new Element(e.Element.InternalElement, Log, string.Empty, LocatorStrategy.Js));
            Browser.TakeScreenshot(string.Empty, true);
        }

        private void InitializeSessionSettings(bool IsVisualTest)
        {
            Settings = new SessionSettings
            {
                IsBaseLine = TestSetup.TestConfiguration.IsBaseLine,

                IsMobileView = TestSetup.TestConfiguration.IsMobileView,

                IsTabletView = TestSetup.TestConfiguration.IsTabletView,

                IsTabletEmulationView = TestSetup.TestConfiguration.IsTabletEmulationView, //Verifies if Chrome tablet emulation test

                IsLocalEnvironment = EnvironmentResolver.IsLocalEnvironment,
                Browser = WebBrowser,
                ProxyAddress = EnvironmentResolver.ProxyIpAddress,

                HubIpAddress = EnvironmentResolver.HubIpAddress,
                HubPort = EnvironmentResolver.HubPort,

                IsVisualTest = IsVisualTest,

                TargetInstance = EnvironmentResolver.TargetEnvironment,

                BaselineInstance = EnvironmentResolver.BaselineEnvironment,

                SettingsTestName = TestName
            };

            var _applicationMobileGridSettings = ConfigurationManager.GetSection("GridGroup/MobileGrid") as NameValueCollection;
            var MobileHubHost = _applicationMobileGridSettings.GetValues("SeleniumMobileHubHost").FirstOrDefault();

            if (OperatingSystem == OperatingSystem.iPhone)
            {
                var MobileHubAlternativeHost = _applicationMobileGridSettings.GetValues("SeleniumMobileHubHostAlternative").FirstOrDefault();
                var IsMobileProductionRegression = _applicationMobileGridSettings.GetValues("IsProductionRegression").FirstOrDefault();

                if (IsMobileProductionRegression.Equals("true"))
                {
                    Settings.HubIpAddress = IsVisualTest ? MobileHubAlternativeHost : MobileHubHost;
                }
            }

            if (OperatingSystem == OperatingSystem.iPad)
            {
                var tabletHubPort = _applicationMobileGridSettings.GetValues("ProxyPortTablet").FirstOrDefault();

                if (IsVisualTest)
                {
                    tabletHubPort = _applicationMobileGridSettings.GetValues("ProxyVisualPortTablet").FirstOrDefault();
                }

                Settings.HubIpAddress = MobileHubHost;
                Settings.HubPort = tabletHubPort;
            }

            if (OperatingSystem == OperatingSystem.Android)
            {
                Settings.MobileDevice = LampsPlusMobileDevices.MotoX;
            }
            if (OperatingSystem == OperatingSystem.iPad)
            {
                Settings.MobileDevice = LampsPlusMobileDevices.iPadPro;
            }
            else if (OperatingSystem == OperatingSystem.iPhone)
            {
                Settings.MobileDevice = LampsPlusMobileDevices.iPhone;
            }
        }

        private void InitializeActions(DatabaseConnectionStringsManager connectionStringsManager)
        {
            AccountActions = new AccountActions(connectionStringsManager.CartEasyConnectionString);
            ProductActions = new ProductActions(connectionStringsManager.CartEasyConnectionString,
                                                connectionStringsManager.ProductsConnectionString,
                                                connectionStringsManager.ProdutMicroServicesConnectionString);
            OrderActions = new OrderActions(connectionStringsManager.CartEasyConnectionString,
                                            connectionStringsManager.AssetsConnectionString,
                                            connectionStringsManager.DomExportOrderConnectionString,
                                            connectionStringsManager.UserProfileConnectionString);
            SortActions = new SortActions(connectionStringsManager.AssetsConnectionString, connectionStringsManager.ProductsConnectionString, connectionStringsManager.CartEasyConnectionString);
            ShoppingCartActions = new ShoppingCartActions(connectionStringsManager.CartEasyConnectionString,
                                                          connectionStringsManager.AssetsConnectionString);
        }

        private void InitializeBrowser(bool visualTest)
        {
            if (OperatingSystem == OperatingSystem.Android)
            {
                Browser = new MobileBrowser(TestSetup.TestConfiguration.Device, Log, TestName, _testTagName, _desiredViewPortWidth, Settings, ImplicitWaitTime, _persistentStringInSource, visualTest, DisposeOfBrowserAfterTest);
            }

            else if (OperatingSystem == OperatingSystem.iPhone)
            {
                Browser = new IphoneBrowser(TestSetup.TestConfiguration.Device, Log, TestName, _testTagName, _desiredViewPortWidth, Settings, ImplicitWaitTime, _persistentStringInSource, visualTest, DisposeOfBrowserAfterTest);
            }

            else if (OperatingSystem == OperatingSystem.iPad)
            {
                Browser = new IpadBrowser(TestSetup.TestConfiguration.Device, Log, TestName, _testTagName, _desiredViewPortWidth, Settings, ImplicitWaitTime, _persistentStringInSource, visualTest, DisposeOfBrowserAfterTest);
            }

            else if (WebBrowser == WebBrowser.ChromeMobileView)
            {
                Browser = new MobileBrowser(WebBrowser, Log, TestName, _testTagName, _desiredViewPortWidth, Settings, ImplicitWaitTime, _persistentStringInSource, visualTest, DisposeOfBrowserAfterTest);
            }

            else if (OperatingSystem == OperatingSystem.Windows || OperatingSystem == OperatingSystem.Mac) // Desktop configuration.
            {
                Browser = new Browser(WebBrowser, Log, TestName, _testTagName, _desiredViewPortWidth, Settings, ImplicitWaitTime, _persistentStringInSource, visualTest, DisposeOfBrowserAfterTest);
            }

            else
            {
                Browser = new Browser(WebBrowser, Log, TestName, _testTagName, _desiredViewPortWidth, Settings, ImplicitWaitTime, _persistentStringInSource, visualTest, DisposeOfBrowserAfterTest);
            }
        }

        private void InitializeAssert()
        {
            Assert = new Assert(Browser);

            // Initialize events to get notified when Browser behavior is needed by a Verify statement.
            Assert.ReadyToMoveToEventHandler += OnReadyToMoveToEvent;
        }
        #endregion

        #region Protected methods
        /// <summary>
        /// Log all xUnit traits.
        /// </summary>
        protected void LogTraits()
        {
            Log.Header("Traits");

            foreach (var trait in TestCase.TestCase.Traits)
            {
                foreach (var val in trait.Value)
                {
                    Log.Message($"{trait.Key} : {val}", false);
                }
            }

            Log.Footer();
        }

        #endregion

        #region Public methods (contains entry point method InitializeFramework() and Dispose() method)

        /// <summary>
        /// Initialize a test framework based on the given configuration and optional initial URL to navigate to.
        /// </summary>
        /// <param name="config">Environment configuration used by the test.</param>
        /// <param name="url">Optional parameter: Initial URL to navigate to after framework initialization.</param>
        /// <param name="disposeBrowserAfterTest">Optional parameter: Dispose of the browser and driver after the test has completed when true.</param>
        /// <param name="skipGlobalSetup">Optional parameter: Skip the global setup Lamps Plus setup when true.</param>
        /// <param name="skipHomePageNav">Optional parameter: Skip the navigation to LP home page when true.</param>
        /// <param name="emptyCart">Optional parameter: Empties LP shopping cart when true.</param>
        /// <param name="visualTestAccount">Optional parameter: Visual test when true.</param>
        /// <param name="setup">Optional parameter: TestSetup setup when not null.</param>
        /// <param name="isInstanceSwitchMobile">Optional parameter: Is Bamboo pre-condition step (LP testing instance switch).</param>
        public void InitializeFramework(string config, string url = "", bool disposeBrowserAfterTest = true, bool skipGlobalSetup = false, bool skipHomePageNav = false, bool emptyCart = false, bool visualTestAccount = false, TestSetup setup = null , bool isInstanceSwitchMobile = false)
        {
            TestSetup = setup ?? new TestSetup(config, url);

            DisposeOfBrowserAfterTest = disposeBrowserAfterTest;

            IsVisualTest = visualTestAccount;

            EmptyCart = emptyCart;

            IsLpInstanceSwitchForMobileTest = isInstanceSwitchMobile;

            Log.Header("Begin Framework Initialization");

            EnvironmentResolver = new EnvironmentResolver(TestSetup.TestConfiguration.EnvironmentUnderTest, TestSetup.IsNetworkLoggingTest, TestSetup.TestConfiguration.OperatingSystem, Log);

            InitializeSessionSettings(visualTestAccount);

            if (IsLpInstanceSwitchForMobileTest)
            {
                SwitchMobileGridEnvironmentalTestingInstance(visualTestAccount);

                return;   //Exit method if LP instance switch mobile test.
            }

            InitializeBrowser(visualTestAccount);
            InitializeAssert();
            NetworkLoggingUtility = new NetworkLoggingUtility(Browser, Assert, Settings, OperatingSystem, RequestApi, EnvironmentResolver, Log);

            DevEnvInformation = new DevEnvInformation(TestSetup.TestConfiguration.EnvironmentUnderTest, new DenvPageParser(Browser, Settings));
            Browser.SiteVersion = DevEnvInformation.FixVersion;
            Browser.IsProdInstance = DevEnvInformation.IsProductionInstance;
            DevEnvInformation.LogInformation(Log);

            DataCaptureUtility = new DataCaptureUtility(Browser, Assert, NetworkLoggingUtility);

            Log.Message($"DatabaseConnectionString:{DevEnvInformation.DatabaseString}");
            InitializeActions(new DatabaseConnectionStringsManager(DevEnvInformation.DatabaseString));

            //TODO Check if DbClust 
            IsDbClustRefactored = DevEnvInformation.DatabaseString.Equals("clust");
            Log.Message($"Is dbClust: {IsDbClustRefactored}");

            IsTestConfigurationSet = true; // Set flag to true to indicate the InitializeFramework has completed.
            Log.Message("Framework Initialization Complete");

            ClearNetworkLogIfLoggingTest();

            if (Browser.Device != null && (Browser.Device.IsIphone || Browser.Device.IsPad))
            {
                Browser.ClearBrowserSession(Urls.DevEnvPageUrl);
            }

            Log.Header("Begin Test Case");

            if (visualTestAccount) //Exit method if visualTestAccount
            {
                return;
            }

            TestSetup.AccountSetup(); //Regression Account setup based on 'IsDbClust': DBclust or DBtest
        }

        /// <summary>
        /// Teardown method to close the WebDriver and cleanup unused resources.
        /// </summary>
        public virtual void Dispose()
        {
        }

        private void SwitchMobileGridEnvironmentalTestingInstance(bool isVisualAccount)
        {
            var testTraitValue = TestCase?.TestCase.Traits.Values.SelectMany(list => list).Distinct().ToList().First();
            switch (testTraitValue)
            {
                case LpTraits.Unit.SwitchLpInstance:
                    EnvironmentResolver.SwitchLpInstanceIphoneFunctional();
                    break;
                case LpTraits.Unit.SwitchLpInstanceIphoneVisual:
                    EnvironmentResolver.SwitchLpInstanceIphoneVisual();
                    break;
                case LpTraits.Unit.SwitchLpInstanceIpadVisual:
                    EnvironmentResolver.SwitchLpInstanceIpadVisual();
                    break;
                default:
                    EnvironmentResolver.SwitchLpInstanceMobile(NetworkLoggingUtility, isVisualAccount, IsLpInstanceSwitchForMobileTest);
                    break;
            }
        }

        /// <summary>
        /// Clears network log in Browser Mob Proxy if current test is a logging test.
        /// </summary>
        public void ClearNetworkLogIfLoggingTest()
        {
            if (!TestSetup.IsNetworkLoggingTest) return;

            Log.Message("Network HAR Log Cleared.");

            NetworkLoggingUtility.ClearNetworkLog();
        }

        /// <summary>
        /// Log the page source (DOM) for the current page.
        /// </summary>
        public void LogPageSource() { Log.LogPageSource(Browser.PageSource); }

        /// <summary>
        /// Is an element present in the DOM and take a screenshot.
        /// </summary>
        /// <param name="cssSelector">CSS Selector to locate an element by.</param>
        /// <param name="isCheckImmediate">When true do not wait for the element to be located.</param>
        public bool IsElementPresent(string cssSelector, bool isCheckImmediate = false)
        {
            var isElementPresent = false;

            if (!isCheckImmediate)
            {
                if (Browser.Locate.ElementBySelector(cssSelector) != null)
                {
                    Browser.MouseOverOnElement(Browser.Locate.ElementBySelector(cssSelector));
                    Browser.TakeScreenshot();

                    isElementPresent = true;
                }
            }
            else { isElementPresent = Browser.Locate.ElementImmediately(cssSelector).IsInitialized; }

            return isElementPresent;
        }

        public static IElement GetElementByElementText(IElement parentElement, string control, string text)
        {
            return parentElement.FindElement(By.XPath("//" + control + "[.='" + text + "']"));
        }

        public static IEnumerable<object[]> RepeatFunctionalTest(string config) => Enumerable.Range(1, 10).Select(x => new object[] { config }).ToList();
        #endregion
    }
}
