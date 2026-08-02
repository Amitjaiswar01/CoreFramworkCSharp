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
    [Trait(LpTraits.Integration.PageObjectModel, "SortPla")]
    public class SortPlaLocatorTests : PageObjectTestsBase
    {
        /// <summary>
        /// Tests to ensure this page can find all its IElements.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public SortPlaLocatorTests(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the requested elements could be located on the given Sort page.
        /// </summary>
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LocateSortPlaElementsTest(string config)
        {
            InitializeFramework(config);
            BuildElementsList(SortPla);

            Browser.Navigate($"{Urls.PlaSortPageBaseUrl}{SortPla.GetPlaSkuWithReviews()}");
            
            VerifyElementDisplayed(() => SortPla.PlaFrameElement);

            Browser.SwitchFocusToIframe(SortPla.PlaFrameElement);

            VerifyElementDisplayed(() => SortPla.PdpBodyElement);
            VerifyElementDisplayed(() => SortPla.PlaAddToCartElement);
            VerifyElementNotDisplayed("PlaProductSkuElement");
            VerifyElementDisplayed(() => SortPla.PlaFullCertonaElement);
            VerifyElementDisplayed(() => SortPla.PlaMoreDetailsLinkElement);
            VerifyElementDisplayed(() => SortPla.PlaProductTitleElement);
            
            if (!Settings.IsMobileView)
            {
                VerifyElementDisplayed(() => SortPla.PlaReadReviewsElement);
                VerifyElementDisplayed(() => SortPla.PlaRatingBoxElement);
                VerifyElementDisplayed(() => SortPla.PlaQuestionsElement);

                VerifyElementNotImplemented(() => SortPla.PlaViewLargerLinkElement);
                VerifyElementNotImplemented(() => SortPla.PlaLargeImageElement);
                VerifyElementNotImplemented(() => SortPla.PlaCloseButtonElement);
                VerifyElementNotImplemented(() => SortPla.ShipsTodayQLElement);
            }
            else
            {
                VerifyElementDisplayed(() => SortPla.ShipsTodayQLElement);

                Browser.Navigate($"{Urls.HomePageUrl}/sfp/{SortPla.GetPlaSkuWithReviews()}");

                Browser.Wait.ForDisplayedElement(SortPla.PlaViewLargerLinkElement);

                VerifyElementDisplayed(() => SortPla.PlaViewLargerLinkElement);

                SortPla.PlaViewLargerLinkElement.Click();
                Browser.Wait.ForElement(SortPla.PlaLargeImageElement, 10);
                
                VerifyElementDisplayed(() => SortPla.PlaLargeImageElement);

                Browser.Wait.ForDisplayedElement(SortPla.PlaCloseButtonElement);
                VerifyElementDisplayed(() => SortPla.PlaCloseButtonElement);

                VerifyElementNotImplemented(() => SortPla.PlaReadReviewsElement);
                VerifyElementNotImplemented(() => SortPla.PlaRatingBoxElement);
                VerifyElementNotImplemented(() => SortPla.PlaQuestionsElement);
            }
        }
    }
}
