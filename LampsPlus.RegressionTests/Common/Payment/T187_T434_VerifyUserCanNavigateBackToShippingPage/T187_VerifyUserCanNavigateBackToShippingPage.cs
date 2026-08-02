using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Payment.T187_T434_VerifyUserCanNavigateBackToShippingPage
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T187_Windows_VerifyUserCanNavigateBackToShippingPage : T187_DesktopBase
    {
        public T187_Windows_VerifyUserCanNavigateBackToShippingPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void UserCanNavigateBackToShippingPage(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T187_Mac_VerifyUserCanNavigateBackToShippingPage : T187_DesktopBase
    {
        public T187_Mac_VerifyUserCanNavigateBackToShippingPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void UserCanNavigateBackToShippingPage(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T187_iPad_VerifyUserCanNavigateBackToShippingPage : T187_DesktopBase
    {
        public T187_iPad_VerifyUserCanNavigateBackToShippingPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void UserCanNavigateBackToShippingPage(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T187_TabletEmulator_VerifyUserCanNavigateBackToShippingPage : T187_DesktopBase
    {
        public T187_TabletEmulator_VerifyUserCanNavigateBackToShippingPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void UserCanNavigateBackToShippingPage(string config) => Validate(config);
    }


    /// <summary>
    /// Verify a user can navigate back to the Shipping page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10002
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T187
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10002"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T187")]
    public abstract class T187_DesktopBase : TestsBaseDesktop
    {
        protected T187_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: Add any item to the cart and proceed to the Payment page.
            InitializeFunctionalTest(config);
            ShoppingCartWorkflow.ProceedToPaymentWithSingleProduct();

            //Act: On the top of the Payment page, click on the SHIPPING breadcrumb in SHIPPING > PAYMENT
            ShoppingCartWorkflow.NavigateBackToShippingPageFromPaymentPage();

            //Assert: The user is re-directed to the Shipping page.
            Assert.PageUrl(Urls.ShippingPageUrl, Browser.PageUrl, "Did not redirect to shipping page after clicking Shipping link in breadcrumb.");
        }
    }
}
