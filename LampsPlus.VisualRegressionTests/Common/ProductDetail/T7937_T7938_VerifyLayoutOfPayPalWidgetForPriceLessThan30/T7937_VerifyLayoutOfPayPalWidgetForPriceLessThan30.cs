using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail.T7937_T7938_VerifyLayoutOfPayPalWidgetForPriceLessThan30
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7937_Windows_VerifyLayoutOfPayPalWidgetForPriceLessThan30 : T7937_DesktopBase
    {
        public T7937_Windows_VerifyLayoutOfPayPalWidgetForPriceLessThan30(ITestOutputHelper output, T7937_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfPayPalWidget30(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7937_Mac_VerifyLayoutOfPayPalWidgetForPriceLessThan30 : T7937_DesktopBase
    {
        public T7937_Mac_VerifyLayoutOfPayPalWidgetForPriceLessThan30(ITestOutputHelper output, T7937_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfPayPalWidget30(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7937_iPad_VerifyLayoutOfPayPalWidgetForPriceLessThan30 : T7937_DesktopBase
    {
        public T7937_iPad_VerifyLayoutOfPayPalWidgetForPriceLessThan30(ITestOutputHelper output, T7937_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfPayPalWidget30(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7937_TabletEmulator_VerifyLayoutOfPayPalWidgetForPriceLessThan30 : T7937_DesktopBase
    {
        public T7937_TabletEmulator_VerifyLayoutOfPayPalWidgetForPriceLessThan30(ITestOutputHelper output, T7937_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfPayPalWidget30(string config) => Validate(Validate, config);
    }


    public class T7937_SharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }
        public T7937_SharedSku_Fixture()
        {
            ShortSku = ProductActions.GetSkuWithPriceLessThan30;
        }
    }


    /// <summary>
    /// Verify Layout of the PayPal Widget For Product with Price Less than $30
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10875
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7937
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10875"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7937")]
    public abstract class T7937_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7937_SharedSku_Fixture>
    {
        protected readonly T7937_SharedSku_Fixture Fixture;

        protected T7937_DesktopBase(ITestOutputHelper output, T7937_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            //Arrange : User has identified the SKU with a price less than $30
            InitializeVisualTest(config);

            //Act : Navigate to PDP 
            ProductDetail.NavigateToProductDetailByShortSku(Fixture.ShortSku);
            Assert.True(ProductDetail.IsCurrentPage, "User is not on the PDP");

            //Act : Scroll to the PayPal Widget 
            Browser.ScrollIntoView(ProductDetail.GetPayPalLogo());

            //Act : Capture Visible Screen
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement> {ProductDetail.IgnoreStockCheckWrapper()});

            //Act : Add product to Cart and scroll to Paypal Widget
            ProductDetail.AddToCart();
            Assert.True(Cart.IsCurrentPage, "User is not on the Cart page");

            //Act : Capture Visible Screen
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, Cart.IgnoreCartIdAndMoreYouMayLike(), false, true, Cart.IgnoreCartId(), 20);
        }
    }
}