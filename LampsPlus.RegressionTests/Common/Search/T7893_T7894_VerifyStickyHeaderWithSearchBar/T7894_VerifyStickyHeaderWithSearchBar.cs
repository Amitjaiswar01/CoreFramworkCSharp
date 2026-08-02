using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Search.T7893_T7894_VerifyStickyHeaderWithSearchBar
{
	//[Collection(LpTraits.BatchGroup.Mobile.Search)]
	[Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Search)]
	public class T7894_iPhone_VerifyStickyHeaderWithSearchBar : T7894_MobileBase
	{
		public T7894_iPhone_VerifyStickyHeaderWithSearchBar(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
		[SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifyStickyHeaderSearchBar(string config) => Validate(config);
	}


	//[Collection(LpTraits.BatchGroup.Common.Search)]
	[Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
	public class T7894_Emulator_VerifyStickyHeaderWithSearchBar : T7894_MobileBase
	{
		public T7894_Emulator_VerifyStickyHeaderWithSearchBar(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
		[SkippableTheory]
		[InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
		public void VerifyStickyHeaderSearchBar(string config) => Validate(config);
	}

	/// <summary>
	/// Verify Sticky Header with a Search Bar
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10343
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7894
	/// </summary>
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10343"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7894")]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
	public class T7894_MobileBase : TestsBaseMobile
	{
		protected T7894_MobileBase(ITestOutputHelper output) : base(output) { }

		protected virtual void Validate(string config)
		{
			// Arrange : Navigate to homepage
			InitializeFunctionalTest(config);
            Browser.Navigate(url: Urls.HomePageUrl);

            /* Act : 
			Get the random search term
            Execute the search on the site
            */
			var searchTerm = Search.GetRandomSearchTerm();
			Search.EnterSearchTerm(searchTerm);
			Search.ExecuteSearch();
			var currentUrl = Browser.PageUrl;

            // Assert : Verify that LP site returns the page with search results.
            Assert.True(Sort.IsCurrentPage, "LP site not returned the page with search results.");

			// Act : Scroll down to bottom of the page
			Browser.ScrollToBottomOfPage(currentUrl);

            // Act :  Get the search term text from the search bar
            var stickySearchText = Search.GetSearchFieldText();

            /* Assert : 
			Verify if the sticky search bar is displayed or not
            Verify if the search term in sticky header matching to the search term on for which search is made 
            */
			Assert.True(Search.IsSearchBoxVisible, "Sticky search bar is not displayed");
			Assert.Equals(searchTerm, stickySearchText, "Search term is not matching");

			// Act : Clear the search field 
			Search.ClearSearchFieldText();

            /* Act :
            Get the random search term
            Execute the search on the site
            */
			string searchTerm2 = Search.GetRandomSearchTerm();
			Search.EnterSearchTerm(searchTerm2);
			Search.ExecuteSearch();
            var currentUrl2 = Browser.PageUrl;

			// Assert : Verify that LP site returns the page with search results.
			Assert.True(Sort.IsCurrentPage, "LP site not returned the page with search results.");

            // Act : Scroll down to bottom of the Page
			Browser.ScrollToBottomOfPage(currentUrl2);

			// Act :  Get the search term text from the search bar
			var stickySearchText2 = Search.GetSearchFieldText();

            /* Assert : 
			Verify if the sticky search bar is displayed or not
            Verify if the search term in sticky header matching to the search term on for which search is made 
            */
			Assert.True(Search.IsSearchBoxVisible, "Sticky search bar is not displayed");
			Assert.Equals(searchTerm2, stickySearchText2, "Search term is not matching");
		}
	}
}