using System;
using System.Collections.ObjectModel;
using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Utilities;
using OpenQA.Selenium;

using Page = Automation.Framework.Core.Page;

namespace LampsPlus.AutomationFramework.Pages.Base
{
    /// <summary>
    /// Base class for common behavior between desktop and mobile views.
    /// </summary>
    public abstract class HomeBase : Page, IHome
    {
        /// <inheritdoc />
        protected HomeBase(IBrowser browser, IGlobalLocators globalLocators) : base(browser) { GlobalLocators = globalLocators; }

        #region Class Setup
        internal IGlobalLocators GlobalLocators { get; }
        #endregion

        #region CSS Selector Strings
        public string BdHomePageId { get; } = "bdHomePage";
		public string CartCountId { get; } = "cartCount";
        private string OverlayContentWrapperClass { get; } = "Overlay__contentWrapper";
        public string InvisibleClass { get; } = "invisible";
        public string TxtStoreNumberId { get; } = "txtStoreNumber";
        public string PlaAddToCartId { get;  } = "pdAddToCart";
        public string HomepageSplashBannerClass { get; } = "homepage-splash-banner";
        public string HpSplashImgClass { get; } = "hpsplash__img";
        public string IsHospitalityClass { get; } = "isHospitality";
        public abstract string PlaDetailsId { get; }
        public abstract string HomeHeaderClass { get; }
        public abstract string PlaViewDetailsLinkXpath { get; } 

        #endregion

        #region Page Elements
        //Elements that are located the same way in both Desktop and Mobile views.
        public IElement CartCountElement => Browser.Locate.ElementById(CartCountId);
        public IElement InstagramModal => Browser.Locate.ElementByClassName(OverlayContentWrapperClass);

        //Elements that exist in both Desktop and Mobile views but are located differently.
        public abstract IElement BodyElement { get; }
        public abstract IElement PlaViewDetailsLinkElement { get; }

        //Elements that exist in Desktop view and NOT Mobile view.
        public abstract IElement CareersLinks { get; }
        public abstract IElement PlaReviews { get; }
        public abstract IElement PlaReviewStars { get; }
        public abstract IElement StoreNumberField { get; }
        #endregion

        /// <summary>
        /// Enter store number to put the site in Store in Session mode.
        /// </summary>
        /// <param name="storeNumber">Store number to enter for the Store in Session.</param>
        public void EnterStoreInSession(string storeNumber)
        {
            StoreNumberField.Clear();
		    StoreNumberField.SendKeys(storeNumber);
            StoreNumberField.SendKeys(Keys.Enter);
        }

        /// <summary>
        /// Clear the store in session.
        /// </summary>
        public void ClearStoreInSession()
	    {
		    StoreNumberField.Clear();
            StoreNumberField.SendKeys(Keys.Enter);
        }

        /// <summary>
	    /// If logged in account is set to store in session or not.
	    /// </summary>
	    /// <returns></returns>
	    public bool IsStoreInSession()
	    {
		    if (!Browser.Locate.DoesElementExistImmediately(TxtStoreNumberId.ToCssIdSelector())) { return false; }

	        try
	        {
	            return Convert.ToInt32(StoreNumberField.GetAttribute(HtmlTextWriterAttribute.Value.ToString())) > 0;
	        }
	        catch
	        {
	            return false;
	        }
	    }
    }
}
