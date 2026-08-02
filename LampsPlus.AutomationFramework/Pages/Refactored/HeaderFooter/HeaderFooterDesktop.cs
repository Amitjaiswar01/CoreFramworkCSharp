using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.UI;
using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Utilities;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Pages.Refactored.Modal;
using LampsPlus.AutomationFramework.Utilities;

namespace LampsPlus.AutomationFramework.Pages.Refactored.HeaderFooter
{
    public class HeaderFooterDesktop : IHeaderFooterDesktop
    {
        //Class members
        private string _hdrSignInId = "hdrSignIn";
        private string _cartCountId = "cartCount";
        private string _manageAccountString = "Manage Account";
        private string _lpHeaderProId = "lpHeader-pro";
        private string _lPLogoClass = "logoWrapper";
        private string _chandeliersId = "chandeliers";
        private string _allChandeliersString = "All Chandeliers";
        private string _diningLivingRoomString = "Dining - Living Room";
        private string _ceilingLightsID = "ceilingLighting";
        private string _flushmountString = "Flushmount";
        private string _lampsID = "lamps";
        private string _allTableLampsString = "All Table Lamps";
        private string _wallLightsID = "wallLights";
        private string _wishListClass = "zeroWishlist";
        private string _savedRoomsName = "hdr_rooms";
        private string _cartIconClass = "headerCart";
        private string _chandeliersNavId = "aChandeliers";
        private string _footerId = "footer";
        private string _userNameId = "userName";
        private string _savedHeaderMenuId = "savedHeaderMenu";
        private string _savedPortfolioYourWishlistLabelId = "savedPortfolio-yourWishlist--label";
        private string _proLogoId = "logo-pros";
        private string _openBoxId = "openBox";
        private string _orderHistory = "Order History";
        private string _recentlyViewedItems = "Recently Viewed";
        private string _portfolioContainerInspirationId = "portfolioContainer-inspiration";
        private string _lightingCatalogString = "Lighting Catalog";
        private string _savedPortfolioTotalSavedId = "savedPortfolio-totalSaved";
        private string _savedPortfolioYourRoomsId = "savedPortfolio-yourRooms";
        private string _saleMenuId = "saleMenu";
        private string _lpSaleNewSectionXpath = "//a[@data-saletype]";
        private string _ceilingLightingId = "ceilingLighting";
        private string _lampsId = "lamps";
        private string _wallLightsId = "wallLights";
        private string _footerCareersId = "footer_careers";
        private string _footerAboutUsId = "footer_about_us";
        private string _footerOrderStatusId = "footer_order_status";
        private string _footerReturnPolicyId = "footer_return_policy";
        private string _footerCatalogsId = "footer_catalogs";
        private string _footerPinterestNameAttribute = "footer_pinterest";
        private string _footerInstagramNameAttribute = "footer_instagram";
        private string _footerFacebookNameAttribute = "footer_facebook";
        private string _footerYoutubeNameAttribute = "footer_youtube";
        private string _footerSeeOurPolicyText = "See our policy.";
        private string _footerCaTransparencyId = "footer_ca_transparency";
        private string _footerStoreLocatorId = "footer_store_locator";
        private string _footerCustomerServiceId = "footer_customer_service";
        private string _footerManageAccountId = "footer_manage_account";
        private string _footerAdviceAndTipsId = "footer_advice_and_tips";
        private string _footerTwitterNameAttribute = "footer_twitter";
        private string _footerDoNotSellMyInfoText = "Do Not Sell My Info.";
        private string _lpFooterRateUsId = "footer_rate_us";
        private string _footerLpProsPhoneNumberClass = "//*[@class='callProMessageText']";
        private string _footerNationsLargestLightingText = "The Nation’s Largest Lighting Retailer";
        private string _footerTermsOfUseId = "footer_terms_of_use";
        private string _footerSiteMapId = "footer_site_map";
        private string _myLampsPlusText = "myLampsPlus";
        private string _emailFooterId = "ftrEmailUs";
        private string _aNavBtnClass = "aNavBtn";
        private string _navWrapperId = "lpHeader-navWrapper";
        private string _lPLogoXpath = "//*[@id='logo']";
        private string _wishListNotifCountClass = "notifCount";
        private string _cacSessionMenuId = "cacSessionMenu";
        private string _hdrStoresNameAttribute = "hdr_stores";
        private string _pnlLoggedOutId = "pnlLoggedOut";
        private string _signInButtonId = "signInButton";
        private string _portalLinksClass = "hdrPortal-portalLinks";
        private string _hdrStoresDropdownId = "hdrStoresDropdown";
        private string _widgetFloatingButtonCloseClass = "widget-floating__button--close";
        private string _confirmationDialogClass = "confirmation-dialog";
        private string _confirmationDialogButtonYesClass = "confirmation-dialog__button--yes";
        private string _lpFooterClass = "lpFooter";
        private string _footerProsId = "footer_pros";
        private string _footerHospitalityId = "footer_hospitality";
        private string _footerLightingDesigneServicesId = "footer_lighting_designe_services";
        private string _footerCustomerId = "footer_help";
        private string _footerContactId = "footer_contact";
        private string _footerGiftCardsId = "footer_giftcards";
        private string _footerNewHomeownerId = "footer_newhomeowner";
        private string _footerTiktokNameAttribute = "footer_tiktok";
        private string _footerAccessibilityId = "footer_accessibility";
        private string _footerPrivacyPolicyId = "footer_privacy_policy";
        private string _ccpaPolicyId = "footer_ccpa_policy";
        private string _footerShippingPolicyId = "footer_shipping_policy";
        private string _freeShippingFreeReturnsDisclaimerString = "Free Shipping, Free Returns valid only on standard shipping to 48 US contiguous states. $49 minimum for Free Shipping. Free Shipping not available on Lamps Plus Open Box items. Other exclusions apply.";
        private string _ftrShippingTextClass = "ftrShippingText";
        private string _ftrLinkListColHeaderClass = "ftrLinkList-col--header";
        private string _recentlyViewedLinkClass = "recentlyViewedLink";
        private string _accountDropDownClass = "accountDropDown";
        private string _userMenuClass = "userMenu";
        private string _hdrEmployeeToolsId = "hdrEmployeeTools";
        private string _hdrMyOrdersId = "hdrMyOrders";
        private string _hdrAccountManageId = "hdrAccountManage";
        private string _liveChatLinkId = "liveChatLink";
        private string _txtStoreNumberId = "txtStoreNumber";
        private string _plLPobClass = "plLPOB";
        private string _footerCharitablePartnershipsId = "footer_charitable_partnerships";
        private string _ftrSubscribeId = "ftrSubscribe";
        private string _txtEmailUpdatesRequestId = "txtEmailUpdatesRequest";
        private string _nightStandLampsString = "Nightstand Lamps";
        private string _aLampsId = "aLamps";
        private string _aHotelBrandProgramsId = "aHotelBrandPrograms";
        private string _anyHotelBestValueString = "Any Hotel - Best Value";
        private string _footerHospitalityFaqsId = "footer_hospitality_faqs";
        private string _footerContactUsId = "footer_contact_us";
        private string _footerWarrantyLinkText = "Warranty Information";
        private string _changePreferencesBtnId = "changePreferencesBtn";
        private string _bcTextClass = "bcText";
        private string _conversationLogScrollableClass = "conversation-log__scrollable";
        private string _userAccountId = "userAccount";
        private string _contactPhoneNameXpath = "//*[contains(@name,'hdr_contact')]";
        private string _logoId = "logo";
        private string _boldChatButtonContainerClass = "boldChatButtonContainer";
        private string _chandeliersNavBtnSelector = "#aChandeliers.aNavBtn";
        private string _chandeliersDiningLivingRoomString = "Dining - Living Room";
        private string _ceilingLightsFlushMountString = "Flushmount";
        private string _wallLightsWallLampsString = "All Wall Lamps";
        private string _livingRoomString = "Living Room";
        private string _roomInspirationString = "Room Inspiration";
        private string _bedroomString = "Bedroom";
        private string _kitchenString = "Kitchen";
        private string _ideasAndAdviceString = "Ideas & Advice";
        private string _buyingGuidesString = "Buying Guides";
        private string _styleAndTrendsString = "Style & Trends";
        private string _roomsString = "Rooms";
        private string _moreArticlesString = "More Articles";
        private string _ftrSubscribeBtnId = "ftrSubscribeBtn";
        private string _categoryDropDownsClass = "categoryDropDowns";
        private string _homepageSplashBannerSplashClass = "homepage-splash-banner--splash";

