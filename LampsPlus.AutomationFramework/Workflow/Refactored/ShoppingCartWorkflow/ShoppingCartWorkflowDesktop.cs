using System;
using System.Collections.Generic;
using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Utilities;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Databases.Actions;
using LampsPlus.AutomationFramework.Pages.Refactored.Address;
using LampsPlus.AutomationFramework.Pages.Refactored.Cart;
using LampsPlus.AutomationFramework.Pages.Refactored.CsrBlock;
using LampsPlus.AutomationFramework.Pages.Refactored.CustomerAddressInformation;
using LampsPlus.AutomationFramework.Pages.Refactored.HeaderFooter;
using LampsPlus.AutomationFramework.Pages.Refactored.Home;
using LampsPlus.AutomationFramework.Pages.Refactored.Modal;
using LampsPlus.AutomationFramework.Pages.Refactored.OrderSummaryBlock;
using LampsPlus.AutomationFramework.Pages.Refactored.Payment;
using LampsPlus.AutomationFramework.Pages.Refactored.ProductDetail;
using LampsPlus.AutomationFramework.Pages.Refactored.Shipping;
using LampsPlus.AutomationFramework.Pages.Refactored.Sort;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Payment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using OperatingSystem = LampsPlus.AutomationFramework.Utilities.TestConfiguration.OperatingSystem;

namespace LampsPlus.AutomationFramework.Workflow.Refactored.ShoppingCartWorkflow
{
    public class ShoppingCartWorkflowDesktop : IShoppingCartWorkflowDesktop
    {
        //Class members
        private static DateTime DayOccurrence(DateTime date, DayOfWeek dayOfWeek, int occurrence)
        {
            var start = new DateTime(date.Year, date.Month, 1);
            var first = start.AddDays((7 - ((int)start.DayOfWeek - (int)dayOfWeek)) % 7);
            return first.AddDays(7 * (occurrence - 1));
        }

        private void WaitForGlobalSpinnerToClose() { _browser.Wait.ForElement(_browser.Locate.ElementBySelector(HtmlTextWriterTag.Body.ToTagNotClassSelector("loading"))); }

        public ShoppingCartWorkflowDesktop(IBrowser browser, IHomeDesktop home, IAssert assert, ProductActions productActions, IOrderSummaryBlockDesktop orderSummaryBlockDesktop, IProductDetailDesktop productDetail, 
            IModalDesktop modal, IShippingDesktop shipping, ISortDesktop sort, ICartDesktop cart, OperatingSystem operatingSystem, ICustomerAddressInformationDesktop customerAddressInformation, IPaymentDesktop payment, IAddress address, ICsrBlockDesktop csrBlock, UserRole userRole, IHeaderFooterDesktop headerFooter)
        {
            _browser = browser;
            _home = home;
            _cart = cart;
            _orderSummaryBlock = orderSummaryBlockDesktop;
            _productDetail = productDetail;
            _shipping = shipping;
            _sort = sort;
            _assert = assert;
            _operatingSystem = operatingSystem;
            _modal = modal;
            _customerAddressInformation = customerAddressInformation;
            _payment = payment;
            _address = address;
            _csrBlock = csrBlock;
            _productActions = productActions;
            _userRole = userRole;
            _headerFooter = headerFooter;
        }

        //Desktop POM and Workflow instances
        private readonly ICartDesktop _cart;
        private readonly IHomeDesktop _home;
        private readonly IOrderSummaryBlockDesktop _orderSummaryBlock;
        private readonly IProductDetailDesktop _productDetail;
        private readonly IShippingDesktop _shipping;
        private readonly ISortDesktop _sort;
        private readonly IModalDesktop _modal;
        private readonly ICustomerAddressInformationDesktop _customerAddressInformation;
        private readonly IPaymentDesktop _payment;
        private readonly ICsrBlockDesktop _csrBlock;
        private readonly IHeaderFooterDesktop _headerFooter;

