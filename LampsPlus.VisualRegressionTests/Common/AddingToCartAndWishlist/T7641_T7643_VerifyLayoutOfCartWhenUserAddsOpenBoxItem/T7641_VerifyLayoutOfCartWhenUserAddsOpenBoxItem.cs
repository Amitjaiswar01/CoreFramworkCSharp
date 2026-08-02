using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.AddingToCartAndWishlist.T7641_T7643_VerifyLayoutOfCartWhenUserAddsOpenBoxItem
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7641_Windows_VerifyLayoutOfCartWhenUserAddsOpenBoxItem : T7641_DesktopBase
    {
        public T7641_Windows_VerifyLayoutOfCartWhenUserAddsOpenBoxItem(ITestOutputHelper output, T7641_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfCartOpenBoxItemAddedToCart(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7641_Mac_VerifyLayoutOfCartWhenUserAddsOpenBoxItem : T7641_DesktopBase
    {
        public T7641_Mac_VerifyLayoutOfCartWhenUserAddsOpenBoxItem(ITestOutputHelper output, T7641_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfCartOpenBoxItemAddedToCart(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7641_iPad_VerifyLayoutOfCartWhenUserAddsOpenBoxItem : T7641_DesktopBase
    {
        public T7641_iPad_VerifyLayoutOfCartWhenUserAddsOpenBoxItem(ITestOutputHelper output, T7641_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfCartOpenBoxItemAddedToCart(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7641_TabletEmulator_VerifyLayoutOfCartWhenUserAddsOpenBoxItem : T7641_DesktopBase
    {
        public T7641_TabletEmulator_VerifyLayoutOfCartWhenUserAddsOpenBoxItem(ITestOutputHelper output, T7641_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfCartOpenBoxItemAddedToCart(string config) => Validate(Validate, config);
    }


    public class T7641_SharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7641_SharedSku_Fixture()
        {
            ShortSku = ProductActions.GetOpenBoxShortSku;
        }
    }

    /// <summary>
    /// Verify the layout of the Cart when a user adds an Open Box item.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9892
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7641
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9892"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7641")]
    public abstract class T7641_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7641_SharedSku_Fixture>
    {
        protected readonly T7641_SharedSku_Fixture Fixture;

        protected T7641_DesktopBase(ITestOutputHelper output, T7641_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            //Arrange: User has identified an Open Box item using the query.
            InitializeVisualTest(config);
            var sku = Fixture.ShortSku;

            //Act: User has navigated to the PDP of the identified SKU.
            ProductDetail.NavigateToOpenBoxProductDetailByShortSku(sku);
            Assert.True(ProductDetail.IsCurrentPage, "user is not on Product Detail Page");

            //Act : User has captured the screenshot of the visible page.
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ProductDetail.IgnoreStockCheckWrapper() });

            //Act : User has added the product to the cart.
            ProductDetail.AddToCart();
            Assert.True(Cart.IsCurrentPage, "User is not on cart page");

            //Act : User has captured the screenshot of the visible page.
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { Cart.IgnoreCartId() }, offset:10);
        }
    }
}
