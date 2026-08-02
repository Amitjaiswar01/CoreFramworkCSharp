using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail.T7566_T7567_VerifyLayoutOfCallToOrder
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7567_iPhone_VerifyLayoutOfCallToOrder : T7567_MobileBase
    {
        public T7567_iPhone_VerifyLayoutOfCallToOrder(ITestOutputHelper output, T7567_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T7567. Rework - CI-3545")]
        //[RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void LayoutOfCallToOrder(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7567_AndroidPhone_VerifyLayoutOfCallToOrder : T7567_MobileBase
    {
        public T7567_AndroidPhone_VerifyLayoutOfCallToOrder(ITestOutputHelper output, T7567_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfCallToOrder(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7567_Emulator_VerifyLayoutOfCallToOrder : T7567_MobileBase
    {
        public T7567_Emulator_VerifyLayoutOfCallToOrder(ITestOutputHelper output, T7567_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfCallToOrder(string config) => Validate(Validate, config);
    }


    public class T7567_SharedSkus_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7567_SharedSkus_Fixture()
        {
            ShortSku = ProductActions.GetCallToOrderSku;
        }
    }


    /// <summary>
    /// Verify the layout of the page for Call To Order products.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9840
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7567
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9840"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7567")]
    public abstract class T7567_MobileBase : VisualTestsBaseMobile, IClassFixture<T7567_SharedSkus_Fixture>
    {
        protected readonly T7567_SharedSkus_Fixture Fixture;

        protected T7567_MobileBase(ITestOutputHelper output, T7567_SharedSkus_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            //Arrange: User has identified a SKU that has a PDP.
            InitializeVisualTest(config);
            Assert.DatabaseObject(Fixture.ShortSku, "ProductActions.GetCallToOrderSku");
            var shortSku = Fixture.ShortSku;

            //Act: Navigate to the page https://www.lampsplus.com/sfp/<SKU> using the SKU from the pre-conditions.
            Browser.Navigate(Urls.ProductFullPageBaseUrl + shortSku);
            Assert.True(ProductDetail.IsCallCustomerServiceBlockVisible, "User is not on an '/sfp/' page with an unavailable SKU.");
            Browser.ScrollToBottomOfPage(Urls.ProductFullPageBaseUrl + shortSku);
            Browser.ScrollToTopOfWindow();

            //Act: Capture a screenshot of the entire page and ignore the Similar Design SKUs.
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { SortFullPageCertona.IgnoreSimilarDesignsContainer() }, true);
            
            //Act: Navigate to the page https://www.lampsplus.com/products/table-lamps/?sfp=<SKU>
            Browser.Navigate(Urls.PlaTableLampsSfpUrl + Fixture.ShortSku);
            Assert.True(ProductDetail.IsCallCustomerServiceBlockVisible, "User is not on an '?sfp' page with an unavailable SKU.");

            //Act: Capture a screenshot of the visible screen.
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { Sort.IgnoreLpContainer() }, true);

            //Act: Navigate to the page https://www.lampsplus.com/products/<SKU>
            ProductDetail.NavigateToProductDetailByShortSku(shortSku);

            //Act: Capture a screenshot of the entire page.
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ProductDetail.IgnoreCertonaDrawerName() }, true, true);
        }
    }
}
