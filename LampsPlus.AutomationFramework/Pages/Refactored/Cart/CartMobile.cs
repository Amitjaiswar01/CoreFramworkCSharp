using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Utilities;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Databases.Actions;
using LampsPlus.AutomationFramework.Pages.Refactored.MobileDrawer;
using LampsPlus.AutomationFramework.Pages.Refactored.Modal;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Cart
{
    public class CartMobile : CartDesktop, ICartMobile
    {
        //Class members
        private string _jsShipZipApplyClass = "jsShipZipApply";
        private string _cartPromotionalCodeId = "cartPromotionalCode";
        private string _jsCloseShippingOptionsOverlayClass = "jsCloseShippingOptionsOverlay";
        private string _prodQtyClass  = "prodQty";
        private string _promoDiscountClass = "promoDiscount";
        private string _shippingTypeShippingCostClass = "shippingType__shippingCost";
        private string _checkOutNowBtnXpath= "//*[@id='regionCheckoutNowButtonBottom']/div/button";
        private string _changeShippingOptionsOverlayId  = "changeShippingOptionsOverlay";
        private string _emailButtonXpath = "//*[@id='regionPromoCodeLinks']//li[contains(@class, 'emailCart')]/a";
        private string _emailCartSubmitBtnId = "emailCartSubmitBtn";
        private string _cartidSelector = "#cartId";
        private string _successMessageClass = "success";
        private string _standardShippingXpath = "//*[@class ='available-shipping-options__item']//label[contains(@for, 'shippingType0')]";
        private string _emailCartContainerXpath = "//*[@id=\"emailCartContainer\"]/a";
        private string _PriceValidationMessageXpath = "//*[@id='cartOverview']//div[contains(@class, 'minimumPriceValidationMessage')]";
        private string _shipZipFieldId = "shipZipField";
        private string _availableShippingOptionsItemClass = "available-shipping-options__item";
        private string _removePromoCodeClass = "removePromoCode";
        private string _cartTermsAndConditionsId = "cartTermsAndConditions";
        private string ProductQtyMobileLabel (int index) => ProductQtyField(index).GetAttribute("value");

        private IElement CloseButton => Browser.Locate.ElementByClassName(_jsCloseShippingOptionsOverlayClass);
        private IElement CheckOutNowButton => Browser.Locate.ElementByXpath(_checkOutNowBtnXpath);
        private IElement ProductQtyField (int index) => Browser.Locate.ElementsByClassName(_prodQtyClass)[index];
        private IElement PromoDiscount => Browser.Locate.ElementByClassName(_promoDiscountClass);
        private IElement EmailButton => Browser.Locate.ElementByXpath(_emailButtonXpath);
        private IElement StandardShipping => Browser.Locate.ElementByXpath(_standardShippingXpath);
        private IElement SendEmailButton => Browser.Locate.ElementBySelector(_emailCartSubmitBtnId.ToCssIdSelector());
        private IElement ToolTip => Browser.Locate.ElementByXpath(_PriceValidationMessageXpath);
        private IElement MobileRemovePromoCode => Browser.Locate.ElementByClassName(_removePromoCodeClass);

        private bool IsShippingModalVisible(int timeToWait)
        {
            return Browser.Wait.IsVisibleElement(By.CssSelector(_changeShippingOptionsOverlayId.ToCssIdSelector()), timeToWait);
        }

        protected override IElement ChangeShippingOptionsLink => Browser.Locate.ElementByClassName(JsChangeShippingOptionsClass);
        protected override IElement ShippingZipField => Browser.Locate.ElementById(_shipZipFieldId);
        protected override IElement AddPromoCodeLink => Browser.Locate.ElementById(_cartPromotionalCodeId);
        protected override IElement ShippingCellShippingCost => Browser.Locate.ElementByClassName(_shippingTypeShippingCostClass);
        protected override IElement UpdateShippingOptionButton => Browser.Locate.ElementByClassName(_jsShipZipApplyClass);
        protected override IElement ShippingOptionModal => Browser.Locate.ElementById(_changeShippingOptionsOverlayId);
        protected override IElement CartIdContainer => Browser.Locate.ElementBySelector("#cartId");
        protected override IElement ShippingOptionsRadioContainer => Browser.Locate.ElementImmediately(_availableShippingOptionsItemClass.ToCssClassSelector());
        protected override ReadOnlyCollection<IElement> ShippingTypeRadios => Browser.Locate.ElementsByClassName("available-shipping-options__name");
        protected override ReadOnlyCollection<IElement> ProductsCount => Browser.Locate.ElementsByClassName(ProductNameClass);

        //Instances
        private readonly IMobileDrawer _drawer;

        public CartMobile(IBrowser browser, IModalDesktop modal, IMobileDrawer drawer, IAssert assert, ProductActions productActions) : base(browser, modal, assert, productActions)
        {
            _drawer = drawer;
        }

        //Interface implementation
        public override bool DoesCartMatchAddedProducts(Dictionary<string, int> addedProducts)
        {
            ProductsInCartList.Clear(); // Clear the list since it will be rebuilt below.

            for (var index = 0; index < UniqueProductsCount; index++) { ProductsInCartList.Add(new Utilities.ProductModel(ProductName(index), ProductSku(index), ProductQtyMobileLabel(index), ProductTotalPrice(index))); }

            foreach (var product in ProductsInCartList)
            {
                if (addedProducts[product.Sku] != product.Quantity) { return false; }
            }

            return true;
        }

        public override void ApplyZipCode(string zipCode)
        {
            ShippingZipField.Clear();
            ShippingZipField.SendKeys(zipCode);
            ShipTabSearchButton.Click();
        }

        public override void OpenPromoCodeEntryField()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_cartPromotionalCodeId.ToCssIdSelector()));
            Browser.ScrollIntoView(AddPromoCodeLink, true);
            Browser.ClickByJs(AddPromoCodeLink);
        }

        public void CheckOutFromCartPage()
        {
            Browser.Wait.ForClickableElement(CheckOutNowButton);
            CheckOutNowButton.Click();
        }

        public override List<Utilities.ProductModel> GetListOfAllProductsOnCartPage()
        {
            ProductsInCartList.Clear(); // Clear the list since it will be rebuilt below.

            for (var index = 0; index < UniqueProductsCount; index++) { ProductsInCartList.Add(new Utilities.ProductModel(ProductName(index), ProductSku(index), ProductQtyMobileLabel(index), ProductTotalPrice(index))); }

            return ProductsInCartList;
        }

        public override void OpenShippingOptions()
        {
            Browser.ClickOnButtonMultipleTimes(ChangeShippingOptionsLink, 5, IsShippingModalVisible);
        }

        public override void ShippingUpdate()
        {
            UpdateShippingOptionButton.Click();

            CloseButton.Click();

            Browser.Wait.UntilElementDoesntExist(_jsCloseShippingOptionsOverlayClass);
        }

        public override void RemoveCartItems()
        {
            if (Browser.Locate.DoesElementExistImmediately(RemoveItemClass.ToCssClassSelector()))
            {
                var itemCount = RemoveItemLinksElements.Count;

                for (var i = 0; i < itemCount; i++)
                {
                    var cachedLink = RemoveItemLinksElements[0];

                    if (cachedLink != null)
                    {
                        Browser.Wait.ForDisplayedElement(cachedLink).Click();
                    }
                    else
                    {
                        return;
                    }

                    Browser.Wait.UntilElementUnloads(cachedLink, 60);
                }
            }

            Browser.Wait.IsVisibleElement(By.CssSelector(CartEmptyWarningClass.ToCssClassSelector()), 30);
        }

        public override void InputEmailRecipientsInForm(string[] recipientEmails)
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(EmailRecipientsId.ToCssIdSelector()));
            FormEmailRecipientsField.SendKeys(String.Join(", ", recipientEmails));  
        }

        public override void OpenAndFocusEmailModal()
        {
            Browser.Wait.IsVisibleElement(By.XPath(_emailButtonXpath));
            Browser.ClickByJs(EmailButton);
        }

        public override void EmailShoppingCart(params string[] recipientEmails)
        {
            OpenAndFocusEmailModal();

            InputEmailRecipientsInForm(recipientEmails);
            Browser.Wait.IsVisibleElement(By.CssSelector(FirstNameId.ToCssIdSelector()));

            FormFirstNameField.SendKeys("LPFirst");
            FormLastNameField.SendKeys("LPLast");
            FormEmailFromField.SendKeys("fedcsrmanager@lampsplus.com");
            Browser.Wait.IsVisibleElement(By.Id(_emailCartSubmitBtnId));

            SendEmailButton.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(_successMessageClass.ToCssClassSelector()));
        }

        public void CloseEmailModal()
        {
            Browser.Wait.IsVisibleElement(By.XPath(_emailCartContainerXpath));
            Browser.Locate.ElementByXpath(_emailCartContainerXpath).Click();
        }

        public void ScrollToPromoCodeSection()
        {
            Browser.ScrollIntoView(ChangeShippingOptionsLink);
            Browser.Wait.ForDomReady();
            Browser.Wait.IsVisibleElement(By.CssSelector(_cartidSelector), 5);
        }

        public void UpdateZipCodeFlow(string zipCode)
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(JsChangeShippingOptionsClass.ToCssClassSelector()));
            ChangeShippingOptionsLink.Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(_shipZipFieldId.ToCssIdSelector()));
            ShippingZipField.SendKeys(zipCode);
            ShipTabSearchButton.Click();
            Browser.Wait.IsVisibleElement(By.XPath(_standardShippingXpath));
            StandardShipping.Click();
            Browser.Wait.ForElementToStopAnimating(ShippingOptionModal);
            Browser.RefreshPage(); // Refresh page to avoid stale element exceptions.
        }

        public override IElement GetToolTip()
        {
            Browser.ScrollIntoView(ToolTip);
            return ToolTip;
        }

        public override decimal EnterCartZipCodeForShippingOption(string countryCode, string zipCode, int index)
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(JsChangeShippingOptionsClass.ToCssClassSelector()));
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

            Browser.Wait.ForDomReady();
            Browser.Wait.WaitForAjaxComplete();
            Browser.Wait.IsVisibleElement(By.CssSelector(AvailableShippingOptionsClass.ToCssClassSelector()));

            var radios = ShippingTypeRadios;
            Browser.Wait.IsVisibleElement(By.ClassName(JsShippingCostClass));

            var shippingTotal = GetShippingOptionTotal(index);

            radios[index].Click();

            Browser.RefreshPage();
            Browser.Wait.ForDomReady();

            return shippingTotal;
        }

        public override void RemovePromoCode()
        {
            var promoCodeElementExists = Browser.Locate.DoesElementExistImmediately(_removePromoCodeClass.ToCssClassSelector());

            if (promoCodeElementExists)
            {
                MobileRemovePromoCode.Click();
            }
        }

        public override string GetPromoCodeStatusMessage()
        {
            return PromoCodeLabel.Text.Replace("\r\n", " ").ToLower();
        }

        public decimal GetPromoCodeDiscountDisplayed()
        {
            return decimal.Parse(PromoDiscount.Text.Split('$')[1]);
        }

        public bool IsPromoCodeMessageVisible()
        {
            return Browser.Wait.IsVisibleElement(By.CssSelector(_cartTermsAndConditionsId.ToCssIdSelector()));
        }

        public override string UndoMessageProductName()
        {
            var productName = Browser.Locate.ElementByClassName(UndoBlockClass).Text;
            return productName.Replace("\r\n", "").Replace("Undo", "");
        }
    }
}
