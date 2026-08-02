using xRetry;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.AddingToCartAndWishlist.T7359_T7580_VerifyLayoutOfWishListWithBYODimmerItem
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7580_iPhone_VerifyLayoutOfWishListWithByoDimmerItem : T7580_MobileBase
    {
        public T7580_iPhone_VerifyLayoutOfWishListWithByoDimmerItem(ITestOutputHelper output, T7580_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void LayoutOfWishListWithByoDimmerItem(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7580_Android_VerifyLayoutOfWishListWithByoDimmerItem : T7580_MobileBase
    {
        public T7580_Android_VerifyLayoutOfWishListWithByoDimmerItem(ITestOutputHelper output, T7580_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfWishListWithByoDimmerItem(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7580_Emulator_VerifyLayoutOfWishListWithByoDimmerItem : T7580_MobileBase
    {
        public T7580_Emulator_VerifyLayoutOfWishListWithByoDimmerItem(ITestOutputHelper output, T7580_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfWishListWithByoDimmerItem(string config) => Validate(Validate, config);
    }


    public class T7580_SharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }
        public int Quantity { get; }
        public T7580_SharedSku_Fixture()
        {
            ShortSku = ProductActions.GetProductWithBuildFullSystemSkus().PrimarySku;
            Quantity = MathHelper.GetRandomNumber(2, 5);
        }
    }


    /// <summary>
    /// Verify the layout of the Wish List page when adding a BYO Dimmer item.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8787
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7580
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8787"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7580")]
    public abstract class T7580_MobileBase : VisualTestsBaseMobile, IClassFixture<T7580_SharedSku_Fixture>
    {
        protected readonly T7580_SharedSku_Fixture Fixture;

        protected T7580_MobileBase(ITestOutputHelper output, T7580_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            //Act: User is on a PDP that has a 'Build Full System' button.
            InitializeVisualTest(config);

            var sku = Fixture.ShortSku;
            Assert.DatabaseObject(sku, "ProductActions.GetProductWithBuildFullSystemSkus().Keys.First()");
            ProductDetail.NavigateToProductDetailByShortSku(sku);

            /*Act
            On the PDP with the 'Build Full System' drawer, open the drawer and add several items with a quantity greater than 1 from the 'Build Full System' section to the Wish List.
            /Navigate to the Wish List.
            */
            ProductDetail.AddAllBuildFullSystemSkusToWishList(Fixture.Quantity);
            Assert.True(WishList.IsCurrentPage, "Current page is not WishList page");

            //Assert: On the Wish List page, capture a screenshot of the entire page.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, true, true);
        }
    }
}
