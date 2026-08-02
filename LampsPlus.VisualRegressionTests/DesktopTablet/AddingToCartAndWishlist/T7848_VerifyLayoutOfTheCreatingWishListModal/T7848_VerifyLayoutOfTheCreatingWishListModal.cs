using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.DesktopTablet.AddingToCartAndWishList.T7848_VerifyLayoutOfTheCreatingWishListModal
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7848_Windows_VerifyLayoutOfTheCreatingWishListModal : T7848_DesktopBase
    {
        public T7848_Windows_VerifyLayoutOfTheCreatingWishListModal(ITestOutputHelper output, T7848_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void VerifyLayoutOfTheCreatingWishListModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7848_Mac_VerifyLayoutOfTheCreatingWishListModal : T7848_DesktopBase
    {
        public T7848_Mac_VerifyLayoutOfTheCreatingWishListModal(ITestOutputHelper output, T7848_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI)]
        public void VerifyLayoutOfTheCreatingWishListModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7848_iPad_VerifyLayoutOfTheCreatingWishListModal : T7848_DesktopBase
    {
        public T7848_iPad_VerifyLayoutOfTheCreatingWishListModal(ITestOutputHelper output, T7848_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI)]
        public void LVerifyLayoutOfTheCreatingWishListModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7848_TabletEmulator_VerifyLayoutOfTheCreatingWishListModal : T7848_DesktopBase
    {
        public T7848_TabletEmulator_VerifyLayoutOfTheCreatingWishListModal(ITestOutputHelper output, T7848_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI)]
        public void VerifyLayoutOfTheCreatingWishListModal(string config) => Validate(Validate, config);
    }


    public class T7848_SharedProductSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7848_SharedProductSku_Fixture()
        {
            ShortSku = ProductActions.GetAnySkuWithProductDetailPage;
        }
    }


    /// <summary>
    /// Verify Layout Of The Creating Wish List Modal.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9648
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7848
    /// </summary>
    [Collection(LpTraits.UserRole.Customer)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9648"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7848")]

    public abstract class T7848_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7848_SharedProductSku_Fixture>
    {
        protected readonly T7848_SharedProductSku_Fixture Fixture;

        protected T7848_DesktopBase(ITestOutputHelper output, T7848_SharedProductSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            /* Arrangement
            User is signed in as a consumer
            User has cleared a existing Wish List
            User has added item to Wish List
            User is on the Wish List page
            */
            InitializeVisualTest(config);
            Assert.DatabaseObject(Fixture.ShortSku, "ProductionActions.GetAnySkuWithProductDetailPage");
            WishList.EmptyWishList();
            ProductDetail.NavigateToProductDetailByShortSku(Fixture.ShortSku);
            Assert.True(ProductDetail.IsCurrentPage, "User is not on PDP.");
            ProductDetail.AddToWishList();
            Browser.Navigate(Urls.WishListPageUrl);

            // Act: User has named Wish List.
            var createWishListName = $"{WishListTypes.WishListNames.CreateWishList}{DateTime.Now}";
            WishList.CreateWishList(createWishListName);

            // Act: Capture a screenshot of the entire visible screen.
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { WishList.IgnoreWishListName()});

            // Act. On the Wish List page, click on the "Open List" button.
            WishList.OpenWishList();

            // Act: Capture a screenshot of the entire visible screen.
            ScreenCapturer.CaptureElementAreaWithIgnoredLayouts(Browser.PageUrl, Modal.GetLpModal(), new List<IElement> { WishList.IgnoreOpenList() } );
        }
    }
}
