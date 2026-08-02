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
    public class HeaderFooterMobile : HeaderFooterDesktop, IHeaderFooterMobile
    {
        //Class members
        private string _lpmmLoginStatusDividerClass   = "lpmmLoginStatus__divider";
        private string _toggleMenuXpath = "//div[@class='toggleMenu']";
        private string _lpmmMenuClass = "lpmmMenu";
        private string _lpCollapsibleClass  = "lpCollapsible";
        private string _sessionCamHideTextClass  = "sessioncamhidetext";
        private string _myAccountXpathLocator  = "//button[contains (@class,'lpmmLoginStatus__link')]";
        private string _lpmmLoginStatusLinkClass = "lpmmLoginStatus__link";
        private string _proLogoClass = "lampsPlusLogoIcon";
        private string _lpIconLampsPlusClass = "lpIcon-lampsplus";
        private string _subListClass  = "subList";
        private string _diningLivingRoomChandeliersString = "nav_Chandeliers_Dining_Living_Room";
        private string _flushMountString  = "nav_Ceiling_Lights_Flushmount_Ceiling_Lights";
        private string _wallLampsString = "nav_Wall_Lights_All_Wall_Lamps";
        private string _globalSearchFieldId = "globalSearchField";
        private string _searchIconXpath = "//button[@class='toggleSearch']";
        private string _globalSearchClass = "globalSearch";
        private string _contactUsLinkClass  = "lpmmLoginStatus__link";
        private string _lpDropdownNavigationMenuClass = "hdrMyAccountSubmenu";
        private string _ctDropdownAccountClass = "lpmmLoginStatus__link";
        private string _ctDropdownSignOutId  = "hdrSignOut";
        private string _allChandeliersNameString  = "nav_Chandeliers_All_Chandeliers";
        private string _tableLampsString = "nav_Lamps_All_Table_Lamps";
        private string _ftrCareersId  = "ftr-careers";
        private string _ftrAboutUsId  = "ftr-about-us";
        private string _ftrOrderStatusId = "ftr-order-status";
        private string _ftrReturnPolicyId  = "ftr-return-policy";
        private string _ftrCatalogsId = "ftr-catalogs";
        private string _ftrPinterestId  = "ftr-pinterest";
        private string _ftrInstagramId = "ftr-instagram";
        private string _ftrFacebookId = "ftr-facebook";
        private string _ftrYoutubeId = "ftr-youtube";
        private string _ftrHelpAndPoliciesCcpaId  = "ftr-help-and-policies-ccpa";
        private string _ftrCaTransparencyId = "ftr-ca-transparency";
        private string _ftrStoresId = "ftr-stores";
        private string _ftrHelpId = "ftr-help";
        private string _ftrTwitterId = "ftr-twitter";
        private string _footerDoNotSellMyInfoClass = "mblftrCCPA__inlineLinks";
        private string _ftrTermsOfUseId = "ftr-terms-of-use";
        private string _ftrSitemapId = "ftr-sitemap";
        private string _ftrEmailId = "ftr-email";
        private string _lpmmMenuContainerClass = "lpmmMenuContainer";
        private string _lpCollapsibleHeaderClass = "lpCollapsible__header";
        private string _hamburgerMenuXpath = "//*[@id='globalMenu']//div[contains(@class, 'lpScrollContainer')]";
        private string _createAccountString = "Create Account";
        private string _lpmmLoginStatusWrapperClass = "lpmmLoginStatusWrapper";
        private string _ftrProsId = "ftr-pros";
        private string _ftrHospitalityId = "ftr-hospitality";
        private string _rateUsClass = "rateUs";
        private string _ftrAccessibilityId = "ftr-accessibility";
        private string _ftrPrivacyId = "ftr-privacy";
        private string _footerSubscribeEmailClass = "footerSubscribe__email";
        private string _footerCallNumberString = "tel:18887390201";
        private string _emailAddressFtrId = "EmailAddressFtr";
        private string _footerSubscribeSubmitBtnId = "footerSubscribeSubmitBtn";
        private string _emailSubscribeSting = "Stay Connected\r\nGreat deals & inspiration to your inbox";
        private string _footerSubscribeCopyClass = "footerSubscribe__copy";
        private string _onSaleString = "On Sale";
        private string _ceilingLightsString = "Ceiling Lights";
        private string _tableAndFloorLampsString = "Table & Floor Lamps";
        private string _wallLightsString = "Wall Lights";
        private string _lampsAndShadesString = "Lamps & Shades";
        private string _chandeliersString = "Chandeliers";
        private string _footerChatXpath = "//nav //div[contains(text(), 'Chat')]";
        private string _ftrSubscribeSubmitBtnId = "footerSubscribeSubmitBtn";

        protected string _signInButtonClass => "globalMenuSignInButton";

        private IElement HamburgerMenu => Browser.Locate.ElementByXpath(_toggleMenuXpath);
        private IElement DisplayedMobileDrawerMenu => Browser.Locate.ElementByClassName(_lpmmMenuClass);
        private IElement LoginStatusIcon => Browser.Locate.ElementByXpath("//button[contains(@class,'lpmmLoginStatus__link')]");
        private IElement MyAccountDrawer => Browser.Locate.ElementBySelector(_lpCollapsibleClass.ToCssClassSelector());
        private IElement ManageAccountMenu => Browser.Locate.ElementByXpath("//a[@data-nav-sale=\"Manage Account\"]");
        private IElement HamburgerSubList => Browser.Locate.ElementByClassName(_subListClass);
        private IElement CartIcon => Browser.Locate.ElementByXpath("//a[@class='navCart']");
        private IElement SearchIcon => Browser.Locate.ElementByXpath(_searchIconXpath);
        private IElement GlobalSearch => Browser.Locate.ElementByClassName(_globalSearchClass);
        private IElement AccountIcon => Browser.Locate.ElementByClassName(_contactUsLinkClass);
        private IElement AccountDropdown => Browser.Locate.ElementById(_lpDropdownNavigationMenuClass);
        private IElement SignOutButton => Browser.Locate.ElementById(_ctDropdownSignOutId);
        private IElement LampsAndShadeNavLink => TestsBase.GetElementByElementText(HamburgerSubList, "button", _lampsAndShadesString);
        private IElement SignInButton => Browser.Locate.ElementBySelector(_lpmmLoginStatusDividerClass.ToCssClassSelector());
        private IElement SignInCreateAccountHamburger => Browser.Locate.ElementByClassName(_lpmmLoginStatusWrapperClass);
        private IElement FooterCallLink => Browser.Locate.ElementBySelector("ftr-call".ToCssIdSelector());
        private IElement FooterLpProsLink => Browser.Locate.ElementById(_ftrProsId);
        private IElement FooterHelpLink => Browser.Locate.ElementById(_ftrHelpId);
        private IElement FooterLpHospitalityLink => Browser.Locate.ElementById(_ftrHospitalityId);
        private IElement FooterRateUsLink => Browser.Locate.ElementBySelector(_rateUsClass.ToCssClassSelector());
        private IElement FooterAccessibilityLink => Browser.Locate.ElementBySelector(_ftrAccessibilityId.ToCssIdSelector());
        private IElement FooterPrivacyPolicyLink => Browser.Locate.ElementBySelector(_ftrPrivacyId.ToCssIdSelector());
        private IElement FooterEmailField => Browser.Locate.ElementBySelector(_footerSubscribeEmailClass.ToCssClassSelector());
        private IElement MyAccountLink => Browser.Locate.ElementByClassName(_ctDropdownAccountClass);
        private IElement SearchField => Browser.Locate.ElementBySelector(_globalSearchFieldId.ToCssIdSelector());

        private ReadOnlyCollection<IElement> AccountMenuDropDownsElements => Browser.Locate.ElementsBySelector($"{HtmlTextWriterTag.Ul} {HtmlTextWriterTag.Li}", AccountDropdown);

        private void WaitForDrawerAnimation()
        {
            Browser.Wait.ForElementToStopAnimating(HamburgerSubList);
        }

        protected override IElement UserNameLink => Browser.Locate.ElementByClassName(_sessionCamHideTextClass);
        protected override IElement ProLampsLogo => Browser.Locate.ElementByClassName(_proLogoClass);
        protected override IElement LampsLogo => Browser.Locate.ElementByClassName(_lpIconLampsPlusClass);
        protected override IElement ChandeliersDiningLivingRoomLink => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.A, HtmlTextWriterAttribute.Name, _diningLivingRoomChandeliersString);
        protected override IElement CeilingLightsNavLink => TestsBase.GetElementByElementText(HamburgerSubList, "button", _ceilingLightsString);
        protected override IElement CeilingLightsFlushMountLink => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.A, HtmlTextWriterAttribute.Name, _flushMountString);
        protected override IElement WallLightsNavLink => TestsBase.GetElementByElementText(HamburgerSubList, "button", _wallLightsString);
        protected override IElement WallLightsWallLampsLink => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.A, HtmlTextWriterAttribute.Name, _wallLampsString);
        protected override IElement ChandeliersNavLink => TestsBase.GetElementByElementText(HamburgerSubList, "button", _chandeliersString);
        protected override IElement AllChandeliersLink => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.A, HtmlTextWriterAttribute.Name, _allChandeliersNameString);
        protected override IElement AllTableLampsLink => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.A, HtmlTextWriterAttribute.Name, _tableLampsString);
        protected override IElement FooterCareersLink => Browser.Locate.ElementBySelector(_ftrCareersId.ToCssIdSelector());
        protected override IElement FooterAboutUsLink => Browser.Locate.ElementById(_ftrAboutUsId);
        protected override IElement FooterOrderStatusLink => Browser.Locate.ElementById(_ftrOrderStatusId);
        protected override IElement FooterReturnPolicyLink => Browser.Locate.ElementById(_ftrReturnPolicyId);
        protected override IElement FooterCatalogsLink => Browser.Locate.ElementById(_ftrCatalogsId);
        protected override IElement FooterPinterestLink => Browser.Locate.ElementBySelector(_ftrPinterestId.ToCssIdSelector());
        protected override IElement FooterInstagramLink => Browser.Locate.ElementBySelector(_ftrInstagramId.ToCssIdSelector());
        protected override IElement FooterFacebookLink => Browser.Locate.ElementBySelector(_ftrFacebookId.ToCssIdSelector());
        protected override IElement FooterYoutubeLink => Browser.Locate.ElementBySelector(_ftrYoutubeId.ToCssIdSelector());
        protected override IElement FooterCaTransparencyActLink => Browser.Locate.ElementBySelector(_ftrCaTransparencyId.ToCssIdSelector());
        protected override IElement FooterStoreLocatorLink => Browser.Locate.ElementById(_ftrStoresId);
        protected override IElement FooterCustomerServiceLink => Browser.Locate.ElementById(_ftrHelpId);
        protected override IElement FooterTwitterLink => Browser.Locate.ElementBySelector(_ftrTwitterId.ToCssIdSelector());
        protected override IElement FooterDoNotSellMyInfoLink => Browser.Locate.ElementByTagName(HtmlTextWriterTag.A, Browser.Locate.ElementByClassName(_footerDoNotSellMyInfoClass));
        protected override IElement FooterTermsOfUseLink => Browser.Locate.ElementBySelector(_ftrTermsOfUseId.ToCssIdSelector());
        protected override IElement FooterSiteMapLink => Browser.Locate.ElementBySelector(_ftrSitemapId.ToCssIdSelector());
        protected override IElement SignUpForCouponsOffersAndSaleAlertsLabel => Browser.Locate.ElementByClassName(_footerSubscribeCopyClass);
        protected override IElement HeaderSignInButton => Browser.Locate.ElementByClassName(_signInButtonClass);
        protected override IElement FooterChatLink => Browser.Locate.ElementByXpath(_footerChatXpath);
        protected override IElement SignUpForCouponsOffersAndSaleAlertsField => Browser.Locate.ElementById(_emailAddressFtrId);
        protected override IElement SignUpForEmailUpdatesSubmitButton => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Button, HtmlTextWriterAttribute.Id, _ftrSubscribeSubmitBtnId);
        protected override IElement SignUpForCouponsOffersAndSaleAlertsSubscribeButton => Browser.Locate.ElementById(_ftrSubscribeSubmitBtnId);

        protected override ReadOnlyCollection<IElement> NavElements => Browser.Locate.ElementByClassName(_lpmmMenuContainerClass).FindElements(By.CssSelector(_lpCollapsibleHeaderClass.ToCssClassSelector()));

        public HeaderFooterMobile(IBrowser browser, IAssert assert, IModalDesktop modal) : base(browser, assert, modal) { }

        //Interface implementation
        public override bool IsSignInLinkVisible => Browser.Locate.ElementBySelector(_lpmmLoginStatusDividerClass.ToCssClassSelector()).IsInitialized;

        public string GetGlobalNavLink(IElement parentElement, IElement subElement)
        {
            WaitForDrawerAnimation();
            Browser.ScrollIntoView(parentElement);
            parentElement.Click();
            WaitForDrawerAnimation();

            return GetElementLink(subElement);
        }

        public  Dictionary<string, string> GetGlobalNavLinks()
        {
            WaitForDrawerAnimation();

            var dict = new Dictionary<string, string>
            {
                {"AllChandeliersSortPageUrl", GetGlobalNavLink( ChandeliersNavLink, AllChandeliersLink)},
                {"ChandeliersDiningLivingRoomUrl", GetElementLink( ChandeliersDiningLivingRoomLink)},
                {"CeilingLightsFlushMountUrl", GetGlobalNavLink( CeilingLightsNavLink, CeilingLightsFlushMountLink)},
                {"LampsAndShadesUrl", GetGlobalNavLink( LampsAndShadeNavLink, AllTableLampsLink)},
                {"WallLampsPageUrl", GetGlobalNavLink( WallLightsNavLink, WallLightsWallLampsLink)}
            };

            return dict;
        }

        public override Dictionary<string, string> GetFooterLinks()
        {
            var dict = new Dictionary<string, string>
            {
                {"DoNotSellMyInfoUrl",GetElementLink(FooterDoNotSellMyInfoLink)},
                {"HelpAndPoliciesPageUrl", GetElementLink(FooterCustomerServiceLink)},
                {"CATransparencyPageUrl",GetElementLink(FooterCaTransparencyActLink)}
            };

            return dict;
        }

        public override Dictionary<string, string> GetCommonFooterNavLinksLinks()
        {
            var dict = new Dictionary<string, string>
            {
                {"StoresPageUrl", GetElementLink(FooterStoreLocatorLink)},
                {"CatalogPageUrl",GetElementLink(FooterCatalogsLink)},
                {"AboutUsPageUrl",GetElementLink(FooterAboutUsLink)},
                {"ProsPageUrl",GetElementLink(FooterLpProsLink)},
                {"OrderHistoryPageUrl",GetElementLink(FooterOrderStatusLink)},
                {"ReturnsPolicyPageUrl",GetElementLink(FooterReturnPolicyLink)},
                {"HelpPageUrl",GetElementLink(FooterHelpLink)},
                {"HospitalityPageUrl",GetElementLink(FooterLpHospitalityLink)},
            };

            return dict;
        }

        public Dictionary<string, string> GetMobileProUserFooterNavLinksLinks()
        {
            var dict = new Dictionary<string, string>
            {
                {"StoresPageUrl", GetElementLink(FooterStoreLocatorLink)},
                {"CatalogPageUrl",GetElementLink(FooterCatalogsLink)},
                {"AboutUsPageUrl",GetElementLink(FooterAboutUsLink)},
                {"OrderHistoryPageUrl",GetElementLink(FooterOrderStatusLink)},
                {"ReturnsPolicyPageUrl",GetElementLink(FooterReturnPolicyLink)},
                {"HelpPageUrl",GetElementLink(FooterHelpLink)}
            };

            return dict;
        }

        public override Dictionary<string, string> GetFooterSocialLinks()
        {
            var dict = new Dictionary<string, string>
            {
                {"LpPinterestUrl", GetElementLink(FooterPinterestLink)},
                {"LpInstagramUrl",GetElementLink(FooterInstagramLink)},
                {"LpFacebookUrl",GetElementLink(FooterFacebookLink)},
                {"LpYouTubeUrl",GetElementLink(FooterYoutubeLink)},
                {"LpTwitterUrl", GetElementLink(FooterTwitterLink)},
            };

            return dict;
        }

        public override Dictionary<string, string> GetCommonFooterLegalLinks()
        {
            var dict = new Dictionary<string, string>
            {
                { "CareersUrl", GetElementLink(FooterCareersLink)},
                { "RateUsUrl", GetElementLink(FooterRateUsLink) },
                { "AccessibilityPageUrl", GetElementLink(FooterAccessibilityLink) },
                { "PrivacyPageUrl", GetElementLink(FooterPrivacyPolicyLink) },
                { "TermsOfUsePageUrl", GetElementLink(FooterTermsOfUseLink) },
                { "SitemapPageUrl", GetElementLink(FooterSiteMapLink) }
            };

            return dict;
        }

        public Dictionary<string, string> GetMobileProUserFooterLegalLinks()
        {
            var dict = new Dictionary<string, string>
            {
                { "CareersUrl", GetElementLink(FooterCareersLink)},
                { "AccessibilityPageUrl", GetElementLink(FooterAccessibilityLink) },
                { "PrivacyPageUrl", GetElementLink(FooterPrivacyPolicyLink) },
                { "TermsOfUsePageUrl", GetElementLink(FooterTermsOfUseLink) },
                { "SitemapPageUrl", GetElementLink(FooterSiteMapLink) }
            };

            return dict;
        }

        public override Dictionary<string, string> GetHeaderElementsLinks()
        {
            var dict = new Dictionary<string, string>
            {
                { "MobileHeaderCartIconUrl", GetElementLink(CartIcon) },
                { "HamburgerLampsPlusLogoUrl", GetElementLink(LampsLogo) }
            };

            return dict;
        }

        public List<IElement> GetMyAccountElements()
        {
            var list = new List<IElement>
            {
                AccountDropdown,
                MyAccountLink,
                AccountMenuDropDownsElements[0],
                AccountMenuDropDownsElements[1],
                AccountMenuDropDownsElements[2],
                SignOutButton,
                DisplayedMobileDrawerMenu
            };
            return list;
        }

        public bool IsSearchFieldHidden()
        {
            return Browser.Wait.ForElementWithCssClassReturned(GlobalSearch, "hidden");
        }

        public override string GetCartIconLink()
        {
            Assert.Displayed(CartIcon, "Cart Icon Element not displayed");
            return GetElementLink(CartIcon);
        }

        public void HideSearchField()
        {
            Assert.Displayed(SearchField, "Search field not displayed on page load");
            Browser.Wait.ForElementToStopAnimating(SearchField);
            SearchIcon.Click();
            Browser.Wait.ForElementToStopAnimating(SearchField);
        }

        public void ToggleSearchIcon()
        {
            Browser.ScrollToTopOfWindow();
            Browser.Wait.ForDomReady();
            Browser.Wait.ForClickableElement(SearchIcon).Click();
        }

        public void ToggleHamburgerMenu()
        {
            HamburgerMenu.Click();
        }

        public override void OpenMyAccountMenu()
        {
            HamburgerMenu.Click();
            Browser.Wait.ForDomReady();
            Browser.Wait.ForElementToStopAnimating(AccountIcon);

            AccountIcon.Click();
            Browser.Wait.ForElementToStopAnimating(AccountDropdown);

            Assert.Displayed(AccountDropdown, "Account menu is not displayed.");
        }

        public override void NavigateToManageAccount()
        {
            OpenLpMenu();
            Browser.Wait.IsVisibleElement(By.XPath(_myAccountXpathLocator));
            LoginStatusIcon.Click();
            Browser.Wait.ForElementToStopAnimating(MyAccountDrawer);
            ManageAccountMenu.Click();
        }

        public override void SignOut()
        {
            if (!SignOutLink.IsInitialized) return;

            if (Browser.PageUrl.Equals(Urls.OrderConfirmationPageUrl))
            {
                Browser.Navigate(Urls.HomePageUrl);
            }

            OpenLpMenu();

            //Show My Account menu
            Browser.Wait.IsVisibleElement(By.XPath(_myAccountXpathLocator));
            Browser.Locate.ElementByXpath(_myAccountXpathLocator).Click();

            //Click Sign Out
            Browser.Wait.IsVisibleElement(By.CssSelector(HdrSignOutId.ToCssIdSelector()));
            SignOutLink.Click();
            Browser.Wait.ForDomReady();

            Browser.Wait.IsInvisibleElement(By.CssSelector(_lpmmLoginStatusLinkClass.ToCssClassSelector()));
        }



        public void OpenLpMenu()
        {
            Browser.Wait.IsVisibleElement(By.XPath(_toggleMenuXpath));
            Browser.ClickByJs(HamburgerMenu);
        }
        
        public void SelectSignInButton()
        {
            SignInButton.Click();
        }

        public By GetHamburgerMenu()
        {
            return By.XPath(_hamburgerMenuXpath);
        }

        public IElement GetHamburgerMenuSublist()
        {
            return HamburgerSubList;
        }

        public IElement GetSearchField()
        {
            return SearchField;
        }

        public string GetCreateAccountString()
        {
            return _createAccountString;
        }

        public string GetSignInText()
        {
            return SignInCreateAccountHamburger.Text;
        }

        public IElement GetFooterEmailField()
        {
            return FooterEmailField;
        }

        public string GetFooterCallButton()
        {
            return FooterCallLink.GetAttribute("href");
        }

        public string GetCallButtonPhoneNumber()
        {
            return _footerCallNumberString;
        }

        public string GetExpectedEmailSubscribeString()
        {
            return _emailSubscribeSting;
        }

        public override string GetEmailSubscribeFieldText()
        {
            return SignUpForCouponsOffersAndSaleAlertsLabel.Text;
        }

        public IElement WaitForEmailSubscribeElementToLoad()
        {
            return Browser.Wait.ForClickableElement(SignUpForCouponsOffersAndSaleAlertsLabel);
        }

        public IElement GetChandeliersNavLink()
        {
            OpenLpMenu();
            return ChandeliersNavLink;
        }

        public IElement GetAllChandeliersLink()
        {
            return AllChandeliersLink;
        }
    }
}
