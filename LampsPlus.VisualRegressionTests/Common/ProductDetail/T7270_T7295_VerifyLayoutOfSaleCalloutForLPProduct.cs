using System.Collections.Generic;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using OpenQA.Selenium;
using xRetry;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7270_Windows_VerifyLayoutOfSaleCalloutForLpProduct : T7270_DesktopBase
    {
        public T7270_Windows_VerifyLayoutOfSaleCalloutForLpProduct(ITestOutputHelper output, T7270_T7295_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfSaleCalloutForLpProduct(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7270_Mac_VerifyLayoutOfSaleCalloutForLpProduct : T7270_DesktopBase
    {
        public T7270_Mac_VerifyLayoutOfSaleCalloutForLpProduct(ITestOutputHelper output, T7270_T7295_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfSaleCalloutForLpProduct(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7270_iPad_VerifyLayoutOfSaleCalloutForLpProduct : T7270_DesktopBase
    {
        public T7270_iPad_VerifyLayoutOfSaleCalloutForLpProduct(ITestOutputHelper output, T7270_T7295_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfSaleCalloutForLpProduct(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7270_TabletEmulator_VerifyLayoutOfSaleCalloutForLpProduct : T7270_DesktopBase
    {
        public T7270_TabletEmulator_VerifyLayoutOfSaleCalloutForLpProduct(ITestOutputHelper output, T7270_T7295_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfSaleCalloutForLpProduct(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7295_iPhone_VerifyLayoutOfSaleCalloutForLpProduct : T7295_MobileBase
    {
        public T7295_iPhone_VerifyLayoutOfSaleCalloutForLpProduct(ITestOutputHelper output, T7270_T7295_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth)]
        public void LayoutOfSaleCalloutForLpProduct(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7295_Android_VerifyLayoutOfSaleCalloutForLpProduct : T7295_MobileBase
    {
        public T7295_Android_VerifyLayoutOfSaleCalloutForLpProduct(ITestOutputHelper output, T7270_T7295_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfSaleCalloutForLpProduct(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7295_Emulator_VerifyLayoutOfSaleCalloutForLpProduct : T7295_MobileBase
    {
        public T7295_Emulator_VerifyLayoutOfSaleCalloutForLpProduct(ITestOutputHelper output, T7270_T7295_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfSaleCalloutForLpProduct(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Sale Callouts & Pricing / Comparable Value for an LP Product that is on Sale.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7364
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7270
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7364"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7270")]
    public abstract class T7270_DesktopBase : T7270_T7295_Base
    {
        protected T7270_DesktopBase(ITestOutputHelper output, T7270_T7295_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        protected override void TakeScreenshot()
        {
            ScreenCapturer.CaptureScreenRegionWithIgnoredLayouts(Browser.PageUrl, ProductDetail.TopContentProductDetail, new List<IElement> { ProductDetail.StockCheckWrapper });
        }
    }


    /// <summary>
    /// Verify the layout of sale callout for LP products.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7364
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7295
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7364"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7295")]
    public abstract class T7295_MobileBase : T7270_T7295_Base
    {
        protected T7295_MobileBase(ITestOutputHelper output, T7270_T7295_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        protected override void TakeScreenshot()
        {
            Browser.ScrollToTopOfWindow();
            Browser.Wait.IsVisibleElement(By.ClassName(ProductDetail.ShipsInMessageClass));

            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ProductDetail.StockCheckWrapper, ProductDetail.CertonaDrawerName, ProductDetail.MoreYouMayLikeContainer }, true, true);
        }
    }


    public class T7270_T7295_SharedProductSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7270_T7295_SharedProductSku_Fixture()
        {
            ShortSku = ProductActions.GetLpProductOnSaleWithComparePrice().ShortSku;
        }
    }


    public abstract class T7270_T7295_Base : VisualTestsBase, IClassFixture<T7270_T7295_SharedProductSku_Fixture>
    {
        protected readonly T7270_T7295_SharedProductSku_Fixture Fixture;

        protected T7270_T7295_Base(ITestOutputHelper output, T7270_T7295_SharedProductSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        } 
        
        protected void Validate(string config)
        {
            InitializeVisualTest(config);

            var sku = Fixture.ShortSku;

            Assert.DatabaseObject(sku, "ProductActions.GetLpProductOnSaleWithComparePrice()");

            ProductDetail.NavigateToProductDetailByShortSku(sku);

            TakeScreenshot();
        }

        protected abstract void TakeScreenshot();
    }
}
