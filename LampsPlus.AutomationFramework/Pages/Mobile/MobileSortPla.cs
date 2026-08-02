using System;
using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Base;
using OpenQA.Selenium;

namespace LampsPlus.AutomationFramework.Pages.Mobile
{
    /// <summary>
    /// Example page: https://www.lampsplus.com/products/chandeliers/style_crystal/?sfp=00044.
    /// </summary>
    public class MobileSortPla : SortPlaBase
    {
        public MobileSortPla(IBrowser browser) : base(browser) { }

        #region CSS Selector Strings

        public override string PlaCertonaImageLoadedXpath { get; } = "//*[@id=\"certonaItems\"]/div[1]/a[contains(@target, '_parent')]";
        public override string QlViewDetailsId { get; } = "moreDetails";
        public override string PlaDetailsId { get; } = "qlBkg";
        public override string PlaMainImageLoadedXpath { get; } = "//*[@id='qlBkg']/div[1]/a[contains(@target, '_parent')]";
        public override string PlaMainImageClass { get; } = "glider-container";
        public override string PlaSortStickyFilterHeaderId { get; } = "pdpStickyHeader";
        public override string ProductTitleClass { get; } = "productTitle";
        public override string ProductImageClass { get; } = "plaImage";
        public override string ProductLargeImageContainerId { get; } = "fsContent";
        public override string PhoneNumberClass { get; } = "anchorLink";
        public override string LargeImageCloseButtonClass { get; } = "jsCloseZoomImageModal";

        public override string AttributeWrapper => throw new NotImplementedException();
        public override string QlTitleWrapperClass => throw new NotImplementedException();
        #endregion

        /// <summary>
        /// PLA more details link element.
        /// </summary>
        public override IElement PlaMoreDetailsLinkElement => Browser.Locate.ElementById(MoreDetailsId);

        /// <summary>
        /// PLA product name element.
        /// </summary>
        public override IElement PlaProductTitleElement => Browser.Locate.ElementById(PlaDetailsId).FindElement(By.ClassName(ProductTitleClass));
        
        /// <summary>
        /// PLA view large image link element.
        /// </summary>
        public override IElement PlaViewLargerLinkElement => Browser.Locate.ElementByClassName(ProductImageClass);

        /// <summary>
        /// PLA large image element.
        /// </summary>
        public override IElement PlaLargeImageElement => Browser.Locate.ElementById(ProductLargeImageContainerId);

        /// <summary>
        /// PLA large image close button element.
        /// </summary>
        public override IElement PlaCloseButtonElement => Browser.Locate.ElementByClassName(LargeImageCloseButtonClass);

        /// <summary>
        /// PLA sort sticky filter header.
        /// </summary>
        public override IElement PlaSortStickyFilterHeader => Browser.Locate.ElementById(PlaSortStickyFilterHeaderId);

        /// <summary>
        /// PLA ProductNotAvailable Callout.
        /// </summary>
        public override IElement ProductNotAvailableCallout => Browser.Locate.ElementByTagName(HtmlTextWriterTag.Span, Browser.Locate.ElementById(ProductNotAvailableCalloutId));

        /// <summary>
        /// PLA ProductNotAvailable Callout. - Mobile  
        /// </summary>
        public override IElement ProductNotAvailableCalloutNew => Browser.Locate.ElementByClassName(ProductNotAvailableCalloutClass); 

        /// <summary>
        /// "Ships Today!" callout on some mobile views (PLA and SFP)
        /// </summary>
        public override IElement ShipsTodayQLElement => Browser.Locate.ElementBySelector(ShipsTodayQLClass.ToCssClassSelector());

        /// <summary>
        /// "Ships Today!" callout on Pdp
        /// </summary>
        public override IElement ShipsTodayPdpElement => Browser.Locate.ElementBySelector(ShipsTodayPdpClass.ToCssClassSelector());

        public override IElement PlaFilters => throw new NotImplementedException();

        /// <summary>
        /// PLA rating box element.
        /// </summary>
        public override IElement PlaRatingBoxElement => throw new NotImplementedException();

        /// <summary>
        /// PLA read reviews element.
        /// </summary>
        public override IElement PlaReadReviewsElement => throw new NotImplementedException();

        /// <summary>
        /// PLA Questions and Answers element.
        /// </summary>
        public override IElement PlaQuestionsElement => throw new NotImplementedException();

        public override IElement PlaEmail => Browser.Locate.ElementBySelector(EmailFieldId.ToCssIdSelector());          
        public override IElement PlaEmailNew => Browser.Locate.ElementByClassName(PlaEmailNewClass);    
        public override IElement PhoneNumber => Browser.Locate.ElementByClassName(PhoneNumberClass);
        public override IElement EmailField => Browser.Locate.ElementById(EmailFieldId);
        public override IElement NavigateToPlaWithReviews(string url, string sku)
        {
            Browser.Navigate($"{url}?sfp={sku}");
            Browser.Wait.IsVisibleElement(By.ClassName(PlaMainImageClass));
            return Browser.Locate.ElementByClassName(PlaMainImageClass);
        }

        public override IElement VerifyPlaStickyHeader()
        {
            Browser.ScrollToBottomOfWindow();
            return Browser.Wait.ForDisplayedElement(PlaSortStickyFilterHeader);
        }

        public override void ClickOnPlaProduct()
        {
            Browser.ScrollToTopOfWindow();
            Browser.Wait.ForDomReady();
            Browser.SwitchFocusToIframe(PlaFrameElement);
            PlaProductTitleElement.Click();
        }
    }
}
