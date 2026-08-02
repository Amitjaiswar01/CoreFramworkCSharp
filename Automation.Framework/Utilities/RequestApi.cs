using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Xml;

using Newtonsoft.Json;

using RestSharp;

namespace Automation.Framework.Utilities
{
    /// <summary>
    /// Provides a common way to interact with REST services.
    /// </summary>
    public class RequestApi
    {
        private RestClient _client;

		public RequestApi()
		{
			_client = new RestClient();
			LocalIpList = new List<string>();
		}

		/// <summary>
		/// Provides a common way to interact with REST services.
		/// </summary>
		public RequestApi(params string[] localIps)
        {
            _client = new RestClient();
            LocalIpList = new List<string>();

            LocalIpList.AddRange(localIps);
        }

        /// <summary>
        /// Local list of IP's to enable enhanced security for API requests.
        /// </summary>
        public static List<string> LocalIpList { get; set; }

        /// <summary>
        /// Executes GET REST request and returns the response.
        /// </summary>
        /// <param name="url">URL of request.</param>
        /// <returns>Deserialized dictionary of the returned content from the REST call.</returns>
        public Dictionary<string, dynamic> GetRestRequest(string url)
        {
            var response = GetResponse(url);
           
            return GetDeserializedJsonContent(response);
		}

		/// <summary>
		/// Executes PUT REST request and returns the response.
		/// </summary>
		/// <param name="url">URL of request.</param>
		/// <returns>Deserialized dictionary of the returned content from the REST call.</returns>
		public Dictionary<string, dynamic> PutRestRequest(string url)
        {
            var response = GetResponse(url, restMethod: Method.PUT);

            return GetDeserializedJsonContent(response);
		}

		/// <summary>
		/// Get string response from GET web request.
		/// </summary>
		/// <param name="requestUrl">URL to request.</param>
		/// <param name="isSecure">Should security protocols for the request be set?</param>
		/// <returns>String for the given request.</returns>
		public string GetResponseString(string requestUrl, bool isSecure = false)
        {
            return GetResponse(requestUrl, isSecure).Content;
        }

        /// <summary>
        /// XmlDocument returned from the given url request.
        /// </summary>
        /// <param name="resourceUrl">Url for the requested document.</param>
        /// <returns>XmlDocument for the given request.</returns>
        public XmlDocument GetXml(string resourceUrl)
        {
            var response = GetResponse(resourceUrl, true);
            {
                var xml = new XmlDocument();
                xml.LoadXml(response.Content);

                return xml;
            }
        }

        /// <summary>
        /// Does the given url return a 200 status code?
        /// </summary>
        /// <param name="resourceUrl">Resource URL to request information from.</param>
        /// <returns>True when the request returns a success status code.</returns>
        public bool IsSuccess(string resourceUrl)
        {
            var response = GetResponse(resourceUrl, true);

            return response.StatusCode == HttpStatusCode.OK;
        }

        private Dictionary<string, dynamic> GetDeserializedJsonContent(IRestResponse restResponse)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(restResponse.Content);
        }

        /// <summary>
        /// Base call for all Get requests.
        /// </summary>
        /// <param name="requestUrl">Url to get data from.</param>
        /// <param name="setSecurityProtocol">true if Security Protocol should be set for internal IP addresses.</param>
        /// <param name="restMethod">Method of REST request (ie: GET, PUT, etc)</param>
        /// <returns>Rest response object.</returns>
        private IRestResponse GetResponse(string requestUrl, bool setSecurityProtocol = false, Method restMethod = Method.GET)
        {
            var splitUrl = SplitUrl(requestUrl);
            var baseUrl = splitUrl[0];
            var resourceUrl = splitUrl[1];

            _client = new RestClient(baseUrl);

            if (setSecurityProtocol) { SetSecurityProtocol(requestUrl); }

            var request = new RestRequest(resourceUrl, restMethod);

            return _client.Execute(request);
        }

        /// <summary>
        /// Returns a list where first string is the base url, and second string is the resource url.
        /// </summary>
        /// <param name="url">Url to split.</param>
        /// <returns>Split url in a List.</returns>
        private List<string> SplitUrl(string url)
        {
            var list = new List<string>();

            if (url.Count(x => x == '/') > 2)
            {
                var indexToSplit = IndexOfNthCharInString(url, '/', 3);

                list.Add(url.Substring(0, indexToSplit + 1));
                list.Add(url.Substring(indexToSplit + 1));
            }

            return list;
        }

