using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail.T7783_T7784_VerifyLayoutOfPhotoModalForReviews
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7783_Window_VerifyLayoutOfPhotoModalForReviews : T7783_DesktopBase
    {
        public T7783_Window_VerifyLayoutOfPhotoModalForReviews(ITestOutputHelper output, T7783_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfPhotoModalInReviews(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7268_Mac_VerifyLayoutOfPhotoModalForReviews : T7783_DesktopBase
    {
        public T7268_Mac_VerifyLayoutOfPhotoModalForReviews(ITestOutputHelper output, T7783_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfPhotoModalInReviews(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7266_iPad_VerifyLayoutOfPhotoModalForReviews : T7783_DesktopBase
    {
        public T7266_iPad_VerifyLayoutOfPhotoModalForReviews(ITestOutputHelper output, T7783_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfPhotoModalInReviews(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7783_TabletEmulator_VerifyLayoutOfPhotoModalForReviews : T7783_DesktopBase
    {
        public T7783_TabletEmulator_VerifyLayoutOfPhotoModalForReviews(ITestOutputHelper output, T7783_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfPhotoModalInReviews(string config) => Validate(Validate, config);
    }


    public class T7783_SharedSku_Fixture : FixtureBase
    {
        public string RandomSku { get; }

        public T7783_SharedSku_Fixture()
        {
            var random = new Random();
            var list = new List<string> { "38E40", "71f57" };
            var index = random.Next(list.Count);
            RandomSku = list[index];
        }
    }


    /// <summary>
    /// Verify the layout of the Photo Modal In The Review Modal
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9844
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7783
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9844"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7783")]
    public abstract class T7783_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7783_SharedSku_Fixture>
    {
        protected readonly T7783_SharedSku_Fixture Fixture;

        protected T7783_DesktopBase(ITestOutputHelper output, T7783_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            //Arrange: Use one of the following SKUs - 38E40, 71F57
            InitializeVisualTest(config);
            var sku = Fixture.RandomSku;

            //Act: Take one of the SKUs in the pre-conditions and enter it at the end of the URL https://www.lampsplus.com/products/.
            Browser.Navigate(Urls.LampsPlusProductsUrl + sku);
            Assert.True(ProductDetail.IsCurrentPage, "User is not on the PDP.");

            //Act: Once the PDP loads, scroll down to the 'Customer Reviews' section and click on the photo to launch the photo modal
            ProductDetail.OpenReviewPhotoModal();

            //Act: Capture a screenshot of the 'Photo Modal' section element.
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, ProductDetail.GetMediaModalContentModal());
        }
    }
}
