using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using Automation.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Automation.Framework.Utilities;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Utilities.Environment;
using OperatingSystem = LampsPlus.AutomationFramework.Utilities.TestConfiguration.OperatingSystem;

namespace LampsPlus.AutomationFramework.Utilities
{
    /// <summary>
    /// Network logging helper methods
    /// </summary>
    public class NetworkLoggingUtility
    {
        private static string _harApiUrl;
        private static string _harApiUrlSecondPort;//HAR file for iPad
        private const string RequestString = "request";
        private const string QueryString = "queryString";
        private const string PostData = "postData";
        private const string Text = "text";
        private const string Get = "GET";
        private const int MaximumMilliSecondsToWait = 30000;

        private static object _lock = new object();
        private static bool _isInitialized;
        private static readonly NameValueCollection _applicationMobileGridSettings = ConfigurationManager.GetSection("GridGroup/MobileGrid") as NameValueCollection;
        private static readonly NameValueCollection _applicationDesktopGridSettings = ConfigurationManager.GetSection("GridGroup/DesktopGrid") as NameValueCollection;

        private bool IsMobileCloud => ConfigurationManager.AppSettings["MobileGridCloud"].Equals("true");

        
        private string HubPort = _applicationDesktopGridSettings.GetValues("SeleniumHubPort").FirstOrDefault();
        private readonly string HubHost = _applicationDesktopGridSettings.GetValues("SeleniumHubHost").FirstOrDefault();
        private readonly string MobileHubHost = _applicationMobileGridSettings.GetValues("SeleniumMobileHubHost").FirstOrDefault();
        private readonly string MobileHubAlternativeHost = _applicationMobileGridSettings.GetValues("SeleniumMobileHubHostAlternative").FirstOrDefault();
        private readonly string MobileHubAppiumNode = _applicationMobileGridSettings.GetValues("SeleniumMobileHubAppiumNode").FirstOrDefault();
        public readonly string MobileGridNodeFirstPort = _applicationMobileGridSettings.GetValues("MobileGridNodeFirstPort")?.FirstOrDefault();
        public readonly string MobileGridNodeSecondPort = _applicationMobileGridSettings.GetValues("MobileGridNodeSecondPort")?.FirstOrDefault();
        private readonly string MobileGridDefaultBmpPort = _applicationMobileGridSettings.GetValues("DefaultBmpProxyPort")?.FirstOrDefault();
        public readonly string MobileGridNodeFirstBmpPort = _applicationMobileGridSettings.GetValues("MobileGridBmpPort")?.FirstOrDefault();
        public readonly string MobileGridNodeSecondBmpPort = _applicationMobileGridSettings.GetValues("MobileGridSecondBmpPort")?.FirstOrDefault();
        public readonly string MobileGridNodeThirdBmpPort = _applicationMobileGridSettings.GetValues("MobileGridThirdBmpPort")?.FirstOrDefault();
        private readonly string DefaultBmpProxy = _applicationDesktopGridSettings.GetValues("ProxyPort")?.FirstOrDefault();
        private readonly string ProxyHost = _applicationDesktopGridSettings.GetValues("ProxyHost")?.FirstOrDefault();
        private readonly string BmpDataCaptureInstanceHost = _applicationDesktopGridSettings.GetValues("DataCaptureBmpInstanceHost")?.FirstOrDefault();

        //TestsBase instances
        private readonly IBrowser _browser;
        private readonly IAssert _assert;
        private readonly SessionSettings _settings;
        private readonly OperatingSystem _operatingSystem;
        private readonly RequestApi _requestApi;
        private readonly EnvironmentResolver _environmentResolver;
        private readonly Log _log;

        public NetworkLoggingUtility(IBrowser browser, IAssert assert, SessionSettings settings, OperatingSystem operatingSystem, RequestApi requestApi,
            EnvironmentResolver environmentResolver, Log log)
        {
            _browser = browser;
            _assert = assert;
            _operatingSystem = operatingSystem;
            _log = log;
            _settings = settings;
            _operatingSystem = operatingSystem;
            _requestApi= requestApi;
            _environmentResolver = environmentResolver;
            _log = log;

            Initialize();
        }

