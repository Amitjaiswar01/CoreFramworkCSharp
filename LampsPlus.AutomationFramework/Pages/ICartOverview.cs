using System.Collections.Generic;
using System.Collections.ObjectModel;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Databases.Actions;
using OperatingSystem = LampsPlus.AutomationFramework.Utilities.TestConfiguration.OperatingSystem;

namespace LampsPlus.AutomationFramework.Pages
{
    /// <summary>
    /// Base class for common behavior between desktop and mobile views.
    /// </summary>
    public interface ICartOverview
    {
        #region Class Setup

        /// <summary>
        /// <see cref="Databases.Actions.ProductActions"/>
        /// </summary>
        ProductActions ProductActions { get; }

        string AdditionalDiscountsXpath { get; }
        string CartPromotionalCodeId { get; }
        string ChangeShippingOptionsClass { get; }
        string SubTotalOnCartClass { get; }
        string CartTitleXpath { get; }
        string ShowUpTooltipXpath { get; }
        string AlmostSoldOut { get; }
        string ApaResultsContainerClass { get; }
        string AppliedCodeLabel { get; }
        string CouponAndMemberSpecialPriceSavingsLabel { get; }
        string FreeShippingFreeReturns { get; }
        string ProductTotalPrefix { get; }
        string AdditionalDiscountsLabel { get; }
        string JsCloseShippingOptionsOverlayClass { get; }
        string ProfessionalSavingsLabel { get; }
        string PromoCodeDiscountTotalPrefix { get; }
        string SubtotalPrefix { get; }
        string ShippingTotalPrefix { get; }
        string ShippingAndReturnType { get; }
        string TaxTotalPrefix { get; }
        string OrderTotalPrefix { get; }
        string AddProfessionalAccountId { get; }
        string JsUpdateStorePickupBtnClass { get; }
        string ApaQueryClass { get; }
        string AdditionalDiscountsClass { get; } 
        string AvailableShippingOptionsClass { get; } 
        string AvailableShippingOptionsNameClass { get; } 
        string BtnApplyDiscountClass { get; } 
        string CartContentClass { get; }
        string CartEmptyWarningClass { get; } 
        string CartIdElementId { get; }
        string CartSuggestedProductsContainerId { get; }
        string CheckOutNowClass { get; } 
        string AddPromoCodeLinkClass { get; } 
        string DeliveryOptionsCountryClass { get; } 
        string DeliveryOptionsZipBtnClass { get; }
        string DeliveryOptionsContainerClass { get; }
        string DiscountTooltipClass { get; }
        string EditPriceClass { get; }
        string ItemTotalClass { get; }
        string JsDeliveryOptionsTabClass { get; } 
        string JsInventoryZipApplyClass { get; } 
        string JsShippingCountryClass { get; } 
        string FieldRadioClass { get; }
        string JsShippingCostClass { get; }
        string JsShippingDaysClass { get; } 
        string JsShipZipApplyClass { get; }
        string BoldChatContainerClass { get; }
        string JsShowPromoCodeTermsAndConditionsClass { get; }
        string JsStoreZipApplyClass { get; } 
        string JsUpdateShipBtnClass { get; } 
        string JsUpdateStoreInventoryBtnClass { get; } 
        string JsZipForStoreClass { get; } 
        string JsZipForInventoryClass { get; } 
        string LimitedQuantityCalloutClass { get; }
        string LnkRemoveDiscountClass { get; } 
        string ManualDiscountManagerApprovalClass { get; } 
        string PosPurchaseOptionCheckboxClass { get; }
        string ProductItemsId { get; }
        string ProfessionalAccountLabelId { get; } 
        string PromoCodeTextClass { get; } 
        string ProdImageCellClass { get; } 
        string ProdQtyClass { get; }
        string ProductQtyFieldClass { get; } 
        string PromoDiscountClass { get; } 
        string RemoveCartItemBtnClass { get; } 
        string RemovePromoCodeXpath { get; }
        string ProductNameClass { get; } 
        string PromoInputClass { get; } 
        string RemoveItemClass { get; }
        string RemovePromoCodeClass { get; }
        string RemoveProfessionalAccountId { get; }
        string SelDiscountReasonClass { get; }
        string SelectDeliveryOptionsModalId { get; } 
        string ChangeShippingOptionsOverlayId { get; }
        string JsChangeShippingOptionsClass { get; }
        string ShippingOptionsChangedContainerClass { get; }
        string ShipZipFieldId { get; } 
        string ShowUpTooltipClass { get; } 
        string StorePickupListNameClass { get; } 
        string StyleNumberClass { get; }
        string TxtPercentDiscClass { get; }
        string UpperCaseClass { get; }
        string ProceedToPaymentClass { get; } 
        string ShippingOptionContainerClass { get; }
        #endregion

