using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Core;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Databases.Actions;
using LampsPlus.AutomationFramework.Pages.Base;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;
using OperatingSystem = LampsPlus.AutomationFramework.Utilities.TestConfiguration.OperatingSystem;
using static System.Int16;
using System.Collections.Generic;

namespace LampsPlus.AutomationFramework.Pages.Desktop
{
    /// <summary>
    /// https://www.lampsplus.com/cart/
    /// </summary>
    public class CartOverview : CartOverviewBase
	{
        /// <inheritdoc />
        public CartOverview(IBrowser browser, ShoppingCartActions shoppingCartActions, ProductActions productActions, IGlobalLocators globalLocators, IShipping shipping) : base(browser, shoppingCartActions, productActions, globalLocators, shipping) { }

        #region CSS Selector Strings
        private string PromoCodeLineClass { get; } = "promoCodeLine";
        private string ProfSavingsClass = "profSavings";
        private string AdditionalDiscountsClass = "additionalDiscounts";
        private string DataProfessionalAccountNumberAttribute { get; } = "data-professionalaccountnumber";
        private string MdmaUserNameId { get; } = "mdmaUserName";
        private string MdmaPasswordId { get; } = "mdmaPassword";
        private string OrderSummaryContainerId { get; } = "orderSummaryContainer";
        private string ResponseErrorsClass { get; } = "responseErrors";
        private string SubmitClass { get; } = "submit";
        private string TacClass { get; } = "tac";
        private string SaleCountDownClass { get; } = "saleCountdown";
        public override string ShippingOptionContainerClass { get; } = "jsAvailableShippingOptionsContainer";
        public override string SubTotalOnCartClass { get; } = "//div[contains(text(),'Subtotal')]/following-sibling::div";
        public override string CartPromotionalCodeId { get; } = "cartPromotionalCode";
        public override string CartId => CartIdElement.Text.Replace("Cart #", string.Empty);
        public override string ShippingAndReturnType { get; } = "shippingCell__shippingCost";
        public override string ShowUpTooltipXpath { get; } = "//*[@id='cartOverview']//div[contains(@class, 'showUp')]";
        public override string CartTitleXpath { get; } = "//*[@id='csrCartTitle']";
        public override string ProductQtyDropdownXpath { get; } = "//select[@class='prodQtyDropdown']";
        public override string ProductQtyFieldClass => throw new NotImplementedException();
        public override string ChangeShippingOptionsClass => throw new NotImplementedException();
        #endregion

