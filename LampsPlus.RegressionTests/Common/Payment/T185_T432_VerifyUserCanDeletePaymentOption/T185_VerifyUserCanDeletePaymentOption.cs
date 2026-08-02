using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.Payment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Payment.T185_T432_VerifyUserCanDeletePaymentOption
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T185_Windows_VerifyUserCanDeletePaymentOption : T185_DesktopBase
    {
        public T185_Windows_VerifyUserCanDeletePaymentOption(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void VerifyUserCanDeletePaymentOption(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T185_Mac_VerifyUserCanDeletePaymentOption : T185_DesktopBase
    {
        public T185_Mac_VerifyUserCanDeletePaymentOption(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI)]
        public void VerifyUserCanDeletePaymentOption(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T185_iPad_VerifyUserCanDeletePaymentOption : T185_DesktopBase
    {
        public T185_iPad_VerifyUserCanDeletePaymentOption(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI)]
        public void VerifyUserCanDeletePaymentOption(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T185_TabletEmulator_VerifyUserCanDeletePaymentOption : T185_DesktopBase
    {
        public T185_TabletEmulator_VerifyUserCanDeletePaymentOption(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI)]
        public void VerifyUserCanDeletePaymentOption(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that a user can delete a saved payment option.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9995
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T185
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9995"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T185")]
    public abstract class T185_DesktopBase : TestsBaseDesktop
    {
        protected T185_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: User is signed in and has a previously saved payment information
            InitializeFunctionalTest(config);
            ManageAccountWorkflow.AddNewDefaultPaymentMethod(CreditCards.TestVisaCard);
            ShoppingCartWorkflow.ProceedToPaymentWithSingleProduct();

            /*Act:
             Select the saved payment option by clicking on the radio button.
             Click on the DETAILS link for the selected payment.
             Scroll down and click on the DELETE PAYMENT button.
             */
            Payment.DeletePaymentOption();

            //Assert: The saved payment option is deleted.
            var expectedLandingPage = ManageAccount.PageUrl + ManageAccount.PaymentOptionsUrl;
            var browser = ManageAccount.Navigate(ManageAccount.PaymentOptionsUrl);
            Assert.Equals(expectedLandingPage, browser.PageUrl, $"{expectedLandingPage} is expected, but actual url is {browser.PageUrl}");

            Assert.True(ManageAccount.IsPaymentOptionDeleted(), "The saved payment option is not deleted");
        }
    }
}
