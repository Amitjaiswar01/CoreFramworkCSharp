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
    public class T7272_Window_VerifyLayoutOfClearanceCallout : T7272_DesktopBase
    {
        public T7272_Window_VerifyLayoutOfClearanceCallout(ITestOutputHelper output, T7272_T7297_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfClearanceCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7272_Mac_VerifyLayoutOfClearanceCallout : T7272_DesktopBase
    {
        public T7272_Mac_VerifyLayoutOfClearanceCallout(ITestOutputHelper output, T7272_T7297_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfClearanceCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7272_iPad_VerifyLayoutOfClearanceCallout : T7272_DesktopBase
    {
        public T7272_iPad_VerifyLayoutOfClearanceCallout(ITestOutputHelper output, T7272_T7297_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfClearanceCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7272_TabletEmulator_VerifyLayoutOfClearanceCallout : T7272_DesktopBase
    {
        public T7272_TabletEmulator_VerifyLayoutOfClearanceCallout(ITestOutputHelper output, T7272_T7297_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfClearanceCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7297_iPhone_VerifyLayoutOfClearanceCallout : T7297_MobileBase
    {
        public T7297_iPhone_VerifyLayoutOfClearanceCallout(ITestOutputHelper output, T7272_T7297_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth)]
        public void LayoutOfClearanceCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7297_AndroidPhone_VerifyLayoutOfClearanceCallout : T7297_MobileBase
    {
        public T7297_AndroidPhone_VerifyLayoutOfClearanceCallout(ITestOutputHelper output, T7272_T7297_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfClearanceCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7297_Emulator_VerifyLayoutOfClearanceCallout : T7297_MobileBase
    {
        public T7297_Emulator_VerifyLayoutOfClearanceCallout(ITestOutputHelper output, T7272_T7297_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)] 
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfClearanceCallout(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Clearance Callout and Limited Qty Badge.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7365
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7272
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7365"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7272")]
    public abstract class T7272_DesktopBase : T7272_T7297_Base
    {
        protected T7272_DesktopBase(ITestOutputHelper output, T7272_T7297_SharedSku_Fixture fixture) : base(output, fixture) { }
    }


    /// <summary>
    /// Verify the layout of the Clearance Callout and Limited Qty Badge.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7365
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7297
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7365"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7297")]
    public abstract class T7297_MobileBase : T7272_T7297_Base
    {
        protected T7297_MobileBase(ITestOutputHelper output, T7272_T7297_SharedSku_Fixture fixture) : base(output, fixture) { }

        protected override void Validate(string config)
        {
            InitializeVisualTest(config);
            
            var sku = Fixture.ShortSku;
            Assert.DatabaseObject(sku, "ProductActions.GetProductWithLimitedInventory().Sku");

            ProductDetail.NavigateToProductDetailByShortSku(sku);
            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ProductDetail.StockCheckWrapper, HeaderFooter.Footer }, true, true);

            GlobalLocators.AddToCartButton.Click();
            Browser.Wait.IsVisibleElement(By.ClassName(CartOverview.CheckOutNowClass));

            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { CartOverview.CartMoreYouMayLikeContainer, CartOverview.CartIdContainer }, true, true, CartOverview.CartIdElement,5,5,5,5);
        }
    }


    public class T7272_T7297_SharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7272_T7297_SharedSku_Fixture()
        {
            ShortSku = ProductActions.GetProductWithLimitedInventory().Sku;
        }
    }


    public abstract class T7272_T7297_Base : VisualTestsBase, IClassFixture<T7272_T7297_SharedSku_Fixture>
    {
        protected readonly T7272_T7297_SharedSku_Fixture Fixture;

        protected T7272_T7297_Base(ITestOutputHelper output, T7272_T7297_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            InitializeVisualTest(config);

            var sku = Fixture.ShortSku;
            Assert.DatabaseObject(sku, "ProductActions.GetProductWithLimitedInventory().Sku");

            ProductDetail.NavigateToProductDetailByShortSku(sku);
            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

            ScreenCapturer.CaptureScreenRegionWithIgnoredLayouts(Browser.PageUrl, ProductDetail.TopContentProductDetail, new List<IElement> { ProductDetail.StockCheckWrapper });

            GlobalLocators.AddToCartButton.Click();
            Browser.Wait.IsVisibleElement(By.ClassName(CartOverview.CheckOutNowClass));

            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { CartOverview.CartMoreYouMayLikeContainer, CartOverview.CartIdContainer }, true, false, CartOverview.CartMoreYouMayLikeContainer, 10);
        }
    }
}
