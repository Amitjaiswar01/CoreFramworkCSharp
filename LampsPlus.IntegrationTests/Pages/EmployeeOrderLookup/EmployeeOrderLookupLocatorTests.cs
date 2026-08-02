using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.IntegrationTests.Pages.EmployeeOrderLookup
{
    /// <summary>
    /// Tests to ensure all IElements and Lists of IElements can be found on the given page object.
    /// </summary>
    [Trait(LpTraits.Integration.PageObjectModel, "EmployeeOrderLookup")]
    public class EmployeeOrderLookupLocatorTests : PageObjectTestsBase
    {
        /// <summary>
        /// Tests to ensure this page can find all its IElements.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public EmployeeOrderLookupLocatorTests(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the requested elements could be located on the given EmployeeOrderLookup page.
        /// </summary>
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LocateEmployeeOrderLookupElementsTest(string config)
        {
            InitializeFramework(config);
            BuildElementsList(EmployeeOrderLookup);

            SignInWorkflow.SignIn(LampsPlusAccounts.CustomerServiceManagerLoginAccount);
            Browser.Navigate(Urls.EmployeeOrderLookupPageUrl);

            VerifyElementDisplayed(() => EmployeeOrderLookup.FirstOrder);
            VerifyElementDisplayed(() => EmployeeOrderLookup.MyOrdersRadioButton);
            VerifyElementDisplayed(() => EmployeeOrderLookup.OrderSearchButton);
            VerifyElementDisplayed(() => EmployeeOrderLookup.OrderSearchInput);
            VerifyElementDisplayed(() => EmployeeOrderLookup.PaginationDropdown);
            VerifyElementDisplayed(() => EmployeeOrderLookup.SearchTypeDropdown);
            VerifyElementDisplayed(() => EmployeeOrderLookup.StoreRadioButton);
            VerifyElementDisplayed(() => EmployeeOrderLookup.StoreNumberDropDown);
            VerifyElementDisplayed(() => EmployeeOrderLookup.PaginationDropdownPageOptions);
            VerifyElementDisplayed(() => EmployeeOrderLookup.Orders);
        }
    }
}
