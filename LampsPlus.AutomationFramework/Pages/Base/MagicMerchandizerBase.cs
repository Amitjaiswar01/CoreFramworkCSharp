using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.UI;
using Automation.Framework;

using Page = Automation.Framework.Core.Page;

namespace LampsPlus.AutomationFramework.Pages.Base
{
    /// <summary>
    /// https://mm.lampsplus.com/SignIn?ReturnUrl=%2f
    /// </summary>
    public class MagicMerchandizerBase : Page, IMagicMerchandizer
    {
        /// <inheritdoc />
        public MagicMerchandizerBase(IBrowser browser) : base(browser) { }

        #region Class Setup
        public string ChandeliersCategory => "Chandeliers";
        public string MoreCategory => "More";
        #endregion

        #region CSS Selector Strings
        private string AdvancedOptionsId { get; } = "advancedOptions";
        private string ChandeliersId { get; } = "chandeliers";
        private string MmAdvancedContainerClass { get; } = "mmAdvancedContainer";
        private string MmProductsContainerId { get; } = "mmProductsContainer";
        private string MmWrapperClass { get; } = "mmWrapper";
        private string MoreId { get; } = "more";
        private string SortResultContainerClass { get; } = "sortResultContainer";
        private string SortResultGmdClass { get; } = "sortResultGMD";
        private string SortResultsProdInfoClass { get; } = "sortResultsProdInfo";
        #endregion

        #region Page Elements
        private IElement MmProductsContainer => Browser.Locate.ElementById(MmProductsContainerId);

        public IElement AdvancedOptionsElement => Browser.Locate.ElementById(AdvancedOptionsId);
        public IElement AdvanceOptionsFormulaElement => Browser.Locate.ElementByClassName(MmAdvancedContainerClass);
        public IElement ChandelierNavLinkElement => Browser.Locate.ElementByLinkText(ChandeliersCategory, ChandeliersNavCategoryElement);
        public IElement ChandeliersNavCategoryElement => Browser.Locate.ElementById(ChandeliersId);
        public IElement MagicMerchandizerBodyElement => Browser.Locate.ElementByClassName(MmWrapperClass);
        public IElement MoreNavCategoryElement => Browser.Locate.ElementById(MoreId);
        public IElement MoreNavLinkElement => Browser.Locate.ElementByLinkText(MoreCategory, MoreNavCategoryElement);
        public IElement SortResultContainerElement => Browser.Locate.ElementByClassName(SortResultContainerClass);

        #endregion

        /// <summary>
        /// Get All Skus on the MM category sort page.
        /// </summary>
        /// <returns></returns>
        public List<string> GetListOfSkus()
        {
            var listOfProductShortSku = new List<string>();

            foreach (var sku in Browser.Locate.ElementsByClassName(SortResultContainerClass, MmProductsContainer))
            {
                listOfProductShortSku.Add(sku.GetAttribute(HtmlTextWriterAttribute.Id.ToString()));
            }
            return listOfProductShortSku;
        }

        /// <summary>
        /// Get list of all foruma key value
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetFormulaKeyValues()
        {
            var listOfFormulaValue = new Dictionary<string, string>();
            foreach (var li in Browser.Locate.ElementsByTagName(HtmlTextWriterTag.Li, AdvanceOptionsFormulaElement))
            {
                var key = Browser.Locate.ElementByTagName(HtmlTextWriterTag.Span, li).Text;
                var value = Browser.Locate.ElementByTagName(HtmlTextWriterTag.Input, li).GetAttribute(HtmlTextWriterAttribute.Value.ToString());

                listOfFormulaValue.Add($"@#{key}", value);
            }

            return listOfFormulaValue;        
        }
        /// <summary>
        /// Get All Products Information such as Formular, GMD... on the MM category sort page.
        /// </summary>
        /// <returns></returns>
        public List<string> GetListOfGmdValues()
        {
             var gMdValues = new List<string>();

             foreach (var sku in Browser.Locate.ElementsByClassName(SortResultContainerClass, MmProductsContainer))
             {
                 var sortResultsProdInfo = Browser.Locate.ElementByClassName(SortResultsProdInfoClass, sku);
                 var gmdValue = Browser.Locate.ElementByClassName(SortResultGmdClass, sortResultsProdInfo);
                var allSpanText = gmdValue.Text;

               // Regex regex = new Regex("([.\\d,]+)", RegexOptions.Multiline);

                Regex regex = new Regex("([-*.\\d,]+)", RegexOptions.Multiline);
                MatchCollection splitMatchCollectionOfString = regex.Matches(allSpanText);

                var formulaValue = Convert.ToDecimal(splitMatchCollectionOfString[0].ToString());

                var gmdBySort = Convert.ToDecimal(splitMatchCollectionOfString[1].ToString());
                var gmdAllOtherSorts = Convert.ToDecimal(splitMatchCollectionOfString[2].ToString()); 
                var atcValue = Convert.ToDecimal(splitMatchCollectionOfString[3].ToString());        
                var pdpValue = Convert.ToDecimal(splitMatchCollectionOfString[4].ToString());
               

                var totalValue = gmdBySort + gmdAllOtherSorts + atcValue + pdpValue;

                gMdValues.Add($"{totalValue} {formulaValue}");
            }
             return gMdValues;
        }
    }
}
