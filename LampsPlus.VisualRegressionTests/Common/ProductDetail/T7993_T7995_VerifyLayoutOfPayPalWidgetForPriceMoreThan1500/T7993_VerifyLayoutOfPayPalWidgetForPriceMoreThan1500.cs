using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail.T7993_T7995_VerifyLayoutOfPayPalWidgetForPriceMoreThan1500
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7993_Window_VerifyLayoutOfPayPalWidgetForPriceMoreThan1500 : T7993_DesktopBase
    {
        public T7993_Window_VerifyLayoutOfPayPalWidgetForPriceMoreThan1500(ITestOutputHelper output, T7993_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfPayPalWidget1500(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7993_Mac_VerifyLayoutOfPayPalWidgetForPriceMoreThan1500 : T7993_DesktopBase
    {
        public T7993_Mac_VerifyLayoutOfPayPalWidgetForPriceMoreThan1500(ITestOutputHelper output, T7993_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfPayPalWidget1500(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7993_iPad_VerifyLayoutOfPayPalWidgetForPriceMoreThan1500 : T7993_DesktopBase
    {
        public T7993_iPad_VerifyLayoutOfPayPalWidgetForPriceMoreThan1500(ITestOutputHelper output, T7993_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfPhotoModalInReviews(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7993_TabletEmulator_VerifyLayoutOfPayPalWidgetForPriceMoreThan1500 : T7993_DesktopBase
    {
        public T7993_TabletEmulator_VerifyLayoutOfPayPalWidgetForPriceMoreThan1500(ITestOutputHelper output, T7993_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfPayPalWidget1500(string config) => Validate(Validate, config);
    }


    public class T7993_SharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }
        public T7993_SharedSku_Fixture()
        {
            ShortSku = ProductActions.GetSkuWithPriceMoreThan1500;
        }
    }


    /// <summary>
    /// Verify Layout of the PayPal Widget For Product with Price More than $1,500
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10877
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7993
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10877"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T993")]
    public abstract class T7993_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7993_SharedSku_Fixture>
    {
        protected readonly T7993_SharedSku_Fixture Fixture;

        protected T7993_DesktopBase(ITestOutputHelper output, T7993_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            //Arrange : User has identified the SKU with a price more than $1500
            InitializeVisualTest(config);

            //Act : Navigate to PDP 
            ProductDetail.NavigateToProductDetailByShortSku(Fixture.ShortSku);
            Assert.True(ProductDetail.IsCurrentPage, "User is not on the PDP");

            //Act : Scroll to the PayPal Widget 
            Browser.ScrollIntoView(ProductDetail.GetPayPalLogo());

            //Act : Capture Visible Screen
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ProductDetail.IgnoreStockCheckWrapper() });

            //Act : Add product to Cart and scroll to Paypal Widget
            ProductDetail.AddToCart();
            Assert.True(Cart.IsCurrentPage, "User is not on the Cart page");

            //Act : Capture Visible Screen
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, Cart.IgnoreCartIdAndMoreYouMayLike(), true, true, null, 30 );
        }
    }
}