using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Databases.Actions;
using OpenQA.Selenium;
using OperatingSystem = LampsPlus.AutomationFramework.Utilities.TestConfiguration.OperatingSystem;
using Page = Automation.Framework.Core.Page;

namespace LampsPlus.AutomationFramework.Pages.Base
{
    /// <summary>
    /// Base class for common behavior between desktop and mobile views.
    /// </summary>
    public abstract class CartOverviewBase : Page, ICartOverview
    {
        /// <inheritdoc />
        protected CartOverviewBase(IBrowser browser, ShoppingCartActions shoppingCartActions, ProductActions productActions, IGlobalLocators globalLocators, IShipping shipping) : base(browser)
        {
            ProductsInCartList = new List<Utilities.ProductModel>();
            _shoppingCartActions = shoppingCartActions;
            ProductActions = productActions;
            GlobalLocators = globalLocators;
            Shipping = shipping;
        }

        #region Class Setup
        private ShoppingCartActions _shoppingCartActions { get; }
        public IShipping Shipping { get; }

        internal IGlobalLocators GlobalLocators { get; }

        /// <inheritdoc />
        public ProductActions ProductActions { get; }

        public string AlmostSoldOut => "Almost Sold Out!";
        public string AppliedCodeLabel => "APPLIED CODE:";
        public string BoldChatContainerClass => "boldChatButtonContainer";
        public string CouponAndMemberSpecialPriceSavingsLabel => "Promotions and Discounts:";
        public string FreeShippingFreeReturns => "FREE* + Free Returns";
        public string ProductTotalPrefix => "Product Total";
        public string AdditionalDiscountsLabel => "Additional Discounts:";
        public string ProfessionalSavingsLabel => "Professional Savings:";
        public string PromoCodeDiscountTotalPrefix => "Promotions and Discounts";
        public string SubtotalPrefix => "Subtotal";
        public string ShippingTotalPrefix => "Shipping & Processing";
        public string TaxTotalPrefix => "Tax ¹:";
        public string TaxTotalPrefixOnOrderConfirmationPage => "Tax¹:";
        public string OrderTotalPrefix => "Order Total";
        #endregion

        #region CSS Selector Strings
        private string GiftCardInfoItemClass { get; } = "giftCardInfoItem";

