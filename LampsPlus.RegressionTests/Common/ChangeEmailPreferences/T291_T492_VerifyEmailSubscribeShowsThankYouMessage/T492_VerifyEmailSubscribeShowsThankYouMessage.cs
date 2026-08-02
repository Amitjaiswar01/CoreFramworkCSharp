using Xunit;
using xRetry;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.ChangeEmailPreferences.T291_T492_VerifyEmailSubscribeShowsThankYouMessage
{
    //[Collection(LpTraits.BatchGroup.Mobile.ChangeEmailPreferences)]
    public class T492_iPhone_VerifyEmailSubscribeShowsThankYouMessage : T492_MobileBase
    {
        public T492_iPhone_VerifyEmailSubscribeShowsThankYouMessage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void SubscribeShowsThankYouMsg(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ChangeEmailPreferences)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ChangeEmailPreferences)]
    public class T492_Emulator_VerifyEmailSubscribeShowsThankYouMessage : T492_MobileBase
    {
        public T492_Emulator_VerifyEmailSubscribeShowsThankYouMessage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void SubscribeShowsThankYouMsg(string config) => Validate(config);
    }


    // <summary>
    // Verify that subscribing successfully to the email list shows a thank you message
    // Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9940
    // Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T492
    // </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9940"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T492")]
    public class T492_MobileBase : TestsBaseMobile
    {
        protected T492_MobileBase(ITestOutputHelper output) : base(output) { }

        protected virtual void Validate(string config)
        {
            // Arrange : User Navigated to Homepage
            InitializeFunctionalTest(config);

            /* Act :
            Navigate to Email Page
            Fill out "Subscribe Now" Form and Click on Subscribe Button
            */
            var account = new Account();
            Email.Navigate();
            Email.FillOutSubscribeNow(account);
            var optOutValuesList = AccountActions.GetUserProfileOptOutValuesList(account.EmailAddress);

            // Assert : Verify "Thank You" Message is displayed after subscribing 
            Assert.Equals(Messages.EmailPageMessages.ThankYouMessageAfterSubscribingMobile, Email.GetThankYouMessageAfterSubscribing(), "Thank you message is not displayed on Email page");

            // Assert : Verify Opt out values for Sub-location
            Assert.True(optOutValuesList.Exists(p => p.SubLocation == "9003" && p.OptOutEmail == "2"), "LP OptOutEmail code should be '2'");
            Assert.True(optOutValuesList.Exists(p => p.SubLocation == "9004" && p.OptOutEmail == "0"), "Pro OptOutEmail code should be '0'");
            Assert.True(optOutValuesList.Exists(p => p.SubLocation == "9008" && p.OptOutEmail == "0"), "Bdl OptOutEmail code should be '0'");
            Assert.True(optOutValuesList.Exists(p => p.SubLocation == "9009" && p.OptOutEmail == "1"), "Lpob OptOutEmail code should be '1'");
        }
    }
}
