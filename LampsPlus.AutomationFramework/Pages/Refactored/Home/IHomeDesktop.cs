using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Home
{
    public interface IHomeDesktop : IPageObjectModel
    {
        IBrowser Navigate();
        string InvisibleClass { get; }
        string GetCertonaWidgetSku();
        string PageUrl { get; }
        string GetCartWidgetSku();
        string GetJustForYouWidgetSku();
        bool IsRecentlyViewedWidgetVisible { get; }
        bool IsFreeShippingHeadingVisible { get; }
        bool IsInYourCartWidgetVisible { get; }
        bool IsJustForYouWidgetVisible { get; }
        bool IsStoreInSession();
        void ClearStoreInSession();
        void NavigateToSalePageViaSplashBanner();
        void NavigateToHospitalityProductsPageViaSplashBanner();
        void EnterStoreInSession(string storeNumber);
        void OpenInstagramWidget();
        void WaitForHomePageToLoad();
        void WaitForHospitalityHomePage();
        IElement GetHomepageStickyHeader();
        IElement GetChandelierMenu();
        IElement GetInstagramOverlayContent();
        IElement GetHospitalityBannerImage();
        IElement GetBodyElement();
        IElement GetInstagramWidget();
    }
}