        public string AdditionalDiscountsXpath { get; } = "//*[@id=\"orderSummary\"]//div[contains(text(), \"Additional Discounts:\")]";
        public string AddProfessionalAccountId { get; } = "addProfessionalAccount";
        public string ApaResultsContainerClass { get; } = "apaResultsContainer";
        public string JsUpdateStorePickupBtnClass { get; } = "jsUpdateStorePickupBtn";
        public string ApaQueryClass { get; } = "apaQuery";
        public string AdditionalDiscountsClass { get; } = "additionalDiscounts";
        public string AvailableShippingOptionsClass { get; } = "available-shipping-options";
        public string AvailableShippingOptionsNameClass { get; } = "available-shipping-options__name";
        public string BtnApplyDiscountClass { get; } = "btnApplyDiscount";
        public string CartContentClass { get; } = "cartContent";
        public string CartEmptyWarningClass { get; } = "cartEmptyWarning";
        public string CartIdElementId { get; } = "cartId";
        public string CartSuggestedProductsContainerId { get; } = "cartMoreYouMayLikeContainer";
        public string CheckOutNowClass { get; } = "checkOutNow";
        public string AddPromoCodeLinkClass { get; } = "addPromoCodeLink";
        public string DeliveryOptionsCountryClass { get; } = "delivery-options__country";
        public string DeliveryOptionsZipBtnClass { get; } = "delivery-options__zipBtn";
        public string DeliveryOptionsContainerClass { get; } = "delivery-options-container";
        public string DiscountTooltipClass { get; } = "discountTooltip";
        public string EditPriceClass { get; } = "editPrice";
        public string ItemTotalClass { get; } = "itemTotal";
        public string JsCloseShippingOptionsOverlayClass { get; } = "jsCloseShippingOptionsOverlay";
        public string JsDeliveryOptionsTabClass { get; } = "jsDeliveryOptionsTab";
        public string JsInventoryZipApplyClass { get; } = "jsInventoryZipApply";
        public string JsShippingCountryClass { get; } = "jsShippingCountry";
        public string JsShowPromoCodeTermsAndConditionsClass { get; } = "jsShowPromoCodeTermsAndConditions";
        public string FieldRadioClass { get; } = "fieldRadio";
        public string JsShippingCostClass { get; } = "available-shipping-options__cost";
        public string JsShippingDaysClass { get; } = "available-shipping-options__days";
        public string JsShipZipApplyClass { get; } = "jsShipZipApply";
        public string JsStoreZipApplyClass { get; } = "jsStoreZipApply";
        public string JsUpdateShipBtnClass { get; } = "jsUpdateShipBtn";
        public string JsUpdateStoreInventoryBtnClass { get; } = "jsUpdateStoreInventoryBtn";
        public string JsZipForStoreClass { get; } = "jsZipForStore";
        public string JsZipForInventoryClass { get; } = "jsZipForInventory";
        public string LimitedQuantityCalloutClass { get; } = "limitedQuantityCallout";
        public string LnkRemoveDiscountClass { get; } = "lnkRemoveDiscount";
        public string ManualDiscountManagerApprovalClass { get; } = "manualDiscountManagerApproval";
        public string PosPurchaseOptionCheckboxClass { get; } = "pos-purchase-option__checkbox";
        public string ProfessionalAccountLabelId { get; } = "professionalAccountLabel";
        public string PromoCodeTextClass { get; } = "promoCodeText";
        public string ProdImageCellClass { get; } = "prodImageCell";
        public string ProdQtyClass { get; } = "prodQtyDropdown";
        public string GiftCardQtyClass { get; } = "prodQty";
        public string PromoDiscountClass { get; } = "promoDiscount";
        public string RemoveCartItemBtnClass { get; } = "removeCartItemBtn";
        public string RemovePromoCodeClass { get; } = "removePromoCode";
        public string RemovePromoCodeXpath { get; } = "//*[@id='regionRemovePromoCode']/button";
        public string ProductNameClass { get; } = "productName";
        public string PromoInputClass { get; } = "promoInput";
        public string RemoveItemClass { get; } = "removeItem";
        public string RemoveProfessionalAccountId { get; } = "removeProfessionalAccount";
        public string SelDiscountReasonClass { get; } = "selDiscountReason";
        public string SelectDeliveryOptionsModalId { get; } = "selectDeliveryOptionsModal";
        public string ChangeShippingOptionsOverlayId { get; } = "changeShippingOptionsOverlay";
        public string JsChangeShippingOptionsClass { get; } = "jsChangeShippingOptions";
        public string ShippingOptionsChangedContainerClass { get; } = "shippingOptionsChangedContainer";
        public string ShipZipFieldId { get; } = "shipZipField";
        public string ShowUpTooltipClass { get; } = "showUp";
        public string StorePickupListNameClass { get; } = "store-pickup-list__name";
        public string StyleNumberClass { get; } = "styleNumber";
        public string TxtPercentDiscClass { get; } = "txtPercentDisc";
        public string UpperCaseClass { get; } = "upperCase";
        public string ProceedToPaymentClass { get; } = "proceedToPayment";
        public string ProductItemsId { get; } = "productItems";
        public abstract string CartId { get; }
        public abstract string CartPromotionalCodeId { get; }
        public abstract string ChangeShippingOptionsClass { get; }
        public abstract string ShippingAndReturnType { get; }
        public abstract string ShowUpTooltipXpath { get; }
        public abstract string SubTotalOnCartClass { get; }
        public abstract string CartTitleXpath { get; }
        public abstract string ProductQtyFieldClass { get; }
        public abstract string ProductQtyDropdownXpath { get; }
        public abstract string ShippingOptionContainerClass { get; }
        #endregion

