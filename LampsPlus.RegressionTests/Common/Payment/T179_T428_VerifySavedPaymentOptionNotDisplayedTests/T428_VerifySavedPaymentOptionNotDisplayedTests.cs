using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using xRetry;

namespace LampsPlus.RegressionTests.Common.Payment.T179_T428_VerifySavedPaymentOptionNotDisplayedTests
{
    //[Collection(LpTraits.BatchGroup.Mobile.Payment)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Payment)]
    public class T428_iPhone_VerifySavedPymtOptNotAvailForUsers : T428_MobileBase
    {
        public T428_iPhone_VerifySavedPymtOptNotAvailForUsers(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void SavedPymtOptNotAvailForUsers(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Payment)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T428_Emulator_VerifySavedPymtOptNotAvailForUsers : T428_MobileBase
    {
        public T428_Emulator_VerifySavedPymtOptNotAvailForUsers(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void SavedPymtOptNotAvailForUsers(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the saved payment option isn't available for certain user roles.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5480
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T428
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5480"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T428")]
    public abstract class T428_MobileBase : TestsBaseMobile
    {
        protected T428_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange
            InitializeFunctionalTest(config);

            //Act
            ShoppingCartWorkflow.ProceedToPaymentWithSingleProduct();

            //Assert
            Assert.False(Payment.IsSavedPaymentsElementVisible, "Saved Payment Area should not be displayed for Anonymous users.");
        }
    }
}

