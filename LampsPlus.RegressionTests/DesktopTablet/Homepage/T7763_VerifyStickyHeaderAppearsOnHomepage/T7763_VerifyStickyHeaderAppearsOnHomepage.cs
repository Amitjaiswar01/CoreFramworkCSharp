using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.DesktopTablet.Homepage.T7763_VerifyStickyHeaderAppearsOnHomepage
{
    //[Collection(LpTraits.BatchGroup.Desktop.Homepage)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Homepage)]
    public class T7763_Windows_VerifyStickyHeaderAppearsOnHomePage : T7763_DesktopBase
    {
        public T7763_Windows_VerifyStickyHeaderAppearsOnHomePage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void StickyHeaderAppearsOnHomePage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.Homepage)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Homepage)]
    public class T7763_Mac_VerifyStickyHeaderAppearsOnHomePage : T7763_DesktopBase
    {
        public T7763_Mac_VerifyStickyHeaderAppearsOnHomePage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void StickyHeaderAppearsOnHomePage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.Homepage)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Homepage)]
    public class T7763_iPad_VerifyStickyHeaderAppearsOnHomePage : T7763_DesktopBase
    {
        public T7763_iPad_VerifyStickyHeaderAppearsOnHomePage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void StickyHeaderAppearsOnHomePage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.Homepage)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Homepage)]
    public class T7763_TabletEmulator_VerifyStickyHeaderAppearsOnHomePage : T7763_DesktopBase
    {
        public T7763_TabletEmulator_VerifyStickyHeaderAppearsOnHomePage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void StickyHeaderAppearsOnHomePage(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the Sticky Header appears on the homepage.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9950
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7763
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9950"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7763")]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Homepage)]
    public abstract class T7763_DesktopBase : TestsBaseDesktop
    {
        protected T7763_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrangement: User is on the Homepage.
            InitializeFunctionalTest(config);

            //Act: Scroll to the footer of the home page.
            HeaderFooter.ScrollToFooter();

            //Assert: Verify a sticky nav appears at the top of the page.
            Assert.Displayed(Home.GetHomepageStickyHeader(), "Sticky Header Not Displayed on Homepage ");

            //Act: Hover over the 'Chandeliers' menu link in the sticky nav.
            HeaderFooter.HoverOverChandelierStickyNavigation();

            //Assert: Verify that the Chandeliers menu appears.
            Assert.Displayed(Home.GetChandelierMenu(), "Chandelier Menu not displayed");

            //Act: Click on the Search icon in the sticky nav, enter in a search for 'table lamps', and execute the search
            HeaderFooterWorkflow.SearchExecution();

            //Assert: The URL has the search term in it (e.g. table-lamps).
            var searchFilterUrl = Browser.PageUrl;
            var trimUrl = "s_table-lamps";
            Assert.StringContains(searchFilterUrl, trimUrl, "URL does not contain the search term");

            //Assert: The breadcrumbs have the search term in it (e.g. 'Table Lamps').
            var searchTerm = "table lamps";
            var searchText = Search.GetSearchText(searchTerm);
            var breadcrumbText = Sort.GetBreadCrumbText();
            Assert.True(breadcrumbText.Contains(searchText), "Last Breadcrumb is not matching");
        }
    }
}
