using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Payment.T183_T430_VerifyCheckOptionNotDisplayed
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T183_Windows_VerifyCheckOptionNotDisplayed : T183_DesktopBase
    {
        public T183_Windows_VerifyCheckOptionNotDisplayed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void CheckOptionNotDisplayed(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T183_Mac_VerifyCheckOptionNotDisplayed : T183_DesktopBase
    {
        public T183_Mac_VerifyCheckOptionNotDisplayed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void CheckOptionNotDisplayed(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T183_iPad_VerifyCheckOptionNotDisplayed : T183_DesktopBase
    {
        public T183_iPad_VerifyCheckOptionNotDisplayed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void CheckOptionNotDisplayed(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T183_TabletEmulator_VerifyCheckOptionNotDisplayed : T183_DesktopBase
    {
        public T183_TabletEmulator_VerifyCheckOptionNotDisplayed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void CheckOptionNotDisplayed(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the payment option for checks is NOT available.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10000
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T183
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10000"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T183")]
    public abstract class T183_DesktopBase : TestsBaseDesktop
    {
        protected T183_DesktopBase(ITestOutputHelper output) : base(output) { }

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
