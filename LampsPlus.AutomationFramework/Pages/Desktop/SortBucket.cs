using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Pages.Desktop
{
    /// <summary>
    /// Example page: https://www.lampsplus.com/products/close-to-ceiling-lights/.
    /// </summary>
    public class SortBucket : SortBucketBase
    {
        /// <inheritdoc />
        public SortBucket(IBrowser browser, IGlobalLocators globalLocators) : base(browser, globalLocators) { }
    }
}