using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.LightingCollection
{
    public class LightingCollectionDesktop : ILightingCollectionDesktop
    {
        //Class members

        //Instances
        protected IBrowser Browser;

        public LightingCollectionDesktop(IBrowser browser)
        {
            Browser = browser;
        }

        //Interface implementation
        public string PageTitle { get; }
        public string PageUrl { get; }
        public bool IsCurrentPage { get; }
    }
}