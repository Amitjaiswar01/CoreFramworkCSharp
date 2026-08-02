using Xunit;
using Xunit.Abstractions;
using xRetry;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.CreateAccount.T296_T497_VerifyFacebookRedirection
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.CreateAccount)]
    public class T497_iPhone_VerifyFacebookButtonRedirectsToLogin : T497_MobileBase
    {
        public T497_iPhone_VerifyFacebookButtonRedirectsToLogin(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void FacebookButtonRedirectsToLogin(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.CreateAccount)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CreateAccount)]
    public class T497_Emulator_VerifyFacebookButtonRedirectsToLogin : T497_MobileBase
    {
        public T497_Emulator_VerifyFacebookButtonRedirectsToLogin(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void FacebookButtonRedirectsToLogin(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that clicking on the 'Connect using Facebook' button re-directs user to Facebook login.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9897
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T497
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9897"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T497")]
    public abstract class T497_MobileBase : TestsBaseMobile
    {
        protected T497_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: User is on the Create Account page.
            InitializeFunctionalTest(config);
            CreateAccount.Navigate();
            Assert.True(CreateAccount.IsCurrentPage, "Use is not on Create Account page.");

            //Act: Click on 'Connect using Facebook' button.
            CreateAccount.OpenFacebookLoginPage();

            //Assert: User is on the Facebook login page.
            var test = TextActions.TrimUrlAfterDesignatedString(Browser.PageUrl, "php");
            Assert.Equals(CreateAccount.FacebookLoginUrl, TextActions.TrimUrlAfterDesignatedString(Browser.PageUrl, "php"), "Facebook login page did not loaded.");
        }
    }
}
