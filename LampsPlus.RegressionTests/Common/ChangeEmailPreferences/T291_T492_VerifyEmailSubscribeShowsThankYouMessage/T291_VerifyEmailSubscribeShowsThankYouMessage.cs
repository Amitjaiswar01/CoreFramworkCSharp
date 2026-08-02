using System.Text.RegularExpressions;
using Xunit;
using Xunit.Abstractions;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.ChangeEmailPreferences.T291_T492_VerifyEmailSubscribeShowsThankYouMessage
{
    //[Collection(LpTraits.BatchGroup.Common.ChangeEmailPreferences)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ChangeEmailPreferences)]
    public class T291_Windows_VerifyEmailSubscribeShowsThankYouMessage : T291_DesktopBase
    {
        public T291_Windows_VerifyEmailSubscribeShowsThankYouMessage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void SubscribeShowsThankYouMsg(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ChangeEmailPreferences)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ChangeEmailPreferences)]
    public class T291_Mac_VerifyEmailSubscribeShowsThankYouMessage : T291_DesktopBase
    {
        public T291_Mac_VerifyEmailSubscribeShowsThankYouMessage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void RedirectedExistingEmail(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ChangeEmailPreferences)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ChangeEmailPreferences)]
    public class T291_iPad_VerifyEmailSubscribeShowsThankYouMessage : T291_DesktopBase
    {
        public T291_iPad_VerifyEmailSubscribeShowsThankYouMessage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void SubscribeShowsThankYouMsg(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ChangeEmailPreferences)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ChangeEmailPreferences)]
    public class T291_TabletEmulator_VerifyEmailSubscribeShowsThankYouMessage : T291_DesktopBase
    {
        public T291_TabletEmulator_VerifyEmailSubscribeShowsThankYouMessage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void SubscribeShowsThankYouMsg(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that subscribing successfully to the email list shows a thank you message
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9940
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T291
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9940"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T291")]
    public abstract class T291_DesktopBase : TestsBaseDesktop
    {
        protected T291_DesktopBase(ITestOutputHelper output) : base(output) { }

        public void Validate(string config)
        {
            // Arrange : User Navigated to Homepage
            InitializeFunctionalTest(config);

            /* Act :
            Navigate to Email Page
            Fill out "Subscribe Now" Form and Click on Subscribe Button
            */
            var account = new Account();
            HeaderFooter.NavigateToEmailPageFromFooter(account.EmailAddress);
            Email.FillOutSubscribeNow(account);
            var optOutValuesList = AccountActions.GetUserProfileOptOutValuesList(account.EmailAddress);
            var thankYouMessage = TextActions.RegexNoTabsAndNewLines(Email.GetThankYouMessageAfterSubscribing());

            // Assert : Verify "Thank You" Message is displayed after subscribing
            Assert.Equals(Messages.EmailPageMessages.ThankYouMessageAfterSubscribingDesktop, TextActions.NormalizeWhitespace(Regex.Match(thankYouMessage, @"^(.*?!)").ToString()), "Thank you message is not displayed on Email page");

            // Assert : Verify Opt out values for Sub-location
            Assert.True(optOutValuesList.Exists(p => p.SubLocation == "9003" && p.OptOutEmail == "2"), "LP OptOutEmail code should be '2'");
            Assert.True(optOutValuesList.Exists(p => p.SubLocation == "9004" && p.OptOutEmail == "0"), "Pro OptOutEmail code should be '0'");
            Assert.True(optOutValuesList.Exists(p => p.SubLocation == "9008" && p.OptOutEmail == "0"), "Bdl OptOutEmail code should be '0'");
            Assert.True(optOutValuesList.Exists(p => p.SubLocation == "9009" && p.OptOutEmail == "1"), "Lpob OptOutEmail code should be '1'");
        }
    }
}