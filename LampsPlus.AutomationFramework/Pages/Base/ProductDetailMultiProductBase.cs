using System.Collections.ObjectModel;
using Automation.Framework;
using Automation.Framework.Utilities;

using Page = Automation.Framework.Core.Page;

namespace LampsPlus.AutomationFramework.Pages.Base
{
    /// <summary>
    /// Base class for common behavior between desktop and mobile views.
    /// </summary>
    public abstract class ProductDetailMultiProductBase : Page, IProductDetailMultiProduct
    {
        protected ProductDetailMultiProductBase(IBrowser browser, IGlobalLocators globalLocators, IProductDetail productDetail) : base(browser)
        {
            GlobalLocators = globalLocators;
            ProductDetail = productDetail;
        }

        #region Class Setup
        internal IGlobalLocators GlobalLocators { get; }
        internal IProductDetail ProductDetail { get; }

        public string MultiProductAvailableOptionsText { get; } = "Available Options";
        #endregion

        #region CSS Selector Strings
        public string AddToCartContainerClass { get; } = "addToCartContainer";
        public string H6Class { get; } = "h6";
        public string ListNameClass { get; } = "listName";
        public string MainSelectorClass { get; } = "mainSelector";
        public string MultiProdSizeOptionsClass { get; } = "multiProdSizeOptions";
        public string MultiProdSizeOptionsOptionClass { get; } = "multiProdSizeOptions__option";
        public string PriceColClass { get; } = "priceCol";

        public abstract string MultiSelectClass { get; }
        public abstract string CllOutClass { get; }
        public abstract string MultiOptionMenuOpenId { get; }
        public abstract string MultiProdOptionPriceClass { get; }
        public abstract string MultiProdSizeOptionsOption { get; }
        public abstract string OptionSectionTitleClass { get; }
        public abstract string ProdOptionDescriptionClass { get; }

        public abstract bool IsMultiProductOverlayOpen(int timeToWait);
        #endregion

        #region Page Elements
        public IElement MultiProdSizeOptionsElement => Browser.Locate.ElementByClassName(MultiProdSizeOptionsClass);
       
        public abstract IElement AvailableOptionsSectionTitle { get; }
        public abstract IElement MultiProdSizeOptions { get; }
        public abstract IElement SelectedMultiProductDropdownOption { get; }
        public abstract IElement ShipsFreeWithOrdersOver49CallOutForMultiProduct { get; }
        public abstract IElement UnselectedMultiProductDropdownOption { get; }
       
        public abstract ReadOnlyCollection<IElement> MultiProductPrices { get; }
        public abstract ReadOnlyCollection<IElement> MultiProductOptionNames { get; }
        public abstract ReadOnlyCollection<IElement> MultiProductDropdownOptions { get; }
        public abstract ReadOnlyCollection<IElement> MultiProductRadioOptions { get; }
        #endregion

        public string GetShippingCallOut()
        {
            if (Browser.Locate.ElementImmediately(MultiProdSizeOptionsClass.ToCssClassSelector()).IsInitialized)
            {
                return ShipsFreeWithOrdersOver49CallOutForMultiProduct.Text;
            }

            return ProductDetail.ShipsFreeWithOrdersOver49CallOut.Text;
        }
    }
}
