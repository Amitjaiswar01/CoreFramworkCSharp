using System;
using System.Linq;
using System.Web.UI;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

using OpenQA.Selenium;

using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.IntegrationTests.Pages.HeaderFooter
{
    public class HeaderFooterLocatorDesktopTests : HeaderFooterLocatorTests
    {
        public HeaderFooterLocatorDesktopTests(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Integration.PageObjectModel, "HeaderFooter")]
        [Theory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LocateHeaderFooterElements(string config) => Locate(config);

        protected override void AnonymousElementValidation()
        {
            VerifyElementDisplayed(() => HeaderFooter.LpHeader);
            VerifyElementDisplayed(() => HeaderFooter.HeaderChatLink);
            VerifyElementDisplayed(() => HeaderFooter.SavedIcon);
            VerifyElementDisplayed(() => HeaderFooter.StoreLocations);

            Browser.MouseOverOnElement(HeaderFooter.StoreLocations);
            Browser.Wait.ForDisplayedElement(HeaderFooter.StoresDropdownMenu);

            VerifyElementDisplayed(() => HeaderFooter.StoresDropdownMenu);
            VerifyElementDisplayed(() => HeaderFooter.PortalLinks);
            VerifyElementDisplayed(() => HeaderFooter.SignUpForCouponsOffersAndSaleAlertsField);
            VerifyElementDisplayed(() => HeaderFooter.SignUpForCouponsOffersAndSaleAlertsLabel);
            VerifyElementDisplayed(() => HeaderFooter.SignUpForCouponsOffersAndSaleAlertsSubscribeButton);
            VerifyElementDisplayed(() => HeaderFooter.FreeShippingSpan);
            VerifyElementDisplayed(() => HeaderFooter.CartButtonImmediately);

            VerifyElementNotImplemented(() => HeaderFooter.CartIcon);
            VerifyElementNotImplemented(() => HeaderFooter.SearchIcon);
            VerifyElementNotImplemented(() => HeaderFooter.GlobalSearch);
            VerifyElementNotImplemented(() => HeaderFooter.WishListIcon);
            VerifyElementNotImplemented(() => HeaderFooter.MobileStickyHeader);
            VerifyElementNotImplemented(() => HeaderFooter.ContactUsPhoneIcon);

            Browser.Wait.ForDisplayedElement(HeaderFooter.FreeShippingSpan);
            HeaderFooter.FreeShippingSpan.Click();

            VerifyElementDisplayed(() => HeaderFooter.FreeShippingModal);
            
            CloseLpModal();
            
            VerifyElementDisplayed(() => HeaderFooter.SignInLink);
            Browser.MouseOverOnElement(HeaderFooter.SignInLink);
            Browser.Wait.ForDisplayedElement(HeaderFooter.SignInContainer);
            VerifyElementDisplayed(() => HeaderFooter.SignInContainer);
            Browser.Wait.ForDisplayedElement(HeaderFooter.SignInToolTip);
            Verify.Displayed(HeaderFooter.SignInToolTip, "The signin modal did not open.");
            VerifyElementDisplayed(() => HeaderFooter.SignInToolTip);
            Browser.SwitchFocusToIframe(HeaderFooter.SignInToolTip);
            VerifyElementDisplayed(() => HeaderFooter.SignInPopUp);
            VerifyElementDisplayed(() => HeaderFooter.HeaderOrderHistoryLink);
            VerifyElementDisplayed(() => HeaderFooter.HeaderRecentlyViewedLinkForUnSignedInUser);
            VerifyElementDisplayed(() => HeaderFooter.HeaderCreateAccountLink);
            VerifyElementDisplayed(() => HeaderFooter.HeaderSignInButton);


            Browser.SwitchToDefaultContent();
            Browser.MouseOverOnElement(HeaderFooter.LampsLogo);

            VerifyElementDisplayed(() => HeaderFooter.PortalLinkLpOpenBox);
            VerifyElementDisplayed(() => HeaderFooter.FooterCurbsidePickupLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterDoNotSellMyInfoLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterHomeLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterLightingDesignServicesLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterLpHospitalityLogoLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterLpProsLogoLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterInstallationServicesLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterNewHomeownerSavingsLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterNewHomeownerSavingsLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterTermOfUseLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterPrivacyPolicyLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterSeeOurPolicyLink);

            Browser.Navigate(Urls.HomePageUrl);
            Browser.MouseOverOnElement(HeaderFooter.SavedMenu);
            Browser.Wait.ForDisplayedElement(HeaderFooter.SavedRooms);

            VerifyElementDisplayed(() => HeaderFooter.SavedMenu);
            VerifyElementDisplayed(() => HeaderFooter.SavedRooms);
            VerifyElementDisplayed(() => HeaderFooter.WishListItemCountElement);
            VerifyElementDisplayed(() => HeaderFooter.WishListLink);
            VerifyElementDisplayed(() => HeaderFooter.WishListButtonImmediately);
            VerifyElementDisplayed(() => HeaderFooter.BodyElement);
            VerifyElementDisplayed(() => HeaderFooter.FooterAdviceAndTipsLink);
            VerifyElementDisplayed(() => HeaderFooter.ProFooterCatalogsLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterTradeProgramLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterFaqsLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterManageAccountLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterShippingInfoLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterSiteMapLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterTermsOfUseLink);
            VerifyElementDisplayed(() => HeaderFooter.CartButton);

            VerifyElementNotImplemented(() => HeaderFooter.HamburgerMenu);
            VerifyElementNotImplemented(() => HeaderFooter.HamburgerSubList);
            VerifyElementNotImplemented(() => HeaderFooter.HamburgerMenuContainer);
            VerifyElementNotImplemented(() => HeaderFooter.FooterList);
            VerifyElementNotImplemented(() => HeaderFooter.FooterCallIcon);
            VerifyElementNotImplemented(() => HeaderFooter.FooterDesktopSiteLink);
            VerifyElementNotImplemented(() => HeaderFooter.FooterCreateAccountLink);
            VerifyElementNotImplemented(() => HeaderFooter.FooterLpHospitalityLink);
            VerifyElementNotImplemented(() => HeaderFooter.FooterLpProsLink);
            VerifyElementNotImplemented(() => HeaderFooter.FooterOpenBoxLink);
            VerifyElementNotImplemented(() => HeaderFooter.FooterSignInLink);
            VerifyElementNotImplemented(() => HeaderFooter.FooterTextIcon);
            VerifyElementNotImplemented(() => HeaderFooter.StoreLocationBar);
            VerifyElementNotImplemented(() => HeaderFooter.StoreIcon);
            VerifyElementNotImplemented(() => HeaderFooter.FooterCallLink);
            VerifyElementNotImplemented(() => HeaderFooter.FooterTextLink);
            VerifyElementNotImplemented(() => HeaderFooter.Footer);

            VerifyElementDisplayed(() => HeaderFooter.WallLightsNavLink);
            VerifyElementDisplayed(() => HeaderFooter.CeilingLightsNavLink);
            VerifyElementDisplayed(() => HeaderFooter.ChandeliersNavLink);
            VerifyElementDisplayed(() => HeaderFooter.FansNavLink);
            VerifyElementDisplayed(() => HeaderFooter.FurnitureNavLink);
            VerifyElementDisplayed(() => HeaderFooter.HomeDecorNavLink);
            VerifyElementDisplayed(() => HeaderFooter.TableAndFloorLampsNavLink);
            VerifyElementDisplayed(() => HeaderFooter.OutdoorNavLink);
            VerifyElementDisplayed(() => HeaderFooter.SaleMenu);
            
            Browser.MouseOverOnElement(HeaderFooter.SaleMenu);
            Browser.Wait.ForDisplayedElement(HeaderFooter.SaleLink);

            VerifyElementDisplayed(() => HeaderFooter.SaleLink);
            VerifyElementDisplayed(() => HeaderFooter.DailySalesLink);
            VerifyElementDisplayed(() => HeaderFooter.ClearanceLink);
            VerifyElementDisplayed(() => HeaderFooter.InstantCouponLink);
            VerifyElementDisplayed(() => HeaderFooter.OpenBoxLink);
            VerifyElementDisplayed(() => HeaderFooter.OpenBoxSaleLink);

            Browser.MouseOverOnElement(HeaderFooter.InspirationMenu);

            Browser.Wait.ForDisplayedElement(HeaderFooter.InspirationMenu);

            VerifyElementDisplayed(() => HeaderFooter.InspirationMenu);
            VerifyElementDisplayed(() => HeaderFooter.RoomInspirationLink);
            VerifyElementDisplayed(() => HeaderFooter.InspirationShopByTrendLink);
            VerifyElementDisplayed(() => HeaderFooter.InspirationIdeasAndAdviceLink);
            VerifyElementDisplayed(() => HeaderFooter.ShopByStyleModernLink);
            VerifyElementDisplayed(() => HeaderFooter.ShopByStyleFarmhouseLink);
            VerifyElementDisplayed(() => HeaderFooter.ShopByStyleGlamLuxeLink);
            VerifyElementDisplayed(() => HeaderFooter.ShopByStyleIndustrialLink);
            VerifyElementDisplayed(() => HeaderFooter.ShopByStyleCrystalLink);
            VerifyElementDisplayed(() => HeaderFooter.ShopByStyleTiffanyLink);
            VerifyElementDisplayed(() => HeaderFooter.ShopByStyleCountryCottageLink);
            VerifyElementDisplayed(() => HeaderFooter.ShopByStyleMidCenturyLink);
            VerifyElementDisplayed(() => HeaderFooter.ShopByStyleRusticLink);

            VerifyElementNotImplemented(() => HeaderFooter.ShopByStyleMenu);

            Browser.MouseOverOnElement(HeaderFooter.ChandeliersNavLink);
            VerifyElementDisplayed(() => HeaderFooter.AllChandeliersLink);

            Browser.MouseOverOnElement(HeaderFooter.ChandeliersNavLink);
            VerifyElementDisplayed(() => HeaderFooter.ChandeliersDiningLivingRoomLink);

            Browser.MouseOverOnElement(HeaderFooter.CeilingLightsNavLink);
            VerifyElementDisplayed(() => HeaderFooter.CeilingLightsFlushMountLink);

            Browser.MouseOverOnElement(HeaderFooter.TableAndFloorLampsNavLink);
            VerifyElementDisplayed(() => HeaderFooter.AllTableLampsLink);

            Browser.MouseOverOnElement(HeaderFooter.WallLightsNavLink);
            VerifyElementDisplayed(() => HeaderFooter.WallLightsWallLampsLink);

            Browser.Wait.ForClickableElement(HeaderFooter.FooterRateUsLink).Click();
            Browser.Wait.ForDisplayedElement(HeaderFooter.RateUsPage);

            VerifyElementDisplayed(() => HeaderFooter.RateUsPage);
            VerifyElementDisplayed(() => HeaderFooter.SubmitRatingBtn);
            VerifyElementDisplayed(() => HeaderFooter.RateUsConfirmationPage);
            VerifyElementDisplayed(() => HeaderFooter.RateUsContainer);
            VerifyElementDisplayed(() => HeaderFooter.RateUsStarsContainer);
            VerifyElementDisplayed(() => HeaderFooter.RateUsStarsFifthStarElement);

            Browser.SwitchToDefaultContent();
            CloseLpModal();

            Browser.SwitchToDefaultContent();

            VerifyElementDisplayed(() => HeaderFooter.FooterCCPA);
            VerifyElementDisplayed(() => HeaderFooter.FooterDontSellPersonalInfoLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterDontSeePolicyLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterEmailLink);
        }

        protected override void CustomerServiceElementValidation()
        {
            SignInWorkflow.SignIn(LampsPlusAccounts.CustomerServiceRegularLoginAccount);
            Browser.MouseOverOnElement(HeaderFooter.ActiveSession);
            
            Browser.Wait.ForDisplayedElement(HeaderFooter.CloseCurrentSessionLink);

            VerifyElementDisplayed(() => HeaderFooter.CloseCurrentSessionLink);
            VerifyElementDisplayed(() => HeaderFooter.SessionMenu);
            VerifyElementDisplayed(() => HeaderFooter.UserNameLink);
            VerifyElementDisplayed(() => HeaderFooter.HeaderUserMenu);
            VerifyElementDisplayed(() => HeaderFooter.ActiveSession);


            Browser.MouseOverOnElement(HeaderFooter.UserNameLink);

            Browser.Wait.ForDisplayedElement(HeaderFooter.SignOutLink);

            VerifyElementDisplayed(() => HeaderFooter.SignOutLink);

            HeaderFooter.SignOutLink.Click();
        }

        protected override void CustomerServiceManagerElementValidation()
        {
            SignInWorkflow.SignIn(LampsPlusAccounts.CustomerServiceManagerLoginAccount);

            Browser.MouseOverOnElement(HeaderFooter.HeaderUserMenu);
            VerifyElementDisplayed(() => HeaderFooter.EmployeeToolsLink);
            VerifyElementDisplayed(() => HeaderFooter.ManageAccountPageLink);
            VerifyElementDisplayed(() => HeaderFooter.HeaderManageAccountLink);
            VerifyElementDisplayed(() => HeaderFooter.MyOrdersLink);
            VerifyElementDisplayed(() => HeaderFooter.HeaderRecentlyViewedLinkForSignedInUsers);
            VerifyElementDisplayed(() => HeaderFooter.SignOutLink);
            HeaderFooter.SignOutLink.Click();
        }

        protected override void HospitalityElementValidation()
        {
            Browser.ClearAllCookies();
            Browser.Navigate(Urls.HomePageUrl);
            SignInWorkflow.SignIn(LampsPlusAccounts.HospitalityLoginAccount);
			VerifyElementDisplayed(() => HeaderFooter.FooterHospitalityFaqsLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterWarrantyLink);
            VerifyElementDisplayed(() => HeaderFooter.HospitalityLampsMenuLink);
            VerifyElementDisplayed(() => HeaderFooter.HotelBrandProgramsLink);


            Browser.MouseOverOnElement(HeaderFooter.HospitalityLampsMenuLink);
            VerifyElementDisplayed(() => HeaderFooter.HospitalityNightstandLampsLink);

            Browser.MouseOverOnElement(HeaderFooter.HotelBrandProgramsLink);
            VerifyElementDisplayed(() => HeaderFooter.HospitalityHotelAndBrandBestValueLink);
        }

        protected override void VerifyBoldChat()
        {
            Browser.ClearAllCookies();
            Browser.Navigate(Urls.HomePageUrl);

            VerifyElementDisplayed(() => HeaderFooter.FooterChatLink);
            HeaderFooter.FooterChatLink.FindElement(By.TagName(HtmlTextWriterTag.A.ToString())).Click();
            Browser.Wait.ForDisplayedElement(HeaderFooter.ChatContainer);
            VerifyElementDisplayed(() => HeaderFooter.ChatContainer);
            VerifyElementDisplayed(() => HeaderFooter.BoldChatCloseButton);
        }

        protected override void ProfessionalElementValidation()
        {
            VerifyElementDisplayed(() => HeaderFooter.NavElements);
            VerifyElementDisplayed(() => HeaderFooter.GetRandomNavCategoryElement);
            VerifyElementDisplayed(() => HeaderFooter.GetChandeliersNavElement);

            VerifyElementNotImplemented(() => HeaderFooter.ProContactPhoneLink);
            VerifyElementNotImplemented(() => HeaderFooter.FooterSignOutLink);
            VerifyElementNotImplemented(() => HeaderFooter.ProFooterStoreLocatorLink);
            VerifyElementNotImplemented(() => HeaderFooter.FooterProCustomerServiceLink);
            VerifyElementNotImplemented(() => HeaderFooter.ProFooterCallLink);
            VerifyElementNotImplemented(() => HeaderFooter.ProFooterTextLink);

            Browser.MouseOverOnElement(HeaderFooter.HeaderUserMenu);
            VerifyElementDisplayed(() => HeaderFooter.HeaderUserOrderHistoryLink);

            Browser.MouseOverOnElement(HeaderFooter.ChandeliersNavLink);
            
            Browser.Wait.ForDisplayedElement(HeaderFooter.CategoryDropDowns.First());

            VerifyElementDisplayed(() => HeaderFooter.CategoryDropDowns);

            Browser.MouseOverOnElement(HeaderFooter.SaleMenu);
            Browser.Wait.ForDisplayedElement(HeaderFooter.ProsSpecialsLink);

            VerifyElementDisplayed(() => HeaderFooter.ProsSpecialsLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterMyLampsPlusLink);
        }
    }


    public class HeaderFooterLocatorMobileTests : HeaderFooterLocatorTests
    {
        public HeaderFooterLocatorMobileTests(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Integration.PageObjectModel, "HeaderFooter")]
        [Theory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LocateHeaderFooterElements(string config) => Locate(config);

        protected override void AnonymousElementValidation()
        {
            VerifyElementNotImplemented(() => HeaderFooter.WishListLink);

            ShoppingCartWorkflow.AddSingleItemToCart();

            Browser.Navigate(Urls.HomePageUrl);

            VerifyElementDisplayed(() => HeaderFooter.ContactUsPhoneIcon);
            VerifyElementDisplayed(() => HeaderFooter.CartButtonImmediately);
            VerifyElementDisplayed(() => HeaderFooter.CartButton);
            VerifyElementDisplayed(() => HeaderFooter.CartIcon);
            VerifyElementDisplayed(() => HeaderFooter.FooterDoNotSellMyInfoLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterSeeOurPolicyLink);
            VerifyElementDisplayed(() => HeaderFooter.SearchIcon);
			VerifyElementDisplayed(() => HeaderFooter.GlobalSearch);
            VerifyElementDisplayed(() => HeaderFooter.FooterList);
            VerifyElementDisplayed(() => HeaderFooter.MobileStickyHeader);
            
            VerifyElementNotImplemented(() => HeaderFooter.HeaderSignInButton);
            VerifyElementNotImplemented(() => HeaderFooter.WishListIcon);
            VerifyElementNotImplemented(() => HeaderFooter.BodyElement);
            VerifyElementNotImplemented(() => HeaderFooter.FooterCurbsidePickupLink);
            VerifyElementNotImplemented(() => HeaderFooter.FooterFaqsLink);
            VerifyElementNotImplemented(() => HeaderFooter.FooterHomeLink);
            VerifyElementNotImplemented(() => HeaderFooter.FooterInHomeConsultationsLink);
            VerifyElementNotImplemented(() => HeaderFooter.FooterLightingDesignServicesLink);
            VerifyElementNotImplemented(() => HeaderFooter.FooterManageAccountLink);
            VerifyElementNotImplemented(() => HeaderFooter.FooterNewHomeownerSavingsLink);
            VerifyElementNotImplemented(() => HeaderFooter.FooterShippingInfoLink);
            VerifyElementNotImplemented(() => HeaderFooter.FooterSiteMapLink);
            VerifyElementNotImplemented(() => HeaderFooter.FooterTermsOfUseLink);
            VerifyElementNotImplemented(() => HeaderFooter.FooterTradeProgramLink);
            VerifyElementNotImplemented(() => HeaderFooter.FreeShippingSpan);
            VerifyElementNotImplemented(() => HeaderFooter.FreeShippingModal);
            VerifyElementNotImplemented(() => HeaderFooter.HeaderChatLink);
            VerifyElementNotImplemented(() => HeaderFooter.FooterLpHospitalityLogoLink);
            VerifyElementNotImplemented(() => HeaderFooter.FooterLpProsLogoLink);
            VerifyElementNotImplemented(() => HeaderFooter.OpenBoxLink);
            VerifyElementNotImplemented(() => HeaderFooter.PortalLinks);
            VerifyElementNotImplemented(() => HeaderFooter.SavedMenu);
            VerifyElementNotImplemented(() => HeaderFooter.SavedRooms);
            VerifyElementNotImplemented(() => HeaderFooter.SignInLink);
            VerifyElementNotImplemented(() => HeaderFooter.SignInPopUp);
            VerifyElementNotImplemented(() => HeaderFooter.SignInToolTip);
            VerifyElementNotImplemented(() => HeaderFooter.SignUpForCouponsOffersAndSaleAlertsField);
            VerifyElementNotImplemented(() => HeaderFooter.SignUpForCouponsOffersAndSaleAlertsLabel);
            VerifyElementNotImplemented(() => HeaderFooter.SignUpForCouponsOffersAndSaleAlertsSubscribeButton);
            VerifyElementNotImplemented(() => HeaderFooter.StoreLocations);
            VerifyElementNotImplemented(() => HeaderFooter.StoresDropdownMenu);
            VerifyElementNotImplemented(() => HeaderFooter.StoreIcon);
            VerifyElementNotImplemented(() => HeaderFooter.StoreLocationBar);
            VerifyElementNotImplemented(() => HeaderFooter.WishListButtonImmediately);
            VerifyElementNotImplemented(() => HeaderFooter.WishListItemCountElement);
            VerifyElementNotImplemented(() => HeaderFooter.SessionMenu);
            VerifyElementNotImplemented(() => HeaderFooter.FooterPrivacyPolicyLink);
            VerifyElementNotImplemented(() => HeaderFooter.InspirationMenu);
            VerifyElementNotImplemented(() => HeaderFooter.ProFooterCatalogsLink);
            VerifyElementNotImplemented(() => HeaderFooter.SignInContainer);
            VerifyElementNotImplemented(() => HeaderFooter.FooterAdviceAndTipsLink);
            VerifyElementNotImplemented(() => HeaderFooter.HeaderOrderHistoryLink);
            VerifyElementNotImplemented(() => HeaderFooter.HeaderRecentlyViewedLinkForSignedInUsers);
            VerifyElementNotImplemented(() => HeaderFooter.HeaderCreateAccountLink);
         

            HeaderFooter.HamburgerMenu.Click();
            Browser.Wait.ForDomReady();

            VerifyElementDisplayed(() => HeaderFooter.HamburgerMenu);
            VerifyElementDisplayed(() => HeaderFooter.HamburgerSubList);
            VerifyElementDisplayed(() => HeaderFooter.HamburgerMenuContainer);
            VerifyElementDisplayed(() => HeaderFooter.SaleMenu);
            VerifyElementDisplayed(() => HeaderFooter.WallLightsNavLink);
            VerifyElementDisplayed(() => HeaderFooter.CeilingLightsNavLink);
            VerifyElementDisplayed(() => HeaderFooter.ChandeliersNavLink);
            VerifyElementDisplayed(() => HeaderFooter.FansNavLink);
            VerifyElementDisplayed(() => HeaderFooter.FurnitureNavLink);
            VerifyElementDisplayed(() => HeaderFooter.HomeDecorNavLink);
            VerifyElementDisplayed(() => HeaderFooter.TableAndFloorLampsNavLink);
            VerifyElementDisplayed(() => HeaderFooter.OutdoorNavLink);
            VerifyElementDisplayed(() => HeaderFooter.SavedIcon);

            HeaderFooter.ChandeliersNavLink.Click();
            Browser.Wait.ForDisplayedElement(HeaderFooter.ChandeliersNavLink);
            VerifyElementDisplayed(() => HeaderFooter.AllChandeliersLink);
            HeaderFooter.AllChandeliersLink.Click();

            HeaderFooter.HamburgerMenu.Click();
            HeaderFooter.ChandeliersNavLink.Click();
            Browser.Wait.ForDisplayedElement(HeaderFooter.ChandeliersDiningLivingRoomLink);
            VerifyElementDisplayed(() => HeaderFooter.ChandeliersDiningLivingRoomLink);
            HeaderFooter.ChandeliersDiningLivingRoomLink.Click();

            HeaderFooter.HamburgerMenu.Click();
            HeaderFooter.CeilingLightsNavLink.Click();
            VerifyElementDisplayed(() => HeaderFooter.CeilingLightsFlushMountLink);
            HeaderFooter.CeilingLightsFlushMountLink.Click();

            HeaderFooter.HamburgerMenu.Click();
            HeaderFooter.TableAndFloorLampsNavLink.Click();
            VerifyElementDisplayed(() => HeaderFooter.AllTableLampsLink);
            HeaderFooter.AllTableLampsLink.Click();

            HeaderFooter.HamburgerMenu.Click();
            Browser.Wait.ForDomReady();

            Browser.Wait.ForDisplayedElement(HeaderFooter.WallLightsNavLink, 2);
            Browser.MouseOverOnElement(HeaderFooter.WallLightsNavLink);
            HeaderFooter.WallLightsNavLink.Click();

            Browser.Wait.ForDisplayedElement(HeaderFooter.WallLightsWallLampsLink, 10);
            VerifyElementDisplayed(() => HeaderFooter.WallLightsWallLampsLink);

            HeaderFooter.HamburgerMenu.Click();

            VerifyElementDisplayed(() => HeaderFooter.FooterCallIcon);
            VerifyElementNotImplemented(() => HeaderFooter.FooterDesktopSiteLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterCreateAccountLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterLpHospitalityLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterLpProsLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterOpenBoxLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterSignInLink);
            VerifyElementExists(() => HeaderFooter.FooterTextIcon);

            HeaderFooter.HamburgerMenu.Click();

            Browser.Wait.ForDisplayedElement(HeaderFooter.SaleMenu,2);
            HeaderFooter.SaleMenu.Click();

            VerifyElementDisplayed(() => HeaderFooter.SaleLink);
            VerifyElementDisplayed(() => HeaderFooter.DailySalesLink);
            VerifyElementDisplayed(() => HeaderFooter.ClearanceLink);
            VerifyElementDisplayed(() => HeaderFooter.InstantCouponLink);
            VerifyElementDisplayed(() => HeaderFooter.PortalLinkLpOpenBox);
            
            
            Browser.Wait.ForDisplayedElement(HeaderFooter.ShopByStyleMenu,5);
            HeaderFooter.ShopByStyleMenu.Click();

            VerifyElementDisplayed(() => HeaderFooter.ShopByStyleModernLink);
            VerifyElementDisplayed(() => HeaderFooter.ShopByStyleFarmhouseLink);
            VerifyElementDisplayed(() => HeaderFooter.ShopByStyleIndustrialLink);
            VerifyElementDisplayed(() => HeaderFooter.ShopByStyleCrystalLink);
            VerifyElementDisplayed(() => HeaderFooter.ShopByStyleGlamLuxeLink);
            VerifyElementDisplayed(() => HeaderFooter.ShopByStyleTiffanyLink);
            VerifyElementDisplayed(() => HeaderFooter.ShopByStyleCountryCottageLink);
            VerifyElementDisplayed(() => HeaderFooter.ShopByStyleMidCenturyLink);
            VerifyElementDisplayed(() => HeaderFooter.ShopByStyleRusticLink);

            HeaderFooter.ShopByStyleMenu.Click();
            Browser.ScrollToElement(HeaderFooter.InspirationIdeasAndAdviceLink);

            VerifyElementDisplayed(() => HeaderFooter.ShopByStyleMenu);
            VerifyElementDisplayed(() => HeaderFooter.RoomInspirationLink);
            VerifyElementDisplayed(() => HeaderFooter.InspirationShopByTrendLink);
            VerifyElementDisplayed(() => HeaderFooter.InspirationIdeasAndAdviceLink);

            HeaderFooter.HamburgerMenu.Click();

            Browser.Wait.ForClickableElement(HeaderFooter.FooterRateUsLink).Click();
            VerifyElementDisplayed(() => HeaderFooter.RateUsPage);
            VerifyElementDisplayed(() => HeaderFooter.SubmitRatingBtn);
            VerifyElementDisplayed(() => HeaderFooter.RateUsContainer);
            VerifyElementDisplayed(() => HeaderFooter.RateUsStarsContainer);
            VerifyElementDisplayed(() => HeaderFooter.RateUsStarsFifthStarElement);

            HeaderFooter.RateUsStarsFifthStarElement.Click();
            HeaderFooter.SubmitRatingBtn.Click();

            VerifyElementDisplayed(() => HeaderFooter.RateUsConfirmationPage);

            Browser.Navigate(Urls.HomePageUrl);
        }

        protected override void CustomerServiceElementValidation()
        {
            SignInWorkflow.SignIn(LampsPlusAccounts.CustomerLoginAccount);
            HeaderFooter.HamburgerMenu.Click();
            VerifyElementNotImplemented(() => HeaderFooter.ActiveSession);
            VerifyElementNotImplemented(() => HeaderFooter.CloseCurrentSessionLink);
            VerifyElementNotImplemented(() => HeaderFooter.HeaderUserMenu);
            VerifyElementDisplayed(() => HeaderFooter.UserNameLink);
            VerifyElementDisplayed(() => HeaderFooter.HeaderManageAccountLink);
            VerifyElementDisplayed(() => HeaderFooter.SignOutLink);
            SignInWorkflow.SignOut();

        }

        protected override void CustomerServiceManagerElementValidation()
        {
            throw new NotImplementedException();
        }

        protected override void HospitalityElementValidation()
        {
            VerifyElementNotImplemented(() => HeaderFooter.FooterHospitalityFaqsLink);
            VerifyElementNotImplemented(() => HeaderFooter.FooterWarrantyLink);
            VerifyElementNotImplemented(() => HeaderFooter.HospitalityLampsMenuLink);
            VerifyElementNotImplemented(() => HeaderFooter.HotelBrandProgramsLink);
            VerifyElementNotImplemented(() => HeaderFooter.HospitalityNightstandLampsLink);
            VerifyElementNotImplemented(() => HeaderFooter.HospitalityHotelAndBrandBestValueLink);
        }

        protected override void ProfessionalElementValidation()
        {
            VerifyElementDisplayed(() => HeaderFooter.GetRandomNavCategoryElement);

            HeaderFooter.HamburgerMenu.Click();

            VerifyElementDisplayed(() => HeaderFooter.GetChandeliersNavElement);

            VerifyElementDisplayed(() => HeaderFooter.NavElements);
            VerifyElementDisplayed(() => HeaderFooter.ProContactPhoneLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterSignOutLink);
            VerifyElementDisplayed(() => HeaderFooter.ProFooterStoreLocatorLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterProCustomerServiceLink);

            VerifyElementsNotImplemented(() => HeaderFooter.CategoryDropDowns);
            VerifyElementNotImplemented(() => HeaderFooter.ProsSpecialsLink);
            VerifyElementNotImplemented(() => HeaderFooter.FooterMyLampsPlusLink);
        }

        protected override void VerifyBoldChat()
        {
            Browser.ClearAllCookies();
            Browser.Navigate(Urls.HomePageUrl);

            HeaderFooter.ContactUsPhoneIcon.Click();

            VerifyElementDisplayed(() => HeaderFooter.FooterChatLink);
            HeaderFooter.FooterChatLink.FindElement(By.TagName(HtmlTextWriterTag.A.ToString())).Click();
            Browser.Wait.ForDisplayedElement(HeaderFooter.ChatContainer);
            VerifyElementDisplayed(() => HeaderFooter.ChatContainer);
        }
    }


    /// <summary>
    /// Tests to ensure all IElements and Lists of IElements can be found in the Header and Footer sections.
    /// </summary>
    public abstract class HeaderFooterLocatorTests : PageObjectTestsBase
    {
        protected HeaderFooterLocatorTests(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the requested elements could be located in the Header and Footer sections.
        /// </summary>
        public void Locate(string config)
        {
            InitializeFramework(config, Urls.HomePageUrl);
            BuildElementsList(HeaderFooter); 

            WishListWorkflow.AddToWishlistIfEmpty();

            Browser.Navigate(Urls.HomePageUrl);

            //Anonymous mode.
            VerifyElementDisplayed(() => HeaderFooter.LampsLogo);

            var item = ProductActions.GetListableInStockShortSku();
            
            ConditionalVerify.DatabaseObject(item, "ProductActions.GetListableInStockShortSku()");

            ProductDetail.NavigateToProductDetailByShortSku(item);

            Browser.Navigate(Urls.SortPageUrls.OrderBy(s => Guid.NewGuid()).First());

            VerifyElementDisplayed(() => HeaderFooter.ViewAllRecentlyViewedButton);

            Browser.Navigate(Urls.HomePageUrl);

            AnonymousElementValidation();

            VerifyElementDisplayed(() => HeaderFooter.FooterEmailIcon);
            VerifyElementDisplayed(() => HeaderFooter.FooterStoreLocatorLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterCustomerServiceLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterStoreCouponsAndOffersLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterReturnPolicyLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterCaTransparencyActLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterAboutUsLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterCareersLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterCatalogsLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterRateUsLink);
            VerifyElementDisplayed(() => HeaderFooter.ContactPhoneLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterContactUs);
            VerifyElementDisplayed(() => HeaderFooter.FooterFacebookLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterHouzzLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterInstagramLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterPinterestLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterTwitterLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterYoutubeLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterOrderStatusLink);
            VerifyElementDisplayed(() => HeaderFooter.FooterAccessibilityLink);


            // Employee mode.
            CustomerServiceElementValidation();
            CustomerServiceManagerElementValidation();

            // Professional mode.
            SignInWorkflow.SignIn(LampsPlusAccounts.ProfessionalLoginAccount);
            VerifyElementDisplayed(() => HeaderFooter.ProLampsLogo);

            ProfessionalElementValidation();

            //Hospitality mode
            HospitalityElementValidation();

            VerifyBoldChat();
        }

        protected abstract void AnonymousElementValidation();

        protected abstract void CustomerServiceElementValidation();

        protected abstract void CustomerServiceManagerElementValidation();

        protected abstract void ProfessionalElementValidation();

        protected abstract void HospitalityElementValidation();

        protected abstract void VerifyBoldChat();
    }
}
