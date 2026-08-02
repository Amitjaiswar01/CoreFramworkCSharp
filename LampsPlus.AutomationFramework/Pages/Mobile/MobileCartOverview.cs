using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Databases.Actions;
using LampsPlus.AutomationFramework.Pages.Base;
using OpenQA.Selenium;
using OperatingSystem = LampsPlus.AutomationFramework.Utilities.TestConfiguration.OperatingSystem;

namespace LampsPlus.AutomationFramework.Pages.Mobile
{
    /// <summary>
    /// https://www.lampsplus.com/cart/
    /// </summary>
    public class MobileCartOverview : CartOverviewBase
    {
        /// <inheritdoc />
        public MobileCartOverview(IBrowser browser, ShoppingCartActions shoppingCartActions, ProductActions productActions, IGlobalLocators globalLocators, IShipping shipping) : base(browser, shoppingCartActions, productActions, globalLocators, shipping) { }

        #region CSS Selector Strings
        private string AvailableShippingOptionsItemClass { get; } = "available-shipping-options__item";
        private string DeliveryOptionShipZipFieldClass { get; } = "delivery-options__zip";
        private string JsApplyShipBtnClass { get; } = "jsShipZipApply";
        private string JsCloseShippingOptionsOverClass { get; } = "jsCloseShippingOptionsOverlay";
        private string CartSignInLinkId { get; } = "acccountDropdown";
        private string PromoCodeClass { get; } = "promoCode";

        public override string CartPromotionalCodeId { get; } = "cartPromotionalCode";
        public override string CartId => CartIdElement.Text.Replace("#", string.Empty);
        public override string ChangeShippingOptionsClass { get; } = "jsChangeShippingOptions";
        public string WelcomeBackMessageClass { get; } = "cartSignInSuccess";
        public override string ShippingAndReturnType { get; } = "shippingType__shippingCost";
        public override string ProductQtyFieldClass { get; } = "prodQty";
        public override string ProductQtyDropdownXpath => throw new NotImplementedException();
        public override string ShowUpTooltipXpath => throw new NotImplementedException();
        public override string SubTotalOnCartClass => throw new NotImplementedException();
        public override string CartTitleXpath => throw new NotImplementedException();
        public override string ShippingOptionContainerClass => throw new NotImplementedException();
        #endregion

        #region Page Elements
        public override IElement SubTotalOnCart => throw new NotImplementedException();
        public override IElement CartIdContainer => Browser.Locate.ElementBySelector("#cartId");
        public override IElement CartSignInLink =>Browser.Locate.ElementBySelector($"{CartSignInLinkId.ToCssIdSelector()} > {HtmlTextWriterTag.Button} > {HtmlTextWriterTag.Img}");
        public override IElement CartPromotionalButton => Browser.Locate.ElementById(CartPromotionalCodeId);
        public override IElement CloseButton => Browser.Locate.ElementByClassName(JsCloseShippingOptionsOverlayClass);
        public override IElement ChangeShippingOptionsLink => Browser.Locate.ElementByClassName(ChangeShippingOptionsClass);
        public override IElement CloseShippingOptionElement => Browser.Locate.ElementByClassName(JsCloseShippingOptionsOverClass);
        public override IElement MobileRemovePromoCode => Browser.Locate.ElementByClassName(RemovePromoCodeClass);
        public override IElement ShippingZipField => Browser.Locate.ElementByClassName(DeliveryOptionShipZipFieldClass);
        public override IElement ShipZipField => Browser.Locate.ElementByClassName(DeliveryOptionShipZipFieldClass);
        public override IElement UpdateShipButton => Browser.Locate.ElementByClassName(JsApplyShipBtnClass);
        public override IElement PromoCode => Browser.Locate.ElementBySelector(PromoCodeClass.ToCssClassSelector());
        public override IElement ShippingOptionModal => Browser.Locate.ElementById(ChangeShippingOptionsOverlayId);
        public override IElement ShippingOptionsRadioButton => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Li, HtmlTextWriterAttribute.Class, AvailableShippingOptionsItemClass);//Browser.Locate.ElementByClassName(FieldRadioClass);
        public override IElement SubTotalLabel => OrderSummaryBlockLabel(ProductTotalPrefix);
        public override IElement CloseShippingOptionsOverlay => Browser.Locate.ElementByClassName(JsCloseShippingOptionsOverlayClass);
        public override IElement ProductQtyField => Browser.Locate.ElementByClassName(ProductQtyFieldClass);
        public override IElement ProductQtyDropdownField => throw new NotImplementedException();
        public override ReadOnlyCollection<IElement> ShippingTypeRadios => Browser.Locate.ElementsBySelector(AvailableShippingOptionsItemClass.ToCssClassSelector());
        public override ReadOnlyCollection<IElement> RemoveItemLinksElements => Browser.Locate.ElementsByXpath("//span[@class='removeItemText']");

