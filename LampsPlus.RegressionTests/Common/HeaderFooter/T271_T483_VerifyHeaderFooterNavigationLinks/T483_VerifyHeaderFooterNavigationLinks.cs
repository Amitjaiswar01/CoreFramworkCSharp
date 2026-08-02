using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.HeaderFooter.T271_T483_VerifyHeaderFooterNavigationLinks
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.HeaderFooter)]
    public class T483_iPhone_VerifyLpHeaderFooterDisplaysTheCorrectElementsAnon : T483_MobileBase
    {
        public T483_iPhone_VerifyLpHeaderFooterDisplaysTheCorrectElementsAnon(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifyLpHeaderFooterDisplaysTheCorrectElementsAnon(string config) => Validate(config);
    }


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
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9943
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T483
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9943"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T483")]
    public abstract class T483_MobileBase : TestsBaseMobile
    {
        protected T483_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: User has navigated to the Lamps Plus homepage.
            InitializeFunctionalTest(config);
            Browser.Navigate(Urls.HomePageUrl);
            Browser.Wait.ForDomReady();
            Assert.True(Home.IsCurrentPage, "User is not on the Home page.");

            //Act and Assert Verify Header links.
            HeaderFooter.ToggleHamburgerMenu();
            Assert.Displayed(HeaderFooter.GetHamburgerMenuSublist(), "The Category list is not displayed.");

            HeaderFooter.ToggleSearchIcon();
            Assert.Displayed(HeaderFooter.GetSearchField(), "The Search field is not displayed.");

            Assert.True(DoCollectionsMatch(Urls.MobileHeaderUrls, HeaderFooter.GetHeaderElementsLinks()), "Mobile header links do not match.");

            //Act and Assert Hamburger menu links.
            HeaderFooter.ToggleHamburgerMenu();
            Assert.True(HeaderFooter.IsSignInButtonVisible(), "Sign In button is not present in Top Category inside Hamburger");
            Assert.StringContains(HeaderFooter.GetSignInText(), HeaderFooter.GetCreateAccountString(), "Create Account is not present in Top Category inside Hamburger");

            //Act and Assert hamburger menu categories
            Assert.True(DoCollectionsMatch(Urls.GlobalNavMobileUrls, HeaderFooter.GetGlobalNavLinks()), "Hamburger menu Global Nav elements link(s) do not match");
            HeaderFooter.ToggleHamburgerMenu();

            //Act and Assert footer links
            Assert.Displayed(HeaderFooter.GetFooterEmailField(), "Email field in footer is not displayed");

            var footerChatOption = ProductDetail.IsChatIconEnabled();

            if (footerChatOption)
            {
                Assert.Equals(HeaderFooter.GetCallButtonPhoneNumber(),HeaderFooter.GetFooterCallButton(), "Call button phone number does not match");
                HeaderFooter.OpenFooterChatModal();

                Assert.True(HeaderFooter.IsChatModalWindowVisible(), "Chat container did not display");
                HeaderFooter.CloseChatModal();
            }
            else
            {
                Log.Message("Chat and Customer Service is outside business hours");
            }

            Assert.True(DoCollectionsMatch(Urls.MobileFooterNavLinksUrls, HeaderFooter.GetCommonFooterNavLinksLinks()), "Footer Nav links do not match");
            Assert.True(DoCollectionsMatch(Urls.MobileFooterSocialUrls, HeaderFooter.GetFooterSocialLinks()), "Footer Nav links do not match");
            Assert.True(DoCollectionsMatch(Urls.MobileFooterLegalUrls, HeaderFooter.GetCommonFooterLegalLinks()), "Footer Legal links do not match");
            Assert.True(DoCollectionsMatch(Urls.FooterMobileUrls, HeaderFooter.GetFooterLinks()), "Footer link(s) do not match");
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
