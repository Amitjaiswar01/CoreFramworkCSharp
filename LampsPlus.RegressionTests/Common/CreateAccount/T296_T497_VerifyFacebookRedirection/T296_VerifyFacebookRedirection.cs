using Xunit;
using Xunit.Abstractions;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.CreateAccount.T296_T497_VerifyFacebookRedirection
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CreateAccount)]
    public class T296_Windows_VerifyFacebookButtonRedirectsToLogin : T296_DesktopBase
    {
        public T296_Windows_VerifyFacebookButtonRedirectsToLogin(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void FacebookButtonRedirectsToLogin(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CreateAccount)]
    public class T296_Mac_VerifyFacebookButtonRedirectsToLogin : T296_DesktopBase
    {
        public T296_Mac_VerifyFacebookButtonRedirectsToLogin(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void FacebookButtonRedirectsToLogin(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CreateAccount)]
    public class T296_iPad_VerifyFacebookButtonRedirectsToLogin : T296_DesktopBase
    {
        public T296_iPad_VerifyFacebookButtonRedirectsToLogin(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void FacebookButtonRedirectsToLogin(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CreateAccount)]
    public class T296_TabletEmulator_VerifyFacebookButtonRedirectsToLogin : T296_DesktopBase
    {
        public T296_TabletEmulator_VerifyFacebookButtonRedirectsToLogin(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void FacebookButtonRedirectsToLogin(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that clicking on the 'Connect using Facebook' button re-directs user to Facebook login.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9897
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T296
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9897"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T296")]
    public abstract class T296_DesktopBase : TestsBaseDesktop
    {
        protected T296_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: User is on the Create Account page.
            InitializeFunctionalTest(config);
            CreateAccount.Navigate();
            Assert.True(CreateAccount.IsCurrentPage, "Use is not on Create Account page.");

            //Act: Click on 'Connect using Facebook' button.
            CreateAccount.OpenFacebookLoginPage();

            //Assert: User is on the Facebook login page.
            Assert.Equals(CreateAccount.FacebookLoginUrl, TextActions.TrimUrlAfterDesignatedString(Browser.PageUrl, "php"), "Facebook login page did not loaded.");
        }
    }
}
