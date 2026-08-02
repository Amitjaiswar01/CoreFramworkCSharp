using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Environment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.HeaderFooter.T7079_VerifyHeaderFooterLinksForHospitality
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.HeaderFooter)]
    public class T7079_Windows_VerifyHospitalityHeaderLinks : T7079_DesktopBase
    {
        public T7079_Windows_VerifyHospitalityHeaderLinks(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_HCSI)]
        public void HospitalityHeaderLinks(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.HeaderFooter)]
    public class T7079_Mac_VerifyHospitalityHeaderLinks : T7079_DesktopBase
    {
        public T7079_Mac_VerifyHospitalityHeaderLinks(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_HCSI)]
        public void HospitalityHeaderLinks(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.HeaderFooter)]
    public class T7079_iPad_VerifyHospitalityHeaderLinks : T7079_DesktopBase
    {
        public T7079_iPad_VerifyHospitalityHeaderLinks(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_HCSI)]
        public void HospitalityHeaderLinks(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.HeaderFooter)]
    public class T7079_TabletEmulator_VerifyHospitalityHeaderLinks : T7079_DesktopBase
    {
        public T7079_TabletEmulator_VerifyHospitalityHeaderLinks(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_HCSI)]
        public void HospitalityHeaderLinks(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that all Header and Footer links point to the correct URLs for Hospitality Users.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9947
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7079
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9947"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7079")]
    public abstract class T7079_DesktopBase : TestsBaseDesktop
    {
        protected T7079_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange User has navigated to the Lamps Plus homepage.
            var setup = new TestSetup(config) { ShoppingCartConfig = { EmptyOnSetup = false, EmptyOnTearDown = false } };
            InitializeFunctionalTest(config, setup: setup);

            //Assert Lamps Plus Hospitality logo is present.
            Assert.Equals(Urls.NormalizeUrl(Urls.HomePageUrl), HeaderFooter.GetLampsPlusLogoLink(), "Lamps Plus Hospitality logo does not point to the correct URL.");

            //Act and Assert 'My Account' header links.
            HeaderFooter.OpenMyAccountMenu();
            Assert.True(DoCollectionsMatch(Urls.HospitalityAccountHeaderElementsUrls, HeaderFooter.GetHospitalityAccountHeaderElementsLinks()), "'My Account' Header Elements link(s) do not match");

            //Assert Phone Number points to the correct URL.
            Assert.Equals(Urls.ContactUsPageUrl, HeaderFooter.GetHospitalityContactPhoneLink(), "Contact us link is not the same");

            //Act and Assert 'Saved' header links point to the correct URLs.
            HeaderFooter.OpenSavedMenu();
            Assert.Equals(Urls.WishListPageUrl, HeaderFooter.GetWishListLink(), "'Saved' Header Elements link(s) do not match");

            //Act and Assert the Nightstand Lamps link points to the correct URL.
            HeaderFooter.OpenHospitalityLampsMenu();
            Assert.Equals(Urls.HospitalityNightstandLampsPageUrl, HeaderFooter.GetHospitalityLampsLink(), "Night Stand Lamp link does not point to the correct URL.");

            //Act and Assert the Best Value link points to the correct URL.
            HeaderFooter.OpenHotelProgramsMenu();
            Assert.Equals(Urls.HotelBrandProgramsBestValuePageUrl, HeaderFooter.GetHospitalityBestValueLink(), "Best Value link does not point to the correct URL.");

            //Assert all links under OUR COMPANY point to the correct URLs.
            Assert.True(DoCollectionsMatch(Urls.FooterHospitalityOurCompanyProgramsUrls, HeaderFooter.GetHospitalityOurCompanyElementsLinks()), "Our Company links do not match URLs.");

            //Assert all links under HELP CENTER point to the correct URLs.
            Assert.True(DoCollectionsMatch(Urls.FooterHospitalityHelpCenterProgramsUrls, HeaderFooter.GetHospitalityHelpCenterElementsLinks()), "Help Center links do not match URLs.");

            //Assert all links under RESOURCES point to the correct URLs.
            Assert.True(DoCollectionsMatch(Urls.FooterHospitalityResourcesProgramsUrls, HeaderFooter.GetHospitalityResourcesElementsLinks()), "Resources links do not match URLs.");

            //Assert Footer 'Rate Us' feedback modal opens up
            Assert.True(HeaderFooter.IsRateUsModalOpened(), "LP Modal not displayed after clicking Rate Us in the footer");
            HeaderFooter.CloseRateUsModal();

            //Assert the EMAIL link points to the correct URL.
            Assert.True(DoCollectionsMatch(Urls.FooterEmailIconUrl, HeaderFooter.GetFooterEmailIconLink()), "Footer email link does not match");

            //Assert the links at the bottom of footer point to the correct URLs.
            Assert.Equals(Urls.HomePageUrl, HeaderFooter.GetFooterHomePageLink(), "Nations Largest Lighting Retailer link does not point to correct URL.");
            Assert.True(DoCollectionsMatch(Urls.FooterHospitalityLegalUrls, HeaderFooter.GetFooterHospitalityLegalLinks()), "Footer legal links do not match");
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
