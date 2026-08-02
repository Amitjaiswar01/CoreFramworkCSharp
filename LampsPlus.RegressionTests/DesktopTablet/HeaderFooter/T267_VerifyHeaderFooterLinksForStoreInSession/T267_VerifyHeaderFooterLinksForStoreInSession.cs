using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Environment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.HeaderFooter.T267_VerifyHeaderFooterLinksForStoreInSession
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.HeaderFooter)]
    public class T267_Windows_VerifyHeaderWhenStoreInSession : T267_DesktopBase
    {
        public T267_Windows_VerifyHeaderWhenStoreInSession(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_ESI)]
        public void HeaderFooterLinks(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.HeaderFooter)]
    public class T267_Mac_VerifyHeaderWhenStoreInSession : T267_DesktopBase
    {
        public T267_Mac_VerifyHeaderWhenStoreInSession(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SIS_ESI)]
        public void HeaderFooterLinks(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.HeaderFooter)]
    public class T267_iPad_VerifyHeaderWhenStoreInSession : T267_DesktopBase
    {
        public T267_iPad_VerifyHeaderWhenStoreInSession(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SIS_ESI)]
        public void HeaderFooterLinks(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.HeaderFooter)]
    public class T267_TabletEmulator_VerifyHeaderWhenStoreInSession : T267_DesktopBase
    {
        public T267_TabletEmulator_VerifyHeaderWhenStoreInSession(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SIS_ESI)]
        public void HeaderFooterLinks(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that correct links appear in the site header and footer when Store Is In Session.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9945
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T267
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9945"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T267")]
    public abstract class T267_DesktopBase : TestsBaseDesktop
    {
        protected T267_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: User is signed in as an Employee with a store modifier and is on the home page.
            var setup = new TestSetup(config, Urls.HomePageUrl, true) { AccountConfig = { StoreInSessionStoreNumber = "12" } };
            InitializeFunctionalTest(config, setup: setup);

            //Act and Assert Header links
            Assert.Equals(HeaderFooter.GetLampsPlusLogoLink(), Urls.NormalizeUrl(Urls.HomePageUrl), "Lamps Plus Logo does not point to the correct URL.");
            HeaderFooter.OpenSessionMenu();
            Assert.True(HeaderFooter.IsSessionMenuVisible(), "Session Menu not displayed");
            Assert.Equals(Urls.ContactUsPageUrl, HeaderFooter.GetStoreInSessionPhoneNumberLink(), "Contact us link is not the same");

            //Act and Assert account header links.
            HeaderFooter.OpenAccountMenuForStoreInSession();
            Assert.True(DoCollectionsMatch(Urls.AccountHeaderElementsForStoreInSessionUrls, HeaderFooter.GetAccountHeaderElementsForStoreInSessionLinks()), "Account header links do not match.");

            //Act and Assert Stores drop-down
            HeaderFooter.OpenStoresMenu();
            Assert.True(HeaderFooter.IsStoresLinkDropdownVisible(), "Stores link dropdown not displayed");

            //Act and Assert 'Inspiration' header links.
            HeaderFooter.OpenInspirationMenu();
            Assert.True(DoCollectionsMatch(Urls.InspirationHeaderElementsUrls, HeaderFooter.GetInspirationHeaderElementsLinks()), "'Inspiration' Header Elements link(s) do not match");

            //Act and Assert 'Saved' header links.
            HeaderFooter.OpenSavedMenu();
            Assert.True(DoCollectionsMatch(Urls.SavedHeaderElementsUrls, HeaderFooter.GetSavedHeaderElementsLinks()), "'Saved' Header Elements link(s) do not match");

            //Act and Assert 'Sale' header links.
            HeaderFooter.OpenSaleMenu();
            Assert.True(DoCollectionsMatch(Urls.SaleHeaderElementsUrlsForStoreInSession, HeaderFooter.GetSaleHeaderElementLinksForStoreInSession()), "'Sale' Header Elements link(s) do not match");

            //Act and Assert Store number field and Open Box link.
            Assert.True(HeaderFooter.IsStoreNumberFieldVisible(), "Store Number not displayed.");
            Assert.False(HeaderFooter.IsOpenBoxLinkVisible(), "Open Box icon is visible.");

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

            //Act and Assert Footer 'About Us' links
            Assert.True(DoCollectionsMatch(Urls.FooterAboutUsStoreInSessionUrls, HeaderFooter.GetFooterStoreInSessionAboutUsLinks()), "Footer 'About Us' links do not match");

            //Act and Assert Footer 'B2B Programs' links
            Assert.True(DoCollectionsMatch(Urls.FooterB2BProgramsUrls, HeaderFooter.GetFooterB2BProgramsLinks()), "Footer 'B2B Programs' links do not match");

            //Act and Assert Footer 'Customer Service' links
            Assert.True(DoCollectionsMatch(Urls.FooterCustomerServiceUrls, HeaderFooter.GetFooterCustomerServiceLinks()), "Footer 'Customer Service' links do not match");

            //Assert Footer 'Rate Us' feedback modal opens up
            Assert.True(HeaderFooter.IsRateUsModalOpened(), "LP Modal not displayed after clicking Rate Us in the footer");
            HeaderFooter.CloseRateUsModal();

            //Act and Assert Footer 'Resources' links
            Assert.True(DoCollectionsMatch(Urls.FooterResourceUrls, HeaderFooter.GetFooterResourcesLinks()), "Footer 'Resources' links do not match");

            //Act and Assert Footer legal links
            Assert.Equals(Urls.HomePageUrl, HeaderFooter.GetFooterHomePageLink(), "Nations Largest Lighting Retailer link does not point to correct URL.");
            Assert.True(DoCollectionsMatch(Urls.FooterLegalUrls, HeaderFooter.GetFooterLegalLinks()), "Footer legal links do not match");
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
