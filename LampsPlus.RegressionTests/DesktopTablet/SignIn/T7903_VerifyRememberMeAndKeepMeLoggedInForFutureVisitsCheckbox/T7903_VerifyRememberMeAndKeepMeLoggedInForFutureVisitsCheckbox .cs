using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.SignIn.T7903_VerifyRememberMeAndKeepMeLoggedInForFutureVisitsCheckbox
{
    //[Collection(LpTraits.BatchGroup.DesktopTablet.SignIn)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.SecureSignin)]
    public class T7903_Windows_VerifyRememberMeAndKeepMeLoggedInForFutureVisitsCheckbox : T7903_DesktopBase
    {
        public T7903_Windows_VerifyRememberMeAndKeepMeLoggedInForFutureVisitsCheckbox(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_UNSI)]
        public void RememberMeAndKeepMeLoggedInForFutureVisitsCheckbox(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the 'Remember Me' and 'Keep me logged in for future visits' Checkbox is Not Displayed for Kiosk Sign-Ins Modal and Sign-Ins Page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10443
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7903
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10443"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7903")]
    public abstract class T7903_DesktopBase : TestsBaseDesktop
    {
        protected T7903_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            // Arrange : User is on the homepage.
            InitializeFunctionalTest(config);

            // Act : Click on Sign In on Homepage
            SignIn.OpenSignInModal();

            // Assert : 'Remember Me' checkbox should not be displayed below the password field
            Assert.True(SignIn.IsRememberMeCheckboxVisible, "Remember Me Checkbox is Displayed");

            // Act : Navigate to Sign In page
            SignIn.Navigate();

            // Assert : 'Keep me logged in for future visits' checkbox should not be displayed below the password field
            Assert.True(SignIn.IsRememberMeCheckboxVisible, "Remember Me Checkbox is Displayed");

            // Act : Navigate to Pros page
            SignIn.NavigateToProSignInPage();

            // Assert : 'Keep me logged in for future visits' checkbox should not be displayed below the password field
            Assert.True(SignIn.IsRememberMeCheckboxVisible, "Remember Me Checkbox is Displayed");
        }
    }
}