using Automation.Framework;
using Automation.Framework.Core;
using Automation.Framework.Verifies;

namespace LampsPlus.AutomationFramework.Utilities
{
    /// <summary>
    /// Cookie Utility class
    /// </summary>
    public class CookieUtility
    {
        private string _disableKioskModifierKeyCookieName = "DisableKioskModifierKey";
        private string _kioskStoreNumberCookieName = "KioskStoreNumber";
	    private string _checkoutSurveyIdCookieName = "CheckoutSurveyId";

		private IBrowser Browser { get; }
        private IAssert Assert { get; }

        public CookieUtility(IBrowser browser, IAssert assert)
        {
            Browser = browser;
            Assert = assert;
        }

        /// <summary>
        /// Enters store in session mode on the site.
        /// NOTE: This is ignored for mobile device (Appium) tests.
        /// </summary>
        public void EnterStoreInSessionMode()
        {
            Browser.AddCookie(_kioskStoreNumberCookieName, "12");
            Browser.AddCookie(_disableKioskModifierKeyCookieName, "1");

            Browser.RefreshPage();

            Assert.True(IsStoreInSession(), "Unable to set Store In Session from CookieUtility.EnterStoreInSessionMode()");
        }

        /// <summary>
        /// Exits store in session mode on the site.
        /// NOTE: This is ignored for mobile device (Appium) tests.
        /// </summary>
        public void ExitStoreInSessionMode()
        {
            Browser.DeleteCookie(_kioskStoreNumberCookieName);
            Browser.DeleteCookie(_disableKioskModifierKeyCookieName);

            Browser.RefreshPage();

            Assert.True(!IsStoreInSession(), "Unable to exit Store In Session from CookieUtility.ExitStoreInSessionMode()");
        }

		/// <summary>
		/// Disable Checkout Survey by setting Survey Id Cookie to 0
		/// </summary>
		public void DisableCheckoutSurvey()
	    {
            Browser.AddCookie(_checkoutSurveyIdCookieName, "0");
	    }

	    /// <summary>
			/// Checks if Store In Session / Kiosk mode is currently active on site.
			/// NOTE: This is ignored for mobile device (Appium) tests. False is returned for Appium tests.
			/// </summary>
			/// <returns>Boolean if store is in session.</returns>
			private bool IsStoreInSession()
        {
            var kioskStoreNumberCookie = Browser.GetCookie(_kioskStoreNumberCookieName); //TODO Provided cookie check
            return kioskStoreNumberCookie != null 
                   && int.TryParse(kioskStoreNumberCookie.Value, out var kioskNumber) 
                   && kioskNumber > 0;
        }
    }
}
