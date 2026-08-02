using Automation.Framework.Utilities;
using Castle.Core.Internal;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Environment;
using LampsPlus.AutomationFramework.Utilities.Payment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.DesktopTablet.OrderConfirmation;
using OpenQA.Selenium;
using xRetry;
using Xunit;
using Xunit.Abstractions;
using Skip = Xunit.Skip;

namespace LampsPlus.RegressionTests.Common.OrderConfirmation
{
	//[Collection(LpTraits.UserRole.Professional)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.RunEnvironment.TestDatabaseOnly)]
	public class T556_Windows_Pro_VerifyCanEnterNewPayment : T556_DesktopBase
	{
		public T556_Windows_Pro_VerifyCanEnterNewPayment(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [Theory(Skip = "Bug - LP-62624")]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_PCSI)]
		public void UserCanEnterNewPayment(string config) => Validate(config);
	}


    //[Collection(LpTraits.UserRole.Professional)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.RunEnvironment.TestDatabaseOnly)]
    public class T556_Mac_Pro_VerifyCanEnterNewPayment : T556_DesktopBase
    {
        public T556_Mac_Pro_VerifyCanEnterNewPayment(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_PCSI)]
        public void UserCanEnterNewPayment(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Professional)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.RunEnvironment.TestDatabaseOnly)]
    public class T556_iPad_Pro_VerifyCanEnterNewPayment : T556_DesktopBase
    {
        public T556_iPad_Pro_VerifyCanEnterNewPayment(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_PCSI)]
        public void UserCanEnterNewPayment(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Professional)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.RunEnvironment.TestDatabaseOnly)]
    public class T556_TabletEmulator_Pro_VerifyCanEnterNewPayment : T556_DesktopBase
    {
        public T556_TabletEmulator_Pro_VerifyCanEnterNewPayment(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_PCSI)]
        public void UserCanEnterNewPayment(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Customer)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.RunEnvironment.TestDatabaseOnly)]
	public class T567_iPhone_VerifyCanEnterNewPayment : T567_MobileBase
	{
		public T567_iPhone_VerifyCanEnterNewPayment(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
		public void UserCanEnterNewPayment(string config) => Validate(config);
	}


	//[Collection(LpTraits.UserRole.Professional)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.RunEnvironment.TestDatabaseOnly)]
	public class T567_iPhone_Pro_VerifyCanEnterNewPayment : T567_MobileBase
	{
		public T567_iPhone_Pro_VerifyCanEnterNewPayment(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_PCSI)]
		public void UserCanEnterNewPayment(string config) => Validate(config);
	}


	//[Collection(LpTraits.UserRole.Customer)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.RunEnvironment.TestDatabaseOnly)]
	public class T567_Android_VerifyCanEnterNewPayment : T567_MobileBase
	{
		public T567_Android_VerifyCanEnterNewPayment(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_NPCSI)]
		public void UserCanEnterNewPayment(string config) => Validate(config);
	}


	//[Collection(LpTraits.UserRole.Professional)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.RunEnvironment.TestDatabaseOnly)]
	public class T567_Android_Pro_VerifyCanEnterNewPayment : T567_MobileBase
	{
		public T567_Android_Pro_VerifyCanEnterNewPayment(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_PCSI)]
        public void UserCanEnterNewPayment(string config) => Validate(config);
    }


	//[Collection(LpTraits.UserRole.Customer)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.RunEnvironment.TestDatabaseOnly)]
	public class T567_Emulator_VerifyCanEnterNewPayment : T567_MobileBase
	{
		public T567_Emulator_VerifyCanEnterNewPayment(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void UserCanEnterNewPayment(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Customer)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.RunEnvironment.TestDatabaseOnly)]
    public class T567_Pro_Emulator_VerifyCanEnterNewPayment : T567_MobileBase
    {
        public T567_Pro_Emulator_VerifyCanEnterNewPayment(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_PCSI)]
        public void UserCanEnterNewPayment(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that a user can enter in a new payment type at the time of checkout.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-6589
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T556
    /// </summary>
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-6589"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T556"), Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
	public abstract class T556_DesktopBase : T556_T567_Base
	{
		protected T556_DesktopBase(ITestOutputHelper output) : base(output) { }
	}


    /// <summary>
    /// Verify that a user can enter in a new payment type at the time of checkout.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-6591
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T567
    /// </summary>
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-6591"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T567"), Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
	public abstract class T567_MobileBase : T556_T567_Base
	{
		protected T567_MobileBase(ITestOutputHelper output) : base(output) { }
	}


    public abstract class T556_T567_Base : OrderConfirmationTestsBase
    {
        protected T556_T567_Base(ITestOutputHelper output) : base(output) { }
       
        protected void Validate(string config)
        {
            // Initialization & Pre-conditions
            var setup = new TestSetup(config, Urls.ManagePaymentOptionsPageUrl);
            InitializeFramework(config, setup: setup);

            Skip.IfNot(DevEnvInformation.DatabaseSymbol == "T", "This test can only be executed against DBTEST.");

            ManageAccountWorkflow.AddNewDefaultPaymentMethod();
            Browser.Navigate(Urls.ManageAccountPageUrl);
			long.TryParse(ManageAccount.RewardNumber, out var rewardsNumber);

			var productBetweenTenAndTwenty = ProductActions.GetSkuBetweenTenAndTwentyDollars;
            Assert.DatabaseObject(productBetweenTenAndTwenty, "ProductActions.GetSkuBetweenTenAndTwentyDollars()");
            ShoppingCartWorkflow.AddItemToCartBySku(new ProductModel { Sku = productBetweenTenAndTwenty });
            Browser.Wait.ForDomReady();

            // Cart Overview Workflow
            Browser.Wait.IsVisibleElement(By.CssSelector(CartOverview.CheckOutNowClass.ToCssClassSelector()));
            CartOverview.CheckOutNowButton.Click();

            // Shipping Page Workflow
            Browser.Wait.IsVisibleElement(By.CssSelector(Shipping.ProceedPaymentId.ToCssIdSelector()));
            WaitForGlobalSpinnerToClose();
            ShoppingCartWorkflow.EnterDefaultShippingAddress();
            ShoppingCartWorkflow.ProceedToPayment();

            // Payment Page Workflow
            Browser.Wait.IsVisibleElement(By.CssSelector(Payment.PlaceYourOrderButtonId.ToCssIdSelector()));

            Payment.NewPaymentOption.Click();
            var newPaymentMethod = CreditCards.TestMasterCard;
            Payment.EnterCreditCardInfo(newPaymentMethod);
            Payment.PlaceOrderButton.Click();

            // Order Confirmation Workflow
            Browser.Wait.ForPage(Urls.OrderConfirmationPageUrl);

            VerifyDatabaseInformation(rewardsNumber);
        }

        private void VerifyDatabaseInformation(long rewardNumber)
        {
            var paymentInfo = AccountActions.GetPaymentInfoFromUser(rewardNumber);

            Assert.True(paymentInfo.Count == 2, "Database doesn't contain 2 payment options for user.");

            for (var i = 0; i < paymentInfo.Count; i++)
            {
                var payment = paymentInfo[i];
                // First card in added by workflow is a Visa, our second card added at checkout is a MasterCard
                var cardType = i == 0 ? CreditCards.TestVisaCard.CardType : CreditCards.TestMasterCard.CardType;

                Assert.False(payment.PaymentToken.IsNullOrEmpty(), "Payment token column is empty.");
                Assert.Equals(payment.LastFourDigit, payment.PaymentToken.Substring(payment.PaymentToken.Length - 4), "Last four of PaymentToken column isn't equal to LastFourDigit column.");
                Assert.Equals(cardType, payment.CardType,"CardType column isn't equal to the actual entered card type.");
            }
        }
    }
}
