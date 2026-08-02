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
    public abstract class SortFullPageCertonaBase : Page, ISortFullPageCertona
    {
        /// <inheritdoc />
        protected SortFullPageCertonaBase(IBrowser browser, TestsBase testsBase) : base(browser) { TestsBase = testsBase; }

        internal TestsBase TestsBase { get; }
        private string CertonaItemsXpath { get; } = "//*[@id='certonaItems']";
        private string CompareCallOutClass { get; } = "comparePrice";
        private string MainePriceId { get; } = "lblPrice";
        private string SortResultImgContainerClass { get; } = "sortResultImgContainer";

        public string CertonaSimilarDesignsItemsFirstItem { get; } = "(//*[@id='certonaItems']//img[@class='unveil unveil--done'])[1]";
        public string EndCalloutClass { get; } = "saleEnd";
        public string PdStoreDetailsTitleByClass { get; } = "pdStoreDetails__title";
        public string TextLabel { get; } = "TEXT";
        public abstract string AddressByClass { get; }
        public abstract string DailySaleCalloutClass { get; }
        public abstract string DailySaleCalloutId { get; }
        public abstract string MainPrice { get; }
        public abstract string MobileStoreAddressAndHoursByClass { get; }
        public abstract string MobileStoreNameByClass { get; }
        public abstract string MobileStrikeThroughPriceXpath { get; }
        public abstract string QlStoreLocationLinkByClass { get; }
        public abstract string QlStoreLocationInfoContactInfoByClass { get; }
        public abstract string SaveCalloutId { get; }
        public abstract string SaveCallOutId { get; }
        public abstract string StoreNameByClass { get; }
        public abstract string PhoneAndTextNumbersClass { get; }
        public abstract string StrikeThroughClass { get; }
        public abstract string ProductPriceTypeXpath { get; }
        public abstract string SaleClass { get; }

        public IElement PhoneAndTextNumbers(int index) => Browser.Locate.ElementsByClassName(PhoneAndTextNumbersClass)[index];
        public IElement AddressLocalityField => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Span, ItemPropAttribute, "addressLocality");
        public IElement AddressRegionField => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Span, ItemPropAttribute, "addressRegion");
        public IElement ComparableValueCallOut => Browser.Locate.ElementByClassName(CompareCallOutClass);
        public IElement FirstDisplayedSimilarDesignElement => Browser.Locate.ElementByClassName(SortResultImgContainerClass);
        public IElement FullPageCertonaSimilarDesignsTitleElement => Browser.Locate.ElementByClassName(TestsBase.Sort.JsCertonaTitleClass);
        public IElement FullPageCertonaItemInSimilarDesignsSection => Browser.Locate.ElementByTagName(HtmlTextWriterTag.Img, FirstDisplayedSimilarDesignElement);
        public IElement FullPageCertonaSimilarDesignsContainer => Browser.Locate.ElementByXpath(CertonaItemsXpath);
        public IElement FullPageCertonaSimilarDesignsItemsFirstItem => Browser.Locate.ElementByXpath(CertonaSimilarDesignsItemsFirstItem);
        public IElement MainPriceOnSfp => Browser.Locate.ElementById(MainePriceId);
        public IElement PostalCodeField => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Span, ItemPropAttribute, "postalCode");
        public IElement StreetAddressField => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Span, ItemPropAttribute, "streetAddress");
        public IElement StoreDetailInfo => Browser.Locate.ElementByClassName(PdStoreDetailsTitleByClass);

        public abstract IElement AddressInformation { get; }
        public abstract IElement DailySaleCallout { get; }
        public abstract IElement EndCallOut { get; }
        public abstract IElement MobileStrikeThroughPrice { get; }
        public abstract IElement SaveCallOut { get; }
        public abstract IElement StoreName { get; }
        public abstract IElement StoreAddressAndHours { get; }
        public abstract IElement StrikeThroughPrice { get; }
        public abstract bool IsPriceVerbiageVisible { get; }
        public bool IsMobileSaleVerbiage => Browser.Locate.ElementBySelector($"{HtmlTextWriterTag.Ul} {HtmlTextWriterTag.Li.ToDirectChildSelector().ToNthChildSelector(3)}", Browser.Locate.ElementById(SaveCallOutId)).IsInitialized;

        public ReadOnlyCollection<IElement> FullPageCertonaSimilarDesignsItems => Browser.Locate.ElementsByClassName(TestsBase.Sort.SortResultContainerClass, FullPageCertonaSimilarDesignsContainer);
        public bool IsEndDateVerbiageVisible => Browser.Locate.ElementByClassName(EndCalloutClass).IsInitialized;
    }
}
