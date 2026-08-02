using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Payment.T181_T429_VerifyProp65WarningOnlyDisplaysForCalifornia
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T181_Windows_VerifyProp65WarningDisplaysForCalifornia : T181_DesktopBase
    {
        public T181_Windows_VerifyProp65WarningDisplaysForCalifornia(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void Prop65WarnDisplaysForCalifornia(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T181_Mac_VerifyProp65WarningDisplaysForCalifornia : T181_DesktopBase
    {
        public T181_Mac_VerifyProp65WarningDisplaysForCalifornia(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void Prop65WarnDisplaysForCalifornia(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T181_iPad_VerifyProp65WarningDisplaysForCalifornia : T181_DesktopBase
    {
        public T181_iPad_VerifyProp65WarningDisplaysForCalifornia(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void Prop65WarnDisplaysForCalifornia(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T181_TabletEmulator_VerifyProp65WarningDisplaysForCalifornia : T181_DesktopBase
    {
        public T181_TabletEmulator_VerifyProp65WarningDisplaysForCalifornia(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void Prop65WarnDisplaysForCalifornia(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the California Prop 65 Warning' content displays for addresses in California only.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9999
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T181
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9999"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T181")]
    public abstract class T181_DesktopBase : TestsBaseDesktop
    {
        protected T181_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: Add any item to the cart and proceed to the Payment page.
            InitializeFramework(config);
            ShoppingCartWorkflow.ProceedToPaymentWithSingleProduct();

            //Assert: The Prop 65 Warning content displays for addresses in California.
            Assert.True(Payment.IsProp65WarningDialogVisible, "Prop 65 warning not displayed when state is California.");

            /*Act
             Navigate back to the Shipping page and change the shipping state to a state OTHER than California.
             Proceed to the Payment page.
             */
            ShoppingCartWorkflow.UpdateShippingStateFromPaymentPage(StateCodeListUnitedStates.ID);

            //Assert: The Prop 65 Warning content does not display.
            Assert.False(Payment.IsProp65WarningDialogVisible, "Prop 65 warning not displayed when state is California.");
        }
    }
}
