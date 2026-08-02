using System;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.Common.Sort;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.DesktopTablet.OtherPages
{
    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7778_Windows_VerifyPricingBlockValuesOnSfpAndPla : T7778_DesktopBase
    {
        public T7778_Windows_VerifyPricingBlockValuesOnSfpAndPla(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void PricingBlockValuesOnPlaAndSfp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7778_Mac_VerifyPricingBlockValuesOnSfpAndPla : T7778_DesktopBase
    {
        public T7778_Mac_VerifyPricingBlockValuesOnSfpAndPla(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T778. Rework - CI-3212")]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void PricingBlockValuesOnPlaAndSfp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7778_iPad_VerifyPricingBlockValuesOnSfpAndPla : T7778_DesktopBase
    {
        public T7778_iPad_VerifyPricingBlockValuesOnSfpAndPla(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void PricingBlockValuesOnPlaAndSfp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7778_TabletEmulator_VerifyPricingBlockValuesOnSfpAndPla : T7778_DesktopBase
    {
        public T7778_TabletEmulator_VerifyPricingBlockValuesOnSfpAndPla(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void PricingBlockValuesOnPlaAndSfp(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the functionality of Pricing Block Values on PLA/SFP Section
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9220
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T778
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9220"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7778")]
    public abstract class T7778_DesktopBase : T7778_Base
    {
        protected T7778_DesktopBase(ITestOutputHelper output) : base(output) { }
    }


    public abstract class T7778_Base : SortTestsBase
    {
        protected T7778_Base(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config) 
        {
            InitializeFramework(config);

            var saleProducts = ProductActions.GetResidentialSaleProduct;
            var sku = saleProducts.ShortSku;

            Assert.DatabaseObject(sku, "ProductActions.GetResidentialSaleProduct()");

            //Navigate to SFP Page
            Browser.Navigate(Urls.ProductFullPageBaseUrl + sku);

            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

            // Verify the Sale callout
            Browser.Wait.ForDisplayedElement(SortFullPageCertona.DailySaleCallout);
            var saleVerbiage = SortFullPageCertona.DailySaleCallout.Text.Substring(0, 4);
            Assert.Equals(saleVerbiage, "SALE", "Sale call out is not displayed");

            var mainPrice = TextActions.RemoveDollarSign(ProductDetail.ItemPriceText).Trim().Replace("Price:", string.Empty).Replace("\r\n", "");
            var formattedMainPrice = TextActions.FormatToTwoDecimals(saleProducts.SalePrice);

            var struckThroughPrice = SortFullPageCertona.DailySaleCallout.Text.Replace("SALE\r\n$", "").Replace("\r\n", "").Replace("i", "").Replace("SALEPREVIOUS PRICE:$", "");
            var formattedStruckPrice = TextActions.FormatToTwoDecimals(saleProducts.RetailPrice);
            
            Browser.Wait.ForDisplayedElement(ProductDetail.EndVerbiagePlaAndSfp);

            var endsCallout = ProductDetail.EndVerbiagePlaAndSfp.Text;
            var saleEndDate = Convert.ToDateTime(saleProducts.EndSale).ToString("M-d-yy").Replace("-", "/");
            var elementSaleEndDate = endsCallout.Replace("Ends ", string.Empty);

            //Verify the mainPrice, struckprice, end callout are visible
            Assert.Equals(formattedMainPrice, mainPrice, "Sale price on UI doesn't not match SalePrice1Internet or SalePrice1 in the database.");
            Assert.Equals(formattedStruckPrice, struckThroughPrice, "The Sale value does not match with the RetailPriceInternet or RetailPrice column from the database query.");
            Assert.StringContains(endsCallout, "Ends", "Ends call out is not displayed.");
            Assert.Equals(saleEndDate, elementSaleEndDate, "The Ends date does not match the SaleEndDate in the database.");

            //Verify the Comparable, Sale and Save callout should not display
            Assert.False(ProductDetail.IsSavePriceAndVerbiageVisible, "Save Price and Save verbiage is displayed");
            Assert.False(ProductDetail.IsCheckCompareCallOut, "Compare Value should not display on site");
            Assert.False(ProductDetail.IsSaleVerbiageVisible, "Sale verbiage and Sale value should not display");

            //Scroll the page to the bottom of the window 
            Browser.ScrollToBottomOfPage(Browser.PageUrl);

            //Verify Sticky Filter on Sfp 
            Browser.Wait.ForDisplayedElement(ProductDetail.StickyContainerSfp);

            var stickyElements = ProductDetail.StickyContainerSfp.Text;
            var stickyEndsCallout = stickyElements.Split(' ')[3].Replace("\r\nADD", string.Empty).Trim();
            var stickyCallout = ProductDetail.StickyContainerSfp.Text.Substring(0, 4).TrimEnd();

            var mainPriceStruckPrice = stickyElements.Split(' ')[1].Replace("\r\n", " ");
            var endVerbiage = stickyElements.Split(' ')[4];
 
            var stickyMainPrice = TextActions.RemoveDollarSign(mainPriceStruckPrice.Split(' ')[0]);
            var stickyStruckThroughPrice = TextActions.RemoveDollarSign(mainPriceStruckPrice.Split(' ')[1]).Trim();

            var stickySaveVerbiage = ProductDetail.StickySaveCallout.Text;
            var stickySaveCalloutPrice = TextActions.RemoveDollarSign(ProductDetail.StickySaveCallout.Text).Replace("Save", string.Empty).Trim();
            var formattedSavingsPrice = TextActions.FormatToTwoDecimals(saleProducts.Savings);

            //Verify Sale Verbiage, Main price, StruckPrice, Save & End section are visible.
            Assert.StringContains(stickyCallout, "SALE", "Sale call out is not displayed.");
            Assert.Equals(formattedMainPrice, stickyMainPrice, "Sale price on UI doesn't not match SalePrice1Internet or SalePrice1 in the database.");
            Assert.Equals(formattedStruckPrice, stickyStruckThroughPrice, "The Sale value does not match with the RetailPriceInternet or RetailPrice column from the database query.");
            Assert.StringContains(stickySaveVerbiage, "Save", "Save call out is not displayed.");
            Assert.Equals(formattedSavingsPrice, stickySaveCalloutPrice, "The Save value does not match with the Savings column from the database query.");
            Assert.StringContains(endVerbiage, "Ends", "Ends Verbiage is not visible");
            Assert.Equals(saleEndDate, stickyEndsCallout, "The Ends date does not match the SaleEndDate in the database."); 

            //Navigate to PLA Page
            Browser.Navigate(Urls.PlaSortPageBaseUrl + sku);

            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

            var saleVerbiagePla = SortFullPageCertona.DailySaleCallout.Text.Substring(0, 4);
            Assert.Equals(saleVerbiagePla, "SALE", "Sale call out is not displayed");

            var mainPricePla = TextActions.RemoveDollarSign(ProductDetail.ItemPriceText).Replace("Price:", string.Empty).Replace("\r\n", "");
            var struckThroughPricePla = SortFullPageCertona.DailySaleCallout.Text.Replace("SALE\r\n$", "").Replace("\r\n", "").Replace("i", "").Replace("SALEPREVIOUS PRICE:$", "");

            var endsCalloutPla = ProductDetail.EndVerbiagePlaAndSfp.Text;
            var saleEndDatePla = Convert.ToDateTime(saleProducts.EndSale).ToString("M-d-yy").Replace("-","/");
            var elementSaleEndDatePla = endsCallout.Replace("Ends ", string.Empty);

            //Verify the mainPrice, struckprice, end callout are visible
            Assert.Equals(formattedMainPrice, mainPricePla, "Sale price on UI doesn't not match SalePrice1Internet or SalePrice1 in the database.");
            Assert.Equals(formattedStruckPrice, struckThroughPricePla, "The Sale value does not match with the RetailPriceInternet or RetailPrice column from the database query.");
            Assert.StringContains(endsCalloutPla, "Ends", "Ends call out is not displayed.");
            Assert.Equals(saleEndDatePla, elementSaleEndDatePla, "The Ends date does not match the SaleEndDate in the database.");

            //Verify the Comparable, Sale and Save callout should not display
            Assert.False(ProductDetail.IsSavePriceAndVerbiageVisible, "Save Price and Save verbiage is displayed");
            Assert.False(ProductDetail.IsCheckCompareCallOut, "Compare Value should not display on site");
            Assert.False(ProductDetail.IsSaleVerbiageVisible, "Sale verbiage and Sale value should not display");
        }
    }
}
