using System;
using System.Collections.ObjectModel;
using System.Web.UI;
using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Pages.Desktop
{
    /// <summary>
    /// Example page: https://www.lampsplus.com/products/possini-euro-design-vicina-chrome-led-torchiere-floor-lamp__4g433.html.
    /// </summary>
    public class ProductDetailMultiProduct : ProductDetailMultiProductBase
    {
        public ProductDetailMultiProduct(IBrowser browser, IGlobalLocators globalLocators, IProductDetail productDetail) : base(browser, globalLocators, productDetail) { }

        #region Class Setup
        public override string CllOutClass { get; } = "cllout";
        public override string MultiSelectClass { get; } = "lpSelect";

        public override string MultiOptionMenuOpenId => throw new System.NotImplementedException();
        public override string MultiProdOptionPriceClass => throw new System.NotImplementedException();
        public override string MultiProdSizeOptionsOption => throw new System.NotImplementedException();
        public override string OptionSectionTitleClass => throw new System.NotImplementedException();
        public override string ProdOptionDescriptionClass => throw new System.NotImplementedException();
        public override bool IsMultiProductOverlayOpen(int timeToWait) => throw new System.NotImplementedException();
        #endregion

        #region Page Elements
        public IElement AddToCartContainer => Browser.Locate.ElementByClassName(AddToCartContainerClass);

        public override IElement ShipsFreeWithOrdersOver49CallOutForMultiProduct => Browser.Locate.ElementByClassName(CllOutClass, Browser.Locate.ElementByClassName(MultiSelectClass));
        public override IElement AvailableOptionsSectionTitle => Browser.Locate.ElementByClassName(H6Class, AddToCartContainer);
        public override IElement SelectedMultiProductDropdownOption => Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.Button, MainSelectorClass);
        public override IElement MultiProdSizeOptions => Browser.Locate.ElementByClassName(MultiProdSizeOptionsClass, AddToCartContainer);
        public override IElement UnselectedMultiProductDropdownOption => Browser.Locate.ElementBySelector($".{MultiProdSizeOptionsOptionClass}:not(.{GlobalLocators.SelectedTextString})");
       
        public override ReadOnlyCollection<IElement> MultiProductPrices => Browser.Locate.ElementsByClassName(PriceColClass, MultiProdSizeOptions);
        public override ReadOnlyCollection<IElement> MultiProductOptionNames => Browser.Locate.ElementsByClassName(ListNameClass, MultiProdSizeOptions);
        public override ReadOnlyCollection<IElement> MultiProductDropdownOptions => Browser.Locate.ElementsByClassName(MultiProdSizeOptionsOptionClass);
       
        public override ReadOnlyCollection<IElement> MultiProductRadioOptions => throw new NotImplementedException();
        #endregion
    }
}