        protected string HdrSignOutId => "hdrSignOut";
        protected string AutoCsrRString => "AUTO-CSR-R";
        
        private IElement Footer => Browser.Locate.ElementById(_footerId);
        private IElement LpLogo => Browser.Locate.ElementBySelector(_lPLogoClass.ToCssClassSelector());
        private IElement ChandeliersCategoryElement => Browser.Locate.ElementById(_chandeliersId);
        private IElement DiningLivingRoomLink => Browser.Locate.ElementByLinkText(_diningLivingRoomString);
        private IElement CeilingLightsCategoryElement => Browser.Locate.ElementById(_ceilingLightsID);
        private IElement Flushmount => Browser.Locate.ElementByLinkText(_flushmountString);
        private IElement LampsCategoryElement => Browser.Locate.ElementById(_lampsID);
        private IElement WallLightsCategoryElement => Browser.Locate.ElementById(_wallLightsID);
        private IElement WallLampsLink => Browser.Locate.ElementByXpath("//ul[contains(@class,'categoryDropDowns')]/li/a[starts-with(text(),'Wall Lamps')]");
        private IElement WishListLink => Browser.Locate.ElementByClassName(_wishListClass);
        private IElement CartElement => Browser.Locate.ElementByClassName(_cartIconClass);
        private IElement CartButtonProductCount => Browser.Locate.ElementImmediately(_cartCountId.ToCssIdSelector());
        private IElement WishListHeaderLink => Browser.Locate.ElementById(_savedHeaderMenuId);
        private IElement SavedRoomsElement => Browser.Locate.ElementByName(_savedRoomsName);
        private IElement WishListMenuLink => Browser.Locate.ElementBySelector(_savedPortfolioYourWishlistLabelId.ToCssIdSelector());
        private IElement OpenBoxLink => Browser.Locate.ElementById(_openBoxId);
        private IElement HeaderUserOrderHistoryLink => Browser.Locate.ElementByLinkText(_orderHistory, LpHeader);
        private IElement HeaderManageAccountLink => Browser.Locate.ElementByLinkText(_manageAccountString);
        private IElement HeaderRecentlyViewedLinkForSignedInUsers => Browser.Locate.ElementByLinkText(_recentlyViewedItems);
        private IElement ContactPhoneLink => Browser.Locate.ElementByXpath(_contactPhoneNameXpath);
        private IElement InspirationMenu => Browser.Locate.ElementById(_portfolioContainerInspirationId);
        private IElement LightingCatalogLink => Browser.Locate.ElementByXpath($"//*[@id='hdrINSPIRATIONDropdown']//div[text()='{_lightingCatalogString}']");
        private IElement SavedMenu => Browser.Locate.ElementById(_savedPortfolioTotalSavedId);
        private IElement SavedRooms => Browser.Locate.ElementById(_savedPortfolioYourRoomsId);
        private IElement LpSaleSections(int index) => Browser.Locate.ElementsByXpath(_lpSaleNewSectionXpath)[index];
        private IElement FooterManageAccountLink => Browser.Locate.ElementById(_footerManageAccountId);
        private IElement FooterAdviceAndTipsLink => Browser.Locate.ElementById(_footerAdviceAndTipsId);
        private IElement LpFooterRateUs => Browser.Locate.ElementById(_lpFooterRateUsId);
        private IElement FooterLpProsPhoneNumber => Browser.Locate.ElementByXpath(_footerLpProsPhoneNumberClass);
        private IElement FooterHomeLink => Browser.Locate.ElementByLinkText(_footerNationsLargestLightingText);
        private IElement FooterMyLampsPlusLink => Browser.Locate.ElementByLinkText(_myLampsPlusText);
        private IElement WishListItemNotification => Browser.Locate.ElementByClassName(_wishListNotifCountClass);
        private IElement SessionMenu => Browser.Locate.ElementById(_cacSessionMenuId);
        private IElement StoresHeaderLink => Browser.Locate.ElementByAttributeEquals(HtmlTextWriterAttribute.Name, _hdrStoresNameAttribute);
        private IElement StoresLinkDropdown => Browser.Locate.ElementBySelector(_hdrStoresDropdownId.ToCssIdSelector());
        private IElement SignInLink => Browser.Locate.ElementById(_hdrSignInId);
        private IElement HeaderCreateAccountLink => Browser.Locate.ElementBySelector($"{_pnlLoggedOutId.ToCssIdSelector()} {HtmlTextWriterTag.Li.ToNthChildSelector(1)} {HtmlTextWriterTag.A}");
        private IElement PortalLinks => Browser.Locate.ElementByClassName(_portalLinksClass);
        private IElement LiveChatHeader => Browser.Locate.ElementByClassName(_boldChatButtonContainerClass);
        private IElement ChatWindow => Browser.Locate.ElementBySelector(_conversationLogScrollableClass.ToCssClassSelector());
        private IElement ChatCloseButton => Browser.Locate.ElementBySelector(_widgetFloatingButtonCloseClass.ToCssClassSelector());
        private IElement ChatCloseButtonConfirmation => Browser.Locate.ElementBySelector(_confirmationDialogButtonYesClass.ToCssClassSelector());
        private IElement FooterProsLink => Browser.Locate.ElementById(_footerProsId);
        private IElement FooterHospitalityLink => Browser.Locate.ElementById(_footerHospitalityId);
        private IElement FooterHelpLink => Browser.Locate.ElementById(_footerCustomerId);
        private IElement FooterLightingDesignServicesLink => Browser.Locate.ElementById(_footerLightingDesigneServicesId);
        private IElement FooterGiftCardLink => Browser.Locate.ElementById(_footerGiftCardsId);
        private IElement FooterContactUsLink => Browser.Locate.ElementById(_footerContactId);
        private IElement FooterNewHomeownerSavingsLink => Browser.Locate.ElementById(_footerNewHomeownerId);
        private IElement FooterTiktokLink => Browser.Locate.ElementByName(_footerTiktokNameAttribute);
        private IElement FooterAccessibilityLink => Browser.Locate.ElementById(_footerAccessibilityId);
        private IElement FooterCcpaPolicyLink => Browser.Locate.ElementById(_ccpaPolicyId);
        private IElement FooterPrivacyPolicyLink => Browser.Locate.ElementById(_footerPrivacyPolicyId);
        private IElement FooterShippingPolicyLink => Browser.Locate.ElementById(_footerShippingPolicyId);
        private IElement HeaderUserMenu => Browser.Locate.ElementByClassName(_userMenuClass);
        private IElement EmployeeToolsLink => Browser.Locate.ElementById(_hdrEmployeeToolsId);
        private IElement MyOrdersLink => Browser.Locate.ElementById(_hdrMyOrdersId);
        private IElement FooterFreeShippingFreeReturnsDisclaimer => Browser.Locate.ElementBySelector(_ftrShippingTextClass.ToCssClassSelector());
        private IElement StoreInSessionManageAccountPageLink => Browser.Locate.ElementById(_hdrAccountManageId);
        private IElement HeaderChatLink => Browser.Locate.ElementById(_liveChatLinkId);
        private IElement StoreNumberField => Browser.Locate.ElementById(_txtStoreNumberId);
        private IElement EmailSubscribeField => Browser.Locate.ElementById(_ftrSubscribeId);
        private IElement EmailSubscribeMessage => Browser.Locate.ElementById(_txtEmailUpdatesRequestId);
        private IElement ContactUsLink => Browser.Locate.ElementByTagName(HtmlTextWriterTag.A, HeaderChatLink);
        private IElement FooterCharitablePartnershipsLink => Browser.Locate.ElementBySelector(_footerCharitablePartnershipsId.ToCssIdSelector());
        private IElement ContactNumber => Browser.Locate.ElementBySelector(_liveChatLinkId.ToCssIdSelector());
        private IElement HospitalityPhoneNumber => Browser.Locate.ElementByTagName(HtmlTextWriterTag.A, ContactNumber);
        private IElement HospitalityNightstandLampsLink => Browser.Locate.ElementByLinkText(_nightStandLampsString);
        private IElement HospitalityLampsMenuLink => Browser.Locate.ElementById(_aLampsId);
        private IElement HotelBrandProgramsLink => Browser.Locate.ElementById(_aHotelBrandProgramsId);
        private IElement HospitalityHotelAndBrandBestValueLink => Browser.Locate.ElementByLinkText(_anyHotelBestValueString);
        private IElement FooterHospitalityFaqsLink => Browser.Locate.ElementById(_footerHospitalityFaqsId);
        private IElement FooterHospitalityContactUsLink => Browser.Locate.ElementById(_footerContactUsId);
        private IElement FooterWarrantyLink => Browser.Locate.ElementsByLinkText(_footerWarrantyLinkText)[0];
        private IElement ChandeliersNavMenu => Browser.Locate.ElementBySelector(_chandeliersNavBtnSelector);
        private IElement RoomInspirationLivingRoomLink => Browser.Locate.ElementByXpath($"//*[@id='hdrINSPIRATIONDropdown']//div[text()='{_livingRoomString}']");
        private IElement RoomInspirationLink => Browser.Locate.ElementByXpath($"//*[@id='hdrINSPIRATIONDropdown']//div[text()='{_roomInspirationString}']");
        private IElement RoomInspirationBedroomLink => Browser.Locate.ElementByXpath($"//*[@id='hdrINSPIRATIONDropdown']//div[text()='{_bedroomString}']");
        private IElement RoomInspirationKitchenLink => Browser.Locate.ElementByXpath($"//*[@id='hdrINSPIRATIONDropdown']//div[text()='{_kitchenString}']");
        private IElement InspirationIdeasAndAdviceLink => Browser.Locate.ElementByXpath($"//*[@id='hdrINSPIRATIONDropdown']//div[text()='{_ideasAndAdviceString}']");
        private IElement InspirationBuyingGuidesLink => Browser.Locate.ElementByXpath($"//*[@id='hdrINSPIRATIONDropdown']//a[text()='{_buyingGuidesString}']");
        private IElement InspirationStyleAndTrendsLink => Browser.Locate.ElementByXpath($"//*[@id='hdrINSPIRATIONDropdown']//a[text()='{_styleAndTrendsString}']");
        private IElement InspirationRoomsLink => Browser.Locate.ElementByXpath($"//*[@id='hdrINSPIRATIONDropdown']//a[text()='{_roomsString}']");
        private IElement InspirationMoreArticlesLink => Browser.Locate.ElementByXpath($"//*[@id='hdrINSPIRATIONDropdown']//a[text()='{_moreArticlesString}']");
        private bool IsLoggedInUser => (bool)Browser.ExecuteJs("return lp.globals.isLoggedIn");

