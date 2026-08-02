using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Utilities;
using OpenQA.Selenium;

using Page = Automation.Framework.Core.Page;

namespace LampsPlus.AutomationFramework.Pages.Base
{
    /// <summary>
    /// Base class for common behavior between desktop and mobile views.
    /// </summary>
    public abstract class StoresBase : Page, IStores
    {
        /// <inheritdoc />
        protected StoresBase(IBrowser browser, IGlobalLocators globalLocators) : base(browser) { GlobalLocators = globalLocators; }

        #region Class Setup
        internal IGlobalLocators GlobalLocators { get; }

        public string MakeThisMyStoreString { get; } = "make this my store";
        #endregion

        #region CSS Selector Strings
        private string AddressStoreClass { get; } = "addressStore";
        private string BreadcrumbClass { get; } = "breadcrumb";
        private string OpenNowClass { get; } = "openNow";
        private string ScheduleAppointmentLinkClass { get; } = "scheduleAppointmentLink";
        private string StoreNameClass { get; } = "storeName";
        private string StoreInfoClass { get; } = "storeInfo";

        public string AllStoresLampsPlusLinkClass { get; } = "allStoresLampsPlus__link";
        public string AllStoresLampsPlusId { get; } = "allStoresLampsPlus";
        public string BopusSubmenuId { get; } = "bopusSubmenu";
        public string CallForAppointmentInStoreId { get; } = "callForAppointmentInStore";
        public string DivStoreResultClass { get; } = ".storeResultContainer div.divStoreResult:not(.emptyFlexFiller)";
        public string HeaderDropDownsMenuClass { get; } = "headerDropDowns-menu";
        public string GetDirectionsBtnId { get; } = "mapDirections";
        public string GetDirectionsIconId { get; } = "directionsIcon";
        public string MakeThisMyStoreClass { get; } = "makeThisMyStore";
        public string MakeThisMyStoreXpath { get; } = "//*[@id='makeThisMyStoreBlock']/button";
        public string MakeThisMyStoreBlockClass { get; } = "makeThisMyStoreBlock";
        public string MyStore { get; } = "//button[@class='calloutBtn makeThisMyStore myStore noPointerEvents']";
        public string MyStoreClass { get; } = "myStore";
        public string MyStoreWrapperClass { get; } = "myStoreWrapper";
        public string ScottsdaleStoreXpath { get; } = "//*[text() = 'Scottsdale']";
        public string StoreDetailsSelectorName { get; } = ".divStoreResultContent h2 span";
        public string StoreDetailsSelectorLink { get; } = ".divStoreResult__links a:not(.scheduleAppointmentLink)";
        public string StoreZipCodeInputClass { get; } = "searchZipBtn";
        public string StorePhotosImgId { get; } = "storePhotosImg";
        public string StoreLinksClass { get; } = "storeLink";
        public string StoresOptionsXpath { get; } = "//*[@class='storeOptions']";
        public string MakeMyStoreClass { get; } = "makeMyStore";

        public abstract string AllStoresListClass { get; }
        public abstract string DirectionsButtonXpath { get; }
        public abstract string DivStoreSearchResultClass { get; }
        public abstract string LpIconCalendarClass { get; }
        public abstract string LpIconCallClass { get; }
        public abstract string LpIconCouponClass { get; }
        public abstract string LpIconDetailsClass { get; }
        public abstract string LpIconDirectionsClass { get; }
        public abstract string MapsString { get; }
        public abstract string MyStoreBlockClass { get; }
        public abstract string NavWishlistClass { get; }
        public abstract string StoreDetailsBtnClass { get; }      
        public abstract string StoreDetailsLink { get; }
        public abstract string StorePickerSubmitId { get; }
        public abstract string StoreZipCodeInputId { get; }      
        #endregion

