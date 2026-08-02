using xRetry;
using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.HeaderFooter.T273_T7785_VerifyPresenceOfEmailSubscribeBox
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.HeaderFooter)]
    public class T7785_iPhone_VerifyEmailSubBoxVisibleInFooter : T7785_MobileBase
    {
        public T7785_iPhone_VerifyEmailSubBoxVisibleInFooter(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void EmailSubBoxVisibleInFooter(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.HeaderFooter)]
    public class T7785_Emulator_VerifyLpHeaderFooterDisplaysTheCorrectElementsAnon : T7785_MobileBase
    {
        public T7785_Emulator_VerifyLpHeaderFooterDisplaysTheCorrectElementsAnon(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void EmailSubBoxVisibleInFooter(string config) => Validate(config);
    }


    /// <summary>
    /// Verify Email Sign Up is added to the global footer for Mobile.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9944
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7785
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9944"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7785")]
    public abstract class T7785_MobileBase : TestsBaseMobile
    {
        protected T7785_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: User has navigated to the Home page.
            InitializeFunctionalTest(config);
            Browser.Navigate(Urls.HomePageUrl);
            Assert.True(Home.IsCurrentPage, "User is not on the Home page.");
            
            //Act and Assert: Scroll to the footer and verify the presence of the email text field and subscribe button.
            HeaderFooter.WaitForEmailSubscribeElementToLoad();
            Assert.Equals(HeaderFooter.GetExpectedEmailSubscribeString(), HeaderFooter.GetEmailSubscribeFieldText(), "Label does not match");
            Assert.True(HeaderFooter.IsEmailSubscribeFieldVisible(), "Sign up for email updates field not displayed.");
            Assert.True(HeaderFooter.IsEmailSubscribeButtonVisible(), "Sign up for email updates subscribe button not displayed.");
        }
    }
}
