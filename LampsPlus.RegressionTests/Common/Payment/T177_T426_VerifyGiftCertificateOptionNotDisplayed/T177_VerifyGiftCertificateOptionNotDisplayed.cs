using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Payment.T177_T426_VerifyGiftCertificateOptionNotDisplayed
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T177_Windows_VerifyGiftCertificateOptionNotDisplayed : T177_DesktopBase
    {
        public T177_Windows_VerifyGiftCertificateOptionNotDisplayed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void GiftCertificateOptionNotDisplayed(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T177_Mac_VerifyGiftCertificateOptionNotDisplayed : T177_DesktopBase
    {
        public T177_Mac_VerifyGiftCertificateOptionNotDisplayed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void GiftCertificateOptionNotDisplayed(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T177_iPad_VerifyGiftCertificateOptionNotDisplayed : T177_DesktopBase
    {
        public T177_iPad_VerifyGiftCertificateOptionNotDisplayed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void GiftCertificateOptionNotDisplayed(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T177_TabletEmulator_VerifyGiftCertificateOptionNotDisplayed : T177_DesktopBase
    {
        public T177_TabletEmulator_VerifyGiftCertificateOptionNotDisplayed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void GiftCertificateOptionNotDisplayed(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the Gift Certificate payment option isn't available for certain user roles.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9997
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T177
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9997"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T177")]
    public abstract class T177_DesktopBase : TestsBaseDesktop
    {
        protected T177_DesktopBase(ITestOutputHelper output) : base(output) { }

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