        protected virtual IElement SignUpForCouponsOffersAndSaleAlertsField => Browser.Locate.ElementById(_txtEmailUpdatesRequestId);
        protected virtual IElement SignOutLink => Browser.Locate.ElementBySelector(HdrSignOutId.ToCssIdSelector());
        protected virtual IElement UserNameLink => Browser.Locate.ElementBySelector(_userNameId.ToCssIdSelector());
        protected virtual IElement ProLampsLogo => Browser.Locate.ElementById(_proLogoId);
        protected virtual IElement LampsLogo => Browser.Locate.ElementById(_logoId);
        protected virtual IElement SaleMenu => Browser.Locate.ElementById(_saleMenuId);
        protected virtual IElement ChandeliersDiningLivingRoomLink => Browser.Locate.ElementByLinkText(_chandeliersDiningLivingRoomString);
        protected virtual IElement CeilingLightsNavLink => Browser.Locate.ElementById(_ceilingLightingId);
        protected virtual IElement CeilingLightsFlushMountLink => Browser.Locate.ElementByLinkText(_ceilingLightsFlushMountString);
        protected virtual IElement TableAndFloorLampsNavLink => Browser.Locate.ElementById(_lampsId);
        protected virtual IElement WallLightsNavLink => Browser.Locate.ElementById(_wallLightsId);
        protected virtual IElement WallLightsWallLampsLink => Browser.Locate.ElementByLinkText(_wallLightsWallLampsString);
        protected virtual IElement ChandeliersNavLink => Browser.Locate.ElementById(_chandeliersNavId);
        protected virtual IElement AllChandeliersLink => Browser.Locate.ElementByLinkText(_allChandeliersString);
        protected virtual IElement AllTableLampsLink => Browser.Locate.ElementByLinkText(_allTableLampsString);
        protected virtual IElement FooterCareersLink => Browser.Locate.ElementById(_footerCareersId);
        protected virtual IElement FooterAboutUsLink => Browser.Locate.ElementById(_footerAboutUsId);
        protected virtual IElement FooterOrderStatusLink => Browser.Locate.ElementById(_footerOrderStatusId);
        protected virtual IElement FooterReturnPolicyLink => Browser.Locate.ElementById(_footerReturnPolicyId);
        protected virtual IElement FooterCatalogsLink => Browser.Locate.ElementById(_footerCatalogsId);
        protected virtual IElement FooterPinterestLink => Browser.Locate.ElementByName(_footerPinterestNameAttribute);
        protected virtual IElement FooterInstagramLink => Browser.Locate.ElementByName(_footerInstagramNameAttribute);
        protected virtual IElement FooterFacebookLink => Browser.Locate.ElementByName(_footerFacebookNameAttribute);
        protected virtual IElement FooterYoutubeLink => Browser.Locate.ElementByName(_footerYoutubeNameAttribute);
        protected virtual IElement FooterSeeOurPolicyLink => Browser.Locate.ElementByLinkText(_footerSeeOurPolicyText);
        protected virtual IElement FooterCaTransparencyActLink => Browser.Locate.ElementById(_footerCaTransparencyId);
        protected virtual IElement FooterStoreLocatorLink => Browser.Locate.ElementById(_footerStoreLocatorId);
        protected virtual IElement FooterCustomerServiceLink => Browser.Locate.ElementById(_footerCustomerServiceId);
        protected virtual IElement FooterTwitterLink => Browser.Locate.ElementByName(_footerTwitterNameAttribute);
        protected virtual IElement FooterDoNotSellMyInfoLink => Browser.Locate.ElementByLinkText(_footerDoNotSellMyInfoText);
        protected virtual IElement FooterTermsOfUseLink => Browser.Locate.ElementById(_footerTermsOfUseId);
        protected virtual IElement FooterSiteMapLink => Browser.Locate.ElementById(_footerSiteMapId);
        protected virtual IElement FooterEmailIcon => Browser.Locate.ElementById(_emailFooterId);
        protected virtual IElement FooterChatLink => Browser.Locate.ElementByClassName(_lpFooterClass).FindElement(By.ClassName(_bcTextClass));
        protected virtual IElement SignUpForCouponsOffersAndSaleAlertsSubscribeButton => Browser.Locate.ElementById(_ftrSubscribeBtnId);
        protected virtual IElement SignUpForCouponsOffersAndSaleAlertsLabel => Browser.Locate.ElementByClassName(_ftrLinkListColHeaderClass);
        protected virtual IElement HeaderSignInButton => Browser.Locate.ElementById(_signInButtonId);
        protected virtual IElement SignUpForEmailUpdatesSubmitButton => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Button, HtmlTextWriterAttribute.Id, _ftrSubscribeBtnId);

