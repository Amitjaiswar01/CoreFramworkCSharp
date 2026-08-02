using System.Collections.Generic;
using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Home.Visual
{
    public interface IHomeDesktopVisual : IHomeDesktop
    {
        IElement IgnoreInstagramMediaVideo();
        IElement IgnoreInstagramFeed();
        IElement IgnoreRecentlyViewedWidget();
        IElement IgnoreHospitalityBanner();
        IElement IgnoreRecentlyViewedItems();
        List<IElement> IgnoreCertona();
        List<IElement> IgnoreHospitalityElements();
    }
}
