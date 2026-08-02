using System.Collections.Generic;
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
    public class T7429_Windows_VerifyLayoutChangeEmailPreferencesThankYouPage : T7429_DesktopBase
    {
        public T7429_Windows_VerifyLayoutChangeEmailPreferencesThankYouPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyLayoutChangeEmailPreferencesThankYouPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7429_Mac_VerifyLayoutChangeEmailPreferencesThankYouPage : T7429_DesktopBase
    {
        public T7429_Mac_VerifyLayoutChangeEmailPreferencesThankYouPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyLayoutChangeEmailPreferencesThankYouPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7429_iPad_VerifyLayoutChangeEmailPreferencesThankYouPage : T7429_DesktopBase
    {
        public T7429_iPad_VerifyLayoutChangeEmailPreferencesThankYouPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyLayoutChangeEmailPreferencesThankYouPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7429_TabletEmulator_VerifyLayoutChangeEmailPreferencesThankYouPage : T7429_DesktopBase
    {
        public T7429_TabletEmulator_VerifyLayoutChangeEmailPreferencesThankYouPage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyLayoutChangeEmailPreferencesThankYouPage(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Change Email Preferences page and Thank You page after subscribing to emails.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7593
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7429
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7593"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7429")]
    public abstract class T7429_DesktopBase : VisualTestsBaseDesktop, IClassFixture<FixtureBase>
    {
        protected T7429_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            //Arrange: 
            InitializeVisualTest(config);

            /*Act:
            On the Lamps Plus footer, in the Sign Up for Email Updates field, enter the email address.
            On the Welcome To LAMPS PLUS E-Mail page, capture a screenshot for the entire page, but ignore the Email Address field.
            */
            var account = new Account();
            HeaderFooter.SignUpForCouponsOffersAndSaleAlerts(account);
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { Email.IgnoreEmailAddressField() }, true, true, Email.IgnoreEmailAddressField(), 10, 0, 10, 10);

            /*Act:
            Fill out the fields in the Subscribe Now! section.
            Once the Thank you for requesting email updates from LAMPS PLUS! page loads, capture a screenshot for the entire page, but ignore the Email Address.
            */
            Email.FillOutSubscribeNow(account);
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { Email.IgnoreEmailUtagElement() }, true, true,Email.IgnoreEmailUtagElement(), 10, 0, 10, 10);
        }
    }
}
