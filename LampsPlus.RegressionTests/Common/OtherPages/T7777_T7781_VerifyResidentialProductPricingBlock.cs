using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.Common.Sort;
using OpenQA.Selenium;
using xRetry;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.Common.OtherPages
{
    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7777_Windows_VerifyResidentialProductPricingBlock : T7777_DesktopBase
    {
        public T7777_Windows_VerifyResidentialProductPricingBlock(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void ResidentialProductPricingBlockValues(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7777_Windows_Kiosk_VerifyResidentialProductPricingBlock : T7777_DesktopBase
    {
        public T7777_Windows_Kiosk_VerifyResidentialProductPricingBlock(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_UNSI)]
        public void ResidentialProductPricingBlockValues(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7777_Mac_VerifyResidentialProductPricingBlock : T7777_DesktopBase
    {
        public T7777_Mac_VerifyResidentialProductPricingBlock(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void ResidentialProductPricingBlockValues(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7777_iPad_VerifyResidentialProductPricingBlock : T7777_DesktopBase
    {
        public T7777_iPad_VerifyResidentialProductPricingBlock(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void ResidentialProductPricingBlockValues(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7777_TabletEmulator_VerifyResidentialProductPricingBlock : T7777_DesktopBase
    {
        public T7777_TabletEmulator_VerifyResidentialProductPricingBlock(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void ResidentialProductPricingBlockValues(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Sort)]
    public class T7781_iPhone_VerifyResidentialProductPricingBlock : T7781_MobileBase
    {
        public T7781_iPhone_VerifyResidentialProductPricingBlock(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void ResidentialProductPricingBlockValues(string config) => Validate(config);
    }

    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7781_Emulator_VerifyResidentialProductPricingBlock : T7781_MobileBase
    {
        public T7781_Emulator_VerifyResidentialProductPricingBlock(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void ResidentialProductPricingBlockValues(string config) => Validate(config);
    }

    /// <summary>
	/// Verify Residential Product on Regular Price, Not Eligible Member Special, No Company in Session.
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9219
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7777
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9219"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7777")]
    public abstract class T7777_DesktopBase : T7777_T7781_Base
    {
        protected T7777_DesktopBase(ITestOutputHelper output) : base(output) { }
    }

    /// <summary>
    /// Verify Residential Product on Regular Price, Not Eligible Member Special, No Company in Session.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9219
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7718
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9219"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7718")]
    public abstract class T7781_MobileBase : T7777_T7781_Base
    {
        protected T7781_MobileBase(ITestOutputHelper output) : base(output) { }
        protected override void Validate(string config)
        {
            InitializeFramework(config);

            var residentialProduct = ProductActions.GetSkuForResidentialProduct();
            var sku = residentialProduct.ShortSku;

            Assert.DatabaseObject(sku, "ProductActions.GetSkuForResidentialProduct()");

            Browser.Navigate(Urls.ProductFullPageBaseUrl + sku);

            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

            var mainPrice = TextActions.RemoveDollarSign(ProductDetail.ItemPriceText).Replace("Price:\r\n", "").Trim();
            var formattedMainPrice = TextActions.FormatToTwoDecimals(residentialProduct.RetailPriceInternet);

            Assert.Equals(formattedMainPrice, mainPrice, "Main Price is not matching with the database.");
            Assert.False(SortFullPageCertona.IsPriceVerbiageVisible, "Price verbiage, Price and StrikeThrough Price are displayed");
            Assert.False(ProductDetail.IsSavePriceAndVerbiageVisible, "Save Price and Save verbiage is displayed");
            Assert.False(ProductDetail.IsCompareCalloutElementVisible, "Compare Callout is displayed");
            Assert.False(SortFullPageCertona.IsEndDateVerbiageVisible, "End Sale verbiage is displayed");
            Assert.False(SortFullPageCertona.IsPriceVerbiageVisible, "Sale verbiage is displayed");

            var sfpUrl = Browser.PageUrl;
            Browser.ScrollToBottomOfPage(sfpUrl);

            Assert.Equals(formattedMainPrice, mainPrice, "Main Price is not matching with the database.");

            Browser.Navigate(Urls.PlaSortPageBaseUrl + sku);

            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

            Assert.Equals(formattedMainPrice, mainPrice, "Main Price is not matching with the database.");
            Assert.False(SortFullPageCertona.IsPriceVerbiageVisible, "Price verbiage, Price and StrikeThrough Price are displayed");
            Assert.False(ProductDetail.IsSavePriceAndVerbiageVisible, "Save Price and Save verbiage is displayed");
            Assert.False(ProductDetail.IsCompareCalloutElementVisible, "Compare Callout is displayed");
            Assert.False(SortFullPageCertona.IsEndDateVerbiageVisible, "End Sale verbiage is displayed");
            Assert.False(SortFullPageCertona.IsPriceVerbiageVisible, "Sale verbiage is displayed");
        }
    }
    
    public abstract class T7777_T7781_Base : SortTestsBase
    {
        protected T7777_T7781_Base(ITestOutputHelper output) : base(output) { }

        protected virtual void Validate (string config)
        {
            InitializeFramework(config);

            var residentialProduct = ProductActions.GetSkuForResidentialProduct();
            var sku = residentialProduct.ShortSku;

            Assert.DatabaseObject(sku, "ProductActions.GetSkuForResidentialProduct()");

            Browser.Navigate(Urls.ProductFullPageBaseUrl + sku);

            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

            var mainPrice = TextActions.RemoveDollarSign(ProductDetail.ItemPriceText).Trim();
            mainPrice = TextActions.RemoveTextBeforeAndIncludingCharacter(mainPrice,':');

            var formattedMainPrice = TextActions.FormatToTwoDecimals(residentialProduct.RetailPriceInternet);

            if (ProductDetail.IsLoggedInAsKiosk)
            {
                mainPrice = TextActions.RemoveDollarSign(ProductDetail.ItemPriceText).Replace("Price:", "").Replace("\r\n", "");
                formattedMainPrice = TextActions.FormatToTwoDecimals(residentialProduct.RetailPrice);
            }

            Assert.Equals(formattedMainPrice, mainPrice, "Main Price is not matching with the database.");
            Assert.False(SortFullPageCertona.IsPriceVerbiageVisible, "Price verbiage, Price and StrikeThrough Price are displayed");
            Assert.False(ProductDetail.IsSavePriceAndVerbiageVisible, "Save Price and Save verbiage is displayed");
            Assert.False(ProductDetail.IsCompareCalloutElementVisible, "Compare Callout is displayed");
            Assert.False(SortFullPageCertona.IsEndDateVerbiageVisible, "End Sale verbiage is displayed");
            Assert.False(SortFullPageCertona.IsPriceVerbiageVisible, "Sale verbiage is displayed");

            var sfpUrl = Browser.PageUrl;
            Browser.ScrollToBottomOfPage(sfpUrl);

            Assert.Equals(formattedMainPrice, mainPrice, "Main Price is not matching with the database.");
            
            Browser.Navigate(Urls.PlaSortPageBaseUrl + sku);

            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

            Assert.Equals(formattedMainPrice, mainPrice, "Main Price is not matching with the database.");
            Assert.False(SortFullPageCertona.IsPriceVerbiageVisible, "Price verbiage, Price and StrikeThrough Price are displayed");
            Assert.False(ProductDetail.IsSavePriceAndVerbiageVisible, "Save Price and Save verbiage is displayed");
            Assert.False(ProductDetail.IsCompareCalloutElementVisible, "Compare Callout is displayed");
            Assert.False(SortFullPageCertona.IsEndDateVerbiageVisible, "End Sale verbiage is displayed");
            Assert.False(SortFullPageCertona.IsPriceVerbiageVisible, "Sale verbiage is displayed");
        }
    }
}
