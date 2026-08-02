using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.DesktopTablet.SignIn
{
    //[Collection(LpTraits.UserRole.Customer)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.SecureSignin)]
    public class T297_Windows_VerifyKeepMeLoggedInCheckBox : T297_DesktopBase
    {
        public T297_Windows_VerifyKeepMeLoggedInCheckBox(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void KeepMeLoggedInCheckBox(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Customer)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.SecureSignin)]
    public class T297_Mac_VerifyKeepMeLoggedInCheckBox : T297_DesktopBase
    {
        public T297_Mac_VerifyKeepMeLoggedInCheckBox(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI)]
        public void KeepMeLoggedInCheckBox(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Customer)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.SecureSignin)]
    public class T297_iPad_VerifyKeepMeLoggedInCheckBox : T297_DesktopBase
    {
        public T297_iPad_VerifyKeepMeLoggedInCheckBox(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI)]
        public void KeepMeLoggedInCheckBox(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Customer)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.SecureSignin)]
    public class T297_TabletEmulator_VerifyKeepMeLoggedInCheckBox : T297_DesktopBase
    {
        public T297_TabletEmulator_VerifyKeepMeLoggedInCheckBox(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI)]
        public void KeepMeLoggedInCheckBox(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the user is not logged out if the 'Keep me logged in' check box is checked
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5436
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T297
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5436"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T297")]
    [Trait(LpTraits.Keys.Category, LpTraits.Categories.CRUD)]
    public abstract class T297_DesktopBase : SignInTestsBase
    {
        protected T297_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            InitializeFramework(config);

            Browser.Navigate("https://google.com");
            Browser.ExecuteJs("window.sessionStorage.clear()");
            Browser.Navigate(Urls.HomePageUrl);

            Assert.Displayed(HeaderFooter.UserNameLink, "User not automatically signed in when navigating back to Lamps Plus.");
        }
    }
}
