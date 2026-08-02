using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Payment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using xRetry;

namespace LampsPlus.RegressionTests.Common.Payment.T185_T432_VerifyUserCanDeletePaymentOption
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Payment)]
    public class T432_iPhone_VerifyUserCanDeletePaymentOption : T432_MobileBase
    {
        public T432_iPhone_VerifyUserCanDeletePaymentOption(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
        public void VerifyUserCanDeletePaymentOption(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T432_Emulator_VerifyUserCanDeletePaymentOption : T432_MobileBase
    {
        public T432_Emulator_VerifyUserCanDeletePaymentOption(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void VerifyUserCanDeletePaymentOption(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that a user can delete a saved payment option.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9995
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T432
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9995"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T432")]
    public abstract class T432_MobileBase : TestsBaseMobile
    {
        protected T432_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: User is signed in and has a previously saved payment information
            InitializeFunctionalTest(config);
            ManageAccountWorkflow.AddNewDefaultPaymentMethod(CreditCards.TestVisaCard);
            ShoppingCartWorkflow.ProceedToPaymentWithSingleProduct();

            /*Act:
             On the Payment page, select the saved payment option by tapping on the radio button.
             Tap on the Edit link for the selected saved payment option.
             Scroll down and tap on the DELETE CARD button.
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
