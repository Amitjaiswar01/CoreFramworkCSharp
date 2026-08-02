using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.AddingToCartAndWishlist.T7359_T7580_VerifyLayoutOfWishListWithBYODimmerItem
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7359_Windows_VerifyLayoutOfWishListWithByoDimmerItem : T7359_DesktopBase
    {
        public T7359_Windows_VerifyLayoutOfWishListWithByoDimmerItem(ITestOutputHelper output, T7359_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfWishListWithByoDimmerItem(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7359_Mac_VerifyLayoutOfWishListWithByoDimmerItem : T7359_DesktopBase
    {
        public T7359_Mac_VerifyLayoutOfWishListWithByoDimmerItem(ITestOutputHelper output, T7359_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfWishListWithByoDimmerItem(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7359_iPad_VerifyLayoutOfWishListWithByoDimmerItem : T7359_DesktopBase
    {
        public T7359_iPad_VerifyLayoutOfWishListWithByoDimmerItem(ITestOutputHelper output, T7359_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfWishListWithByoDimmerItem(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7359_TabletEmulator_VerifyLayoutOfWishListWithByoDimmerItem : T7359_DesktopBase
    {
        public T7359_TabletEmulator_VerifyLayoutOfWishListWithByoDimmerItem(ITestOutputHelper output, T7359_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfWishListWithByoDimmerItem(string config) => Validate(Validate, config);
    }


    public class T7359_SharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }
        public int Quantity { get; }

        public T7359_SharedSku_Fixture()
        {
            ShortSku = ProductActions.GetProductWithBuildFullSystemSkus().PrimarySku;
            Quantity = MathHelper.GetRandomNumber(2, 5);
        }
    }

    /// <summary>
    /// Verify the layout of the Wish List page when adding a BYO Dimmer item.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8787
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7359
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8787"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7359")]
    public abstract class T7359_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7359_SharedSku_Fixture>
    {
        protected readonly T7359_SharedSku_Fixture Fixture;

        protected T7359_DesktopBase(ITestOutputHelper output, T7359_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            //Arrange: User is on a PDP that has a 'Build Full System' button.
            InitializeVisualTest(config);

            var sku = Fixture.ShortSku;
            Assert.DatabaseObject(sku, "ProductActions.GetProductWithBuildFullSystemSkus().Keys.First()");
            ProductDetail.NavigateToProductDetailByShortSku(sku);

            //Act: On the PDP with the 'Build Full System' button, add several items with a quantity greater than 1 from the 'Build Full System' section to the Wish List.
            ProductDetail.AddAllBuildFullSystemSkusToWishList(Fixture.Quantity);
            Browser.Wait.ForDomReady();
            Browser.Navigate(Urls.WishListPageUrl);
            Browser.RefreshPage(); 

            //Act: On the Wish List page, capture a screenshot of the entire page.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, true, true);
        }
    }
}
