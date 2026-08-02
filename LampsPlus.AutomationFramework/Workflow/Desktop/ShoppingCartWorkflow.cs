using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Pages;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.AutomationFramework.Workflow.Base;
using OpenQA.Selenium;
using OperatingSystem = LampsPlus.AutomationFramework.Utilities.TestConfiguration.OperatingSystem;

namespace LampsPlus.AutomationFramework.Workflow.Desktop
{
    /// <summary>
    /// Common behavior for adding products to the shopping cart.
    /// </summary>
    public class ShoppingCartWorkflow : ShoppingCartWorkflowBase
    {
        public ShoppingCartWorkflow(ICartOverview cartOverview, TestsBase testsBase) : base(cartOverview, testsBase) { }

        /// <inheritdoc />
        public override void CloseShippingOptions()
        {
            var cachedLpModal = GlobalLocators.Iframe;
            TestsBase.CartOverview.UpdateShipButton.Click();
            Browser.Wait.UntilElementUnloads(cachedLpModal);
        }
        
        /// <inheritdoc />
        public override void EmptyCart()
        {
            var currentUrl = Browser.PageUrl;

            if (Browser.PageUrl != Urls.CartOverviewPageUrl)
            {
                if (!currentUrl.Contains(Urls.HomePageUrl) || currentUrl.Contains("denv.aspx"))
                {
                    Browser.Navigate(Urls.HomePageUrl);
                    Browser.Wait.ForDomReady(60);
                } // If the current page is not a LampsPlus page navigate home.

                if (Browser.Locate.DoesElementExistImmediately(Framework.Home.InvisibleClass.ToCssClassSelector()))
                {
                    if (TestsBase.Home.CartCountElement.Text == "")
                    {
                        return;
                    }
                }

                TestsBase.Browser.Navigate(Urls.CartOverviewPageUrl);
            }

            Browser.Wait.ForDomReady(30);

            if (Browser.Locate.DoesElementExistImmediately(GlobalLocators.LpModalId.ToCssIdSelector())) { TestsBase.CloseLpModal(); }

            Browser.Wait.ForDomReady();

            if (Browser.Locate.DoesElementExistImmediately(CartOverview.RemoveItemClass.ToCssClassSelector()))
            {
                var cartItemCount = TestsBase.CartOverview.RemoveItemLinksElements.Count;

                for (var i = 0; i < cartItemCount; i++)
                {
                    if (i != 0) { Browser.RefreshPage(); } //Refresh page for all Items except the very first item.
                    Browser.Wait.ForDisplayedElement(TestsBase.CartOverview.CartItemRemoveLinkElement(0));
                    TestsBase.CartOverview.CartItemRemoveLinkElement(0).Click();
                    Browser.Wait.IsVisibleElement(By.Id(GlobalLocators.LpModalId));
                    TestsBase.CartOverview.RemoveButton.Click();
                    Browser.Wait.IsVisibleElement(By.Id(GlobalLocators.LpModalId));
                }
            }

            Browser.Wait.IsVisibleElement(By.ClassName(CartOverview.CartEmptyWarningClass),60);
        }

        /// <inheritdoc />
        public override Address CreateNewSavedAddress(UserRole userRole, Address address = null, bool goBackToShippingPage = false)
        {
            TestsBase.CustomerAddressInformation.Navigate(Urls.ShippingPageUrl);

            var shippingAddress = address ?? new Address();
            shippingAddress.SaveToProfile = true;

            TestsBase.CustomerAddressInformation.EnterShippingAddress(shippingAddress, userRole);

            // clicking Proceed to Payment button enables the full address to be saved
            Browser.Wait.ForClickableElement(TestsBase.CustomerAddressInformation.ProceedToPaymentButton).Click();

            Browser.Wait.UntilElementUnloads(TestsBase.CustomerAddressInformation.ProceedToPaymentButton);
            Browser.Wait.ForDomReady();

            if (Browser.PageUrl == Urls.ShippingNotificationPageUrl)
            {
                TestsBase.Shipping.ShippingNotificationProceedToPaymentButton.Click();
            }

            if (goBackToShippingPage)
            {
                TestsBase.WaitForGlobalSpinnerToClose();

                TestsBase.Payment.EditLink.Click();
            }

            return shippingAddress;
        }