        /// <summary>
        /// Get Grid node host address
        /// </summary>
        public string GetGridNodeHost()
        {
            //Get Grid node session id
            var sessionId = _browser.GridNodeSessionId;
            var applicationMobileGridSettings = ConfigurationManager.GetSection("GridGroup/MobileGrid") as NameValueCollection;

            if (!_settings.IsVisualTest)
            {
                //If Functional iPhone Daily build, switch Hub port
                if (ConfigurationManager.AppSettings["IphoneDailyBuild"].Contains("true"))
                {
                    HubPort = applicationMobileGridSettings.GetValues("FunctionalIphoneBuildHubPort").FirstOrDefault();
                }
                if (_operatingSystem.Equals(OperatingSystem.iPad) && !_settings.IsVisualTest)
                {
                    HubPort = applicationMobileGridSettings.GetValues("ProxyPortTablet").FirstOrDefault();
                }

            }
            else if (_operatingSystem.Equals(OperatingSystem.iPad) && _settings.IsVisualTest)
            {
                HubPort = applicationMobileGridSettings.GetValues("ProxyVisualPortTablet").FirstOrDefault();

            }

            var nodeUrl = _operatingSystem.Equals(OperatingSystem.Windows)
                    ? $"http://{HubHost}:{HubPort}/grid/api/testsession?session={sessionId}"
                    : $"http://{_settings.HubIpAddress}:{HubPort}/grid/api/testsession?session={sessionId}";

            var request = new RequestApi();
            var response = request.GetResponse(nodeUrl, HttpMethod.Get);
            dynamic jsonDataContent = JsonConvert.DeserializeObject(response.Content);
            //Get Node host
            var myUri = new Uri(jsonDataContent.proxyId.ToString());

            var parentHost = myUri.Host.Contains(HubHost) ? HubHost : myUri.Host.Contains(MobileHubHost) ? MobileHubHost :
                _settings.HubIpAddress.Contains(MobileHubHost) ? MobileHubHost : _settings.HubIpAddress.Contains(MobileHubAlternativeHost) ? MobileHubAlternativeHost :
                !_settings.HubIpAddress.Contains("lpseldocker") ? HubHost : myUri.Host;

            return parentHost;
        }

        /// <summary>
        /// Get Mobile Grid node port
        /// </summary>
        public string GetMobileGridNodePort()
        {
            //Get Grid node session id
            var sessionId = _browser.GridNodeSessionId;
            var nodeUrl = $"http://{_settings.HubIpAddress}:{HubPort}/grid/api/testsession?session={sessionId}";

            var request = new RequestApi();
            var response = request.GetResponse(nodeUrl, HttpMethod.Get);
            dynamic jsonDataContent = JsonConvert.DeserializeObject(response.Content);

            //Get Node port
            var myUri = new Uri(jsonDataContent.proxyId.ToString());
            return myUri.Port.ToString();
        }

        private void Initialize()
        {
            if (_isInitialized) return;

            lock (_lock)
            {
                if (_browser.Device != null)
                {
                    if (_browser.Device.IsIphone)
                    {
                        _harApiUrl = !IsMobileCloud
                            ? $"http://{GetGridNodeHost()}:{MobileGridDefaultBmpPort}/proxy/{MobileGridNodeFirstBmpPort}/har"
                            : $"http://{ProxyHost}:{DefaultBmpProxy}/proxy/{_environmentResolver.ProxyPort}/har";
                    }
                    else if (_browser.Device.IsPad)
                    {
                        _harApiUrlSecondPort = !IsMobileCloud
                            ? $"http://{MobileHubHost}:{MobileGridDefaultBmpPort}/proxy/{MobileGridNodeSecondBmpPort}/har"
                            : $"http://{ProxyHost}:{DefaultBmpProxy}/proxy/{_environmentResolver.ProxyPort}/har";
                    }
                }
                else
                {
                    _harApiUrl = _settings.IsLocalEnvironment
                        ? $"http://{ProxyHost}:{DefaultBmpProxy}/proxy/{_environmentResolver.ProxyPort}/har"
                        : $"http://{BmpDataCaptureInstanceHost}:{DefaultBmpProxy}/proxy/{_environmentResolver.ProxyPort}/har";
                }

                _isInitialized = true;
            }
        }

