using System;
using System.Web.UI;
using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Pages.Desktop
{
    /// <summary>
    /// https://www.lampsplus.com/secure/cart/shipping/.
    /// </summary>
    public class Shipping : ShippingBase
    {
        /// <inheritdoc />
        public Shipping(IBrowser browser) : base(browser) { }

        #region CSS Selector Strings
        public override string ShippingCellShippingCostClass { get; } = "shippingCell__shippingCost";
        public override string ShowAnotherAddressFieldClass { get; } = "showAnotherAddressField";
        public override string SelectShippingAddressClass { get; } = "title";
        public override string ShippingAddressModalId { get; } = "lpModalContent";

        public override string AssetCounterContainerXpath => throw new NotImplementedException();
        public override string CartShippingId => throw new NotImplementedException();
        public override string ShippingTypeShippingCostClass => throw new NotImplementedException();
        public override string HideMobileDrawerClass => throw new NotImplementedException();
        public override string CartId => throw new NotImplementedException();
        public override string CartInfoBottom => throw new NotImplementedException();
        public override string LpMobileDrawerContainerClass => throw new NotImplementedException();
        public override string BdCartShippingId => throw new NotImplementedException();
        public override string AddNewAddressDrawerClass => throw new NotImplementedException();
        public override string LpMobileDrawerClass => throw new NotImplementedException();
        public override string LpmdRightClass => throw new NotImplementedException();
        public override string LpmdFullScreenClass => throw new NotImplementedException();
        public override string shippingDrawerClass => throw new NotImplementedException();
        #endregion

        #region Page Elements
        public override IElement SelectShippingAddressOption => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Label, HtmlTextWriterAttribute.For, "address1");
        public override IElement ShippingCellShippingCost => Browser.Locate.ElementByClassName(ShippingCellShippingCostClass);
        public override IElement ShippingPage => Browser.Locate.ElementByClassName(CartShippingClass);
        public override IElement MultipleShipppingGiftCardTo(int index) => Browser.Locate.ElementsByClassName(MultipleShipppingGiftCardToClass)[index];
        public override IElement ShippingPageCartInfo => throw new NotImplementedException();
        public override IElement ShippingPageCartNumber => throw new NotImplementedException();
        public override IElement MobileAssetCounterButton => throw new NotImplementedException();
        public override IElement MobileShippingOptionsModal => throw new NotImplementedException();
        public override IElement NewShippingAddressFormContainer => throw new NotImplementedException();
        public override IElement ShippingInformationPageContainer => Browser.Locate.ElementById(ShippingAddressModalId);
        public override IElement CloseShippingPage => throw new NotImplementedException();
        public override IElement SelectNonDefaultAddress => throw new NotImplementedException();
        public override IElement NewShippingAddressFormFullContent => throw new NotImplementedException();        
        #endregion
    }
}