        public override void ProceedToPayment()
        {
            if (TestsBase.OperatingSystem == OperatingSystem.iPad)
            {
                Browser.ScrollIntoView(TestsBase.OrderSummaryBlock.ProceedToPaymentButton);
                Browser.ScrollToByPixelsVertical("-70");
                var xElementCoordinate = 0;
                var yElementCoordinate = 0;
                Browser.GetElementCoordinates(TestsBase.OrderSummaryBlock.ProceedToPaymentButton, ref xElementCoordinate, ref yElementCoordinate, 110);
                Browser.ClickWithTapByCoordinates(xElementCoordinate, yElementCoordinate);
            }
            else
            {
                Browser.Wait.ForDomReady();
                TestsBase.OrderSummaryBlock.ProceedToPaymentButton.Click();

            }

            Browser.Wait.UntilElementUnloads(TestsBase.CustomerAddressInformation.ProceedToPaymentButton, 30);

            Browser.Wait.ForDomReady();

            if (TestsBase.Payment.IsAgreementContainerVisible)
            {
                Browser.Wait.IsVisibleElement(By.CssSelector(TestsBase.Payment.DeliveryCallOutBtnSelector));
                TestsBase.Payment.DeliveryAgreementBox.Click();
            }

            Browser.Wait.IsVisibleElement(By.CssSelector(TestsBase.Payment.OrderSummaryProductsClass.ToCssClassSelector()), 30);
        }

        public override void ShowFedExValidationModal()
        {
            TestsBase.CustomerAddressInformation.AddAnotherAddressFieldLink.Click();

            TestsBase.CustomerAddressInformation.ClearAndEnterText(TestsBase.CustomerAddressInformation.StreetAddressField, TestsBase.CustomerAddressInformation.StreetAddressArdmoreString);
            Browser.Wait.ForDomReady();
            TestsBase.CustomerAddressInformation.PhoneField.Click();
            TestsBase.CustomerAddressInformation.ClearAndEnterText(TestsBase.CustomerAddressInformation.ApartmentSuiteOtherField, TestsBase.CustomerAddressInformation.ApartmentArdmoreString);
            TestsBase.CustomerAddressInformation.ClearAndEnterText(TestsBase.CustomerAddressInformation.CityField, TestsBase.CustomerAddressInformation.CityArdmoreString);
            TestsBase.CustomerAddressInformation.ClearAndEnterText(TestsBase.CustomerAddressInformation.StateField, StateCodeListUnitedStates.PA);
            TestsBase.CustomerAddressInformation.ClearAndEnterText(TestsBase.CustomerAddressInformation.ZipPostalCodeField, ZipCodeList.Ardmore);
            TestsBase.CustomerAddressInformation.ClearAndEnterText(TestsBase.CustomerAddressInformation.EmailField, TestsBase.CustomerAddressInformation.Address.Email);
            TestsBase.CustomerAddressInformation.ClearAndEnterText(TestsBase.CustomerAddressInformation.PhoneField, TestsBase.CustomerAddressInformation.Address.Phone);
            TestsBase.Shipping.ProceedToPaymentButton.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(TestsBase.GlobalLocators.LpModalId.ToCssIdSelector()));
            Browser.Wait.IsVisibleElement(By.ClassName(TestsBase.Shipping.FedExAddressValidationClass));
        }

        public override bool IsPlaSkuAddedToCart(string url, string sku)
        {
            Browser.Navigate($"{url}?sfp={sku}");
            if (TestsBase.SortPla.PlaFrameElement != null)
            {
                Browser.SwitchFocusToIframe(TestsBase.SortPla.PlaFrameElement);

                TestsBase.GlobalLocators.PlaAddToCartElement.Click();
                Browser.Wait.IsVisibleElement(By.CssSelector(TestsBase.CartOverview.CheckOutNowClass.ToCssClassSelector()));
                if (TestsBase.CartOverview.CartOverviewElement != null && (string.CompareOrdinal(sku.ToLower(), TestsBase.CartOverview.ProductSkuCart.ToLower()) == 0)) { return true; }
            }

            return false;
        }

        public override void WaitForNavigation(int index)
        {
                Browser.Wait.ForDomReady();

                Browser.Wait.ForElementToStopAnimating(TestsBase.Sort.DisplayedProductAtIndex(index));
        }
    }
}
