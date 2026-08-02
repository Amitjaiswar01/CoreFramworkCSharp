using System.Threading;

using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.IntegrationTests.Pages.CartOverview
{
    public class CartOverviewLocatorDesktopTests : CartOverviewLocatorTests
    {
        /// <summary>
        /// Tests to ensure this page can find all its IElements.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public CartOverviewLocatorDesktopTests(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the requested elements could be located on the given shopping cart page.
        /// </summary>
        [Trait(LpTraits.Integration.PageObjectModel, "CartOverview")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LocateElementsOnCartOverviewPageTest(string config) => Locate(config);

        protected override void ElementVerification()
        {
            /*********************************************************************************************************/
            /*                                             Anonymous User                                            */
            /*********************************************************************************************************/

            Browser.ClearAllCookies();

            Browser.Navigate(Urls.CartOverviewPageUrl);
            Browser.Wait.ForPage(Urls.CartOverviewPageUrl);

            VerifyElementDisplayed(() => CartOverview.EmptyCartWarningMessageElement);
            VerifyElementDisplayed(() => CartOverview.EmptyCartWarningMessageElementImmediately);

            ShoppingCartWorkflow.AddItemToCartBySku(new ProductModel(ProductActions.GetFurnitureWithUmrpAndInHomeDelivery));

            VerifyElementDisplayed(() => CartOverview.ShippingName);
            VerifyElementDisplayed(() => CartOverview.CartIdElement);
            VerifyElementDisplayed(() => CartOverview.CartOverviewElement);
            VerifyElementDisplayed(() => CartOverview.CartPromotionalButton);
            VerifyElementDisplayed(() => CartOverview.ChangeShippingOptionsLink);
            VerifyElementDisplayed(() => CartOverview.CheckOutNowButton);
            VerifyElementDisplayed(() => CartOverview.CheckOutNowButtons);
            VerifyElementDisplayed(() => CartOverview.EmailButton);
            VerifyElementDisplayed(() => CartOverview.PayPalButton);
            VerifyElementDisplayed(() => CartOverview.PayPalButtonContainer);
            VerifyElementDisplayed(() => CartOverview.ProductItemWrapElements);
            VerifyElementDisplayed(() => CartOverview.ProductListItems);
            VerifyElementDisplayed(() => CartOverview.ProductQuantityField);
            VerifyElementDisplayed(() => CartOverview.ProductSkuLabelCart);
            VerifyElementDisplayed(() => CartOverview.PromoCodeInfo);
            VerifyElementDisplayed(() => CartOverview.RemoveItemLinksElements);
            VerifyElementDisplayed(() => CartOverview.ShippingAndProcessingLabel);
            VerifyElementDisplayed(() => CartOverview.TaxLabel);
            VerifyElementDisplayed(() => CartOverview.ShopWithConfidenceContainer);
            VerifyElementDisplayed(() => CartOverview.CartIdContainer);

            // Email
            Browser.Wait.ForDisplayedElement(CartOverview.EmailButton);
            CartOverview.EmailButton.Click();

            Browser.Wait.ForDisplayedElement(GlobalLocators.IframeModal, 30);

            Browser.SwitchFocusToIframe(GlobalLocators.IframeModal);
            Browser.Wait.ForDisplayedElement(CartOverview.EmailFormContainer, 30);

            VerifyElementDisplayed(() => CartOverview.EmailFormContainer);
            VerifyElementDisplayed(() => CartOverview.FormEmailFromField);
            VerifyElementDisplayed(() => CartOverview.FormEmailRecipientsField);
            VerifyElementDisplayed(() => CartOverview.FormFirstNameField);
            VerifyElementDisplayed(() => CartOverview.FormLastNameField);
            VerifyElementDisplayed(() => CartOverview.FormZipCodeField);
            VerifyElementDisplayed(() => CartOverview.SendEmailButton);

            Browser.SwitchToDefaultContent();
            Browser.Locate.ElementById(GlobalLocators.LpModalCloseId).Click();
            Thread.Sleep(5000);
            // End Email

            // Promo Codes
            CartOverview.CartPromotionalButton.Click();
            Browser.Wait.ForDisplayedElement(CartOverview.PromoCodeFields, 30);

            VerifyElementDisplayed(() => CartOverview.PromoCodeFields);
            VerifyElementDisplayed(() => CartOverview.PromoInputField);

            CartOverview.PromoInputField.SendKeys("1234");
            VerifyElementDisplayed(() => CartOverview.PromoCodeApplyButton);
            CartOverview.PromoCodeApplyButton.Click();
            Browser.Wait.ForDisplayedElement(CartOverview.CouponCodeError, 30);

            VerifyElementDisplayed(() => CartOverview.CouponCodeError);
            VerifyElementDisplayed(() => CartOverview.CouponCodeErrorElement);
            VerifyElementDisplayed(() => CartOverview.PromoCodeLabel);
            VerifyElementDisplayed(() => CartOverview.RemovePromoCodeElement);
            // End Promo Codes

            /*********************************************************************************************************/
            /*                                              Regular CSR                                              */
            /*********************************************************************************************************/

            Browser.ClearAllCookies();
            Browser.Navigate(Urls.HomePageUrl);
            SignInWorkflow.SignIn(LampsPlusAccounts.CustomerServiceRegularLoginAccount);

            ShoppingCartWorkflow.EmptyCart();

            ShoppingCartWorkflow.AddItemToCartBySku(new ProductModel(ProductActions.GetFurnitureWithUmrpAndInHomeDelivery));

            VerifyElementDisplayed(() => CartOverview.AddProfessionalAccountLink);
            VerifyElementDisplayed(() => CartOverview.AddShortSkuElement);
            VerifyElementDisplayed(() => CartOverview.AddSkuContainer);
            VerifyElementDisplayed(() => CartOverview.CartEditPriceElement);
            VerifyElementDisplayed(() => CartOverview.DeleteCartElement);

            CartOverview.PrintLink.Click();

            Browser.Wait.ForElement(OrderConfirmation.LpModalContent, 3);
            Browser.SwitchFocusToIframe(OrderConfirmation.LpModalContent);

            Browser.Wait.ForElement(CartOverview.SelectNoneLink);
            VerifyElementDisplayed(() => CartOverview.SelectNoneLink);

            CartOverview.SelectNoneLink.Click();

            Browser.Wait.ForElement(CartOverview.SelectLargeImage);
            VerifyElementDisplayed(() => CartOverview.SelectLargeImage);
			Browser.Wait.ForElement(CartOverview.SelectSmallImage);
            VerifyElementDisplayed(() => CartOverview.SelectSmallImage);
            VerifyElementDisplayed(() => CartOverview.PrintCartButton);

            GlobalLocators.LpModalCloseElement.Click();

            Browser.SwitchToDefaultContent();

            CartOverview.DeleteCartElement.Click();
            Browser.Wait.ForDisplayedElement(CartOverview.DeleteCartButton, 10);

            VerifyElementDisplayed(() => CartOverview.DeleteCartButton);

            Browser.Locate.ElementById(GlobalLocators.LpModalCloseId).Click();

            VerifyElementDisplayed(() => CartOverview.ToggleMoreDetailsButton);

            // More Details
            Thread.Sleep(2000);
            CartOverview.ToggleMoreDetailsButton.Click();
            Browser.Wait.ForDisplayedElement(CartOverview.MoreDetailsInventoryElement, 30);

            VerifyElementDisplayed(() => CartOverview.MoreDetailsInventoryElement);

            Browser.Wait.ForDisplayedElement(CartOverview.MoreDetailsElement);
            VerifyElementDisplayed(() => CartOverview.MoreDetailsElement);

            CartOverview.ToggleMoreDetailsButton.Click();
            Thread.Sleep(5000);
            // End More Details

            // Add Professional Account
            AddProfessionalAccount();

            CartOverview.AddProfessionalAccountLink.Click();
            Browser.Wait.ForDisplayedElement(CartOverview.CompanyNameField, 60);

            VerifyElementDisplayed(() => CartOverview.CompanyNameField);
            VerifyElementDisplayed(() => CartOverview.CompanySearchButton);

            CartOverview.CompanyNameField.SendKeys("WebDev QA");

            CartOverview.CompanySearchButton.Click();
            Browser.Wait.ForDisplayedElement(CartOverview.CompanyNameLinkTable, 60);

            VerifyElementDisplayed(() => CartOverview.CompanyNameLink);
            VerifyElementDisplayed(() => CartOverview.CompanyNameLinkTable);

            CartOverview.CompanyNameLink.Click();
            Thread.Sleep(10000);
            Browser.Wait.ForDisplayedElement(CartOverview.ProfessionalAccountLabel, 120);

            VerifyElementDisplayed(() => CartOverview.ProfessionalAccountLabel);
            VerifyElementDisplayed(() => CartOverview.RemoveProfessionalAccountLink);

            CartOverview.RemoveProfessionalAccountLink.Click();
            Thread.Sleep(10000);
            Browser.Wait.ForDisplayedElement(CartOverview.AddProfessionalAccountLink, 120);
            // End Add Professional Account

            // Email
            Browser.Wait.ForDisplayedElement(CartOverview.EmailButton);
            CartOverview.EmailButton.Click();
            Browser.Wait.ForDisplayedElement(GlobalLocators.IframeModal, 30);

            Browser.SwitchFocusToIframe(GlobalLocators.IframeModal);
            Browser.Wait.ForDisplayedElement(CartOverview.EmailFormContainer, 30);

            VerifyElementDisplayed(() => CartOverview.InHomeConsultInfoButton);
            VerifyElementDisplayed(() => CartOverview.InstallationInfoButton);
            VerifyElementDisplayed(() => CartOverview.SmallImagesRadio);
            VerifyElementDisplayed(() => CartOverview.LargeImagesRadio);
            
            Browser.SwitchToDefaultContent();
            Browser.Locate.ElementById(GlobalLocators.LpModalCloseId).Click();
            Thread.Sleep(5000);
            // End Email

            // Ship
            Browser.Wait.ForDisplayedElement(CartOverview.ChangeShippingOptionsLink);
            CartOverview.ChangeShippingOptionsLink.Click();
            VerifyElementNotImplemented(() => CartOverview.CloseButton);

            Browser.Wait.ForDisplayedElement(GlobalLocators.Iframe, 30);

            VerifyElementDisplayed(() => CartOverview.CountryInputField);
            VerifyElementDisplayed(() => CartOverview.DeliveryOptionsContainer);
            VerifyElementDisplayed(() => CartOverview.ShippingCountryDropdown);
            VerifyElementDisplayed(() => CartOverview.ShippingOptionModal);
            VerifyElementDisplayed(() => CartOverview.ShippingZipField);
            VerifyElementDisplayed(() => CartOverview.ShipTab);
            VerifyElementDisplayed(() => CartOverview.ShipTabSearchButton);
            VerifyElementDisplayed(() => CartOverview.ShipZipApplyBtn);
            VerifyElementDisplayed(() => CartOverview.ShipZipField);
            VerifyElementDisplayed(() => CartOverview.StoreInventoryTab);
            VerifyElementDisplayed(() => CartOverview.StorePickupElement);
            VerifyElementDisplayed(() => CartOverview.UpdateShipButton);
            VerifyElementNotImplemented(() => CartOverview.CloseShippingOptionElement);

            CartOverview.ShippingZipField.Clear();
            CartOverview.ShippingZipField.SendKeys(ZipCodeList.Chatsworth);
            CartOverview.ShipTabSearchButton.Click();

            Browser.Wait.ForDisplayedElement(CartOverview.ShippingOptionsRadioButton, 30);

            VerifyElementDisplayed(() => CartOverview.ShippingCostLabels);
            VerifyElementDisplayed(() => CartOverview.ShippingDaysLabels);
            VerifyElementDisplayed(() => CartOverview.ShippingOptionsRadioButton);
            VerifyElementDisplayed(() => CartOverview.ShippingOptionsRadioButtonImmediately);
            VerifyElementDisplayed(() => CartOverview.ShippingTypeRadios);
            VerifyElementDisplayed(() => CartOverview.WhiteGloveShippingOption);
            VerifyElementDisplayed(() => CartOverview.AvailableShippingOptions);
            // End Ship

            // Store Inventory
            CartOverview.StoreInventoryTab.Click();
            Browser.Wait.ForDisplayedElement(CartOverview.StoreInventoryZipField, 30);

            VerifyElementDisplayed(() => CartOverview.StoreInventorySearchButton);
            VerifyElementDisplayed(() => CartOverview.StoreInventorySelectRegionDropdown);
            VerifyElementDisplayed(() => CartOverview.StoreInventoryUpdateButton);
            VerifyElementDisplayed(() => CartOverview.StoreInventoryZipField);

            CartOverview.StoreInventoryZipField.Clear();
            CartOverview.StoreInventoryZipField.SendKeys(ZipCodeList.Chatsworth);
            CartOverview.StoreInventorySearchButton.Click();
            Browser.Wait.ForDisplayedElement(CartOverview.StoreInventoryOptionList, 30);
            Thread.Sleep(1000);

            VerifyElementDisplayed(() => CartOverview.StoreInventoryOptionList);
            // End Store Inventory

            CartOverview.ShipTab.Click();
            Thread.Sleep(5000);
            
            CartOverview.ShippingZipField.Clear();
            CartOverview.ShippingZipField.SendKeys("11111");
            CartOverview.ShipTabSearchButton.Click();
            Browser.Wait.ForDisplayedElement(CartOverview.ShippingOptionsError, 30);

            VerifyElementDisplayed(() => CartOverview.ShippingOptionsError);

            CartOverview.UpdateShipButton.Click();
            Thread.Sleep(5000);

            Browser.Wait.ForDisplayedElement(CartOverview.CartOverviewWarningMessageElement, 30);
            VerifyElementDisplayed(() => CartOverview.CartOverviewWarningMessageElement);

            VerifyElementDisplayed(() => CartOverview.UnknownShippingZoneFields);
            // End Shipping

            // Add Sku
            CartOverview.AddShortSkuElement.SendKeys("12345");
            VerifyElementDisplayed(() => CartOverview.AddSkuLinkElement);
            // End Add Sku

            // UMRP - Manager Approval
            CartOverview.CartEditPriceElement.Click();
            Browser.Wait.ForDisplayedElement(CartOverview.ApplyDiscountButton, 30);

            VerifyElementDisplayed(() => CartOverview.ApplyDiscountButton);
            VerifyElementDisplayed(() => CartOverview.DiscountDropdown);
            VerifyElementDisplayed(() => CartOverview.DiscountPercentTextBox);
            VerifyElementDisplayed(() => CartOverview.DiscountTooltip);
            VerifyElementDisplayed(() => CartOverview.DiscountTooltipContainer);
            VerifyElementDisplayed(() => CartOverview.DiscountTooltipRemoveButton);
            VerifyElementDisplayed(() => CartOverview.SelDiscountReasonField);
            VerifyElementDisplayed(() => CartOverview.TextDiscountPriceField);
            VerifyElementDisplayed(() => CartOverview.TextPercentDiscountField);

            CartOverview.DiscountPercentTextBox.SendKeys("99");
            Browser.Locate.ClickDropdownByValue(CartOverview.DiscountDropdown, "1");
            CartOverview.ApplyDiscountButton.Click();
            Thread.Sleep(5000);
            Browser.Wait.ForDisplayedElement(GlobalLocators.Iframe, 30);

            VerifyElementDisplayed(() => CartOverview.ManualDiscountManagerApprovalForm);

            Browser.Locate.ElementById(GlobalLocators.LpModalCloseId).Click();
            Thread.Sleep(5000);
            // End UMRP - Manager Approval

            // UMRP - Vendor Approval
            CartOverview.CartEditPriceElement.Click();
            Browser.Wait.ForDisplayedElement(CartOverview.ApplyDiscountButton, 30);

            CartOverview.DiscountPercentTextBox.Clear();
            CartOverview.DiscountPercentTextBox.SendKeys("5");
            Browser.Locate.ClickDropdownByValue(CartOverview.DiscountDropdown, "1");
            CartOverview.ApplyDiscountButton.Click();
            Thread.Sleep(5000);
            Browser.Wait.ForDisplayedElement(CartOverview.DiscountVendorApprovalComment, 30);

            VerifyElementDisplayed(() => CartOverview.DiscountVendorApprovalComment);
            VerifyElementDisplayed(() => CartOverview.DiscountVendorApprovalCommentContainer);

            Browser.Locate.ElementById(GlobalLocators.LpModalCloseId).Click();
            Thread.Sleep(5000);
            // End UMRP - Vendor Approval
            
            CartOverview.AddShortSkuElement.SendKeys("99999");
            CartOverview.AddSkuLinkElement.Click();

            VerifyElementDisplayed(() => CartOverview.CartErrorModalElement);

            /*********************************************************************************************************/
            /*                                              Regular CSR                                              */
            /*********************************************************************************************************/

            Browser.ClearAllCookies();
            Browser.Navigate(Urls.HomePageUrl);
            SignInWorkflow.SignIn(LampsPlusAccounts.CustomerServiceRegularLoginAccount);

            ShoppingCartWorkflow.EmptyCart();

            // Patternable Product
            ShoppingCartWorkflow.AddPatternableItemToCart(ProductActions.GetPatternableProductShortSku);

            // More Details
            CartOverview.ToggleMoreDetailsButton.Click();
            Browser.Wait.ForDisplayedElement(CartOverview.MoreDetailsInventoryElement, 30);

            CartOverview.ToggleMoreDetailsButton.Click();
            Thread.Sleep(5000);

            Browser.Wait.ForDisplayedElement(CartOverview.ChangeShippingOptionsLink);
            CartOverview.ChangeShippingOptionsLink.Click();

            Browser.Wait.ForDisplayedElement(GlobalLocators.Iframe, 30);

            CartOverview.ShippingZipField.Clear();
            CartOverview.ShippingZipField.SendKeys(ZipCodeList.Chatsworth);
            CartOverview.ShipTabSearchButton.Click();

            VerifyElementDisplayed(() => CartOverview.StandardShippingOption);

            // Store Pickup
            Browser.RefreshPage();
            ShoppingCartWorkflow.EmptyCart();
            ShoppingCartWorkflow.AddSingleItemToCart();

            Browser.Wait.ForDisplayedElement(CartOverview.ChangeShippingOptionsLink);
            CartOverview.ChangeShippingOptionsLink.Click();

            Browser.Wait.ForDisplayedElement(GlobalLocators.Iframe, 30);

            CartOverview.StorePickupElement.Click();
            Thread.Sleep(5000);
            Browser.Wait.ForDisplayedElement(CartOverview.StorePickupListName, 30);

            VerifyElementDisplayed(() => CartOverview.StorePickupSearchButton);
            VerifyElementDisplayed(() => CartOverview.StorePickupUpdateButton);
            VerifyElementDisplayed(() => CartOverview.StorePickupListName);
            VerifyElementDisplayed(() => CartOverview.StorePickupZipField);
            // End Store Pickup

            /*********************************************************************************************************/
            /*                                              Manager CSR                                              */
            /*********************************************************************************************************/

            Browser.ClearAllCookies();
            Browser.Navigate(Urls.HomePageUrl);
            SignInWorkflow.SignIn(LampsPlusAccounts.CustomerServiceManagerLoginAccount);

            ShoppingCartWorkflow.EmptyCart();

            // White-Glove with UMRP
            ShoppingCartWorkflow.AddItemToCartBySku(new ProductModel(ProductActions.GetFurnitureWithUmrpAndInHomeDelivery));

            // POS
            Home.EnterStoreInSession("12");

            VerifyElementDisplayed(() => CartOverview.AllKioskLink);
            VerifyElementDisplayed(() => CartOverview.AllPosLink);
            VerifyElementDisplayed(() => CartOverview.AllShipLink);
            VerifyElementDisplayed(() => CartOverview.KioskShippingShortcuts);
            VerifyElementDisplayed(() => CartOverview.OrderSummaryHeader);
            VerifyElementDisplayed(() => CartOverview.PosCheckBoxAndLabel);
            VerifyElementDisplayed(() => CartOverview.PosCheckBoxes);
            VerifyElementDisplayed(() => CartOverview.PosLabel);
            // End POS

            // UMRP
            CartOverview.AllKioskLink.Click();
            Thread.Sleep(1000);

            CartOverview.CartEditPriceElement.Click();
            Browser.Wait.ForDisplayedElement(CartOverview.ApplyDiscountButton, 30);

            VerifyElementDisplayed(() => CartOverview.ShowUpTooltip);
            VerifyElementDisplayed(() => CartOverview.TextMarginField);

            CartOverview.DiscountPercentTextBox.SendKeys("99");
            Browser.Locate.ClickDropdownByValue(CartOverview.DiscountDropdown, "1");
            CartOverview.ApplyDiscountButton.Click();
            Thread.Sleep(5000);

            VerifyElementDisplayed(() => CartOverview.AdditionalDiscountElement);
            VerifyElementDisplayed(() => CartOverview.SubTotalLabel);
            // End UMRP

            /*********************************************************************************************************/
            /*                                             Customer User                                             */
            /*********************************************************************************************************/

            Browser.ClearAllCookies();
            Browser.Navigate(Urls.HomePageUrl);
            SignInWorkflow.SignIn(LampsPlusAccounts.CustomerLoginAccount);

            ShoppingCartWorkflow.EmptyCart();

            var shortSku = ProductActions.GetLessThanTenDollarItem;

            ConditionalVerify.DatabaseObject(shortSku, "ProductActions.GetLessThanTenDollarItem()");

            ShoppingCartWorkflow.AddItemToCartBySku(new ProductModel { Sku = shortSku });

            Browser.MouseOverOnElement(CartOverview.CheckOutNowButton);
            Thread.Sleep(1000);
            VerifyElementDisplayed(() => CartOverview.CheckOutValidationTooltip);
            VerifyElementDisplayed(() => CartOverview.CheckOutBtnValidationTooltip);
            VerifyElementDisplayed(() => CartOverview.PaypalValidationTooltip);
            VerifyElementDisplayed(() => CartOverview.CartCalloutButtonsBottomElement);

            VerifyElementNotImplemented(() => CartOverview.CloseShippingOptionsOverlay);
            VerifyElementNotImplemented(() => CartOverview.PromoDiscount);
            VerifyElementNotImplemented(() => CartOverview.PayPalContainer);
        }
    }


    public class CartOverviewLocatorMobileTests : CartOverviewLocatorTests
    {
        /// <summary>
        /// Tests to ensure this page can find all its IElements.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public CartOverviewLocatorMobileTests(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the requested elements could be located on the given shopping cart page.
        /// </summary>
        [Trait(LpTraits.Integration.PageObjectModel, "CartOverview")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LocateElementsOnCartOverviewPageTest(string config) => Locate(config);

        protected override void ElementVerification()
        {
            /*********************************************************************************************************/
            /*                                             Anonymous User                                            */
            /*********************************************************************************************************/

            Browser.ClearAllCookies();

            Browser.Navigate(Urls.CartOverviewPageUrl);
            Browser.Wait.ForPage(Urls.CartOverviewPageUrl);

            VerifyElementDisplayed(() => CartOverview.EmptyCartWarningMessageElement);
            VerifyElementDisplayed(() => CartOverview.EmptyCartWarningMessageElementImmediately);

            ShoppingCartWorkflow.AddSingleItemToCart();

            VerifyElementDisplayed(() => CartOverview.CartIdElement);
            VerifyElementDisplayed(() => CartOverview.CartOverviewElement);
            VerifyElementDisplayed(() => CartOverview.CartPromotionalButton);
            VerifyElementDisplayed(() => CartOverview.ChangeShippingOptionsLink);
            VerifyElementDisplayed(() => CartOverview.CheckOutNowButton);
            VerifyElementDisplayed(() => CartOverview.CheckOutNowButtons);
            VerifyElementDisplayed(() => CartOverview.EmailButton);
            VerifyElementDisplayed(() => CartOverview.PayPalButton);
            VerifyElementDisplayed(() => CartOverview.PayPalButtonContainer);
            VerifyElementDisplayed(() => CartOverview.PayPalContainer);
            VerifyElementDisplayed(() => CartOverview.ProductListItems);
            VerifyElementDisplayed(() => CartOverview.ProductQuantityField);
            VerifyElementDisplayed(() => CartOverview.ProductSkuLabelCart);
            VerifyElementDisplayed(() => CartOverview.PromoCodeInfo);
            VerifyElementDisplayed(() => CartOverview.RemoveItemLinksElements);
            VerifyElementDisplayed(() => CartOverview.ShippingAndProcessingLabel);
            VerifyElementDisplayed(() => CartOverview.TaxLabel);
            VerifyElementDisplayed(() => CartOverview.SubTotalLabel);

            VerifyElementNotImplemented(() => CartOverview.ShopWithConfidenceContainer);
            VerifyElementNotImplemented(() => CartOverview.CartIdContainer);

            // Email
            Browser.Wait.ForDisplayedElement(CartOverview.EmailButton);
            CartOverview.EmailButton.Click();
            Browser.Wait.ForPage(Urls.EmailCartUrl);

            Browser.Wait.ForDisplayedElement(CartOverview.EmailFormContainer, 30);

            VerifyElementDisplayed(() => CartOverview.EmailFormContainer);
            VerifyElementDisplayed(() => CartOverview.FormEmailFromField);
            VerifyElementNotImplemented(() => CartOverview.FormEmailRecipientsField);
            VerifyElementDisplayed(() => CartOverview.FormFirstNameField);
            VerifyElementDisplayed(() => CartOverview.FormLastNameField);
            VerifyElementDisplayed(() => CartOverview.FormZipCodeField);
            VerifyElementDisplayed(() => CartOverview.SendEmailButton);
            Browser.GoBack();
            Thread.Sleep(5000);
            // End Email

            // Promo Codes
            Browser.Wait.ForDisplayedElement(CartOverview.CartPromotionalButton);
            CartOverview.CartPromotionalButton.Click();
            Browser.Wait.ForDisplayedElement(CartOverview.PromoCodeFields, 30);

            VerifyElementDisplayed(() => CartOverview.PromoCodeFields);
            VerifyElementDisplayed(() => CartOverview.PromoInputField);

            CartOverview.PromoInputField.SendKeys("silicustest");
            VerifyElementDisplayed(() => CartOverview.PromoCodeApplyButton);
            CartOverview.PromoCodeApplyButton.Click();

            VerifyElementNotImplemented(() => CartOverview.CouponCodeError);
            VerifyElementNotImplemented(() => CartOverview.CouponCodeErrorElement);
            VerifyElementDisplayed(() => CartOverview.PromoCodeLabel);
            VerifyElementDisplayed(() => CartOverview.RemovePromoCodeElement);
            VerifyElementDisplayed(() => CartOverview.PromoDiscount);
            CartOverview.RemovePromoCodeElement.Click();
            Browser.Wait.UntilElementUnloads(CartOverview.RemovePromoCodeElement);
            // End Promo Codes

            // Shipping
            // Ship
            Browser.RefreshPage();
            Browser.Wait.ForDisplayedElement(CartOverview.ChangeShippingOptionsLink);
            CartOverview.ChangeShippingOptionsLink.Click();
            Thread.Sleep(3000);

            VerifyElementDisplayed(() => CartOverview.CloseButton);

            VerifyElementDisplayed(() => CartOverview.CloseShippingOptionsOverlay);
            VerifyElementDisplayed(() => CartOverview.CountryInputField);
            VerifyElementDisplayed(() => CartOverview.DeliveryOptionsContainer);
            VerifyElementDisplayed(() => CartOverview.ShippingCountryDropdown);
            VerifyElementDisplayed(() => CartOverview.ShippingOptionModal);
            VerifyElementDisplayed(() => CartOverview.ShippingZipField);
            VerifyElementDisplayed(() => CartOverview.ShipTab);
            VerifyElementDisplayed(() => CartOverview.ShipTabSearchButton);
            VerifyElementDisplayed(() => CartOverview.ShipZipApplyBtn);
            VerifyElementDisplayed(() => CartOverview.ShipZipField);
            VerifyElementNotImplemented(() => CartOverview.StoreInventoryTab);
            VerifyElementNotImplemented(() => CartOverview.StorePickupElement);
            VerifyElementDisplayed(() => CartOverview.UpdateShipButton);

            CartOverview.ShippingZipField.Clear();
            CartOverview.ShippingZipField.SendKeys(ZipCodeList.Chatsworth);
            CartOverview.ShipTabSearchButton.Click();

            Browser.Wait.ForElement(CartOverview.ShippingOptionsRadioButton, 30);

            VerifyElementDisplayed(() => CartOverview.ShippingCostLabels);
            VerifyElementDisplayed(() => CartOverview.ShippingDaysLabels);
            VerifyElementDisplayed(() => CartOverview.ShippingOptionsRadioButton);
            VerifyElementDisplayed(() => CartOverview.ShippingOptionsRadioButtonImmediately);
            VerifyElementDisplayed(() => CartOverview.ShippingTypeRadios);
            VerifyElementDisplayed(() => CartOverview.StandardShippingOption);

            CartOverview.ShippingZipField.Clear();
            CartOverview.ShippingZipField.SendKeys("11111");
            CartOverview.ShipTabSearchButton.Click();
            Browser.Wait.ForDisplayedElement(CartOverview.ShippingOptionsError, 30);
            VerifyElementDisplayed(() => CartOverview.ShippingOptionsError);
            VerifyElementDisplayed(() => CartOverview.CloseShippingOptionElement);
            CartOverview.CloseShippingOptionElement.Click();

            CartOverviewWarningMessage();

            VerifyElementDisplayed(() => CartOverview.UnknownShippingZoneFields);

            ShoppingCartWorkflow.EmptyCart();

            var product = new ProductModel(ProductActions.GetFurnitureWithUmrpAndInHomeDelivery);
            ShoppingCartWorkflow.AddItemToCartBySku(product);
            Browser.Wait.ForDisplayedElement(CartOverview.ChangeShippingOptionsLink);
            CartOverview.ChangeShippingOptionsLink.Click();

            CartOverview.ShippingZipField.Clear();
            CartOverview.ShippingZipField.SendKeys(ZipCodeList.Chatsworth);
            CartOverview.ShipTabSearchButton.Click();

            VerifyElementDisplayed(() => CartOverview.AvailableShippingOptions);
            VerifyElementDisplayed(() => CartOverview.WhiteGloveShippingOption);

            VerifyElementDisplayed(() => CartOverview.ShippingName);

            // End Shipping

            //Elements not Implemented
            VerifyElementNotImplemented(() => CartOverview.StorePickupSearchButton);
            VerifyElementNotImplemented(() => CartOverview.StorePickupUpdateButton);
            VerifyElementNotImplemented(() => CartOverview.AllPosLink);
            VerifyElementNotImplemented(() => CartOverview.AddShortSkuElement);
            VerifyElementNotImplemented(() => CartOverview.AddSkuContainer);
            VerifyElementNotImplemented(() => CartOverview.AddSkuLinkElement);
            VerifyElementNotImplemented(() => CartOverview.AdditionalDiscountElement);
            VerifyElementNotImplemented(() => CartOverview.ApplyDiscountButton);
            VerifyElementNotImplemented(() => CartOverview.CartEditPriceElement);
            VerifyElementNotImplemented(() => CartOverview.CartErrorModalElement);
            VerifyElementNotImplemented(() => CartOverview.CompanyNameField);
            VerifyElementNotImplemented(() => CartOverview.CompanySearchButton); 
            VerifyElementNotImplemented(() => CartOverview.CompanyNameLinkTable);
            VerifyElementNotImplemented(() => CartOverview.DeleteCartElement);
            VerifyElementNotImplemented(() => CartOverview.DeleteCartButton);
            VerifyElementNotImplemented(() => CartOverview.DiscountDropdown);
            VerifyElementNotImplemented(() => CartOverview.DiscountPercentTextBox);
            VerifyElementNotImplemented(() => CartOverview.DiscountTooltip);
            VerifyElementNotImplemented(() => CartOverview.DiscountTooltipContainer);
            VerifyElementNotImplemented(() => CartOverview.DiscountTooltipRemoveButton);
            VerifyElementNotImplemented(() => CartOverview.DiscountVendorApprovalComment);
            VerifyElementNotImplemented(() => CartOverview.DiscountVendorApprovalCommentContainer);
            VerifyElementNotImplemented(() => CartOverview.ManualDiscountManagerApprovalForm);
            VerifyElementNotImplemented(() => CartOverview.OrderSummaryHeader);
            VerifyElementNotImplemented(() => CartOverview.MoreDetailsInventoryElement);
            VerifyElementNotImplemented(() => CartOverview.PosLabel);
            VerifyElementNotImplemented(() => CartOverview.PosCheckBoxAndLabel);
            VerifyElementNotImplemented(() => CartOverview.ProfessionalAccountLabel);
            VerifyElementNotImplemented(() => CartOverview.StoreInventorySelectRegionDropdown);
            VerifyElementNotImplemented(() => CartOverview.StoreInventoryUpdateButton);
            VerifyElementNotImplemented(() => CartOverview.ShowUpTooltip);
            VerifyElementNotImplemented(() => CartOverview.StoreInventoryZipField);
            VerifyElementNotImplemented(() => CartOverview.StorePickupListName);
            VerifyElementNotImplemented(() => CartOverview.StoreInventorySearchButton);
            VerifyElementNotImplemented(() => CartOverview.CompanyNameLink);
            VerifyElementNotImplemented(() => CartOverview.AllShipLink);
            VerifyElementNotImplemented(() => CartOverview.AllKioskLink);
            VerifyElementNotImplemented(() => CartOverview.KioskShippingShortcuts);
            VerifyElementNotImplemented(() => CartOverview.InHomeConsultInfoButton);
            VerifyElementNotImplemented(() => CartOverview.InstallationInfoButton);
            VerifyElementNotImplemented(() => CartOverview.StorePickupZipField);
            VerifyElementNotImplemented(() => CartOverview.SelDiscountReasonField);
            VerifyElementNotImplemented(() => CartOverview.TextDiscountPriceField);
            VerifyElementNotImplemented(() => CartOverview.TextMarginField);
            VerifyElementNotImplemented(() => CartOverview.TextPercentDiscountField);
            VerifyElementNotImplemented(() => CartOverview.ToggleMoreDetailsButton);
            VerifyElementNotImplemented(() => CartOverview.StoreInventoryOptionList);
            VerifyElementNotImplemented(() => CartOverview.RemoveProfessionalAccountLink);
            VerifyElementNotImplemented(() => CartOverview.AddProfessionalAccountLink);
            VerifyElementsNotImplemented(() => CartOverview.PosCheckBoxes);
            VerifyElementNotImplemented(() => CartOverview.CheckOutValidationTooltip);
            VerifyElementNotImplemented(() => CartOverview.CheckOutBtnValidationTooltip);
            VerifyElementNotImplemented(() => CartOverview.PaypalValidationTooltip);
            VerifyElementNotImplemented(() => CartOverview.CartCalloutButtonsBottomElement);
            VerifyElementsNotImplemented(() => CartOverview.ProductItemWrapElements);
            VerifyElementNotImplemented(() => CartOverview.PrintCartButton);
            VerifyElementNotImplemented(() => CartOverview.SelectNoneLink);
            VerifyElementNotImplemented(() => CartOverview.SelectLargeImage);
			VerifyElementNotImplemented(() => CartOverview.SelectSmallImage);
            VerifyElementNotImplemented(() => CartOverview.MoreDetailsElement);
        }

        private void CartOverviewWarningMessage()
        {
            Browser.Wait.ForDisplayedElement(CartOverview.CartOverviewWarningMessageElement, 30);
            VerifyElementDisplayed(() => CartOverview.CartOverviewWarningMessageElement);
        }
    }


    /// <summary>
    /// Tests to ensure all IElements and Lists of IElements can be found on the given page object.
    /// </summary>
    public abstract class CartOverviewLocatorTests : PageObjectTestsBase
    {
        /// <summary>
        /// Tests to ensure this page can find all its IElements.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        protected CartOverviewLocatorTests(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the requested elements could be located on the given shopping cart page.
        /// </summary>
        public void Locate(string config)
        {
            InitializeFramework(config);
            BuildElementsList(CartOverview);

            ElementVerification();
        }

        protected abstract void ElementVerification();

        protected void AddProfessionalAccount()
        {
            if (CartOverview.RemoveProfessionalAccountLink.Displayed)
            {
                CartOverview.RemoveProfessionalAccountLink.Click();
                Thread.Sleep(10000);
                Browser.Wait.ForDisplayedElement(CartOverview.AddProfessionalAccountLink, 120);
            }
        }
    }
}
