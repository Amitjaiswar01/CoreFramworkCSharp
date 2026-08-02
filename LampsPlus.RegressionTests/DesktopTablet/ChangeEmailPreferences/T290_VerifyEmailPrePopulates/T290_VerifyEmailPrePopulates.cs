using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.ChangeEmailPreferences.T290_VerifyEmailPrePopulates
{
    //[Collection(LpTraits.BatchGroup.Desktop.ChangeEmailPreferences)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.ChangeEmailPreferences)]
    public class T290_Windows_VerifyEmailPrePopulates : T290_DesktopBase
    {
        public T290_Windows_VerifyEmailPrePopulates(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyEmailPrePopulatesOnEmail(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.ChangeEmailPreferences)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.ChangeEmailPreferences)]
    public class T290_Mac_VerifyEmailPrePopulates : T290_DesktopBase
    {
        public T290_Mac_VerifyEmailPrePopulates(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyEmailPrePopulatesOnEmail(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.ChangeEmailPreferences)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.ChangeEmailPreferences)]
    public class T290_iPad_VerifyEmailPrePopulates : T290_DesktopBase
    {
        public T290_iPad_VerifyEmailPrePopulates(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyEmailPrePopulatesOnEmail(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.ChangeEmailPreferences)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.ChangeEmailPreferences)]
    public class T290_TabletEmulator_VerifyEmailPrePopulates : T290_DesktopBase
    {
        public T290_TabletEmulator_VerifyEmailPrePopulates(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyEmailPrePopulatesOnEmail(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the email address is pre-populated on the Email page
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9941
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T290
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9941"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T290")]
    public abstract class T290_DesktopBase : TestsBaseDesktop
    {
        protected T290_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            // Arrange : User is on Homepage
            InitializeFunctionalTest(config);
            Skip.IfNot(DevEnvInformation.DatabaseSymbol == "P", "This test can only be executed against DBCLUST.");

            // Act : Enter Email in "Stay Connected.." Box in Footer and click on Subscribe button
            var account = new Account();
            HeaderFooter.NavigateToEmailPageFromFooter(account.EmailAddress);

            // Assert : Verify the user is on "Subscribe Now" page and email address is pre-populated 
            Assert.True(Email.IsCurrentPage, "User is Not on Email Page");
            Assert.Equals(account.EmailAddress, Email.GetEmailFromEmailAddressField(), "Email address is not pre-populated");
        }
    }
}
