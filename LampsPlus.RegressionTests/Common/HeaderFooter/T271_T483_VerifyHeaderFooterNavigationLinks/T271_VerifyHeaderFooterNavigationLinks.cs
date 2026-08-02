using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.HeaderFooter.T271_T483_VerifyHeaderFooterNavigationLinks
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.HeaderFooter)]
    public class T271_Windows_VerifyHeaderFooterNavigationLinks : T271_DesktopBase
    {
        public T271_Windows_VerifyHeaderFooterNavigationLinks(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyHeaderFooterNavigationLinks(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.HeaderFooter)]
    public class T271_Mac_VerifyHeaderFooterNavigationLinks : T271_DesktopBase
    {
        public T271_Mac_VerifyHeaderFooterNavigationLinks(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void SubscribeShowsThankYouMsg(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.HeaderFooter)]
    public class T271_iPad_VerifyHeaderFooterNavigationLinks : T271_DesktopBase
    {
        public T271_iPad_VerifyHeaderFooterNavigationLinks(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void SubscribeShowsThankYouMsg(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.HeaderFooter)]
    public class T271_TabletEmulator_VerifyHeaderFooterNavigationLinks : T271_DesktopBase
    {
        public T271_TabletEmulator_VerifyHeaderFooterNavigationLinks(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void SubscribeShowsThankYouMsg(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that all Footer links navigate to the correct page when clicked.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9142
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T271
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9142"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T271")]
    public abstract class T271_DesktopBase : TestsBaseDesktop
    {
        protected T271_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: User has navigated to the Lamps Plus homepage.
            InitializeFunctionalTest(config);
            Browser.Navigate(Urls.HomePageUrl);
            Assert.True(Home.IsCurrentPage, "User is not on the Home page.");

            //Act and Assert Header links
            Assert.Equals(HeaderFooter.GetLampsPlusLogoLink(), Urls.NormalizeUrl(Urls.HomePageUrl), "Lamps Plus Logo does not point to the correct URL.");
            Assert.True(DoCollectionsMatch(Urls.HeaderElementsUrls, HeaderFooter.GetHeaderElementsLinks()), "Header elements link(s) do not match");
            Assert.Equals(HeaderFooter.GetLampsPlusContactUsPhoneNumber(), HeaderFooter.GetContactPhoneLink(), "Contact us phone number not same");

            //Act and Assert 'Sign In' header links.
            HeaderFooter.HoverOverSignInLink();
            Assert.True(DoCollectionsMatch(Urls.AccountHeaderElementsUrls, HeaderFooter.GetAccountHeaderElementsLinks()), "'My Account' Header Elements link(s) do not match");
            Assert.True(HeaderFooter.IsSignInButtonVisible(), "Sign In button is not visible in drop-down");

            //Act and Assert 'Stores' header link.
            HeaderFooter.OpenStoresMenu();
            Assert.True(HeaderFooter.IsStoresLinkDropdownVisible(), "Stores link dropdown not displayed");

            //Act and Assert 'Chat' header link.
            var headerChatOption = ProductDetail.IsChatIconEnabled();

            if (headerChatOption)
            {
                HeaderFooter.OpenHeaderChatModal();
                Assert.True(HeaderFooter.IsChatModalWindowVisible(), "Chat container did not display");
                HeaderFooter.CloseChatModal();
            }
            else
            {
                Log.Message("Chat is outside business hours");
            }

            //Act and Assert 'Inspiration' header links.
            HeaderFooter.OpenInspirationMenu();
            Assert.True(DoCollectionsMatch(Urls.InspirationHeaderElementsUrls, HeaderFooter.GetInspirationHeaderElementsLinks()), "'Inspiration' Header Elements link(s) do not match");

            //Act and Assert 'Saved' header links.
            HeaderFooter.OpenSavedMenu();
            Assert.True(DoCollectionsMatch(Urls.SavedHeaderElementsUrls, HeaderFooter.GetSavedHeaderElementsLinks()), "'Saved' Header Elements link(s) do not match");
            
            //Act and Assert 'Sale' header links.
            HeaderFooter.OpenSaleMenu();
            Assert.True(DoCollectionsMatch(Urls.SaleHeaderElementsUrls, HeaderFooter.GetSaleHeaderElementLinks()), "'Sale' Header Elements link(s) do not match");

            //Act and Assert 'Menu' links
            HeaderFooter.OpenChandelierMenu();
            Assert.True(DoCollectionsMatch(Urls.ChandeliersMenuUrls, HeaderFooter.GetChandelierMenuLinks()), "'Chandelier' Menu link(s) do no match");
            HeaderFooter.OpenCeilingLightsMenu();
            Assert.True(DoCollectionsMatch(Urls.CeilingLightsMenuUrl, HeaderFooter.GetCeilingLightsMenuLink()), "'Ceiling Lights' menu link does not match");
            HeaderFooter.OpenLampsMenu();
            Assert.True(DoCollectionsMatch(Urls.LampsMenuUrl, HeaderFooter.GetLampsMenuLink()), "'Table Lamps' menu link does not match");
            HeaderFooter.OpenWallLightsMenu();
            Assert.True(DoCollectionsMatch(Urls.WallLightsMenuUrl, HeaderFooter.GetWallLightsMenuLink()), "'Wall Lights' menu link does not match");

            //Act and Assert Footer elements.
            Assert.True(DoCollectionsMatch(Urls.FooterEmailIconUrl, HeaderFooter.GetFooterEmailIconLink()), "Footer email link does not match");

            var footerChatOption = ProductDetail.IsChatIconEnabled();

            if (footerChatOption)
            {
                HeaderFooter.OpenFooterChatModal();

                Assert.True(HeaderFooter.IsChatModalWindowVisible(), "Chat container did not display");
                HeaderFooter.CloseChatModal();
            }
            else
            {
                Log.Message("Chat is outside business hours");
            }

            //Act and Assert Footer 'About Us' links
            Assert.True(DoCollectionsMatch(Urls.FooterAboutUsProgramsUrls, HeaderFooter.GetFooterAboutUsLinks()), "Footer 'About Us' links do not match");
            
            //Act and Assert Footer 'B2B Programs' links
            Assert.True(DoCollectionsMatch(Urls.FooterB2BProgramsUrls, HeaderFooter.GetFooterB2BProgramsLinks()), "Footer 'B2B Programs' links do not match");

            //Act and Assert Footer 'Customer Service' links
            Assert.True(DoCollectionsMatch(Urls.FooterCustomerServiceUrls, HeaderFooter.GetFooterCustomerServiceLinks()), "Footer 'Customer Service' links do not match");

            //Assert Footer 'Rate Us' feedback modal opens up
            Assert.True(HeaderFooter.IsRateUsModalOpened(), "LP Modal not displayed after clicking Rate Us in the footer");
            HeaderFooter.CloseRateUsModal();

            //Act and Assert Footer 'Resources' links
            Assert.True(DoCollectionsMatch(Urls.FooterResourceUrls, HeaderFooter.GetFooterResourcesLinks()), "Footer 'Resources' links do not match");

            //Act and Assert all social media links
            Assert.True(DoCollectionsMatch(Urls.FooterSocialUrls, HeaderFooter.GetFooterSocialLinks()), "Footer social media links do not match");

            //Act and Assert Footer legal links
            Assert.True(DoCollectionsMatch(Urls.FooterLegalUrls, HeaderFooter.GetFooterLegalLinks()), "Footer legal links do not match");
            Assert.StringContains(HeaderFooter.FreeShippingFreeReturnsDisclaimer(), HeaderFooter.FooterShippingTest(), "Footer shipping disclaimer and expected message do not match.");

            //Act and Assert 'Account' links while Store is in Session
            CookieUtility.EnterStoreInSessionMode();
            HeaderFooter.HoverOverAccountLinkWhileStoreInSession();
            Assert.True(DoCollectionsMatch(Urls.StoreInSessionUrls, HeaderFooter.GetStoreInSessionAccountHeaderLink()), "Header 'Account' links for Store in Session does not match");

            var recentlyViewedSelector = HeaderFooter.GetRecentlyViewedSectionForStoreInSession();
            Assert.False(SpinWait.SpinUntil(() => HeaderFooter.WaitForRecentlyViewedSection(recentlyViewedSelector), TimeSpan.FromSeconds(2)), "Recently Viewed is displayed.");
        }


        private bool DoCollectionsMatch(Dictionary<string, string> dictionaryOne, Dictionary<string, string> dictionaryTwo)
        {
            var mismatch = dictionaryOne.Where(entry => dictionaryTwo[entry.Key] != entry.Value)
                .ToDictionary(entry => entry.Key, entry => entry.Value);

            if (mismatch.Count <= 0) return true;
            foreach (var kvp in mismatch)
            {
                Log.Message($"Mismatch: linkname = {kvp.Key}, link = {kvp.Value}");
            }
            return false;
        }
    }
}