        #region Page Elements
        IElement SaleCountDown { get; }
        IElement ShippingOptionContainer { get; }
        IElement SubTotalOnCart { get; }
        IElement AlmostSoldOutCallout { get; }
        IElement ApplyDiscountButton { get; }
        IElement ApplyDiscountModal { get; }
        IElement CartEditPriceElement { get; }
        IElement CartIdContainer { get; }
        IElement CartMoreYouMayLikeContainer { get; }
        IElement CartOverviewElement { get; }
        IElement CartSignInLink { get; }
        IElement CartTitle { get; }
        IElement CheckOutNowButton { get; }
        IElement CloseButton { get; }
        IElement CloseShippingOptionElement { get; }
        IElement CompanyNameField { get; }
        IElement DeliveryOptionsContainer { get; }
        IElement CartIdElement { get; }
        IElement DiscountDropdown { get; }
        IElement DiscountPercentTextBox { get; }
        IElement DiscountTooltip { get; }
        IElement DiscountTooltipRemoveButton { get; }
        IElement MobileRemovePromoCode { get; }
        IElement GiftCardQtyField { get; }
        IElement ProfessionalAccountLabel { get; }
        IElement PromoCodeText { get; }
		IElement StoreInventoryUpdateButton { get; }
		IElement ShowUpTooltip { get; }
		IElement StoreInventoryZipField { get; }
		IElement StoreInventorySearchButton { get; }
        IElement RemoveButton { get;}
        IElement RemovePromoCodeElement { get; }
		IElement CompanyNameLink { get; }
        IElement ChangeShippingOptionsLink { get; }
		IElement CountryInputField { get; }
        IElement ShippingOptionModal { get; }
		IElement ShipTabSearchButton { get; }
		IElement ShippingZipField { get; }
		IElement StorePickupZipField { get; }
		IElement UpdateShipButton { get; }
		IElement PromoCodeLabel { get; }
		IElement ProductSkuLabelCart { get; }
		IElement CartPromotionalButton { get; }
        IElement PromoCode { get; }
        IElement PromoInputField { get; }
        IElement SelDiscountReasonField { get; }
		IElement ShippingCountryDropdown { get; }
		IElement ShipZipField { get; }
		IElement ShipZipApplyBtn { get; }
		IElement TextPercentDiscountField { get; }
        IElement ShippingOptionsRadioButton { get; }
		IElement StorePickupElement { get; }
		IElement StoreInventoryTab { get; }
		IElement TaxLabel { get; }
		IElement SubTotalLabel { get; }
		IElement ShippingAndProcessingLabel { get; }
		IElement RemoveProfessionalAccountLink { get; }
		IElement AddProfessionalAccountLink { get; }
        IElement StorePickupSearchButton { get; }
        IElement StorePickupUpdateButton { get; }
        IElement CloseShippingOptionsOverlay { get; }
        IElement DeliveryPolicyAgreementProceedToPayment { get; }
        IElement AuthorizationModalUsernameInput { get; }
        IElement AuthorizationModalPasswordInput { get; }
        IElement ModalSubmitButton { get; }
        IElement AuthorizationModalErrorText { get; }
        IElement GiftCardDetails(int index);
        IElement BoldChatContainer { get; }
        IElement TaxLabelOnOrderConfirmationPage { get; }

        ReadOnlyCollection<IElement> AvailableShippingOptions { get; }
        ReadOnlyCollection<IElement> PosCheckBoxes { get; }
        ReadOnlyCollection<IElement> ShippingTypeRadios { get; }
        ReadOnlyCollection<IElement> ShippingCostLabels { get; }
        ReadOnlyCollection<IElement> ShippingDaysLabels { get; }
        ReadOnlyCollection<IElement> RemoveItemLinksElements { get; }
        #endregion

