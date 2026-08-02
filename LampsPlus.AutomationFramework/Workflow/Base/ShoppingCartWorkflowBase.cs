using System.Collections.Generic;
using System.Web.UI;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Pages;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OperatingSystem = LampsPlus.AutomationFramework.Utilities.TestConfiguration.OperatingSystem;

namespace LampsPlus.AutomationFramework.Workflow.Base
{
    /// <summary>
    /// Common behavior for the Shopping Cart workflow.
    /// </summary>
    public abstract class ShoppingCartWorkflowBase : CheckoutWorkflowBase, IShoppingCartWorkflow
    {
        protected ShoppingCartWorkflowBase(ICartOverview cartOverview, TestsBase testsBase) : base(testsBase)
        {
            CartOverview = cartOverview;
            Framework = testsBase;
        }

        internal TestsBase Framework;

        protected ICartOverview CartOverview { get; }

        /// <inheritdoc />
        public abstract void EmptyCart();


        /// <inheritdoc />
        public abstract Address CreateNewSavedAddress(UserRole userRole, Address address = null, bool goBackToShippingPage = false);

        /// <inheritdoc />
        public void AddSingleItemToCart() { AddSingleItemToCart(Urls.ContemporaryFloorLampsSortPageUrl); }

        /// <inheritdoc />
        public void AddSingleItemToCart(string url)
        {
            AddMultipleItemsToCart(url, 1);
        }

        public void AddMultipleSkuWithPriceOverTwoHundredDollarsToCart(int numberOfProducts)
        {
            for (int i = 0; i < numberOfProducts; i++)
            {
                string shortSku = TestsBase.ProductActions.GetSkuGreaterThanTwoHundredDollars;
                AddItemToCartBySku(new ProductModel(shortSku));
                Browser.Wait.ForClickableElement(TestsBase.CartOverview.CheckOutNowButton);
            }
        }

        /// <inheritdoc />
        public void CheckoutWithSingleItem(string shortSku = "")
        {
            if (shortSku == string.Empty)
            {
                AddSingleItemToCart();
            }
            else
            {
                AddItemToCartBySku(new ProductModel { Sku = shortSku });
            }

            if (TestsBase.UserRole == UserRole.SIS_ESI || TestsBase.UserRole == UserRole.SNIS_ESI || TestsBase.UserRole == UserRole.SIS_ESI_CIC || TestsBase.UserRole == UserRole.SNIS_ESI_CIC)
            {
                Browser.Wait.IsVisibleElement(By.XPath(TestsBase.CsrBlock.SaleSourceXpath));
                Browser.Locate.ClickDropdownByValue(TestsBase.CsrBlock.SaleSourceField, "1");
            }

            Browser.Wait.IsVisibleElement(By.CssSelector(CartOverview.CheckOutNowClass.ToCssClassSelector()));
            TestsBase.CartOverview.CheckOutNowButton.Click();
        }

        /// <inheritdoc />
        public void ProceedToPaymentWithSingleProduct(string shortSku = "")
        {
            CheckoutWithSingleItem(shortSku);
            Browser.Wait.IsVisibleElement(By.CssSelector(TestsBase.Shipping.ProceedPaymentId.ToCssIdSelector()));
            EnterDefaultShippingAddress();
            ProceedToPayment();
        }

        /// <inheritdoc />
        public void EmployeePlaceOrderViaCheck()
        {
            TestsBase.Payment.CheckRadio.Click();
            Browser.Wait.ForDomReady();
            TestsBase.Payment.CheckNumberField.Clear();
            TestsBase.Payment.CheckNumberField.SendKeys("1111");
            TestsBase.Payment.PlaceOrderButton.Click();
        }

        /// <inheritdoc />
        public void EmployeePlaceOrderViaPo()
        {
            TestsBase.Payment.PurchaseOrderRadioButton.Click();
            TestsBase.Payment.PurchaseOrderNumberField.SendKeys("1234");
            TestsBase.Payment.PlaceOrderButton.Click();
        }

        /// <inheritdoc />
        public void EmployeePlaceOrderWithDefaultAddressViaWireTransfer()
        {
            TestsBase.Payment.WireTransferRadio.Click();
            TestsBase.CustomerAddressInformation.EnterWireTransferBillingAddress(new Address());
            TestsBase.Payment.PlaceOrderButton.Click();
            Browser.Wait.ForPage(Urls.OrderConfirmationPageUrl, 15);
        }

    private void ClickAddToCartIpad()
        {
            Browser.ScrollIntoView(GlobalLocators.AddToCartButton);
            Browser.ScrollToByPixelsVertical("-70");
            var xElementCoordinate = 0;
            var yElementCoordinate = 0;
            var zoomFactor = 105;
            Browser.GetElementCoordinates(GlobalLocators.AddToCartButton, ref xElementCoordinate, ref yElementCoordinate, zoomFactor);
            Browser.ClickWithTapByCoordinates(xElementCoordinate, yElementCoordinate);
        }

        public void AddItemsToCartBySku(List<ProductModel> cartProductAddItems)
        {
            TestsBase.ProductDetail.NavigateToProductDetailByShortSku(cartProductAddItems[0].Sku);
            Browser.Wait.IsVisibleElement(By.CssSelector(TestsBase.GlobalLocators.PdAddToCartId.ToCssIdSelector()),60);

            if (TestsBase.OperatingSystem == OperatingSystem.iPad)
            {
                if (!TestsBase.Settings.SettingsTestName.Contains("T7369_iPad_VerifyLayoutOfShippingPageErrorValidation"))
                {
                    ClickAddToCartIpad();
                }
                else
                {
                    Browser.ExecuteJs("arguments[0].click()", GlobalLocators.AddToCartButton.InternalElement);
                }
            }
            else
            {
                Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));
                Browser.ScrollIntoView(GlobalLocators.AddToCartButton);
                Browser.Wait.ForDomReady();
                Browser.ClickByJs(GlobalLocators.AddToCartButton);
            }
            
