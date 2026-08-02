using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Payment.T184_T431_VerifyPurchaseOrderOptionNotDisplayed
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T184_Windows_VerifyPurchaseOrderOptionNotDisplayed : T184_DesktopBase
    {
        public T184_Windows_VerifyPurchaseOrderOptionNotDisplayed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void PurchaseOrderOptionNotDisplayed(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T184_Mac_VerifyPurchaseOrderOptionNotDisplayed : T184_DesktopBase
    {
        public T184_Mac_VerifyPurchaseOrderOptionNotDisplayed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void PurchaseOrderOptionNotDisplayed(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T184_iPad_VerifyPurchaseOrderOptionNotDisplayed : T184_DesktopBase
    {
        public T184_iPad_VerifyPurchaseOrderOptionNotDisplayed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void PurchaseOrderOptionNotDisplayed(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T184_TabletEmulator_VerifyPurchaseOrderOptionNotDisplayed : T184_DesktopBase
    {
        public T184_TabletEmulator_VerifyPurchaseOrderOptionNotDisplayed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void PurchaseOrderOptionNotDisplayed(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the payment option for P.O. is NOT available.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9996
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T184
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9996"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T184")]
    public abstract class T184_DesktopBase : TestsBaseDesktop
    {
        protected T184_DesktopBase(ITestOutputHelper output) : base(output) { }

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
