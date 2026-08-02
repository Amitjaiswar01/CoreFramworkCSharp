using Xunit;
using Xunit.Abstractions;
using xRetry;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.Payment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.ManageAccount.T302_T502_VerifyUserCanDeletePaymentOption
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.ManageAccount)]
    public class T502_iPhone_VerifyUserCanDeletePaymentOption : T502_MobileBase
    {
        public T502_iPhone_VerifyUserCanDeletePaymentOption(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
        public void UserCanDeletePaymentOption(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ManageAccount)]
    public class T502_Emulator_VerifyUserCanDeletePaymentOption : T502_MobileBase
    {
        public T502_Emulator_VerifyUserCanDeletePaymentOption(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void UserCanDeletePaymentOption(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that a user can delete a payment option.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9899
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T502
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9899"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T502")]
    public abstract class T502_MobileBase : TestsBaseMobile
    {
        protected T502_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            /*Arrange:
            User has 2 saved payment options.
            */
            InitializeFunctionalTest(config);
            ManageAccountWorkflow.AddNewDefaultPaymentMethod(CreditCards.TestVisaCard);
            ManageAccountWorkflow.AddNewDefaultPaymentMethod(CreditCards.TestMasterCard);

            //Act: Click on 'Remove' for one of the saved payment options.
            ManageAccount.DeleteOneSavedPaymentOption();

            //Assert: The payment option is deleted.
            var numberOfPaymentOptions = 1;
            Assert.Equals(numberOfPaymentOptions, ManageAccount.IsOnlyDefaultPaymentOptionAvailable(), "Second payment option is still visible");
        }
    }
}
