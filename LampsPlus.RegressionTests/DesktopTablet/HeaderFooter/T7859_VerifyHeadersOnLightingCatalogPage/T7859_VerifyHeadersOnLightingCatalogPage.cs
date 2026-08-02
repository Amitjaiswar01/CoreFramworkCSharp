using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.HeaderFooter.T7859_VerifyHeadersOnLightingCatalogPage
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.HeaderFooter)]
    public class T7859_Windows_VerifyCollapsedHeaderFunctionalityLightingCatalogPage : T7859_DesktopBase
    {
        public T7859_Windows_VerifyCollapsedHeaderFunctionalityLightingCatalogPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void CollapsedHeaderFunctionality(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.HeaderFooter)]
    public class T7859_Mac_VerifyCollapsedHeaderFunctionalityLightingCatalogPage : T7859_DesktopBase
    {
        public T7859_Mac_VerifyCollapsedHeaderFunctionalityLightingCatalogPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void CollapsedHeaderFunctionality(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.HeaderFooter)]
    public class T7859_iPad_VerifyCollapsedHeaderFunctionalityLightingCatalogPage : T7859_DesktopBase
    {
        public T7859_iPad_VerifyCollapsedHeaderFunctionalityLightingCatalogPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void CollapsedHeaderFunctionality(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.HeaderFooter)]
    public class T7859_TabletEmulator_VerifyCollapsedHeaderFunctionalityLightingCatalogPage : T7859_DesktopBase
    {
        public T7859_TabletEmulator_VerifyCollapsedHeaderFunctionalityLightingCatalogPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void CollapsedHeaderFunctionality(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that Collapsed Header Links Point to the Correct URLs on "Lamps Plus Lighting Sale Catalog" Page
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10683
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7859
    /// </summary>
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10683"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7859")]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    public abstract class T7859_DesktopBase : TestsBaseDesktop
    {
        protected T7859_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: Navigate to Lighting Catalog Page.
            InitializeFunctionalTest(config, Urls.LightingCatalogSaleUrl);

            //Act and Assert the Lamps Plus logo points to the correct URL.
            Assert.Equals(HeaderFooter.GetLampsPlusLogoLink(), Urls.NormalizeUrl(Urls.HomePageUrl), "Lamps Plus Logo does not point to the correct URL.");

            //Act and Assert 'Menu' links
            HeaderFooter.OpenChandelierMenu();
            Assert.True(DoCollectionsMatch(Urls.ChandeliersMenuUrls, HeaderFooter.GetChandelierMenuLinks()), "'Chandelier' Menu link(s) do no match");
            HeaderFooter.OpenCeilingLightsMenu();
            Assert.True(DoCollectionsMatch(Urls.CeilingLightsMenuUrl, HeaderFooter.GetCeilingLightsMenuLink()), "'Ceiling Lights' menu link does not match");
            HeaderFooter.OpenLampsMenu();
            Assert.True(DoCollectionsMatch(Urls.LampsMenuUrl, HeaderFooter.GetLampsMenuLink()), "'Table Lamps' menu link does not match");
            HeaderFooter.OpenWallLightsMenu();
            Assert.True(DoCollectionsMatch(Urls.WallLightsMenuUrl, HeaderFooter.GetWallLightsMenuLink()), "'Wall Lights' menu link does not match");
            
            //Act and Assert 'Saved' header links.
            HeaderFooter.OpenSavedMenu();
            Assert.True(DoCollectionsMatch(Urls.SavedHeaderElementsUrls, HeaderFooter.GetSavedHeaderElementsLinks()), "'Saved' Header Elements link(s) do not match");

            //Act and Assert Cart icon points to the correct URL.
            Assert.Equals(Urls.CartOverviewPageUrl, HeaderFooter.GetCartIconLink(), "cartLink is not correct");
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