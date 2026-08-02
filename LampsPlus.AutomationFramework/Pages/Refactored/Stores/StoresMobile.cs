using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Stores
{
    public class StoresMobile : StoresDesktop, IStoresMobile
    {
        public StoresMobile(IBrowser browser) : base(browser)
        {
        }
    }
}