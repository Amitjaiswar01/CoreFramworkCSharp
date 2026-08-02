using System;
using System.Web.UI;
using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Utilities;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Home
{
    public class HomeDesktop : IHomeDesktop
    {
        //Class members
        private string _pixleeContainerClass = "pixleeContainer";
        private string _instagramWidgetXpath = "//div[@id=\"pixleeWidget\"]//img";
        private string _pixleeElementSelector = ".Overlay__contentWrapper .react-multi-carousel-track li";
        private string _homepageSplashBannerClass = "homepage-splash-banner";
        private string _sliderLPHId = "sliderLPH";
        private string _justForYouWidgetId = "justForYouWidgetContainer";
        private string _recentlyViewedWidgetId = "recentlyViewedWidgetContainer";
        private string _hpWrapperClass = "hpWrapper";
        private string _hpSaleTxtWrapperClass = "hpSaleTxtWrapper";
        private string _cartWidgetContainerClass = "cartWidgetContainer";
        private string _justForYouWidgetContainerId = "justForYouWidgetContainer";
        private string _cartWidgetContainerProdImgClass = "cartWidgetContainer__prodImg";
        private string _lpHeaderId = "lpHeader";
        private string _chandelierMenuXpath  = "//*[@id='chandeliers']/div";
        private string _recentlyViewedWidgetContainerId = "recentlyViewedWidgetContainer";
        private string _isHospitalityClass = "isHospitality";
        private string _certonaWidgetContainerListClass = "certonaWidgetContainer__list ";
        private string _overlayContentClass = "OverlayContent";
        private string _mediaVideoClass = "MediaVideo";
        private string _nivoSliderClass = "nivoSlider";
        private string _moreTopCategorySecondRowXpath = "//div[contains(@class, 'container4Across')][2]";
        private string _moreTopCategoryFirstRowXpath = "//div[contains(@class, 'container4Across')][1]";
        private string _bdHomePageId = "bdHomePage";
        private string _recentlyViewedWrapperClass = "recentlyViewedWrapper";
        private string _pixleeMediaModalContentClass = "PixleeMediaModal__content";

        protected string PixleeModalBodyClass => "PixleeMediaModal";
        protected string TxtStoreNumberId => "txtStoreNumber";
        
        private IElement InstagramWidgetDisplayedFirst => Browser.Locate.ElementByXpath(_instagramWidgetXpath);
        private IElement HomepageStickyHeader => Browser.Locate.ElementById(_lpHeaderId);
        private IElement ChandelierMenu => Browser.Locate.ElementByXpath(_chandelierMenuXpath);
        private IElement StoreNumberField => Browser.Locate.ElementById(TxtStoreNumberId);
        private IElement JustForYouWidgetSku => Browser.Locate.ElementByTagName(HtmlTextWriterTag.A, JustForYouWidgetElement);
        private IElement CartWidgetElement => Browser.Locate.ElementByClassName(_cartWidgetContainerProdImgClass);
        private IElement CertonaRecentlyViewedWidgetSku => Browser.Locate.ElementByTagName(HtmlTextWriterTag.A, CertonaRecentlyViewedWidget);
        private IElement InstagramOverlayContent => Browser.Locate.ElementBySelector(_pixleeMediaModalContentClass.ToCssClassSelector());
        private IElement HospitalityHomePageBanner => Browser.Locate.ElementByClassName(_nivoSliderClass);

        protected IElement BodyElement => Browser.Locate.ElementById(_bdHomePageId);
        protected IElement HospitalityHomepageSplashBanner => Browser.Locate.ElementById(_sliderLPHId);
        protected IElement PixleeElement => Browser.Locate.ElementBySelector(_pixleeElementSelector);
        protected IElement RecentlyViewedWidgetElement => Browser.Locate.ElementById(_recentlyViewedWidgetId);
        protected IElement RecentlyViewedContainer => Browser.Locate.ElementByClassName(_recentlyViewedWrapperClass);
        protected IElement MoreTopCategoriesSecondRow => Browser.Locate.ElementByXpath(_moreTopCategorySecondRowXpath);
        protected IElement MoreTopCategoriesFirstRow => Browser.Locate.ElementByXpath(_moreTopCategoryFirstRowXpath);
        protected IElement JustForYouWidgetElement => Browser.Locate.ElementById(_justForYouWidgetId);
        protected IElement CertonaRecentlyViewedWidget => Browser.Locate.ElementById(_recentlyViewedWidgetContainerId);

        protected IElement InstagramMediaVideo => Browser.Locate.ElementByClassName(_mediaVideoClass);
        protected IElement InstagramWidgetModal => Browser.Locate.ElementByClassName(PixleeModalBodyClass);
        protected virtual IElement HomepageSplashBanner => Browser.Locate.ElementByClassName(_homepageSplashBannerClass);
        protected virtual IElement InstagramFeed => Browser.Locate.ElementByClassName(_pixleeContainerClass);
        
        //Instances 
        protected IBrowser Browser;

        public HomeDesktop(IBrowser browser)
        {
            Browser = browser;
        }

        //Interface implementation
        public string InvisibleClass => "invisible";
        public string PageTitle { get; }
        public string PageUrl => "https://www.lampsplus.com/";
        public bool IsCurrentPage => IsHomePageLoaded();
        public bool IsFreeShippingHeadingVisible => Browser.Locate.DoesElementExistImmediately($"{_hpWrapperClass.ToCssClassSelector()} {_hpSaleTxtWrapperClass.ToCssClassSelector()}");
        public bool IsInYourCartWidgetVisible => Browser.Locate.DoesElementExistImmediately(_cartWidgetContainerClass.ToCssClassSelector());
        public bool IsJustForYouWidgetVisible => Browser.Locate.DoesElementExistImmediately(_justForYouWidgetContainerId.ToCssIdSelector());
        public virtual bool IsRecentlyViewedWidgetVisible => Browser.Locate.DoesElementExistImmediately(_recentlyViewedWidgetContainerId.ToCssIdSelector());
        
        public IBrowser Navigate()
        {
            // Navigate to base page
            Browser.Navigate(PageUrl);

            return Browser;
        }

        public bool IsStoreInSession()
        {
            if (!Browser.Locate.DoesElementExistImmediately(TxtStoreNumberId.ToCssIdSelector()))
            {
                return false;
            }

            try
            {
                return Convert.ToInt32(StoreNumberField.GetAttribute(HtmlTextWriterAttribute.Value.ToString())) > 0;
            }
            catch
            {
                return false;
            }
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
        /// Enter store number to put the site in Store in Session mode.
        /// </summary>
        /// <param name="storeNumber">Store number to enter for the Store in Session.</param>
        public void EnterStoreInSession(string storeNumber)
        {
            StoreNumberField.Clear();
            StoreNumberField.SendKeys(storeNumber);
            StoreNumberField.SendKeys(Keys.Enter);
        }

        public virtual void OpenInstagramWidget()
        {
            Browser.Wait.ForDisplayedElement(InstagramFeed);

            Browser.ScrollIntoView(InstagramFeed, true);
            Browser.Wait.IsVisibleElement(By.CssSelector(_pixleeContainerClass.ToCssClassSelector()));
            Browser.Wait.ForElementToStopAnimating(InstagramFeed);
            InstagramWidgetDisplayedFirst.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(PixleeModalBodyClass.ToCssClassSelector()));
            Browser.Wait.ForDomReady();
            Browser.SwitchToDefaultContent();
            Browser.SwitchFocusToIframe(Browser.Locate.ElementBySelector(_pixleeMediaModalContentClass.ToCssClassSelector()));
        }

        public virtual string GetCertonaWidgetSku()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_certonaWidgetContainerListClass.ToCssClassSelector()));
            return CertonaRecentlyViewedWidgetSku.GetAttribute("data-certonasku");
        }

        public string GetJustForYouWidgetSku()
        {
            return JustForYouWidgetSku.GetAttribute("data-certonasku");
        }

        public string GetCartWidgetSku()
        {
            Browser.Wait.ForDomReady();
            return CartWidgetElement.GetAttribute("data-sku");
        }

        public virtual void NavigateToSalePageViaSplashBanner()
        {
            HomepageSplashBanner.Click();
            Browser.Wait.ForDomReady();
        }

        public void NavigateToHospitalityProductsPageViaSplashBanner()
        {
            Browser.MouseOverOnElement(HospitalityHomepageSplashBanner);
            Browser.Wait.ForElementToStopAnimating(HospitalityHomepageSplashBanner);
            HospitalityHomepageSplashBanner.Click();
            Browser.Wait.ForDomReady();
        }

        public virtual bool IsHomePageLoaded()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_homepageSplashBannerClass.ToCssClassSelector()));
            return true;
        }

        public IElement GetHomepageStickyHeader()
        {
            return HomepageStickyHeader;
        }

        public IElement GetChandelierMenu()
        {
            return ChandelierMenu;
        }

        public virtual void WaitForHomePageToLoad()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_homepageSplashBannerClass.ToCssClassSelector()));
        }

        public void WaitForHospitalityHomePage()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_isHospitalityClass.ToCssClassSelector()));
        }

        public IElement GetInstagramOverlayContent()
        {
            return InstagramOverlayContent;
        }

        public IElement GetHospitalityBannerImage()
        {
            return HospitalityHomePageBanner;
        }

        public IElement GetBodyElement()
        {
            return BodyElement;
        }

        public IElement GetInstagramWidget()
        {
            return InstagramWidgetModal;
        }
    }
}