        /// <summary>
        /// Clear and reset HAR log information in Browser Mob Proxy.
        /// </summary>
        public void ClearNetworkLog()
        {
            if (_browser.Device != null)
            {
                if (_browser.Device.IsIphone && !IsMobileCloud)
                {
                    _requestApi.PutRestRequest($"{_harApiUrl}?captureContent=true");
                }
                else if (_browser.Device.IsPad && !IsMobileCloud)
                {
                    _requestApi.PutRestRequest($"{_harApiUrlSecondPort}?captureContent=true");
                }
            }
            else
            {
                _requestApi.PutRestRequest($"{_harApiUrl}?captureContent=true");
            }
        }

        /// <summary>
        /// Get HAR log information from Browser Mob Proxy.
        /// </summary>
        /// <returns>Deserialized dictionary of the returned content from the REST call.</returns>
        public Dictionary<string, dynamic> GetNetworkLog()
        {
            if (_browser.Device != null)
            {
                if (_browser.Device.IsIphone && !IsMobileCloud)
                {
                    return _requestApi.GetRestRequest(_harApiUrl);

                }

                if (_browser.Device.IsPad && !IsMobileCloud)
                {
                    return _requestApi.GetRestRequest(_harApiUrlSecondPort);
                }
            }

            return _requestApi.GetRestRequest(_harApiUrl);
        }

        /// <summary>
        /// Find request entries where urlContainedString is contained in the url, and each queryParams pair is contained in the requests' query string parameters.
        /// </summary>
        /// <param name="urlContainedString">A URL, or part of a URL, that needs to be contained in the request URL.</param>
        /// <param name="queryParams">A dictionary of all the expected query parameter key value pairs that should appear in a single requests' query string parameters.</param>
        /// <param name="caseInsensitive">Flag determining case insensitivity for query string value equality comparison.</param>
        /// <param name="maximumMilliSecondsToWait">Maximum seconds to wait for the request before returning the result.</param>
        /// <returns>Boolean if a request exists that contains the passed url, and all the passed query paramaters.</returns>
        public bool RequestHasQueryParams(string urlContainedString, Dictionary<string, string> queryParams, bool caseInsensitive = false)
        {
            var caseSensitivity = caseInsensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;

            return SpinWait.SpinUntil(() =>
            {
                var entries = GetLogNetworkEntries();

                var findEntry = entries.Where(e =>
                {

                    if (e[RequestString]["method"].ToString() == Get || e[RequestString][PostData]?[Text] == null)

                    {
                        return e[RequestString]["url"].ToString().ToLower().Contains(urlContainedString.ToLower()) &&
                               queryParams.All(expectedParamPair =>
                               {
                                   return e[RequestString][QueryString].Any(paramPair => string.Equals(paramPair["name"]?.ToString(), expectedParamPair.Key, caseSensitivity) &&
                                       string.Equals(paramPair["value"]?.ToString(), expectedParamPair.Value, caseSensitivity));
                               });
                    }
                    else
                    {
                        return e[RequestString][PostData] != null && e[RequestString][PostData][Text] != null && e[RequestString][PostData][Text].ToString().ToLower().Contains(urlContainedString.ToLower()) && queryParams.All(expectedParamPair =>
                        {
                            if (caseInsensitive)
                            {
                                string expectedParamString = $"{Uri.EscapeDataString(expectedParamPair.Key.ToLower())}={Uri.EscapeDataString(expectedParamPair.Value.ToLower())}";
                                return e[RequestString][PostData][Text].ToString().ToLower().Contains(expectedParamString);
                            }
                            else
                            {
                                string expectedParamString = $"{Uri.EscapeDataString(expectedParamPair.Key)}={Uri.EscapeDataString(expectedParamPair.Value)}";
                                return e[RequestString][PostData][Text].ToString().Contains(expectedParamString);
                            }

                        });
                    }
                });

                return findEntry.Any();
            }, MaximumMilliSecondsToWait);
        }

