using System;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.AddingToCartAndWishlist.T7360_T7987_VerifyLayoutOfWishListRenaming
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7360_Windows_VerifyLayoutOfWishListRenaming : T7360_DesktopBase
    {
        public T7360_Windows_VerifyLayoutOfWishListRenaming(ITestOutputHelper output, T7360_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void LayoutOfRenameWishList(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7360_Mac_VerifyLayoutOfWishListRenaming : T7360_DesktopBase
    {
        public T7360_Mac_VerifyLayoutOfWishListRenaming(ITestOutputHelper output, T7360_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfRenameWishList(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7360_iPad_VerifyLayoutOfWishListRenaming : T7360_DesktopBase
    {
        public T7360_iPad_VerifyLayoutOfWishListRenaming(ITestOutputHelper output, T7360_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfRenameWishList(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7360_TabletEmulator_VerifyLayoutOfWishListRenaming : T7360_DesktopBase
    {
        public T7360_TabletEmulator_VerifyLayoutOfWishListRenaming(ITestOutputHelper output, T7360_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfRenameWishList(string config) => Validate(Validate, config);
    }


    public class T7360_SharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }
        public string WishlistName { get; }
        public T7360_SharedSku_Fixture()
        {
            ShortSku = ProductActions.GetAnySkuWithProductDetailPage;
            WishlistName = "New Wish List" + DateTime.Now;
        }
    }

    /// <summary>
    /// Verify the Layout of the Wish List Renaming
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9893
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7360
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9893"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7360")]
    public abstract class T7360_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7360_SharedSku_Fixture>
    {
        protected readonly T7360_SharedSku_Fixture Fixture;

        protected T7360_DesktopBase(ITestOutputHelper output, T7360_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            /*Arrange:
            User has added a Random item to WishList
            User has navigated to the WishList 
            */
            InitializeVisualTest(config);
            WishList.EmptyWishList();
            ProductDetail.NavigateToProductDetailByShortSku(Fixture.ShortSku);
            Assert.True(ProductDetail.IsCurrentPage, "User is not on Product Detail page");
            WishListWorkflow.AddToWishlistAndVerifyCount();
            WishList.Navigate();
            Assert.True(WishList.IsCurrentPage, "User is not on Wishlist page");
            
            //Act : User has selected the pencil icon and captured the screenshot of the visible region
            WishList.SelectPencilIcon();
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            //Act : User has Renamed the Wishlist and Captured the screenshot of the entire page.
            Browser.RefreshPage();
            WishList.RenameWishList(Fixture.WishlistName);
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture);

            //Data Clean-up
            WishList.DeleteWishList();
        }
    }
}