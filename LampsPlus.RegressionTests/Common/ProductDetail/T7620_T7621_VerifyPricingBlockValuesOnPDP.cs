using System;
using Xunit;
using Xunit.Abstractions;
using OpenQA.Selenium;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.DesktopTablet.ProductDetail;
using ProductModel = LampsPlus.AutomationFramework.Databases.Entities.ProductModel;

namespace LampsPlus.RegressionTests.Common.ProductDetail
{
    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7620_Windows_VerifyPricingBlockValuesOnPdp : T7620_DesktopBase
    {
        public T7620_Windows_VerifyPricingBlockValuesOnPdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]        
        public void PricingBlockValuesOnPdp(string config) => Validate(config);
    }

    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7620_Windows_VerifyPricingBlockValuesOnPdpInKiosk : T7620_DesktopBase
    {
        public T7620_Windows_VerifyPricingBlockValuesOnPdpInKiosk(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_UNSI)]
        public void PricingBlockValuesOnPdp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7620_Mac_VerifyPricingBlockValuesOnPdp : T7620_DesktopBase
    {
        public T7620_Mac_VerifyPricingBlockValuesOnPdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void PricingBlockValuesOnPdp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7620_iPad_VerifyPricingBlockValuesOnPdp : T7620_DesktopBase
    {
        public T7620_iPad_VerifyPricingBlockValuesOnPdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void PricingBlockValuesOnPdp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7620_TabletEmulator_VerifyPricingBlockValuesOnPdp : T7620_DesktopBase
    {
        public T7620_TabletEmulator_VerifyPricingBlockValuesOnPdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void PricingBlockValuesOnPdp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.ProductDetail)]
    public class T7621_iPhone_VerifyPricingBlockValuesOnPdp : T7621_MobileBase
    {
        public T7621_iPhone_VerifyPricingBlockValuesOnPdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T7621. Rework - ACD-10854")]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void PricingBlockValuesOnPdp(string config) => Validate(config);
    }

    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7621_Emulator_VerifyPricingBlockValuesOnPdp : T7621_MobileBase
    {
        public T7621_Emulator_VerifyPricingBlockValuesOnPdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void PricingBlockValuesOnPdp(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the Pricing Block values on the PDP.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8821
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7620
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8821"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7620")]
    public abstract class T7620_DesktopBase : T7620_T7621_Base
    {
        protected T7620_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected override void GetSalesCallout(string salesCallout)
        {
            Assert.True(salesCallout.CaseInsensitiveContains("SALE"), "Sale call out is not displayed");
        }

        protected override void VerifyStickyFilter(ProductModel salesProduct, string formattedMainPrice, string formattedStruckPrice, string formattedSavingsPrice, string elementSaleEndDate)
        {
            var stickySaleCallout = ProductDetail.ProductCallOut.Text.Trim().ToLower();
            var stickyMainPrice = TextActions.RemoveDollarSign(ProductDetail.StickyPrice.Text);
            var stickyStrikeThroughPrice = TextActions.RegexNoTabsAndNewLines(ProductDetail.StruckThroughPrice.Replace("$", string.Empty).Replace("i", string.Empty).TrimEnd());

            stickyStrikeThroughPrice = TextActions.GetPriceTextOnly(stickyStrikeThroughPrice);

            var saveCallout = ProductDetail.StickySaveCallout.Text;
            var saveAmount = saveCallout.Replace("Save $", string.Empty);
            var stickyEndsCallout = ProductDetail.EndsDate(1).Text;
            var stickyEndsDate = stickyEndsCallout.Replace("Ends ", string.Empty);

            Assert.Equals(stickySaleCallout, "sale", "Sale call out is not displayed");
            Assert.Equals(stickyMainPrice, formattedMainPrice, "Sale price on UI doesn't not match SalePrice in the database.");
            Assert.Equals(stickyStrikeThroughPrice, formattedStruckPrice, "Strike Through on UI doesn't not match RetailPrice in the database.");
            Assert.StringContains(saveCallout, "Save", "Save Callout is not displayed");
            Assert.Equals(saveAmount, formattedSavingsPrice, "Saving price does not match with the Database");
            Assert.StringContains(stickyEndsCallout, "Ends", "Ends callout is not displayed" );
            Assert.Equals(stickyEndsDate, elementSaleEndDate, "The Ends date does not match the SaleEndDate in the database.");
        }
    }


    /// <summary>
    /// Verify the Pricing Block values on the PDP.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8821
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7621
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8821"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7621")]
    public abstract class T7621_MobileBase : T7620_T7621_Base
    {
        protected T7621_MobileBase(ITestOutputHelper output) : base(output) { }

        protected override void GetSalesCallout(string salesCallout)
        {
            Assert.Equals(salesCallout, "Sale", "Sale call out is not displayed");
        }

        protected override void VerifyStickyFilter(ProductModel salesProduct, string formattedMainPrice, string formattedStruckPrice, string formattedSavingsPrice, string elementSaleEndDate)
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(ProductDetail.PdAddToCartStickyId.ToCssIdSelector()));

            var stickySaleCallout = ProductDetail.StickySaleCallout.Text;
            var stickyPrice = ProductDetail.StickyHeaderPrice.Text.Replace("$", string.Empty);

            Assert.StringContains(stickySaleCallout, "SALE", "Sale callout is not displayed");
            Assert.Equals(stickyPrice, formattedMainPrice, "Sale price on UI doesn't not match SalePrice in the database");
        }
    }


    public abstract class T7620_T7621_Base : ProductDetailTestsBase
    {
        protected T7620_T7621_Base(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            InitializeFramework(config);
            var salesProduct = ProductActions.GetSkuWithSaleWithComparableValue();

            Assert.DatabaseObject(salesProduct, "ProductActions.GetSkuWithSaleWithComparableValue()");

            // Navigate to PDP of Saving amount > 5 & IsLpProduct = 1 and check Sale Callout.
            Browser.NavigateToPdp(salesProduct.ShortSku);

            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));
            var salesCallout = ProductDetail.ProductCallOut.Text.Trim();
            GetSalesCallout(salesCallout);

            // Take Main Price, Saving and Strike Through prices from the Database.
            var mainPriceFromDb = TextActions.FormatToTwoDecimals(salesProduct.SalePrice1Internet);
            var mainPriceFromDbSis = TextActions.FormatToTwoDecimals(salesProduct.SalePrice1);
            var savingsPriceFromDb = TextActions.FormatToTwoDecimals(salesProduct.Savings);
            var strikeThroughPriceFromDb = TextActions.FormatToTwoDecimals(salesProduct.RetailPriceInternet);
            var strikeThroughPriceFromDbSis = TextActions.FormatToTwoDecimals(salesProduct.RetailPrice).Trim();

            // Take Ends and Save callout from PDP.
            var endsCallout = ProductDetail.ProductSaleEndDateText;
            var saveCallout = ProductDetail.ProductSalePrice;

            // Take Main Price, Saving and Strike Through prices from the PDP.
            var mainPriceFromPdp = TextActions.RemoveDollarSign(ProductDetail.ItemPriceText).Replace("Sale", string.Empty).Trim();
            mainPriceFromPdp = TextActions.RemoveTextBeforeAndIncludingCharacter(mainPriceFromPdp, ':');

            var savingsPriceFromPdp = ProductDetail.ProductSalePrice.Replace("Save $", string.Empty).Trim();
            var strikeThroughPriceFromPdp = TextActions.RegexNoTabsAndNewLines(ProductDetail.StruckThroughPrice.Replace("$", string.Empty).Replace("i", string.Empty).TrimEnd());

            strikeThroughPriceFromPdp = TextActions.GetPriceTextOnly(strikeThroughPriceFromPdp);

            // Take Sale End Date from Database and PDP
            var saleEndDateFromDb = Convert.ToDateTime(salesProduct.EndSale).ToString("M/d/yy").Replace("-", "/");
            var saleEndDateFromPdp = ProductDetail.ProductSaleEndDateText.Replace("Ends ", string.Empty);

            Assert.StringContains(saveCallout, "Save", "Save call out is not displayed.");
            Assert.Equals(savingsPriceFromDb, savingsPriceFromPdp, "The Sale value does not match with the Savings column from the database query.");

            Assert.StringContains(endsCallout, "Ends", "Ends call out is not displayed.");
            Assert.Equals(saleEndDateFromDb, saleEndDateFromPdp, "The Ends date does not match the SaleEndDate in the database.");

            Assert.False(ProductDetail.IsSaleVerbiageVisible, "Sale verbiage is displayed");

            if (config.Contains("SIS"))
            {
                Assert.Equals(mainPriceFromDbSis, mainPriceFromPdp, "Sale price on UI doesn't not match SalePrice in the database.");
                Assert.Equals(strikeThroughPriceFromDbSis, strikeThroughPriceFromPdp, "The Sale value does not match with the Savings column from the database query.");

                Browser.ScrollToBottomOfWindow();
                VerifyStickyFilter(salesProduct, mainPriceFromDbSis, strikeThroughPriceFromDbSis, savingsPriceFromDb, saleEndDateFromDb);
            }
            else
            {
                Assert.Equals(mainPriceFromDb, mainPriceFromPdp, "Sale price on UI doesn't not match SalePrice in the database.");
                Assert.Equals(strikeThroughPriceFromDb, strikeThroughPriceFromPdp, "The Sale value does not match with the Savings column from the database query.");

                Browser.ScrollToBottomOfWindow();
                VerifyStickyFilter(salesProduct, mainPriceFromDb, strikeThroughPriceFromDb, savingsPriceFromDb, saleEndDateFromDb);
            }
        }
        
        protected abstract void GetSalesCallout(string salesCallout);
       
        protected abstract void VerifyStickyFilter(ProductModel salesProduct, string formattedMainPrice, string formattedStruckPrice, string formattedSavingsPrice, string elementSaleEndDate);
    }
}
