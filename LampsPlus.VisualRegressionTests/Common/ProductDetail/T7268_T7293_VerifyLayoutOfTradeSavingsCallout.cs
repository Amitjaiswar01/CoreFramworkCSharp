using System.Collections.Generic;
using xRetry;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7268_Windows_VerifyLayoutOfTradeSavingsCallout : T7268_DesktopBase
    {
        public T7268_Windows_VerifyLayoutOfTradeSavingsCallout(ITestOutputHelper output, T7268_T7293_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_PCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_PCSI)]
        public void LayoutOfTradeSavingsCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7268_Mac_VerifyLayoutOfTradeSavingsCallout : T7268_DesktopBase
    {
        public T7268_Mac_VerifyLayoutOfTradeSavingsCallout(ITestOutputHelper output, T7268_T7293_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_PCSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_PCSI)]
        public void LayoutOfTradeSavingsCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7268_iPad_VerifyLayoutOfTradeSavingsCallout : T7268_DesktopBase
    {
        public T7268_iPad_VerifyLayoutOfTradeSavingsCallout(ITestOutputHelper output, T7268_T7293_SharedSku_Fixture fixture) : base(output, fixture) { }
      

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_PCSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_PCSI)]
        public void LayoutOfTradeSavingsCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7268_TabletEmulator_VerifyLayoutOfTradeSavingsCallout : T7268_DesktopBase
    {
        public T7268_TabletEmulator_VerifyLayoutOfTradeSavingsCallout(ITestOutputHelper output, T7268_T7293_SharedSku_Fixture fixture) : base(output, fixture) { }


        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_PCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_PCSI)]
        public void LayoutOfTradeSavingsCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7293_iPhone_VerifyLayoutOfTradeSavingsCallout : T7293_MobileBase
    {
        public T7293_iPhone_VerifyLayoutOfTradeSavingsCallout(ITestOutputHelper output, T7268_T7293_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_PCSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_PCSI)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_PCSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_PCSI_SecondaryViewPortWidth)]
        public void LayoutOfTradeSavingsCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7293_Android_VerifyLayoutOfTradeSavingsCallout : T7293_MobileBase
    {
        public T7293_Android_VerifyLayoutOfTradeSavingsCallout(ITestOutputHelper output, T7268_T7293_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_PCSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_PCSI)]
        public void LayoutOfTradeSavingsCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7293_Emulator_VerifyLayoutOfTradeSavingsCallout : T7293_MobileBase
    {
        public T7293_Emulator_VerifyLayoutOfTradeSavingsCallout(ITestOutputHelper output, T7268_T7293_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_PCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_PCSI)]
        public void LayoutOfTradeSavingsCallout(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Trade Savings callout for Professionals.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7363
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7268
    /// </summary>
    //[Collection(LpTraits.UserRole.Professional)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7363"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7268")]
    public abstract class T7268_DesktopBase : T7268_T7293_Base
    {
        protected T7268_DesktopBase(ITestOutputHelper output, T7268_T7293_SharedSku_Fixture fixture) : base(output, fixture) { }

        protected override void TakeScreenshot()
        {
            ScreenCapturer.CaptureScreenRegionWithIgnoredLayouts(Browser.PageUrl, ProductDetail.TopContentProductDetail, new List<IElement> { ProductDetail.StockCheckWrapper });
        }
    }


    /// <summary>
    /// Verify the layout of the Trade Savings callout for Professionals.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7363
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7293
    /// </summary>
    //[Collection(LpTraits.UserRole.Professional)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7363"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7293")]
    public abstract class T7293_MobileBase : T7268_T7293_Base
    {
        protected T7293_MobileBase(ITestOutputHelper output, T7268_T7293_SharedSku_Fixture fixture) : base(output, fixture) { }

        protected override void TakeScreenshot()
        {
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ProductDetail.StockCheckWrapper, ProductDetail.CertonaDrawerName }, true, true);
        }
    }


    public class T7268_T7293_SharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7268_T7293_SharedSku_Fixture()
        {
            ShortSku = ProductActions.GetTradePriceInfo().ShortSku;
        }
    }


    public abstract class T7268_T7293_Base : VisualTestsBase, IClassFixture<T7268_T7293_SharedSku_Fixture>
    {
        protected readonly T7268_T7293_SharedSku_Fixture Fixture;

        protected T7268_T7293_Base(ITestOutputHelper output, T7268_T7293_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            InitializeVisualTest(config);

            var sku = Fixture.ShortSku;
            Assert.DatabaseObject(sku, "ProductActions.GetTradePriceInfo().ShortSku");

            ProductDetail.NavigateToProductDetailByShortSku(sku);
            Browser.Wait.ForDomReady();

            TakeScreenshot();
        }

        protected abstract void TakeScreenshot();
    }
}