        /// <summary>
        /// Return the index of the char in a given string based on the number of the char occurence requested.
        /// This will return 0 if the the requested value is not found.
        /// </summary>
        /// <param name="baseString">String to search.</param>
        /// <param name="valueToFind">Character to find.</param>
        /// <param name="numOccurenceToFind">nth occurence of the character in the baseString.</param>
        /// <returns></returns>
        private int IndexOfNthCharInString(string baseString, char valueToFind, int numOccurenceToFind)
        {
            if (baseString.Contains(valueToFind))
            {
                for (var index = 0; index < baseString.Length - 1; index++)
                {
                    if (baseString[index] == valueToFind)
                    {
                        numOccurenceToFind--;

                        if (numOccurenceToFind == 0) { return index; }
                    }
                }
            }

            return 0;
        }

        /// <summary>
        /// Security policy needs to be set if the traffic is being directed through the hosts file.
        /// </summary>
        /// <param name="requestUrl">Base URL for the request.</param>
        private void SetSecurityProtocol(string requestUrl)
        {
            var applicationDesktopGridSettings = ConfigurationManager.GetSection("GridGroup/DesktopGrid") as NameValueCollection;
            var applicationMobileGridSettings = ConfigurationManager.GetSection("GridGroup/MobileGrid") as NameValueCollection;
            var seleniumHubHost = applicationDesktopGridSettings.GetValues("SeleniumHubHost").FirstOrDefault();
            var seleniumMobileHubHost = applicationMobileGridSettings.GetValues("SeleniumMobileHubHost").FirstOrDefault();
            var seleniumMobileHubHostAlternative = applicationMobileGridSettings.GetValues("SeleniumMobileHubHostAlternative").FirstOrDefault();
            var seleniumMobileHubAppiumNode = applicationMobileGridSettings.GetValues("SeleniumMobileHubAppiumNode").FirstOrDefault();


            if (requestUrl.Contains(seleniumMobileHubHost) || requestUrl.Contains(seleniumMobileHubHostAlternative) || requestUrl.Contains(seleniumMobileHubAppiumNode) || requestUrl.Contains(seleniumHubHost))
                        return;

            bool CanSetSecurityProtocol(string url, IPAddress ip) =>
                !url.Contains("localhost")
                && (IsLocalIp(ip)
                    || url.ToLower().Contains("ms.lampsplus.com") // NOTE: meant to cover ims and ems microservices
                );

            var uriAddress = new Uri(requestUrl);
            var ipAddress = requestUrl.Contains("localhost")
                ? new IPAddress(0)
                : Dns.GetHostAddresses(uriAddress.Authority)[0];

            if (CanSetSecurityProtocol(requestUrl, ipAddress))
            {
                ServicePointManager.SecurityProtocol =
                    SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            }
        }

        /// <summary>
        /// IP Addresses for local
        /// </summary>
        /// <param name="ipAddress">IPAddress to compare against the LocalIpList.</param>
        /// <returns>True when the given IP is contained within the LocalIpList.</returns>
        private static bool IsLocalIp(IPAddress ipAddress) { return LocalIpList.Contains(ipAddress.ToString()); }

		public ResponseModel GetResponse(string uri, HttpMethod httpMethod, string requestContent = null, string contentType = "application/json")
		{
			return GetResponse(uri, httpMethod, 0, requestContent, contentType);
		}

		public ResponseModel GetResponse(string uri, HttpMethod httpMethod, int timeOut, string requestContent = null, string contentType = "application/json")
		{
            SetSecurityProtocol(uri); 
			var httpRequest = CreateRequest(uri, httpMethod.Method, timeOut);

			if(!string.IsNullOrEmpty(contentType))
				httpRequest.ContentType = contentType;

			if(!string.IsNullOrEmpty(requestContent))
			{
				var buffer = Encoding.ASCII.GetBytes(requestContent);

				using(var requestStream = httpRequest.GetRequestStream())
				{
					requestStream.Write(buffer, 0, buffer.Length);
				}
			}
            else if (httpMethod == HttpMethod.Post)
                httpRequest.ContentLength = 0;

            return GetResponse(httpRequest);
		}

        private HttpWebRequest CreateRequest(string uri, string method, int timeOut)
		{
			var request = WebRequest.Create(uri) as HttpWebRequest;

			if(request == null)
				throw new Exception($"Could not create a request for {uri}.");

			request.Method = method;

			if(timeOut > 0)
			{
				request.Timeout = timeOut;
			}

			return request;
		}

		private ResponseModel GetResponse(HttpWebRequest httpRequest)
		{
			using(var webResponse = httpRequest.GetResponse())
			{
				var httpResponse = webResponse as HttpWebResponse;

				if(httpResponse == null)
					return new ResponseModel { StatusCode = HttpStatusCode.Gone };

				var response = new ResponseModel { StatusCode = httpResponse.StatusCode };

				if(response.StatusCode != HttpStatusCode.OK)
					return response;

				using(var responseStream = httpResponse.GetResponseStream())
				{
					if(responseStream == null)
						return response;

					using(var reader = new StreamReader(responseStream))
					{
						response.Content = reader.ReadToEnd();
					}

					return response;
				}
			}
		}
	}
}
