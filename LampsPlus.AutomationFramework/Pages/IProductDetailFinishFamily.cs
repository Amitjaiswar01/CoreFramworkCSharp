using System.Collections.Generic;
using System.Collections.ObjectModel;
using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages
{
    public interface IProductDetailFinishFamily
    {
        #region Class Setup
        string MoreOptionsString { get; }
        string MoreFinishesString { get; }
        string OtherOptionsString { get; }
        string OtherOptionsThumbClass { get; }
        #endregion

        #region Page Elements
        IElement MoreOptionsCollapsableSectionHeader { get; }
        IElement MoreOptionsCollapsableSlider { get; }
        IElement OtherOptionsAccordion { get; }

        ReadOnlyCollection<IElement> ItemsList { get; }
        #endregion

        /// <summary>
        /// Get other option widget SKUs.
        /// </summary>
        /// <returns></returns>
        List<string> GetOtherOptionsWidgetSkus();
    }
}
