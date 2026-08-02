using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.Payment.T7410_T7411_VerifyLayoutOfPaymentPageWithMinMsgForIntAddress
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7410_Windows_VerifyLayoutOfPaymentPageWithMinMsgForIntAddress : T7410_DesktopBase
    {
        public T7410_Windows_VerifyLayoutOfPaymentPageWithMinMsgForIntAddress(ITestOutputHelper output, T7410_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfPaymentPageWithMinMsgForIntAddress(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7410_Mac_VerifyLayoutOfPaymentPageWithMinMsgForIntAddress : T7410_DesktopBase
    {
        public T7410_Mac_VerifyLayoutOfPaymentPageWithMinMsgForIntAddress(ITestOutputHelper output, T7410_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfPaymentPageWithMinMsgForIntAddress(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7410_iPad_VerifyLayoutOfPaymentPageWithMinMsgForIntAddress : T7410_DesktopBase
    {
        public T7410_iPad_VerifyLayoutOfPaymentPageWithMinMsgForIntAddress(ITestOutputHelper output, T7410_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfPaymentPageWithMinMsgForIntAddress(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7410_TabletEmulator_VerifyLayoutOfPaymentPageWithMinMsgForIntAddress : T7410_DesktopBase
    {
        public T7410_TabletEmulator_VerifyLayoutOfPaymentPageWithMinMsgForIntAddress(ITestOutputHelper output, T7410_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfPaymentPageWithMinMsgForIntAddress(string config) => Validate(Validate, config);
    }


    public class T7410_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7410_Fixture()
        {
            ShortSku = ProductActions.GetSkuThatIsLessThanTwoHundredDollars;
        }
    }


    /// <summary>
    /// Verify the layout of the International Order Minimum message on the Payment page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9831
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7410
    /// </summary>
    //[Collection(LpTraits.BatchGroup.Common.Payment)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9831"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7410")]
    public abstract class T7410_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7410_Fixture>
    {
        protected readonly T7410_Fixture Fixture;

        protected T7410_DesktopBase(ITestOutputHelper output, T7410_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            // Arrange: User has identified a SKU and added it to the cart
            InitializeVisualTest(config);
            var shortSku = Fixture.ShortSku;
            ShoppingCartWorkflow.ProceedToPaymentWithSingleProduct(shortSku);
            Assert.True(Payment.IsCurrentPage, "User is not on Payment Page");

            //Act : Enter International Order and Click on Place Order Button
            Payment.SelectSameAsShippingCheckbox();
            Payment.ShowCountryField();
            Address.Country = CountryCodeList.GB;
            CustomerAddressInformation.ChangeBillingCountry(Address);

            //Act : Capture the Screenshot of Page with Minimum Message for International Order 
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, true);
        }
    }
}