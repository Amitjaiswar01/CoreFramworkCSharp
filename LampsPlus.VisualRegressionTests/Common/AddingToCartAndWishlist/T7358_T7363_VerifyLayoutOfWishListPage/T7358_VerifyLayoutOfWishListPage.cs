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
    public class T7358_Windows_VerifyLayoutOfWishListPage : T7358_DesktopBase
    {
        public T7358_Windows_VerifyLayoutOfWishListPage(ITestOutputHelper output, T7358_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory] 
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyLayoutOfWishListPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7358_Mac_VerifyLayoutOfWishListPage : T7358_DesktopBase
    {
        public T7358_Mac_VerifyLayoutOfWishListPage(ITestOutputHelper output, T7358_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfWishListPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7358_iPad_VerifyLayoutOfWishListPage : T7358_DesktopBase
    {
        public T7358_iPad_VerifyLayoutOfWishListPage(ITestOutputHelper output, T7358_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfWishListPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7358_TabletEmulator_VerifyLayoutOfWishListPage : T7358_DesktopBase
    {
        public T7358_TabletEmulator_VerifyLayoutOfWishListPage(ITestOutputHelper output, T7358_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyLayoutOfWishListPage(string config) => Validate(Validate, config);
    }


    public class T7358_SharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }
        public string Quantity { get; }

        public T7358_SharedSku_Fixture()
        {
            ShortSku = ProductActions.GetFreeShippingAndReturnShortSkus;
            Quantity = MathHelper.GetRandomNumber(2, 9).ToString();
        }
    }

    /// <summary>
    /// Verify the Layout of the Wish List Page
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9894
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7358
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9894"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7358")]
    public abstract class T7358_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7358_SharedSku_Fixture>
    {
        protected readonly T7358_SharedSku_Fixture Fixture;

        protected T7358_DesktopBase(ITestOutputHelper output, T7358_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            //Arrange: User has identified an item with a quantity greater than 1 and that qualifies for Free Shipping and Free Returns.
            InitializeVisualTest(config);

            //Act : Navigate to the PDP of the item from the query in the preconditions 
            ProductDetail.NavigateToProductDetailByShortSku(Fixture.ShortSku);

            //Act : Select the Add to Wish List button  with a quantity greater than 1
            ProductDetail.ChangeProductQuantity(Fixture.Quantity);
            WishListWorkflow.AddToWishlistAndVerifyCount();
            WishList.Navigate();
            Assert.True(WishList.IsCurrentPage, "User is on the Wishlist page");

            //Act : Capture a Screenshot of Visible Page
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}