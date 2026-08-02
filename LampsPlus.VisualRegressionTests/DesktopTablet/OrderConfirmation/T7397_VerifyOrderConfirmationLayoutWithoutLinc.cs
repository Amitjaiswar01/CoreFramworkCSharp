using System.Collections.Generic;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;

namespace LampsPlus.VisualRegressionTests.DesktopTablet.OrderConfirmation
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7397_Windows_VerifyOrderConfirmationLayoutWithoutLinc : T7397_DesktopBase
    {
        public T7397_Windows_VerifyOrderConfirmationLayoutWithoutLinc(ITestOutputHelper output, T7397_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void T7397_VerifyOrderConfirmationLayoutWithoutLinc(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7397_Mac_VerifyOrderConfirmationLayoutWithoutLincAndExcitedAboutYourPurchaseSections : T7397_DesktopBase
    {
        public T7397_Mac_VerifyOrderConfirmationLayoutWithoutLincAndExcitedAboutYourPurchaseSections(ITestOutputHelper output, T7397_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void T7397_VerifyOrderConfirmationLayoutWithoutLincAndExcitedAboutYourPurchaseSections(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7397_iPad_VerifyOrderConfirmationLayoutWithoutLincAndExcitedAboutYourPurchaseSections : T7397_DesktopBase
    {
        public T7397_iPad_VerifyOrderConfirmationLayoutWithoutLincAndExcitedAboutYourPurchaseSections(ITestOutputHelper output, T7397_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void T7397_VerifyOrderConfirmationLayoutWithoutLincAndExcitedAboutYourPurchaseSections(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7397_TabletEmulator_VerifyOrderConfirmationLayoutWithoutLincAndExcitedAboutYourPurchaseSections : T7397_DesktopBase
    {
        public T7397_TabletEmulator_VerifyOrderConfirmationLayoutWithoutLincAndExcitedAboutYourPurchaseSections(ITestOutputHelper output, T7397_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void T7397_VerifyOrderConfirmationLayoutWithoutLincAndExcitedAboutYourPurchaseSections(string config) => Validate(Validate, config);
    }

    public class T7397_SharedProductSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }
        public string Email { get; }

        public T7397_SharedProductSku_Fixture()
        {
            ShortSku = ProductActions.GetSkuGreaterThanTwoHundredDollars;
            Email = "lamps_plus_test_automation@mailinator.com";
        }
    }


    /// <summary>
    /// Verify the layout of the Order Confirmation page when there are NO Linc widgets and 'Excited About Your Purchase?' section.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7529
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7397
    /// </summary>
    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7529"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7397")]
    public abstract class T7397_DesktopBase : VisualTestsBase, IClassFixture<T7397_SharedProductSku_Fixture>
    {
        protected readonly T7397_SharedProductSku_Fixture Fixture;

        protected T7397_DesktopBase(ITestOutputHelper output, T7397_SharedProductSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            InitializeVisualTest(config);

            var shortSku = Fixture.ShortSku;

            Assert.DatabaseObject(shortSku, "ProductActions.GetSkuGreaterThanTwoHundredDollars()");

            ShoppingCartWorkflow.AddItemToCartBySku(new ProductModel { Sku = shortSku });

            Browser.Wait.ForClickableElement(CsrBlock.SaleSourceField);
            Browser.Locate.ClickDropdownByValue(CsrBlock.SaleSourceField, "1");
            CartOverview.CheckOutNowButton.Click();
            Browser.Wait.ForPage(Urls.ShippingPageUrl);
            Browser.Wait.ForDomReady();

            var intlAddress = new IntAddress { Email = Fixture.Email };
            CustomerAddressInformation.EnterShippingAddress(intlAddress, isIntAddress:true);
            Shipping.ProceedToPaymentButton.Click();
            Browser.Wait.UntilElementUnloads(Shipping.ProceedToPaymentButton);

            Browser.Wait.ForPage(Urls.PaymentPageUrl);
            Browser.Wait.ForDomReady();
            Payment.PlaceInternationalOrder();
            Browser.Wait.UntilElementUnloads(Payment.PlaceIntlOrderButton);

            Browser.Wait.ForPage(Urls.OrderConfirmationPageUrl);
            Browser.Wait.ForDomReady();

            var ignoreElement = OrderConfirmation.OrderIdHeading;
            var orderConfirmationContainer = OrderConfirmation.OrderSummaryContainer;
            ScreenCapturer.CaptureScreenRegionWithIgnoredLayouts(Browser.PageUrl, orderConfirmationContainer, new List<IElement> { ignoreElement });
        }
    }
}