        protected virtual ReadOnlyCollection<IElement> NavElements => Browser.Locate.ElementsByClassName(_aNavBtnClass, Browser.Locate.ElementById(_navWrapperId));

        protected string GetElementLink(IElement element)
        {
            Assert.Displayed(element, $"{element} Element not displayed");
            if (element.TagName.ToLower() != "a")
            {
                var anchorLinkRelative = Browser.Locate.ElementByJavascript(element, "return arguments[0].querySelector('a') || arguments[0].closest('a')");
                element = anchorLinkRelative;
            }

            return Urls.NormalizeUrl(element.GetAttribute("href"));
        }

        //Instances 
        protected IBrowser Browser;
        protected IAssert Assert;
        private IModalDesktop _modal;

        public HeaderFooterDesktop(IBrowser browser, IAssert assert, IModalDesktop modal)
        {
            Browser = browser;
            Assert = assert;
            _modal = modal;
        }

        //Interface implementation
        public IElement LpHeader => Browser.Locate.ElementById(_lpHeaderProId);
        public string PageTitle { get; }
        public string PageUrl { get; }
        public string FootLpProsPhoneNumber => "Text: 3102427537";
        public string DefaultProsNumber => "Text: 818-970-4798";
        public bool IsCurrentPage { get; }
        public int CartItemCount => CartButtonProductCount.Text == string.Empty ? 0 : int.Parse(CartButtonProductCount.Text);

        public virtual bool IsSignInLinkVisible => Browser.Locate.ElementBySelector(_hdrSignInId.ToCssIdSelector()).IsInitialized;

        public virtual void NavigateToManageAccount()
        {
            Browser.Locate.ElementByLinkText(_manageAccountString).Click();
        }

        public virtual void SignOut()
        {
            if (!IsLoggedInUser) return;
            Browser.Wait.IsVisibleElement(By.CssSelector(_userNameId.ToCssIdSelector()));
            Browser.MouseOverOnElement(UserNameLink);
            Browser.Wait.IsVisibleElement(By.CssSelector(HdrSignOutId.ToCssIdSelector()), 30);
            SignOutLink.Click();
            Browser.Wait.ForDomReady();
        }

        public void SignUpForCouponsOffersAndSaleAlerts(Account account)
        {
            Browser.Wait.ForDomReady();
            SignUpForCouponsOffersAndSaleAlertsField.Click();
            SignUpForCouponsOffersAndSaleAlertsField.SendKeys(account.EmailAddress);
            SignUpForCouponsOffersAndSaleAlertsField.SendKeys(Keys.Tab);
            SignUpForCouponsOffersAndSaleAlertsField.SendKeys(Keys.Enter);

            Browser.Wait.IsVisibleElement(By.CssSelector(_changePreferencesBtnId.ToCssIdSelector()));
        }

        public void LoadLightingCatalog()
        {
            FooterCatalogsLink.Click();
        }

        public string GetLpLogoLink()
        {
            Browser.Wait.IsVisibleElement(By.XPath(_lPLogoXpath));
            Browser.MouseOverOnElement(LpLogo);
            var elementAssigned = LpLogo.GetAttribute("href");
            return elementAssigned;
        }

        public string GetAllChandeliersLink()
        {
            Browser.Wait.ForDisplayedElement(ChandeliersCategoryElement);
            Browser.MouseOverOnElement(ChandeliersCategoryElement);
            Browser.MouseOverOnElement(AllChandeliersLink);
            var elementAssigned = AllChandeliersLink.GetAttribute("href");
            return elementAssigned;
        }

        public string GetDiningLivingLink()
        {
            Browser.MouseOverOnElement(DiningLivingRoomLink);
            var elementAssigned = DiningLivingRoomLink.GetAttribute("href");
            return elementAssigned;
        }

        public int GetNumberOfWishListItems()
        {
            Browser.MouseOverOnElement(WishListHeaderLink);
            string itemsWishlist = WishListItemNotification.Text;
            int itemsInWishlist = Convert.ToInt32(itemsWishlist);
            return itemsInWishlist;
        }

        public string GetFlushmountLink()
        {
            Browser.MouseOverOnElement(CeilingLightsCategoryElement);
            Browser.MouseOverOnElement(Flushmount);
            var elementAssigned = Flushmount.GetAttribute("href");
            return elementAssigned;
        }

        public string GetAllTableLampsLink()
        {
            Browser.MouseOverOnElement(LampsCategoryElement);
            Browser.MouseOverOnElement(AllTableLampsLink);
            var elementAssigned = AllTableLampsLink.GetAttribute("href");
            return elementAssigned;
        }

        public string GetWallLampsLink()
        {
            Browser.MouseOverOnElement(WallLightsCategoryElement);
            Browser.Wait.ForDisplayedElement(WallLampsLink);
            Browser.MouseOverOnElement(WallLampsLink);
            var elementAssigned = WallLampsLink.GetAttribute("href");
            return elementAssigned;
        }

        public string GetWishListLink()
        {
            Browser.MouseOverOnElement(WishListHeaderLink);
            Browser.MouseOverOnElement(WishListLink);
            var elementAssigned = WishListLink.GetAttribute("href");
            return elementAssigned;
        }

        public string GetSavedRoomLink()
        {
            Browser.MouseOverOnElement(SavedRoomsElement);
            var elementAssigned = SavedRoomsElement.GetAttribute("href");
            return elementAssigned;
        }

        public string GetFooterLpProsPhoneNumber()
        {
            return FooterLpProsPhoneNumber.Text;
        }

        public virtual string GetCartIconLink()
        {
            Browser.MouseOverOnElement(CartElement);
            var elementAssigned = CartElement.GetAttribute("href");
            return elementAssigned;
        }

        public void ScrollToFooter()
        {
            Browser.Wait.ForDomReady();
            Browser.ScrollIntoView(SignUpForEmailUpdatesSubmitButton);
            Browser.Wait.ForElementToStopAnimating(Footer);
        }

        public void HoverOverChandelierStickyNavigation()
        {
            Browser.MouseOverOnElement(ChandeliersNavLink);
        }

        public void NavigateToWishListThroughHeaderLink()
        {
            Browser.MouseOverOnElement(WishListHeaderLink);
            Browser.ClickByJs(WishListMenuLink);
        }

        public string GetProLampsLogoLink()
        {
            Assert.Displayed(ProLampsLogo, "Lamps Plus Pro Logo Element not displayed");
            return GetElementLink(ProLampsLogo);
        }

