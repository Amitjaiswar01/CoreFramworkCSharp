using xRetry;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.AddingToCartAndWishlist.T7358_T7363_VerifyLayoutOfWishListPage
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7363_iPhone_VerifyLayoutOfWishListPage : T7363_MobileBase
    {
        public T7363_iPhone_VerifyLayoutOfWishListPage(ITestOutputHelper output, T7363_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfWishListPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7363_Android_VerifyLayoutOfWishListPage : T7363_MobileBase
    {
        public T7363_Android_VerifyLayoutOfWishListPage(ITestOutputHelper output, T7363_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void VerifyLayoutOfWishListPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7363_Emulator_VerifyLayoutOfWishListPage : T7363_MobileBase
    {
        public T7363_Emulator_VerifyLayoutOfWishListPage(ITestOutputHelper output, T7363_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyLayoutOfWishListPage(string config) => Validate(Validate, config);
    }


    public class T7363_SharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }
        public string Quantity { get; }

        public T7363_SharedSku_Fixture()
        {
            ShortSku = ProductActions.GetFreeShippingAndReturnShortSkus;
            Quantity = MathHelper.GetRandomNumber(2, 9).ToString();
        }
    }


    /// <summary>
    /// Verify the Layout of the Wish List Page
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9894
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7363
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9894"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7363")]
    public abstract class T7363_MobileBase : VisualTestsBaseMobile, IClassFixture<T7363_SharedSku_Fixture>
    {
        protected readonly T7363_SharedSku_Fixture Fixture;

        protected T7363_MobileBase(ITestOutputHelper output, T7363_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            //Arrange : User has added an item with a quantity greater than 1 and that qualifies for Free Shipping and Free Returns to the Wish List.
            InitializeVisualTest(config);
            ProductDetail.NavigateToProductDetailByShortSku(Fixture.ShortSku);
            ProductDetail.ChangeProductQuantity(Fixture.Quantity);
            ProductDetail.AddToWishList();
            WishList.Navigate();
            Assert.True(WishList.IsCurrentPage, "User is not on the Wishlist page");

            //Act : Capture Screenshot of the Entire Page
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, true);

            //Act : On the Wish List page, tap on the 'Options' button.
            WishList.OpenWishListOptions();

            //Act : Capture Screenshot of the Visible Page
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture, true);
        }
    }
}