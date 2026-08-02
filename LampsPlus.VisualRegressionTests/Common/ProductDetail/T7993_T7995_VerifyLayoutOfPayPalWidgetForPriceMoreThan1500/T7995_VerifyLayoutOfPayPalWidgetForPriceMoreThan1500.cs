using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail.T7993_T7995_VerifyLayoutOfPayPalWidgetForPriceMoreThan1500
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7995_iPhone_VerifyLayoutOfPayPalWidgetForPriceMoreThan1500 : T7995_MobileBase
    {
        public T7995_iPhone_VerifyLayoutOfPayPalWidgetForPriceMoreThan1500(ITestOutputHelper output, T7995_ShareSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void LayoutOfPayPalWidget1500(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7995_Android_VerifyLayoutOfPayPalWidgetForPriceMoreThan1500 : T7995_MobileBase
    {
        public T7995_Android_VerifyLayoutOfPayPalWidgetForPriceMoreThan1500(ITestOutputHelper output, T7995_ShareSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfPayPalWidget1500(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7995_Emulator_VerifyLayoutOfPayPalWidgetForPriceMoreThan1500 : T7995_MobileBase
    {
        public T7995_Emulator_VerifyLayoutOfPayPalWidgetForPriceMoreThan1500(ITestOutputHelper output, T7995_ShareSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfPayPalWidget1500(string config) => Validate(Validate, config);
    }


    public class T7995_ShareSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7995_ShareSku_Fixture()
        {
            ShortSku = ProductActions.GetSkuWithPriceMoreThan1500;
        }
    }


    /// <summary>
    /// Verify Layout of the PayPal Widget For Product with Price More than $1,500
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10877
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7995
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10877"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7995")]
    public abstract class T7995_MobileBase : VisualTestsBaseMobile, IClassFixture<T7995_ShareSku_Fixture>
    {
        protected readonly T7995_ShareSku_Fixture Fixture;

        protected T7995_MobileBase(ITestOutputHelper output, T7995_ShareSku_Fixture fixture) : base(output, fixture)
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
            Browser.ScrollIntoView(ProductDetail.GetPayPalLogo(), true);

            //Act : Capture Visible Screen
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ProductDetail.IgnoreStockCheckWrapper(), ProductDetail.IgnoreCertonaDrawerName() });

            //Act : Add product to Cart and scroll to Paypal Widget
            ProductDetail.AddToCart();
            Assert.True(Cart.IsCurrentPage, "User is not on the Cart page");
            Assert.True(Cart.IsPaypalWidgetDisplayed(), "PayPal Widget is Not Displayed");
            Browser.ScrollIntoView(Cart.GetPaypalButton(), true);

            //Act : Capture Visible Screen
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { Cart.IgnoreCartId() }, floating: Cart.IgnoreCartId(), offset: 20);
        }
    }
}
