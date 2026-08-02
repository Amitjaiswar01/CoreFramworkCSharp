using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.Payment.T180_VerifyEditOrderLinkWorksCorrectly
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Payment)]
    public class T180_Windows_VerifyEditOrderLinkWorksCorrectly : T180_DesktopBase
    {
        public T180_Windows_VerifyEditOrderLinkWorksCorrectly(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void EditOrderLink(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Payment)]
    public class T180_Mac_VerifyEditOrderLinkWorksCorrectly : T180_DesktopBase
    {
        public T180_Mac_VerifyEditOrderLinkWorksCorrectly(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void EditOrderLink(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Payment)]
    public class T180_iPad_VerifyEditOrderLinkWorksCorrectly : T180_DesktopBase
    {
        public T180_iPad_VerifyEditOrderLinkWorksCorrectly(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void EditOrderLink(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Payment)]
    public class T180_TabletEmulator_VerifyEditOrderLinkWorksCorrectly : T180_DesktopBase
    {
        public T180_TabletEmulator_VerifyEditOrderLinkWorksCorrectly(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void EditOrderLink(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the 'Edit Order' link works correctly.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10005
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T180
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10005"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T180")]
    public abstract class T180_DesktopBase : TestsBaseDesktop
    {
        protected T180_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: Add any item to the cart and proceed to the Payment page.
            InitializeFunctionalTest(config);
            ShoppingCartWorkflow.ProceedToPaymentWithSingleProduct();

            //Act: Click on the "Edit Order" link in the Order Summary block.
            OrderSummaryBlock.NavigateBackToCartOverviewPage();

            //Assert: The user is re-directed to the 'Your Cart' page https://www.lampsplus.com/cart/
            Assert.PageUrl(Urls.CartOverviewPageUrl, Browser.PageUrl, "Did not redirect to cart page after clicking Edit Order link.");
        }
    }
}
