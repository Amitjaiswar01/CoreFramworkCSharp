using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Payment.T179_T428_VerifySavedPaymentOptionNotDisplayedTests
{
    //[Collection(LpTraits.BatchGroup.Common.Payment)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T179_Windows_VerifySavedPymtOptNotAvailForUsers : T179_DesktopBase
    {
        public T179_Windows_VerifySavedPymtOptNotAvailForUsers(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void SavedPymtOptNotAvailForUsers(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.Categories.CRUD)]
    public class T179_Windows_Employee_VerifySavedPymtOptNotAvailForUsers : T179_DesktopBase
    {
        public T179_Windows_Employee_VerifySavedPymtOptNotAvailForUsers(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void SavedPymtOptNotAvailForUsers(string config) => Validate(config);
    }

    /// <summary>
    /// Verify the saved payment option isn't available for certain user roles.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5513
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T179
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5513"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T179")]
    public abstract class T179_DesktopBase : TestsBaseDesktop
    {
        protected T179_DesktopBase(ITestOutputHelper output) : base(output) { }

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

