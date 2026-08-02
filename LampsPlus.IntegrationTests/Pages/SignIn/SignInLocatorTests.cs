using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.IntegrationTests.Pages.SignIn
{
    /// <summary>
    /// Tests to ensure all IElements and Lists of IElements can be found on the Header SignIn Menu and SignIn page.
    /// </summary>
    [Trait(LpTraits.Integration.PageObjectModel, "SignIn")]
    public class SignInLocatorTests : PageObjectTestsBase
    {
        /// <summary>
        /// Test 
        /// </summary>
        /// <param name="output"></param>
        public SignInLocatorTests(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the requested Header SignIn elements could be located on the given sort page.
        /// </summary>
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LocateSignInElementsTest(string config)
        {
            InitializeFramework(config, Urls.HomePageUrl);

            BuildElementsList(SignIn);

            HeaderFooter.VerifySignInLinkIsPresentAndClick();

            Browser.Wait.ForDomReady(2000);

            Verify.Displayed(HeaderFooter.SignInPopUp, "The signin modal did not open.");

            VerifyElementDisplayed(() => SignIn.ConnectUsingFbButton);
            VerifyElementDisplayed(() => SignIn.CreateAccountButton);
            VerifyElementDisplayed(() => SignIn.PasswordField);
            VerifyElementDisplayed(() => SignIn.EmailField);
            VerifyElementDisplayed(() => SignIn.HeaderMenuSignInButton);

            Browser.Navigate(Urls.SignInPageUrl);

            VerifyElementDisplayed(() => SignIn.SignInContainer);
            VerifyElementDisplayed(() => SignIn.SignInButton);

            HeaderFooter.VerifySignInLinkIsPresentAndClick();
           
            VerifyElementDisplayed(() => SignIn.CreateAccountLink);
            VerifyElementNotImplemented(() => SignIn.ConnectUsingFb);

            Browser.Navigate(Urls.EmailSubscribeChangeEmailPreferencesUrl); //Verifies 'Message' variable on SignIn.cs
            SubscribeToEmailsWorkflow.GoToEmailPreferencesByEmail(LampsPlusAccounts.CustomerLoginAccount.UserName);

            VerifyElementDisplayed(() => SignIn.MessageElement);
        }
    }
}
