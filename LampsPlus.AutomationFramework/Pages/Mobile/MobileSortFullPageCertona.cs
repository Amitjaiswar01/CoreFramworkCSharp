using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Base;
using System;
using System.Web.UI;
using Automation.Framework.Utilities;

    namespace LampsPlus.AutomationFramework.Pages.Mobile
{
    /// <summary>
    /// Example page: https://www.lampsplus.com/sfp/U4514/.
    /// </summary>
    public class MobileSortFullPageCertona : SortFullPageCertonaBase
    {
        /// <inheritdoc />
        public MobileSortFullPageCertona(IBrowser browser, TestsBase testsBase) : base(browser, testsBase) { }
        public override string DailySaleCalloutClass { get; } = "upperCase";
        public override string SaleClass { get; } = "sale";
        public override string SaveCallOutId { get; } = "pnlProductPrice";
        public override string MobileStoreAddressAndHoursByClass { get; } = "pdStoreInfo__link";
        public override string MobileStoreNameByClass { get; } = "pdStoreInfo__header";
        public override string MobileStrikeThroughPriceXpath { get; } = "//*[@id='pnlProductPrice']/div[1]/ul";
        public override string ProductPriceTypeXpath { get; } = "//strong[contains(@class,'productPriceType')]";
        public override string AddressByClass => throw new NotImplementedException();
        public override string DailySaleCalloutId => throw new NotImplementedException();
        public override string MainPrice => throw new NotImplementedException();
        public override string PhoneAndTextNumbersClass => throw new NotImplementedException();
        public override string QlStoreLocationLinkByClass => throw new NotImplementedException();
        public override string QlStoreLocationInfoContactInfoByClass => throw new NotImplementedException();
        public override string SaveCalloutId => throw new NotImplementedException();
        public override string StoreNameByClass => throw new NotImplementedException();
        public override string StrikeThroughClass { get; } = "lblOriginalPrice";
        public override IElement AddressInformation => Browser.Locate.ElementByClassName(PdStoreDetailsTitleByClass);
        public override IElement DailySaleCallout => Browser.Locate.ElementByClassName(DailySaleCalloutClass);
        public override IElement EndCallOut => Browser.Locate.ElementByClassName(EndCalloutClass);
        public override IElement MobileStrikeThroughPrice => Browser.Locate.ElementByXpath(MobileStrikeThroughPriceXpath);
        public override IElement SaveCallOut => Browser.Locate.ElementBySelector(HtmlTextWriterTag.Strong.ToNthChildSelector(3), Browser.Locate.ElementByTagName(HtmlTextWriterTag.Div, Browser.Locate.ElementBySelector(SaveCallOutId.ToCssIdSelector())));
        public override IElement StoreAddressAndHours => Browser.Locate.ElementByClassName(MobileStoreAddressAndHoursByClass);
        public override IElement StoreName => Browser.Locate.ElementByClassName(MobileStoreNameByClass);
        public override IElement StrikeThroughPrice => Browser.Locate.ElementBySelector(StrikeThroughClass.ToCssClassSelector());
        public override bool IsPriceVerbiageVisible => Browser.Locate.ElementByClassName(DailySaleCalloutClass).IsInitialized;
    }
}