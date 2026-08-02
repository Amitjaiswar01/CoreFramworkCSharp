using System.Collections.Generic;
using OpenQA.Selenium;
using xRetry;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7271_Windows_VerifyLayoutOfSaleCalloutForNonLpProduct : T7271_DesktopBase
    {
        public T7271_Windows_VerifyLayoutOfSaleCalloutForNonLpProduct(ITestOutputHelper output, T7271_T7296_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfSaleCalloutForNonLpProduct(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7271_Mac_VerifyLayoutOfSaleCalloutForNonLpProduct : T7271_DesktopBase
    {
        public T7271_Mac_VerifyLayoutOfSaleCalloutForNonLpProduct(ITestOutputHelper output, T7271_T7296_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfSaleCalloutForNonLpProduct(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7271_iPad_VerifyLayoutOfSaleCalloutForNonLpProduct : T7271_DesktopBase
    {
        public T7271_iPad_VerifyLayoutOfSaleCalloutForNonLpProduct(ITestOutputHelper output, T7271_T7296_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfSaleCalloutForNonLpProduct(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7271_TabletEmulator_VerifyLayoutOfSaleCalloutForNonLpProduct : T7271_DesktopBase
    {
        public T7271_TabletEmulator_VerifyLayoutOfSaleCalloutForNonLpProduct(ITestOutputHelper output, T7271_T7296_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfSaleCalloutForNonLpProduct(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7296_iPhone_VerifyLayoutOfSaleCalloutForNonLpProduct : T7296_MobileBase
    {
        public T7296_iPhone_VerifyLayoutOfSaleCalloutForNonLpProduct(ITestOutputHelper output, T7271_T7296_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth)]
        public void LayoutOfSaleCalloutForNonLpProduct(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7296_Android_VerifyLayoutOfSaleCalloutForNonLpProduct : T7296_MobileBase
    {
        public T7296_Android_VerifyLayoutOfSaleCalloutForNonLpProduct(ITestOutputHelper output, T7271_T7296_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfSaleCalloutForNonLpProduct(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7296_Emulator_VerifyLayoutOfSaleCalloutForNonLpProduct : T7296_MobileBase
    {
        public T7296_Emulator_VerifyLayoutOfSaleCalloutForNonLpProduct(ITestOutputHelper output, T7271_T7296_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfSaleCalloutForNonLpProduct(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Sale Callouts & Pricing Value for a Non-LP Product that is on Sale.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7366
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7271
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7366"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7271")]
    public abstract class T7271_DesktopBase : T7271_T7296_Base
    {
        protected T7271_DesktopBase(ITestOutputHelper output, T7271_T7296_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        protected override void TakeScreenshot()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(Home.PlaAddToCartId.ToCssIdSelector()), 90);
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ProductDetail.PdProdInfoColElement, GlobalLocators.CustomerPhoto }, true, true, ProductDetail.StockCheckWrapper, 10, 15, 10, 10);
        }
    }


    /// <summary>
    /// erify the layout of the Sale Callouts & Pricing Value for a Non-LP Product that is on Sale.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7366
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7296
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7366"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7296")]
    public abstract class T7296_MobileBase : T7271_T7296_Base
    {
        protected T7296_MobileBase(ITestOutputHelper output, T7271_T7296_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        protected override void TakeScreenshot()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(ProductDetail.ShipsInMessageClass));

            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ProductDetail.StockCheckWrapper, ProductDetail.CertonaDrawerName, ProductDetail.MoreYouMayLikeContainer }, true, true, ProductDetail.StockCheckWrapper, 0, 5);
        }
    }


    public class T7271_T7296_SharedProductSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7271_T7296_SharedProductSku_Fixture()
        {
            ShortSku = ProductActions.GetLpProductOnSaleWithComparePrice().ShortSku;
        }
    }


    public abstract class T7271_T7296_Base : VisualTestsBase, IClassFixture<T7271_T7296_SharedProductSku_Fixture>
    {
        protected readonly T7271_T7296_SharedProductSku_Fixture Fixture;

        protected T7271_T7296_Base(ITestOutputHelper output, T7271_T7296_SharedProductSku_Fixture fixture) : base(output, fixture)
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
