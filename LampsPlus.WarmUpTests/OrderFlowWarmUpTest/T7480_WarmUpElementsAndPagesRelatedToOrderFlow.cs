using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.DesktopTablet.OrderConfirmation;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.WarmUpTests.OrderFlowWarmUpTest
{
    public class T7480_WarmUpElementsAndPagesRelatedToOrderFlow : T7480_DesktopBase
    {
        public T7480_WarmUpElementsAndPagesRelatedToOrderFlow(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]		
        public void WarmUpTestForOrderFlow(string config) => Validate(config);
    }


	/// <summary>
	/// Warm up elements and pages related to the Order Flow.
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8405
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7480
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8405"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7480")]
    public abstract class T7480_DesktopBase : OrderConfirmationTestsBase
	{
        protected T7480_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            InitializeFramework(config);

            ShoppingCartWorkflow.AddMultipleSkuWithPriceOverTwoHundredDollarsToCart(2);

	        Browser.Navigate(Urls.ShippingNotificationPageUrl);

            Browser.Wait.IsVisibleElement(By.XPath(Shipping.CalloutBtnXpath));

			Shipping.ShippingNotificationProceedToPaymentButton.Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(CartOverview.CheckOutNowClass.ToCssClassSelector()));

	        Browser.Wait.ForClickableElement(CartOverview.ChangeShippingOptionsLink).Click();
	        Browser.Wait.ForElement(CartOverview.ShippingOptionModal);
	        GlobalLocators.LpModalCloseElement.Click();

	        CartOverview.CheckOutNowButton.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(Shipping.ProceedPaymentId.ToCssIdSelector()));

            CustomerAddressInformation.FirstNameField.SendKeys("John");
            CustomerAddressInformation.LastNameField.SendKeys("Doe");
            CustomerAddressInformation.StreetAddressField.SendKeys("PO Box");
            CustomerAddressInformation.ZipPostalCodeField.Click(); //Automation issue only. Needed so Google Autocomplete appears. This is not an issue when manually entering address.
            CustomerAddressInformation.StreetAddressField.Click();

            Browser.Wait.ForElementToStopAnimating(CustomerAddressInformation.GoogleAutocompleteElement);

            CustomerAddressInformation.StreetAddressField.SendKeys(Keys.ArrowDown);
            CustomerAddressInformation.StreetAddressField.SendKeys(Keys.Enter);

            ShoppingCartWorkflow.ShowFedExValidationModal();

            GlobalLocators.LpModalCloseElement.Click();

            Browser.Wait.IsVisibleElement(By.Id(Payment.PlaceYourOrderButtonId));

            Browser.Navigate(Urls.ShippingPageUrl);
            Browser.Wait.ForClickableElement(Shipping.ProceedToPaymentButton);

			CustomerAddressInformation.ClearFormControl(CustomerAddressInformation.AddressContainerElement);
	        CustomerAddressInformation.EnterShippingAddress(new IntAddress("LP-T7480"), isIntAddress:true);

            CustomerAddressInformation.ProceedToPayment();
			Browser.Wait.ForPage(Urls.PaymentPageUrl);
	        Browser.Wait.ForDomReady();

	        Payment.PlaceInternationalOrder();

            Browser.Wait.IsVisibleElement(By.CssSelector(OrderConfirmation.OrderConfirmationHeadingClass.ToCssClassSelector()));
            Browser.Wait.ForClickableElement(OrderConfirmation.OrderConfirmationCreateAccount);

            Browser.Navigate(Urls.ConfirmationExpiredUrl);
	        Browser.Navigate(Urls.EmailCartUrl);

            SignInWorkflow.SignIn(LampsPlusAccounts.CustomerServiceRegularLoginAccount);

            ShoppingCartWorkflow.AddMultipleSkuWithPriceOverTwoHundredDollarsToCart(2);

            Browser.Locate.ClickDropdownByValue(CsrBlock.SaleSourceField, "1");

            Browser.Wait.IsVisibleElement(By.CssSelector(CartOverview.CheckOutNowClass.ToCssClassSelector()));

            var checkoutButton = CartOverview.CheckOutNowButton;
            checkoutButton.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(Shipping.ProceedPaymentId.ToCssIdSelector()));

            Shipping.ShipToMultipleAddressesButton.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(Shipping.IsMultipleShippingClass.ToCssClassSelector()));

            Shipping.NewAddressButton(0).Click();
        }
    }
}
