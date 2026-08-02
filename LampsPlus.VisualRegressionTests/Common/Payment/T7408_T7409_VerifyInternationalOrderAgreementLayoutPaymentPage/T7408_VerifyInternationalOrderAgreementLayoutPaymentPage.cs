using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.Payment.T7408_T7409_VerifyInternationalOrderAgreementLayoutPaymentPage
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7408_Windows_VerifyInternationalOrderAgreementLayoutPaymentPage : DesktopBase
    {
        public T7408_Windows_VerifyInternationalOrderAgreementLayoutPaymentPage(ITestOutputHelper output, T7408_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void VerifyIntOrderAgreementLayout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7408_Mac_VerifyInternationalOrderAgreementLayoutPaymentPage : DesktopBase
    {
        public T7408_Mac_VerifyInternationalOrderAgreementLayoutPaymentPage(ITestOutputHelper output, T7408_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI)]
        public void VerifyIntOrderAgreementLayout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7408_iPad_VerifyInternationalOrderAgreementLayoutPaymentPage : DesktopBase
    {
        public T7408_iPad_VerifyInternationalOrderAgreementLayoutPaymentPage(ITestOutputHelper output, T7408_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI)]
        public void VerifyIntOrderAgreementLayout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7408_TabletEmulator_VerifyInternationalOrderAgreementLayoutPaymentPage : DesktopBase
    {
        public T7408_TabletEmulator_VerifyInternationalOrderAgreementLayoutPaymentPage(ITestOutputHelper output, T7408_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI)]
        public void VerifyIntOrderAgreementLayout(string config) => Validate(Validate, config);
    }


    public class T7408_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7408_Fixture()
        {
            ShortSku = ProductActions.GetSkuGreaterThanTwoHundredDollars; 
        }
    }


    /// <summary>
    /// Verify the layout of the International Orders Agreement on the Payment page
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9830
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7408
    /// </summary>
    //[Collection(LpTraits.UserRole.Customer)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9830"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7408")]
    public abstract class DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7408_Fixture>
    {
        protected readonly T7408_Fixture Fixture;

        protected DesktopBase(ITestOutputHelper output, T7408_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            /* Arrange
             User has no saved addresses            
             User has identified a SKU and added it to the cart
            */
            InitializeVisualTest(config);

            var shortSku = Fixture.ShortSku;
            Assert.DatabaseObject(shortSku, "ProductActions.GetSkuGreaterThanTwoHundredDollars()");
            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel{ Sku = shortSku});

            // Act : From Cart Page, user has proceeded to Shipping Page
            Cart.CheckOut();
            Assert.True(Shipping.IsCurrentPage, "User is Not on Shipping Page");

            // Act : User enters International Shipping Address and proceeds to Payment Page
            CustomerAddressInformation.EnterShippingAddress(IntAddress, isIntAddress: true);
            Shipping.ProceedToPayment();
            Assert.True(Payment.IsInternationalCheckboxDisplayed, "International Order Agreement Checkbox is not displayed");

            // Act : User takes Screenshot of the Payment Page 
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, true);
        }
    }
}