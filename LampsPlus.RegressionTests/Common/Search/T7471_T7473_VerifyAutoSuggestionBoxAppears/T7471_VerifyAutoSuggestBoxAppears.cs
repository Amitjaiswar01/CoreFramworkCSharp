using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Search.T7471_T7473_VerifyAutoSuggestionBoxAppears
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7471_Windows_VerifyAutoSuggestBoxAppears : T7471_DesktopBase
    {
        public T7471_Windows_VerifyAutoSuggestBoxAppears(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyAutoSuggestBoxAppears(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7471_Mac_VerifyAutoSuggestBoxAppears : T7471_DesktopBase
    {
        public T7471_Mac_VerifyAutoSuggestBoxAppears(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyAutoSuggestBoxAppears(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7471_iPad_VerifyAutoSuggestBoxAppears : T7471_DesktopBase
    {
        public T7471_iPad_VerifyAutoSuggestBoxAppears(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyAutoSuggestBoxAppears(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7471_TabletEmulator_VerifyAutoSuggestBoxAppears : T7471_DesktopBase
    {
        public T7471_TabletEmulator_VerifyAutoSuggestBoxAppears(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyAutoSuggestBoxAppears(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the auto-suggest box appears with search options relevant to the search term and that the user is directed to the correct page after selecting an option.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10054
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7471
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10054"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7471")]
    public abstract class T7471_DesktopBase : TestsBaseDesktop
    {
        protected T7471_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected virtual void Validate(string config)
        {
            //Arrange: User is on the Lamps Plus home page.
            InitializeFunctionalTest(config);
            Assert.True(Home.IsCurrentPage, "User is not on the Home page.");

            SearchFindTextAndVerifyLink("lamp", "lamp shades", "https://www.lampsplus.com/products/s_lamp-shades/?s=1");
            SearchFindTextAndVerifyLink("bathroom", "bathroom vanity lights", "https://www.lampsplus.com/products/s_bathroom-vanity-lights/?s=1");
            SearchFindTextAndVerifyLink("wall", "wall sconces", "https://www.lampsplus.com/products/s_wall-sconces/?s=1");
            SearchFindTextAndVerifyLink("table", "table lamps", "https://www.lampsplus.com/products/s_table-lamps/?s=1");
            SearchFindTextAndVerifyLink("floor", "floor lamps", "https://www.lampsplus.com/products/s_floor-lamps/?s=1");
        }

        private void SearchFindTextAndVerifyLink(string searchText, string textToFind, string urlToVerify)
        {
            //Act: Enter search term into the search field.
            Search.EnterSearchTerm(searchText);
            var linkToClick = Search.GetAutoSuggestDropDownResults(textToFind);

            //Assert: The Search term is present in the list of options in the AutoSuggest box.
            Assert.True(linkToClick != null, $"The following is not displayed in the list of options: {textToFind}.");

            //Act: Click on the search term link in the AutoSuggest box.
            if (linkToClick == null) return;
            Search.SelectOptionFromSearchDropdown(linkToClick);
            Sort.WaitForH1ToHaveSearchTerm(searchText);

            //Assert: The user is directed to the correct Sort page.
            Assert.Equals(urlToVerify, Browser.PageUrl, $"The user is not directed to the following page: {urlToVerify}");
            Browser.Navigate(Urls.HomePageUrl);
            Assert.True(Home.IsCurrentPage, "User is not on the Home page.");
        }
    }
}
