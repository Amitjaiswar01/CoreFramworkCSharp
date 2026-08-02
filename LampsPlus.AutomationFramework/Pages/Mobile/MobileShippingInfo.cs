using System;
using System.Web.UI;
using Automation.Framework;

using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.AutomationFramework.Pages.Mobile
{
    /// <summary>
    /// https://www.lampsplus.com/secure/cart/shipping/.
    /// </summary>
    public class MobileShippingInfo : ShippingInfoBase
    {
        /// <inheritdoc />
        public MobileShippingInfo(IBrowser browser, IGlobalLocators globalLocators) : base(browser, globalLocators) { }

        #region CSS Selector Strings
        public static string CartShippingId => "cartShipping";
        public static string ShippingTypeShippingCostClass => "shippingType__shippingCost";
        public static string CartId => "cartId";
        
        public static string BdCartShippingId => "bdCartShipping";
        public static string AddNewAddressDrawerClass => "addNewAddressDrawer";
        #endregion

        #region Page Elements
        public override IElement ShippingCellShippingCost => Browser.Locate.ElementByClassName(ShippingTypeShippingCostClass);
        public override IElement ShippingPage => Browser.Locate.ElementById(CartShippingId);
        public override IElement ShippingHideMobileDrawer => Browser.Locate.ElementByClassName(GlobalLocators.HideMobileDrawerClass);
        public override IElement ShippingPageCartNumber => Browser.Locate.ElementById(CartId);
        public override IElement NewShippingAddressFormContainer => Browser.Locate.ElementByClassName(GlobalLocators.LpMobileDrawerContainerClass);
        public override IElement ShippingInformationPageContainer => Browser.Locate.ElementById(BdCartShippingId);
        public override IElement CloseShippingPage => Browser.Locate.ElementBySelector($"{AddNewAddressDrawerClass.ToCssClassSelector()} {GlobalLocators.HideMobileDrawerClass.ToCssClassSelector()}");
        public override IElement SelectNonDefaultAddress => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Label, HtmlTextWriterAttribute.For, "address1");
        #endregion
    }
}
