using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Base;
using System.Web.UI;
using System;
using Automation.Framework.Utilities;

namespace LampsPlus.AutomationFramework.Pages.Desktop
{
    /// <summary>
    /// Example page: https://www.lampsplus.com/sfp/U4514/.
    /// </summary>
    public class SortFullPageCertona : SortFullPageCertonaBase
    {
        /// <inheritdoc />
        public SortFullPageCertona(IBrowser browser, TestsBase testsBase) : base(browser, testsBase) { }
        public override string AddressByClass { get; } = "qlStoreLocationInfo__address";
        public override string DailySaleCalloutId { get; } = "lblPriceType";
        public override string MainPrice { get; } = "lblPrice";
        public override string PhoneAndTextNumbersClass { get; } = "qlStoreLocationInfo__text";
        public override string QlStoreLocationLinkByClass { get; } = "qlStoreLocation__link";
        public override string QlStoreLocationInfoContactInfoByClass { get; } = "qlStoreLocationInfo__contactInfo";
        public override string SaveCalloutId { get; } = "priceAdditionalSave";
        public override string StoreNameByClass { get; } = "qlStoreLocation__text";
        public override string StrikeThroughClass { get; } = "pricingCalloutContainer";
        public override string DailySaleCalloutClass => throw new NotImplementedException();
        public override string SaveCallOutId => throw new NotImplementedException();
        public override string MobileStoreAddressAndHoursByClass => throw new NotImplementedException();
        public override string MobileStoreNameByClass => throw new NotImplementedException();
        public override string MobileStrikeThroughPriceXpath => throw new NotImplementedException();
        public override string ProductPriceTypeXpath => throw new NotImplementedException();
        public override string SaleClass => throw new NotImplementedException();

        public IElement AddressName => Browser.Locate.ElementByClassName(AddressByClass);

        public override bool IsPriceVerbiageVisible => Browser.Locate.ElementById(DailySaleCalloutId).IsInitialized;
        public override IElement AddressInformation => Browser.Locate.ElementByTagName(HtmlTextWriterTag.Strong, AddressName);
        public override IElement DailySaleCallout => Browser.Locate.ElementById(DailySaleCalloutId);
        public override IElement EndCallOut => Browser.Locate.ElementByClassNames(EndCalloutClass);
        public override IElement SaveCallOut => Browser.Locate.ElementById(SaveCalloutId);
        public override IElement StoreName => Browser.Locate.ElementByClassName(StoreNameByClass);
        public override IElement StoreAddressAndHours => Browser.Locate.ElementByClassName(QlStoreLocationLinkByClass);
        public override IElement StrikeThroughPrice => Browser.Locate.ElementBySelector(StrikeThroughClass.ToCssClassSelector());
        public override IElement MobileStrikeThroughPrice => throw new NotImplementedException();
    }
}
