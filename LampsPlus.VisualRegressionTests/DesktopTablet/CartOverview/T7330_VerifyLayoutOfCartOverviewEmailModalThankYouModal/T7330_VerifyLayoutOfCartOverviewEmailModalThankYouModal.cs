using System.Collections.Generic;
using Automation.Framework;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.DesktopTablet.CartOverview.T7330_VerifyLayoutOfCartOverviewEmailModalThankYouModal
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7330_Windows_VerifyLayoutOfCartOverviewEmailModalThankYouModal : T7330_DesktopBase
    {
        public T7330_Windows_VerifyLayoutOfCartOverviewEmailModalThankYouModal(ITestOutputHelper output, T7330_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T7330. Rework - ACD-10789")]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void LayoutOfCartOverviewEmailModalThankYouModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7330_Mac_VerifyLayoutOfCartOverviewEmailModalThankYouModal : T7330_DesktopBase
    {
        public T7330_Mac_VerifyLayoutOfCartOverviewEmailModalThankYouModal(ITestOutputHelper output, T7330_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T7330. Rework - ACD-10789")]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void LayoutOfCartOverviewEmailModalThankYouModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7330_iPad_VerifyLayoutOfCartOverviewEmailModalThankYouModal : T7330_DesktopBase
    {
        public T7330_iPad_VerifyLayoutOfCartOverviewEmailModalThankYouModal(ITestOutputHelper output, T7330_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void LayoutOfCartOverviewEmailModalThankYouModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7330_TabletEmulator_VerifyLayoutOfCartOverviewEmailModalThankYouModal : T7330_DesktopBase
    {
        public T7330_TabletEmulator_VerifyLayoutOfCartOverviewEmailModalThankYouModal(ITestOutputHelper output, T7330_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void LayoutOfCartOverviewEmailModalThankYouModal(string config) => Validate(Validate, config);
    }


    public class T7330_SharedProductSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7330_SharedProductSku_Fixture()
        {
            ShortSku = ProductActions.GetSkuBetweenTenAndTwentyDollars;
        }
    }


    /// <summary>
    /// Verify the layout of the Cart Overview page, Email Cart modal and the Thank You modal for ESI.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9787
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7330
    /// </summary>
    [Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9787"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7330")]

    public abstract class T7330_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7330_SharedProductSku_Fixture>
    {
        protected readonly T7330_SharedProductSku_Fixture Fixture;

        protected T7330_DesktopBase(ITestOutputHelper output, T7330_SharedProductSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            //Arrange: Clear Cart 
            InitializeVisualTest(config);

            //Act: Navigate to Cart Page
            Cart.Navigate();
            Assert.True(Cart.IsCartEmpty(), "User is not on an empty cart page.");

            //Act: Take Screeshot of Cart Page
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture);

            //Act: Add Item to Cart
            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel(Fixture.ShortSku));
            Assert.True(Cart.IsCurrentPage, "Current page is not Cart page");

            //Act: Take Screenshot of Cart Page 
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl,new List<IElement>{Cart.IgnoreCartId()}, floating: Cart.IgnoreCartId(), maxLeftOffset:10, maxRightOffset:10);

            //Act: Click Email link and open Email modal
            Cart.OpenAndFocusEmailModal();

            //Act: Take Screenshot of Cart Email Modal
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, Cart.GetCartEmailModal());

            //Act: Re-open modal (required to avoid StaleElement exception for Applitools' modal capture).
            ReopenEmailModal();

            //Act: Enter Email Recipients and Send Email
            Cart.InputEmailRecipientsInForm(new[] { "testingLP1@mailinator.com", "testingLP2@mailinator.com", "testingLP3@mailinator.com" });
            Cart.SendCartEmail();

            //Act: Take Screeshot of Email Thank You Modal
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, Cart.GetCartEmailModal());
        }

        private void ReopenEmailModal()
        {
            Browser.SwitchToDefaultContent();
            Modal.CloseLpModal();
            Cart.OpenAndFocusEmailModal();
        }
    }
}
