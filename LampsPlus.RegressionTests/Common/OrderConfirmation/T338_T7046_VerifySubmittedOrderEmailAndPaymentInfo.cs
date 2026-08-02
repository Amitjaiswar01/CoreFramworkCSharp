using System;
using Xunit;
using Xunit.Abstractions;
using xRetry;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.DesktopTablet.OrderConfirmation;
using OpenQA.Selenium;

namespace LampsPlus.RegressionTests.Common.OrderConfirmation
{
    //[Collection(LpTraits.BatchGroup.Common.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    public class T338_Windows_VerifyOrderEmailAndPymtInfo : T338_DesktopBase
    {
        public T338_Windows_VerifyOrderEmailAndPymtInfo(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void OrderEmailAndPymtInfo(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    public class T338_Mac_VerifyOrderEmailAndPymtInfo : T137_DesktopBase
    {
        public T338_Mac_VerifyOrderEmailAndPymtInfo(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void OrderEmailAndPymtInfo(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    public class T338_iPad_VerifyOrderEmailAndPymtInfo : T137_DesktopBase
    {
        public T338_iPad_VerifyOrderEmailAndPymtInfo(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void OrderEmailAndPymtInfo(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    public class T338_TabletEmulator_VerifyOrderEmailAndPymtInfo : T137_DesktopBase
    {
        public T338_TabletEmulator_VerifyOrderEmailAndPymtInfo(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void OrderEmailAndPymtInfo(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.OrderConfirmation)]
    public class T7046_iPhone_VerifyOrderEmailAndPymtInfo : T7046_MobileBase
	{
		public T7046_iPhone_VerifyOrderEmailAndPymtInfo(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void OrderEmailAndPymtInfo(string config) => Validate(config);
	}


    //[Collection(LpTraits.BatchGroup.Common.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    public class T7046_Emulator_VerifyOrderEmailAndPymtInfo : T7046_MobileBase
    {
        public T7046_Emulator_VerifyOrderEmailAndPymtInfo(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void OrderEmailAndPymtInfo(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that order submissions get email address from the Shipping page & remaining info from the Payment page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-6524
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T338
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-6524"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T338")]
    public abstract class T338_DesktopBase : T338_T7046_Base
    {
        protected T338_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected override void CreateAccount()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(OrderConfirmation.OrderConfirmationOrderIdClass));
        }
    }


	/// <summary>
	/// Verify that order submissions get email address from the Shipping page & remaining info from the Payment page.
	/// Jira Task ID: https://lampstrack.lampsplus.com:8443/browse/ACD-5481
	/// Test Case ID: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7046
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5481"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7046")]
    public abstract class T7046_MobileBase : T338_T7046_Base
    {
        protected T7046_MobileBase(ITestOutputHelper output) : base(output) { }
        protected override void CreateAccount()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(OrderConfirmation.LincOptinWidgetClass));
        }
    }


    public abstract class T338_T7046_Base : OrderConfirmationTestsBase
    {
        protected T338_T7046_Base(ITestOutputHelper output) : base(output) { }
		
		protected void Validate(string config)
        {
            InitializeFramework(config, Urls.HomePageUrl);

            ShoppingCartWorkflow.AddItemToCartBySku(new ProductModel(ProductActions.GetSkuGreaterThanTwoHundredDollars));

            CartOverview.CheckOutNowButton.Click();

            var randomEmail = $"lp-email{DateTime.Now:MMddyyyyHHMMssff}@mailinator.com";
            var shippingAddress = new Address("-LP-Shipping") { Email = randomEmail };
            var billingAddress = new IntAddress("-LP-Billing");

            Browser.Wait.IsVisibleElement(By.CssSelector(Shipping.SingleShippingFirstNameId.ToCssIdSelector()));

            CustomerAddressInformation.EnterShippingAddress(shippingAddress);

            ShoppingCartWorkflow.ProceedToPayment();

            Browser.Wait.IsVisibleElement(By.CssSelector(Payment.PlaceYourOrderButtonId.ToCssIdSelector()));

            CustomerAddressInformation.EnterIntBillingAddress(billingAddress);

            Payment.PlaceInternationalOrder();

            CreateAccount();

            var pageText = OrderConfirmation.OrderSummaryContainer.Text;
            var dbOrderDetails = OrderActions.GetGlobalOrderDetails(randomEmail);

            Assert.DatabaseObject(dbOrderDetails, "OrderActions.GetGlobalOrderDetails()");

            // Testing page data
            Assert.StringContains(pageText.ToLower(), dbOrderDetails.EmailAddress.ToLower(), "Email address isn't shown on order confirmation page.");
            Assert.StringContains(pageText, dbOrderDetails.BillToFirstname, "Payment first name not on order confirmation page.");
            Assert.StringContains(pageText, dbOrderDetails.BillToLastname, "Payment last name not on order confirmation page.");
            Assert.StringContains(pageText, dbOrderDetails.BillToZipCode, "Payment zip code not on order confirmation page.");

            // Testing db data
            Assert.Equals(shippingAddress.Email, dbOrderDetails.EmailAddress, "Email entered on shipping page does not match database entry.");
            Assert.Equals(billingAddress.FirstName, dbOrderDetails.BillToFirstname, "First name that was entered on payment page does not match database entry.");
            Assert.Equals(billingAddress.LastName, dbOrderDetails.BillToLastname, "Last name that was entered on payment page does not match database entry.");
            Assert.Equals(billingAddress.ZipCode, dbOrderDetails.BillToZipCode, "Zip code that was entered on payment page does not match database entry.");
        }

        protected abstract void CreateAccount();
    }
}
