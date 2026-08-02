using System.Collections.Generic;
using OpenQA.Selenium;
using xRetry;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.VisualRegressionTests.Common.OtherPages
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7630_Windows_VerifyLayoutOfPricingBlockOnSfp : T7630_DesktopBase
    {
        public T7630_Windows_VerifyLayoutOfPricingBlockOnSfp(ITestOutputHelper output, T7630_T7631_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyLayoutOfPricingBlockOnSfp(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7630_Mac_VerifyLayoutOfPricingBlockOnSfp : T7630_DesktopBase
    {
        public T7630_Mac_VerifyLayoutOfPricingBlockOnSfp(ITestOutputHelper output, T7630_T7631_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfPricingBlockOnSfp(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7630_iPad_VerifyLayoutOfPricingBlockOnSfp : T7630_DesktopBase
    {
        public T7630_iPad_VerifyLayoutOfPricingBlockOnSfp(ITestOutputHelper output, T7630_T7631_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfPricingBlockOnSfp(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7630_TabletEmulator_VerifyLayoutOfPricingBlockOnSfp : T7630_DesktopBase
    {
        public T7630_TabletEmulator_VerifyLayoutOfPricingBlockOnSfp(ITestOutputHelper output, T7630_T7631_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyLayoutOfPricingBlockOnSfp(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7631_iPhone_VerifyLayoutOfPricingBlockOnSfp : T7631_MobileBase
    {
        public T7631_iPhone_VerifyLayoutOfPricingBlockOnSfp(ITestOutputHelper output, T7630_T7631_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfPricingBlockOnSfp(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7631_Android_VerifyLayoutOfPricingBlockOnSfp : T7631_MobileBase
    {
        public T7631_Android_VerifyLayoutOfPricingBlockOnSfp(ITestOutputHelper output, T7630_T7631_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void VerifyLayoutOfPricingBlockOnSfp(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7631_Emulator_VerifyLayoutOfPaymentPage : T7631_MobileBase
    {
        public T7631_Emulator_VerifyLayoutOfPaymentPage(ITestOutputHelper output, T7630_T7631_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyLayoutOfPricingBlockOnSfp(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Pricing Block values on the SFP.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8824
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7630
    /// </summary>
    //[Collection(LpTraits.UserRole.Anonymous)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8824"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7630")]
    public abstract class T7630_DesktopBase : T7630_T7631_Base
    {
        protected T7630_DesktopBase(ITestOutputHelper output, T7630_T7631_Fixture fixture) : base(output, fixture) { }

        protected override void CaptureScreenshot()
        {
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ProductDetail.StockCheckWrapper });
        }
    }


    /// <summary>
    /// Verify the layout of the Pricing Block values on the SFP.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8824
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7631
    /// </summary>
    //[Collection(LpTraits.UserRole.Anonymous)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8824"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7631")]
    public abstract class T7631_MobileBase : T7630_T7631_Base
    {
        protected T7631_MobileBase(ITestOutputHelper output, T7630_T7631_Fixture fixture) : base(output, fixture) { }

        protected override void CaptureScreenshot()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }


    public class T7630_T7631_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7630_T7631_Fixture()
        {
            ShortSku = ProductActions.GetSkuForPricingBlock;
        }
    }


    public abstract class T7630_T7631_Base : VisualTestsBase, IClassFixture<T7630_T7631_Fixture>
    {
        protected readonly T7630_T7631_Fixture Fixture;

        protected T7630_T7631_Base(ITestOutputHelper output, T7630_T7631_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            InitializeVisualTest(config);

            var sku = Fixture.ShortSku;

            Assert.DatabaseObject(sku, "ProductActions.GetSkuForPricingBlock");

            Browser.Navigate(Urls.ProductFullPageBaseUrl + sku);

            CaptureScreenshot();
        }

        protected abstract void CaptureScreenshot();
    }
}
