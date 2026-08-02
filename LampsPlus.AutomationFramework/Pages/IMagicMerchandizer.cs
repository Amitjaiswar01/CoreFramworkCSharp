using System.Collections.Generic;
using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages
{
    /// <summary>
    /// Common behavior between desktop and mobile views.
    /// </summary>
    public interface IMagicMerchandizer
    {
        #region Class Setup
        string ChandeliersCategory { get; }
        string MoreCategory { get; }
        #endregion

        #region Page Elements
        IElement AdvancedOptionsElement { get; }
        IElement AdvanceOptionsFormulaElement { get; }
        IElement ChandelierNavLinkElement { get; }
        IElement ChandeliersNavCategoryElement { get; }
        IElement MagicMerchandizerBodyElement { get; }
        IElement MoreNavCategoryElement { get; }
        IElement MoreNavLinkElement { get; }
        IElement SortResultContainerElement { get; }
        #endregion

        /// <summary>
        /// Get All Skus on the MM category sort page.
        /// </summary>
        /// <returns></returns>
        List<string> GetListOfSkus();

        Dictionary<string, string> GetFormulaKeyValues();

        /// <summary>
        /// Get All Products Information such as Formular, GMD... on the MM category sort page.
        /// </summary>
        /// <returns></returns>
        List<string> GetListOfGmdValues();
    }
}