        /// <summary>
        /// Remove "Cart #" from the Cart ID to return just the ID.
        /// </summary>
        string CartId { get; }

        /// <summary>
        /// The product sku of item in cart.
        /// </summary>
		string ProductSkuCart { get; }

        /// <summary>
        /// Gets the number of unique items in the shopping cart.
        /// </summary>
        int UniqueProductsCount { get; }

        /// <summary>
        /// Log class to update log messages.
        /// </summary>
        Log Log { get; }

        /// <summary>
        /// Instance of a Browser to enable browser specific UI testing.
        /// </summary>
        IBrowser Browser { get; }
        IElement ChangeShippingOptionsLinkByIndex(int index);
        IElement ProductImageAnchorWebElement(int index);
        IElement ProductNameLabel(int index);
        IElement ProductSkuLabel(int index);
        IElement ProductTotalCostLabel(int index);
        IElement ProductPromoDiscount(int index);
        IElement ShippingOptions(int index);
        IElement ShippingReturnType { get; }
        IElement OrderSummaryBlockLabel(string heading);
        IElement ProductQuantityLabel(int index);
        IElement CartItemRemoveLinkElement(int index);
        IElement StorePickupList(int index);
        IElement ProductQtyDropdownField { get; }
        IElement ProductQtyField { get; }

        ReadOnlyCollection<IElement> OrderSummaryTotalLabels();
        ReadOnlyCollection<IElement> OrderSummaryTotalValues();

        /// <summary>
        /// Wait for Check Out Now button to be ready. There is a current issue where the button is ready in the DOM bu the JS is not bound to the element.
        /// </summary>
        void ClickCheckOutNowButton();

        /// <summary>
        /// Click the Search button on the Ship tab of the shipping options modal and wait 1 second for the page to load.
        /// </summary>
        void ClickShippingOptionShipTabSearchButton();

        /// <summary>
        /// Add professional account with the given company.
        /// </summary>
        /// <param name="company"></param>
        void AddProfessionalAccount(string company);

        /// <summary>
        /// Remove professional account from the cart.
        /// </summary>
        void RemoveProfessionalAccount();

        /// <summary>
        /// Click the Update button on the Ship tab of the Shipping Option modal and wait 3 seconds for the next page to load.
        /// </summary>
        void ClickShippingOptionShipTabUpdateButton();

        /// <summary>
        /// Click the apply promo code button after information has been entered in the promo code field. Then wait 3 seconds for the next page to load.
        /// </summary>
        void ApplyPromoCode();

        /// <summary>
        /// Opens shipping zip modal and changes the zip code
        /// </summary>
        void ChangeShippingZipCode(string zip = ZipCodeList.Chatsworth);

        /// <summary>
        /// Is the LP modal displayed?
        /// </summary>
        /// <returns></returns>
        bool IsLpModalDisplayed();

        /// <summary>
        /// Get the cell shipping cost.
        /// </summary>
        /// <param name="removeCurrencySign">Remove the currency symbol when true.</param>
        /// <returns></returns>
        string GetShippingCellShippingCost(bool removeCurrencySign = true);

        /// <summary>
        /// Get the promo code label.
        /// </summary>
        /// <returns></returns>
        string GetPromoCodeLabel();

        /// <summary>
        /// Get the promo code label.
        /// </summary>
        /// <returns></returns>
        string GetProfessionalSavingsLabel();

        /// <summary>
        /// Get the additional discounts label 
        /// </summary>
        /// <returns></returns>
        string GetAdditionalDiscountsLabel();

        string ProductName(int index);
        string ProductSku(int index);
        string ProductTotalPrice(int index);
        string PromoDiscountPrice(int index);
        string TaxPrice(int index);//monica

        /// <summary>
        /// Get the quantity for the product at the given index.
        /// </summary>
        /// <param name="index">Index of the product to get the quantity of.</param>
        /// <returns></returns>
        string ProductQuantity(int index);

        /// <summary>
        /// Get additional discounts for the order
        /// </summary>
        /// <returns></returns>
        decimal GetAdditionalDiscountsWithPrefix();

        /// <summary>
        /// Gets the total cost of a product.
        /// </summary>
        /// <returns></returns>
        decimal GetProductTotal();

        /// <summary>
        /// Gets the promo discount price without removing - sign.
        /// </summary>
        /// <returns></returns>
        decimal GetActualPromoCodeDiscountPrice();

