using Xunit;
using Xunit.Abstractions;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.RoomViewer.T7860_VerifyCustomerCanCreateSampleRoomForTwoSkus
{
    //[Collection(LpTraits.UserRole.Anonymous)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.AugmentedReality)]
    public class T7860_Windows_VerifyCustomerCanCreateSampleRoomForTwoSkus : T7860_DesktopBase
    {
        public T7860_Windows_VerifyCustomerCanCreateSampleRoomForTwoSkus(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void CustomerCanCreateSampleRoomForTwoSkus(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.AugmentedReality)]
    public class T7860_Mac_VerifyCustomerCanCreateSampleRoomForTwoSkus : T7860_DesktopBase
    {
        public T7860_Mac_VerifyCustomerCanCreateSampleRoomForTwoSkus(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Zephyr: T7860. Rework - ACD-10934")]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void CustomerCanCreateSampleRoomForTwoSkus(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.AugmentedReality)]
    public class T7860_iPad_VerifyCustomerCanCreateSampleRoomForTwoSkus : T7860_DesktopBase
    {
        public T7860_iPad_VerifyCustomerCanCreateSampleRoomForTwoSkus(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void CustomerCanCreateSampleRoomForTwoSkus(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.AugmentedReality)]
    public class T7860_TabletEmulator_VerifyCustomerCanCreateSampleRoomForTwoSkus : T7860_DesktopBase
    {
        public T7860_TabletEmulator_VerifyCustomerCanCreateSampleRoomForTwoSkus(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void CustomerCanCreateSampleRoomForTwoSkus(string config) => Validate(config);
    }


    /// <summary>
    /// Verify That Customer can Create A Same Sample Room for Two Different SKU's
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10241
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7860
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10241"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7860")]
    public abstract class T7860_DesktopBase : TestsBaseDesktop
    {
        protected T7860_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange - User has added 2 different products to the room
            InitializeFunctionalTest(config);
            var shortSku = ProductActions.GetSkuThatHasArOption.ToLower();
            var shortSkus = ProductActions.GetSkusThatHaveArOption();
            Assert.DatabaseObject(shortSku, "ProductActions.GetSkuThatHasArOption");
            Assert.DatabaseObject(shortSkus, "ProductActions.GetSkusThatHaveArOption");

            var productsInDbList = RoomViewer.dataBaseList(shortSkus);
            RoomViewerWorkflow.AddMultipleItemsToRoom(shortSkus.ArProducts);

            Assert.True(RoomViewer.IsCurrentPage, "Current page is not room viewer page");

            var firstProductNameInDb = productsInDbList[0].ProductName;
            var secondProductNameInDb = productsInDbList[1].ProductName;
            var productsCount = shortSkus.ArProducts.Count;
            var productsInRoomViewer = RoomViewer.GetListOfAllProductsOnRoomViewer();

            /*Assert:
            The title should state as: "2 Products In This Room"
            The correct products have been added to the room
            Selected product thumbnail should appears in the top left corner
            */
            Assert.Equals(Messages.ArMessages.ArPageTitle, RoomViewer.GetTitleOfArPage(), "Title is not correct");
            Assert.Equals(($"{productsCount}"), RoomViewer.GetProductListCount(), "The product count does not match");
            Assert.Equals(TextActions.NormalizeWhitespace(RoomViewer.GetProductNameByShortSkuFromDb(firstProductNameInDb)), TextActions.NormalizeWhitespace(productsInRoomViewer[0].Name), "The product name does not match");
            Assert.Equals(TextActions.NormalizeWhitespace(RoomViewer.GetProductNameByShortSkuFromDb(secondProductNameInDb)), TextActions.NormalizeWhitespace(productsInRoomViewer[1].Name), "The product name does not match");
            Assert.Equals(decimal.Round(productsInDbList[0].RetailPriceInternet, 2), decimal.Parse(productsInRoomViewer[0].Price), "Price does not match");
            Assert.Equals(decimal.Round(productsInDbList[1].RetailPriceInternet, 2), decimal.Parse(productsInRoomViewer[1].Price), "Price does not match");
            Assert.Equals(RoomViewer.GetFirstProductHref(0), RoomViewer.GetArCanvasHref(1), "Selected product thumbnail images is not appears in the top left corner");
            Assert.Equals(RoomViewer.GetSecondProductHref(1), RoomViewer.GetArCanvasHref(2), "Selected product thumbnail images is not appears in the top left corner");
        }
    }
}