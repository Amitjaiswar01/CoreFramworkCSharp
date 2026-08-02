using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using Automation.Framework;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail.T7560_T7561_VerifyLayoutOfProductNotAvailable
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7561_iPhone_VerifyLayoutOfProductNotAvailable : T7561_MobileBase
    {
        public T7561_iPhone_VerifyLayoutOfProductNotAvailable(ITestOutputHelper output, T7561_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void LayoutOfProductNotAvailable(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7561_AndroidPhone_VerifyLayoutOfProductNotAvailable : T7561_MobileBase
    {
        public T7561_AndroidPhone_VerifyLayoutOfProductNotAvailable(ITestOutputHelper output, T7561_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfProductNotAvailable(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7561_Emulator_VerifyLayoutOfProductNotAvailable : T7561_MobileBase
    {
        public T7561_Emulator_VerifyLayoutOfProductNotAvailable(ITestOutputHelper output, T7561_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfProductNotAvailable(string config) => Validate(Validate, config);
    }


    public class T7561_SharedSkus_Fixture : FixtureBase
    {
        public string ShortSku { get; }
        public List<Dictionary<string, string>> Url { get; }

        public T7561_SharedSkus_Fixture()
        {
            ShortSku = ProductActions.GetProductNotAvailableShortSku;
            Url = SortActions.GetSortWithNoActiveAbTest();
        }
    }


    /// <summary>
    /// Verify the layout of the page for Product Not Available SKUs.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9841
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7561
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9841"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7561")]
    public abstract class T7561_MobileBase : VisualTestsBaseMobile, IClassFixture<T7561_SharedSkus_Fixture>
    {
        protected readonly T7561_SharedSkus_Fixture Fixture;

        protected T7561_MobileBase(ITestOutputHelper output, T7561_SharedSkus_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            //Arrange: User has identified a qualifying SKU.
            InitializeVisualTest(config);
            Assert.DatabaseObject(Fixture.ShortSku, "ProductActions.GetProductNotAvailableShortSku");
            var shortSku = Fixture.ShortSku;

            //Act: Using the SKU from the query in the pre-conditions, navigate to the following page: https://www.lampsplus.com/sfp/<SKU>
            Browser.Navigate(Urls.ProductFullPageBaseUrl + shortSku);
            Assert.True(SortPla.IsNotifyButtonVisible(), "Notify button is not visible.");

            //Act: Capture a screenshot of the entire page but ignore the Certona Similar Designs container.
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { SortFullPageCertona.IgnoreSimilarDesignsContainer()}, true, true);

            //Act: Navigate to any Sort page and add the following to the end of the URL: ?sfp=<SKU>
            Browser.Navigate($"https://{Fixture.Url[0]["Url"]}?sfp={Fixture.ShortSku}");
            Assert.True(SortPla.IsNotifyButtonVisible(), "Notify button is not visible.");

            //Act: Capture a screenshot of the visible screen.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            //Act: Navigate to the following URL: https://www.lampsplus.com/products/<SKU>
            ProductDetail.NavigateToProductDetailByShortSku(shortSku);

            //Act: Capture a screenshot of the entire screen.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture, true, true);
        }
    }
}
