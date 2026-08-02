using System;
using Automation.Framework.Utilities;
using Xunit;
using Xunit.Abstractions;
using xRetry;
using OpenQA.Selenium;
using LampsPlus.AutomationFramework.Constants;
using Entities = LampsPlus.AutomationFramework.Databases.Entities;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.Common.Sort;

namespace LampsPlus.RegressionTests.Common.OtherPages
{
    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7776_Windows_VerifyResidentialProsProductPricingBlock : T7776_DesktopBase
    {
        public T7776_Windows_VerifyResidentialProsProductPricingBlock(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_PCSI)]
        public void ResidentialProsProductPricingBlockValues(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7776_Mac_VerifyResidentialProsProductPricingBlock : T7776_DesktopBase
    {
        public T7776_Mac_VerifyResidentialProsProductPricingBlock(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_PCSI)]
        public void ResidentialProsProductPricingBlockValues(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7776_iPad_VerifyResidentialProsProductPricingBlock : T7776_DesktopBase
    {
        public T7776_iPad_VerifyResidentialProsProductPricingBlock(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_PCSI)]
        public void ResidentialProsProductPricingBlockValues(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7776_TabletEmulator_VerifyResidentialProsProductPricingBlock : T7776_DesktopBase
    {
        public T7776_TabletEmulator_VerifyResidentialProsProductPricingBlock(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_PCSI)]
        public void ResidentialProsProductPricingBlockValues(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Sort)]
    public class T7780_iPhone_VerifyResidentialProsProductPricingBlock : T7780_MobileBase
    {
        public T7780_iPhone_VerifyResidentialProsProductPricingBlock(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_PCSI)]
        public void ResidentialProsProductPricingBlockValues(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7780_Emulator_VerifyResidentialProsProductPricingBlock : T7780_MobileBase
    {
        public T7780_Emulator_VerifyResidentialProsProductPricingBlock(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_PCSI)]
        public void ResidentialProsProductPricingBlockValues(string config) => Validate(config);
    }

    /// <summary>
	/// Verify Residential Product on Regular Price, Not Eligible Member Special, No Company in Session.
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9218
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7776
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9218"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7776")]
    public abstract class T7776_DesktopBase : T7776_T7780_Base
    {
        protected T7776_DesktopBase(ITestOutputHelper output) : base(output) { }
        protected override void VerifyMainPriceCallOut(Entities.ProductModel residentialProductPros)
        {
            Assert.True(ProductDetail.TradePriceLabel.Text.CaseInsensitiveContains("PROS SPECIAL PRICE"), "Main Price Callout not displayed");
            Assert.False(ProductDetail.IsSaleVerbiageVisible, "Sale verbiage is displayed");

        }
        protected override void VerifySticky(Entities.ProductModel residentialProductPros)
        {
            Assert.True(ProductDetail.TradePriceLabel.Text.CaseInsensitiveContains("PROS SPECIAL PRICE"), "Main Price Callout not displayed");
            Assert.False(ProductDetail.IsSaleVerbiageVisible, "Sale verbiage is displayed");

        }
    }

    /// <summary>
    /// Verify Residential Product on Regular Price, Not Eligible Member Special, No Company in Session.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9218
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7780
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9218"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7780")]
    public abstract class T7780_MobileBase : T7776_T7780_Base
    {
        protected T7780_MobileBase(ITestOutputHelper output) : base(output) { }
        protected override void VerifyMainPriceCallOut(Entities.ProductModel residentialProductPros)
        {
            Assert.Equals(ProductDetail.TradePriceLabel.Text.Substring(0, ProductDetail.TradePriceLabel.Text.LastIndexOf("L", StringComparison.Ordinal) + 1), "PROS SPECIAL", "Main Price Callout not displayed");
            Assert.False(SortFullPageCertona.IsPriceVerbiageVisible, "Sale verbiage is displayed");

        }
        protected override void VerifySticky(Entities.ProductModel residentialProductPros)
        {
            Assert.Equals(ProductDetail.TradePriceLabel.Text.Substring(0, ProductDetail.TradePriceLabel.Text.LastIndexOf("L", StringComparison.Ordinal) + 1), "PROS SPECIAL", "Main Price Callout not displayed");
            Assert.False(SortFullPageCertona.IsPriceVerbiageVisible, "Sale verbiage is displayed");

        }
    }
    
    public abstract class T7776_T7780_Base : SortTestsBase
    {
        protected T7776_T7780_Base(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            InitializeFramework(config);

            var residentialProductPros = ProductActions.GetSkuForResidentialProductPros();
            var sku = residentialProductPros.ShortSku;

            Assert.DatabaseObject(sku, "ProductActions.GetSkuForResidentialProductPros()");

            Browser.Navigate(Urls.ProductFullPageBaseUrl + sku);

            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

            var mainPrice = TextActions.RemoveDollarSign(ProductDetail.ItemPriceText).Trim();
            mainPrice = TextActions.RemoveTextBeforeAndIncludingCharacter(mainPrice, ':');

            var formattedMainPrice = TextActions.FormatToTwoDecimals(residentialProductPros.SpecialDiscount);
            var strikethrough = TextActions.RegexNoTabsAndNewLines(SortFullPageCertona.StrikeThroughPrice.Text).TrimStart();
            var strikethroughprice = TextActions.GetPriceTextOnly(strikethrough);

            var formattedstikethroughPrice = TextActions.FormatToTwoDecimals(residentialProductPros.RetailPriceInternet);
            var index2 = formattedstikethroughPrice.IndexOf(":", StringComparison.Ordinal);
            formattedstikethroughPrice = formattedstikethroughPrice.Substring(index2 + 1).Trim();

            VerifyMainPriceCallOut(residentialProductPros);
            
            Assert.Equals(formattedMainPrice, mainPrice, "Main Price is not matching with the database.");
            Assert.Equals(strikethroughprice, formattedstikethroughPrice, "StrikeThrough Price does not match");
            Assert.False(ProductDetail.IsCompareCalloutElementVisible, "Compare Callout is displayed");
            Assert.False(ProductDetail.IsSavePriceAndVerbiageVisible, "Save Price and Save verbiage is displayed");
            Assert.False(SortFullPageCertona.IsEndDateVerbiageVisible, "End Sale verbiage is displayed");
          
            var sfpUrl = Browser.PageUrl;
            Browser.ScrollToBottomOfPage(sfpUrl);

            VerifySticky(residentialProductPros);
            
            Browser.Navigate(Urls.PlaSortPageBaseUrl + sku);

            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

            VerifyMainPriceCallOut(residentialProductPros);

            Assert.Equals(formattedMainPrice, mainPrice, "Main Price is not matching with the database.");
            Assert.Equals(strikethroughprice, formattedstikethroughPrice, "StrikeThrough Price does not match");
            Assert.False(ProductDetail.IsSavePriceAndVerbiageVisible, "Save Price and Save verbiage is displayed");
            Assert.False(ProductDetail.IsCompareCalloutElementVisible, "Compare Callout is displayed");
            Assert.False(SortFullPageCertona.IsEndDateVerbiageVisible, "End Sale verbiage is displayed");
           
        }

        protected abstract void VerifyMainPriceCallOut(Entities.ProductModel residentialProductPros);
        protected abstract void VerifySticky(Entities.ProductModel residentialProductPros);
    }
}