        public override IElement SaleCountDown => throw new NotImplementedException();
        public override IElement ShippingOptionContainer => throw new NotImplementedException();
        public override IElement ApplyDiscountButton => throw new NotImplementedException();
        public override IElement CartEditPriceElement => throw new NotImplementedException();
        public override IElement CompanyNameField => throw new NotImplementedException();
        public override IElement DiscountDropdown => throw new NotImplementedException();
        public override IElement DiscountPercentTextBox => throw new NotImplementedException();
        public override IElement DiscountTooltip => throw new NotImplementedException();
        public override IElement DiscountTooltipRemoveButton => throw new NotImplementedException();
        public override IElement ProfessionalAccountLabel => throw new NotImplementedException();
        public override IElement StoreInventoryUpdateButton => throw new NotImplementedException();
        public override IElement StoreInventoryZipField => throw new NotImplementedException();
        public override IElement StoreInventorySearchButton => throw new NotImplementedException();
        public override IElement CompanyNameLink => throw new NotImplementedException();
        public override IElement StorePickupElement => throw new NotImplementedException();
        public override IElement StoreInventoryTab => throw new NotImplementedException();
        public override IElement StorePickupZipField => throw new NotImplementedException();
        public override IElement SelDiscountReasonField => throw new NotImplementedException();
        public override IElement TextPercentDiscountField => throw new NotImplementedException();
        public override IElement RemoveProfessionalAccountLink => throw new NotImplementedException();
        public override IElement AddProfessionalAccountLink => throw new NotImplementedException();
        public override IElement StorePickupSearchButton => throw new NotImplementedException();
        public override IElement StorePickupUpdateButton => throw new NotImplementedException();
        public override IElement AuthorizationModalUsernameInput => throw new NotImplementedException();
        public override IElement AuthorizationModalPasswordInput => throw new NotImplementedException();
        public override IElement ModalSubmitButton => throw new NotImplementedException();
        public override IElement AuthorizationModalErrorText => throw new NotImplementedException();
        public override IElement ShowUpTooltip => throw new NotImplementedException();

        public override ReadOnlyCollection<IElement> PosCheckBoxes => throw new NotImplementedException();
        #endregion

        public override void ApplyPromoCode()
        {
            PromoInputField.SendKeys(Keys.Return);
            Browser.Wait.IsVisibleElement(By.ClassName(RemovePromoCodeClass));
        }

        /// <inheritdoc />
        public override decimal GetActualPromoCodeDiscountPrice()
        {
            var promoCodeValue = Browser.Locate.ElementByClassName(PromoDiscountClass);
            return GetLabelCost(promoCodeValue.Text, " $");
        }

        public override void RemovePromoCode()
        {
            var promoCodeElementExists = Browser.Locate.DoesElementExistImmediately(RemovePromoCodeClass.ToCssClassSelector());

            if (promoCodeElementExists)
            {
                MobileRemovePromoCode.Click();
            }
        }

        public override void WaitForPromoCodeToUnload() { Browser.Wait.UntilElementUnloads(MobileRemovePromoCode); }

        public override void ApplyDiscountIosPlatform(string discountApplied, string discountReasonApplied)
        {
            throw new NotImplementedException();
        }

        public override void ApplyDiscount(string discountApplied, string discountReasonApplied, OperatingSystem operatingSystem)
        {
            throw new NotImplementedException();
        }

        public override decimal GetActualPromoCodeDiscount()
        {
            throw new NotImplementedException();
        }

        public override decimal GetProfessionalSavingsPrice()
        {
            throw new NotImplementedException();
        }

        public override decimal GetAdditionalDiscountsPrice()
        {
            throw new NotImplementedException();
        }

        public override bool DoesCartMatchAddedProducts(Dictionary<string, int> addedProducts)
        {
            ProductsInCartList.Clear(); // Clear the list since it will be rebuilt below.

            for (var index = 0; index < UniqueProductsCount; index++) { ProductsInCartList.Add(new Utilities.ProductModel(ProductName(index), ProductSku(index), ProductQtyMobileLabel, ProductTotalPrice(index))); }

            foreach (var product in ProductsInCartList)
            {
                if (addedProducts[product.Sku] != product.Quantity) { return false; }
            }

            return true;
        }
    }
}
