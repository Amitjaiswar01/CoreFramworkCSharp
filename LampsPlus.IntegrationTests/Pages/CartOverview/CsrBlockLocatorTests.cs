using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.IntegrationTests.Pages.ShoppingCart
{
    /// <summary>
    /// Tests to ensure all IElements and Lists of IElements can be found on the given page object.
    /// </summary>
    [Trait(LpTraits.Integration.PageObjectModel, "CsrBlock")]
    public class CsrBlockLocatorTests : PageObjectTestsBase
    {
        /// <summary>
        /// Tests to ensure this page can find all its IElements.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public CsrBlockLocatorTests(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the requested elements could be located on the given shopping cart page.
        /// </summary>
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void LocateElementsOnCsrBlockPageTest(string config)
        {
            InitializeFramework(config);
            BuildElementsList(CsrBlock);

            ShoppingCartWorkflow.EmptyCart();

            ShoppingCartWorkflow.AddSingleItemToCart();

            CartOverview.RemoveProfessionalAccount();

            VerifyElementDisplayed(() => CsrBlock.AddProfessionalAccountLink);
            VerifyElementDisplayed(() => CsrBlock.ApplyMdPercentButton);
            VerifyElementDisplayed(() => CsrBlock.ApplySAndPButton);
            VerifyElementDisplayed(() => CsrBlock.CsrPanelElement);
            VerifyElementDisplayed(() => CsrBlock.ManualDiscountPercentTextBox);
            VerifyElementDisplayed(() => CsrBlock.ReasonCodeDropdown);
            VerifyElementDisplayed(() => CsrBlock.SaleSourceField);
            VerifyElementDisplayed(() => CsrBlock.SAndPField);
            VerifyElementDisplayed(() => CsrBlock.SecondaryEmployeeField);

            CartOverview.AddProfessionalAccount(ShoppingCartTypes.CompanyName);

            VerifyElementDisplayed(() => CsrBlock.RemoveProfessionalAccountElement);

            CartOverview.RemoveProfessionalAccount();
        }
    }
}