        public string GetLampsPlusLogoLink()
        {
            Assert.Displayed(LampsLogo, "Lamps Plus Logo Element not displayed");
            return GetElementLink(LampsLogo);
        }

        public string GetProContactUsPhoneNumber()
        {
            return "800-304-8120";
        }

        public string GetLampsPlusContactUsPhoneNumber()
        {
            return "800-782-1967";
        }

        public string GetStoreInSessionPhoneNumberLink()
        {
            return ContactUsLink.GetAttribute("href");
        }

        public Dictionary<string, string> GetProsFooterLegalLinks()
        {
            var dict = new Dictionary<string, string>
            {
                // Legal links
                {"HomePageUrl", GetElementLink(FooterHomeLink)},
                {"TermsOfUsePageUrl",GetElementLink(FooterTermsOfUseLink)},
                {"SiteMapPageUrl", GetElementLink(FooterSiteMapLink)},
                {"DoNotSellMyInfoUrl", GetElementLink(FooterDoNotSellMyInfoLink)}
            };
            
            return dict;
        }


        public virtual Dictionary<string, string> GetFooterLinks()
        {
            var dict = new Dictionary<string, string>
            {
                // Our Company
                {"StoresPageUrl", GetElementLink(FooterStoreLocatorLink)},
                {"CareersPageUrl",GetElementLink(FooterCareersLink)},
                // Help Center
                {"ProfessionalsInfoPolicyUrl", GetElementLink(FooterCustomerServiceLink)},
                {"ManageAccountPageUrl",GetElementLink(FooterManageAccountLink)},
                // Resources
                {"IdeasAdviceUrlProd", GetElementLink(FooterAdviceAndTipsLink)},
                // Social Media link
                {"LpTwitterUrl",GetElementLink(FooterTwitterLink)},
                // Legal link
                {"DoNotSellMyInfoUrl", GetElementLink(FooterDoNotSellMyInfoLink)},
            };

            return dict;
        }

        public virtual Dictionary<string, string> GetCommonFooterLegalLinks()
        {
            var dict = new Dictionary<string, string>
            {
                {"SeeOurPolicyUrl", GetElementLink(FooterSeeOurPolicyLink)},
                {"CaDisclosureTransparencyPageUrl",GetElementLink(FooterCaTransparencyActLink)},
            };

            return dict;
        }

        public Dictionary<string, string> GetFooterProUserSocialLinks()
        {
            var dict = new Dictionary<string, string>
            {
                {"LpPinterestUrl", GetElementLink(FooterPinterestLink)},
                {"LpInstagramUrl",GetElementLink(FooterInstagramLink)},
                {"LpFacebookUrl",GetElementLink(FooterFacebookLink)},
                {"LpYouTubeUrl",GetElementLink(FooterYoutubeLink)},
            };

            return dict;
        }

        public Dictionary<string, string> GetFooterProsUserSocialLinks()
        {
            var dict = new Dictionary<string, string>
            {
                {"MyLampsPlusPageUrl", GetElementLink(FooterMyLampsPlusLink)},
                {"LpPinterestUrl", GetElementLink(FooterPinterestLink)},
                {"LpInstagramUrl",GetElementLink(FooterInstagramLink)},
                {"LpFacebookUrl",GetElementLink(FooterFacebookLink)},
                {"LpTwitterUrl", GetElementLink(FooterTwitterLink)},
                {"LpYouTubeUrl",GetElementLink(FooterYoutubeLink)},
            };

            return dict;
        }

        public Dictionary<string, string> GetFooterProsHelpCenterLinks()
        {
            var dict = new Dictionary<string, string>
            {
                {"ProfessionalsInfoPolicyUrl", GetElementLink(FooterCustomerServiceLink)},
                {"OrderHistoryPageUrl",GetElementLink(FooterOrderStatusLink)},
                {"ReturnsPolicyPageUrl",GetElementLink(FooterReturnPolicyLink)}
            };

            return dict;
        }

        public virtual Dictionary<string, string> GetFooterSocialLinks()
        {
            var dict = new Dictionary<string, string>
            {
                {"LpPinterestUrl", GetElementLink(FooterPinterestLink)},
                {"LpInstagramUrl",GetElementLink(FooterInstagramLink)},
                {"LpFacebookUrl",GetElementLink(FooterFacebookLink)},
                {"LpYouTubeUrl",GetElementLink(FooterYoutubeLink)},
                {"LpTwitterUrl", GetElementLink(FooterTwitterLink)},
                {"LpTikTokUrl", GetElementLink(FooterTiktokLink)}
            };

            return dict;
        }

        public Dictionary<string, string> GetFooterLegalLinks()
        {
            var dict = new Dictionary<string, string>
            {
                { "FooterTermsOfUseUrl", GetElementLink(FooterTermsOfUseLink) },
                { "FooterAccessibilityUrl", GetElementLink(FooterAccessibilityLink) },
                { "FooterPrivacyPolicyUrl", GetElementLink(FooterPrivacyPolicyLink) },
                { "FooterSiteMapUrl", GetElementLink(FooterSiteMapLink) },
                { "FooterCCPAPolicyUrl", GetElementLink(FooterCcpaPolicyLink)},
                { "FooterShippingPolicyUrl", GetElementLink(FooterShippingPolicyLink)}
            };

            return dict;
        }
        
        public Dictionary<string, string> GetFooterHospitalityLegalLinks()
        {
            var dict = new Dictionary<string, string>
            {
                { "FooterTermsOfUseUrl", GetElementLink(FooterTermsOfUseLink) },
                { "FooterSiteMapUrl", GetElementLink(FooterSiteMapLink) },
                { "FooterCCPAPolicyUrl", GetElementLink(FooterCaTransparencyActLink)}
            };

            return dict;
        }

        public virtual Dictionary<string, string> GetCommonFooterNavLinksLinks()
        {
            var dict = new Dictionary<string, string>
            {
                {"CareersPageUrl", GetElementLink(FooterCareersLink)},
                {"AboutUsPageUrl",GetElementLink(FooterAboutUsLink)},
                {"OrderHistoryPageUrl",GetElementLink(FooterOrderStatusLink)},
                {"ReturnsPolicyPageUrl",GetElementLink(FooterReturnPolicyLink)},
                {"CatalogsPageUrl",GetElementLink(FooterCatalogsLink)},
            };

            return dict;
        }

        public virtual Dictionary<string, string> GetHeaderElementsLinks()
        {
            var dict = new Dictionary<string, string>
            {
                {"LampsPlusOpenBoxUrl", GetElementLink(OpenBoxLink)},
                {"ContactUsPageUrl",GetElementLink(ContactPhoneLink)},
            };

            return dict;
        }

        public Dictionary<string, string> GetProAccountHeaderElementsLinks()
        {
            var dict = new Dictionary<string, string>
            {
                {"OrderHistoryPageUrl", GetElementLink(HeaderUserOrderHistoryLink)},
                {"ManageAccountPageUrl",GetElementLink(HeaderManageAccountLink)},
                {"RecentlyViewedUrl",GetElementLink(HeaderRecentlyViewedLinkForSignedInUsers)},
                {"SignOutPageUrl",GetElementLink(SignOutLink)},
            };

            return dict;
        }
        
        public Dictionary<string, string> GetHospitalityAccountHeaderElementsLinks()
        {
            var dict = new Dictionary<string, string>
            {
                {"ManageAccountPageUrl",GetElementLink(HeaderManageAccountLink)},
                {"RecentlyViewedUrl",GetElementLink(HeaderRecentlyViewedLinkForSignedInUsers)},
                {"SignOutPageUrl",GetElementLink(SignOutLink)},
            };

            return dict;
        }

