using System;
using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Pages.Mobile
{
    /// <summary>
    /// https://www.lampsplus.com/secure/cart/shipping/.
    /// </summary>
    public class MobileShipping : ShippingBase
    {
        /// <inheritdoc />
        public MobileShipping(IBrowser browser) : base(browser) { }

        #region CSS Selector Strings
        public override string AssetCounterContainerXpath { get; } = "//*[@id=\"assetCountsContainer\"]/button";
        public override string CartShippingId { get; } = "cartShipping";
        public override string ShippingTypeShippingCostClass { get; } = "shippingType__shippingCost";
        public override string HideMobileDrawerClass { get; } = "hideMobileDrawer";
        public override string CartId { get; } = "cartId";
        public override string CartInfoBottom { get; } = "cartInfoBottom";
        public override string LpMobileDrawerContainerClass { get; } = "lpMobileDrawerContainer";
        public override string BdCartShippingId { get; } = "bdCartShipping";
        public override string AddNewAddressDrawerClass { get; } = "addNewAddressDrawer";
        public override string LpMobileDrawerClass { get; } = "lpMobileDrawer";
        public override string LpmdRightClass { get; } = "lpmdRight";
        public override string LpmdFullScreenClass { get; } = "lpmdFullScreen ";
        public override string shippingDrawerClass { get; } = "shippingDrawer";
        public override string SelectShippingAddressClass { get; } = "subTitle";

        public override string ShippingCellShippingCostClass => throw new NotImplementedException();
        public override string ShowAnotherAddressFieldClass => throw new NotImplementedException();
        public override string ShippingAddressModalId => throw new NotImplementedException();

        #endregion

        #region Page Elements

        public override IElement SelectShippingAddress => Browser.Locate.ElementByXpath("//h3[text()='Select a Shipping Address']");
        public override IElement ShippingCellShippingCost => Browser.Locate.ElementByClassName(ShippingTypeShippingCostClass);
        public override IElement ShippingPage => Browser.Locate.ElementById(CartShippingId);
        public override IElement ShippingPageCartInfo => Browser.Locate.ElementByClassName(CartInfoBottom);
        public override IElement ShippingPageCartNumber => Browser.Locate.ElementByXpath("//*[@id='cartId']");
        public override IElement MobileAssetCounterButton => Browser.Locate.ElementByXpath(AssetCounterContainerXpath);
        public override IElement MobileShippingOptionsModal => Browser.Locate.ElementByXpath("//*[@id=\"changeShippingOptionsOverlay\"]//div[contains(@class, 'available-shipping-options__days')]");
        public override IElement NewShippingAddressFormContainer => Browser.Locate.ElementByClassName(LpMobileDrawerContainerClass);
        public override IElement ShippingInformationPageContainer => Browser.Locate.ElementBySelector(BdCartShippingId.ToCssIdSelector());
        public override IElement CloseShippingPage => Browser.Locate.ElementBySelector($"{AddNewAddressDrawerClass.ToCssClassSelector()} {HideMobileDrawerClass.ToCssClassSelector()}");
        public override IElement SelectNonDefaultAddress => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Label, HtmlTextWriterAttribute.For, "address1");
        public override IElement NewShippingAddressFormFullContent => Browser.Locate.ElementBySelector($"{BdCartShippingId.ToCssIdSelector()} > {HtmlTextWriterTag.Div}{AddNewAddressDrawerClass.ToCssClassSelector()}{shippingDrawerClass.ToCssClassSelector()}{LpMobileDrawerClass.ToCssClassSelector()}{LpmdRightClass.ToCssClassSelector()}{LpmdFullScreenClass.ToCssClassSelector()} > {HtmlTextWriterTag.Div} > {HtmlTextWriterTag.Div}");
        public override IElement MultipleShipppingGiftCardTo(int index) => Browser.Locate.ElementsByXpath(MultipleShipppingGiftCardToXpath)[index];
        public override IElement SelectShippingAddressOption => throw new NotImplementedException();
        #endregion
    }
}