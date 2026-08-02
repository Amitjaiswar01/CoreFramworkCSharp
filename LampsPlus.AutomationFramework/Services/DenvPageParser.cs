using Automation.Framework;
using LampsPlus.AutomationFramework.Utilities.Environment;
using Newtonsoft.Json.Linq;
using System;
using System.Web.UI;
using Automation.Framework.Utilities;

namespace LampsPlus.AutomationFramework.Services
{
    public class DenvPageParser : IDenvParser
    {
        private readonly IBrowser _browser;
        private readonly SessionSettings _settings;

        public DenvPageParser(IBrowser browser, SessionSettings settings)
        {
            _browser = browser;
            _settings = settings;
        }
        public EnvironmentInformation Parse(string devPageUrl)
        {
            if (!_settings.IsMobileView || _settings.SettingsTestName.Contains("Emulator"))
            {
                _browser.Navigate(devPageUrl);
            }

            _browser.Wait.ForPage(devPageUrl);

            var jsonString = _browser.Locate.ElementByTagName(HtmlTextWriterTag.Pre).Text;
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