        #region Page Elements
        public override IElement SaleCountDown => Browser.Locate.ElementByClassName(SaleCountDownClass);
        public override IElement ShippingOptionContainer => Browser.Locate.ElementByClassName(ShippingOptionContainerClass);
        public override IElement SubTotalOnCart => Browser.Locate.ElementByXpath(SubTotalOnCartClass);
        public override IElement ChangeShippingOptionsLink => Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.Button, JsChangeShippingOptionsClass);
        public override IElement ShippingZipField => Browser.Locate.ElementById(ShipZipFieldId);
        public override IElement UpdateShipButton => Browser.Locate.ElementByClassName(JsUpdateShipBtnClass);
        public override IElement ApplyDiscountButton => Browser.Locate.ElementByClassName(BtnApplyDiscountClass);
        public override IElement CartEditPriceElement => Browser.Locate.ElementByClassName(EditPriceClass);
        public override IElement CartIdContainer => Browser.Locate.ElementById(OrderSummaryContainerId).FindElement(By.ClassName(TacClass));
        public override IElement CartPromotionalButton => Browser.Locate.ElementByClassName(AddPromoCodeLinkClass);
        public override IElement CloseShippingOptionElement => throw new NotImplementedException();
        public override IElement CompanyNameField => Browser.Locate.ElementByClassName(ApaQueryClass);
        public override IElement DiscountDropdown => Browser.Locate.ElementByClassName(SelDiscountReasonClass, DiscountTooltip);
        public override IElement DiscountPercentTextBox => Browser.Locate.ElementByClassName(TxtPercentDiscClass, DiscountTooltip);
        public override IElement DiscountTooltip => Browser.Locate.ElementByClassName(DiscountTooltipClass);
        public override IElement DiscountTooltipRemoveButton => Browser.Locate.ElementByClassName(LnkRemoveDiscountClass);
        public override IElement ProfessionalAccountLabel => Browser.Locate.ElementById(ProfessionalAccountLabelId);
        public override IElement StoreInventoryUpdateButton => Browser.Locate.ElementByClassName(JsUpdateStoreInventoryBtnClass);
        public override IElement ShowUpTooltip => Browser.Locate.ElementByXpath(ShowUpTooltipXpath);
        public override IElement StoreInventoryZipField => Browser.Locate.ElementByClassName(JsZipForInventoryClass);
        public override IElement StoreInventorySearchButton => Browser.Locate.ElementByClassName(JsInventoryZipApplyClass);
        public override IElement ShippingOptionModal => Browser.Locate.ElementById(SelectDeliveryOptionsModalId);
        public override IElement CompanyNameLink => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Button, DataProfessionalAccountNumberAttribute, "1000000246", GlobalLocators.Iframe);
        public override IElement StorePickupElement => Browser.Locate.ElementsByClassName(JsDeliveryOptionsTabClass)[1];
        public override IElement StoreInventoryTab => Browser.Locate.ElementsByClassName(JsDeliveryOptionsTabClass).Last();
        public override IElement StorePickupZipField => Browser.Locate.ElementByClassName(JsZipForStoreClass);
        public override IElement SubTotalLabel => OrderSummaryBlockLabel(SubtotalPrefix);
        public override IElement SelDiscountReasonField => Browser.Locate.ElementByClassName(SelDiscountReasonClass, ShowUpTooltip);
        public override IElement TextPercentDiscountField => Browser.Locate.ElementByClassName(TxtPercentDiscClass, ShowUpTooltip);
        public override IElement RemoveProfessionalAccountLink => Browser.Locate.ElementById(RemoveProfessionalAccountId);
        public override IElement AddProfessionalAccountLink => Browser.Locate.ElementById(AddProfessionalAccountId);
        public override IElement ShippingOptionsRadioButton => Browser.Locate.ElementByClassName(FieldRadioClass);
        public override IElement ShipZipField => Browser.Locate.ElementById(ShipZipFieldId);
        public override IElement StorePickupSearchButton => Browser.Locate.ElementByClassName(JsStoreZipApplyClass);
        public override IElement StorePickupUpdateButton => Browser.Locate.ElementByClassName(JsUpdateStorePickupBtnClass);
        public override IElement AuthorizationModalUsernameInput => Browser.Locate.ElementById(MdmaUserNameId);
        public override IElement AuthorizationModalPasswordInput => Browser.Locate.ElementById(MdmaPasswordId);
        public override IElement ModalSubmitButton => Browser.Locate.ElementByClassName(SubmitClass, GlobalLocators.Iframe);
        public override IElement AuthorizationModalErrorText => Browser.Locate.ElementByClassName(ResponseErrorsClass, GlobalLocators.Iframe);

        public override ReadOnlyCollection<IElement> PosCheckBoxes => Browser.Locate.ElementsByClassName(PosPurchaseOptionCheckboxClass);
        public override ReadOnlyCollection<IElement> ShippingTypeRadios => Browser.Locate.ElementsByClassName(FieldRadioClass);
        public override ReadOnlyCollection<IElement> RemoveItemLinksElements => Browser.Locate.ElementsByXpath("//button[@class= 'anchorLink removeItem']");
        public override IElement ProductQtyDropdownField => Browser.Locate.ElementByXpath(ProductQtyDropdownXpath);
        public override IElement CartSignInLink => throw new NotImplementedException();
        public override IElement CloseButton => throw new NotImplementedException();
        public override IElement CloseShippingOptionsOverlay => throw new NotImplementedException();
        public override IElement MobileRemovePromoCode => throw new NotImplementedException();
        public override IElement PromoCode => throw new NotImplementedException();
        public override IElement ProductQtyField => throw new NotImplementedException();
        #endregion

        public override void ApplyDiscountIosPlatform(string discountValue, string discountReason)
        {
            //Click on Percent Discount text field
            Browser.Locate.ElementByXpath("//XCUIElementTypeOther[@name='Shopping Cart | Lamps Plus']/XCUIElementTypeTextField[5]").Click();

            //Enter discount
            if (Parse(discountValue) > 9)//if discount not a single digit
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

        public override void ApplyPromoCode()
        {
            PromoInputField.SendKeys(Keys.Return);
            Browser.Wait.IsVisibleElement(By.XPath(RemovePromoCodeXpath));
            Browser.Wait.ForDisplayedElement(RemovePromoCodeElement, 30);
        }

	    public override decimal GetActualPromoCodeDiscountPrice()
	    {
	        var promoCodeLine = Browser.Locate.ElementByClassName(PromoCodeLineClass);
	        var promoCodeValue = Browser.Locate.ElementByClassName(GlobalLocators.OsValueClass, promoCodeLine, true);
	        return GetLabelCost(promoCodeValue.Text, " $");
        }

        public override decimal GetProfessionalSavingsPrice()
        {
            var ProfSavings = Browser.Locate.ElementByClassName(ProfSavingsClass);
            var ProfSavingValue = Browser.Locate.ElementByClassName(GlobalLocators.OsValueClass, ProfSavings, true);
            return GetLabelCost(ProfSavingValue.Text, " $");
        }

        public override decimal GetAdditionalDiscountsPrice()
        {
            var AdditionalDiscounts = Browser.Locate.ElementByClassName(AdditionalDiscountsClass);
            var AdditionalDiscountsValue = Browser.Locate.ElementByClassName(GlobalLocators.OsValueClass, AdditionalDiscounts, true);
            return GetLabelCost(AdditionalDiscountsValue.Text, " $");
        }

        public override decimal GetActualPromoCodeDiscount()
        {
            Browser.Wait.WaitForAjaxComplete();
            var promoCodeValue = Browser.Locate.ElementByXpath("//*[@id='!orderSummary']/div[2]/div[2]");
            return GetLabelCost(promoCodeValue.Text, "$");
        }

       
        public override void RemovePromoCode()
        {
            Browser.Wait.ForDomReady(30);

            var promoCodeElementExists = Browser.Locate.DoesElementExistImmediately(JsShowPromoCodeTermsAndConditionsClass.ToCssClassSelector());

            if (promoCodeElementExists)
            {
                RemovePromoCodeElement.Click();
                WaitForPromoCodeToUnload();
            }
        }

        public override void WaitForPromoCodeToUnload() { Browser.Wait.UntilElementUnloads(PromoCodeText); }

        public override void ApplyDiscount(string discountApplied, string discountReasonApplied, OperatingSystem operatingSystem)
        {
            if (operatingSystem == OperatingSystem.iPad)
            {
                ((IpadBrowser)Browser).SwitchToNativeContext((AppiumDriver<AppiumWebElement>)Browser.Driver); //Switch to iOS Native context
                ApplyDiscountIosPlatform(discountApplied, discountReasonApplied); //Apply discount in iOS Native context mode
                ((IpadBrowser)Browser).SwitchToWebViewContext((AppiumDriver<AppiumWebElement>)Browser.Driver); //Switch back to iOS WebView context
                ApplyDiscountButton.Click();
            }
            else
            {
                new SelectElement(DiscountDropdown.InternalElement).SelectByIndex(1);
                DiscountPercentTextBox.SendKeys(discountApplied);
                ApplyDiscountButton.Click();
            }
        }

        public override bool DoesCartMatchAddedProducts(Dictionary<string, int> addedProducts)
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
    }
}
