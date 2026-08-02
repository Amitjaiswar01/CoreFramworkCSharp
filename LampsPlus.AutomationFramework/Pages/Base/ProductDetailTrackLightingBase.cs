using System.Collections.ObjectModel;
using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Utilities;
using Page = Automation.Framework.Core.Page;

namespace LampsPlus.AutomationFramework.Pages.Base
{
    /// <summary>
    /// Base class for common behavior between desktop and mobile views.
    /// </summary>
    public abstract class ProductDetailTrackLightingBase : Page, IProductDetailTrackLighting
    {
        protected ProductDetailTrackLightingBase(IBrowser browser, IGlobalLocators globalLocators) : base(browser) { GlobalLocators = globalLocators; }

        #region Class Setup
        internal IGlobalLocators GlobalLocators { get; }
        #endregion

        #region CSS Selector Strings
        public abstract string BuildFullSystemOptionsId { get; }
        public abstract string BuildFullSystemId { get; }
        public abstract string DyoBannerClass { get; }
        public abstract string LeftId { get; }
        public abstract string PdAddToPortfolioSystemOptionsId { get; }
        public abstract string PdAddToCartSystemOptionsId { get; }
        #endregion

        #region Page Elements
        public abstract IElement BuildFullSystemAddToCartButton { get; }
        public abstract IElement BuildFullSystemAddToWishListButton { get; }
        public abstract IElement BuildFullSystemContainer { get; }
        public abstract IElement BuildFullSystemOptions { get; }
        public abstract IElement DesignYourOwnTrackLightingSystemBanner { get; }
        public abstract IElement DyotsSelectRoom { get; }

        public ReadOnlyCollection<IElement> ListOfFullSystemData(int nthIndex) => Browser.Locate.ElementsBySelector($"{HtmlTextWriterTag.Td.ToNthChildSelector(nthIndex)}", BuildFullSystemOptions);
       
        public abstract ReadOnlyCollection<IElement> ListOfFullSystemProductNames { get; }
        public abstract ReadOnlyCollection<IElement> ListOfFullSystemSkus { get; }
        #endregion
    }
}
