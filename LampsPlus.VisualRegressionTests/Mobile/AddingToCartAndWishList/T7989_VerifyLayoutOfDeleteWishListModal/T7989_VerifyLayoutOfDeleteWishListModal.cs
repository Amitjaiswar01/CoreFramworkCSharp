using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Mobile.AddingToCartAndWishlist.T7989_VerifyLayoutOfDeleteWishListModal
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7989_iPhone_VerifyLayoutOfDeleteWishListModal : T7989_MobileBase
    {
        public T7989_iPhone_VerifyLayoutOfDeleteWishListModal(ITestOutputHelper output, T7989_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
        public void LayoutOfDeleteWishListModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7989_Android_VerifyLayoutOfDeleteWishListModal : T7989_MobileBase
    {
        public T7989_Android_VerifyLayoutOfDeleteWishListModal(ITestOutputHelper output, T7989_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_NPCSI)]
        public void LayoutOfDeleteWishListModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7989_Emulator_VerifyLayoutOfDeleteWishListModal : T7989_MobileBase
    {
        public T7989_Emulator_VerifyLayoutOfDeleteWishListModal(ITestOutputHelper output, T7989_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void LayoutOfDeleteWishListModal(string config) => Validate(Validate, config);
    }


    public class T7989_SharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7989_SharedSku_Fixture()
        {
            ShortSku = ProductActions.GetAnySkuWithProductDetailPage;
        }
    }


    /// <summary>
    /// Verify the Layout of the Deleting Wish List Modal.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10850
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7989
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10850"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-7989")]
    public abstract class T7989_MobileBase : VisualTestsBaseMobile, IClassFixture<T7989_SharedSku_Fixture>
    {
        protected readonly T7989_SharedSku_Fixture Fixture;

        protected T7989_MobileBase(ITestOutputHelper output, T7989_SharedSku_Fixture fixture) : base(output, fixture)
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
            Assert.True(WishList.IsCurrentPage, "User is not on the WishList page");

            //Act : Tap on the 'Options' button and select 'Delete List'
            WishList.OpenDeleteWishListModal();

            //Act : Capture a screenshot of the visible screen
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}