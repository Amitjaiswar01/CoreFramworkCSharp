using Xunit;
using Xunit.Abstractions;
using xRetry;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;


namespace LampsPlus.RegressionTests.Common.Search.T337_T529_VerifySearchPersistence
{
	//[Collection(LpTraits.BatchGroup.Mobile.Search)]
	[Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Search)]
	public class T529_iPhone_VerifySearchPersistence : MobileBase
	{
		public T529_iPhone_VerifySearchPersistence(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
		[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void SearchPersistence(string config) => Validate(config);
	}


	//[Collection(LpTraits.BatchGroup.Common.Search)]
	[Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
	public class T529_Emulator_VerifySearchPersistence : MobileBase
	{
		public T529_Emulator_VerifySearchPersistence(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
		[InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
		public void SearchPersistence(string config) => Validate(config);
	}


	/// <summary>
	/// Verify if a value is removed from Search box, the blank search field persists through site.
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10057
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T529
	/// </summary>
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10057"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T529")]
	[Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
	public class MobileBase : TestsBaseMobile
	{
		protected MobileBase(ITestOutputHelper output) : base(output) { }

		protected void Validate(string config)
		{
			//Arrange: User is on Lamps Plus home page.
			InitializeFunctionalTest(config);
			Browser.Navigate(Urls.HomePageUrl);
			Assert.True(Home.IsCurrentPage, "User is not on the Home page.");

			/*Act:
             Enter in a search term that is NOT in the auto-complete list (e.g. "couch") or enter in a SKU.
             Do NOT execute the search.
             Remove the search term by hitting the backspace button until the ENTIRE value is removed.
             */

			var searchFieldEmptyMessage = Search.GetSearchFieldEmptyMessage();
			var searchTerm = Search.GetSearchTerm();

			Search.EnterSearchTerm(searchTerm);
			Assert.False(Search.IsAutoCompleteVisible, "AutoComplete Visible");
			Search.ClearSearchFieldText();

			//Assert: Search field should be empty.
			Assert.Equals(string.Empty, Search.GetSearchFieldText(), searchFieldEmptyMessage);

			//Act: Navigate to several pages throughout the site, observing the status of the Search box after each successful page load.
			Browser.Navigate(Urls.ContemporaryFloorLampsSortPageUrl);
			Assert.True(Sort.IsCurrentPage, "User is not on a Sort page.");

			//Assert: Search Text should not be Persist on Sort Page.
			Assert.Equals(string.Empty, Search.GetSearchFieldText(), searchFieldEmptyMessage);

			//Act: Navigate to PDP.
			Sort.SelectSortPageSkuByIndex(0);
			Assert.True(ProductDetail.IsCurrentPage, "User is not on Product Detail Page.");

			//Assert: Search Text should not be Persist PDP.
			Assert.Equals(string.Empty, Search.GetSearchFieldText(), searchFieldEmptyMessage);

			//Act: Navigate to any LP Page (Contact Us Page).
			Browser.Navigate(Urls.ContactUsPageUrl);
			Assert.True(ContactUs.IsCurrentPage, "Current page is not Contact Us page.");

			//Assert: Search Text should not be Persist on LP Page (Contact Us Page).
			Assert.Equals(string.Empty, Search.GetSearchFieldText(), searchFieldEmptyMessage);
		}
	}
}
