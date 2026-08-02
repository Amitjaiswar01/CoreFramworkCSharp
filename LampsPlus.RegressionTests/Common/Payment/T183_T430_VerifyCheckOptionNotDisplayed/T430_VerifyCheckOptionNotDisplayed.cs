using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using xRetry;

namespace LampsPlus.RegressionTests.Common.Payment.T183_T430_VerifyCheckOptionNotDisplayed
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Payment)]
    public class T430_iPhone_VerifyCheckOptionNotDisplayed : T430_MobileBase
    {
        public T430_iPhone_VerifyCheckOptionNotDisplayed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void CheckOptionNotDisplayed(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T430_Emulator_VerifyCheckOptionNotDisplayed : T430_MobileBase
    {
        public T430_Emulator_VerifyCheckOptionNotDisplayed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void CheckOptionNotDisplayed(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the payment option for checks is NOT available.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10000
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T430
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10000"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T430")]
    public abstract class T430_MobileBase : TestsBaseMobile
    {
        protected T430_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: User has added an item to the cart and proceeded to the Payment page.
            InitializeFunctionalTest(config);
            ShoppingCartWorkflow.ProceedToPaymentWithSingleProduct();

            //Assert: Verify the payment option "Check" is NOT available.
            Assert.False(Payment.IsPaymentTypeAvailable(PaymentType.PaperCheck), "Paper Check Payment Type should not be displayed.");
        }
    }
}
