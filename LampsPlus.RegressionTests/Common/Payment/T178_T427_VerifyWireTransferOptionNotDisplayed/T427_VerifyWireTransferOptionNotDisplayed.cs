using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using xRetry;

namespace LampsPlus.RegressionTests.Common.Payment.T178_T427_VerifyWireTransferOptionNotDisplayed
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Payment)]
    public class T427_iPhone_VerifyWireTransferOptionNotDisplayed : T427_MobileBase
    {
        public T427_iPhone_VerifyWireTransferOptionNotDisplayed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void WireTransferOptionNotDisplayed(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T427_Emulator_VerifyWireTransferOptionNotDisplayed : T427_MobileBase
    {
        public T427_Emulator_VerifyWireTransferOptionNotDisplayed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void WireTransferOptionNotDisplayed(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the Wire Transfer payment option isn't available for certain user roles.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9998
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T427
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9998"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T427")]
    public abstract class T427_MobileBase : TestsBaseMobile
    {
        protected T427_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: User has added an item to the cart and proceeded to the Payment page.
            InitializeFunctionalTest(config);
            ShoppingCartWorkflow.ProceedToPaymentWithSingleProduct();

            //Assert: Verify the Wire Transfer option is NOT available.
            Assert.False(Payment.IsPaymentTypeAvailable(PaymentType.WireTransfer), "Wire Transfer Payment Type should not be displayed.");
        }
    }
}
