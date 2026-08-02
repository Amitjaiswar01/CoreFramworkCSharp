using System.Linq;
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
    [Trait(LpTraits.Integration.PageObjectModel, "SortFullPageCertona")]
    public class SortFullPageCertonaLocatorTests : PageObjectTestsBase
    {
        /// <summary>
        /// Tests to ensure this page can find all its IElements.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public SortFullPageCertonaLocatorTests(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the requested Sort Full Page Certona elements could be located on the given sort page.
        /// </summary>
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LocateSortFullPageCertonaElementsOnAllChandeliersSortPageTest(string config)
        {
            InitializeFramework(config, Urls.ProductFullPageBaseUrl + "U4514");
            BuildElementsList(SortFullPageCertona);

            Browser.Wait.ForDisplayedElement(SortFullPageCertona.FullPageCertonaSimilarDesignsItems.First());

            VerifyElementDisplayed(() => SortFullPageCertona.FullPageCertonaSimilarDesignsTitleElement);
            VerifyElementDisplayed(() => SortFullPageCertona.FullPageCertonaSimilarDesignsContainer);
            VerifyElementDisplayed(() => SortFullPageCertona.FullPageCertonaSimilarDesignsItems);
            VerifyElementDisplayed(() => SortFullPageCertona.FirstDisplayedSimilarDesignElement);
        }
    }
}