        public Dictionary<string, string> GetHospitalityOurCompanyElementsLinks()
        {
            var dict = new Dictionary<string, string>
            {
                { "FooterAboutLampsPlusUrl", GetElementLink(FooterAboutUsLink)},
                { "FooterContactUsUrl", GetElementLink(FooterHospitalityContactUsLink)},
                { "FooterHospitalityFaqsUrl", GetElementLink(FooterHospitalityFaqsLink)}
            };

            return dict;
        }

        public Dictionary<string, string> GetHospitalityHelpCenterElementsLinks()
        {
            var dict = new Dictionary<string, string>
            {
                { "FooterPrivacyPolicyUrl", GetElementLink(FooterPrivacyPolicyLink) },
                { "FooterAccessibilityUrl", GetElementLink(FooterAccessibilityLink) },
                { "FooterTermsOfUseUrl", GetElementLink(FooterTermsOfUseLink) }
            };

            return dict;
        }

        public Dictionary<string, string> GetHospitalityResourcesElementsLinks()
        {
            var dict = new Dictionary<string, string>
            {
                { "FooterManageAccountUrl", GetElementLink(FooterManageAccountLink) },
                { "FooterWarrantyInformationUrl", GetElementLink(FooterWarrantyLink) }
            };

            return dict;
        }

        public Dictionary<string, string> GetAccountHeaderElementsLinks()
        {
            var dict = new Dictionary<string, string>
            {
                {"OrderHistoryPageUrl", GetElementLink(HeaderUserOrderHistoryLink)},
                {"CreateAccountPageUrl",GetElementLink(HeaderCreateAccountLink)},
                {"RecentlyViewedUrl",GetElementLink(HeaderRecentlyViewedLinkForSignedInUsers)}
            };

            return dict;
        }

        public Dictionary<string, string> GetAccountHeaderElementsForStoreInSessionLinks()
        {
            var dict = new Dictionary<string, string>
            {
                { "EmployeeToolsUrl", GetElementLink(EmployeeToolsLink) },
                { "MyOrders", GetElementLink(MyOrdersLink) },
                { "ManageAccountUrl", GetElementLink(StoreInSessionManageAccountPageLink) },
                { "SignOutUrl", GetElementLink(SignOutLink) }
            };

            return dict;
        }

        public Dictionary<string, string> GetStoreInSessionAccountHeaderLink()
        {
            var dict = new Dictionary<string, string>
            {
                { "HeaderAccountUrl", GetElementLink(HeaderCreateAccountLink) }
            };

            return dict;
        }

        public Dictionary<string, string> GetInspirationHeaderElementsLinks()
        {
            var dict = new Dictionary<string, string>
            {
                { "RoomInspirationUrl", GetElementLink(RoomInspirationLink)},
                { "RoomInspirationLivingRoomPageUrl", GetElementLink(RoomInspirationLivingRoomLink)},
                { "RoomInspirationBedroomPageUrl", GetElementLink(RoomInspirationBedroomLink)},
                { "RoomInspirationKitchenPageUrl", GetElementLink(RoomInspirationKitchenLink)},
                { "RoomInspirationAllRoomsPageUrl", GetElementLink(RoomInspirationLink)},
                { "LightingCatalogUrl", GetElementLink(LightingCatalogLink)},
                { "IdeasAdviceUrlProd", GetElementLink(InspirationIdeasAndAdviceLink)},
                { "BuyingGuidesUrlProd", GetElementLink(InspirationBuyingGuidesLink)},
                { "StyleAndTrendsUrlProd", GetElementLink(InspirationStyleAndTrendsLink)},
                { "RoomsUrlProd", GetElementLink(InspirationRoomsLink)},
                { "MoreArticlesUrlProd", GetElementLink(InspirationMoreArticlesLink)}
            };

            return dict;
        }

        public Dictionary<string, string> GetSavedHeaderElementsLinks()
        {
            var dict = new Dictionary<string, string>
            {
                {"RoomsPageUrl", GetElementLink(SavedRooms)},
                {"WishListPageUrl", GetElementLink(WishListLink)},
            };

            return dict;
        }

        public Dictionary<string, string> GetSaleHeaderElementLinksForPros()
        {
            var dict = new Dictionary<string, string>
            {
                //Verify All Sale & Banner links
                {"OnSaleUrl", GetElementLink(LpSaleSections(0))},
                {"OnSaleUrl2", GetElementLink(LpSaleSections(1))},

                //Verify Row1 Elements Chandeliers, Ceiling Lights & Outdoor Lighting links
                {"ChandeliersOnSaleUrl", GetElementLink(LpSaleSections(2))},
                {"CeilingLightsOnSaleUrl", GetElementLink(LpSaleSections(3))},
                {"OutdoorLightinsOnSaleUrl", GetElementLink(LpSaleSections(4))},

                //Verify Row2 Elements Table Lamps, Bathroom Lighting & Furniture links
                {"TableLampssOnSaleUrl", GetElementLink(LpSaleSections(5))},
                {"BathroomLightingOnSaleUrl", GetElementLink(LpSaleSections(6))},
                {"FurnituresOnSaleUrl", GetElementLink(LpSaleSections(7))},

                //Verify Row3 Elements Floor Lamps, Ceiling Fans & Mirror links
                {"FloorLampssOnSaleUrl", GetElementLink(LpSaleSections(8))},
                {"CeilingFanOnSaleUrl", GetElementLink(LpSaleSections(9))},
                {"MirrosOnSaleUrl", GetElementLink(LpSaleSections(10))},

                //Verify Row4 Elements Pros Special. Daily Sale, Clearance & Open Box links
                {"ProsSpecialPageUrl", GetElementLink(LpSaleSections(11))},
                {"LpDailySalesUrl", GetElementLink(LpSaleSections(12))},
                {"ClearanceViewPageUrl", GetElementLink(LpSaleSections(13))},
                {"LampsPlusOpenBoxLinkFromSaleMenuUrl", GetElementLink(LpSaleSections(14))}
            };

            return dict;
        }

        public Dictionary<string, string> GetSaleHeaderElementLinks()
        {
            var dict = new Dictionary<string, string>
            {
                //Verify All Sale & Banner links
                {"OnSaleUrl", GetElementLink(LpSaleSections(0))},
                {"OnSaleUrl2", GetElementLink(LpSaleSections(1))},

                //Verify Row1 Elements Chandeliers, Ceiling Lights & Outdoor Lighting links
                {"ChandeliersOnSaleUrl", GetElementLink(LpSaleSections(2))},
                {"CeilingLightsOnSaleUrl", GetElementLink(LpSaleSections(3))},
                {"OutdoorLightinsOnSaleUrl", GetElementLink(LpSaleSections(4))},

                //Verify Row2 Elements Table Lamps, Bathroom Lighting & Furniture links
                {"TableLampssOnSaleUrl", GetElementLink(LpSaleSections(5))},
                {"BathroomLightingOnSaleUrl", GetElementLink(LpSaleSections(6))},
                {"FurnituresOnSaleUrl", GetElementLink(LpSaleSections(7))},

                //Verify Row3 Elements Floor Lamps, Ceiling Fans & Mirror links
                {"FloorLampssOnSaleUrl", GetElementLink(LpSaleSections(8))},
                {"CeilingFanOnSaleUrl", GetElementLink(LpSaleSections(9))},
                {"MirrosOnSaleUrl", GetElementLink(LpSaleSections(10))},

                //Verify Row4 Elements Pros Special. Daily Sale, Clearance & Open Box links
                {"LpDailySalesUrl", GetElementLink(LpSaleSections(11))},
                {"ClearanceViewPageUrl", GetElementLink(LpSaleSections(12))},
                {"LampsPlusOpenBoxLinkFromSaleMenuUrl", GetElementLink(LpSaleSections(13))}
            };

            return dict;
        }

