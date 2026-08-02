using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Payment.T178_T427_VerifyWireTransferOptionNotDisplayed
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T178_Windows_VerifyWireTransferOptionNotDisplayed : T178_DesktopBase
    {
        public T178_Windows_VerifyWireTransferOptionNotDisplayed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void WireTransferOptionNotDisplayed(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T178_Mac_VerifyWireTransferOptionNotDisplayed : T178_DesktopBase
    {
        public T178_Mac_VerifyWireTransferOptionNotDisplayed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void WireTransferOptionNotDisplayed(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T178_iPad_VerifyWireTransferOptionNotDisplayed : T178_DesktopBase
    {
        public T178_iPad_VerifyWireTransferOptionNotDisplayed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void WireTransferOptionNotDisplayed(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T178_TabletEmulator_VerifyWireTransferOptionNotDisplayed : T178_DesktopBase
    {
        public T178_TabletEmulator_VerifyWireTransferOptionNotDisplayed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void WireTransferOptionNotDisplayed(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the Wire Transfer payment option isn't available for certain user roles.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9998
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T178
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9998"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T178")]
    public abstract class T178_DesktopBase : TestsBaseDesktop
    {
        protected T178_DesktopBase(ITestOutputHelper output) : base(output) { }

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
