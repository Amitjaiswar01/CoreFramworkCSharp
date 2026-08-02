using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.HeaderFooter.T543_T6995_VerifyHeaderFooterForProfessional
{
    //[Collection(LpTraits.BatchGroup.Desktop.HeaderFooter)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.HeaderFooter)]
    public class T543_Windows_VerifyLpHeaderFooterDisplaysTheCorrectElementsPcsi : T543_DesktopBase
    {
        public T543_Windows_VerifyLpHeaderFooterDisplaysTheCorrectElementsPcsi(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_PCSI)]
        public void HeaderElements(string config) => Validate(config);
    }

    //[Collection(LpTraits.BatchGroup.Common.HeaderFooter)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.HeaderFooter)]
    public class T543_Mac_VerifyLpHeaderFooterDisplaysTheCorrectElementsPcsi : T543_DesktopBase
    {
        public T543_Mac_VerifyLpHeaderFooterDisplaysTheCorrectElementsPcsi(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_PCSI)]
        public void HeaderElements(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.HeaderFooter)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.HeaderFooter)]
    public class T543_iPad_VerifyLpHeaderFooterDisplaysTheCorrectElementsPcsi : T543_DesktopBase
    {
        public T543_iPad_VerifyLpHeaderFooterDisplaysTheCorrectElementsPcsi(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_PCSI)]
        public void HeaderElements(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.HeaderFooter)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.HeaderFooter)]
    public class T543_TabletEmulator_VerifyLpHeaderFooterDisplaysTheCorrectElementsPcsi : T543_DesktopBase
    {
        public T543_TabletEmulator_VerifyLpHeaderFooterDisplaysTheCorrectElementsPcsi(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_PCSI)]
        public void HeaderElements(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the Lamps Plus site header displays the correct elements for PCSI user
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5908
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T543
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5908"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T543")]
    public abstract class T543_DesktopBase : TestsBaseDesktop
    {
        protected T543_DesktopBase(ITestOutputHelper output) : base(output) { }

        public void Validate(string config)
        {
            /*Arrange
            User has navigated to the Lamps Plus homepage.
            */
            InitializeFunctionalTest(config);
            Skip.IfNot(DevEnvInformation.DatabaseSymbol == "P", "This test can only be executed against DBCLUST.");

            //Act and Assert Header links
            Assert.Equals(HeaderFooter.GetProLampsLogoLink(), Urls.NormalizeUrl(Urls.HomePageUrl), "Lamps Plus Pro Logo Element not displayed");
            Assert.True(DoCollectionsMatch(Urls.HeaderElementsUrls, HeaderFooter.GetHeaderElementsLinks()),"Header elements link(s) do not match");
            Assert.Equals(HeaderFooter.GetProContactUsPhoneNumber(), HeaderFooter.GetContactPhoneLink().Trim(), "Contact us phone number not same");

            //Act and Assert 'My Account' header links.
            HeaderFooter.OpenMyAccountMenu();
            Assert.True(DoCollectionsMatch(Urls.ProAccountHeaderElementsUrls, HeaderFooter.GetProAccountHeaderElementsLinks()), "'My Account' Header Elements link(s) do not match");

            //Act and Assert 'Inspiration' header links.
            HeaderFooter.OpenInspirationMenu();
            Assert.True(DoCollectionsMatch(Urls.InspirationHeaderElementsUrls, HeaderFooter.GetInspirationHeaderElementsLinks()), "'Inspiration' Header Elements link(s) do not match");

            //Act and Assert 'Saved' header links.
            HeaderFooter.OpenSavedMenu();
            Assert.True(DoCollectionsMatch(Urls.SavedHeaderElementsUrls, HeaderFooter.GetSavedHeaderElementsLinks()), "'Saved' Header Elements link(s) do not match");

            //Act and Assert 'Sale' header links.
            HeaderFooter.OpenSaleMenu();
            Assert.True(DoCollectionsMatch(Urls.SaleHeaderElementsUrlsForPros, HeaderFooter.GetSaleHeaderElementLinksForPros()), "'Sale' Header Elements link(s) do not match");

            //Act and Assert Our Company Links
            Assert.True(DoCollectionsMatch(Urls.ProsAboutUsFooterNavLinksUrls, HeaderFooter.GetProsFooterAboutUsLinks()), "Our Company Nav link(s) do not match");

            //Assert Rate Us - The Feedback modal opens up
            Assert.True(HeaderFooter.IsRateUsModalOpened(), "LP Modal not displayed after clicking Rate Us in the footer");
            HeaderFooter.CloseRateUsModal();

            //Act and Assert Help Center Links
            Assert.True(DoCollectionsMatch(Urls.FooterProsHelpCenterUrls,HeaderFooter.GetFooterProsHelpCenterLinks()),"Help Center Nav link(s) do not match");
            
            //Act and Assert Resources Links
            Assert.True(DoCollectionsMatch(Urls.FooterProsResourcesUrls, HeaderFooter.GetFooterProsResourcesLinks()), "Resources Nav link(s) do not match");

            //Act and Assert Social Media Links
            Assert.True(DoCollectionsMatch(Urls.FooterProsUserSocialUrls, HeaderFooter.GetFooterProsUserSocialLinks()), "Pros Footer Social link(s) do not match");

            //Assert 'Text' phone number for Your Lamps Plus Pros Rep
            Assert.True(HeaderFooter.FootLpProsPhoneNumber == HeaderFooter.GetFooterLpProsPhoneNumber() || HeaderFooter.DefaultProsNumber == HeaderFooter.GetFooterLpProsPhoneNumber(), "Pros phone number not same");

            //Act and Assert Legal Footer Links.
            Assert.True(DoCollectionsMatch(Urls.FooterLegalLinks, HeaderFooter.GetProsFooterLegalLinks()), "Footer legal link(s) do not match");
            Assert.True(DoCollectionsMatch(Urls.CommonFooterLegalUrls, HeaderFooter.GetCommonFooterLegalLinks()), "Common Footer Legal link(s) do not match");
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
