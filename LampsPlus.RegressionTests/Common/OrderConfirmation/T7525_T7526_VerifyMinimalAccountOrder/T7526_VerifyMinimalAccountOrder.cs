using Xunit;
using Xunit.Abstractions;
using xRetry;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.OrderConfirmation.T7525_T7526_VerifyMinimalAccountOrder
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    public class T7526_iPhone_VerifyMinimalAccountOrder : T7526_MobileBase
    {
        public T7526_iPhone_VerifyMinimalAccountOrder(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void MinimalAccountOrder(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    public class T7526_Emulator_VerifyMinimalAccountOrder : T7526_MobileBase
    {
        public T7526_Emulator_VerifyMinimalAccountOrder(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void OrderDetailsOnConfirmationPage(string config) => Validate(config);
    }


    /// <summary>
    /// Verify a user can place an order with a minimal account.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8570
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7526
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8570"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7526")]
    public abstract class T7526_MobileBase : TestsBaseMobile
    {
        protected T7526_MobileBase(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the various elements of the Order Confirmation page.
        /// </summary>
        /// <param name="config"></param>
        protected void Validate(string config)
        {
            /*Arrange:
            User has an account that is a minimal account.
            User has added an item to the cart.
            */
            InitializeFunctionalTest(config);
            var account = LampsPlusAccounts.MinimalAccount;
            SignInWorkflow.SignInAndClearSession(account.UserName, account.Password);
            ShoppingCartWorkflow.EmptyCart();
            ManageAccountWorkflow.DeleteAllSavedAddresses();
            ShoppingCartWorkflow.AddMultipleSkuWithPriceOverTwoHundredDollarsToCart(1);

            /*Act:
            From the Cart Overview page, click on the 'Check Out Now' button.
            Fill out the Shipping page using an International Address and proceed to the Payment page.
            */
            Cart.CheckOut();
            Assert.True(Shipping.IsCurrentPage, "Current page is not a Shipping page");
            CustomerAddressInformation.EnterShippingAddress(IntAddress, isIntAddress: true);
            Shipping.ProceedToPayment();
            Payment.SelectInternationalAgreementAndPlaceOrder();

            //Assert: Verify the user is brought to the Order Confirmation page.
            Assert.True(OrderConfirmation.IsCurrentPage, "Current page is not Order confirmation page");
            var orderId = OrderConfirmation.GetOrderIdNumber;
            var emailAddress = OrderConfirmation.GetEmail;

            Assert.NotNull(orderId, "Order ID is not displayed");
            Assert.NotNull(emailAddress, "Email is not displayed");
        }
    }
}