        #region Page Elements
        public abstract IElement SubTotalOnCart { get; }
        public IElement AlmostSoldOutCallout => Browser.Locate.ElementByClassName(LimitedQuantityCalloutClass);
        public IElement ApplyDiscountModal => Browser.Locate.ElementBySelector(ShowUpTooltipClass.ToCssClassSelector());
        public IElement ChangeShippingOptionsLinkByIndex(int index) => Browser.Locate.ElementsByClassName(JsChangeShippingOptionsClass)[index];
        public IElement RemoveButton => Browser.Locate.ElementByClassName(RemoveCartItemBtnClass);
        public IElement CartMoreYouMayLikeContainer => Browser.Locate.ElementById(CartSuggestedProductsContainerId);
        public IElement CartOverviewElement => Browser.Locate.ElementByClassName(CartContentClass);
        public IElement CartTitle => Browser.Locate.ElementByXpath(CartTitleXpath);
        public IElement CheckOutNowButton => Browser.Locate.ElementBySelector(CheckOutNowClass.ToCssClassSelector());
        public IElement DeliveryOptionsContainer => Browser.Locate.ElementByClassName(DeliveryOptionsContainerClass);
        public IElement CartIdElement => Browser.Locate.ElementById(CartIdElementId);
        public IElement GiftCardQtyField => Browser.Locate.ElementByClassName(GiftCardQtyClass);
        public IElement ProductImageAnchorWebElement(int index) => Browser.Locate.ElementByTagName(HtmlTextWriterTag.A, Browser.Locate.ElementsBySelector(ProdImageCellClass.ToCssClassSelector())[index]);
        public IElement ProductNameLabel(int index) => Browser.Locate.ElementBySelector(ProductNameClass.ToCssClassSelector(), Browser.Locate.ElementBySelector($"{ProductItemsId.ToCssIdSelector()} > {HtmlTextWriterTag.Div} > {HtmlTextWriterTag.Ul} > {HtmlTextWriterTag.Li.ToNthChildSelector(index + 1)}"));
        public IElement ProductSkuLabel(int index) => Browser.Locate.ElementBySelector(StyleNumberClass.ToCssClassSelector(), Browser.Locate.ElementBySelector($"{ProductItemsId.ToCssIdSelector()} > {HtmlTextWriterTag.Div} > {HtmlTextWriterTag.Ul} > {HtmlTextWriterTag.Li.ToNthChildSelector(index + 1)}"));
        public IElement ProductTotalCostLabel(int index) => Browser.Locate.ElementsByClassName(ItemTotalClass)[index];
        public IElement ProductPromoDiscount(int index) => Browser.Locate.ElementsByClassName(GlobalLocators.OsValueClass)[index];
        public IElement ProductTax(int index) => Browser.Locate.ElementsByClassName(GlobalLocators.OsValueClass)[index];
        public IElement ShippingReturnType => Browser.Locate.ElementByClassName(ShippingAndReturnType);
        public IElement ShippingOptions(int index) => Browser.Locate.ElementsBySelector(AvailableShippingOptionsClass.ToCssClassSelector())[index];
        public IElement OrderSummaryBlockLabel(string heading) => OrderSummaryTotalLabels().FirstOrDefault(e => e.Text.StartsWith(heading, StringComparison.OrdinalIgnoreCase));
        public IElement ProductQuantityLabel(int index) => Browser.Locate.ElementBySelector(ProdQtyClass.ToCssClassSelector(), Browser.Locate.ElementBySelector($"{ProductItemsId.ToCssIdSelector()} > {HtmlTextWriterTag.Div} > {HtmlTextWriterTag.Ul} > {HtmlTextWriterTag.Li.ToNthChildSelector(index + 1)}"));
        public IElement CartItemRemoveLinkElement(int index) => Browser.Locate.ElementsByClassName(RemoveItemClass)[index];
        public IElement RemovePromoCodeElement => Browser.Locate.ElementByXpath(RemovePromoCodeXpath);
        public IElement CountryInputField => Browser.Locate.ElementByClassName(DeliveryOptionsCountryClass);
        public IElement ShipTabSearchButton => Browser.Locate.ElementByClassName(DeliveryOptionsZipBtnClass);
        public IElement PromoCodeLabel => Browser.Locate.ElementBySelector(PromoCodeTextClass.ToCssClassSelector());
        public IElement ProductSkuLabelCart => Browser.Locate.ElementBySelector(StyleNumberClass.ToCssClassSelector());
        public IElement PromoInputField => Browser.Locate.ElementByClassName(PromoInputClass);
        public IElement PromoCodeText => Browser.Locate.ElementByClassName(UpperCaseClass, PromoCodeLabel, true);
        public IElement ShippingCountryDropdown => Browser.Locate.ElementByClassName(JsShippingCountryClass);
        public IElement ShipZipApplyBtn => Browser.Locate.ElementByClassName(JsShipZipApplyClass);
        public IElement StorePickupList(int index) => Browser.Locate.ElementsByClassName(StorePickupListNameClass)[index];
        public IElement TaxLabel => OrderSummaryBlockLabel(TaxTotalPrefix);
        public IElement TaxLabelOnOrderConfirmationPage => OrderSummaryBlockLabel(TaxTotalPrefixOnOrderConfirmationPage);
        public IElement ShippingAndProcessingLabel => OrderSummaryBlockLabel(ShippingTotalPrefix);
        public IElement DeliveryPolicyAgreementProceedToPayment => Browser.Locate.ElementByClassName(ProceedToPaymentClass);
        public IElement GiftCardDetails(int index) => Browser.Locate.ElementsByClassName(GiftCardInfoItemClass)[index];
        public IElement BoldChatContainer => Browser.Locate.ElementByClassName(BoldChatContainerClass);

