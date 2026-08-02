using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.IntegrationTests.Pages.CreateAccount
{
    /// <summary>
    /// Tests to ensure all IElements and Lists of IElements can be found on the CreateAccount page.
    /// </summary>
    [Trait(LpTraits.Integration.PageObjectModel, "CreateAccount")]
    public class CreateAccountLocatorTests : PageObjectTestsBase
    {
        /// <summary>
        /// Test to ensure all IElements are found on the page.
        /// </summary>
        /// <param name="output"></param>
        public CreateAccountLocatorTests(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the requested Create Account elements could be located on the given Create Account page.
        /// </summary>
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LocateCreateAccountElementsTest(string config)
        {
            InitializeFramework(config, Urls.CreateAccountPageUrl);
            BuildElementsList(CreateAccount);

            VerifyElementDisplayed(() => CreateAccount.FirstNameField);
            VerifyElementDisplayed(() => CreateAccount.LastNameField);
            VerifyElementDisplayed(() => CreateAccount.EmailField);
            VerifyElementDisplayed(() => CreateAccount.PasswordField);
            VerifyElementDisplayed(() => CreateAccount.SecurityAnswerField);
            VerifyElementDisplayed(() => CreateAccount.ZipCodeField);
            VerifyElementDisplayed(() => CreateAccount.CreateAccountBtn);
        }
    }
}
