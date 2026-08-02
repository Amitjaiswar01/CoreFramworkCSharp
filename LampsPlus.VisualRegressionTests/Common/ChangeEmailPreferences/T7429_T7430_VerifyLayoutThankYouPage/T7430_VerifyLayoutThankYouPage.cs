using System.Collections.Generic;
using System.Web.UI;
using xRetry;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ChangeEmailPreferences.T7429_T7430_VerifyLayoutThankYouPage
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7430_iPhone_VerifyLayoutChangeEmailPreferencesThankYouPage : T7430_MobileBase
    {
        public T7430_iPhone_VerifyLayoutChangeEmailPreferencesThankYouPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth)]
        public void VerifyLayoutChangeEmailPreferencesThankYouPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7430_Android_VerifyLayoutChangeEmailPreferencesThankYouPage : T7430_MobileBase
    {
        public T7430_Android_VerifyLayoutChangeEmailPreferencesThankYouPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void VerifyLayoutChangeEmailPreferencesThankYouPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7430_Emulator_VerifyLayoutChangeEmailPreferencesThankYouPage : T7430_MobileBase
    {
        public T7430_Emulator_VerifyLayoutChangeEmailPreferencesThankYouPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyLayoutChangeEmailPreferencesThankYouPage(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Change Email Preferences page and Thank You page after subscribing to emails.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7593
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7430
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7593"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7430")]
    public abstract class T7430_MobileBase : VisualTestsBaseMobile, IClassFixture<FixtureBase>
    {
        protected T7430_MobileBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            //Arrange: 
            InitializeVisualTest(config);

            //Act: Navigate to URL: https://www.lampsplus.com/account/email/?isFromFooter=true
            Browser.Navigate(Urls.EmailSubscribeChangeEmailPreferencesUrl);

            /*Act:
            On the Subscribe tab, enter the email address identified in the preconditions in the Email field.
            Fill out the remaining fields.
            Click on the SUBSCRIBE button.
            */
            var account = new Account();
            Email.FillOutSubscribeNow(account);

            //Act:On the page that loads with the Thank You! message, capture a screenshot for the entire page, but ignore the Email Address.
            ScreenCapturer.CaptureScreenRegionWithIgnoredLayouts(Browser.PageUrl, Browser.Locate.ElementByTagName(HtmlTextWriterTag.Body), new List<IElement> { Email.IgnoreEmailUtagElement() });
        }
    }
}
