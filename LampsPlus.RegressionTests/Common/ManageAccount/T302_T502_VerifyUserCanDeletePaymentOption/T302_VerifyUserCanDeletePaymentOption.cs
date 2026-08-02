using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.Payment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.ManageAccount.T302_T502_VerifyUserCanDeletePaymentOption
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ManageAccount)]
    public class T302_Windows_VerifyUserCanDeletePaymentOption : T302_DesktopBase
    {
        public T302_Windows_VerifyUserCanDeletePaymentOption(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void UserCanDeletePaymentOption(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ManageAccount)]
    public class T302_Mac_VerifyUserCanDeletePaymentOption : T302_DesktopBase
    {
        public T302_Mac_VerifyUserCanDeletePaymentOption(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI)]
        public void UserCanDeletePaymentOption(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ManageAccount)]
    public class T302_iPad_VerifyUserCanDeletePaymentOption : T302_DesktopBase
    {
        public T302_iPad_VerifyUserCanDeletePaymentOption(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI)]
        public void UserCanDeletePaymentOption(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ManageAccount)]
    public class T302_TabletEmulator_VerifyUserCanDeletePaymentOption : T302_DesktopBase
    {
        public T302_TabletEmulator_VerifyUserCanDeletePaymentOption(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI)]
        public void UserCanDeletePaymentOption(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that a user can delete a payment option.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9899
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T302
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9899"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T302")]
    public abstract class T302_DesktopBase : TestsBaseDesktop
    {
        protected T302_DesktopBase(ITestOutputHelper output) : base(output) { }

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