        #region Page Elements
        public IElement MakeThisMyStoreButton => Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.Button, MakeThisMyStoreClass);
        public IElement MyStoreButton => Browser.Locate.ElementByXpath(MyStore);
        public IElement AddressRegionField => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Span, ItemPropAttribute, "addressRegion");
        public IElement PostalCodeField => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Span, ItemPropAttribute, "postalCode");
        public IElement StreetAddressField => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Span, ItemPropAttribute, "streetAddress");
        public IElement OpenNow => Browser.Locate.ElementByClassName(OpenNowClass);
        public IElement SelectedStoreElement { get; set; }
        public abstract IElement RandomStoreElement { get; }
        public abstract IElement AddressLocalityField { get; }
        public abstract IElement AllStoresLampsPlus { get; }
        public abstract IElement BopusSubmenu { get; }
        public abstract IElement GetDirectionsButton { get; }
        public abstract IElement LpIconCouponElement { get; }
        public abstract IElement LpIconCalendarElement { get; }
        public abstract IElement LpIconCallElement { get; }
        public abstract IElement LpIconDetailsButton { get; }
        public abstract IElement LpIconDirectionsElement { get; }
        public abstract IElement MakeThisMyStoreContainer { get; }
        public abstract IElement MyStoreWrapper { get; }
        public abstract IElement NearByZipStores { get; }
        public abstract IElement SelectedStoreDetailsLink { get; }
        public abstract IElement StoreZipCodeInputElement { get; }
        public abstract IElement StorePickerSubmitElement { get; }
        public abstract IElement StorePhotosImgElement { get; }
        public abstract IElement StoresDropDownMenu { get; }
        public abstract IElement SelectStoreNearMeLinks { get; }
        public abstract IElement RandomStoreNearMeElement { get; }
        public abstract IElement SelectedStoreDetailsName { get; }

        public abstract ReadOnlyCollection<IElement> StoreResults { get; }
        public abstract ReadOnlyCollection<IElement> StoreDetailBtns { get; }
        public abstract ReadOnlyCollection<IElement> StoreDetailsRegionLinks { get; }
        public abstract ReadOnlyCollection<IElement> StoreNearMeLinks { get; }
        public abstract ReadOnlyCollection<IElement> AllStoresLampsPlusLinks { get; }
        public abstract ReadOnlyCollection<IElement> LampsPlusStoreRegionLinks { get; }
        #endregion

        public string BreadcrumbText => Browser.Locate.ElementByClassName(BreadcrumbClass).Text.ToLower();
        public string DropdownMyStoreName => Browser.Locate.ElementBySelector("div.submenuList > div.storeName > a").Text.ToLower();
        public string DropdownMyStoreAddress => Browser.Locate.ElementByClassName(StoreInfoClass).Text;
        public string StoreAddress => Browser.Locate.ElementByClassName(AddressStoreClass).Text;

        /// <inheritdoc />
        public void ClickMakeThisMyStoreButton()
        {
            if (MakeThisMyStoreButton.Text.ToLower() == MakeThisMyStoreString)
            {
                MakeThisMyStoreButton.Click();
            }
        }

        /// <inheritdoc />
        public List<string> GetLinkTextFromStoreResult(IElement storeResult)
        {
            var details = new List<string>()
            {
                TextActions.RegexNoTabsAndNewLines(storeResult.FindElement(By.CssSelector(StoreDetailsSelectorLink)).Text.ToLower().Trim()),
                storeResult.FindElement(By.ClassName(ScheduleAppointmentLinkClass)).Text.ToLower(),
                storeResult.FindElement(By.ClassName(MakeThisMyStoreClass)).Text.ToLower()
            };

            return details;
        }

        /// <inheritdoc />
        public abstract string GetDetailBtnStoreResult(IElement storeResult);

        /// <inheritdoc />
        public abstract string GetMakeThisMyStoreResult(IElement storeResult);

        public bool IsStoreSelected(int timeToWait)
        {
            return Browser.Wait.ForCondition(() => Browser.PageUrl.Contains("www.google.com/maps"));
        }

        public bool IsStoreSetToMyStore(int timeToWait)
        {
            return Browser.Wait.IsVisibleElement(By.CssSelector(MyStoreClass.ToCssClassSelector()), timeToWait);
        }
    }
}
