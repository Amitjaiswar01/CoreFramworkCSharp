using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;
using Automation.Framework;
using Automation.Framework.Core;
using Automation.Framework.Utilities;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Databases.Actions;
using LampsPlus.AutomationFramework.Pages.Refactored.Modal;
using LampsPlus.AutomationFramework.Utilities;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Cart
{
    public class CartDesktop : ICartDesktop
    {
        //Class members
        private string _jsAvailableShippingOptionsContainerClass = "jsAvailableShippingOptionsContainer";
        private string _csrCartTitleId = "csrCartTitle";
        private string _deliveryOptionsZipBtnClass = "delivery-options__zipBtn";
        private string _jsUpdateShipBtnClass = "jsUpdateShipBtn";
        private string _removeCartItemBtnClass = "removeCartItemBtn";
        private string _checkOutNowClass = "checkOutNow";
        private string _orderTotalPrefix = "Order Total";
        private string _osLabelClass = "osLabel";
        private string _osValueClass = "osValue";
        private string _saleCountdownClass = "saleCountdown";
        private string _paypalButtonClass = "paypal-buttons";
        private string _posPurchaseOptionCheckboxClass = "pos-purchase-option__checkbox";
        private string _productTotalPrefix = "Product Total";
        private string _promoCodeLineClass = "promoCodeLine";
        private string _shippingAndProcessingPrefix = "Shipping & Processing";
        private string _shipZipFieldId = "shipZipField";
        private string _shippingCellShippingCostClass = "shippingCell__shippingCost";
        private string _taxTotalPrefix = "Estimated Tax ¹";
        private string _cartTaxTotalPrefix = "Tax ¹";
        private string _orderTaxTotalPrefix = "Tax¹";
        private string _productQtyDropdownXpath = "//select[@class='prodQtyDropdown']";
        private string _selectDeliveryOptionsModalId = "selectDeliveryOptionsModal";
        private string _availableShippingOptionsDaysClass = "available-shipping-options__days";
        private string _moreDetailsInventoryClass = "moreDetailsInventory";
        private string _emailOptionsModalId = "emailOptionsModal";
        private string _orderSummaryContainerId = "orderSummaryContainer";
        private string _jsCartLinkClass = "jsCartLink";
        private string _cartLinkId = "cartLink";
        private string _copyCartLinkClass = "copyCartLink";
        private string _editPriceClass = "editPrice";
        private string _discountTooltipClass = "discountTooltip";
        private string _btnApplyDiscountClass = "btnApplyDiscount";
        private string _selDiscountReasonClass  = "selDiscountReason";
        private string _toggleMoreDetailsXpath = "//button[contains(@class,'toggleMoreDetails')]";
        private string _addPromoCodeLinkClass = "addPromoCodeLink";
        private string _promoInputClass = "promoInput";
        private string _promoButtonClass = "promoButton";
        private string _cartSuggestedProductsContainerId  = "cartMoreYouMayLikeContainer";
        private string _toolTipXpath = "//*[@id='cartOverview']//div[contains(@class, 'showUp')]";
        private string _selectAllOrNoneClass = "selectAllOrNone";
        private string _rdoLargeImagesId = "rdoLargeImages";
        private string _shortSkuName = "ShortSku";
        private string _textMarginClass = "txtMargin";
        private string _removeAndShippingContainerClass = ".removeAndShippingContainer>div.regionShipping.hidden";
        private string _promoCodeInputErrorId = "promoCodeInput-error";
        private string _lastNameId = "LastName";
        private string _fromId = "From";
        private string _postalCodeId = "PostalCode";
        private string _thankYouMessageClass = "thankYouMessage";
        private string _jsEmailClass = "jsEmail";
        private string _prodImageCellClass = "prodImageCell";                                                               
        private string _paypalButtonContainerClass  = "paypalButtonContainer";
        private string _jsPrintClass = "jsPrint";
        private string _txtPercentDiscClass = "txtPercentDisc";
        private string _showUpTooltipXpath = "//*[@id='cartOverview']//div[contains(@class, 'showUp')]";
        private string _txtDiscPriceClass = "txtDiscPrice";
        private string _additionalDiscountsClass = "additionalDiscounts";
        private string _subtotalPrefix = "Subtotal";
        private string _availableShippingOptionsErrorClass  = "available-shipping-options__error";
        private string _cartErrorModalId = "cartErrorModal";
        private string _addSkuContainerId = "addSkuContainer";
        private string _addSkuAddLinkClass = "addSkuAddLink";
        private string _fieldRadioClass = "fieldRadio";
        private string _lnkRemoveDiscountClass = "lnkRemoveDiscount";
        private string _jsShipZipApplyClass = "jsShipZipApply";
        private string _removePromoCodeXpath = "//*[@id='regionRemovePromoCode']/button";
        private string _jsShowPromoCodeTermsAndConditionsClass = "jsShowPromoCodeTermsAndConditions";
        private string _upperCaseClass  = "upperCase";
        private string _promoCodeTextClass = "promoCodeText";
        private string _vaCommentId = "vaComment_";
        private string _additionalDiscountsLabel = "Additional Discounts:";
        private string _productItemClass = "productItem";
        private string _dataOptionAttribute = "data-option";
        private string _deleteString = "delete";
        private string _cartOptionsClass = "cartOptions";
        private string _btnDeleteClass = "btnDelete";
        private string _shippingTotalPrefix = "Shipping & Processing";
        private string _invalidShortSku = "99999";
        private string _posPurchaseOptionClass = "pos-purchase-option";
        private string _allInStoreClass = "allInStore";
        private string _selectNoneLinkXpath = "//button[text()='None']";
        private string _rdoSmallImagesId = "rdoSmallImages";
        private string _printCartBtnClass = "printCartBtn";
        private string _printOptionsContainerId = "printOptionsContainer";
        private string _printInStoreOptionBtnId = "printInStoreOptionBtn";
        private string _printOutputId = "printOutput";
        private string _productLinkClass = "productLink";
        private string _styleNumberClass = "styleNumber";
        private string _itemTotalClass = "itemTotal";
        private string _jsShippingCountryClass = "jsShippingCountry";
        private string _emailRecipientsXpath = "//label[@for='EmailRecipients']";
        private string _cartEmptyBodyClass = "cartEmptyBody";
        private string _limitedQuantityCalloutClass = "limitedQuantityCallout";
        private string _editOrderClass = "editOrder";
        private string _productItemWrapClass = "productItemWrap";
        private string _payPalLaterWidgetId = "paypalLaterWidget";
        private string _deliveryOptionsContainerClass = "delivery-options-container";
        private string ProductQtyDesktopLabel => ProductQtyDropdownField(0).GetAttribute("value");
        private string ItemTotalPrice(int index) => ProductOriginalTotalCost(index).Text;
        private string ProductQty(int index) => ProductQuantity(index).GetAttribute("value");
        private string PromoDiscountPrice(int index) => ProductPromoDiscount(index).Text;
        private string ChangeShippingOptionsLinkClass(int index) => $"jsChangeShippingOptions:nth-of-type({index})";
        private string RecurringDataIssue => "Recurring Data Issue: ";
        private string InvalidPromoCode => "1234";
        private char SingleSpaceChar => ' ';
        private int GetAllCartSkuPosCheckBoxCount => Browser.Locate.ElementsByClassName(_posPurchaseOptionCheckboxClass).Count;

        protected string AvailableShippingOptionsClass => "available-shipping-options";
        protected string JsChangeShippingOptionsClass => "jsChangeShippingOptions";
        protected string ProductNameClass => "productName";
        protected string RemoveItemClass => "removeItem";                                   
        protected string CartEmptyWarningClass => "cartEmptyWarning";
        protected string EmailRecipientsId => "EmailRecipients";
        protected string FirstNameId => "FirstName";
        protected string UndoBlockClass => "undoBlock";
        protected string JsShippingCostClass => "available-shipping-options__cost";
        protected string ProductTotalPrice(int index) => ProductTotalCostLabel(index).Text;
        protected string ProductName(int index) => ProductNameLabel(index).Text;
        protected string ProductSku(int index) => ProductSkuLabel(index).Text.Split(SingleSpaceChar)[2];
        private IElement PayPalLaterWidget => Browser.Locate.ElementById(_payPalLaterWidgetId);
        private IElement LimitedQuantityCallout => Browser.Locate.ElementByClassName(_limitedQuantityCalloutClass);
        private IElement PosCheckBoxAndLabel(int index)=> Browser.Locate.ElementsBySelector(_posPurchaseOptionCheckboxClass.ToCssClassSelector())[index];
        private IElement PaypalButton => Browser.Locate.ElementByClassName(_paypalButtonClass);
        private IElement TextMarginArea => Browser.Locate.ElementByClassName(_textMarginClass);
        private IElement ProductPromoDiscount(int index) => Browser.Locate.ElementsByClassName(_osValueClass)[index];
        private IElement PrintCartButton => Browser.Locate.ElementByClassName(_printCartBtnClass);
        private IElement PromotionsPrefixLabel => Browser.Locate.ElementByClassName(_promoCodeLineClass);
        private IElement SelectLargeImage => Browser.Locate.ElementById(_rdoLargeImagesId);
        private IElement SelectNoneLink => Browser.Locate.ElementBySelector(_selectAllOrNoneClass.ToCssClassSelector());
        private IElement PrintLink => Browser.Locate.ElementByClassNames(_jsPrintClass);
        private IElement ProductSkuLabel(int index) => Browser.Locate.ElementsBySelector(_styleNumberClass.ToCssClassSelector())[index];
        private IElement CartLink => Browser.Locate.ElementByClassName(_jsCartLinkClass);
        private IElement CartLinkText => Browser.Locate.ElementById(_cartLinkId);
        private IElement CartLinkCopyButton => Browser.Locate.ElementByClassName(_copyCartLinkClass);
        private IElement ProductTotalCostLabel(int index) => Browser.Locate.ElementsByXpath("//*[starts-with(@class, 'itemTotal')]/span")[index];
        private IElement CartEditPriceElement => Browser.Locate.ElementByClassName(_editPriceClass);
        private IElement DiscountDropdown => Browser.Locate.ElementByClassName(_selDiscountReasonClass, DiscountTooltip);
        private IElement DiscountPercentTextBox => Browser.Locate.ElementByClassName(_txtPercentDiscClass, DiscountTooltip);
        private IElement DiscountTooltip => Browser.Locate.ElementByClassName(_discountTooltipClass);
        private IElement ApplyDiscountButton => Browser.Locate.ElementByClassName(_btnApplyDiscountClass);
        private IElement ProductImageAnchorWebElement(int index) => Browser.Locate.ElementByTagName(HtmlTextWriterTag.A, Browser.Locate.ElementsBySelector(_prodImageCellClass.ToCssClassSelector())[index]);
        private IElement RemoveButton => Browser.Locate.ElementByClassName(_removeCartItemBtnClass);
        private IElement CartItemRemoveLinkElement(int index) => Browser.Locate.ElementsByClassName(RemoveItemClass)[index];
        private IElement CheckOutNowButton => Browser.Locate.ElementBySelector(_checkOutNowClass.ToCssClassSelector());
        private IElement ToggleMoreDetailsButton => Browser.Locate.ElementByXpath(_toggleMoreDetailsXpath);
        private IElement PromoCodeInputField => Browser.Locate.ElementByClassName(_promoInputClass);
        private IElement PromoCodeUpdate => Browser.Locate.ElementByClassName(_promoButtonClass);
        private IElement ToolTip => Browser.Locate.ElementByXpath(_toolTipXpath);
        private IElement EmailButton => Browser.Locate.ElementByClassName(_jsEmailClass);
        private IElement SendEmailButton => Browser.Locate.ElementByXpath("//*[@id='emailCartSubmitBtn']");
        private IElement FormZipCodeField => Browser.Locate.ElementById(_postalCodeId);
        private IElement PrintButton => Browser.Locate.ElementByClassName(_jsPrintClass);
        private IElement SmallImagesRadioBtn => Browser.Locate.ElementById(_rdoSmallImagesId);
        private IElement PrintCartBtn => Browser.Locate.ElementByClassName(_printCartBtnClass);
        private IElement AddByStyleShortSkuElement => Browser.Locate.ElementByName(_shortSkuName);
        private IElement AddSkuByStyleContainer => Browser.Locate.ElementByTagName(HtmlTextWriterTag.Input, AddSkuByStyle);
        private IElement AddSkuByStyle => Browser.Locate.ElementById(_addSkuContainerId);
        private IElement AddSkuByStyleLink => Browser.Locate.ElementByClassName(_addSkuAddLinkClass, AddSkuByStyle);
        private IElement DeleteCartLink => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Button, _dataOptionAttribute, _deleteString, Browser.Locate.ElementByClassName(_cartOptionsClass));
        private IElement ShippingOptionsError => Browser.Locate.ElementByClassName(_availableShippingOptionsErrorClass);
        private IElement EditPriceButton => Browser.Locate.ElementByClassName(_editPriceClass);
        private IElement ShowUpTooltip => Browser.Locate.ElementByXpath(_showUpTooltipXpath);
        private IElement TextPercentDiscountField => Browser.Locate.ElementByClassName(_txtPercentDiscClass, ShowUpTooltip);
        private IElement SelectDiscountReasonField => Browser.Locate.ElementByClassName(_selDiscountReasonClass, ShowUpTooltip);
        private IElement TextDiscountPriceField => Browser.Locate.ElementByClassName(_txtDiscPriceClass, ShowUpTooltip);
        private IElement AdditionalDiscountElement => Browser.Locate.ElementByClassName(_additionalDiscountsClass);
        private IElement DiscountTooltipRemoveButton => Browser.Locate.ElementByClassName(_lnkRemoveDiscountClass);
        private IElement DiscountVaComment => Browser.Locate.ElementByAttributeStartsWith(HtmlTextWriterAttribute.Id, _vaCommentId);
        private IElement DeleteCartButton => Browser.Locate.ElementByClassName(_btnDeleteClass);
        private IElement DeleteCartElement => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Button, _dataOptionAttribute, _deleteString, Browser.Locate.ElementByClassName(_cartOptionsClass));
        private IElement ProductOriginalTotalCost(int index) => Browser.Locate.ElementsByTagName(HtmlTextWriterTag.Strike)[index];
        private IElement OrderSummaryBlockLabel(string heading) => OrderSummaryTotalLabels().FirstOrDefault(e => e.Text.StartsWith(heading, StringComparison.OrdinalIgnoreCase));
        private IElement RemovePromoCodeElement => Browser.Locate.ElementByXpath(_removePromoCodeXpath);
        private IElement PromoCodeText => Browser.Locate.ElementByClassName(_upperCaseClass, PromoCodeLabel, true);
        private IElement EmailFormContainer => Browser.Locate.ElementByXpath("//div[contains(@class, 'overviewEmailFormContainer')]");
        private IElement MoreDetailsInventoryElement => Browser.Locate.ElementByClassName(_moreDetailsInventoryClass);
        private IElement InsertInHomeConsultInfoButton => Browser.Locate.ElementByXpath("//div[@class='messageInserts']/button");
        private IElement PromoCodeErrorMessage => Browser.Locate.ElementById(_promoCodeInputErrorId);
        private IElement EmailOptionModal => Browser.Locate.ElementById(_emailOptionsModalId);
        private IElement PaypalButtonDisable => Browser.Locate.ElementByClassName(_paypalButtonContainerClass);
        private IElement EditCartLink => Browser.Locate.ElementBySelector(_editOrderClass.ToCssClassSelector());
        private IElement ProductQtyDropdownField(int index) => Browser.Locate.ElementsByXpath(_productQtyDropdownXpath)[index];
        private IElement ProductNameLabel(int index) => Browser.Locate.ElementsBySelector(_productLinkClass.ToCssClassSelector())[index];
        private IElement ProductQuantity(int index) => Browser.Locate.ElementsByClassName("prodQty")[index];
        private ReadOnlyCollection<IElement> Stock(int index) => Browser.Locate.ElementsByClassName(_availableShippingOptionsDaysClass)[index].FindElements(By.TagName("strong"));
        private ReadOnlyCollection<IElement> ChildElements => EmailFormContainer.FindElements(By.XPath(".//*[normalize-space()][not(self::script)]"));

        protected IElement ShipTabSearchButton => Browser.Locate.ElementByClassName(_deliveryOptionsZipBtnClass);
        protected IElement ShipZipApplyBtn => Browser.Locate.ElementByClassName(_jsShipZipApplyClass);
        protected IElement ShipZipField => Browser.Locate.ElementById(_shipZipFieldId);
        protected IElement ModalTimeCheck(int index) => Browser.Locate.ElementsByClassName(_availableShippingOptionsDaysClass)[index];
        protected IElement CartMoreYouMayLikeContainer => Browser.Locate.ElementById(_cartSuggestedProductsContainerId);
        protected IElement FormEmailRecipientsField => Browser.Locate.ElementById(EmailRecipientsId);
        protected IElement FormFirstNameField => Browser.Locate.ElementById(FirstNameId);
        protected IElement FormLastNameField => Browser.Locate.ElementById(_lastNameId);
        protected IElement FormEmailFromField => Browser.Locate.ElementById(_fromId);
        protected IElement PromoCodeLabel => Browser.Locate.ElementBySelector(_promoCodeTextClass.ToCssClassSelector());
        protected IElement ShippingCountryDropdown => Browser.Locate.ElementByClassName(_jsShippingCountryClass);
        protected IElement SaleCountdown => Browser.Locate.ElementByClassName(_saleCountdownClass);
        protected IElement CartTitle => Browser.Locate.ElementById(_csrCartTitleId);
        protected virtual IElement UpdateShippingOptionButton => Browser.Locate.ElementByClassName(_jsUpdateShipBtnClass);
        protected virtual IElement ChangeShippingOptionsLink => Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.Button, JsChangeShippingOptionsClass);
        protected virtual IElement ShippingCellShippingCost => Browser.Locate.ElementByClassName(_shippingCellShippingCostClass);
        protected virtual IElement ShippingZipField => Browser.Locate.ElementById(_shipZipFieldId);
        protected virtual IElement AddPromoCodeLink => Browser.Locate.ElementByClassName(_addPromoCodeLinkClass);
        protected virtual IElement ShippingOptionModal => Browser.Locate.ElementById(_selectDeliveryOptionsModalId);
        protected virtual IElement CartIdContainer => Browser.Locate.ElementByXpath("//*[@id='cartId']");
        protected virtual IElement ShippingOptionsRadioContainer => Browser.Locate.ElementImmediately(_fieldRadioClass.ToCssClassSelector());

        private ReadOnlyCollection<IElement> OrderSummaryTotalLabels() => Browser.Locate.ElementsByClassName(_osLabelClass);
        private ReadOnlyCollection<IElement> OrderSummaryTotalValues() => Browser.Locate.ElementsByClassName(_osValueClass);
        private ReadOnlyCollection<IElement> ProductListItems => Browser.Locate.ElementsByClassName(_productItemClass);
        private ReadOnlyCollection<IElement> ShippingOptionsContainer => Browser.Locate.ElementsByClassName(JsChangeShippingOptionsClass);
        private ReadOnlyCollection<IElement> PosCheckBoxes => Browser.Locate.ElementsByClassName(_posPurchaseOptionCheckboxClass);
        private ReadOnlyCollection<IElement> ShippingDaysLabels => Browser.Locate.ElementsByClassName(_availableShippingOptionsDaysClass);
        private ReadOnlyCollection<IElement> ShippingCostLabels => Browser.Locate.ElementsByClassName(JsShippingCostClass);
        protected ReadOnlyCollection<IElement> RemoveItemLinksElements => Browser.Locate.ElementsBySelector(RemoveItemClass.ToCssClassSelector());
        protected virtual ReadOnlyCollection<IElement> ShippingTypeRadios => Browser.Locate.ElementsByClassName(_fieldRadioClass);
        protected virtual ReadOnlyCollection<IElement> ProductsCount => Browser.Locate.ElementsByClassName(_productItemWrapClass);

        protected List<ProductModel> ProductsInCartList;

        protected int UniqueProductsCount => Browser.Locate.ElementsByClassName(_itemTotalClass).Count;

        private void ConditionalOrderCheck(string checkText)
        {
            //Define current time and time difference
            var start = new TimeSpan(14, 00, 00);
            var end = new TimeSpan(23, 59, 59);
            var nowOrig = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 00);
            var timeDifference = start - nowOrig;
            var minutesMinusOne = timeDifference.Minutes - 1;

            //Conditional verification for orders based on current time
            if (DateTimeHelper.IsTimeInBetween(start, end, nowOrig))
            {
                Assert.Equals("if ordered by 2 PM Pacific", checkText, $"{RecurringDataIssue} Shipping callout is incorrect for product.");
            }
            else
            {
                if (timeDifference.Hours == 0 && timeDifference.Minutes <= 59)
                {
                    Assert.True($"{timeDifference.Minutes} Min." == checkText || $"{minutesMinusOne} Min." == checkText, $"{RecurringDataIssue}Shipping callout is incorrect for product.");
                }
                else
                    Assert.True($"{timeDifference.Hours} Hr. {timeDifference.Minutes} Min." == checkText || $"{timeDifference.Hours} Hr. {minutesMinusOne} Min." == checkText, $"{RecurringDataIssue}Shipping callout is incorrect for product.");
            }
        }

        private string GetOrderSummaryCostValue(string labelTextPrefix)
        {
            int index = 0;
            var orderSummaryTotalLabels = OrderSummaryTotalLabels();
            var orderSummaryTotalValues = OrderSummaryTotalValues();

            for (; index < orderSummaryTotalLabels.Count; index++)
            {
                if (orderSummaryTotalLabels[index].Text.StartsWith(labelTextPrefix, StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
            }

            if (index < 0 || index >= orderSummaryTotalValues.Count)
                return string.Empty;

            return orderSummaryTotalValues[index].Text;
        }

        public void WaitForUndoLinkToDisappear()
        {
            Browser.Wait.IsInvisibleElement(By.ClassName(UndoBlockClass));
        }

        private decimal GetOrderSummaryCost(string labelTextPrefix, string prefixToRemove = null)
        {
            var cost = GetOrderSummaryCostValue(labelTextPrefix).TrimStart();
            return GetLabelCost(cost, prefixToRemove);
        }
        
        private void WaitForPromoCodeToUnload()
        {
            Browser.Wait.UntilElementUnloads(PromoCodeText);
        }

        public string GetTaxLabel()
        {
            return OrderSummaryTotalLabels()[2].Text;
        }

        private decimal GetLabelCost(string cost, string prefixToRemove = null)
        {
            if (prefixToRemove != null)
                cost = cost.Replace(prefixToRemove, string.Empty).Replace("CAD", string.Empty).Trim();

            decimal.TryParse(Regex.Replace(cost, @" \D+", string.Empty),
                NumberStyles.Currency, CultureInfo.CurrentCulture, out var result);

            return result;
        }

        private void ApplyDiscountOnIosPlatform(string discountValue, string discountReason)
        {
            //Click on Percent Discount text field
            Browser.Locate.ElementByXpath("//XCUIElementTypeOther[@name='Shopping Cart | Lamps Plus']/XCUIElementTypeTextField[5]").Click();

            //Enter discount
            if (int.Parse(discountValue) > 9)//if discount not a single digit
            {
                discountValue.ToCharArray().ToList().ForEach(digit => Browser.Locate.ElementByXpath($"//XCUIElementTypeKey[@name='{digit}']").Click());//Required to enter one digit at a time on iOS native keyboard.
            }
            else
            {
                Browser.Locate.ElementByXpath($"//XCUIElementTypeKey[@name='{discountValue}']").Click();
            }

            //Select Discount Reason option
            Browser.Locate.ElementByXpath("//XCUIElementTypeOther[@name='Discount Reason']").Click();
            Browser.Locate.ElementByXpath($"//XCUIElementTypeButton[@name='{discountReason}']").Click();
        }

        //Instances
        protected IBrowser Browser;
        protected IAssert Assert;
        protected IModalDesktop Modal;
        protected ProductActions ProductActions;

        public CartDesktop(IBrowser browser, IModalDesktop modal, IAssert assert, ProductActions productActions)
        {
            ProductsInCartList = new List<ProductModel>();
            ProductActions = productActions;
            Assert = assert;
            Browser = browser;
            Modal = modal;
            ProductActions = productActions;
        }

        //Interface implementation
        public string PageTitle { get; }
        public string PageUrl => "https://www.lampsplus.com/cart/";
        public string EstimatedTaxLabel => "Estimated Tax ¹:";

        public bool IsCurrentPage => Browser.Wait.IsVisibleElement(By.CssSelector(_checkOutNowClass.ToCssClassSelector()),10);
        public bool IsCheckOutNowButtonDisabled => CheckOutNowButton.GetAttribute("aria-disabled") == "true";
        public bool IsPaypalButtonDisabled => PaypalButtonDisable.GetAttribute("class").Contains("disabled");
        public bool IsTextMarginFieldEmpty() => string.IsNullOrEmpty(TextMarginArea.GetAttribute(HtmlTextWriterAttribute.Value.ToString()));
        public bool IsAllPosCheckboxesUnchecked => Browser.Locate.SelectedElements(PosCheckBoxes).Count != 0;
        public bool IsShippingAndProcessingDisabled => Browser.Wait.IsInvisibleElement(By.CssSelector(_removeAndShippingContainerClass.ToCssClassSelector()));
        public bool IsAdditionalDiscountDisplayed() => Browser.Locate.DoesElementExistImmediately(_additionalDiscountsClass);
        public bool IsPosLabelVisible => Browser.Locate.DoesElementExistImmediately(_posPurchaseOptionClass.ToCssClassSelector());
        public bool IsAllPosLinkVisible => Browser.Locate.DoesElementExistImmediately(_allInStoreClass.ToCssClassSelector());
        public bool IsMarginDisplayedOnEditPriceModal => Browser.Wait.IsVisibleElement(By.ClassName(_textMarginClass));
        public bool IsPromoCodePrefixVisible => Browser.Wait.IsVisibleElement(By.CssSelector(_promoCodeLineClass.ToCssClassSelector()));

        public virtual bool DoesCartMatchAddedProducts(Dictionary<string, int> addedProducts)
        {
            ProductsInCartList.Clear(); // Clear the list since it will be rebuilt below.

            for (var index = 0; index < UniqueProductsCount; index++) { ProductsInCartList.Add(new Utilities.ProductModel(ProductName(index), ProductSku(index), ProductQtyDesktopLabel, ProductTotalPrice(index))); }

            foreach (var product in ProductsInCartList)
            {
                var temp = addedProducts[product.Sku];
                if (temp != product.Quantity) { return false; }
            }

            return true;
        }

        public decimal GetSaleTaxAmount()
        {
            return GetOrderSummaryCost(_taxTotalPrefix, "$");
        }

        public decimal GetProductTotalPriceWithoutDiscount(int index = 0)
        {
            Browser.Wait.ForClickableElement(ProductTotalCostLabel(index));

            var productTotalPrice = ProductTotalPrice(index); // Index of total price.

            productTotalPrice = productTotalPrice.Replace("$", string.Empty);

            return decimal.Parse(productTotalPrice);
        }

        public void VerifyShippingVerbiage()
        {
            // date time condition to execute the if else statement
            var start = new TimeSpan(00, 00, 00);
            var end = new TimeSpan(13, 59, 59);
            var nowOrig = DateTime.Now.TimeOfDay;
            const int shippingMessageLengthWhenFirstDateBeforeTenthOfMonth = 69;
            const int shippingMessageLengthWhenFirstDateAfterTenthOfMonth = 70;

            Browser.Wait.IsVisibleElement(By.ClassName(_availableShippingOptionsDaysClass));

            var outerText = ModalTimeCheck(0).Text;
            var number = outerText.Length;

            if (DateTimeHelper.IsTimeInBetween(start, end, nowOrig))
            {
                //time is between midnight to 2pm
                var modal1 = Stock(0)[0].Text;
                ConditionalOrderCheck(modal1);
            }
            else if (number == shippingMessageLengthWhenFirstDateBeforeTenthOfMonth)          // time is between 2pm to midnight
            {
                var shipToday2 = ModalTimeCheck(0).Text.Remove(0, 26).Substring(0, 26);
                ConditionalOrderCheck(shipToday2);
            }
            else if (number == shippingMessageLengthWhenFirstDateAfterTenthOfMonth)           // time is between 2pm to midnight
            {
                var shipToday2 = ModalTimeCheck(0).Text.Remove(0, 27).Substring(0, 26);
                ConditionalOrderCheck(shipToday2);
            }
            else
            {
                var shipToday2 = ModalTimeCheck(0).Text.Remove(0, 25).Substring(0, 26);
                ConditionalOrderCheck(shipToday2);
            }
        }

        public virtual void VerifySecondDayShippingVerbiage()
        {
            // date time condition to execute the if else statement
            var start = new TimeSpan(00, 00, 00);
            var end = new TimeSpan(13, 59, 59);
            var nowOrig = DateTime.Now.TimeOfDay;
            const int nonStandardShippingMessageLength = 58;

            Browser.Wait.IsVisibleElement(By.ClassName(_availableShippingOptionsDaysClass));

            int shippingOptionsOtherThanStandard;

            for (shippingOptionsOtherThanStandard = 1; shippingOptionsOtherThanStandard < 3; shippingOptionsOtherThanStandard++)
            {
                var outerText = ModalTimeCheck(shippingOptionsOtherThanStandard).Text;
                var number = outerText.Length;
                if (DateTimeHelper.IsTimeInBetween(start, end, nowOrig))
                {
                    int numberOfShippingOptions;
                    for (numberOfShippingOptions = 1; numberOfShippingOptions < 3;)
                    {
                        //time is between midnight to 2pm
                        var secondNextDay = Stock(numberOfShippingOptions)[0].Text;
                        ConditionalOrderCheck(secondNextDay);
                        break;
                    }
                }
                else if (number == nonStandardShippingMessageLength)               // time is between 2pm to midnight
                {
                    var shipToday2 = ModalTimeCheck(shippingOptionsOtherThanStandard).Text.Remove(0, 15).Substring(0, 26);
                    ConditionalOrderCheck(shipToday2);
                }
                else
                {
                    var shipToday2 = ModalTimeCheck(shippingOptionsOtherThanStandard).Text.Remove(0, 16).Substring(0, 26);
                    ConditionalOrderCheck(shipToday2);
                }
            }
        }

        public void CheckPosBox()
        {
            PosCheckBoxAndLabel(0).Click();
        }

        public void CheckPosBoxForAllCartSkus(int numberOfProducts)
        {
            for (var index = 0; index < numberOfProducts; index++)
            {
                Browser.RefreshPage();
                PosCheckBoxAndLabel(index).Click();
                Browser.Wait.IsInvisibleElement(By.CssSelector(ChangeShippingOptionsLinkClass(index).ToCssClassSelector()));
            }
        }

        public string GetOrderSummaryBlockText()
        {
            var orderSummaryBlock = Browser.Locate.ElementById(_orderSummaryContainerId).Text;
            return orderSummaryBlock;
        }

        public void AddSkuToCartByStyleNumber(string sku)
        {
            Browser.Wait.IsVisibleElement(By.Id(_addSkuContainerId));
            AddSkuByStyleContainer.SendKeys(sku);
            AddSkuByStyleLink.Click();
        }

        public string GetCartErrorModalText()
        {
            var cartErrorModal = Browser.Locate.ElementById(_cartErrorModalId).Text.Replace("Error", string.Empty).Trim();
            return cartErrorModal;
        }

        public void OpenDiscountTooltip()
        {
            Browser.Wait.ForDomReady();
            var editButton = EditPriceButton; //To avoid a stale element exception.
            editButton.Click();
            Browser.ScrollIntoView(Modal.GetDiscountToolTipModal());
        }

        public virtual void OpenShippingOptions()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(JsChangeShippingOptionsClass));
            ChangeShippingOptionsLink.Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(_selectDeliveryOptionsModalId.ToCssIdSelector()));
        }

        public virtual void ApplyZipCode(string zipCode)
        {
            Browser.Wait.IsVisibleElement(By.Id(_shipZipFieldId));
            ShippingZipField.Clear();
            ShippingZipField.SendKeys(zipCode);
            ShipTabSearchButton.Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(_jsAvailableShippingOptionsContainerClass.ToCssClassSelector()));
        }

        public virtual void OpenPromoCodeEntryField()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_addPromoCodeLinkClass),30);
            AddPromoCodeLink.Click();
        }

        public void UpdatePromoCode(string promoCode)
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_promoInputClass));
            PromoCodeInputField.SendKeys(promoCode);
            PromoCodeUpdate.Click();
            Browser.Wait.ForDomReady();
            Browser.ScrollToTopOfWindow();
        }

        public void IsPromoCodeTextFieldVisible()
        {
            Browser.ScrollIntoView(ChangeShippingOptionsLink);
            Browser.Wait.IsVisibleElement(By.ClassName(_promoInputClass));
        }

        public string GetInvalidPromoCodeValue()
        {
            return InvalidPromoCode;
        }

        public string GetInvalidPromoCodeErrorMessage()
        {
            Browser.Wait.IsVisibleElement(By.Id(_promoCodeInputErrorId),20);
            return PromoCodeErrorMessage.Text;
        }

        public void ClearPromoCode()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_promoInputClass));
            PromoCodeInputField.Clear();
        }

        public void ScrollToPayPalLaterWidget()
        {
            Browser.ScrollIntoView(PayPalLaterWidget, true);
        }

        public virtual string GetPromoCodeStatusMessage()
        {
            return PromoCodeLabel.Text;
        }

        public string GetSaleEndsInCallOut()
        {
            var saleEndsInCallOut = SaleCountdown.Text.Substring(0, 12);
            return saleEndsInCallOut;

        }

        public IBrowser Navigate()
        {
            // Navigate to base page
            Browser.Navigate(PageUrl);
            return Browser;
        }

        public int GetShippingChargeCost()
        {
            return decimal.ToInt32(GetShippingCost());
        }

        public decimal GetOrderTotalCost()
        {
            return GetOrderSummaryCost(_orderTotalPrefix, "$");
        }

        public void CheckOut()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_checkOutNowClass.ToCssClassSelector()));
            Browser.Wait.ForClickableElement(CheckOutNowButton);
            CheckOutNowButton.Click();
            Browser.Wait.ForDomReady(30);
        }

        public void OpenEditPriceModal()
        {
            Browser.Locate.ElementByXpath(_productQtyDropdownXpath).Click();
            Browser.Wait.IsVisibleElement(By.ClassName(_editPriceClass));
            EditPriceButton.Click();
        }

        public IElement GetDiscountVendorApprovalComment()
        {
            return DiscountVaComment;
        }

        public decimal GetProductTotal()
        {
            return GetOrderSummaryCost(_productTotalPrefix, "$");
        }

        public decimal GetShippingTotal()
        {
            return GetOrderSummaryCost(_shippingAndProcessingPrefix, "$");
        }

        public string GetShippingOptionsErrorText()
        {
            return ShippingOptionsError.Text;
        }

        public bool AreShippingZoneFieldsRemoved()
        {
            return ShippingOptionsContainer.All(test => test.Text.Contains("Unknown Shipping Zone"));
        }

        public decimal GetOrderTotalWithoutDiscount()
        {
            var originalItemPrice = GetProductTotalPriceWithoutDiscount();
            var shippingCharge = GetShippingChargeCost();
            var saleTax = GetSaleTaxAmount();
            var orderTotal = originalItemPrice + shippingCharge + saleTax;

            return orderTotal;
        }

        public List<ProductModel> GetProductDetailsInCart(string shortSkuOnSale)
        {
            var itemsInCart = GetListOfAllProductsOnCartPage();
            return itemsInCart.Where(p => p.Sku.ToUpper().Equals(shortSkuOnSale.ToUpper())).ToList();
        }

        public virtual void ShippingUpdate()
        {
            Browser.Wait.ForElementToStopAnimating(Browser.Locate.ElementByClassName(_deliveryOptionsContainerClass));
            Browser.Wait.IsVisibleElement(By.CssSelector(_jsAvailableShippingOptionsContainerClass.ToCssClassSelector()));
            Browser.Wait.ForClickableElement(Browser.Locate.ElementByClassName(_jsUpdateShipBtnClass));
            UpdateShippingOptionButton.Click();
            Browser.Wait.IsInvisibleElement(By.CssSelector(Modal.LpModalId.ToCssIdSelector()));
        }

        public void OpenMoreDetailsDrawer()
        {
            Browser.Wait.IsVisibleElement(By.XPath(_toggleMoreDetailsXpath));
            ToggleMoreDetailsButton.Click();
        }

      public void SelectLargeImageOnPrintModal()
        {
            Modal.WaitForModalContentToLoad();
            SelectLargeImage.Click();
        }

        public virtual void RemoveCartItems()
        {
            Browser.Wait.ForDomReady();

            if (Browser.Locate.DoesElementExistImmediately(RemoveItemClass.ToCssClassSelector()))
            {
                var cartItemCount = RemoveItemLinksElements.Count;

                for (var i = 0; i < cartItemCount; i++)
                {
                    if (i != 0) { Browser.RefreshPage(); } //Refresh page for all Items except the very first item.

                    Browser.Wait.IsVisibleElement(By.CssSelector(_checkOutNowClass.ToCssClassSelector()));
                    Browser.Wait.ForDisplayedElement(CartItemRemoveLinkElement(0));

                    CartItemRemoveLinkElement(0).Click();
                }
            }

            Browser.Wait.IsVisibleElement(By.ClassName(CartEmptyWarningClass), 60);
        }

        public void RemoveSingleItemFromCart()
        {
            Browser.Wait.ForDomReady();

            if (Browser.Locate.DoesElementExistImmediately(RemoveItemClass.ToCssClassSelector()))
            {
                Browser.Wait.IsVisibleElement(By.CssSelector(_checkOutNowClass.ToCssClassSelector()));
                Browser.Wait.ForDisplayedElement(CartItemRemoveLinkElement(0));
                CartItemRemoveLinkElement(0).Click();
            }
        }

        public virtual List<ProductModel> GetListOfAllProductsOnCartPage()
        {
            ProductsInCartList.Clear(); // Clear the list since it will be rebuilt below.

            for (var index = 0; index < UniqueProductsCount; index++) 
            { ProductsInCartList.Add(new ProductModel(ProductName(index), ProductSku(index), ProductQty(index), ProductTotalPrice(index))); }

            return ProductsInCartList;
        }

        public int GetCountOfAllProductsInCart()
        {
            return ProductsCount.Count;
        }
        
        public void SendCartEmail()
        {
           Browser.Wait.ForDomReady();
           Browser.ScrollToElement(SendEmailButton);
           SendEmailButton.Click();
           Browser.SwitchToDefaultContent();
           Browser.Wait.IsVisibleElement(By.CssSelector(Modal.LpModalIframeId.ToCssIdSelector()));
           Browser.SwitchFocusToIframe(Browser.Locate.ElementBySelector(Modal.LpModalIframeId.ToCssIdSelector()));
           Browser.Wait.IsVisibleElement(By.CssSelector(_emailOptionsModalId.ToCssIdSelector()));
           Browser.Wait.ForDomReady(); //Give the 'Thank You' window time to re-size itself after form submission.
        }

        public void ApplyDiscountForProduct(string discountApplied, string discountReasonApplied)
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_editPriceClass));
            CartEditPriceElement.Click();
            new SelectElement(DiscountDropdown.InternalElement).SelectByIndex(1);
            DiscountPercentTextBox.SendKeys(discountApplied);
            ApplyDiscountButton.Click();
        }

        public void OpenEditPriceModalOnIpad()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_editPriceClass));
            CartEditPriceElement.Click();
            Browser.Wait.IsVisibleElement(By.ClassName(_btnApplyDiscountClass));
        }

        public void ApplyDiscountForProductOnIpad(string discountApplied, string discountReasonApplied)
        {
            ((IpadBrowser)Browser).SwitchToNativeContext((AppiumDriver<AppiumWebElement>)Browser.Driver); //Switch to iOS Native context
            ApplyDiscountOnIosPlatform(discountApplied, discountReasonApplied); //Apply discount in iOS Native context mode
            ((IpadBrowser)Browser).SwitchToWebViewContext((AppiumDriver<AppiumWebElement>)Browser.Driver); //Switch back to iOS WebView context
            ApplyDiscountButton.Click();
        }

        public IElement GetCartEmailModal()
        {
            Browser.Wait.IsVisibleElement(By.Id(_emailOptionsModalId));
            return EmailOptionModal;
        }

        public int GetAllCartProductsPosLinkCount()
        {
            return GetAllCartSkuPosCheckBoxCount;
        }

        public List<string> GetListOfCartSkus(string url, int cartItemsQty)
        {
            var cartSkusList = new List<string>();

            Browser.Navigate(url);
            Assert.True(IsCurrentPage, "User is not on Cart page.");

            for (var index = 0; index < cartItemsQty; index++)
            {
                cartSkusList.Add(ProductSkuLabel(index).Text.Replace("Style # ", String.Empty).Trim());
            }

            return cartSkusList;
        }

        public IElement GetCheckOutNowButton()
        {
            return CheckOutNowButton;
        } 

        public virtual IElement GetToolTip()
        {
            return ToolTip;
        }

        public IElement GetPaypalButton()
        {
            return PaypalButton;
        }


        public IElement GetMoreYouMayLike()
        {
            return CartMoreYouMayLikeContainer;
        }

        public void OpenPrintModal()
        {
            Browser.Wait.ForPage(PageUrl);
            Browser.Wait.ForElement(PrintLink).Click();
        }

        public void SelectLargeImagesOption()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_selectAllOrNoneClass.ToCssClassSelector()));
            SelectNoneLink.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(_rdoLargeImagesId.ToCssIdSelector()));

            if (!SelectLargeImage.Selected) { SelectLargeImage.Click(); }
        }

        public void ClosePrintModal()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_printCartBtnClass.ToCssClassSelector()));

            PrintCartButton.Click();
        }

        public string GetLimitedQuantityCallout()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_limitedQuantityCalloutClass));

            return LimitedQuantityCallout.Text;
        }

        public IElement GetTextMarginField()
        {
            return TextMarginArea;
        }

        public virtual void InputEmailRecipientsInForm(string[] recipientEmails)
        {
            FormEmailRecipientsField.SendKeys(string.Join(", ", recipientEmails));
        }

        public virtual void OpenAndFocusEmailModal()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_jsEmailClass));
            Browser.ClickByJs(EmailButton);

            Browser.Wait.IsVisibleElement(By.CssSelector(Modal.LpModalIframeId.ToCssIdSelector()));
            Browser.SwitchFocusToIframe(Browser.Locate.ElementBySelector(Modal.LpModalIframeId.ToCssIdSelector()));

            Browser.Wait.IsVisibleElement(By.XPath(_emailRecipientsXpath));
        }

        public void HoverOverCheckOutNowButton()
        {
            Browser.MouseOverOnElement(CheckOutNowButton);
        }

        public virtual void EmailShoppingCart(params string[] recipientEmails)
        {
            OpenAndFocusEmailModal();

            InputEmailRecipientsInForm(recipientEmails);
            Browser.SwitchToDefaultContent();
            Browser.SwitchFocusToIframe(Browser.Locate.ElementBySelector(Modal.LpModalIframeId.ToCssIdSelector()));
            Browser.Wait.ForDomReady();
            FormFirstNameField.SendKeys("LPFirst");
            FormLastNameField.SendKeys("LPLast");
            FormEmailFromField.SendKeys("fedcsrmanager@lampsplus.com");
            FormZipCodeField.SendKeys("91311");

            SendEmailButton.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(_thankYouMessageClass.ToCssClassSelector()));
            Browser.SwitchToDefaultContent();
            Browser.SwitchFocusToIframe(Browser.Locate.ElementBySelector(Modal.LpModalIframeId.ToCssIdSelector()));
        } 
        
        public void DeleteCart()
        {
            OpenDeleteCartModal();
            Browser.Wait.IsVisibleElement(By.ClassName(_btnDeleteClass));
            DeleteCartButton.Click();
            Browser.Wait.IsInvisibleElement(By.ClassName(_btnDeleteClass));
        }

        public void OpenDeleteCartModal()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_cartOptionsClass));
            DeleteCartLink.Click();
        }

        public void OpenCartLinkModal()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_jsCartLinkClass),20);
            CartLink.Click();
        }

        public List<string> GetCartLinkDetails()
        {
            Browser.SwitchFocusToIframe(Modal.GetLpModal());
            Browser.Wait.IsVisibleElement(By.ClassName(_copyCartLinkClass));
            CartLinkCopyButton.Click();
            var cartLinkCopyButtonText = CartLinkCopyButton.Text;
            var cartLinkText = CartLinkText.GetAttribute("value");
            var cartLink = new List<string>();
            cartLink.Add(cartLinkCopyButtonText);
            cartLink.Add(cartLinkText);
            return cartLink;
		}
		
        public void SelectPrintButton()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_jsPrintClass));
            PrintButton.Click();
            Browser.Wait.IsVisibleElement(By.Id(_printOptionsContainerId));
        }

        public void EnterDiscountValue(decimal discountValue)
        {
            Browser.Wait.ForClickableElement(TextPercentDiscountField).SendKeys(discountValue.ToString());
            Browser.DispatchChangeEvent(TextPercentDiscountField);
        }

        public string GetSaleDiscountValue()
        {
            new SelectElement(SelectDiscountReasonField.InternalElement).SelectByIndex(1);
            return TextDiscountPriceField.GetAttribute("value");
        }

        public void SelectAddDiscountButton()
        {
            ApplyDiscountButton.Click();
        }

        public IElement GetAdditionalDiscount()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_additionalDiscountsClass.ToCssClassSelector()));
            return AdditionalDiscountElement;
        }

        public decimal GetSubTotal()
        {
            return GetOrderSummaryCost(_subtotalPrefix, "$");
        }

        public void NavigateToPdpViaProductImageInCart()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_checkOutNowClass));
            ProductImageAnchorWebElement(0).Click();
        }

        public void ApplyDiscount(decimal discount)
        {
            OpenDiscountTooltip();
            EnterDiscountValue(discount);
            GetSaleDiscountValue();
            SelectAddDiscountButton();
        }

        public void NavigateToPdpViaProductNameInCart()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_checkOutNowClass));
            ProductNameLabel(0).Click();
        }

        public string GetProductNameOnCart()
        {
            return ProductNameLabel(0).Text;
        }

        public decimal GetOrderSummaryValuesByIndex(int index)
        {
            var total = PromoDiscountPrice(index);
            var zero = 0;

            total = Regex.Replace(total, "[^0-9.]", string.Empty);

            return total == "" ? zero : decimal.Parse(total);
        }

        public decimal GetShippingOptionTotal(int index)
        {
            var total = ShippingCostLabels[index].Text;
            const int zero = 0;
            total = Regex.Replace(total, "[^0-9.]", string.Empty);

            return total == "" ? zero : decimal.Parse(total);
        }

        public void FillFormSelectByValue(IElement selectControl, string value)
        {
            Browser.Locate.ClickDropdownByValue(selectControl, value);
        }

        public virtual decimal EnterCartZipCodeForShippingOption(string countryCode, string zipCode, int index)
        {
            OpenShippingOptions();

            //compensates for both logged in and anonymous users
            var cachedRadioButton = ShippingOptionsRadioContainer; 

            FillFormSelectByValue(ShippingCountryDropdown, countryCode);

            ShipZipField.Clear();
            ShipZipField.SendKeys(zipCode);
            ShipZipApplyBtn.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(AvailableShippingOptionsClass.ToCssClassSelector()));

            if (cachedRadioButton.IsInitialized)
            {
                Browser.Wait.UntilElementUnloads(cachedRadioButton);
            }

            var radios = ShippingTypeRadios;
            radios[index].Click();

            var shippingTotal = GetShippingOptionTotal(index);

            UpdateShippingOptionButton.Click();

            Browser.Wait.IsInvisibleElement(By.CssSelector(Modal.LpModalId.ToCssIdSelector()));

            return shippingTotal;
        }

        public int GetNumberOfShippingOptions()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(AvailableShippingOptionsClass.ToCssClassSelector()));
            return ShippingTypeRadios.Count();
        }

        public void EnterSkuInAddByStyle(string shortSku)
        {
            Browser.Wait.ForDomReady();
            AddByStyleShortSkuElement.SendKeys(shortSku);
            AddByStyleShortSkuElement.SendKeys(Keys.Enter);
        }

        public void EmailShoppingCartWithOptionsSelected(params string[] recipientEmails)
        {
            OpenAndFocusEmailModal();

            InputEmailRecipientsInForm(recipientEmails);
            Browser.SwitchToDefaultContent();
            Browser.SwitchFocusToIframe(Browser.Locate.ElementBySelector(Modal.LpModalIframeId.ToCssIdSelector()));
            Browser.ClickByJs(InsertInHomeConsultInfoButton);
            Browser.ClickByJs(SendEmailButton);
            Browser.Wait.UntilElementUnloads(FormEmailRecipientsField);

            Browser.Wait.IsVisibleElement(By.CssSelector(_thankYouMessageClass.ToCssClassSelector()));
        }

        public string GetEmailThankYouMessageOnModal()
        {
            return EmailFormContainer.Text;
        }

        public bool AreEmailsFoundInDatabase(params string[] recipientEmails)
        {
            var recipientEmailData = ProductActions.GetRecipientsByEmailAddedTodayFromCart(recipientEmails);
            // Returned emails from db should match the length of emails in argument
            return recipientEmailData.Count == recipientEmails.Length; //, "Database doesn't contain recipient email records");
        }

        public bool VerifySkusInCartRemainSame(List<ProductModel> list1, List<ProductModel> list2)
        {
            return list1.OrderBy(x => x).SequenceEqual(list2.OrderBy(x => x));
        }

        public bool IsPaypalWidgetDisplayed()
        {
            return Browser.Wait.IsVisibleElement(By.Id(_payPalLaterWidgetId));
        }

        public virtual void RemovePromoCode()
        {
            Browser.Wait.ForDomReady(30);

            var promoCodeElementExists = Browser.Locate.DoesElementExistImmediately(_jsShowPromoCodeTermsAndConditionsClass.ToCssClassSelector());

            if (!promoCodeElementExists) return;
            RemovePromoCodeElement.Click();
            WaitForPromoCodeToUnload();
        }

        public void ChangeItemQuantity(string quantity)
        {
            var beforeQuantityChange = ProductTotalCostLabel(0).Text;
            
            Browser.SelectDropDownByText(ProductQtyDropdownField(0), quantity);
            bool IsTotalPriceChanged() => beforeQuantityChange != ProductTotalCostLabel(0).Text;
            Browser.Wait.ForCondition(IsTotalPriceChanged, 3);
        }

        public ReadOnlyCollection<IElement> GetShippingTypeRadios()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_fieldRadioClass));
            return ShippingTypeRadios;
        }

        public void RemoveAppliedDiscount()
        {
            DiscountTooltipRemoveButton.Click();
        }

        public decimal GetAdditionalDiscounts()
        {
            return GetOrderSummaryCost(_additionalDiscountsLabel);
        }

        public string GetAdditionalDiscountLabel()
        {
            return _additionalDiscountsLabel;
        }

        public string GetMarginTextValue()
        {
            return TextMarginArea.GetAttribute("value");
        }

        public string GetAdditionalDiscountsLabel()
        {
            var additionalDiscountLabel = OrderSummaryBlockLabel(_additionalDiscountsLabel).Text.Trim(); //index of additional discount label
            additionalDiscountLabel = additionalDiscountLabel.Replace("\r\n", string.Empty);

            return additionalDiscountLabel;
        }

        public decimal GetProductTotalPrice(int index = 0)
        {
            var productTotalPrice = ProductTotalPrice((index * 2) + 1); // Index of total price.

            productTotalPrice = productTotalPrice.Replace("Was $", string.Empty);

            return decimal.Parse(productTotalPrice, NumberStyles.Currency);
        }

        public decimal GetItemTotalPriceWithoutDiscount(int index = 0)
        {
            var itemTotalPrice = ItemTotalPrice(index); // Index of total price.

            itemTotalPrice = itemTotalPrice.Replace("$", string.Empty);

            return decimal.Parse(itemTotalPrice);
        }

        public decimal GetCalculatedPromoDiscount(int discountRate, bool withoutDiscount = false, bool employeeDiscount = false)
        {
            var discountTotal = 0m;

            for (var i = 0; i < ProductListItems.Count; i++)
            {
                var itemTotal = withoutDiscount ? (employeeDiscount ? GetItemTotalPriceWithoutDiscount(i) : GetProductTotalPriceWithoutDiscount(i)) : GetProductTotalPrice();
                discountTotal += Math.Floor(itemTotal * discountRate) / 100;
            }

            return discountTotal;
        }

        public decimal GetCalculateSubTotal(int discountRate, bool withoutDiscount = false, bool employeeDiscount = false)
        {
           return (withoutDiscount ? GetProductTotal() : GetProductTotalPrice()) - GetCalculatedPromoDiscount(discountRate, withoutDiscount, employeeDiscount);
        }

        public decimal GetShippingCost()
        {
            return GetOrderSummaryCost(_shippingTotalPrefix, "$");
        }

        public decimal GetSubTotalCost()
        {
            return GetOrderSummaryCost(_subtotalPrefix, "$");
        }

        public decimal GetDiscountTotalCost()
        {
            var promoCodeValue = Browser.Locate.ElementByClassName(_osValueClass, PromotionsPrefixLabel, true);
            return Math.Abs(GetLabelCost(promoCodeValue.Text, " $"));
        }

        public void CartElementDelete()
        { 
            DeleteCartElement.Click();
            Browser.Wait.ForClickableElement(DeleteCartButton);
            DeleteCartButton.Click();
        }

        public void SelectNoneLinkUnderPrintYourCartModal()
        {
            Browser.Wait.IsVisibleElement(By.XPath(_selectNoneLinkXpath));
            SelectNoneLink.Click();
        }

        public void SmallImagesRadioButton()
        {
            Browser.MoveToAndClickElement(SmallImagesRadioBtn);
            Browser.Wait.IsVisibleElement(By.Id(_rdoSmallImagesId));

            if (!SmallImagesRadioBtn.Selected)
            {
                SmallImagesRadioBtn.Click();
            }
        }

        public void SelectModalPrintCartBtn()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_printCartBtnClass));
            PrintCartBtn.Click();
        }
        
        public string GetInvalidShortSku()
        {
            return _invalidShortSku;
        }

        public string GetSuccessfulEmailMessage(string lastEmail = null)
        {
            Browser.SwitchToDefaultContent();
            Browser.Wait.IsVisibleElement(By.CssSelector(Modal.LpModalIframeId.ToCssIdSelector()));
            Browser.Wait.ForElementToStopAnimating(Browser.Locate.ElementBySelector(Modal.LpModalIframeId.ToCssIdSelector()));
            Browser.SwitchFocusToIframe(Browser.Locate.ElementBySelector(Modal.LpModalIframeId.ToCssIdSelector()));

            var resultTrimmed = Regex.Replace(EmailFormContainer.Text, @"[ \t]+(?<=\r\n)|[ \t]+", " ").Trim();
            var index = resultTrimmed.IndexOf(lastEmail, StringComparison.Ordinal);
            var result = TextActions.NormalizeWhitespace(resultTrimmed.Substring(0, index + lastEmail.Length).Trim());
            return result.Replace("! ", "!\r\n").Replace(": ", ":\r\n").Replace(".com ", ".com\r\n");
        }

        public IElement GetDiscountPriceField()
        {
            return TextDiscountPriceField;
        }

        public IElement GetPercentDiscountPriceField()
        {
            return TextPercentDiscountField;
        }

        public void SelectPrintInStoreButton()
        {
            Browser.Wait.ForClickableElement(Browser.Locate.ElementById(_printInStoreOptionBtnId)).Click();
            Browser.Wait.IsVisibleElement(By.Id(_printOutputId));
        }

        public string GetProductInventoryDetails()
        {
            Browser.Wait.ForDisplayedElement(MoreDetailsInventoryElement);
            return MoreDetailsInventoryElement.Text;
        }

        public string GetPayPalCallout()
        {
            Browser.SwitchFocusToIframe(Browser.Locate.ElementBySelector("[allowtransparency='true']"));

            var vera1 = Browser.Locate.ElementByClassName("tag--medium").Text;
            string elementSelectora = ".message__logo-container";
            string beforeStylesa = (string)Browser
                .ExecuteJs(
                    "return window.getComputedStyle(document.querySelector(arguments[0]), ':before').getPropertyValue('content');",
                    elementSelectora).ToString().Remove(5).Substring(1);
            string AfterStylesa = (string)Browser.ExecuteJs("return window.getComputedStyle(document.querySelector(arguments[0]), ':after').getPropertyValue('content');", elementSelectora).ToString().Remove(2).Substring(1);
            var payPalTxt1a = Browser.Locate.ElementBySelector(".message__logo-container").GetAttribute("alt");
            var vera2 = Browser.Locate.ElementByClassName("tag--default").Text.Remove(10);

            var FinalStringa = vera1 + " " + beforeStylesa + " " + payPalTxt1a + AfterStylesa + " " + vera2;
            Browser.SwitchToDefaultContent();
            return FinalStringa;
        }

        public ReadOnlyCollection<IElement> GetShippingCostLabels()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(JsShippingCostClass));
            return ShippingCostLabels;
        }

        public ReadOnlyCollection<IElement> GetShippingDaysLabels()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_availableShippingOptionsDaysClass));
            return ShippingDaysLabels;
        }

        public List<ShippingOptionItem> GetAvailableShippingOptions()
        {
            var availableOptions = new List<ShippingOptionItem>();
            var radios = GetShippingTypeRadios();
            var costs = GetShippingCostLabels();
            var days = GetShippingDaysLabels();

            for (var i = 0; i < radios.Count; i++)
            {
                decimal.TryParse(costs[i].Text.Trim().Trim('$'), out var cost);
                var dateParts = days[i].Text.Split(' ');

                var firstShipDate = dateParts[1].Replace(".", string.Empty) + " " + dateParts[2] + " " + DateTime.Now.Year;
                var firstShippingDate = DateTime.Parse(firstShipDate);
                var lastShipDate = radios[i].Text.StartsWith("Standard") ? dateParts[4].Replace(".", string.Empty) + " " + dateParts[5] + " " + DateTime.Now.Year : null;

                var lastShippingDate = lastShipDate != null ? Convert.ToDateTime(lastShipDate) : default(DateTime);

                if (firstShippingDate < DateTime.Now)
                    firstShippingDate = firstShippingDate.AddYears(1);

                if (lastShippingDate < DateTime.Now)
                    lastShippingDate = lastShippingDate.AddYears(1);

                availableOptions.Add(new ShippingOptionItem()
                {
                    ShippingType = radios[i].FindElement(By.ClassName("jsShippingTypeRadio")).GetAttribute("value"),
                    Cost = cost,
                    ArrivesDate = firstShippingDate,
                    LastArrivalDate = lastShippingDate
                });
            }
            return availableOptions;
        }

        public bool IsCartEmpty()
        {
            return Browser.Wait.IsVisibleElement(By.CssSelector(_cartEmptyBodyClass.ToCssClassSelector()));
        }

        public string GetCartId()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_checkOutNowClass));
            return CartIdContainer.Text.Replace("Cart #", string.Empty).Replace("#", "");
        }

        public decimal GetTaxAmount(string page = null)
        {
            var taxValue = page != "orderConfirmation" ? GetOrderSummaryCost(_cartTaxTotalPrefix, "$") : GetOrderSummaryCost(_orderTaxTotalPrefix, "$");
            return taxValue;
        }

        public void SelectEditCartLink()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_editOrderClass.ToCssClassSelector()));
            EditCartLink.Click();
        }

        public string GetProductTitleOnCart()
        {
            var productName= ProductNameLabel(0).Text;

            if (productName.Length > 30)
            {
                if (productName.Contains("\""))
                {
                    var productTitle = ProductNameLabel(0).Text.Substring(0, 25);
                    var addProductTitle = "\"" + productTitle + "..." + "\" ";
                    return addProductTitle;
                }
                else
                {
                    var productTitle = ProductNameLabel(0).Text.Substring(0, 30);
                    var addProductTitle = "\"" + productTitle + "..." + "\" ";
                    return addProductTitle;
                }
            }
            else
            {
                return "\"" + productName + "\" ";
            }
        }

        public bool AfterUndoMessageToDisappear()
        {
            return Browser.Wait.IsInvisibleElement(By.ClassName(UndoBlockClass));
        }

        public virtual string UndoMessageProductName()
        {
            var productName = Browser.Locate.ElementByClassName(UndoBlockClass).Text;
            return productName.Replace("\r\nUndo", "");
        }
    }
}