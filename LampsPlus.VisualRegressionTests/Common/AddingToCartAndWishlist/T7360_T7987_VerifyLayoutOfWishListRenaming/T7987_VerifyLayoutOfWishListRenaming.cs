using System;
using xRetry;
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
    public class T7987_iPhone_VerifyLayoutOfWishListRenaming : T7987_MobileBase
    {
        public T7987_iPhone_VerifyLayoutOfWishListRenaming(ITestOutputHelper output, T7987_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
        public void LayoutOfRenameWishList(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7987_Android_VerifyLayoutOfWishListRenaming : T7987_MobileBase
    {
        public T7987_Android_VerifyLayoutOfWishListRenaming(ITestOutputHelper output, T7987_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfRenameWishList(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7987_Emulator_VerifyLayoutOfWishListRenaming : T7987_MobileBase
    {
        public T7987_Emulator_VerifyLayoutOfWishListRenaming(ITestOutputHelper output, T7987_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfRenameWishList(string config) => Validate(Validate, config);
    }


    public class T7987_SharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }
        public string WishlistName { get; }

        public T7987_SharedSku_Fixture()
        {
            ShortSku = ProductActions.GetAnySkuWithProductDetailPage;
            WishlistName = "New Wish List" + DateTime.Now;
        }
    }


    /// <summary>
    /// Verify the Layout of the Wish List Renaming.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9893
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7987
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9893"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T9897")]
    public abstract class T7987_MobileBase : VisualTestsBaseMobile, IClassFixture<T7987_SharedSku_Fixture>
    {
        protected readonly T7987_SharedSku_Fixture Fixture;

        protected T7987_MobileBase(ITestOutputHelper output, T7987_SharedSku_Fixture fixture) : base(output, fixture)
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
            ProductDetail.AddToWishList();
            WishList.Navigate();
            Assert.True(WishList.IsCurrentPage, "user is not on Wishlist page");

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
