using System;
using System.Net;
using System.Net.Http;
using System.Text;
using LampsPlus.AutomationFramework.Enums;
using LampsPlus.AutomationFramework.Utilities.Environment;

namespace LampsPlus.AutomationFramework.Services
{
    public class DenvStringParser : IDenvParser
    {
        private readonly string _proxyAddress;

        private readonly string _deploymentsDeckUrl = "http://10.1.16.80/";

        public EnvironmentInformation Parse(EnvironmentUnderTest environment , string instanceConfig)
        {
            var httpClientHandler = new HttpClientHandler
            {
                Proxy = new WebProxy(_proxyAddress, false),
                UseProxy = true
            };

            HttpResponseMessage responseDb = null;
            var responseFixVersion = string.Empty;
            var fixVersion = string.Empty;
            var dBtype = string.Empty; 

            // Ignore SSL Validation error;
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

            using (var client = new HttpClient(httpClientHandler))
            {
                //get Fix version Deployments Deck response
                responseFixVersion = client.GetStringAsync(_deploymentsDeckUrl).Result;

                //get DB telemetry response
                HttpContent contentPost = new StringContent("", Encoding.UTF8, "application/json");
                responseDb = client.PostAsync($"https://appservices.lampsplus.com/LampsPlus-{instanceConfig}/api/telemetry/denv", contentPost).Result;
                dBtype = responseDb.Content.ReadAsStringAsync().Result;
            }

            //Parse Fix version data
            var fixVersionLengthInitialIndex = responseFixVersion.IndexOf($"{instanceConfig} (172.24") + 20;
            var fixVersionLength = 10;
            fixVersion = responseFixVersion.Substring(fixVersionLengthInitialIndex, fixVersionLength);

            //Parse DB type
            var dBInitialIndex = dBtype.LastIndexOf("Products - ");
            var databaseString  = dBtype.Substring(dBInitialIndex, 1);
            
            var environmentInformation = new EnvironmentInformation
            {
                FixVersion = fixVersion,
                InstanceName = instanceConfig,
                DatabaseSymbol = databaseString
            };

            return environmentInformation;
        }

        public EnvironmentInformation Parse(string devPageUrl)
        {
            throw new NotImplementedException();
        }
    }
}