        /// <summary>
        /// Find request entries where urlContainedString is contained in the url, and each queryKeys key is contained in the requests' query string parameters.
        /// </summary>
        /// <param name="urlContainedString">A URL, or part of a URL, that needs to be contained in the request URL.</param>
        /// <param name="queryKeys">A dictionary of all the expected query parameter keys that should appear in a single requests' query string parameters.</param>
        /// <param name="caseInsensitive">Flag determining case insensitivity for query string value equality comparison.</param>
        /// <param name="maximumMilliSecondsToWait">Maximum seconds to wait for the request before returning the result.</param>
        /// <returns>Boolean if a request exists that contains the passed url, and all the passed query keys.</returns>
        public bool RequestHasQueryKeys(string urlContainedString, List<string> queryKeys, bool caseInsensitive = false)
        {
            var caseSensitivity = caseInsensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;

            return SpinWait.SpinUntil(() =>
            {
                var entries = GetLogNetworkEntries();

                var findEntry = entries.Where(e =>
                {
                    if (e[RequestString]["method"].ToString() == Get)
                    {
                        return
                            e[RequestString]["url"].ToString().ToLower().Contains(urlContainedString.ToLower()) &&
                            queryKeys.All(expectedKey =>
                            {
                                return e[RequestString][QueryString].Any(paramPair => string.Equals(paramPair["name"]?.ToString(), expectedKey, caseSensitivity));
                            });
                    }
                    else
                    {
                        return e[RequestString][PostData] != null && e[RequestString][PostData][Text] != null && e[RequestString][PostData]["text"].ToString().ToLower().Contains(urlContainedString.ToLower()) && queryKeys.All(expectedKey =>
                        {
                            if (caseInsensitive)
                            {
                                string expectedKeyString = $"{Uri.EscapeDataString(expectedKey.ToLower())}=";
                                return e[RequestString][PostData][Text].ToString().ToLower().Contains(expectedKeyString);
                            }
                            else
                            {
                                string expectedKeyString = $"{Uri.EscapeDataString(expectedKey)}=";
                                return e[RequestString][PostData][Text].ToString().Contains(expectedKeyString);
                            }

                        });
                    }
                });

                return findEntry.Any();
            }, MaximumMilliSecondsToWait);
        }

        /// <summary>
        /// Find the number of products in the request where urlContainedString is contained in the url.
        /// </summary>
        /// <param name="urlContainedString">A URL, or part of a URL, that needs to be contained in the request URL.</param>
        /// <param name="productPrefix">A string indicating how to find the product keys within the request</param>
        /// <param name="maximumMilliSecondsToWait">Maximum seconds to wait for the request before returning the result.</param>
        /// <returns>Int indicating the number of products found in the request indicated by the URL or partial URL.</returns>
        public int GetNumberOfProductsInRequest(string urlContainedString, string productPrefix)
        {
            List<JToken> products = new List<JToken>();

            SpinWait.SpinUntil(() =>
            {
                var entries = GetLogNetworkEntries();


                var findEntry = entries.Where(e =>
                {
                    if (e[RequestString][PostData] != null && e[RequestString][PostData][Text] != null)
                    {
                        return e[RequestString][PostData][Text].ToString().ToLower().Contains(urlContainedString.ToLower());
                    }
                    else
                    {
                        return e[RequestString]["url"].ToString().ToLower().Contains(urlContainedString.ToLower());
                    }

                });
                if (findEntry.ToList().Count > 0)//If entry found
                {
                    var request = findEntry.ToList()[0]["request"];
                    if (request["method"].ToString() == Get || request[PostData]?[Text] == null)
                    {
                        var queryString = request[QueryString];
                        var queryValues = queryString.Children().ToList();
                        products = queryValues.Where(k =>
                            k["name"].ToString().Contains(productPrefix) && k["name"].ToString().Contains("id")).ToList();
                    }

                    else
                    {
                        var postData = request[PostData];

                        if (postData[Text] != null)
                        {
                            var stringText = postData[Text].ToString();

                            var dict = HttpUtility.ParseQueryString(stringText);
                            string json = JsonConvert.SerializeObject(dict.Cast<string>().ToDictionary(k => k, v => dict[v]));

                            var postQueryValues = (JObject)JsonConvert.DeserializeObject(json);

                            foreach (var pair in postQueryValues)
                            {
                                var name = pair.Key.ToString().ToLower();
                                if (name.Contains(productPrefix.ToLower()) && name.Contains("id"))
                                {
                                    products.Add(pair.Value);
                                };
                            }
                        }
                    }
                }

                return products.Any();
            }, MaximumMilliSecondsToWait);

            return products.Count();
        }

