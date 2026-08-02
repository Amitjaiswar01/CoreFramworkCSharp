using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using Automation.Framework;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail.T7937_T7938_VerifyLayoutOfPayPalWidgetForPriceLessThan30
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7938_iPhone_VerifyLayoutOfPayPalWidgetForPriceLessThan30 : T7938_MobileBase
    {
        public T7938_iPhone_VerifyLayoutOfPayPalWidgetForPriceLessThan30(ITestOutputHelper output, T7938_ShareSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void LayoutOfPayPalWidget30(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7938_Android_VerifyLayoutOfPayPalWidgetForPriceLessThan30 : T7938_MobileBase
    {
        public T7938_Android_VerifyLayoutOfPayPalWidgetForPriceLessThan30(ITestOutputHelper output, T7938_ShareSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfPayPalWidget30(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7938_Emulator_VerifyLayoutOfPayPalWidgetForPriceLessThan30 : T7938_MobileBase
    {
        public T7938_Emulator_VerifyLayoutOfPayPalWidgetForPriceLessThan30(ITestOutputHelper output, T7938_ShareSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfPayPalWidget30(string config) => Validate(Validate, config);
    }


    public class T7938_ShareSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7938_ShareSku_Fixture()
        {
            ShortSku = ProductActions.GetSkuWithPriceLessThan30;
        }
    }


    /// <summary>
    /// Verify Layout of the PayPal Widget For Product with Price Less than $30
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10875
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7938
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10875"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7938")]
    public abstract class T7938_MobileBase : VisualTestsBaseMobile, IClassFixture<T7938_ShareSku_Fixture>
    {
        protected readonly T7938_ShareSku_Fixture Fixture;

        protected T7938_MobileBase(ITestOutputHelper output, T7938_ShareSku_Fixture fixture) : base(output, fixture)
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
            Browser.ScrollIntoView(ProductDetail.GetPayPalLogo(), true);

            //Act : Capture Visible Screen
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ProductDetail.IgnoreStockCheckWrapper(), ProductDetail.IgnoreCertonaDrawerName() });

            //Act : Add product to Cart and scroll to Paypal Widget
            ProductDetail.AddToCart();
            Assert.True(Cart.IsCurrentPage, "User is not on the Cart page");
            Assert.True(Cart.IsPaypalWidgetDisplayed(), "PayPal Widget is Not Displayed"); 
            Cart.ScrollToPayPalLaterWidget();

            //Act : Capture Visible Screen
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}