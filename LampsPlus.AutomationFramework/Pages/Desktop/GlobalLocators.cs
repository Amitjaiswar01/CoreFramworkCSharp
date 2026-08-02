using System;
using System.Collections.ObjectModel;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Base;
using OpenQA.Selenium;

namespace LampsPlus.AutomationFramework.Pages.Desktop
{
    /// <summary>
    /// Common across all pages.
    /// </summary>
    public class GlobalLocators : GlobalLocatorsBase
    {

        /// <inheritdoc />
        public GlobalLocators(IBrowser browser) : base(browser) { }

        #region Class Setup
        public override string NotifyMyPhoneNumberString { get; } = "800-782-1967";
        public override string LpModalXpath { get; } = "//*[@id='lpModalContent']";
        public override string BillingStateSelectorId { get; } = "singleShippingState-view32";
        #endregion

        #region CSS Selector Strings
        public override string GlobalHtml { get; } = "globalHtml";

        public override string CalloutBtnList => throw new NotImplementedException();
        public override string ConfirmDrawerActionClass => throw new NotImplementedException();
        public override string CountrySelectorId => throw new NotImplementedException();
        public override string HideMobileDrawerClass => throw new NotImplementedException();
        public override string LpDropdownPanelClass => throw new NotImplementedException();
        public override string LpmcToggleCollapsibleClass => throw new NotImplementedException();
        public override string LpmmMenuClass => throw new NotImplementedException();
        public override string LpmmMenuContainer => throw new NotImplementedException();
        public override string LpmmOpenClass => throw new NotImplementedException();
        public override string LpMobileDrawerClass => throw new NotImplementedException();
        public override string LpMobileOverlayClass => throw new NotImplementedException();
        public override string LpMobileOverlayContentClass => throw new NotImplementedException();
        public override string RemoveItemClass => throw new NotImplementedException();
        public override string CloseLpModalClass => throw new NotImplementedException();
        public override string RemoveCartItemButtonClass => throw new NotImplementedException();
        #endregion

        #region Page Elements
        //Elements that exist in both Desktop and Mobile views but are located differently.
        public override IElement Iframe => Browser.Locate.ElementBySelector(LpModalId.ToCssIdSelector());

        //Elements that exist in Desktop view and NOT Mobile view.
        public override IElement AllPageContent => Browser.Locate.ElementById(GlobalHtml);
        public override IElement CalloutButton => Browser.Locate.ElementByClassName(CalloutBtnClass);
        public override IElement IframeModal => Browser.Locate.ElementById(ModalIframeId);
        public override IElement LpModalBackdrop => Browser.Locate.ElementById(LpModalBackdropId);
        public override IElement LpModalContent => Browser.Locate.ElementById(LpModalContentId);
        public override IElement LpModalCloseElement => Browser.Locate.ElementById(LpModalCloseId);

        // Elements that exist in Mobile and NOT Desktop
        public override IElement RemoveCartItemButton (int index) => throw new NotImplementedException();
        public override IElement LpDropdownPanel => throw new NotImplementedException();
        public override IElement BannerCandleholders => throw new NotImplementedException();
        public override IElement CandleHoldersAnimatedGif => throw new NotImplementedException();
        public override IElement CloseDrawerButton => throw new NotImplementedException();
        public override IElement CountryDropdown => throw new NotImplementedException();
        public override IElement DisplayedMobileDrawerMenu => throw new NotImplementedException();
        public override IElement GlobalMenu => throw new NotImplementedException();
        public override IElement LpMobileDrawerElement => throw new NotImplementedException();
        public override IElement LpMobileOverlayElement => throw new NotImplementedException();
        public override IElement LpMobileOverlayVideoElement => throw new NotImplementedException();
        public override IElement LpModalCloseVideoElement => throw new NotImplementedException();
        public override IElement MobileDrawerMenuInnerContainer => throw new NotImplementedException();
        public override IElement PdpDrawerElement => throw new NotImplementedException();
        public override IElement StateDropdown => throw new NotImplementedException();
        public override IElement BillingStateDropdown => Browser.Locate.ElementById(BillingStateSelectorId);
        public override IElement CloseLpModal => throw new NotImplementedException();

        public override ReadOnlyCollection<IElement> PdpDrawerElements => throw new NotImplementedException();
        #endregion

        public override void ClickDropdownByValue(IElement element, string optionValue)
        {
            element.FindElement(By.CssSelector($"[value={optionValue}]")).Click();
        }
    }
}
