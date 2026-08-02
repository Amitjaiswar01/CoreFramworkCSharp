using Automation.Framework;

using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

using OpenQA.Selenium;

using System;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.IntegrationTests.Pages.OrderConfirmation
{
    /// <summary>
    /// Tests to ensure all IElements and Lists of IElements can be found on the given page object.
    /// </summary>
    public class OrderConfirmationDesktopPageLocatorTest : OrderConfirmationPageLocatorTest
    {
        /// <summary>
        /// Tests to ensure this page can find all its IElements.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public OrderConfirmationDesktopPageLocatorTest(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the Order Confirmation page elements could be located.
        /// </summary>
        [Trait(LpTraits.Integration.PageObjectModel, "OrderConfirmation")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LocateElementsOnOrderConfirmationPageTest(string config) => Locate(config);

        protected override void VerifyOrderConfirmation()
        {
            VerifyOcPageHeadingElement();

            Browser.Navigate(Urls.HomePageUrl);

            PlaceInternationalOrder();

            // check for modal content and close them
            var lpModal = Browser.Locate.ElementImmediately($"#{GlobalLocators.LpModalId}");

            Browser.Wait.ForDisplayedElement(OrderConfirmation.OrderConfirmationReviewModal, 15);
            VerifyElementDisplayed(() => OrderConfirmation.OrderConfirmationReviewModal);

            Browser.Wait.UntilElementUnloads(lpModal);

            VerifyElementDisplayed(() => OrderConfirmation.OrderDetailsItemShipmentElements);
            VerifyElementDisplayed(() => OrderConfirmation.OrderIdLabel);
            VerifyElementDisplayed(() => OrderConfirmation.EmailUTagElement);
            VerifyElementDisplayed(() => OrderConfirmation.OrderIdHeading);
            VerifyElementDisplayed(() => OrderConfirmation.OrderIdNumberElement);
            VerifyElementDisplayed(() => OrderConfirmation.ProductNameElement);
            VerifyElementDisplayed(() => OrderConfirmation.ProductSkuLabelOrder);
            VerifyElementDisplayed(() => OrderConfirmation.CreateAnAccountButton);
            VerifyElementDisplayed(() => OrderConfirmation.OrderSummaryContainer);
            VerifyElementDisplayed(() => OrderConfirmation.BillingAddressElement);
            VerifyElementDisplayed(() => OrderConfirmation.ShippingAddressElement);

            // Create Account
            OrderConfirmation.CreateAnAccountButton.Click();

            VerifyElementDisplayed(() => OrderConfirmation.CreateAccountModalElement);
            VerifyElementDisplayed(() => OrderConfirmation.LpModalContent);

            OrderConfirmation.CreateAccountPasswordElement.Clear();
            OrderConfirmation.CreateAccountPasswordElement.SendKeys("Password123");
            OrderConfirmation.CreateAccountSecurityQuestionElement.Click();
            OrderConfirmation.SelectQuestion("What is your favorite color?");
            OrderConfirmation.CreateAccountSecurityAnswerElement.Clear();
            OrderConfirmation.CreateAccountSecurityAnswerElement.SendKeys("Blue");

            VerifyElementDisplayed(() => OrderConfirmation.CreateAccountPasswordElement);
            VerifyElementDisplayed(() => OrderConfirmation.CreateAccountSecurityQuestionElement);
            VerifyElementDisplayed(() => OrderConfirmation.CreateAccountSecurityAnswerElement);
            VerifyElementDisplayed(() => OrderConfirmation.CreateAccountModalButtonElement);
            VerifyElementNotImplemented(() => OrderConfirmation.MobileSecurityQuestionDrawer);
            VerifyElementNotImplemented(() => OrderConfirmation.MobileSecurityQuestion);
            VerifyElementNotImplemented(() => OrderConfirmation.MobileDrawerContainer);

            OrderConfirmation.CreateAccountModalButtonElement.Click();

            Browser.Wait.ForClickableElement(OrderConfirmation.CreateAccountSuccessElement);

            VerifyElementDisplayed(() => OrderConfirmation.CreateAccountSuccessElement);
            VerifyElementDisplayed(() => OrderConfirmation.CreateAccountConfirmationElement);

            OrderConfirmation.CreateAccountConfirmationElement.Click();

            VerifyLincOptInWidget();
        }

        protected void VerifyOcPageHeadingElement()
        {
            // Customer Service - Order
            Browser.Navigate(Urls.HomePageUrl);

            var userAccountUnderTest = LampsPlusAccounts.CustomerServiceRegularLoginAccount;
            SignInWorkflow.SignIn(userAccountUnderTest);

            Home.ClearStoreInSession();
            ShoppingCartWorkflow.EmptyCart();
            ShoppingCartWorkflow.EmployeeCheckoutWithSingleItem();
            CustomerAddressInformation.EnterShippingAddress(new Address
            {
                Country = CountryCodeList.US,
                State = StateCodeListUnitedStates.CA,
                Email = userAccountUnderTest.UserName
            });
            ShoppingCartWorkflow.ProceedToPayment();
            ShoppingCartWorkflow.EmployeePlaceOrderViaCheck();

            // Customer Service - Order Confirmation
            WaitForGlobalSpinnerToClose();

            VerifyElementDisplayed(() => OrderConfirmation.HoldReasonsElement);
            VerifyElementNotImplemented(() => OrderConfirmation.OcPageHeadingElement);
            SignInWorkflow.SignOut();
        }

    }


    /// <summary>
    /// Tests to ensure all IElements and Lists of IElements can be found on the given page object.
    /// </summary>
    public class OrderConfirmationMobilePageLocatorTest : OrderConfirmationPageLocatorTest
    {
        /// <summary>
        /// Tests to ensure this page can find all its IElements.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public OrderConfirmationMobilePageLocatorTest(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the Order Confirmation page elements could be located.
        /// </summary>
        [Trait(LpTraits.Integration.PageObjectModel, "OrderConfirmation")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LocateElementsOnOrderConfirmationPageTest(string config) => Locate(config);

        protected override void VerifyOrderConfirmation()
        {
            PlaceInternationalOrder();

            VerifyElementDisplayed(() => OrderConfirmation.MobileDrawerContainer);
            VerifyElementDisplayed(() => OrderConfirmation.OrderIdLabel);
            VerifyElementDisplayed(() => OrderConfirmation.EmailUTagElement);
            VerifyElementDisplayed(() => OrderConfirmation.OrderIdHeading);
            VerifyElementDisplayed(() => OrderConfirmation.OrderIdNumberElement);
            VerifyElementDisplayed(() => OrderConfirmation.ProductNameElement);
            VerifyElementDisplayed(() => OrderConfirmation.ProductSkuLabelOrder);
            VerifyElementDisplayed(() => OrderConfirmation.BillingAddressElement);
            VerifyElementDisplayed(() => OrderConfirmation.ShippingAddressElement);

            VerifyElementDisplayed(() => OrderConfirmation.CreateAnAccountButton);
            VerifyElementDisplayed(() => OrderConfirmation.OrderSummaryContainer);
            VerifyElementNotImplemented(() => OrderConfirmation.GoogleSurveyModalIframe);
            VerifyElementNotImplemented(() => OrderConfirmation.GoogleSurveyModalNoButton);
            VerifyElementNotImplemented(() => OrderConfirmation.HoldReasonsElement);
            VerifyElementNotImplemented(() => OrderConfirmation.CloseWinDialogElement);
            VerifyElementNotImplemented(() => OrderConfirmation.OrderDetailsItemShipmentElements);
            VerifyElementNotImplemented(() => OrderConfirmation.OrderConfirmationReviewModal);

            OrderConfirmation.CreateAnAccountButton.Click();
            Browser.Wait.ForDisplayedElement(OrderConfirmation.LpModalContent);
            VerifyElementDisplayed(() => OrderConfirmation.LpModalContent);
            VerifyElementNotImplemented(() => OrderConfirmation.CreateAccountModalElement);
            OrderConfirmation.CreateAccountPasswordElement.Clear();
            OrderConfirmation.CreateAccountPasswordElement.SendKeys("Password123");
            VerifyElementDisplayed(() => OrderConfirmation.CreateAccountPasswordElement);
            VerifyElementDisplayed(() => OrderConfirmation.CreateAccountSecurityQuestionElement);
            VerifyElementDisplayed(() => OrderConfirmation.CreateAccountSecurityAnswerElement);
            Browser.ExecuteJs("document.getElementById('securityQuestionDrawer').classList.remove('hidden')");
            VerifyElementDisplayed(() => OrderConfirmation.MobileSecurityQuestionDrawer);
            VerifyElementDisplayed(() => OrderConfirmation.MobileSecurityQuestion);
            OrderConfirmation.MobileSecurityQuestionDrawer.FindElement(By.XPath("//label[@for='securityQuestion1']"))
                .Click();
            OrderConfirmation.CreateAccountSecurityAnswerElement.Clear();
            OrderConfirmation.CreateAccountSecurityAnswerElement.SendKeys("Blue");
            VerifyElementDisplayed(() => OrderConfirmation.CreateAccountModalButtonElement);
            Browser.Wait.ForClickableElement(OrderConfirmation.CreateAccountModalButtonElement).Click();
            Browser.Wait.ForDisplayedElement(OrderConfirmation.CreateAccountSuccessElement);
            VerifyElementDisplayed(() => OrderConfirmation.CreateAccountSuccessElement);
            VerifyElementDisplayed(() => OrderConfirmation.CreateAccountConfirmationElement);
            Browser.ClickByJs(OrderConfirmation.CreateAccountConfirmationElement);
            Browser.Wait.UntilElementUnloads(OrderConfirmation.CreateAccountSuccessElement);
            Browser.Navigate(Urls.ConfirmationExpiredUrl);
            VerifyElementDisplayed(() => OrderConfirmation.OcPageHeadingElement);
            VerifyLincOptInWidget();
        } 
    }


    /// <summary>
    /// Tests to ensure all IElements and Lists of IElements can be found on the given page object.
    /// </summary>
    [Trait(LpTraits.Keys.Database, LpTraits.PPE.DbTest)]
    public abstract class OrderConfirmationPageLocatorTest : PageObjectTestsBase
    {
        /// <summary>
        /// Tests to ensure this page can find all its IElements.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        protected OrderConfirmationPageLocatorTest(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the Order Confirmation page elements could be located.
        /// </summary>
        public void Locate(string config)
        {
            // Initialize
            InitializeFramework(config);
            BuildElementsList(OrderConfirmation);
            VerifyOrderConfirmation();
            // TODO: Not sure how to reproduce the DOM with these elements
            VerifyElementNotDisplayed("SpecificErrorElement");
            //
        }

        protected abstract void VerifyOrderConfirmation();

        protected void HandleModalOnOrderConfirmation(IElement lpModal, string surveyId)
        {
            if (lpModal.IsInitialized)
            {
                // LP Modal and content is showing
                // surveyId = "4" (Trust Pilot) content also lives inside a lpModal
                VerifyElementNotDisplayed("GoogleSurveyModalIframe");
                VerifyElementNotDisplayed("GoogleSurveyModalNoButton");
                VerifyElementNotDisplayed("CloseWinDialogElement");
                GlobalLocators.LpModalCloseElement.Click();
            }
            else if (surveyId != "3" && surveyId != "")
            {
                // 3rd Party Modal and/or content is showing
                switch (surveyId)
                {
                    case "1":   // Google Review Modal
                        {
                            VerifyElementDisplayed(() => OrderConfirmation.GoogleSurveyModalIframe);

                            Browser.SwitchFocusToIframe(OrderConfirmation.GoogleSurveyModalIframe);

                            VerifyElementDisplayed(() => OrderConfirmation.GoogleSurveyModalNoButton);

                            Browser.SwitchToDefaultContent();

                            VerifyElementNotDisplayed("CloseWinDialogElement");

                            break;
                        }
                    case "2":   // Biz Rate Survey Modal
                        {
                            VerifyElementDisplayed(() => OrderConfirmation.CloseWinDialogElement);
                            VerifyElementNotDisplayed("GoogleSurveyModalIframe");
                            VerifyElementNotDisplayed("GoogleSurveyModalNoButton");

                            break;
                        }
                    case "0":   // Unknown
                    default:
                        {
                            VerifyElementNotDisplayed("GoogleSurveyModalIframe");
                            VerifyElementNotDisplayed("GoogleSurveyModalNoButton");
                            VerifyElementNotDisplayed("CloseWinDialogElement");

                            break;
                        }
                }
            }
            else
            {
                // No modal showing
                VerifyElementNotDisplayed("GoogleSurveyModalIframe");
                VerifyElementNotDisplayed("GoogleSurveyModalNoButton");
                VerifyElementNotDisplayed("CloseWinDialogElement");
            }
        }

        protected void VerifyLincOptInWidget()
        {
            Browser.Navigate(Urls.HomePageUrl);
            var getLincCompatibleProduct = ProductActions.GetLincCompatibleProduct;
            ConditionalVerify.DatabaseObject(getLincCompatibleProduct, "ProductActions.GetLincCompatibleProduct()");
            ShoppingCartWorkflow.AddItemToCartBySku(new ProductModel { Sku = getLincCompatibleProduct });

            SignInWorkflow.SignIn(LampsPlusAccounts.CustomerLoginAccount);

            Browser.Navigate(Urls.CartOverviewPageUrl);
            Browser.Wait.ForPage(Urls.CartOverviewPageUrl);
            Browser.Wait.ForDomReady();

            CartOverview.CheckOutNowButton.Click();
            Browser.Wait.ForPage(Urls.ShippingPageUrl);

            WaitForGlobalSpinnerToClose();
         
            if (!CustomerAddressInformation.SavedAddressShippingInfo.IsInitialized)
            {
                ShoppingCartWorkflow.EnterDefaultShippingAddress();
            }
            ShoppingCartWorkflow.ProceedToPayment();

            Browser.Wait.ForClickableElement(Payment.SameAsShippingCheckBoxGeneric).Click();

            CustomerAddressInformation.EnterIntBillingAddress(new IntAddress());

            Payment.PlaceInternationalOrder();

            Browser.Wait.ForPage(Urls.OrderConfirmationPageUrl);

            Browser.Wait.ForDisplayedElement(OrderConfirmation.LincOptInWidget);
            VerifyElementDisplayed(() => OrderConfirmation.LincOptInWidget);
        }

        protected void PlaceInternationalOrder()
        {
            //Place international order with > $200 item
            ShoppingCartWorkflow.AddItemToCartBySku(new ProductModel()
            {
                Sku = ProductActions.GetSkuGreaterThanTwoHundredDollars,
                Quantity = 1
            });

            CartOverview.CheckOutNowButton.Click();

            var email = $"{DateTime.Now:MMddyyyyHHMMss}@automation.com";

            CustomerAddressInformation.EnterShippingAddress(new IntAddress { Email = email }, true);

            Browser.Wait.ForClickableElement(CustomerAddressInformation.ProceedToPaymentButton);

            CustomerAddressInformation.ProceedToPayment();

            Browser.Wait.ForPage(Urls.PaymentPageUrl, 30);

            Payment.PlaceInternationalOrder();

            Browser.Wait.ForPage(Urls.OrderConfirmationPageUrl);
        }
    }
}
