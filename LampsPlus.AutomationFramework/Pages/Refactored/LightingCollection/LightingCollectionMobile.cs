using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.LightingCollection
{
    public class LightingCollectionMobile : LightingCollectionDesktop, ILightingCollectionMobile
    {
        public LightingCollectionMobile(IBrowser browser) : base(browser)
        {
        }
    }
}