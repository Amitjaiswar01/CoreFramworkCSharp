using System;
using System.Collections.ObjectModel;
using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Base;
using OpenQA.Selenium;

namespace LampsPlus.AutomationFramework.Pages.Desktop
{
    /// <summary>
    /// https://www.lampsplus.com/stores/
    /// </summary>
    public class Stores : StoresBase
    {
        /// <inheritdoc />
        public Stores(IBrowser browser, IGlobalLocators globalLocators) : base(browser, globalLocators) { }

        #region CSS Selectors
        public override string StoreZipCodeInputId { get; } = "searchZipInput";

        public override string AllStoresListClass => throw new NotImplementedException();
        public override string DirectionsButtonXpath => throw new NotImplementedException();
        public override string DivStoreSearchResultClass => throw new NotImplementedException();
        public override string MapsString => throw new NotImplementedException();
        public override string MyStoreBlockClass => throw new NotImplementedException(); 
        public override string StorePickerSubmitId => throw new NotImplementedException(); 
        public override string StoreDetailsLink => throw new NotImplementedException();
        public override string StoreDetailsBtnClass => throw new NotImplementedException();
        public override string NavWishlistClass => throw new NotImplementedException();
        public override string LpIconCallClass => throw new NotImplementedException();
        public override string LpIconDetailsClass => throw new NotImplementedException();
        public override string LpIconDirectionsClass => throw new NotImplementedException();
        public override string LpIconCalendarClass => throw new NotImplementedException();
        public override string LpIconCouponClass => throw new NotImplementedException(); 
        #endregion

        #region Page Elements
        public override IElement AddressLocalityField => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Span, ItemPropAttribute, "addressLocality");
        public override IElement AllStoresLampsPlus => Browser.Locate.ElementByClassName(AllStoresLampsPlusId);
        public override IElement BopusSubmenu => Browser.Locate.ElementById(BopusSubmenuId);
        public override IElement GetDirectionsButton => Browser.Locate.ElementById(GetDirectionsBtnId);
        public override IElement MakeThisMyStoreContainer => Browser.Locate.ElementByClassName(MakeThisMyStoreBlockClass);
        public override IElement MyStoreWrapper => Browser.Locate.ElementByClassName(MyStoreWrapperClass);
        public override IElement NearByZipStores => Browser.Locate.ElementByLinkText("Store details");
        public override IElement RandomStoreElement => ElementActions.SelectRandom(StoreResults);
        public override IElement SelectedStoreDetailsLink => SelectedStoreElement.FindElement(By.CssSelector(StoreDetailsSelectorLink));
        public override IElement StoreZipCodeInputElement => Browser.Locate.ElementById(StoreZipCodeInputId);
        public override IElement StorePickerSubmitElement => Browser.Locate.ElementByClassName(StoreZipCodeInputClass);
        public override IElement SelectedStoreDetailsName => SelectedStoreElement.FindElement(By.CssSelector(StoreDetailsSelectorName));
        public override IElement StoresDropDownMenu => Browser.Locate.ElementByClassName(HeaderDropDownsMenuClass);

        public override IElement LpIconCouponElement => throw new NotImplementedException();
        public override IElement LpIconCalendarElement => throw new NotImplementedException();
        public override IElement LpIconCallElement => throw new NotImplementedException();
        public override IElement LpIconDetailsButton => throw new NotImplementedException();
        public override IElement LpIconDirectionsElement => throw new NotImplementedException();
        public override IElement StorePhotosImgElement => throw new NotImplementedException();
        public override IElement RandomStoreNearMeElement => throw new NotImplementedException();
        public override IElement SelectStoreNearMeLinks => throw new NotImplementedException();

        public override ReadOnlyCollection<IElement> AllStoresLampsPlusLinks => Browser.Locate.ElementsByClassName(AllStoresLampsPlusLinkClass);
        public override ReadOnlyCollection<IElement> LampsPlusStoreRegionLinks => Browser.Locate.ElementsBySelector($"{HtmlTextWriterTag.H3} {HtmlTextWriterTag.A.ToFirstChildSelector()}", AllStoresLampsPlus);
        public override ReadOnlyCollection<IElement> StoreResults => Browser.Locate.ElementsBySelector(DivStoreResultClass);
        public override ReadOnlyCollection<IElement> StoreDetailsRegionLinks => Browser.Locate.ElementsBySelector(StoreDetailsSelectorLink);

        public override ReadOnlyCollection<IElement> StoreDetailBtns => throw new NotImplementedException();
        public override ReadOnlyCollection<IElement> StoreNearMeLinks => throw new NotImplementedException();

        #endregion

        /// <inheritdoc />
        public override string GetDetailBtnStoreResult(IElement storeResult)
        {
            return string.Empty;
        }

        /// <inheritdoc />
        public override string GetMakeThisMyStoreResult(IElement storeResult)
        {
            return string.Empty;
        }
    }
}
