using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;
using Address = LampsPlus.AutomationFramework.Pages.Refactored.Address.Address;

namespace LampsPlus.VisualRegressionTests.Common.OrderConfirmation.T7398_T7399_VerifyLayoutOfModalsOnOrderConfirmation
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7398_Windows_VerifyLayoutOfModalsOnOrderConfirmation : T7398_DesktopBase
    {
        public T7398_Windows_VerifyLayoutOfModalsOnOrderConfirmation(ITestOutputHelper output, T7398_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyLayoutOfModalsOnOrderConfirmation(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7398_Mac_VerifyLayoutOfModalsOnOrderConfirmation : T7398_DesktopBase
    {
        public T7398_Mac_VerifyLayoutOfModalsOnOrderConfirmation(ITestOutputHelper output, T7398_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfModalsOnOrderConfirmation(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7398_iPad_VerifyLayoutOfModalsOnOrderConfirmation : T7398_DesktopBase
    {
        public T7398_iPad_VerifyLayoutOfModalsOnOrderConfirmation(ITestOutputHelper output, T7398_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfModalsOnOrderConfirmation(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7398_TabletEmulator_VerifyLayoutOfModalsOnOrderConfirmation : T7398_DesktopBase
    {
        public T7398_TabletEmulator_VerifyLayoutOfModalsOnOrderConfirmation(ITestOutputHelper output, T7398_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyLayoutOfModalsOnOrderConfirmation(string config) => Validate(Validate, config);
    }


    public class T7398_SharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7398_SharedSku_Fixture()
        {
            ShortSku = ProductActions.GetLincCompatibleProduct;
        }
    }


    /// <summary>
    /// Verify the layout of the Print Preview, Create Account modal, Linc widgets and 'Excited About Your Purchase?' section on the Order Confirmation.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7530
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7398
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7530"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7398")]
    public abstract class T7398_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7398_SharedSku_Fixture>
    {
        protected readonly T7398_SharedSku_Fixture Fixture;

        protected T7398_DesktopBase(ITestOutputHelper output, T7398_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            //Arrange: User has added a Linc Compatible item > $200 to the cart
            InitializeVisualTest(config);

            var sku = Fixture.ShortSku;

            Assert.DatabaseObject(sku, "ProductActions.GetLincCompatibleProduct()");
            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel { Sku = sku });

            Shipping.Navigate();
            var email = string.Format("{0}@mailinator.com", DateTime.Now.Ticks);

            //Act: After adding an item to the cart, proceed through the order flow using a US shipping address and a unique email address.
            CustomerAddressInformation.EnterShippingAddress(new Address { Email = email});
            ShoppingCartWorkflow.ProceedToPayment();

            //Act: Use an international billing address. Place the order.
            Payment.SelectSameAsShippingCheckbox();

            CustomerAddressInformation.EnterBillingAddress(IntAddress, true);
            Payment.SelectInternationalAgreementAndPlaceOrder();
            Assert.True(OrderConfirmation.IsCurrentPage, "Current Page is not an Order Confirmation");

            /*Act: Populate the Password field.
            Click the 'Create Account' button.
            Capture a screenshot of the Success Create Account modal element
             */
            TakeScreenshotCreateAccount();
        }

        protected void TakeScreenshotCreateAccount()
        {
            OrderConfirmation.FillInCreateAccountForm("test123");
            ScreenCapturer.CaptureElementAreaWithIgnoredLayouts(Browser.PageUrl, Modal.GetLpModal(), new List<IElement> { OrderConfirmation.IgnoreCreateAccountEmail() });

            OrderConfirmation.CloseCreateAccountModal();
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { OrderConfirmation.IgnoreOrderConfirmationContainer(), Cart.IgnoreMoreYouMayLike() }, true, true);
        }
    }
}