        public Dictionary<string, string> GetSaleHeaderElementLinksForStoreInSession()
        {
            var dict = new Dictionary<string, string>
            {
                //Verify All Sale & Banner links
                {"OnSaleUrl", GetElementLink(LpSaleSections(0))},
                {"OnSaleUrl2", GetElementLink(LpSaleSections(1))},

                //Verify Row1 Elements Chandeliers, Ceiling Lights & Outdoor Lighting links
                {"ChandeliersOnSaleUrl", GetElementLink(LpSaleSections(2))},
                {"CeilingLightsOnSaleUrl", GetElementLink(LpSaleSections(3))},
                {"OutdoorLightinsOnSaleUrl", GetElementLink(LpSaleSections(4))},

                //Verify Row2 Elements Table Lamps, Bathroom Lighting & Furniture links
                {"TableLampssOnSaleUrl", GetElementLink(LpSaleSections(5))},
                {"BathroomLightingOnSaleUrl", GetElementLink(LpSaleSections(6))},
                {"FurnituresOnSaleUrl", GetElementLink(LpSaleSections(7))},

                //Verify Row3 Elements Floor Lamps, Ceiling Fans & Mirror links
                {"FloorLampssOnSaleUrl", GetElementLink(LpSaleSections(8))},
                {"CeilingFanOnSaleUrl", GetElementLink(LpSaleSections(9))},
                {"MirrosOnSaleUrl", GetElementLink(LpSaleSections(10))},

                //Verify Row4 Elements Pros Special. Daily Sale, Clearance & Open Box links
                {"LpDailySalesUrl", GetElementLink(LpSaleSections(11))},
                {"ClearanceViewPageUrl", GetElementLink(LpSaleSections(12))},
            };

            return dict;
        }

        public Dictionary<string, string> GetChandelierMenuLinks()
        {
            var dict = new Dictionary<string, string>
            {
                {"AllChandeliersUrl", GetElementLink(AllChandeliersLink)},
                {"ChandeliersDiningLivingRoomUrl", GetElementLink(ChandeliersDiningLivingRoomLink)},
            };

            return dict;
        }

        public Dictionary<string, string> GetCeilingLightsMenuLink()
        {
            var dict = new Dictionary<string, string>
            {
                {"CeilingLightsFlushMountUrl", GetElementLink(CeilingLightsFlushMountLink)}
            };

            return dict;
        }

        public Dictionary<string, string> GetLampsMenuLink()
        {
            var dict = new Dictionary<string, string>
            {
                {"TableLampsUrl", GetElementLink(AllTableLampsLink)}
            };

            return dict;
        }

        public Dictionary<string, string> GetWallLightsMenuLink()
        {
            var dict = new Dictionary<string, string>
            {
                {"WallLightsUrl", GetElementLink(WallLightsWallLampsLink)}
            };

            return dict;
        }

        public Dictionary<string, string> GetFooterEmailIconLink()
        {
            var dict = new Dictionary<string, string>
            {
                {"FooterEmailIconUrl", GetElementLink(FooterEmailIcon)}
            };

            return dict;
        }

        public Dictionary<string, string> GetFooterB2BProgramsLinks()
        {
            var dict = new Dictionary<string, string>
            {
                {"FooterProsUrl", GetElementLink(FooterProsLink)},
                {"FooterHospitalityUrl", GetElementLink(FooterHospitalityLink)}
            };

            return dict;
        }

        public Dictionary<string, string> GetFooterResourcesLinks()
        {
            var dict = new Dictionary<string, string>
            {
                { "FooterIdeasAndAdviceUrl", GetElementLink(FooterAdviceAndTipsLink) },
                { "FooterCatalogsUrl", GetElementLink(FooterCatalogsLink) },
                { "FooterGiftCardsUrl", GetElementLink(FooterGiftCardLink) },
                { "FooterManageAccountUrl", GetElementLink(FooterManageAccountLink) },
                { "FooterNewHomeownerCouponUrl", GetElementLink(FooterNewHomeownerSavingsLink) }
            };

            return dict;
        }

        public Dictionary<string, string> GetFooterProsResourcesLinks()
        {
            var dict = new Dictionary<string, string>
            {
                { "FooterIdeasAndAdviceUrl", GetElementLink(FooterAdviceAndTipsLink) },
                { "FooterCatalogsUrl", GetElementLink(FooterCatalogsLink) },
                { "FooterGiftCardsUrl", GetElementLink(FooterGiftCardLink) },
                { "FooterManageAccountUrl", GetElementLink(FooterManageAccountLink) }
            };

            return dict;
        }

        public Dictionary<string, string> GetFooterCustomerServiceLinks()
        {
            var dict = new Dictionary<string, string>
            {
                { "FooterHelpUrl", GetElementLink(FooterHelpLink) },
                { "FooterContactUsUrl", GetElementLink(FooterContactUsLink) },
                { "FooterOrderStatusUrl", GetElementLink(FooterOrderStatusLink) },
                { "FooterReturnPolicyUrl", GetElementLink(FooterReturnPolicyLink) }
            };

            return dict;
        }

        public Dictionary<string, string> GetFooterAboutUsLinks()
        {
            var dict = new Dictionary<string, string>
            {
                {"FooterAboutLampsPlusUrl", GetElementLink(FooterAboutUsLink)},
                {"FooterStoreLocatorUrl", GetElementLink(FooterStoreLocatorLink)},
                {"FooterCareersUrl", GetElementLink(FooterCareersLink)},
                {"FooterLightingDesignServicesUrl", GetElementLink(FooterLightingDesignServicesLink)},
                {"FooterCharitablePartnerships",GetElementLink(FooterCharitablePartnershipsLink) }
            };

            return dict;
        }

        public Dictionary<string, string> GetProsFooterAboutUsLinks()
        {
            var dict = new Dictionary<string, string>
            {
                {"FooterAboutLampsPlusUrl", GetElementLink(FooterAboutUsLink)},
                {"FooterStoreLocatorUrl", GetElementLink(FooterStoreLocatorLink)},
                {"FooterCareersUrl", GetElementLink(FooterCareersLink)},
                {"FooterCharitablePartnerships",GetElementLink(FooterCharitablePartnershipsLink) }
            };

            return dict;
        }

        public Dictionary<string, string> GetFooterStoreInSessionAboutUsLinks()
        {
            var dict = new Dictionary<string, string>
            {
                {"FooterAboutLampsPlusUrl", GetElementLink(FooterAboutUsLink)},
                {"FooterStoreLocatorUrl", GetElementLink(FooterStoreLocatorLink)},
                {"FooterCareersUrl", GetElementLink(FooterCareersLink)},
                {"FooterLightingDesignServicesUrl", GetElementLink(FooterLightingDesignServicesLink)},
                {"FooterCharitablePartnerships", GetElementLink(FooterCharitablePartnershipsLink)}
            };

            return dict;
        }

        public void OpenStoresMenu()
        {
            Browser.Wait.ForDisplayedElement(Browser.MouseOverOnElement(StoresHeaderLink));
        }

        public void OpenSessionMenu()
        {
            Browser.Wait.ForDisplayedElement(Browser.MouseOverOnElement(SessionMenu));
        }

        public bool IsSessionMenuVisible()
        {
            return SessionMenu.Displayed;
        }

        public void OpenInspirationMenu()
        {
            Browser.MouseOverOnElement(InspirationMenu);
            Browser.Wait.IsVisibleElement(By.CssSelector($"{_portfolioContainerInspirationId.ToCssIdSelector()} > {HtmlTextWriterTag.Div}"));
        }

