using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.IntegrationTests.Pages.Search
{

	public class SearchLocatorDesktopTests : SearchLocatorTests
	{
		/// <summary>
		/// Tests to ensure this page can find all its IElements.
		/// </summary>
		/// <param name="output"></param>
		public SearchLocatorDesktopTests(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Integration.PageObjectModel, "Search")]
		[SkippableTheory]
		[InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
		public void LocateSearchElementsTest(string config) => Locate(config);

		protected override void VerifyCameraSearchElements()
		{
			VerifyElementDisplayed(() => Search.FilePhotoLabelElement);
			VerifyElementExists(() => Search.FilePhotoInputElement);

			// This elements are only displayed for mobile
			VerifyElementNotImplemented(() => Search.ImageSearchModalElement);
			VerifyElementNotImplemented(() => Search.TakeAPhotoElement);
			VerifyElementNotImplemented(() => Search.UseExistingPhotoElement);
		}

    }

	public class SearchLocatorMobileTests : SearchLocatorTests
	{
		public SearchLocatorMobileTests(ITestOutputHelper output) : base(output)
		{
		}

		[Trait(LpTraits.Integration.PageObjectModel, "Search")]
		[SkippableTheory]
		[InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
		public void LocateSearchElementsTest(string config) => Locate(config);

		protected override void VerifyCameraSearchElements()
		{
			VerifyElementDisplayed(() => Search.ImageSearchModalElement);
			VerifyElementDisplayed(() => Search.TakeAPhotoElement);
			VerifyElementDisplayed(() => Search.UseExistingPhotoElement);
			VerifyElementExists(() => Search.FilePhotoInputElement);

			// This element is only displayed for desktop
			VerifyElementNotDisplayed("FilePhotoLabelElement");
		}

    }


	/// <summary>
	/// Tests to ensure all IElements and Lists of IElements can be found on the Search page.
	/// </summary>
	[Trait(LpTraits.Integration.PageObjectModel, "Search")]
    public abstract class SearchLocatorTests : PageObjectTestsBase
    {
        /// <summary>
        /// Test 
        /// </summary>
        /// <param name="output"></param>
        protected SearchLocatorTests(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the requested elements could be located on the given Search page.
        /// </summary>
        public void Locate(string config)
        {
            InitializeFramework(config, Urls.HomePageUrl);
            BuildElementsList(Search);

            VerifyElementDisplayed(() => Search.SearchField);
            VerifyElementDisplayed(() => Search.SearchButton);

            Search.SearchField.SendKeys("bathroom");
            VerifyElementDisplayed(() => Search.AutoSuggestDropDownDiv);
            VerifyElementDisplayed(() => Search.AutoSuggestDropDownResultsDiv);
            VerifyElementExists(() => Search.AutoSuggestDropDownResults);
            Browser.RefreshPage();

            VerifyElementDisplayed(() => Search.CameraButtonElement);

	        Search.CameraButtonElement.Click();

			// This element is only displayed afterImageProcessed. We can only verify the element exists.
			VerifyElementExists(() => Search.ImageSearchLoadingScreenElement);

	        VerifyElementDisplayed(() => Search.PhotoSearchTextElement);

			VerifyCameraSearchElements();
		}

	    protected abstract void VerifyCameraSearchElements();

    }
}
