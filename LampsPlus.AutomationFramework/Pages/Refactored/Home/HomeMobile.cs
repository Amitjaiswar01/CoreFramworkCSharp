using System.Web.UI;
using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Utilities;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Home
{
    public class HomeMobile : HomeDesktop, IHomeMobile
    {
        //Class members
        private string _instagramFeedXpath = "//*[contains(@class, 'instagramFeed')]";
        private string _instagramWidgetXpath = "//div[@id=\"pixleeWidgetMobile\"]//img";
        private string _instagramFeedMobileClass = "instagramFeedMobile";
        private string _hpSplashLink = "hpsplash__link";
        private string _recentlyViewedWidgetContainerClass = "recentlyViewedWrapper";
        private string _hpSplashImgClass = "hpsplash__img";
        private string _recentlyViewedContainerId = "recentlyViewedContainer";

        private IElement InstagramWidgetDisplayedFirst => Browser.Locate.ElementByXpath(_instagramWidgetXpath);
        private IElement CertonaRecentlyViewedWidgetSkuMobile => Browser.Locate.ElementByTagName(HtmlTextWriterTag.A, CertonaRecentlyViewedWidgetMobile);
        private IElement CertonaRecentlyViewedWidgetMobile => Browser.Locate.ElementById(_recentlyViewedContainerId);

        protected override IElement InstagramFeed => Browser.Locate.ElementByXpath(_instagramFeedXpath);
        protected override IElement HomepageSplashBanner => Browser.Locate.ElementBySelector(_hpSplashLink.ToCssClassSelector());

        //Instances 
        public HomeMobile(IBrowser browser) : base(browser) { }

        //Interface implementation
        public override bool IsHomePageLoaded()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_hpSplashImgClass.ToCssClassSelector()));
            return true;
        }

        public override bool IsRecentlyViewedWidgetVisible => Browser.Locate.DoesElementExistImmediately(_recentlyViewedWidgetContainerClass.ToCssClassSelector());

        public override void NavigateToSalePageViaSplashBanner()
        {
            HomepageSplashBanner.Click();
        }

        public override void OpenInstagramWidget()
        {
            Browser.ScrollToBottomOfPage(Urls.HomePageUrl);
            Browser.Wait.IsVisibleElement(By.XPath(_instagramFeedXpath));

            Browser.ScrollIntoView(InstagramFeed, true);
            Browser.Wait.IsVisibleElement(By.CssSelector(_instagramFeedMobileClass.ToCssClassSelector()));

            Browser.Wait.IsVisibleElement(By.XPath(_instagramWidgetXpath));
            InstagramWidgetDisplayedFirst.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(PixleeModalBodyClass.ToCssClassSelector()));
            Browser.Wait.ForElementToStopAnimating(InstagramWidgetModal);
        }

        public override void WaitForHomePageToLoad()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_hpSplashImgClass.ToCssClassSelector()));
        }

        public override string GetCertonaWidgetSku()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_recentlyViewedContainerId.ToCssIdSelector()));
            return CertonaRecentlyViewedWidgetSkuMobile.GetAttribute("data-certonasku");
        }
    }
}
