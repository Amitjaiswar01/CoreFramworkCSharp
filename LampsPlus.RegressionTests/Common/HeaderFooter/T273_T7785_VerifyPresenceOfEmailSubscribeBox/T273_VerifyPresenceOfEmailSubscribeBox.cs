using Xunit;
using Xunit.Abstractions;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.HeaderFooter.T273_T7785_VerifyPresenceOfEmailSubscribeBox
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.HeaderFooter)]
    public class T273_Windows_VerifyEmailSubBoxVisibleInFooter : T273_DesktopBase
    {
        public T273_Windows_VerifyEmailSubBoxVisibleInFooter(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void EmailSubBoxVisibleInFooter(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.HeaderFooter)]
    public class T273_Mac_VerifyEmailSubBoxVisibleInFooter : T273_DesktopBase
    {
        public T273_Mac_VerifyEmailSubBoxVisibleInFooter(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void EmailSubBoxVisibleInFooter(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.HeaderFooter)]
    public class T273_iPad_VerifyEmailSubBoxVisibleInFooter : T273_DesktopBase
    {
        public T273_iPad_VerifyEmailSubBoxVisibleInFooter(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void EmailSubBoxVisibleInFooter(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.HeaderFooter)]
    public class T273_TabletEmulator_VerifyEmailSubBoxVisibleInFooter : T273_DesktopBase
    {
        public T273_TabletEmulator_VerifyEmailSubBoxVisibleInFooter(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void EmailSubBoxVisibleInFooter(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the presence of the Email subscribe text box in the footer.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9944
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T273
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9944"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T273")]
    public abstract class T273_DesktopBase : TestsBaseDesktop
    {
        protected T273_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: User has navigated to the Home page.
            InitializeFunctionalTest(config);
            Browser.Navigate(Urls.HomePageUrl);
            Assert.True(Home.IsCurrentPage, "User is not on the Home page.");

            //Act and Assert: Scroll to the footer and verify the presence of the email text field and subscribe button.
            Assert.True(HeaderFooter.GetEmailSubscribeFieldText().CaseInsensitiveContains("STAY CONNECTED"), "Label does not match");
            Assert.True(HeaderFooter.IsEmailSubscribeFieldVisible(), "Sign up for email updates field not displayed.");
            Assert.True(HeaderFooter.IsEmailSubscribeButtonVisible(), "Sign up for email updates subscribe button not displayed.");
        }
    }
}