        //TestsBase instances
        private readonly IAddress _address;
        private readonly IAssert _assert;
        private readonly IBrowser _browser;
        private readonly OperatingSystem _operatingSystem;
        private readonly ProductActions _productActions;
        private readonly UserRole _userRole;

        //Interface implementation
        public void AddItemsToCartBySku(ProductModel cartProductAddItems)
        {
            var cartProductAddItem = new List<ProductModel> { cartProductAddItems };

            _productDetail.NavigateToProductDetailByShortSku(cartProductAddItem[0].Sku);
            _assert.True(_productDetail.IsCurrentPage, "User is not on PDP.");

            if (_operatingSystem == OperatingSystem.iPad)
            {
                _productDetail.AddToCartIpad();
            }
            else
            {
                _productDetail.AddToCart();
            }

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

                    if (_operatingSystem == OperatingSystem.iPad)
                    {
                        _productDetail.AddToCartIpad();
                    }
                    else
                    {
                        _productDetail.AddToCart();
                    }

                    _assert.True(_cart.IsCurrentPage, "User is not on Cart Overview page.");
                }
            }
        }

        public void AddMultipleSkuWithPriceOverTwoHundredDollarsToCart(int numberOfProducts)
        {
            for (int i = 0; i < numberOfProducts; i++)
            {
                var shortSku = _productActions.GetSkuGreaterThanTwoHundredDollars;
                AddItemsToCartBySku(new ProductModel { Sku = shortSku });
            }
        }

        public virtual void ShowFedExValidationModal(bool enterApartment = true, Address address = null)
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

            _customerAddressInformation.FillFormControlByText(_customerAddressInformation.ShippingElementsCollection["FirstNameField"], addressCustom.FirstName);
            _customerAddressInformation.FillFormControlByText(_customerAddressInformation.ShippingElementsCollection["LastNameField"], addressCustom.LastName);

            _customerAddressInformation.AddAnotherAddressField();

            _customerAddressInformation.FillFormControlByText(_customerAddressInformation.ShippingElementsCollection["StreetAddressField"], addressCustom.AddressLine1);
            
            if (enterApartment)
            {
                _customerAddressInformation.FillFormControlByText(_customerAddressInformation.ShippingElementsCollection["ApartmentSuiteOtherField"], addressCustom.AddressLine2);
            }

            _customerAddressInformation.FillFormControlByText(_customerAddressInformation.ShippingElementsCollection["CityField"], addressCustom.City);
            _customerAddressInformation.FillFormControlByText(_customerAddressInformation.ShippingElementsCollection["StateField"], StateCodeListUnitedStates.CA);//state selected
            _customerAddressInformation.FillFormControlByText(_customerAddressInformation.ShippingElementsCollection["ZipPostalCodeField"], addressCustom.ZipCode);
            _customerAddressInformation.FillFormControlByText(_customerAddressInformation.ShippingElementsCollection["EmailField"], addressCustom.Email);
            _customerAddressInformation.FillFormControlByText(_customerAddressInformation.ShippingElementsCollection["PhoneField"], addressCustom.Phone);

            //Act: Proceed to payment
            _shipping.ProceedToPayment();

            //Assert: FedEx modal is opened
            _assert.True(_shipping.DoesFedExModalShow(), "Fed Ex address validation modal is not displayed");
            _customerAddressInformation.WaitForFedExModalToStopAnimating();
        }

        public Address CreateNewSavedAddressFromModal(Address address = null, string shippingNameSuffix = "FromAutomation", int newAddressButtonIndex = 0)
        {
            var shippingAddress = address ?? new Address(shippingNameSuffix);
            shippingAddress.SaveToProfile = true;

            _shipping.OpenNewAddressByIndex(newAddressButtonIndex);

            _customerAddressInformation.EnterShippingAddress(shippingAddress, isIntAddress:false,  isMultiAddress:true);
            
            _customerAddressInformation.SaveAddressFromModal();

            _browser.Wait.ForDomReady();

            return shippingAddress;
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
                WaitForGlobalSpinnerToClose();

                _payment.SelectShippingHeaderLink();
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

        public void ProceedToShippingPage()
        {
            _cart.CheckOut();
            _shipping.WaitForShippingPageToLoad();
        }

        public void EmptyCart()
        {
            var currentUrl = _browser.PageUrl;

            if (_browser.PageUrl != Urls.CartOverviewPageUrl)
            {
                if (!currentUrl.Contains(Urls.HomePageUrl) || currentUrl.Contains("denv.aspx"))
                {
                    _browser.Navigate(Urls.HomePageUrl);
                    _browser.Wait.ForDomReady(60);
                } // If the current page is not a LampsPlus page navigate home.

                if (_browser.Locate.DoesElementExistImmediately(_home.InvisibleClass.ToCssClassSelector()))
                {
                    if (_headerFooter.GetCartCountInHeader() == "")
                    {
                        return;
                    }
                }

                _browser.Navigate(Urls.CartOverviewPageUrl);
            }

            if (_browser.Locate.DoesElementExistImmediately(_modal.LpModalId.ToCssIdSelector())) { _modal.CloseLpModal(); }

            _cart.RemoveCartItems();
        }

        public bool WaitForOrderStatusKioskPriceUpdate()
        {
            _cart.CheckPosBox();
            return _orderSummaryBlock.IsOrderSummaryKioskPriceStatusVisible();
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

            if((_userRole == UserRole.SIS_ESI || _userRole == UserRole.SIS_ESI_CIC || _userRole == UserRole.SNIS_ESI || _userRole == UserRole.SNIS_ESI_CIC))
            {
                _csrBlock.SetSaleSourceValue();
            }

            ProceedToShippingPage();

            _customerAddressInformation.EnterShippingAddress(_address);

            ProceedToPayment();
        }

        public void ProceedToPayment()
        {
            _orderSummaryBlock.ClickProceedToPaymentButton();
            _assert.True(_payment.IsCurrentPage, "User is not on Payment page.");

            _browser.Wait.ForCondition(
                () => _browser.Wait.ForPageWait(Urls.PaymentPageUrl) || _browser.Wait.ForPageWait(Urls.ShippingNotificationPageUrl), 30);

            _payment.SelectDeliveryPolicyAgreementIfVisible();
        }

        public void EnableTooltip(IElement element)
        {
            if (_operatingSystem == OperatingSystem.iPad)
            {
                _browser.ScrollIntoView(element);
                _browser.ScrollToByPixelsVertical("-70");
                var xElementCoordinate = 0;
                var yElementCoordinate = 0;
                _browser.GetElementCoordinates(element, ref xElementCoordinate, ref yElementCoordinate, 100);
                _browser.ClickWithTapByCoordinates(xElementCoordinate, yElementCoordinate);
            }
            else
            {
                _browser.MouseOverOnElement(element);
            }
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

        public void LargeImageOnPrintModal()
        {
            _cart.OpenPrintModal();
            _modal.PrintModal();
            _cart.SelectLargeImagesOption();
        }

        public bool IsShippingTypeAvailable(string countryCode, string zipCode, string shippingType)
        {
            OpenShippingOptions(countryCode, zipCode);
            _cart.ApplyZipCode(zipCode);

            var radios = _cart.GetShippingTypeRadios();
            foreach (var radio in radios)
            {
                if (radio.GetAttribute(HtmlTextWriterAttribute.Value.ToString()) == shippingType)
                {
                    return true;
                }
            }
            return false;
        }

        public void OpenShippingOptions(string countryCode, string zipCode)
        {
            _browser.Wait.ForDomReady();
            _cart.OpenShippingOptions();
            _customerAddressInformation.SelectCountry(countryCode);
            _cart.ApplyZipCode(zipCode);
        }

 
        public DateTime AddBusinessDaysForStandardShipping(DateTime inputDate, int numOfDays)
        {
            const int afterNoon = 14;
            var outputDate = inputDate;

            if (DateTime.Now.Hour >= afterNoon)
                numOfDays++;

            while (numOfDays > 0)
            {
                outputDate = outputDate.AddDays(1);
                numOfDays--;
            }

            return outputDate;
        }

        public List<DateTime> GetUspsHolidays(int year)
        {
            var holidays = new List<DateTime>();

            //NEW YEARS - January 1st
            holidays.Add(new DateTime(year, 1, 1));

            //MLK - 3rd Monday of January
            holidays.Add(DayOccurrence(new DateTime(year, 1, 1), DayOfWeek.Monday, 3));

            //WASHINGTON'S BIRTHDAY - 3rd Monday in February
            holidays.Add(DayOccurrence(new DateTime(year, 2, 1), DayOfWeek.Monday, 3));

            //MEMORIAL DAY  -- last monday in May
            var memorialDay = new DateTime(year, 5, 31);
            while (memorialDay.DayOfWeek != DayOfWeek.Monday)
            {
                memorialDay = memorialDay.AddDays(-1);
            }
            holidays.Add(memorialDay);

            //INDEPENCENCE DAY - 4th of July
            holidays.Add(new DateTime(year, 7, 4));

            //LABOR DAY -- 1st Monday in September 
            holidays.Add(DayOccurrence(new DateTime(year, 9, 1), DayOfWeek.Monday, 1));

            //COLUMBUS DAY - 2nd Monday in October
            holidays.Add(DayOccurrence(new DateTime(year, 10, 1), DayOfWeek.Monday, 2));

            //VETERANS DAY - 11th of November
            holidays.Add(new DateTime(year, 11, 11));

            //THANKSGIVING DAY - 4th Thursday in November 
            holidays.Add(DayOccurrence(new DateTime(year, 11, 1), DayOfWeek.Thursday, 4));

            //CHRISTMAS DAY - 25th of December
            holidays.Add(new DateTime(year, 12, 25));

            return holidays;
        }

        public DateTime AddBusinessDays(DateTime inputDate, int numOfDays)
        {
            const int afterNoon = 14;
            var outputDate = inputDate;
            var listOfHolidays = GetUspsHolidays(DateTime.Now.Year);

            if (DateTime.Now.Hour >= afterNoon)
                numOfDays++;

            while (numOfDays > 0)
            {
                outputDate = outputDate.AddDays(1);
                if (outputDate.DayOfWeek != DayOfWeek.Saturday
                    && outputDate.DayOfWeek != DayOfWeek.Sunday
                    && !listOfHolidays.Contains(outputDate))
                    numOfDays--;
            }

            return outputDate;
        }

        public void EmployeeProceedToPaymentPageWithSingleItem(string shortSku = "")
        {
            EmployeeProceedToShippingPageWithSingleItem(shortSku);
            _assert.True(_shipping.IsCurrentPage, "User is not on the Shipping page.");
            _customerAddressInformation.EnterShippingAddress(_address);
            ProceedToPayment();
        }

        public void EmployeeProceedToShippingPageWithSingleItem(string shortSku = "")
        {
            if (string.IsNullOrEmpty(shortSku))
            {
                shortSku = _productActions.GetLessThanTenDollarItem;
                _assert.DatabaseObject(shortSku, "ProductActions.GetLessThanTenDollarItem()");
            }
            else
            {
                _assert.DatabaseObject(shortSku, "ProductActions.GetLessThanTenDollarItem()");
            }

            _productDetail.AddSingleProductToCart(shortSku);
            _assert.True(_cart.IsCurrentPage, "User is not on the Cart Overview page.");
            _csrBlock.SetSaleSourceValue();
            _cart.RemovePromoCode(); // In case a promo code has not been removed.
            _cart.CheckOut();
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
    }
}