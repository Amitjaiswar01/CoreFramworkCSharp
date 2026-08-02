using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.MagicMerchandizer
{
    public class MagicMerchandizerDesktop : IMagicMerchandizerDesktop
    {
        //Class members

        //Instances
        protected IBrowser Browser;

        public MagicMerchandizerDesktop(IBrowser browser)
        {
            Browser = browser;
        }

        //Interface implementation
        public string PageTitle { get; }
        public string PageUrl { get; }
        public bool IsCurrentPage { get; }
    }
}