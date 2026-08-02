using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Enums;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.DesktopTablet.ProductDetail;
using xRetry;

namespace LampsPlus.RegressionTests.Common.ProductDetail
{
    public class T256_Windows_VerifyShipInVerbiageIsCorrect : T256_DesktopBase
    {
        public T256_Windows_VerifyShipInVerbiageIsCorrect(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyShipInVerbiageIsCorrect(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T256_Mac_VerifyShipInVerbiageIsCorrect : T256_DesktopBase
    {
        public T256_Mac_VerifyShipInVerbiageIsCorrect(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyShipInVerbiageIsCorrect(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T256_iPad_VerifyShipInVerbiageIsCorrect : T256_DesktopBase
    {
        public T256_iPad_VerifyShipInVerbiageIsCorrect(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyShipInVerbiageIsCorrect(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T256_TabletEmulator_VerifyShipInVerbiageIsCorrect : T256_DesktopBase
    {
        public T256_TabletEmulator_VerifyShipInVerbiageIsCorrect(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyShipInVerbiageIsCorrect(string config) => Validate(config);
    }


    public class T1600_iPhone_VerifyShipInVerbiageIsCorrect : T1600_MobileBase
    {
        public T1600_iPhone_VerifyShipInVerbiageIsCorrect(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifyShipInVerbiageIsCorrect(string config) => Validate(config);
    }


    public class T1600_Emulator_VerifyShipInVerbiageIsCorrect : T1600_MobileBase
    {
        public T1600_Emulator_VerifyShipInVerbiageIsCorrect(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyShipInVerbiageIsCorrect(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the 'Ships in' verbiage on the PDP is correct.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5539
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T256
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5539"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T256")]
    public abstract class T256_DesktopBase : T256_T1600_Base
    {
        protected T256_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected override void VerifyVerbiage(string shipsInVerbiage)
        {
            Assert.True(ProductDetail.InStockCaps.CaseInsensitiveContains(ProductDetail.InStockElement.Text.Trim()), "The 'In Stock - Ships in…' text does not match  on the PDP");
            Assert.StringContains(ProductDetail.ProductInStockTextLink.Text, ProductDetail.ShipsIn, "Product stock text does not display ship in.");
            Assert.Equals(shipsInVerbiage, ProductDetail.ProductInStockTextLink.Text, "The 'In Stock - Ships in…' text does not match the FirstShipDays and LastShipDays from the database.");
        }
    }


    /// <summary>
    /// Verify that the 'Ships in' verbiage on the PDP is correct.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7777
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1600
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7777"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T1600")]
    public abstract class T1600_MobileBase : T256_T1600_Base
    {
        protected T1600_MobileBase(ITestOutputHelper output) : base(output) { }

        protected override void VerifyVerbiage(string shipsInVerbiage)
        {
            Assert.StringContains(SortPla.ShipsTodayPdpElement.Text, ProductDetail.InStockNonCaps, "The 'In Stock - Ships in…' text does not match  on the PDP");
            Assert.StringContains(SortPla.ShipsTodayPdpElement.Text, ProductDetail.ShipsIn, "Product stock text does not display ship in.");
            Assert.StringContains(SortPla.ShipsTodayPdpElement.Text, shipsInVerbiage, "The 'In Stock - Ships in…' text does not match the FirstShipDays and LastShipDays from the database.");
        }
    }


    public abstract class T256_T1600_Base : ProductDetailTestsBase
    {
        protected T256_T1600_Base(ITestOutputHelper output) : base(output) { }
       
        protected void Validate(string config)
        {
            InitializeFramework(config);

            var singleShortSku = ProductActions.GetSingleSku();
            var shipsInVerbiage = ProductActions.GetShipsInVerbiage(singleShortSku.FirstShipDays, SubLocationCode.Lp);
            Assert.DatabaseObject(singleShortSku, "ProductActions.GetSingleSku()");

            ProductDetail.NavigateToProductDetailByShortSku(singleShortSku.ShortSku);
            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));
            Browser.TakeScreenshot("PDP - In Stock Text is Displayed Below the Add to Cart Button");

            VerifyVerbiage(shipsInVerbiage);
        }

        protected abstract void VerifyVerbiage(string shipsInVerbiage);
    }
}
