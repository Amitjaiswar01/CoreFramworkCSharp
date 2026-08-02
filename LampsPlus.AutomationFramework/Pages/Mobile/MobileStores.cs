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
    /// https://www.lampsplus.com/stores/
    /// </summary>
    public class MobileStores : StoresBase
    {
        #region CSS Selector Strings

        public override string MapsString { get; } = "https://maps.google.com/maps?saddr=";
        public override string AllStoresListClass { get; } = "list";
        public override string DirectionsButtonXpath { get; } = "//*[@id=\"directionsIcon\"]/div";
        public override string DivStoreSearchResultClass { get; } = "divStoreResultButtons";
        public override string MyStoreBlockClass { get; } = "group";
        public override string StoreZipCodeInputId { get; } = "zipCode";
        public override string StorePickerSubmitId { get; } = "storePickerSubmit";
        public override string StoreDetailsLink { get; } = ".group a";
        public override string StoreDetailsBtnClass { get; } = "storeDetailsBtn";
        public override string NavWishlistClass { get; } = "navWishlist";
        public override string LpIconCallClass { get; } = "lpIcon-call";
        public override string LpIconDetailsClass { get; } = "lpIcon-more";
        public override string LpIconDirectionsClass { get; } = "lpIcon-directions";
        public override string LpIconCalendarClass { get; } = "lpIcon-calendar01";
        public override string LpIconCouponClass { get; } = "lpIcon-coupon";
        #endregion

        #region Page Elements
        public override IElement AllStoresLampsPlus => Browser.Locate.ElementByClassName(AllStoresListClass);
        public override IElement AddressLocalityField => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Span, ItemPropAttribute, "name");
        public override IElement LpIconCouponElement => Browser.Locate.ElementByClassName(LpIconCouponClass);
        public override IElement LpIconCalendarElement => Browser.Locate.ElementByClassName(LpIconCalendarClass);
        public override IElement LpIconCallElement => Browser.Locate.ElementByClassName(LpIconCallClass);
        public override IElement LpIconDetailsButton => Browser.Locate.ElementByClassName(LpIconDetailsClass);
        public override IElement LpIconDirectionsElement => Browser.Locate.ElementByClassName(LpIconDirectionsClass);
        public override IElement MakeThisMyStoreContainer => Browser.Locate.ElementByClassName(MyStoreBlockClass);
        public override IElement StoreZipCodeInputElement => Browser.Locate.ElementById(StoreZipCodeInputId);
        public override IElement StorePickerSubmitElement => Browser.Locate.ElementById(StorePickerSubmitId);
        public override IElement StorePhotosImgElement => Browser.Locate.ElementById(StorePhotosImgId);
        public override IElement GetDirectionsButton => Browser.Locate.ElementById(GetDirectionsIconId);
        public override IElement SelectedStoreDetailsLink => SelectedStoreElement.FindElement(By.CssSelector(StoreDetailsLink));
        public override IElement SelectStoreNearMeLinks => SelectedStoreElement.FindElement(By.CssSelector(StoreDetailsLink));
        public override IElement RandomStoreNearMeElement => ElementActions.SelectRandom(StoreNearMeLinks);

        public override IElement RandomStoreElement => throw new NotImplementedException();
        public override IElement BopusSubmenu => throw new NotImplementedException();
        public override IElement MyStoreWrapper => throw new NotImplementedException();
        public override IElement SelectedStoreDetailsName => throw new NotImplementedException();
        public override IElement StoresDropDownMenu => throw new NotImplementedException();

        public override ReadOnlyCollection<IElement> AllStoresLampsPlusLinks => LampsPlusStoreRegionLinks;
        public override ReadOnlyCollection<IElement> LampsPlusStoreRegionLinks => Browser.Locate.ElementsBySelector($"{HtmlTextWriterTag.Li} {HtmlTextWriterTag.A.ToFirstChildSelector()}", AllStoresLampsPlus);
        public override ReadOnlyCollection<IElement> StoreDetailBtns => Browser.Locate.ElementsByClassName(LpIconDetailsClass);
        public override ReadOnlyCollection<IElement> StoreNearMeLinks => Browser.Locate.ElementsByTagNameAndClassName(HtmlTextWriterTag.A, StoreLinksClass);
        public override ReadOnlyCollection<IElement> StoreResults =>Browser.Locate.ElementsByClassName(DivStoreSearchResultClass);
        
        public override IElement NearByZipStores => throw new NotImplementedException();
        #endregion

        /// <inheritdoc />
        public MobileStores(IBrowser browser, IGlobalLocators globalLocators) : base(browser, globalLocators) { }
        /// <inheritdoc />
       
        public override string GetDetailBtnStoreResult(IElement storeResult)
        {
            return storeResult.FindElement(By.ClassName(StoreDetailsBtnClass)).Text;
        }

        /// <inheritdoc />
        public override string GetMakeThisMyStoreResult(IElement storeResult)
        {
            return storeResult.FindElement(By.ClassName(MakeThisMyStoreClass)).Text;
        }

        /// <inheritdoc />
        public override ReadOnlyCollection<IElement> StoreDetailsRegionLinks => throw new NotImplementedException();
    }
}
