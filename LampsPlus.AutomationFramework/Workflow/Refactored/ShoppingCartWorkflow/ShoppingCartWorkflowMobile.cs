using System;
using System.Collections.Generic;
using Automation.Framework;
using Automation.Framework.Core;
using Automation.Framework.Utilities;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Databases.Actions;
using LampsPlus.AutomationFramework.Pages.Refactored.Address;
using LampsPlus.AutomationFramework.Pages.Refactored.Cart;
using LampsPlus.AutomationFramework.Pages.Refactored.CsrBlock;
using LampsPlus.AutomationFramework.Pages.Refactored.CustomerAddressInformation;
using LampsPlus.AutomationFramework.Pages.Refactored.OrderSummaryBlock;
using LampsPlus.AutomationFramework.Pages.Refactored.Payment;
using LampsPlus.AutomationFramework.Pages.Refactored.ProductDetail;
using LampsPlus.AutomationFramework.Pages.Refactored.Shipping;
using LampsPlus.AutomationFramework.Pages.Refactored.Sort;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Payment;
using OperatingSystem = LampsPlus.AutomationFramework.Utilities.TestConfiguration.OperatingSystem;

namespace LampsPlus.AutomationFramework.Workflow.Refactored.ShoppingCartWorkflow
{
    public class ShoppingCartWorkflowMobile : IShoppingCartWorkflowMobile
    {
        public ShoppingCartWorkflowMobile(IBrowser browser, IAssert assert, IProductDetailMobile productDetail, ICartMobile cart, 
            ICustomerAddressInformationMobile customerAddressInformation, IShippingMobile shipping, IPaymentMobile payment, 
            IOrderSummaryBlockMobile orderSummaryBlock, IAddress address, ICsrBlockMobile csrBlock, ISortMobile sort, OperatingSystem operatingSystem, ProductActions productActions)
        {
            _browser = browser;
            _assert = assert;
            _productDetail = productDetail;
            _cart = cart;
            _customerAddressInformation = customerAddressInformation;
            _shipping = shipping;
            _payment = payment;
            _orderSummaryBlock = orderSummaryBlock;
            _address = address;
            _csrBlock = csrBlock;
            _sort = sort;
            _operatingSystem = operatingSystem;
            _productActions = productActions;
        }

        //Mobile POM and Workflow instances
        private readonly ICartMobile _cart;
        private readonly ICustomerAddressInformationMobile _customerAddressInformation;
        private readonly IShippingMobile _shipping;
        private readonly IPaymentMobile _payment;
        private readonly IOrderSummaryBlockMobile _orderSummaryBlock;
        private readonly IProductDetailMobile _productDetail;
        private readonly ICsrBlockMobile _csrBlock;
        private readonly ISortMobile _sort;

        //TestsBase instances
        private readonly IAddress _address;
        private readonly IAssert _assert;
        private readonly IBrowser _browser;
        private readonly OperatingSystem _operatingSystem;
        private readonly ProductActions _productActions;

        //Interface implementation
        public void AddItemsToCartBySku(ProductModel cartProductAddItems)
        {
            var cartProductAddItem = new List<ProductModel> { cartProductAddItems };

            _productDetail.NavigateToProductDetailByShortSku(cartProductAddItem[0].Sku);
            _assert.True(_productDetail.IsCurrentPage, "User is not on PDP.");

            _productDetail.AddToCart();
            
            _assert.True(_cart.IsCurrentPage, "User is not on Cart Overview page.");
        }

        public void AddMultipleItemsToCart(string url = null, int numberOfProducts = 0, IList<string> listOfSkus = null)
        {
            if (url != string.Empty && numberOfProducts > 0)
            {
                var index = 0;
                while (numberOfProducts > 0)
                {
                    _browser.Navigate(url);
                    _assert.True(_sort.IsCurrentPage, "User is not on a Sort page.");

                    _sort.SelectSortPageSkuByIndex(index);

                    numberOfProducts--;
                    _assert.True(_productDetail.IsCurrentPage, "User is not on Product Detail page.");
                    _productDetail.AddToCart();
                    _assert.True(_cart.IsCurrentPage, "User is not on Cart Overview page.");

                    index++;
                }
            }
            else
            {
                foreach (var sku in listOfSkus)
                {
                    _productDetail.NavigateToProductDetailByShortSku(sku);
                    _assert.True(_productDetail.IsCurrentPage, "User is not on PDP.");

                    _productDetail.AddToCart();

                    _assert.True(_cart.IsCurrentPage, "User is not on Cart Overview page.");
                }
            }
        }

