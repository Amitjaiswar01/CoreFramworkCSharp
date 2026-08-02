using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.ChangeEmailPreferences
{
    //[Collection(LpTraits.BatchGroup.Common.ChangeEmailPreferences)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ChangeEmailPreferences)]
    public class T293_Windows_VerifyUnsubscribeThankYouMsg : T293_DesktopBase
    {
        public T293_Windows_VerifyUnsubscribeThankYouMsg(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void UnsubscribeThankYouMsg(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ChangeEmailPreferences)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ChangeEmailPreferences)]
    public class T293_Mac_VerifyUnsubscribeThankYouMsg : T293_DesktopBase
    {
        public T293_Mac_VerifyUnsubscribeThankYouMsg(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI)]
        public void UnsubscribeThankYouMsg(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ChangeEmailPreferences)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ChangeEmailPreferences)]
    public class T293_iPad_VerifyUnsubscribeThankYouMsg : T293_DesktopBase
    {
        public T293_iPad_VerifyUnsubscribeThankYouMsg(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI)]
        public void UnsubscribeThankYouMsg(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ChangeEmailPreferences)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ChangeEmailPreferences)]
    public class T293_TabletEmulator_VerifyUnsubscribeThankYouMsg : T293_DesktopBase
    {
        public T293_TabletEmulator_VerifyUnsubscribeThankYouMsg(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI)]
        public void UnsubscribeThankYouMsg(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that unsubscribing from emails shows a confirmation message.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5477
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T293
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5477"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T293")]
    //[Collection(LpTraits.UserRole.Customer)]
    [Trait(LpTraits.Keys.Category, LpTraits.Categories.CRUD)]
    public class T293_DesktopBase : TestsBaseDesktop
    {
        protected T293_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: User is on the change preferences page
            InitializeFunctionalTest(config, Urls.HomePageUrl);
            Browser.Navigate(Urls.EmailSubscribeChangeEmailPreferencesUrl);
            Assert.True(Email.IsEmailPreferencesPage, "Current page is not Emails preferences page");

            //Act: Unsubscribe for all options, then save settings.
            Email.ChangeEmailPreferencesSubscribe();
            Email.ChangeEmailPreferencesUnsubscribe();
            Email.SaveSettings();

            //Assert: A thank you message is displayed.
            Assert.StringContains("Thank You! Your preference changes have been successfully updated and saved.", Email.GetSaveSettingsThankYouMessage(), "No thank you message displayed.");
        }
    }
}
