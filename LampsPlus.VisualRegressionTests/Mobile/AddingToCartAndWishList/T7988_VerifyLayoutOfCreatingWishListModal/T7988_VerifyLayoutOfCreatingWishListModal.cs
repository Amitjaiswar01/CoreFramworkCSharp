using System;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Mobile.AddingToCartAndWishlist.T7988_VerifyLayoutOfCreatingWishListModal
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7988_iPhone_VerifyLayoutOfCreatingWishListModal : T7988_MobileBase
    {
        public T7988_iPhone_VerifyLayoutOfCreatingWishListModal(ITestOutputHelper output, T7988_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
        public void LayoutOfCreatingWishListModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7988_Android_VerifyLayoutOfCreatingWishListModal : T7988_MobileBase
    {
        public T7988_Android_VerifyLayoutOfCreatingWishListModal(ITestOutputHelper output, T7988_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_NPCSI)]
        public void LayoutOfCreatingWishListModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7988_Emulator_VerifyLayoutOfCreatingWishListModal : T7988_MobileBase
    {
        public T7988_Emulator_VerifyLayoutOfCreatingWishListModal(ITestOutputHelper output, T7988_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void LayoutOfCreatingWishListModal(string config) => Validate(Validate, config);
    }


    public class T7988_SharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }
        public string WishlistName { get; }

        public T7988_SharedSku_Fixture()
        {
            ShortSku = ProductActions.GetAnySkuWithProductDetailPage;
            WishlistName = "New Wish List" + DateTime.Now;
        }
    }


    /// <summary>
    /// Verify the Layout of the Creating Wish List Modal.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10850
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7988
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10850"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7988")]
    public abstract class T7988_MobileBase : VisualTestsBaseMobile, IClassFixture<T7988_SharedSku_Fixture>
    {
        protected readonly T7988_SharedSku_Fixture Fixture;

        protected T7988_MobileBase(ITestOutputHelper output, T7988_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            /* Arrange : 
            User has added any random item to the Wish List.
            Navigate to the wish-list page.
            */
            InitializeVisualTest(config);
            var sku = Fixture.ShortSku;
            ProductDetail.NavigateToProductDetailByShortSku(sku);
            ProductDetail.AddToWishList();

            /* Act : 
            Select the 'Create New List' option from the menu.
            Capture a screenshot of the visible screen.
             */
            WishList.OpenCreateNewListOption();
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            /* Act :
            Enter in a name for a Wish List and tap the 'Create Wish List' button.
            Once the page loads, tap on the 'Options' button again and select 'Open List'.
            Capture a screenshot of the visible screen.
            */
            WishList.EnterNameForCreateNewWishList(Fixture.WishlistName);
            WishList.OpenWishList();
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            // Data Clean-up 
            WishList.DeleteWishList();
            WishList.OpenNewWishList(0); 
            WishList.DeleteWishList();
        }
    }
}