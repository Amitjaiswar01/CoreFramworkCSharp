using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.IntegrationTests.Pages.Sort
{
    /// <summary>
    /// Tests to ensure all IElements and Lists of IElements can be found on the given page object.
    /// </summary>
    [Trait(LpTraits.Integration.PageObjectModel, "SortBucket")]
    public class SortBucketLocatorTests : PageObjectTestsBase
    {
        /// <summary>
        /// Tests to ensure this page can find all its IElements.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public SortBucketLocatorTests(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the requested Sort Bucket elements could be located on the given sort page.
        /// </summary>
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LocateSortBucketElementsOnAllChandeliersSortPageTest(string config)
        {
            InitializeFramework(config, Urls.AllChandeliersSortPageUrl);
            BuildElementsList(SortBucket);
           
            VerifyElementDisplayed(() => SortBucket.SplashMessageElement);
            VerifyElementDisplayed(() => SortBucket.BucketContainerElement);
            VerifyElementDisplayed(() => SortBucket.SplashBucketContainerElements);
        }
    }
}
