using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using xRetry;

namespace LampsPlus.RegressionTests.Common.Payment.T177_T426_VerifyGiftCertificateOptionNotDisplayed
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Payment)]
    public class T426_iPhone_VerifyGiftCertificateOptionNotDisplayed : T426_MobileBase
    {
        public T426_iPhone_VerifyGiftCertificateOptionNotDisplayed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void GiftCertificateOptionNotDisplayed(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T426_Emulator_VerifyGiftCertificateOptionNotDisplayed : T426_MobileBase
    {
        public T426_Emulator_VerifyGiftCertificateOptionNotDisplayed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void GiftCertificateOptionNotDisplayed(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the Gift Certificate payment option isn't available for certain user roles.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9997
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T426
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9997"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T426")]
    public abstract class T426_MobileBase : TestsBaseMobile
    {
        protected T426_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: User has added an item to the cart and proceeded to the Payment page.
            InitializeFunctionalTest(config);
            ShoppingCartWorkflow.ProceedToPaymentWithSingleProduct();

            //Act: Click on the Gift Card link.
            Payment.SelectGiftCardLink();

            //Assert: The payment option for Gift Certificate is not available (Gift Cards and Gift Certificates are different in this context).
            Assert.False(Payment.IsGiftCertContainerVisible, "Gift Certificate Area should not be displayed unless user is a CSR.");
        }
    }
}