            Browser.Wait.IsVisibleElement(By.CssSelector(CartOverview.CheckOutNowClass.ToCssClassSelector()), 30);
        }

        /// <inheritdoc />
        public void AddItemToCartBySku(ProductModel cartProductAddItem)
        {
            var cartProductAddItems = new List<ProductModel> { cartProductAddItem };
            AddItemsToCartBySku(cartProductAddItems);
        }

        /// <inheritdoc />
        public void AddItemToCartBySearchedSkuAndCheckOut(string searchedSku)
        {
            TestsBase.ProductDetail.NavigateToProductDetailByShortSku(searchedSku);
            TestsBase.GlobalLocators.AddToCartButton.Click();

            TestsBase.CsrBlock.SelectSaleSource(Sources.CartSources.SalesPhone);

            TestsBase.CartOverview.CheckOutNowButton.Click();
        }

        /// <inheritdoc />
        public void EnterCartZipCodeForShipping(string countryCode, string zipCode, string shippingType = null, bool clickUpdateButton = true)
        {
            TestsBase.CartOverview.ChangeShippingOptionsLink.Click();

            GlobalLocators.ClickDropdownByValue(TestsBase.CartOverview.ShippingCountryDropdown, countryCode);

            TestsBase.CartOverview.ShipZipField.Clear();
            TestsBase.CartOverview.ShipZipField.SendKeys(zipCode);
            TestsBase.CartOverview.ShipZipApplyBtn.Click();

            Browser.Wait.ForElementToStopAnimating(CartOverview.DeliveryOptionsContainer);
            // select a specific shipping type if specified
            if (!string.IsNullOrWhiteSpace(shippingType))
            {
                var radios = TestsBase.CartOverview.ShippingTypeRadios;
                foreach (var radio in radios)
                {
                    if (radio.GetAttribute(HtmlTextWriterAttribute.Value.ToString()) == shippingType)
                    {
                        radio.Click();
                        break;
                    }
                }
            }

            if (clickUpdateButton)
            {
                TestsBase.CartOverview.UpdateShipButton.Click();
            }
        }
        
        /// <inheritdoc />
        public void EnterDefaultShippingAddress(UserRole userRole = default)
        {
            TestsBase.CustomerAddressInformation.EnterShippingAddress(new Address(), userRole);
        }

        /// <inheritdoc />
        public Address CreateNewSavedAddressFromModal(Address address = null, string shippingNameSuffix = "FromAutomation")
        {
            var shippingAddress = address ?? new Address(shippingNameSuffix);
            shippingAddress.SaveToProfile = true;

            TestsBase.CustomerAddressInformation.EnterShippingAddress(shippingAddress, isIntAddress:false, isMultiAddress:true);

            TestsBase.CustomerAddressInformation.SaveAddressFromModalButton.Click();

            Browser.Wait.ForDomReady();

            return shippingAddress;
        }

        /// <inheritdoc />
        public void ApplyCartItemDiscount(int cartItemIndex, decimal percentDiscount)
        {
            TestsBase.CartOverview.CartEditPriceElement.Click();

            Browser.Wait.ForClickableElement(TestsBase.CartOverview.TextPercentDiscountField);

            var discountDropdown = TestsBase.CartOverview.DiscountDropdown.InternalElement;
            new SelectElement(discountDropdown).SelectByIndex(1);

            var discountPercentTextBox = TestsBase.CartOverview.DiscountPercentTextBox;
            Browser.Wait.ForDisplayedElement(discountPercentTextBox, 15).Clear();
            discountPercentTextBox.SendKeys(percentDiscount.ToString());

            Browser.DispatchChangeEvent(TestsBase.CartOverview.TextPercentDiscountField);

            TestsBase.CartOverview.ApplyDiscountButton.Click();
        }

	    /// <inheritdoc />
		public abstract void CloseShippingOptions();

        /// <inheritdoc />
        public void AddMultipleItemsToCart(string url, int numberOfProducts)
        {
            var index = 0;
            while (numberOfProducts > 0)
            {
                TestsBase.Sort.Navigate(url);

                WaitForNavigation(index);

                Browser.Wait.ForElementToStopAnimating(TestsBase.Sort.DisplayedProductAtIndex(index));

                if (TestsBase.Sort.DisplayedProductAtIndex(index).GetAttribute("title").Contains("Colorful Table Lamps"))
                {
                    Browser.Wait.ForClickableElement(TestsBase.Sort.DisplayedProductAtIndex(index + 1));
                    TestsBase.Sort.DisplayedProductAtIndex(index + 1).Click();
                }
                else
                {
                    Browser.Wait.ForClickableElement(TestsBase.Sort.DisplayedProductAtIndex(index));
                    TestsBase.Sort.DisplayedProductAtIndex(index).Click();
                }

                numberOfProducts--;
                Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));
                Browser.ScrollIntoView(GlobalLocators.AddToCartButton);
                Browser.Wait.ForDomReady();
                GlobalLocators.AddToCartButton.Click();
                Browser.Wait.IsVisibleElement(By.CssSelector(CartOverview.CheckOutNowClass.ToCssClassSelector()));

                index++;
            }
        }

        /// <inheritdoc />
        public abstract void ProceedToPayment();

        public abstract void WaitForNavigation(int index);

        public abstract void ShowFedExValidationModal();

        /// <inheritdoc />
        public abstract bool IsPlaSkuAddedToCart(string url, string sku);
    }
}