        /// <summary>
        /// Gets the professional saving price without removing - sign.
        /// </summary>
        /// <returns></returns>
        decimal GetProfessionalSavingsPrice();

        /// <summary>
        /// Gets the additional discount price without removing - sign.
        /// </summary>
        /// <returns></returns>
        decimal GetAdditionalDiscountsPrice();

        /// <summary>
        /// Gets the promo discount price without removing - sign with no spaces.
        /// </summary>
        /// <returns></returns>
        decimal GetActualPromoCodeDiscount();

        /// <summary>
        /// Gets the Sales Tax for the order.
        /// </summary>
        /// <returns></returns>
        decimal GetSaleTax();

        /// <summary>
        /// Gets the sub-total cost of the order.
        /// </summary>
        /// <returns></returns>
        decimal GetSubTotal();

        /// <summary>
        /// Gets the shipping cost for the order.
        /// </summary>
        /// <returns></returns>
        decimal GetShippingCost();

        /// <summary>
        /// Returns string value of shipping costs, in order to test for '--', "FREE*", or "FREE" etc. 
        /// </summary>
        /// <returns></returns>
        string GetShippingCostValue();

        /// <summary>
        /// Gets the Order Total value.
        /// </summary>
        /// <returns></returns>
        decimal GetOrderTotalCost();

        /// <summary>
        /// Clears currently item in list, and returns a reference to items currently in cart.
        /// </summary>
        /// <returns></returns>
        List<Utilities.ProductModel> GetListOfAllProductsOnPage();

        /// <summary>
        /// Compares the items that were added to the cart to the actual items in the cart.
        /// </summary>
        /// <param name="addedProducts"></param>
        /// <returns></returns>
        bool DoesCartMatchAddedProducts(Dictionary<string, int> addedProducts);

        /// <summary>
        /// Returns the shipping totals.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        decimal GetShippingTotals(int index);

        /// <summary>
        /// Gets the total number of shipping options available for the order.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>

        //added by me
        decimal GetTaxTotal(int index);

        /// <summary>
        /// Gets the total amount of tax available for the order.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>

        decimal GeProductTotal(int index);

        /// <summary>
        /// Gets the total amount of tax available for the order.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        

        //till here

        /// <summary>
        /// Returns the discounted price.
        /// </summary>
        /// <param name="itemPrice"></param>
        /// <param name="discountRate"></param>
        /// <returns></returns>
        decimal GetDiscountedPrice(decimal itemPrice, decimal discountRate);

        /// <summary>
        /// Un-checks POS checkboxes one at a time (allowing each change to apply).                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            
        /// </summary>
        void UncheckAllPosCheckboxes();

        /// <summary>
        /// Remove Promo Code if it is present.
        /// </summary>
        void RemovePromoCode();

        /// <summary>
        /// Navigate to the given URL.
        /// </summary>
        /// <param name="url">URL to navigate to. This must be begin with http:// or https://.</param>
        void Navigate(string url);

        /// <summary>
        /// Select Store inventory shipping option by item index and zip code.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="zipCode"></param>
        void SelectStoreInventoryShippingOption(int index, string zipCode = ZipCodeList.Chatsworth);

        /// <summary>
        /// Select Store pick up shipping option by item index and zip code.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="zipCode"></param>
        void SelectStorePickupShippingOption(int index, string zipCode = ZipCodeList.Chatsworth);

        /// <summary>
        /// Change country in shipping options. Smart enough to know if modal is already open or if it should open it.
        /// </summary>
        /// <param name="countryDropdownOptionValue">Country value in dropdown (not the label/name)</param>
        void ChangeShippingCountry(string countryDropdownOptionValue);

        /// <summary>
        /// Change both the country and zip code in shipping options modal.
        /// </summary>
        /// <param name="countryDropdownOptionValue">Country value in dropdown (not the label/name)</param>
        /// <param name="zip">Zip code of area.</param>
        void ChangeShippingCountryAndZip(string countryDropdownOptionValue, string zip);

        void WaitForPromoCodeToUnload();

        void ApplyDiscountIosPlatform(string discountApplied, string discountReasonApplied);

        void ApplyDiscount(string discountApplied, string discountReasonApplied, OperatingSystem operatingSystem);
    }
}
