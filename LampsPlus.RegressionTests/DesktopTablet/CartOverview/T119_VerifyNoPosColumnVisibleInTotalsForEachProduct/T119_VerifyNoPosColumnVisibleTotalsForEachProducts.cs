using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.CartOverview.T119_VerifyNoPosColumnVisibleInTotalsForEachProduct
{
    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.CartOverview)]
    public class T119_Windows_VerifyNoPosColumnVisibleTotalsForEachProducts : T119_DesktopBase
    {
        public T119_Windows_VerifyNoPosColumnVisibleTotalsForEachProducts(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void VerifyNoPosColumnIsVisible(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.CustomerKiosk)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.CartOverview)]
    public class T119_Windows_Kiosk_VerifyNoPosColumnVisibleTotalsForEachProducts : T119_DesktopBase
    {
        public T119_Windows_Kiosk_VerifyNoPosColumnVisibleTotalsForEachProducts(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_UNSI)]
        public void VerifyNoPosColumnIsVisible(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T119_Mac_VerifyNoPosColumnVisibleTotalsForEachProducts : T119_DesktopBase
    {
        public T119_Mac_VerifyNoPosColumnVisibleTotalsForEachProducts(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void VerifyNoPosColumnIsVisible(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T119_iPad_VerifyNoPosColumnVisibleTotalsForEachProducts : T119_DesktopBase
    {
        public T119_iPad_VerifyNoPosColumnVisibleTotalsForEachProducts(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void VerifyNoPosColumnIsVisible(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T119_TabletEmulator_VerifyNoPosColumnVisibleTotalsForEachProducts : T119_DesktopBase
    {
        public T119_TabletEmulator_VerifyNoPosColumnVisibleTotalsForEachProducts(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void VerifyNoPosColumnIsVisible(string config) => Validate(config);
    }


    /// <summary>
    /// Verify no 'POS' column is visible in the totals for each item 
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9925
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T119 
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9925"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T119")]
    [Trait(LpTraits.Keys.Category, LpTraits.Categories.CRUD)]
    public abstract class T119_DesktopBase : TestsBaseDesktop
    {
        protected T119_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            /*Arrange:
            Ensure to Empty the cart
            Add the products to the cart page
            */
            InitializeFunctionalTest(config);

            ShoppingCartWorkflow.EmptyCart();
            ShoppingCartWorkflow.AddMultipleItemsToCart(Urls.ContemporaryFloorLampsSortPageUrl, 2);

            /*Assert:
            Verify the POS label is not displayed
            Verify the POS Link is not displayed
            */
            Assert.False(Cart.IsPosLabelVisible, "POS check box label is displayed.");
            Assert.False(Cart.IsAllPosLinkVisible, "POS link is displayed.");
        }
    }
}
