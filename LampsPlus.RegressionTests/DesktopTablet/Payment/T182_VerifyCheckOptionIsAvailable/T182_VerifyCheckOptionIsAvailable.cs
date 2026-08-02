using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.Payment.T182_VerifyCheckOptionIsAvailable
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Payment)]
    public class T182_Windows_VerifyCheckOptionIsAvailable : T182_DesktopBase
    {
        public T182_Windows_VerifyCheckOptionIsAvailable(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void CheckOptionIsAvailable(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Payment)]
    public class T182_Mac_VerifyCheckOptionIsAvailable : T182_DesktopBase
    {
        public T182_Mac_VerifyCheckOptionIsAvailable(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void CheckOptionIsAvailable(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Payment)]
    public class T182_iPad_VerifyCheckOptionIsAvailable : T182_DesktopBase
    {
        public T182_iPad_VerifyCheckOptionIsAvailable(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void CheckOptionIsAvailable(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Payment)]
    public class T182_TabletEmulator_VerifyCheckOptionIsAvailable : T182_DesktopBase
    {
        public T182_TabletEmulator_VerifyCheckOptionIsAvailable(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void CheckOptionIsAvailable(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the payment option for checks is available.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10004
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T182
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10004"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T182"), Trait(LpTraits.Keys.Category, LpTraits.Categories.CRUD)]
    public abstract class T182_DesktopBase : TestsBaseDesktop
    {
        protected T182_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            /*Arrange:
             Add an item to the cart.
             Select an option from the 'Sale Source' dropdown.
             Proceed through the order flow to the Payment page.
             */
            InitializeFunctionalTest(config);
            ShoppingCartWorkflow.EmployeeProceedToPaymentPageWithSingleItem();

            //Act: On the Payment page, click the radio button next to the option 'Check'.
            Payment.SelectCheckPaymentOption();

            //Assert: When the 'Check' radio button is selected, the billing address appears for the user and there is a field called 'Check Number'.
            Assert.True(Payment.IsPaymentTypeAvailable(PaymentType.PaperCheck), "Check Payment Type should be displayed.");
        }
    }
}