        public void ShowFedExValidationModal(bool enterApartment = true, Address address = null)
        {
            var addressCustom = address ?? new Address
            {
                FirstName = "Test",
                LastName = "Test",
                AddressLine1 = "20250 Plummer",
                AddressLine2 = "1",
                City = "Chatsworth",
                ZipCode = "91311"
            };

            _customerAddressInformation.AddAnotherAddressField();

            _customerAddressInformation.FillFormControlByText(_customerAddressInformation.ShippingElementsCollection["FirstNameField"], addressCustom.FirstName);
            _customerAddressInformation.FillFormControlByText(_customerAddressInformation.ShippingElementsCollection["LastNameField"], addressCustom.LastName);

            _customerAddressInformation.FillFormControlByText(_customerAddressInformation.ShippingElementsCollection["StreetAddressField"], addressCustom.AddressLine1);

            if (enterApartment)
            {
                _customerAddressInformation.FillFormControlByText(_customerAddressInformation.ShippingElementsCollection["ApartmentSuiteOtherField"], addressCustom.AddressLine2);
            }

            _customerAddressInformation.FillFormControlByText(_customerAddressInformation.ShippingElementsCollection["CityField"], addressCustom.City);
            _customerAddressInformation.FillFormControlByText(_customerAddressInformation.ShippingElementsCollection["ZipPostalCodeField"], addressCustom.ZipCode);
            _customerAddressInformation.FillFormControlByText(_customerAddressInformation.ShippingElementsCollection["PhoneField"], addressCustom.Phone);
            _customerAddressInformation.FillFormControlByText(_customerAddressInformation.ShippingElementsCollection["EmailField"], addressCustom.Email);
            _customerAddressInformation.FillFormControlByText(_customerAddressInformation.ShippingElementsCollection["StateField"], StateCodeListUnitedStates.CA);//state selected
            _customerAddressInformation.ShippingElementsCollection["EmailField"].Click();

            //Act: Proceed to payment
            _browser.ScrollIntoView(_customerAddressInformation.ShippingElementsCollection["StateField"]);
            _shipping.ProceedToPayment();

            //Assert: FedEx modal is opened
            _assert.True(_shipping.DoesFedExModalShow(), "Fed Ex address validation modal is not displayed");
            _customerAddressInformation.WaitForFedExModalToStopAnimating();
        }

        public Address CreateNewSavedAddress(Address address = null, bool goBackToShippingPage = false)
        {
            _shipping.Navigate();

            var shippingAddress = address ?? new Address();
            shippingAddress.SaveToProfile = true;

            _customerAddressInformation.EnterShippingAddress(shippingAddress);
            _shipping.ProceedToPayment();
            _assert.True(_payment.IsCurrentPage, "User is not on Payment page.");


            if (goBackToShippingPage)
            {
                _shipping.Navigate();
            }

            return shippingAddress;
        }

        public List<string> SelectRandomQuantityAndAddToCart(int totalItems)
        {
            var quantityList = new List<string>();
            while (totalItems > 0)
            {
                _productDetail.NavigateToProductDetailByShortSku(_productActions.GetSkuThatHasQuantityGreaterThanTwenty);

                var quantity = MathHelper.GetRandomNumber(20).ToString();

                _productDetail.ChangeProductQuantity(quantity);
                _productDetail.AddToCart();

                _assert.True(_cart.IsCurrentPage, "User is not on Cart Page");
                quantityList.Add(quantity);

                totalItems--;
            }
            return quantityList;
        }

        public void AddMultipleSkuWithPriceOverTwoHundredDollarsToCart(int numberOfProducts)
        {
            for (int i = 0; i < numberOfProducts; i++)
            {
                var shortSku = _productActions.GetSkuGreaterThanTwoHundredDollars;
                AddItemsToCartBySku(new ProductModel { Sku = shortSku });
            }
        }

        public void EmptyCart()
        {
            var currentUrl = _browser.PageUrl;

            if (_browser.PageUrl != Urls.CartOverviewPageUrl)
            {
                if (!currentUrl.Contains(Urls.HomePageUrl) || currentUrl.Contains("denv.aspx?j=1"))
                {
                    _browser.Navigate(Urls.HomePageUrl);
                } // If the current page is not a LampsPlus page navigate home.

                if (Convert.ToInt32(_browser.ExecuteJs("return lp.globals.cartCount")) == 0)
                {
                    return;
                }

                _browser.Navigate(Urls.CartOverviewPageUrl);
                _assert.True(_cart.IsCurrentPage, "User is not on Cart Overview page.");
            }

            _cart.RemoveCartItems();
        }