        public virtual void OpenMyAccountMenu()
        {
            Browser.MouseOverOnElement(UserNameLink);
            Browser.Wait.IsVisibleElement(By.CssSelector($"{_userAccountId.ToCssIdSelector()} > {HtmlTextWriterTag.Div}"));
        }

        public void OpenSavedMenu()
        {
            Browser.Wait.ForDisplayedElement(Browser.MouseOverOnElement(SavedMenu));
        }

        public void OpenSaleMenu()
        {
            Browser.MouseOverOnElement(SaleMenu);
            Browser.Wait.IsVisibleElement(By.CssSelector($"{_saleMenuId.ToCssIdSelector()} > {HtmlTextWriterTag.Div}"));
            Browser.Wait.IsVisibleElement(By.XPath(_lpSaleNewSectionXpath));
        }

        public void OpenSignInMenu()
        {
            Browser.Wait.ForDisplayedElement(Browser.MouseOverOnElement(SignInLink));
        }

        public string GetContactPhoneLink()
        {
            return ContactPhoneLink.Text;
        }

        public string GetHospitalityContactPhoneLink()
        {
            return HospitalityPhoneNumber.GetAttribute("href");
        }

        public bool IsRateUsModalOpened()
        {
            LpFooterRateUs.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(_modal.LpModalId.ToCssIdSelector()));

            var modal = _modal.GetIframe();

            return modal.Displayed;
        }

        public void CloseRateUsModal()
        {
            _modal.GetLpModalClose().Click();
            Browser.SwitchToTabByIndex(0);
        }

        public ReadOnlyCollection<IElement> GetNavElements()
        {
            return NavElements;
        }

        public bool IsEmployeeSignedInWithStoreInSession()
        {
            return Browser.Wait.ForBoolCondition(AutoCsrRString.Equals(TextActions.NormalizeWhitespace(UserNameLink.Text), StringComparison.InvariantCultureIgnoreCase));
        }

        public string GetCartCountInHeader()
        {
            return CartButtonProductCount.Text;
        }

        public void HoverOverSignInLink()
        {
            Browser.MouseOverOnElement(PortalLinks.FindElement(By.CssSelector(_pnlLoggedOutId.ToCssIdSelector())));
            Browser.Wait.IsVisibleElement(By.CssSelector($"{_pnlLoggedOutId.ToCssIdSelector()} > {HtmlTextWriterTag.Div} > {HtmlTextWriterTag.Div}"));
        }

        public bool IsSignInButtonVisible()
        {
            return HeaderSignInButton.Displayed;
        }

        public bool IsStoresLinkDropdownVisible()
        {
            return StoresLinkDropdown.Displayed;
        }

        public void OpenHeaderChatModal()
        {
            LiveChatHeader.Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(_conversationLogScrollableClass.ToCssClassSelector()));
        }

        public void OpenFooterChatModal()
        {
            FooterChatLink.Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(_conversationLogScrollableClass.ToCssClassSelector()));
        }

        public bool IsChatModalWindowVisible()
        {
            return ChatWindow.Displayed;
        }

        public void CloseChatModal()
        {
            ChatCloseButton.Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(_confirmationDialogClass.ToCssClassSelector()));
            ChatCloseButtonConfirmation.Click();
            Browser.Wait.IsInvisibleElement(By.CssSelector(_conversationLogScrollableClass.ToCssClassSelector()));
        }

        public void OpenChandelierMenu()
        {
            Browser.RefreshPage();
            Browser.SwitchToCurrentWindow();
            
            Browser.MouseOverOnElement(ChandeliersNavMenu);
            Browser.Wait.IsVisibleElement(By.ClassName(_categoryDropDownsClass));
        }

        public void OpenCeilingLightsMenu()
        {
            Browser.MouseOverOnElement(CeilingLightsNavLink);
            Browser.Wait.IsVisibleElement(By.CssSelector($"{_ceilingLightingId.ToCssIdSelector()} > {HtmlTextWriterTag.Div}"));
        }

        public void OpenLampsMenu()
        {
            Browser.MouseOverOnElement(TableAndFloorLampsNavLink);
            Browser.Wait.IsVisibleElement(By.CssSelector($"{_lampsId.ToCssIdSelector()} > {HtmlTextWriterTag.Div}"));
        }

        public void OpenWallLightsMenu()
        {
            Browser.MouseOverOnElement(WallLightsNavLink);
            Browser.Wait.IsVisibleElement(By.CssSelector($"{_wallLightsId.ToCssIdSelector()} > {HtmlTextWriterTag.Div}"));
        }

        public string FreeShippingFreeReturnsDisclaimer()
        {
            return _freeShippingFreeReturnsDisclaimerString;
        }

        public string FooterShippingTest()
        {
            return FooterFreeShippingFreeReturnsDisclaimer.Text;
        }

        public void HoverOverAccountLinkWhileStoreInSession()
        {
            Browser.ScrollToTopOfWindow();
            Browser.MouseOverOnElement(PortalLinks.FindElement(By.Id(_pnlLoggedOutId)));
            Browser.Wait.IsVisibleElement(By.CssSelector(_accountDropDownClass.ToCssClassSelector()));
        }

        public string GetRecentlyViewedSectionForStoreInSession()
        {
            return _recentlyViewedLinkClass;
        }

        public bool WaitForRecentlyViewedSection(string recentlyViewedSelector)
        {
            return Browser.Locate.DoesElementExistImmediately(recentlyViewedSelector);
        }

        public void OpenAccountMenuForStoreInSession()
        {
            Browser.MouseOverOnElement(HeaderUserMenu);
        }

        public bool IsStoreNumberFieldVisible()
        {
            return StoreNumberField.Displayed;
        }

        public bool IsOpenBoxLinkVisible()
        {
            return Browser.Locate.DoesElementExistImmediately(_plLPobClass.ToCssClassSelector());
        }

        public string GetFooterHomePageLink()
        {
            return FooterHomeLink.GetAttribute("href");
        }

        public virtual string GetEmailSubscribeFieldText()
        {
            return SignUpForCouponsOffersAndSaleAlertsLabel.Text;
        }

        public bool IsSignUpForCouponsOffersAndSaleAlertsLabelVisible()
        {
            return EmailSubscribeField.IsInitialized;
        }

        public bool IsSignUpForCouponsOffersAndSaleAlertsMessageVisible()
        {
            return EmailSubscribeMessage.IsInitialized;
        }

        public bool IsEmailSubscribeFieldVisible()
        {
            return SignUpForCouponsOffersAndSaleAlertsField.Displayed;
        }

        public bool IsEmailSubscribeButtonVisible()
        {
            return SignUpForCouponsOffersAndSaleAlertsSubscribeButton.IsInitialized;
        }

        public void OpenHospitalityLampsMenu()
        {
            Browser.MouseOverOnElement(HospitalityLampsMenuLink);
        }

        public string GetHospitalityLampsLink()
        {
            return HospitalityNightstandLampsLink.GetAttribute("href");
        }

        public void OpenHotelProgramsMenu()
        {
            Browser.MouseOverOnElement(HotelBrandProgramsLink);
        }

        public string GetHospitalityBestValueLink()
        {
            return HospitalityHotelAndBrandBestValueLink.GetAttribute("href");
        }

        public void NavigateToEmailPageFromFooter(string Email)
        {
            EmailSubscribeMessage.Click();
            EmailSubscribeMessage.SendKeys(Email);
            SignUpForEmailUpdatesSubmitButton.Click();
        }
    }
}
