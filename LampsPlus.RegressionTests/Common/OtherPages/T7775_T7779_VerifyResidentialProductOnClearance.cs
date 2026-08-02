using System.Text.RegularExpressions;
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
    public class T7775_Windows_VerifyResidentialProductOnClearance : T7775_DesktopBase
    {
        public T7775_Windows_VerifyResidentialProductOnClearance(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyResidentialProductOnClearance(string config) => Validate(config);
    }


    public class T7775_Windows_Kiosk_VerifyResidentialProductOnClearance : T7775_DesktopBase
    {
        public T7775_Windows_Kiosk_VerifyResidentialProductOnClearance(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_UNSI)]
        public void VerifyResidentialProductOnClearance(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7775_Mac_VerifyResidentialProductOnClearance : T7775_DesktopBase
    {
        public T7775_Mac_VerifyResidentialProductOnClearance(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyResidentialProductOnClearance(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7775_iPad_VerifyResidentialProductOnClearance : T7775_DesktopBase
    {
        public T7775_iPad_VerifyResidentialProductOnClearance(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyResidentialProductOnClearance(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7775_TabletEmulator_VerifyResidentialProductOnClearance : T7775_DesktopBase
    {
        public T7775_TabletEmulator_VerifyResidentialProductOnClearance(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyResidentialProductOnClearance(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Sort)]
    public class T7779_iPhone_VerifyResidentialProductOnClearance : T7779_MobileBase
    {
        public T7779_iPhone_VerifyResidentialProductOnClearance(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifyResidentialProductOnClearance(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7779_Emulator_VerifyResidentialProductOnClearance : T7779_MobileBase
    {
        public T7779_Emulator_VerifyResidentialProductOnClearance(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyResidentialProductOnClearance(string config) => Validate(config);
    }


    /// <summary>
    /// Verify Residential Product on Clearance, Not Eligible Member Special, No Company in Session.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9217
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7775
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9217"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7775")]
    public abstract class T7775_DesktopBase : T7775_T7779_Base
    {
        protected T7775_DesktopBase(ITestOutputHelper output) : base(output) { }  
    }


    /// <summary>
    /// Verify Residential Product on Clearance, Not Eligible Member Special, No Company in Session.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9217
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7779
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9217"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7779")]
    public abstract class T7779_MobileBase : T7775_T7779_Base
    {
        protected T7779_MobileBase(ITestOutputHelper output) : base(output) { }
        protected override void Validate(string config)
        {
            InitializeFramework(config);

            var residentialClearanceProduct = ProductActions.GetResidentialClearanceProduct();
            var sku = residentialClearanceProduct.ShortSku;

            Assert.DatabaseObject(sku, "ProductActions.GetResidentialClearanceProduct()");

            Browser.Navigate(Urls.ProductFullPageBaseUrl + sku);

            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

            string clearanceCallout = SortFullPageCertona.DailySaleCallout.Text;
                
            var mainPrice = TextActions.RemoveDollarSign(ProductDetail.ItemPriceText).Replace("Price:\r\n", string.Empty).Trim();
            var formattedMainPrice = TextActions.FormatToTwoDecimals(residentialClearanceProduct.RetailPriceInternet);
                  
            var struckThroughPrice = SortFullPageCertona.MobileStrikeThroughPrice.Text.Replace("$", "").Replace("\r\n", "").Replace("i", "").Replace("Prevous Prce:", "").Trim(); 
            var formattedStruckPrice = TextActions.FormatToTwoDecimals(residentialClearanceProduct.InitialRetailPrice);

            Assert.Equals(clearanceCallout, "CLEARANCE", "Clearance call out is not displayed");
            Assert.Equals(formattedMainPrice, mainPrice.Replace("Price:\r\n", ""), "Clearance price on UI doesn't not match Clearance in the database.");
            Assert.Equals(formattedStruckPrice, struckThroughPrice, "Struckthrough Price does not match with database.");
            Assert.False(ProductDetail.IsSavePriceAndVerbiageVisible, "Save Price and Save verbiage is displayed");
            Assert.False(ProductDetail.IsCompareCalloutElementVisible, "Compare Callout is displayed");
            Assert.False(ProductDetail.IsEndDateVerbiageVisible, "End Sale verbiage is displayed");
            Assert.False(SortFullPageCertona.IsMobileSaleVerbiage, "Sale verbiage is displayed");

            var sfpUrl = Browser.PageUrl;

            Browser.ScrollToBottomOfPage(sfpUrl);         

            Assert.Equals(clearanceCallout, "CLEARANCE", "Clearance call out is not displayed");
            Assert.Equals(formattedMainPrice, mainPrice.Replace("Price:\r\n", ""), "Clearance price on UI doesn't not match Clearance in the database.");
            
            Browser.Navigate(Urls.PlaSortPageBaseUrl + sku);

            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

            Assert.Equals(clearanceCallout, "CLEARANCE", "Clearance call out is not displayed");
            Assert.Equals(formattedMainPrice, mainPrice.Replace("Price:\r\n", ""), "Clearance price on UI doesn't not match Clearance in the database.");
            Assert.Equals(formattedStruckPrice, struckThroughPrice, "Struckthrough Price does not match with database.");
            Assert.False(ProductDetail.IsSavePriceAndVerbiageVisible, "Save Price and Save verbiage is displayed");
            Assert.False(ProductDetail.IsCompareCalloutElementVisible, "Compare Callout is displayed");
            Assert.False(ProductDetail.IsEndDateVerbiageVisible, "End Sale verbiage is displayed");
            Assert.False(SortFullPageCertona.IsMobileSaleVerbiage, "Sale verbiage is displayed");
        }
    }


    public abstract class T7775_T7779_Base : SortTestsBase
    {
        protected T7775_T7779_Base(ITestOutputHelper output) : base(output) { }

        protected virtual void  Validate(string config)
        {
            InitializeFramework(config);

            var residentialClearanceProduct = ProductActions.GetResidentialClearanceProduct();
            var sku = residentialClearanceProduct.ShortSku;

            Assert.DatabaseObject(sku, "ProductActions.GetResidentialClearanceProduct()");

            Browser.Navigate(Urls.ProductFullPageBaseUrl + sku);

            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

            var clearanceCallout = SortFullPageCertona.DailySaleCallout.Text.Trim();
            var callout = clearanceCallout.Remove(9).Trim();
            
            var mainPrice = TextActions.RemoveDollarSign(ProductDetail.ItemPriceText).Trim();
            mainPrice = TextActions.RemoveTextBeforeAndIncludingCharacter(mainPrice, ':');
            var formattedMainPrice = TextActions.FormatToTwoDecimals(residentialClearanceProduct.RetailPriceInternet);

            if (ProductDetail.IsLoggedInAsKiosk)
            {
                mainPrice = TextActions.RemoveDollarSign(ProductDetail.ItemPriceText).Replace("Price:\r\n", string.Empty).Trim();
                formattedMainPrice = TextActions.FormatToTwoDecimals(residentialClearanceProduct.RetailPrice);
            }

            var struckThroughPrice = SortFullPageCertona.DailySaleCallout.Text.Replace("Clearance $" , "");
            var struckThroughCallout = Regex.Match(struckThroughPrice, @"(\d+\.\d+)").ToString();
            var formattedStruckPrice = TextActions.FormatToTwoDecimals(residentialClearanceProduct.InitialRetailPrice);

            Assert.Equals(callout.ToLower(), "clearance", "Clearance call out is not displayed");
            Assert.Equals(formattedMainPrice, mainPrice, "Clearance price on UI doesn't not match Clearance in the database.");
            Assert.Equals(formattedStruckPrice, struckThroughCallout, "Struckthrough Price does not match with database.");
            Assert.False(ProductDetail.IsSavePriceAndVerbiageVisible, "Save Price and Save verbiage is displayed");
            Assert.False(ProductDetail.IsCompareCalloutElementVisible, "Compare Callout is displayed");
            Assert.False(ProductDetail.IsEndDateVerbiageVisible, "End Sale verbiage is displayed");
            Assert.False(ProductDetail.IsSaleVerbiageVisible, "Sale verbiage is displayed");

            var sfpUrl = Browser.PageUrl;

            Browser.ScrollToBottomOfPage(sfpUrl);

            var saveCallout = ProductDetail.StickySaveCallout.Text;
            var saveAmount = saveCallout.Replace("Save $", "");
            var formattedSavingsPrice = TextActions.FormatToTwoDecimals(residentialClearanceProduct.Savings);

            Assert.Equals(callout.ToLower(), "clearance", "Clearance call out is not displayed");
            Assert.Equals(formattedMainPrice, mainPrice, "Clearance price on UI doesn't not match Clearance in the database.");
            Assert.Equals(formattedStruckPrice, struckThroughCallout, "Struckthrough Price does not match with database.");
            Assert.StringContains(saveCallout, "Save", "Save Callout is not displayed");
            Assert.Equals(saveAmount, formattedSavingsPrice, "Saving price does not match with the Database");

            Browser.Navigate(Urls.PlaSortPageBaseUrl + sku);

            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

            Assert.Equals(callout.ToLower(), "clearance", "Clearance call out is not displayed");
            Assert.Equals(formattedMainPrice, mainPrice, "Clearance price on UI doesn't not match Clearance in the database.");
            Assert.Equals(formattedStruckPrice, struckThroughCallout, "Struckthrough Price does not match with database.");
            Assert.False(ProductDetail.IsSavePriceAndVerbiageVisible, "Save Price and Save verbiage is displayed");
            Assert.False(ProductDetail.IsCompareCalloutElementVisible, "Compare Callout is displayed");
            Assert.False(ProductDetail.IsEndDateVerbiageVisible, "End Sale verbiage is displayed");
            Assert.False(ProductDetail.IsSaleVerbiageVisible, "Sale verbiage is displayed");
        }
    }
}