        /// <summary>
        /// Find request entries where urlContainedString is contained in the url, and return the value of the designated query parameter.
        /// </summary>
        /// <param name="urlContainedString">A URL, or part of a URL, that needs to be contained in the request URL.</param>
        /// <param name="queryParam">A string representing the query parameter key for the value you want to retrieve.</param>
        /// <param name="caseInsensitive">Flag determining case insensitivity for query string value equality comparison.</param>
        /// <param name="maximumMilliSecondsToWait">Maximum seconds to wait for the request before returning the result.</param>
        /// <returns>The value of the requested query parameter.</returns>
        public string GetRequestQueryParamValue(string urlContainedString, string queryParam, bool caseInsensitive = false)
        {
            var caseSensitivity = caseInsensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            IEnumerable<JToken> reqParam = null;
            SpinWait.SpinUntil(() =>
            {
                var entries = GetLogNetworkEntries();

                var findEntry = entries.Where(e =>
                {

                    if (e[RequestString]["method"].ToString() == Get)
                    {
                        if (e[RequestString]["url"].ToString().ToLower().Contains(urlContainedString.ToLower()))
                        {
                            reqParam = e[RequestString][QueryString].Where(paramPair => string.Equals(paramPair["name"]?.ToString(), queryParam, caseSensitivity));
                            return true;
                        }
                    }

                    else
                    {
                        if (e[RequestString][PostData] != null && e[RequestString][PostData][Text] != null && e[RequestString][PostData][Text].ToString().ToLower().Contains(urlContainedString.ToLower()))
                        {
                            var postData = e[RequestString][PostData];
                            var stringText = postData[Text].ToString();

                            var dict = HttpUtility.ParseQueryString(stringText);
                            string json = JsonConvert.SerializeObject(dict.Cast<string>().ToDictionary(k => k, v => dict[v]));

                            var postQueryValues = (JObject)JsonConvert.DeserializeObject(json);

                            foreach (var pair in postQueryValues)
                            {
                                var name = pair.Key.ToString().ToLower();
                                if (name.Contains(queryParam.ToLower()))
                                {
                                    reqParam = pair.Value;
                                    return true;
                                };
                            }

                        }

                    }
                    return false;
                });
                return findEntry.Any();

            }, MaximumMilliSecondsToWait);

            var reqParamToken = reqParam.FirstOrDefault();
            var returnValue = reqParamToken["value"].ToString();
            return returnValue;
        }


