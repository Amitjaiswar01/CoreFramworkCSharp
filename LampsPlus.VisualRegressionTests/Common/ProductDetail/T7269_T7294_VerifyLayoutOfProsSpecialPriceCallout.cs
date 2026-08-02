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
    public class T7269_Windows_VerifyLayoutOfProsSpecialPriceCallout : T7269_DesktopBase
    {
        public T7269_Windows_VerifyLayoutOfProsSpecialPriceCallout(ITestOutputHelper output, SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_PCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_PCSI)]
        public void LayoutOfProsSpecialPriceCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7269_Mac_VerifyLayoutOfProsSpecialPriceCallout : T7269_DesktopBase
    {
        public T7269_Mac_VerifyLayoutOfProsSpecialPriceCallout(ITestOutputHelper output, SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_PCSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_PCSI)]
        public void LayoutOfProsSpecialPriceCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7269_iPad_VerifyLayoutOfProsSpecialPriceCallout : T7269_DesktopBase
    {
        public T7269_iPad_VerifyLayoutOfProsSpecialPriceCallout(ITestOutputHelper output, SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_PCSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_PCSI)]
        public void LayoutOfProsSpecialPriceCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7269_TabletEmulator_VerifyLayoutOfProsSpecialPriceCallout : T7269_DesktopBase
    {
        public T7269_TabletEmulator_VerifyLayoutOfProsSpecialPriceCallout(ITestOutputHelper output, SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_PCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_PCSI)]
        public void LayoutOfProsSpecialPriceCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7294_iPhone_VerifyLayoutOfProsSpecialPriceCallout : T7294_MobileBase
    {
        public T7294_iPhone_VerifyLayoutOfProsSpecialPriceCallout(ITestOutputHelper output, SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_PCSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_PCSI)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_PCSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_PCSI_SecondaryViewPortWidth)]
        public void LayoutOfProsSpecialPriceCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7294_Android_VerifyLayoutOfProsSpecialPriceCallout : T7294_MobileBase
    {
        public T7294_Android_VerifyLayoutOfProsSpecialPriceCallout(ITestOutputHelper output, SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_PCSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_PCSI)]
        public void LayoutOfProsSpecialPriceCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7294_Emulator_VerifyLayoutOfProsSpecialPriceCallout : T7294_MobileBase
    {
        public T7294_Emulator_VerifyLayoutOfProsSpecialPriceCallout(ITestOutputHelper output, SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_PCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_PCSI)]
        public void LayoutOfProsSpecialPriceCallout(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Pros Special Price callout for Professionals.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7216
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7269
    /// </summary>
    //[Collection(LpTraits.UserRole.Professional)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7216"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7269")]
    public abstract class T7269_DesktopBase : T7269_T7294_Base
    {
        protected T7269_DesktopBase(ITestOutputHelper output, SharedSku_Fixture fixture) : base(output, fixture) { }

        protected override void TakeScreenshot()
        {
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ProductDetail.StockCheck }, true);
        }
    }


    /// <summary>
    /// Verify the layout of the Pros Special Price callout for Professionals.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7216
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7294
    /// </summary>
    //[Collection(LpTraits.UserRole.Professional)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7216"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7294")]
    public abstract class T7294_MobileBase : T7269_T7294_Base
    {
        protected T7294_MobileBase(ITestOutputHelper output, SharedSku_Fixture fixture) : base(output, fixture) { }

        protected override void TakeScreenshot()
        {
            Browser.ScrollToBottomOfPage(Browser.PageUrl);
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ProductDetail.StockCheckWrapper, ProductDetail.MoreYouMayLikeContainer }, true, true);
        }
    }


    public class SharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public SharedSku_Fixture()
        {
            ShortSku = ProductActions.GetProMemberSpecialPriceDiscountCallOutShortSku;
        }
    }


    public abstract class T7269_T7294_Base : VisualTestsBase, IClassFixture<SharedSku_Fixture>
    {
        protected readonly SharedSku_Fixture Fixture;

        protected T7269_T7294_Base(ITestOutputHelper output, SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        } 
        
        protected void Validate(string config)
        {
            InitializeVisualTest(config);

            var sku = Fixture.ShortSku;

            Assert.DatabaseObject(sku, "ProductActions.GetProMemberSpecialPriceDiscountCallOutShortSku()");

            ProductDetail.NavigateToProductDetailByShortSku(sku);

            TakeScreenshot();
        }

        protected abstract void TakeScreenshot();
    }
}
