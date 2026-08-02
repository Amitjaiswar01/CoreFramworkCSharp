using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.DesktopTablet.AddingToCartAndWishlist
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7849_Windows_VerifyLayoutOfTheDeletingWishListModal : T7849_DesktopBase
    {
        public T7849_Windows_VerifyLayoutOfTheDeletingWishListModal(ITestOutputHelper output, T7849_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void LayoutOfTheDeletingWishListModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7849_Mac_VerifyLayoutOfTheDeletingWishListModal : T7849_DesktopBase
    {
        public T7849_Mac_VerifyLayoutOfTheDeletingWishListModal(ITestOutputHelper output, T7849_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI)]
        public void LayoutOfTheDeletingWishListModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7849_iPad_VerifyLayoutOfTheDeletingWishListModal : T7849_DesktopBase
    {
        public T7849_iPad_VerifyLayoutOfTheDeletingWishListModal(ITestOutputHelper output, T7849_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI)]
        public void LayoutOfTheDeletingWishListModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7849_TabletEmulator_VerifyLayoutOfTheDeletingWishListModal : T7849_DesktopBase
    {
        public T7849_TabletEmulator_VerifyLayoutOfTheDeletingWishListModal(ITestOutputHelper output, T7849_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI)]
        public void LayoutOfTheDeletingWishListModal(string config) => Validate(Validate, config);
    }


    public class T7849_SharedProductSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7849_SharedProductSku_Fixture()
        {
            ShortSku = ProductActions.GetAnySkuWithProductDetailPage;
        }
    }


    /// <summary>
    /// Verify the Layout of the Deleting Wish List Modal.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9649
    /// https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7849
    /// </summary>
    [Collection(LpTraits.UserRole.Customer)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9649"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7849")]

    public abstract class T7849_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7849_SharedProductSku_Fixture>
    {
        protected readonly T7849_SharedProductSku_Fixture Fixture;

        protected T7849_DesktopBase(ITestOutputHelper output, T7849_SharedProductSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            /*Arrange
            User has identified a SKU.
            User has navigated to the PDP page  
            Empty the wishlist
            Load the PDP page.
            Add the item in wishlist
            Navigate to wishlist
            */
            InitializeVisualTest(config);
            var randomSKU = Fixture.ShortSku;
            Assert.DatabaseObject(Fixture.ShortSku, "ProductionActions.GetAnySkuwithProductDetailPage");

            WishList.EmptyWishList();
            ProductDetail.NavigateToProductDetailByShortSku(randomSKU);

            Assert.True(ProductDetail.IsCurrentPage,"User is not on PDP");

            ProductDetail.AddToWishList();
            Browser.Navigate(Urls.WishListPageUrl);

            Assert.True(WishList.IsCurrentPage,"Current page is not Wishlist page");

            /*Act
            Click on Delete button
            Capture a screenshot of the Delete modal
            */
            WishList.DeleteWishListItems();

            ScreenCapturer.CaptureElementArea(Browser.PageUrl, Modal.GetLpModal());
        }
    }
}