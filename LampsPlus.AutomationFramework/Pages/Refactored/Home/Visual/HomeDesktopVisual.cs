using System.Collections.Generic;
using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Home.Visual
{
    public class HomeDesktopVisual : HomeDesktop, IHomeDesktopVisual
    {
        public HomeDesktopVisual(IBrowser browser) : base(browser) { }

        public List<IElement> IgnoreCertona()
        {
            return new List<IElement> { JustForYouWidgetElement, RecentlyViewedWidgetElement };
        }

        public IElement IgnoreInstagramMediaVideo()
        {
            return InstagramMediaVideo;
        }

        public IElement IgnoreInstagramFeed()
        {
            return InstagramFeed;
        }

        public IElement IgnoreRecentlyViewedWidget()
        {
            return RecentlyViewedContainer;
        }

        public List<IElement> IgnoreHospitalityElements()
        {
            return new List<IElement> { HospitalityHomepageSplashBanner, MoreTopCategoriesSecondRow, MoreTopCategoriesFirstRow };
        }

        public IElement IgnoreHospitalityBanner()
        {
            return HospitalityHomepageSplashBanner;
        }

        public IElement IgnoreRecentlyViewedItems()
        {
            return CertonaRecentlyViewedWidget;
        }
    }
}
