using System.Collections.Generic;
using xRetry;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail

{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7273_Windows_VerifyLayoutOfSoldOutCallout : T7273_DesktopBase
    {
        public T7273_Windows_VerifyLayoutOfSoldOutCallout(ITestOutputHelper output, SharedLimitedInventorySku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfSoldOutCalloutPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7273_Mac_VerifyLayoutOfSoldOutCallout : T7273_DesktopBase
    {
        public T7273_Mac_VerifyLayoutOfSoldOutCallout(ITestOutputHelper output, SharedLimitedInventorySku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfSoldOutCalloutPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7273_iPad_VerifyLayoutOfSoldOutCallout : T7273_DesktopBase
    {
        public T7273_iPad_VerifyLayoutOfSoldOutCallout(ITestOutputHelper output, SharedLimitedInventorySku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfSoldOutCalloutPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7273_TabletEmulator_VerifyLayoutOfSoldOutCallout : T7273_DesktopBase
    {
        public T7273_TabletEmulator_VerifyLayoutOfSoldOutCallout(ITestOutputHelper output, SharedLimitedInventorySku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfSoldOutCalloutPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7298_iPhone_VerifyLayoutOfSoldOutCallout : T7298_MobileBase
    {
        public T7298_iPhone_VerifyLayoutOfSoldOutCallout(ITestOutputHelper output, SharedLimitedInventorySku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth)]
        public void LayoutOfSoldOutCalloutPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7298_AndroidPhone_VerifyLayoutOfSoldOutCallout : T7298_MobileBase
    {
        public T7298_AndroidPhone_VerifyLayoutOfSoldOutCallout(ITestOutputHelper output, SharedLimitedInventorySku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfSoldOutCalloutPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7298_Emulator_VerifyLayoutOfSoldOutCallout : T7298_MobileBase
    {
        public T7298_Emulator_VerifyLayoutOfSoldOutCallout(ITestOutputHelper output, SharedLimitedInventorySku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfSoldOutCalloutPage(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Sold Out callout.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7367
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7273
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7367"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7273")]
    public abstract class T7273_DesktopBase : T7273_T7298_Base
    {
        protected T7273_DesktopBase(ITestOutputHelper output, SharedLimitedInventorySku_Fixture fixture) : base(output, fixture) { }
    }


    /// <summary>
    /// Verify the layout of the Sold Out callout
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7367
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7298
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7367"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7298")]
    public abstract class T7298_MobileBase : T7273_T7298_Base
    {
        protected T7298_MobileBase(ITestOutputHelper output, SharedLimitedInventorySku_Fixture fixture) : base(output, fixture) { }

        protected override void Validate(string config)
        {
            InitializeVisualTest(config);

            ProductDetail.NavigateToProductDetailByShortSku(Fixture.ShortSku);
            Browser.Wait.ForDomReady();

            ProductDetail.AddMaxQuantityToCart();
            Browser.Wait.ForPage(Urls.CartOverviewPageUrl);

            ProductDetail.NavigateToProductDetailByShortSku(Fixture.ShortSku);

            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }


    public class SharedLimitedInventorySku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public SharedLimitedInventorySku_Fixture()
        {
            ShortSku = ProductActions.GetProductWithLimitedInventory().Sku;
        }
    }


    public abstract class T7273_T7298_Base : VisualTestsBase, IClassFixture<SharedLimitedInventorySku_Fixture>
    {
        protected readonly SharedLimitedInventorySku_Fixture Fixture;

        protected T7273_T7298_Base(ITestOutputHelper output, SharedLimitedInventorySku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }
       
        protected virtual void Validate(string config)
        {
            InitializeVisualTest(config);

            ProductDetail.NavigateToProductDetailByShortSku(Fixture.ShortSku);
            Browser.Wait.ForDomReady();

            ProductDetail.AddMaxQuantityToCart();
            Browser.Wait.ForPage(Urls.CartOverviewPageUrl);

            ProductDetail.NavigateToProductDetailByShortSku(Fixture.ShortSku);

            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement> {ProductDetail.StockCheckWrapper});
        }
    }
}
