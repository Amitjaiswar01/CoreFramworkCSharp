using System;
using System.Collections.ObjectModel;
using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Pages.Mobile
{
    /// <summary>
    /// Example page: https://www.lampsplus.com/products/possini-euro-design-vicina-chrome-led-torchiere-floor-lamp__4g433.html.
    /// </summary>
    public class MobileProductDetailMultiProduct : ProductDetailMultiProductBase
    {
        public MobileProductDetailMultiProduct(IBrowser browser, IGlobalLocators globalLocators, IProductDetail productDetail) : base(browser, globalLocators, productDetail) { }

       
        #region CSS Selector Strings
        private string MultiProductRadio { get; } = "MultiproductRadio";
        private string ProdOptionsMenuId { get; } = "prodOptionsMenu";

        public override string CllOutClass {get; } = "pdMultiProdCallout";        
        public override string MultiOptionMenuOpenId { get; } = "prodOptionsMenuOpen";
        public override string MultiProdOptionPriceClass { get; } = "pdMultiProdPrice";
        public override string MultiProdSizeOptionsOption { get; } = "multiProdSizeOptions__option";
        public override string OptionSectionTitleClass { get; } = "sectionTitle--multiOption";
        public override string ProdOptionDescriptionClass { get; } = "prodOptionDescription";

        public override string MultiSelectClass => throw new System.NotImplementedException();
        #endregion

        #region Page Elements

        private IElement MultiProductOverlay => Browser.Locate.ElementBySelector(ProdOptionsMenuId.ToCssIdSelector());
        public override IElement AvailableOptionsSectionTitle => Browser.Locate.ElementByClassName(OptionSectionTitleClass);
        public override IElement SelectedMultiProductDropdownOption => Browser.Locate.ElementById(MultiOptionMenuOpenId);
        public override IElement ShipsFreeWithOrdersOver49CallOutForMultiProduct => Browser.Locate.ElementByClassName(CllOutClass);
        public override IElement UnselectedMultiProductDropdownOption => GetFirstUnselectedMultiProductItem();

        public override ReadOnlyCollection<IElement> MultiProductDropdownOptions => Browser.Locate.ElementsByTagName(HtmlTextWriterTag.Li, MultiProdSizeOptionsElement);
        public override ReadOnlyCollection<IElement> MultiProductOptionNames => Browser.Locate.ElementsByTagNameAndClassName(HtmlTextWriterTag.Span, ProdOptionDescriptionClass);
        public override ReadOnlyCollection<IElement> MultiProductPrices => Browser.Locate.ElementsByClassName(MultiProdOptionPriceClass, MultiProdSizeOptionsElement);
        public override ReadOnlyCollection<IElement> MultiProductRadioOptions => Browser.Locate.ElementsByTagNameAndAttributeEquals(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Name, MultiProductRadio);

        public override IElement MultiProdSizeOptions => throw new NotImplementedException();
        #endregion

        private IElement GetFirstUnselectedMultiProductItem()
        {
            var multiProductOptionsList = Browser.Locate.ElementsByXpath("//ul[contains(@class,'multiProdSizeOptions')]/li");

            for (int i = 0; i <= multiProductOptionsList.Count; i++) 
            {
                if (!multiProductOptionsList[i].GetCssValue("label").Contains("multiProduct::before"))
                {
                    return multiProductOptionsList[i];
                }
            }

            return null;
        }

        public override bool IsMultiProductOverlayOpen(int timeToWait)
        {
            return Browser.Wait.ForCondition(() => MultiProductOverlay.GetAttribute("aria-hidden") == "false");
        }
    }
}
