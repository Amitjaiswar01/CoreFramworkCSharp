using LampsPlus.AutomationFramework.Utilities.Environment;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;

namespace LampsPlus.AutomationFramework.Services
{
    public class DenvJsonParser : IDenvParser
    {
        private readonly string _proxyAddress;

        public DenvJsonParser(string proxyAddress)
        {
            _proxyAddress = proxyAddress;
        }

        public DenvJsonParser() { }

        public EnvironmentInformation Parse(string devPageUrl)
        {
            var httpClientHandler = new HttpClientHandler
            {
                Proxy = new WebProxy(_proxyAddress, false),
                UseProxy = true
            };

            var jsonString = string.Empty;

            // Ignore SSL Validation error;
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

            using (var client = new HttpClient(httpClientHandler))
            {
                jsonString = client.GetStringAsync(devPageUrl).Result;
            }

            var jsonObject = JObject.Parse(jsonString);

            if (!bool.TryParse(jsonObject["Is Prod Instance"].ToString(), out var isProductionInstance))
                throw new InvalidCastException("cannot parse the value of Is Prod Instance");

            var environmentInformation = new EnvironmentInformation
            {
                InstanceName = jsonObject.SelectToken("InstanceName").ToString(),
                DatabaseSymbol = jsonObject.SelectToken("DatabaseSymbol").ToString(),
                PssVersion = jsonObject.SelectToken("PssVersion").ToString(),
                SearchProviderVersion = jsonObject.SelectToken("ExternalServerLastIpOctet").ToString(),
                FixVersion = jsonObject.SelectToken("['LP FixVersion']").ToString(),
                IsProductionInstance = isProductionInstance
            };

            return environmentInformation;
        }
    }
}