        //Elements that exist in both Desktop and Mobile views but are located differently.

        public abstract IElement SaleCountDown { get; }
        public abstract IElement ShippingOptionContainer { get; }
        public abstract IElement ApplyDiscountButton { get; }
        public abstract IElement CartEditPriceElement { get; }
        public abstract IElement CartIdContainer { get; }
        public abstract IElement CartPromotionalButton { get; }
        public abstract IElement CloseButton { get; }
        public abstract IElement CartSignInLink { get; }
        public abstract IElement CloseShippingOptionElement { get; }
        public abstract IElement CompanyNameField { get; }
        public abstract IElement DiscountDropdown { get; }
        public abstract IElement DiscountPercentTextBox { get; }
        public abstract IElement DiscountTooltip { get; }
        public abstract IElement DiscountTooltipRemoveButton { get; }
        public abstract IElement MobileRemovePromoCode { get; }
        public abstract IElement ProfessionalAccountLabel { get; }
        public abstract IElement PromoCode { get; }
        public abstract IElement StoreInventoryUpdateButton { get; }
        public abstract IElement StoreInventoryZipField { get; }
        public abstract IElement StoreInventorySearchButton { get; }
        public abstract IElement ShipZipField { get; }
        public abstract IElement CompanyNameLink { get; }
        public abstract IElement StorePickupElement { get; }
        public abstract IElement StoreInventoryTab { get; }
        public abstract IElement StorePickupZipField { get; }
        public abstract IElement ShippingOptionModal { get; }
        public abstract IElement SelDiscountReasonField { get; }
        public abstract IElement TextPercentDiscountField { get; }
        public abstract IElement RemoveProfessionalAccountLink { get; }
        public abstract IElement AddProfessionalAccountLink { get; }
        public abstract IElement SubTotalLabel { get; }
        public abstract IElement CloseShippingOptionsOverlay { get; }
        public abstract IElement AuthorizationModalUsernameInput { get; }
        public abstract IElement AuthorizationModalPasswordInput { get; }
        public abstract IElement ModalSubmitButton { get; }
        public abstract IElement AuthorizationModalErrorText { get; }
        public abstract ReadOnlyCollection<IElement> PosCheckBoxes { get; }
        public abstract ReadOnlyCollection<IElement> ShippingTypeRadios { get; }
        public abstract IElement ChangeShippingOptionsLink { get; }
        public abstract IElement ShippingOptionsRadioButton { get; }
        public abstract IElement ShippingZipField { get; }
        public abstract IElement UpdateShipButton { get; }
        public abstract IElement StorePickupSearchButton { get; }
        public abstract IElement StorePickupUpdateButton { get; }
        public abstract IElement ShowUpTooltip { get; }
        public abstract IElement ProductQtyDropdownField { get; }
        public abstract IElement ProductQtyField { get; }
        public abstract ReadOnlyCollection<IElement> RemoveItemLinksElements { get; }
        public ReadOnlyCollection<IElement> AvailableShippingOptions => Browser.Locate.ElementsByClassName(AvailableShippingOptionsNameClass);
        public ReadOnlyCollection<IElement> ShippingCostLabels => Browser.Locate.ElementsByClassName(JsShippingCostClass);
        public ReadOnlyCollection<IElement> ShippingDaysLabels => Browser.Locate.ElementsByClassName(JsShippingDaysClass);
        public ReadOnlyCollection<IElement> OrderSummaryTotalLabels() => Browser.Locate.ElementsByXpath("//div[contains(@class,'osLabel')]");
        public ReadOnlyCollection<IElement> OrderSummaryTotalValues() => Browser.Locate.ElementsByClassName(GlobalLocators.OsValueClass);
        #endregion

        #region Page Methods
        private void OpenLpModalIfNotOpen()
        {
            if (IsLpModalDisplayed()) return;
            ChangeShippingOptionsLinkByIndex(0).Click();
            Browser.Wait.ForCondition(IsLpModalDisplayed);
        }