        public bool GetRequestQueryParamValueLogging(string urlContainedString, Dictionary<string, string> queryParams, bool caseInsensitive = false, bool verboseLogsEnabled = false)
        {
            var caseSensitivity = caseInsensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;

            IEnumerable<JToken> findEntry = null;
            Dictionary<string, string> dict = null;
            var isEventFound = false;

            //check if entry exists
            SpinWait.SpinUntil(() =>
            {
                var entriesCheck = GetLogNetworkEntries();

                findEntry = entriesCheck.Where(e =>
                {
                    if (e[RequestString][PostData] != null && e[RequestString][PostData][Text] != null)
                    {
                        return e[RequestString][PostData][Text].ToString().ToLower().Contains(urlContainedString.ToLower());
                    }

                    return e[RequestString]["url"].ToString().ToLower().Contains(urlContainedString.ToLower());
                });

                return findEntry.Count() > 0;
            }, MaximumMilliSecondsToWait);

            _log.Message($"\nFound event entry: {findEntry.Count()}");

            if (findEntry.ToList().Count > 0) //If entry found
            {
                var request = findEntry.ToList()[0]["request"];
                if (request["method"].ToString() == Get)
                {
                    _log.Message("Request type is: Get");

                    //Create entry dictionary
                    var getDict = new Dictionary<string, string>();
                    request[QueryString].ToList().ForEach(jtoken => getDict.Add(jtoken["name"]?.ToString(), jtoken["value"]?.ToString()));
                    dict = getDict;
                }

                else
                {
                    _log.Message("Request type is: Post");

                    var postData = request[PostData];

                    if (postData[Text] != null)
                    {
                        //Create entry dictionary
                        var stringText = postData[Text].ToString();
                        var dictQuery = HttpUtility.ParseQueryString(stringText);
                        var json = JsonConvert.SerializeObject(dictQuery.Cast<string>().ToDictionary(k => k, v => dictQuery[v]));
                        var serializer = new JavaScriptSerializer(); //using System.Web.Script.Serialization;
                        dict = serializer.Deserialize<Dictionary<string, string>>(json);
                    }
                }

                if (verboseLogsEnabled)
                {
                    _log.Message("\nActual entry data:");
                    foreach (var pair in dict)
                    {
                        _log.Message($"{pair.Key}={pair.Value}");
                    }
                }

                //Find all matches
                foreach (var pair in queryParams)
                {
                    if (pair.Key.Contains("ca")) //If pair is Product category
                    {
                        _assert.True(!string.IsNullOrEmpty(dict[pair.Key]), "Product category event data does not have value");
                        isEventFound = true;
                        if (verboseLogsEnabled)_log.Message($"Product category actual event pair is: '{pair.Key}={dict[pair.Key]}'");
                    }
                    else
                    {

                        string actualValue;
                        try
                        {
                            var dictValue = dict[pair.Key].Split(' ').Where(s => !string.IsNullOrWhiteSpace(s));//Command removes extra spaces.
                            actualValue = string.Join(" ", dictValue);
                        }
                        catch(Exception)
                        {
                            _log.Message($"Expected entry {pair} was not found");
                            throw;
                        }

                        _assert.True(pair.Value.Equals(actualValue), $"Expected event entry '{pair.Key}={pair.Value}' does not match actual entry '{pair.Key}={actualValue}'");

                        isEventFound = true;
                        if (verboseLogsEnabled) _log.Message($"Expected event entry '{pair.Key}={pair.Value}' matches actual entry '{pair.Key}={dict[pair.Key]}'");
                    }
                }

            }

            return isEventFound;
            }


            public IEnumerable<JToken> GetPostRequestsByUrl(string urlContainedString)
            {
                IEnumerable<JToken> findEntry = new List<JToken>();

                SpinWait.SpinUntil(() =>
                {
                    var entries = GetLogNetworkEntries();

                    findEntry = entries.Where(e => e[RequestString]["url"].ToString().ToLower().Contains(urlContainedString.ToLower()) &&
                                                   e[RequestString][PostData] != null);

                    return findEntry.Any();

                }, MaximumMilliSecondsToWait);

                return findEntry;
            }

            public JArray GetLogNetworkEntries()
            {
                var json = GetNetworkLog();
                return (JArray)json["log"]["entries"];
            }
    }
}