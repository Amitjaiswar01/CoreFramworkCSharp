using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using xRetry;

namespace LampsPlus.RegressionTests.Common.Payment.T184_T431_VerifyPurchaseOrderOptionNotDisplayed
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Payment)]
    public class T431_iPhone_VerifyPurchaseOrderOptionNotDisplayed : T431_MobileBase
    {
        public T431_iPhone_VerifyPurchaseOrderOptionNotDisplayed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void PurchaseOrderOptionNotDisplayed(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T431_Emulator_VerifyPurchaseOrderOptionNotDisplayed : T431_MobileBase
    {
        public T431_Emulator_VerifyPurchaseOrderOptionNotDisplayed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void PurchaseOrderOptionNotDisplayed(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the payment option for P.O. is NOT available.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9996
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T431
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9996"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T431")]
    public abstract class T431_MobileBase : TestsBaseMobile
    {
        protected T431_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: User has added an item to the cart and proceeded to the Payment page.
            InitializeFunctionalTest(config);
            ShoppingCartWorkflow.ProceedToPaymentWithSingleProduct();

            //Assert: Verify the Purchase Order option is NOT available.
            Assert.False(Payment.IsPaymentTypeAvailable(PaymentType.PurchaseOrder), "Purchase Order Payment Type should not be displayed.");
        }
    }
}
