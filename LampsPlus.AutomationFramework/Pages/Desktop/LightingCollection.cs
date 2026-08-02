using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Pages.Desktop
{
    /// <summary>
    /// https://www.lampsplus.com/lightingcollections/chandeliers/default.aspx
    /// </summary>
    public class LightingCollection: LightingCollectionBase
    {
        /// <inheritdoc />
        public LightingCollection(IBrowser browser) : base(browser) { }
    }
}
