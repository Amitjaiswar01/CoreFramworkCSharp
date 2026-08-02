using System;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Pages;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.AutomationFramework.Workflow.Base;
using OpenQA.Selenium;

namespace LampsPlus.AutomationFramework.Workflow.Mobile
{
    /// <summary>
    /// Common behavior for adding products to the shopping cart for mobile tests.
    /// </summary>
    public class MobileShoppingCartWorkflow : ShoppingCartWorkflowBase
    {
        public MobileShoppingCartWorkflow(ICartOverview cartOverview, TestsBase testsBase) : base(cartOverview, testsBase) { }

        /// <inheritdoc />
        public override void CloseShippingOptions()
        {
            if (TestsBase.CartOverview.ShippingOptionsRadioButton.IsInitialized)
            {
                TestsBase.CartOverview.ShippingOptionsRadioButton.Click();
            }
        }

        /// <inheritdoc />
        public override void EmptyCart()
        {
            var currentUrl = Browser.PageUrl;

            if (Browser.PageUrl != Urls.CartOverviewPageUrl)
            {
                if (!currentUrl.Contains(Urls.HomePageUrl) || currentUrl.Contains("denv.aspx?j=1"))
                {
                    Browser.Navigate(Urls.HomePageUrl);
                } // If the current page is not a LampsPlus page navigate home.

                if (Convert.ToInt32(Browser.ExecuteJs("return lp.globals.cartCount")) == 0)
                {
                    return;
                }

                TestsBase.Browser.Navigate(Urls.CartOverviewPageUrl);

                Browser.Wait.IsVisibleElement(By.CssSelector(CartOverview.CheckOutNowClass.ToCssClassSelector()));
            }
     
            if (Browser.Locate.DoesElementExistImmediately(CartOverview.RemoveItemClass.ToCssClassSelector()))
            {
                var itemCount = TestsBase.CartOverview.RemoveItemLinksElements.Count;

                for (var i = 0; i < itemCount; i++)
                {
                    var cachedLink = TestsBase.CartOverview.RemoveItemLinksElements[0];

                    if (cachedLink != null)
                    {
                        cachedLink.Click(); 
                    }
                    else
                    {
                        return;
                    }

                    Browser.Wait.UntilElementUnloads(cachedLink, 60);
                }
            }

            Browser.Wait.IsVisibleElement(By.CssSelector(CartOverview.CartEmptyWarningClass.ToCssClassSelector()),30);
        }

        /// <inheritdoc />
        public override Address CreateNewSavedAddress(UserRole userRole, Address address = null, bool goBackToShippingPage = false)
        {
            TestsBase.CustomerAddressInformation.Navigate(Urls.ShippingPageUrl);

            var shippingAddress = address ?? new Address();
            shippingAddress.SaveToProfile = true;

            Browser.Wait.IsVisibleElement(By.CssSelector(TestsBase.Shipping.SingleShippingFirstNameId.ToCssIdSelector()));

            TestsBase.CustomerAddressInformation.EnterShippingAddress(shippingAddress, userRole);

            // clicking Proceed to Payment button enables the full address to be saved
            TestsBase.CustomerAddressInformation.ProceedToPaymentButton.Click();

            Browser.Wait.ForPage(Urls.PaymentPageUrl, 15);

            if (goBackToShippingPage)
            {
                Browser.Navigate(Urls.ShippingPageUrl);

                Browser.Wait.IsVisibleElement(By.Id(TestsBase.Shipping.ProceedPaymentId));
            }

            return shippingAddress;
        }

        public override void ProceedToPayment()
        {
            Browser.ScrollIntoView(TestsBase.CustomerAddressInformation.StateField);
            Browser.ExecuteJs("arguments[0].click()", TestsBase.OrderSummaryBlock.ProceedToPaymentButton.InternalElement);

            Browser.Wait.UntilElementUnloads(TestsBase.CustomerAddressInformation.ProceedToPaymentButton, 30);

            if (TestsBase.Payment.IsAgreementContainerVisible)
            {
                Browser.Wait.IsVisibleElement(By.ClassName(TestsBase.CartOverview.ProceedToPaymentClass));
                TestsBase.CartOverview.DeliveryPolicyAgreementProceedToPayment.Click();
            }
        }

        public override void ShowFedExValidationModal()
        {
            TestsBase.CustomerAddressInformation.AddAnotherAddressFieldLink.Click();

            TestsBase.CustomerAddressInformation.ClearAndEnterText(TestsBase.CustomerAddressInformation.EmailField, TestsBase.CustomerAddressInformation.Address.Email);
            TestsBase.CustomerAddressInformation.ClearAndEnterText(TestsBase.CustomerAddressInformation.PhoneField, TestsBase.CustomerAddressInformation.Address.Phone);

            TestsBase.CustomerAddressInformation.ClearAndEnterText(TestsBase.CustomerAddressInformation.StreetAddressField, TestsBase.CustomerAddressInformation.StreetAddressArdmoreString);
            Browser.Wait.ForDomReady();
            TestsBase.CustomerAddressInformation.ClearAndEnterText(TestsBase.CustomerAddressInformation.ApartmentSuiteOtherField, TestsBase.CustomerAddressInformation.ApartmentArdmoreString);
            TestsBase.CustomerAddressInformation.ClearAndEnterText(TestsBase.CustomerAddressInformation.CityField, TestsBase.CustomerAddressInformation.CityArdmoreString);

            TestsBase.CustomerAddressInformation.ClearAndEnterText(TestsBase.CustomerAddressInformation.ZipPostalCodeField, ZipCodeList.Ardmore);

            Browser.ScrollIntoView(TestsBase.CustomerAddressInformation.StateField,true);
            TestsBase.CustomerAddressInformation.StateField.Click();
            Browser.Wait.ForElementToStopAnimating(GlobalLocators.StateDropdown);
            Browser.ScrollIntoView(TestsBase.CustomerAddressInformation.StateSelection,true);
            TestsBase.CustomerAddressInformation.StateSelection.Click();
            Browser.Wait.ForElementToStopAnimating(GlobalLocators.StateDropdown);

            Browser.ScrollIntoView(TestsBase.CustomerAddressInformation.StateField);
            TestsBase.Shipping.ProceedToPaymentButton.Click();

            Browser.Wait.IsVisibleElement(By.ClassName(TestsBase.Shipping.FedExAddressValidationClass));
            Browser.SwitchFocusToIframe(TestsBase.CustomerAddressInformation.FedExAddressValidationModal);
        }

        public override bool IsPlaSkuAddedToCart(string url, string sku)
        {
            Browser.Navigate($"{url}?sfp={sku}");
            Browser.Wait.IsVisibleElement(By.CssSelector(TestsBase.SortPla.SfpQuickLookId.ToCssIdSelector()));
            if (TestsBase.SortPla.PlaFrameElement == null) return false;
            Browser.SwitchFocusToIframe(TestsBase.SortPla.PlaFrameElement);
            Browser.Wait.ForClickableElement(GlobalLocators.AddToCartButton).Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(TestsBase.CartOverview.CheckOutNowClass.ToCssClassSelector()));
            return TestsBase.CartOverview.CartOverviewElement != null && (string.CompareOrdinal(sku.ToLower(), TestsBase.CartOverview.ProductSkuCart.ToLower()) == 0);
        }

        public override void WaitForNavigation(int index)
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(TestsBase.Sort.ToggleSortMenuClass.ToCssClassSelector()));
        }
    }
}
