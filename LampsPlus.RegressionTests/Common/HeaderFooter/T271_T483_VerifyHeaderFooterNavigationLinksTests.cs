using System;
using System.Threading;
using System.Web.UI;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.DesktopTablet.HeaderFooter;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.Common.HeaderFooter
{
    //[Collection(LpTraits.BatchGroup.Common.HeaderFooter)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.HeaderFooter)]
    public class T271_Windows_VerifyLpHeaderFooterDisplaysTheCorrectElementsAnon : T271_DesktopBase
    {
        public T271_Windows_VerifyLpHeaderFooterDisplaysTheCorrectElementsAnon(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyLpHeaderFooterDisplaysTheCorrectElementsAnon(string config) => Validate(config);        
    }


    //[Collection(LpTraits.BatchGroup.Common.HeaderFooter)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.HeaderFooter)]
    public class T271_Mac_VerifyLpHeaderFooterDisplaysTheCorrectElementsAnon : T271_DesktopBase
    {
        public T271_Mac_VerifyLpHeaderFooterDisplaysTheCorrectElementsAnon(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void SubscribeShowsThankYouMsg(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.HeaderFooter)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.HeaderFooter)]
    public class T271_iPad_VerifyLpHeaderFooterDisplaysTheCorrectElementsAnon : T271_DesktopBase
    {
        public T271_iPad_VerifyLpHeaderFooterDisplaysTheCorrectElementsAnon(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void SubscribeShowsThankYouMsg(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.HeaderFooter)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.HeaderFooter)]
    public class T271_TabletEmulator_VerifyLpHeaderFooterDisplaysTheCorrectElementsAnon : T271_DesktopBase
    {
        public T271_TabletEmulator_VerifyLpHeaderFooterDisplaysTheCorrectElementsAnon(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void SubscribeShowsThankYouMsg(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.HeaderFooter)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.HeaderFooter)]
    public class T483_iPhone_VerifyLpHeaderFooterDisplaysTheCorrectElementsAnon : T483_MobileBase
    {
        public T483_iPhone_VerifyLpHeaderFooterDisplaysTheCorrectElementsAnon(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifyLpHeaderFooterDisplaysTheCorrectElementsAnon(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.HeaderFooter)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.HeaderFooter)]
    public class T483_Emulator_VerifyLpHeaderFooterDisplaysTheCorrectElementsAnon : T483_MobileBase
    {
        public T483_Emulator_VerifyLpHeaderFooterDisplaysTheCorrectElementsAnon(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyLpHeaderFooterDisplaysTheCorrectElementsAnon(string config) => Validate(config);
    }

     
    /// <summary>
    /// Verify that all Footer links navigate to the correct page when clicked.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9142
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T271
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9142"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T271")]
    public abstract class T271_DesktopBase : T271_T483_Base
    {
        protected T271_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected override void VerifyHeaderLinks()
        {
            Browser.Wait.ForDomReady();

            VerifyLinkHref(HeaderFooter.LampsLogo, Urls.HomePageUrl);
            VerifyLinkHref(HeaderFooter.OpenBoxLink.FindElement(By.TagName("a")), Urls.LampsPlusOpenBoxUrl);

            Browser.MouseOverOnElement(HeaderFooter.PortalLinks.FindElement(By.Id("pnlLoggedOut")));

            Browser.Wait.IsVisibleElement(By.CssSelector($"{HeaderFooter.PnlLoggedOutId.ToCssIdSelector()} > {HtmlTextWriterTag.Div} > {HtmlTextWriterTag.Div}"));

            VerifyLinkHref(HeaderFooter.HeaderCreateAccountLink, Urls.CreateAccountPageUrl);
            VerifyLinkHref(HeaderFooter.HeaderOrderHistoryLink, Urls.OrderHistoryPageUrl);
            VerifyLinkHref(HeaderFooter.HeaderRecentlyViewedLinkForUnSignedInUser, Urls.RecentlyViewedUrl);
            Assert.Displayed(HeaderFooter.HeaderSignInButton, "Sign In button is not visible in drop-down");

            Browser.MouseOverOnElement(HeaderFooter.StoreLocations);
            VerifyLinkHref(HeaderFooter.StoreLocations, Urls.StoresPageUrl);

            var headerChatOption = ProductDetail.IsWeekday();

            if (headerChatOption)
            {
                Browser.MoveToElement(HeaderFooter.HeaderChatLink); 
                Browser.MouseOverOnElement(HeaderFooter.HeaderChatLink);

                Browser.Wait.IsVisibleElement(By.Id(HeaderFooter.LiveChatLinkId));
                HeaderFooter.LiveChatHeader.Click();
                Browser.Wait.IsVisibleElement(By.ClassName(HeaderFooter.WidgetFloatingWrapperClass));
                Assert.Displayed(HeaderFooter.VirtualAssistant, "Chat container did not display");
                ProductDetail.CloseVirtualAssistant();

                VerifyLinkHref(HeaderFooter.ContactPhoneLink, Urls.ContactUsPageUrl);
            }
            else
            {         
                VerifyLinkHref(HeaderFooter.ContactUsLink, Urls.ContactUsPageUrl);
            }

            Browser.MoveToElement(HeaderFooter.InspirationMenu);
            Browser.MouseOverOnElement(HeaderFooter.InspirationMenu);
            Browser.Wait.IsVisibleElement(By.CssSelector($"{HeaderFooter.PortfolioContainerInspirationId.ToCssIdSelector()} > {HtmlTextWriterTag.Div}"));
            VerifyLinkHref(HeaderFooter.RoomInspirationLink, Urls.RoomInspirationUrl);
            VerifyLinkHref(HeaderFooter.LightingCatalogLink, Urls.LightingCatalogUrl);
            VerifyLinkHref(HeaderFooter.InspirationIdeasAndAdviceLink, Urls.IdeasAdviceUrlProd);

            // Codes purpose to move away from the saved menu to be able to verify the Saved menu after
            Browser.Wait.ForDisplayedElement(HeaderFooter.OpenBoxLink);
            Browser.MoveToElement(HeaderFooter.OpenBoxLink);
            Browser.MouseOverOnElement(HeaderFooter.OpenBoxLink);

            Browser.Wait.ForDisplayedElement(Browser.MouseOverOnElement(HeaderFooter.SavedMenu));
            VerifyLinkHref(HeaderFooter.SavedRooms, Urls.RoomsPageUrl);
            VerifyLinkHref(HeaderFooter.WishListLink, Urls.WishListPageUrl);

            Browser.Wait.ForDomReady();

            // Codes purpose to move away from the saved menu to be able to verify the Sale menu after
            Browser.Wait.ForDisplayedElement(HeaderFooter.OpenBoxLink);
            Browser.MoveToElement(HeaderFooter.OpenBoxLink);
            Browser.MouseOverOnElement(HeaderFooter.OpenBoxLink);

            Browser.MouseOverOnElement(HeaderFooter.SaleMenu);
            Browser.Wait.IsVisibleElement(By.CssSelector($"{HeaderFooter.SaleMenuId.ToCssIdSelector()} > {HtmlTextWriterTag.Div}"));
            Browser.Wait.IsVisibleElement(By.XPath(HeaderFooter.LpSaleNewSectionXpath));

            //Verify All Sale & Banner links
            VerifyLinkHref(HeaderFooter.LpSaleSections(0), Urls.OnSaleUrl); 
            VerifyLinkHref(HeaderFooter.LpSaleSections(1), Urls.OnSaleUrl);

            //Verify Row1 Elements Chandeliers, Ceiling Lights & Outdoor Lighting links
            VerifyLinkHref(HeaderFooter.LpSaleSections(2), Urls.ChandeliersOnSaleUrl);
            VerifyLinkHref(HeaderFooter.LpSaleSections(3), Urls.CeilingLightsOnSaleUrl);
            VerifyLinkHref(HeaderFooter.LpSaleSections(4), Urls.OutdoorLightinsOnSaleUrl);

            //Verify Row2 Elements Table Lamps, Bathroom Lighting & Furniture links
            VerifyLinkHref(HeaderFooter.LpSaleSections(5), Urls.TableLampssOnSaleUrl);
            VerifyLinkHref(HeaderFooter.LpSaleSections(6), Urls.BathroomLightingOnSaleUrl);
            VerifyLinkHref(HeaderFooter.LpSaleSections(7), Urls.FurnituresOnSaleUrl);

            //Verify Row3 Elements Floor Lamps, Ceiling Fans & Mirror links
            VerifyLinkHref(HeaderFooter.LpSaleSections(8), Urls.FloorLampssOnSaleUrl);
            VerifyLinkHref(HeaderFooter.LpSaleSections(9), Urls.CeilingFanOnSaleUrl);
            VerifyLinkHref(HeaderFooter.LpSaleSections(10), Urls.MirrosOnSaleUrl);

            //Verify Row4 Elements Daily Sale, Clearance & Open Box links
            VerifyLinkHref(HeaderFooter.LpSaleSections(11), Urls.LpDailySalesUrl);
            VerifyLinkHref(HeaderFooter.LpSaleSections(12), Urls.ClearanceViewPageUrl);
            VerifyLinkHref(HeaderFooter.LpSaleSections(13), Urls.LampsPlusOpenBoxLinkFromSaleMenuUrl);
        }

        protected override void VerifyMenuLinks()
        {
            Browser.MouseOverOnElement(HeaderFooter.ChandeliersNavLink);
            Browser.Wait.IsVisibleElement(By.CssSelector($"{HeaderFooter.ChandeliersId.ToCssIdSelector()} > {HtmlTextWriterTag.Div}"));
            VerifyLinkHref(HeaderFooter.AllChandeliersLink, Urls.AllChandeliersSortPageUrl);
            VerifyLinkHref(HeaderFooter.ChandeliersDiningLivingRoomLink, Urls.ChandeliersDiningLivingRoomUrl);

            Browser.MouseOverOnElement(HeaderFooter.CeilingLightsNavLink);
            Browser.Wait.IsVisibleElement(By.CssSelector($"{HeaderFooter.CeilingLightingId.ToCssIdSelector()} > {HtmlTextWriterTag.Div}"));
            VerifyLinkHref(HeaderFooter.CeilingLightsFlushMountLink, Urls.CeilingLightsFlushMountUrl);

            Browser.MouseOverOnElement(HeaderFooter.TableAndFloorLampsNavLink);
            Browser.Wait.IsVisibleElement(By.CssSelector($"{HeaderFooter.LampsId.ToCssIdSelector()} > {HtmlTextWriterTag.Div}"));
            VerifyLinkHref(HeaderFooter.AllTableLampsLink, Urls.TableLampsSortPageUrl);

            Browser.MouseOverOnElement(HeaderFooter.WallLightsNavLink);
            Browser.Wait.IsVisibleElement(By.CssSelector($"{HeaderFooter.WallLightsId.ToCssIdSelector()} > {HtmlTextWriterTag.Div}"));
            VerifyLinkHref(HeaderFooter.WallLightsWallLampsLink, Urls.WallLampsPageUrl);
        }

        protected override void VerifyFooterLinks()
        {
            VerifyLinkHref(HeaderFooter.EmailFooterIcon, Urls.ContactUsPageEmailUrl);

            var footerChatOption = ProductDetail.IsWeekday();

            if (footerChatOption) 
            {
                HeaderFooter.FooterChatLink.Click(); 
                Browser.Wait.IsVisibleElement(By.ClassName(HeaderFooter.WidgetFloatingWrapperClass));   
                Assert.Displayed(HeaderFooter.VirtualAssistant, "Chat container did not display");
                ProductDetail.CloseVirtualAssistant();
            }

            VerifyLinkHref(HeaderFooter.FooterLpProsLogoLink, Urls.ProfessionalsPageUrl);
            VerifyLinkHref(HeaderFooter.FooterLpHospitalityLogoLink, Urls.HospitalityPageUrl);

            //Verify All links under About US
            VerifyLinkHref(HeaderFooter.FooterAboutUsLink, Urls.AboutUsPageUrl);
            VerifyLinkHref(HeaderFooter.FooterContact, Urls.ContactUsPageUrl);
            VerifyLinkHref(HeaderFooter.FooterCareersLink, Urls.CareersPageUrl);
            VerifyLinkHref(HeaderFooter.FooterPrivacyPolicyLink, Urls.PrivacyPolicyPageUrl);
            VerifyLinkHref(HeaderFooter.FooterStoreLocatorLink, Urls.StoresPageUrl);
            VerifyLinkHref(HeaderFooter.FooterLightingDesignServicesLink, Urls.LightingDesignServicesPageUrl);
            //VerifyLinkHref(HeaderFooter.FooterStoreCouponsAndOffersLink, Urls.CouponsPageUrl); TODO: Temporary removal as part of LPATCH-13825.
            VerifyLinkHref(HeaderFooter.FooterNewHomeownerSavingsLink, Urls.NewHomeOwnerPageUrl);

            //Verify All links under Customer Service
            VerifyLinkHref(HeaderFooter.FooterCustomerLink, Urls.HelpAndPoliciesPageUrl);
            VerifyLinkHref(HeaderFooter.FooterOrderStatusLink, Urls.OrderHistoryPageUrl);
            VerifyLinkHref(HeaderFooter.FooterReturnPolicyLink, Urls.ReturnsPolicyPageUrl);
            VerifyLinkHref(HeaderFooter.FooterShippingInfoLink, Urls.ShippingPolicyPageUrl);

            VerifyLinkHref(HeaderFooter.FooterAdviceAndTipsLink, Urls.IdeasAdviceUrlProd);
            VerifyLinkHref(HeaderFooter.FooterCatalogsLink, Urls.CatalogsPageUrl);
            VerifyLinkHref(HeaderFooter.FooterGiftCardLink, Urls.GiftCardLandingPageUrl);
            VerifyLinkHref(HeaderFooter.FooterManageAccountLink, Urls.ManageAccountPageUrl);
            VerifyLinkHref(HeaderFooter.FooterAccessibilityLink, Urls.AccessibilityPageUrl);
           
            //Verify All links under Social Media
            VerifyLinkHref(HeaderFooter.FooterPinterestLink, Urls.LpPinterestUrl);
            VerifyLinkHref(HeaderFooter.FooterInstagramLink, Urls.LpInstagramUrl);
            VerifyLinkHref(HeaderFooter.FooterFacebookLink, Urls.LpFacebookUrl);
            VerifyLinkHref(HeaderFooter.FooterTwitterLink, Urls.LpTwitterUrl);
            VerifyLinkHref(HeaderFooter.FooterYoutubeLink, Urls.LpYouTubeUrl);
            VerifyLinkHref(HeaderFooter.FooterTiktokLink, Urls.LpTiktokUrl);

            //Verify links at the bottom of footer.
            VerifyLinkHref(HeaderFooter.FooterHomeLink, Urls.HomePageUrl);
            VerifyLinkHref(HeaderFooter.FooterTermsOfUseLink, Urls.TermsOfUsePageUrl);
            VerifyLinkHref(HeaderFooter.FooterAccessibilityLink, Urls.AccessibilityPageUrl);
            VerifyLinkHref(HeaderFooter.FooterSiteMapLink, Urls.SiteMapPageUrl);
            VerifyLinkHref(HeaderFooter.CCPAPolicy, Urls.SeeOurPolicyUrl);
            VerifyLinkHref(HeaderFooter.FooterPrivacyPolicyLink, Urls.PrivacyPolicyPageUrl);
            
            HeaderFooter.LpFooterRateUs.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.LpModalId.ToCssIdSelector()));

            Assert.Displayed(HeaderFooter.RateUsContainer, "Rate Us modal is not displayed.");
            GlobalLocators.LpModalCloseElement.Click();
            Browser.Wait.UntilElementUnloads(GlobalLocators.LpModalCloseElement);

            VerifyKioskUserDropdownLinks();
        }

        private void VerifyKioskUserDropdownLinks()
        {
            CookieUtility.EnterStoreInSessionMode();
            Browser.ScrollToTopOfWindow();
            Browser.MouseOverOnElement(HeaderFooter.PortalLinks.FindElement(By.Id("pnlLoggedOut")));

            Browser.Wait.IsVisibleElement(By.CssSelector($"{HeaderFooter.PnlLoggedOutId.ToCssIdSelector()} > {HtmlTextWriterTag.Div} > {HtmlTextWriterTag.Div}"));

            var recentlyViewedSelector = $"{HeaderFooter.AccountSignInSmallId.ToCssIdSelector()} {HtmlTextWriterTag.Ul.ToDirectChildSelector()} {HtmlTextWriterTag.Li.ToDirectChildSelector().ToNthChildSelector(3)}";

            VerifyLinkHref(HeaderFooter.HeaderCreateAccountLink, Urls.CreateAccountPageUrl);
            Assert.False(SpinWait.SpinUntil(() => Browser.Locate.DoesElementExistImmediately(recentlyViewedSelector), TimeSpan.FromSeconds(2)), "Recently Viewed is displayed.");
        }
    }
    

    /// <summary>
    /// Verify that all Footer links navigate to the correct page when clicked.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5320
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T483
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5320"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T483")]
    public abstract class T483_MobileBase : T271_T483_Base
    {
        protected T483_MobileBase(ITestOutputHelper output) : base(output) { }

        protected override void VerifyHeaderLinks()
        {
            Browser.Navigate(Urls.HomePageUrl);
            Browser.Wait.ForDomReady();
            HeaderFooter.HamburgerMenu.Click(); 
            Assert.Displayed(HeaderFooter.HamburgerSubList, "The Category list is not displayed.");
            
            HeaderFooter.SearchIcon.Click();
            Assert.Displayed(Search.SearchField, "The Search field is not displayed.");

            HeaderFooter.HamburgerMenu.Click();
            Assert.Displayed(HeaderFooter.SignInHamburger, "Sign In button is not present in Top Category inside Hamburger");
            Assert.StringContains(HeaderFooter.SignInCreateAccountHamburger.Text, "Create Account", "Create Account is not present in Top Category inside Hamburger");
            HeaderFooter.HamburgerMenu.Click();

            VerifyLinkHref(HeaderFooter.CartIcon, Urls.CartOverviewPageUrl);

            VerifyLinkHref(HeaderFooter.ProLampsLogo, Urls.HomePageUrl);
        }

        private void WaitForDrawerAnimation()
        {
            Browser.Wait.ForElementToStopAnimating(HeaderFooter.HamburgerSubList);
        }

        protected override void VerifyMenuLinks()
        {
            HeaderFooter.HamburgerMenu.Click();
            Browser.Wait.ForDomReady();

            Browser.Wait.ForDisplayedElement(HeaderFooter.ChandeliersNavLink);
            Browser.ScrollIntoView(HeaderFooter.ChandeliersNavLink);
            HeaderFooter.ChandeliersNavLink.Click();
            WaitForDrawerAnimation();

            VerifyLinkHref(HeaderFooter.AllChandeliersLink, Urls.AllChandeliersSortPageUrl);
            VerifyLinkHref(HeaderFooter.ChandeliersDiningLivingRoomLink, Urls.ChandeliersDiningLivingRoomUrl);

            Browser.ScrollIntoView(HeaderFooter.CeilingLightsNavLink);
            HeaderFooter.CeilingLightsNavLink.Click();
            WaitForDrawerAnimation();
            VerifyLinkHref(HeaderFooter.CeilingLightsFlushMountLink, Urls.CeilingLightsFlushMountUrl);

            Browser.ScrollIntoView(HeaderFooter.LampsAndShadesNavLink);
            HeaderFooter.LampsAndShadesNavLink.Click();
            WaitForDrawerAnimation();
            VerifyLinkHref(HeaderFooter.AllTableLampsLink, Urls.TableLampsUrl);

            Browser.ScrollIntoView(HeaderFooter.WallLightsNavLink);
            HeaderFooter.WallLightsNavLink.Click();
            WaitForDrawerAnimation();
            VerifyLinkHref(HeaderFooter.WallLightsWallLampsLink, Urls.WallLampsPageUrl);

            HeaderFooter.HamburgerMenu.Click();
            Browser.Wait.ForDomReady();
        }

        protected override void VerifyFooterLinks()
        {
            VerifyLinkHref(HeaderFooter.FooterCallLink, "tel:18887390201");

            //Footer chat functionality is tested in T239.

            VerifyLinkHref(HeaderFooter.FooterStoreLocatorLink, Urls.StoresMobilePageUrl);
            VerifyLinkHref(HeaderFooter.FooterCatalogsLink, Urls.CatalogsPageUrl);
            VerifyLinkHref(HeaderFooter.FooterAboutUsLink, Urls.AboutUsMobilePageUrl);
            VerifyLinkHref(HeaderFooter.FooterLpProsLink, Urls.ProfessionalsPageUrl);
            VerifyLinkHref(HeaderFooter.FooterOrderStatusLink, Urls.OrderHistoryMobilePageUrl);
            VerifyLinkHref(HeaderFooter.FooterReturnPolicyLink, Urls.ReturnsPolicyPageUrl);
            VerifyLinkHref(HeaderFooter.FooterHelpLink, Urls.HelpAndPoliciesPageUrl);
            VerifyLinkHref(HeaderFooter.FooterLpHospitalityLink, Urls.HospitalityPageUrl);

            VerifyLinkHref(HeaderFooter.FooterCareersLink, Urls.CareersPageUrl);
            VerifyLinkHref(HeaderFooter.FooterRateUsLink, Urls.RateUsMobileUrl);
            VerifyLinkHref(HeaderFooter.FooterAccessibilityLink, Urls.AccessibilityPageUrl);
            VerifyLinkHref(HeaderFooter.FooterPrivacyPolicyLink, Urls.PrivacyPolicyPageUrl);
            VerifyLinkHref(HeaderFooter.FooterTermsOfUseLink, Urls.TermsOfUsePageUrl);
            VerifyLinkHref(HeaderFooter.FooterSiteMapLink, Urls.SiteMapPageUrl);
            
            VerifyLinkHref(HeaderFooter.FooterPinterestLink, Urls.LpPinterestUrl);
            VerifyLinkHref(HeaderFooter.FooterInstagramLink, Urls.LpInstagramMobileUrl);
            VerifyLinkHref(HeaderFooter.FooterFacebookLink, Urls.LpFacebookUrl);
            VerifyLinkHref(HeaderFooter.FooterTwitterLink, Urls.LpTwitterUrl);
            VerifyLinkHref(HeaderFooter.FooterYoutubeLink, Urls.LpYouTubeUrl);
                       
            VerifyLinkHref(HeaderFooter.FooterDoNotSellMyInfoLink, Urls.DoNotSellMyInfoUrl);
            VerifyLinkHref(HeaderFooter.FooterSeeOurPolicyLink, Urls.SeeOurPolicyUrl);
            VerifyLinkHref(HeaderFooter.FooterCaTransparencyActLink, Urls.CaDisclosureTransparencyPageUrl);
        }
    }


    public abstract class T271_T483_Base : HeaderFooterTestsBase
    {
        protected T271_T483_Base(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            InitializeFramework(config);

            VerifyHeaderLinks();
            VerifyMenuLinks();
            VerifyFooterLinks();
        }

        protected abstract void VerifyHeaderLinks();

        protected abstract void VerifyMenuLinks();

        protected abstract void VerifyFooterLinks();
    }
}