        /// <inheritdoc />
        public void ClickCheckOutNowButton()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(CheckOutNowClass.ToCssClassSelector()));
            CheckOutNowButton.Click();
        }

        /// <inheritdoc />
		public void ClickShippingOptionShipTabSearchButton()
        {
            ShipTabSearchButton.Click();
            Browser.Wait.ForElementToStopAnimating(ShippingOptionContainer);
        }

        /// <inheritdoc />
		public void AddProfessionalAccount(string company)
        {
            AddProfessionalAccountLink.Click();

            Browser.Wait.IsVisibleElement(By.Id(GlobalLocators.LpModalId));

            var cachedElem = Browser.Locate.ElementByTagName(HtmlTextWriterTag.Body);

            CompanyNameField.SendKeys(company);
            Browser.Locate.ElementByClassName(GlobalLocators.CalloutBtnClass, GlobalLocators.Iframe).Click();
            Browser.Wait.IsVisibleElement(By.ClassName(ApaResultsContainerClass));
            CompanyNameLink.Click();

            Browser.Wait.UntilElementUnloads(cachedElem);
            Browser.Wait.ForDomReady();
        }

        /// <inheritdoc />
		public void RemoveProfessionalAccount()
        {
            if (RemoveProfessionalAccountLink != null && RemoveProfessionalAccountLink.Displayed)
            {
                var cachedElem = Browser.Locate.ElementByTagName(HtmlTextWriterTag.Body);

                RemoveProfessionalAccountLink.Click();

                Browser.Wait.UntilElementUnloads(cachedElem);
                Browser.Wait.ForDomReady();
            }
        }

        /// <inheritdoc />
		public void ClickShippingOptionShipTabUpdateButton()
        {
            UpdateShipButton.Click();
            Thread.Sleep(3000); // TODO: This can be replaced with a Browser.Wait.ForElement on the given page.
        }

        /// <inheritdoc />
		public void ChangeShippingZipCode(string zip = ZipCodeList.Chatsworth)
        {
            OpenLpModalIfNotOpen();
            ShippingZipField.Clear();
            ShippingZipField.SendKeys(zip);
            ClickShippingOptionShipTabSearchButton();
            ClickShippingOptionShipTabUpdateButton();
            Browser.Wait.ForCondition(() => !IsLpModalDisplayed());
        }

        /// <inheritdoc />
		public bool IsLpModalDisplayed() { return Browser.Locate.ElementImmediately(GlobalLocators.LpModalId.ToCssIdSelector()).IsInitialized; }

        /// <inheritdoc />
		public string GetShippingCellShippingCost(bool removeCurrencySign = true)
        {
            var shippingCellShippingCost = Shipping.ShippingCellShippingCost.Text;

            return removeCurrencySign ? shippingCellShippingCost.ToLower().Replace("$", string.Empty).Replace("* cad", "") : shippingCellShippingCost;
        }

        /// <inheritdoc />
        public string GetPromoCodeLabel()
        {
            var promoCodeLineLabel = OrderSummaryBlockLabel(PromoCodeDiscountTotalPrefix).Text; //index of promo code label
            promoCodeLineLabel = promoCodeLineLabel.Replace("\r\n", " ");

            return promoCodeLineLabel;
        }

        /// <inheritdoc />
		public string GetProfessionalSavingsLabel()
        {
            var professionalSavingsLabel = OrderSummaryBlockLabel(ProfessionalSavingsLabel).Text; //index of professional savings label
            professionalSavingsLabel = professionalSavingsLabel.Replace("\r\n", string.Empty);

            return professionalSavingsLabel;
        }

        /// <inheritdoc />
		public string GetAdditionalDiscountsLabel()
        {
            var additionalDiscountLabel = OrderSummaryBlockLabel(AdditionalDiscountsLabel).Text; //index of additional discount label
            additionalDiscountLabel = additionalDiscountLabel.Replace("\r\n", string.Empty);

            return additionalDiscountLabel;
        }

        #region Element Text
        public string ProductName(int index) => ProductNameLabel(index).Text;
        public string ProductQtyDesktopLabel => ProductQtyDropdownField.GetAttribute("value");
        public string ProductQtyMobileLabel => ProductQtyField.GetAttribute("value");
        public string ProductSku(int index) => ProductSkuLabel(index).Text.Split(SingleSpaceChart)[2];
        public string ProductTotalPrice(int index) => ProductTotalCostLabel(index).Text;
        public string ProductSkuCart => ProductSkuLabelCart.Text.Split(SingleSpaceChart)[2];
        public string PromoDiscountPrice(int index) => ProductPromoDiscount(index).Text;
        public string TaxPrice(int index) => ProductTax(index).Text;
        public string ProductQuantity(int index) => ProductQuantityLabel(index).GetAttribute(HtmlTextWriterAttribute.Value.ToString());
        #endregion

        /// <inheritdoc />
        public int UniqueProductsCount => Browser.Locate.ElementsByXpath("//div[contains(@class,'itemTotal')]").Count;

        public decimal GetAdditionalDiscountsWithPrefix()
        {
            return GetOrderSummaryCost(AdditionalDiscountsLabel, "$");
        }

        /// <inheritdoc />
        public decimal GetProductTotal()
        {
            return GetOrderSummaryCost(ProductTotalPrefix, "$");
        }

        /// <inheritdoc />
        public decimal GetSaleTax()
        {
            return GetOrderSummaryCost(TaxTotalPrefix, "$");
        }

        /// <inheritdoc />
        public decimal GetSubTotal()
        {
            return GetOrderSummaryCost(SubtotalPrefix, "$");
        }

        /// <inheritdoc />
		public decimal GetShippingCost()
        {
            return GetOrderSummaryCost(ShippingTotalPrefix, "$");
        }

        /// <inheritdoc />
		public string GetShippingCostValue()
        {
            return GetOrderSummaryCostValue(ShippingTotalPrefix);
        }

        /// <inheritdoc />
		public decimal GetOrderTotalCost()
        {
            return GetOrderSummaryCost(OrderTotalPrefix, "$");
        }

        private decimal GetOrderSummaryCost(string labelTextPrefix, string prefixToRemove = null)
        {
            var cost = GetOrderSummaryCostValue(labelTextPrefix);
            return GetLabelCost(cost, prefixToRemove);
        }

        protected decimal GetLabelCost(string cost, string prefixToRemove = null)
        {
            if (prefixToRemove != null)
                cost = cost.Replace(prefixToRemove, string.Empty).Replace("CAD", string.Empty).Trim();

            cost = TextActions.RemoveWhitespace(cost);

            decimal.TryParse(Regex.Replace(cost, @" \D+", string.Empty),
                NumberStyles.Currency, CultureInfo.CurrentCulture, out var result);

            return result;
        }

        /// <inheritdoc />
		public string GetOrderSummaryCostValue(string labelTextPrefix)
        {
            int index = 0;
            Browser.Wait.AreAllElementsVisible(By.XPath("//div[contains(@class,'osLabel')]"));
            var orderSummaryTotalLabels = OrderSummaryTotalLabels();
            var orderSummaryTotalValues = OrderSummaryTotalValues();

            for (; index < orderSummaryTotalLabels.Count; index++)
            {
                var orderSummaryTotalLabel = orderSummaryTotalLabels[index].Text;

                if (orderSummaryTotalLabel.StartsWith(labelTextPrefix, StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
            }

            if (index < 0 || index >= orderSummaryTotalValues.Count)
                return string.Empty;

            return orderSummaryTotalValues[index].Text;
        }

        /// <inheritdoc />
        public List<Utilities.ProductModel> GetListOfAllProductsOnPage()
        {
            ProductsInCartList.Clear(); // Clear the list since it will be rebuilt below.

            for (var index = 0; index < UniqueProductsCount; index++) { ProductsInCartList.Add(new Utilities.ProductModel(ProductName(index), ProductSku(index), ProductQuantity(index), ProductTotalPrice(index))); }

            return ProductsInCartList;
        }
       
        /// <inheritdoc />
        public abstract bool DoesCartMatchAddedProducts(Dictionary<string, int> addedProducts);
        
        /// <summary>
        /// Lists all the products in the cart.
        /// </summary>
		public List<Utilities.ProductModel> ProductsInCartList;

        /// <inheritdoc />
        public decimal GetShippingTotals(int index)
        {
            var total = PromoDiscountPrice(index);
            var zero = 0;

            total = Regex.Replace(total, "[^0-9.]", string.Empty);

            return total == "" ? zero : decimal.Parse(total);
        }

        public decimal GetTaxTotal(int index)
        {
            var total = TaxPrice(index);
            var zero = 0;

            total = Regex.Replace(total, "[^0-9.]", string.Empty);

            return total == "" ? zero : decimal.Parse(total);
        }

        public decimal GeProductTotal(int index)
        {
            var total = TaxPrice(index);
            var zero = 0;

            total = Regex.Replace(total, "[^0-9.]", string.Empty);

            return total == "" ? zero : decimal.Parse(total);
        }

        /// <inheritdoc />
		public decimal GetDiscountedPrice(decimal itemPrice, decimal discountRate)
        {
            return (itemPrice - Math.Floor((itemPrice * discountRate / 100) * 100) / 100);
        }

        /// <inheritdoc />
		public void UncheckAllPosCheckboxes()
        {
            foreach (var element in Browser.Locate.SelectedElements(PosCheckBoxes))
            {
                Browser.Wait.ForElement(element, 3).Click();
                Browser.Wait.UntilElementUnloads(element, 3);
            }
        }

        /// <summary>
        /// Select Store inventory shipping option
        /// </summary>
        /// <param name="index"></param>
        /// <param name="zipCode"></param>
        public void SelectStoreInventoryShippingOption(int index, string zipCode)
        {
            Browser.Wait.ForClickableElement(ChangeShippingOptionsLinkByIndex(index)).Click();

            Browser.Wait.ForElement(ShippingOptionModal);

            Browser.Wait.ForClickableElement(StoreInventoryTab).Click();

            Browser.Wait.ForDisplayedElement(StoreInventoryZipField);
            StoreInventoryZipField.Clear();
            StoreInventoryZipField.SendKeys(zipCode);
            StoreInventorySearchButton.Click();

            Thread.Sleep(2500); // wait for search

            Browser.Wait.ForDisplayedElement(StorePickupList(0)).Click();
            Browser.Wait.ForClickableElement(StoreInventoryUpdateButton).Click();

            Thread.Sleep(2500); // wait for modal animation

        }

        /// <summary>
        /// Select Store pick up shipping option
        /// </summary>
        /// <param name="index"></param>
        /// <param name="zipCode"></param>
        public void SelectStorePickupShippingOption(int index, string zipCode)
        {
            Browser.Wait.ForClickableElement(ChangeShippingOptionsLinkByIndex(index), 30).Click();

            Browser.Wait.ForElement(ShippingOptionModal);

            Browser.Wait.ForClickableElement(StorePickupElement).Click();

            Browser.Wait.ForDisplayedElement(StorePickupZipField);
            StorePickupZipField.Clear();
            StorePickupZipField.SendKeys(zipCode);
            StorePickupSearchButton.Click();

            Thread.Sleep(2500); // wait for search

            Browser.Wait.ForDisplayedElement(StorePickupList(0)).Click();
            Browser.Wait.ForClickableElement(StorePickupUpdateButton).Click();

            Thread.Sleep(2500); // wait for modal animation
        }

        /// <summary>
        /// Change country in shipping options. Smart enough to know if modal is already open or if it should open it.
        /// </summary>
        /// <param name="countryDropdownOptionValue">Country value in dropdown (not the label/name)</param>
        public void ChangeShippingCountry(string countryDropdownOptionValue)
        {
            OpenLpModalIfNotOpen();
            Browser.Locate.ClickDropdownByValue(CountryInputField, countryDropdownOptionValue);
        }

        /// <summary>
        /// Change both the country and zip code in shipping options modal.
        /// </summary>
        /// <param name="countryDropdownOptionValue">Country value in dropdown (not the label/name)</param>
        /// <param name="zip">Zip code of area.</param>
        public void ChangeShippingCountryAndZip(string countryDropdownOptionValue, string zip)
        {
            ChangeShippingCountry(countryDropdownOptionValue);
            ChangeShippingZipCode(zip);
        }

        public abstract void ApplyPromoCode();

        public abstract void RemovePromoCode();

        public abstract void WaitForPromoCodeToUnload();
        public abstract void ApplyDiscountIosPlatform(string discountApplied, string discountReasonApplied);

        /// <inheritdoc />
        public abstract decimal GetActualPromoCodeDiscountPrice();

        public abstract decimal GetProfessionalSavingsPrice();

        public abstract decimal GetAdditionalDiscountsPrice();

        public abstract void ApplyDiscount(string discountApplied, string discountReasonApplied, OperatingSystem operatingSystem);

        /// <inheritdoc />
        public abstract decimal GetActualPromoCodeDiscount();

        #endregion
    }
}
