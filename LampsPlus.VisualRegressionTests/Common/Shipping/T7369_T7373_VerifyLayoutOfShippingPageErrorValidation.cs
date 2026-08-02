using System.Collections.Generic;
using Automation.Framework;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.VisualRegressionTests.Common.Shipping
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7369_Windows_VerifyLayoutOfShippingPageErrorValidation : T7369_DesktopBase
    {
        public T7369_Windows_VerifyLayoutOfShippingPageErrorValidation(ITestOutputHelper output, SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfErrorValidation(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7369_Mac_VerifyLayoutOfShippingPageErrorValidation : T7369_DesktopBase
    {
        public T7369_Mac_VerifyLayoutOfShippingPageErrorValidation(ITestOutputHelper output, SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfErrorValidation(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7369_iPad_VerifyLayoutOfShippingPageErrorValidation : T7369_DesktopBase
    {
        public T7369_iPad_VerifyLayoutOfShippingPageErrorValidation(ITestOutputHelper output, SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfErrorValidation(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7369_TabletEmulator_VerifyLayoutOfShippingPageErrorValidation : T7369_DesktopBase
    {
        public T7369_TabletEmulator_VerifyLayoutOfShippingPageErrorValidation(ITestOutputHelper output, SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfErrorValidation(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7373_iPhone_VerifyLayoutOfShippingPageErrorValidation : T7373_MobileBase
    {
        public T7373_iPhone_VerifyLayoutOfShippingPageErrorValidation(ITestOutputHelper output, SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void LayoutOfErrorValidation(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7373_AndroidPhone_VerifyLayoutOfShippingPageErrorValidation : T7373_MobileBase
    {
        public T7373_AndroidPhone_VerifyLayoutOfShippingPageErrorValidation(ITestOutputHelper output, SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfErrorValidation(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7373_Emulator_VerifyLayoutOfShippingPageErrorValidation : T7373_MobileBase
    {
        public T7373_Emulator_VerifyLayoutOfShippingPageErrorValidation(ITestOutputHelper output, SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfErrorValidation(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Error Validation on the Shipping page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7510
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7369
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7510"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7369")]
    public abstract class T7369_DesktopBase : T7369_T7373_Base
    {
        protected T7369_DesktopBase(ITestOutputHelper output, SharedSku_Fixture fixture) : base(output, fixture) { }

        protected override void CaptureShippingPage()
        {
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture);
        }
    }


    /// <summary>
    /// Verify the layout of the Error Validation on the Shipping page.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7510
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7373
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7510"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7373")]
    public abstract class T7373_MobileBase : T7369_T7373_Base
    {
        protected T7373_MobileBase(ITestOutputHelper output, SharedSku_Fixture fixture) : base(output, fixture) { }

        protected override void CaptureShippingPage()
        {
            Browser.ScrollToTopOfWindow();

            // Ignore both Cart Id and Edit Cart to avoid pixel difference in spacing between them
            var cartInfo = Shipping.ShippingPageCartInfo;

            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { cartInfo });
        }
    }


    public class SharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public SharedSku_Fixture()
        {
            ShortSku = ProductActions.GetAnySkuWithProductDetailPage;
        }
    }


    public abstract class T7369_T7373_Base : VisualTestsBase, IClassFixture<SharedSku_Fixture>
    {
        protected readonly SharedSku_Fixture Fixture;

        protected T7369_T7373_Base(ITestOutputHelper output, SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            InitializeVisualTest(config);

            var sku = Fixture.ShortSku;

            Assert.DatabaseObject(sku, "ProductActions.GetAnySkuWithProductDetailPage()");

            ShoppingCartWorkflow.AddItemToCartBySku(new ProductModel { Sku = sku });

            CartOverview.CheckOutNowButton.Click();
            Browser.Wait.ForPage(Urls.ShippingPageUrl);

            Browser.Wait.IsVisibleElement(By.CssSelector(Shipping.ProceedPaymentId.ToCssIdSelector()));
            Shipping.ProceedToPaymentButton.Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(Shipping.SingleShippingFirstNameErrorId.ToCssIdSelector()), 30);

            CaptureShippingPage();
        }

        protected abstract void CaptureShippingPage();
    }
}
