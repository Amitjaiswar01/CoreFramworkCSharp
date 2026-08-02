using System;
using System.Collections.ObjectModel;
using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Base;
using OpenQA.Selenium;



namespace LampsPlus.AutomationFramework.Pages.Mobile
{
	/// <summary>
	/// Global locators used for mobile tests.
	/// </summary>
	public class MobileGlobalLocators : GlobalLocatorsBase
	{
        public MobileGlobalLocators(IBrowser browser) : base(browser) { }

        #region Class Setup
        public override string NotifyMyPhoneNumberString { get; } = "888-739-0201";

        public override string LpModalXpath => throw new NotImplementedException();
        public override string GlobalHtml => throw new NotImplementedException();
        #endregion

        #region CSS Selectors

        private string BannerCandleholdersClass { get; } = "banner-candleholders";
        private string GlobalMenuId { get; } = "globalMenu";

        public override string BillingStateSelectorId { get; } = "lpSelectMobileDrawer__singleShippingState-view29";
        public override string CalloutBtnList { get; } = "calloutBtnList";
        public override string ConfirmDrawerActionClass { get; } = "confirmDrawerAction";
        public override string CountrySelectorId { get; } = "countrySelector";
        public override string HideMobileDrawerClass { get; } = "hideMobileDrawer";
        public override string LpDropdownPanelClass { get; } = "lpDropdown__panel";
        public override string LpmcToggleCollapsibleClass { get; } = "lpMobileCollapsible";
        public override string LpmmMenuClass { get; } = "lpmmMenu";
        public override string LpmmMenuContainer { get; } = "lpmmMenuContainer";
        public override string LpmmOpenClass { get; } = "lpmmOpen";
        public override string LpMobileDrawerClass { get; } = "lpMobileDrawer";
        public override string LpMobileOverlayClass { get; } = "lpMobileOverlay";
        public override string LpMobileOverlayContentClass { get; } = "lpMobileOverlayContent";
        public override string RemoveItemClass { get; } = "removeItem";
        public override string CloseLpModalClass { get; } = "toggleSortMenu";
        public override string RemoveCartItemButtonClass { get; } = "removeItem";
        #endregion

        #region Page Elements

        //Elements that exist in both Desktop and Mobile views but are located differently.
        public override IElement Iframe => Browser.Locate.ElementByClassName(LpMobileOverlayContentClass);

        //Elements that exist in Mobile view and NOT Desktop view.
        public override IElement RemoveCartItemButton(int index) => Browser.Locate.ElementsByClassName(RemoveCartItemButtonClass)[index];
        public override IElement LpDropdownPanel => Browser.Locate.ElementByClassName(LpDropdownPanelClass);
        public override IElement BannerCandleholders => Browser.Locate.ElementBySelector(BannerCandleholdersClass.ToCssClassSelector());
        public override IElement CandleHoldersAnimatedGif => Browser.Locate.ElementByTagName(HtmlTextWriterTag.Img, BannerCandleholders);
        public override IElement CloseDrawerButton => Browser.Locate.ElementByClassName(CalloutBtnClass, DisplayedMobileDrawerMenu);
        public override IElement CountryDropdown => Browser.Locate.ElementById(CountrySelectorId);
        public override IElement DisplayedMobileDrawerMenu => Browser.Locate.ElementByClassName(LpmmMenuClass);
        public override IElement GlobalMenu => Browser.Locate.ElementBySelector(GlobalMenuId.ToCssIdSelector());
        public override IElement LpMobileDrawerElement => Browser.Locate.ElementBySelector(LpMobileDrawerClass.ToCssClassSelector());
        public override IElement LpMobileOverlayElement => Browser.Locate.ElementByClassName(LpMobileOverlayClass);
        public override IElement LpMobileOverlayVideoElement => Browser.Locate.ElementByClassName(LpMobileOverlayVideoClass);
        public override IElement LpModalCloseElement => Browser.Locate.ElementBySelector(LpMobileOverlayCloseClass.ToCssClassSelector());
        public override IElement CloseLpModal => Browser.Locate.ElementByClassName(CloseLpModalClass);
        public override IElement LpModalCloseVideoElement => Browser.Locate.ElementBySelector(".lpMobileOverlay--withVideo .lpMobileOverlayClose");
        public override IElement MobileDrawerMenuInnerContainer => Browser.Locate.ElementByClassName(LpmmMenuContainer, DisplayedMobileDrawerMenu);
        public override IElement PdpDrawerElement => Browser.Locate.ElementByClassName(LpmcToggleCollapsibleClass);
        public override IElement StateDropdown => Browser.Locate.ElementById(StateSelectorId);

        public override IElement BillingStateDropdown => Browser.Locate.ElementById(BillingStateSelectorId);

        public override ReadOnlyCollection<IElement> PdpDrawerElements => Browser.Locate.ElementsByClassName(LpmcToggleCollapsibleClass);

        //Elements that exist in Desktop view and NOT Mobile view.
        public override IElement AllPageContent => throw new NotImplementedException();
        public override IElement CalloutButton => throw new NotImplementedException();
	    public override IElement IframeModal => throw new NotImplementedException();
        public override IElement LpModalBackdrop => throw new NotImplementedException();
        public override IElement LpModalContent => throw new NotImplementedException();
        #endregion

        public override void ClickDropdownByValue(IElement element, string optionValue)
        {
            var valueAttribute = string.Equals(element.TagName, HtmlTextWriterTag.Select.ToString(), StringComparison.CurrentCultureIgnoreCase)
                ? HtmlTextWriterAttribute.Value.ToString().ToLower()
                : "data-value";

            var option = Browser.Wait.ForElement(element.FindElement(By.CssSelector($"[{valueAttribute}*={optionValue}]")));

            if (!element.Displayed)
            {
                element.Click();
            }

            Browser.Wait.ForElementToStopAnimating(option);

            Browser.ScrollIntoView(option);
            option.Click();

            Browser.Wait.ForElementToStopAnimating(option);
        }         
    }
}
