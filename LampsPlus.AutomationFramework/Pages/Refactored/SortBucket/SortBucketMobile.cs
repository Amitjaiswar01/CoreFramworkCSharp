using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.SortBucket
{
    public class SortBucketMobile : SortBucketDesktop, ISortBucketMobile
    {
        public SortBucketMobile(IBrowser browser) : base(browser)
        {
        }
    }
}