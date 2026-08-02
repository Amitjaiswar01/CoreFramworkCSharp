using System;
using System.Data;
using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Pages.Desktop
{
    /// <summary>
    /// https://www.lampsplus.com/secure/cart/shipping/.
    /// </summary>
    public class ShippingInfo : ShippingInfoBase
    {
        /// <inheritdoc />
        public ShippingInfo(IBrowser browser, IGlobalLocators globalLocators) : base(browser, globalLocators) { }

        #region CSS Selector Strings
        public static string ShippingCellShippingCostClass => "shippingCell__shippingCost";
        #endregion

        #region Page Elements
        public override IElement ShippingCellShippingCost => Browser.Locate.ElementByClassName(ShippingCellShippingCostClass);
        public override IElement ShippingPage => Browser.Locate.ElementByClassName(CartShippingClass);
        public override IElement ShippingHideMobileDrawer => throw new NotImplementedException();
        public override IElement ShippingPageCartNumber => throw new NotImplementedException();
        public override IElement NewShippingAddressFormContainer => throw new NotImplementedException();
        public override IElement ShippingInformationPageContainer => throw new NotImplementedException();
        public override IElement CloseShippingPage => throw new NotImplementedException();
        public override IElement SelectNonDefaultAddress => throw new NotImplementedException();
        #endregion
    }
}
