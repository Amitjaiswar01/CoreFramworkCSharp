using System.Web.UI;
using Automation.Framework;
using Page = Automation.Framework.Core.Page;

namespace LampsPlus.AutomationFramework.Pages.Base
{
    /// <summary>
    /// Base class for common behavior between desktop and mobile views.
    /// </summary>
    public abstract class LightingCollectionBase : Page, ILightingCollection 
    {
        protected LightingCollectionBase(IBrowser browser) : base(browser) { }

        #region CSS Selector Strings
        private string BdLightingCollId { get; } = "bdLightingColl";
        private string ColorPlusUrl { get; } = "/images/color-plus/Color-Plus-Candlesticks";
        private string InputCheckBoxClass { get; } = "inputCheckbox";
        private string LnkViewDetailsClass { get; } = "lnkViewDetails";
        private string PriceContainerClass { get; } = "priceContainer";
        private string SlickListClass { get; } = "slick-list";
        private string TrendingCertonaItemsId { get; } = "trendingCertonaItems";

        public string ProductPrice => Browser.Locate.ElementByTagName(HtmlTextWriterTag.H2, PriceElement).ToString().Replace("$", string.Empty);
        public string ProductSku => CheckBoxElement.GetAttribute(HtmlTextWriterAttribute.Value.ToString());
        #endregion

        #region Page Elements
        public IElement CandleHolderSetImage => Browser.Locate.ElementByTagNameAndAttributeStartsWith(HtmlTextWriterTag.Img, HtmlTextWriterAttribute.Src, ColorPlusUrl);
        public IElement CheckBoxElement => Browser.Locate.ElementByClassName(InputCheckBoxClass);
        public IElement LightingCollectionElement => Browser.Locate.ElementById(BdLightingCollId);
        public IElement PriceElement => Browser.Locate.ElementByClassName(PriceContainerClass);
        public IElement RelatedVideosSlider => Browser.Locate.ElementByClassName(SlickListClass);
        public IElement TopTrendingSlider => Browser.Locate.ElementById(TrendingCertonaItemsId);
        public IElement ViewDetailsElement => Browser.Locate.ElementByClassName(LnkViewDetailsClass);
        #endregion
    }
}