        public void ProceedToShippingPage()
        {
            _cart.CheckOut();
            _shipping.WaitForShippingPageToLoad();
        }

        public void ProceedToPayment()
        {
            _orderSummaryBlock.ClickProceedToPaymentButton();
            _assert.True(_payment.IsCurrentPage, "User is not on Payment page.");

            _browser.Wait.ForCondition(
                () => _browser.Wait.ForPageWait(Urls.PaymentPageUrl) || _browser.Wait.ForPageWait(Urls.ShippingNotificationPageUrl), 30);

            _payment.SelectDeliveryPolicyAgreementIfVisible();
        }

        public void ProceedToPaymentWithSingleProduct(string shortSku = "")
        {
            if (shortSku == string.Empty)
            {
                AddMultipleItemsToCart(Urls.ContemporaryFloorLampsSortPageUrl, 1);
            }
            else
            {

                AddItemsToCartBySku(new ProductModel(shortSku));
            }

            if (_csrBlock.IsSaleSourceFieldDisplayed)
            {
                _csrBlock.SetSaleSourceValue();
            }

            ProceedToShippingPage();

            _customerAddressInformation.EnterShippingAddress(_address);

            ProceedToPayment();
        }

        public int GetShippingTypeOptions()
        {
            _cart.OpenShippingOptions();

            _customerAddressInformation.SelectCountry(CountryCodeList.US);
            _customerAddressInformation.ChangeShippingZip(_address);

            int numOfShippingTypeOptions = _cart.GetNumberOfShippingOptions();
            _cart.ShippingUpdate();

            _browser.Wait.ForPageWait(Urls.CartOverviewPageUrl);//Added to confirm page navigation 
            _browser.RefreshPage();//Added to avoid stale exception for zip

            return numOfShippingTypeOptions;
        }

        public void LoopThroughAndVerifyShippingOptions(int numOfShippingOptions)
        {
            var productTotal = _cart.GetOrderSummaryValuesByIndex(0);
            var orderTax = _cart.GetOrderSummaryValuesByIndex(2);

            var orderTotalNoShipping = productTotal + orderTax;

            for (var i = 0; i < numOfShippingOptions; i++)
            {
                var shippingCost = _cart.EnterCartZipCodeForShippingOption(CountryCodeList.US, _address.ZipCode, i);
                _browser.Wait.ForDomReady(2);
                var newShippingOrderTotal = orderTotalNoShipping + shippingCost;
                var orderTotal = _cart.GetOrderSummaryValuesByIndex(3);
                _assert.Equals(orderTotal, newShippingOrderTotal, "Order total does not match.");
            }
        }

        public void UpdateShippingStateFromPaymentPage(string state)
        {
            NavigateBackToShippingPageFromPaymentPage();
            _orderSummaryBlock.IsProceedToPaymentButtonVisible();

            _customerAddressInformation.SelectState(state);

            ProceedToPayment();
        }

        public void NavigateBackToShippingPageFromPaymentPage()
        {
            _payment.SelectShippingHeaderLink();
            _assert.True(_shipping.IsCurrentPage, "User is not on the Shipping page.");
        }

        public void GoToOrderConfirmationFromCartUsingCc()
        {
            _cart.CheckOut();
            _assert.True(_shipping.IsCurrentPage, "Current Page is not an Shipping Page");

            _shipping.ProceedToPayment();
            _assert.True(_payment.IsCurrentPage, "Current Page is not an Payment Page");

            var testCreditCard = CreditCards.TestVisaCard;
            _payment.EnterCreditCartInformation(testCreditCard);
            _payment.PlaceOrder();
        }

        public bool VerifyAddress(Address address, IElement addressElement)
        {
            var stringSeparators = new string[] { "\r\n" };
            var addressLines = addressElement.Text.Split(stringSeparators, StringSplitOptions.None);
            var fullName = address.FirstName + " " + address.LastName;
            var cityAddress = $"{address.City}, {address.State} {address.ZipCode} {address.Country}";
            return fullName == addressLines[1] && address.AddressLine1 == addressLines[2] && address.AddressLine2 == addressLines[3] && cityAddress == addressLines[4] && address.Phone == addressLines[5];
        }

        public void WaitForTaxLabelToUpdate()
        {
            _browser.Wait.ForCondition(() => _cart.GetTaxAmount() > 0);
        } 
    }
}
