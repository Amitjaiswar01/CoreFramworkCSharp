using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail.T7618_T7619_VerifyLayoutOfThumbnails
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7618_Windows_VerifyLayoutOfThumbnails : T7618_DesktopBase
    {
        public T7618_Windows_VerifyLayoutOfThumbnails(ITestOutputHelper output, T7618_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyLayoutOfThumbnails(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7618_Mac_VerifyLayoutOfThumbnails : T7618_DesktopBase
    {
        public T7618_Mac_VerifyLayoutOfThumbnails(ITestOutputHelper output, T7618_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfThumbnails(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7618_iPad_VerifyLayoutOfThumbnails : T7618_DesktopBase
    {
        public T7618_iPad_VerifyLayoutOfThumbnails(ITestOutputHelper output, T7618_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfThumbnails(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7618_TabletEmulator_VerifyLayoutOfThumbnails : T7618_DesktopBase
    {
        public T7618_TabletEmulator_VerifyLayoutOfThumbnails(ITestOutputHelper output, T7618_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyLayoutOfThumbnails(string config) => Validate(Validate, config);
    }


    public class T7618_SharedSku_Fixture : FixtureBase
    {
        public string RandomSku { get; }

        public T7618_SharedSku_Fixture()
        {
            var random = new Random();
            var list = new List<string> { "38E40", "8G405", "1D961" };
            var index = random.Next(list.Count);
            RandomSku = list[index];
        }
    }


    /// <summary>
    /// Verify the Layout of the Image Modal of the PDP.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9837
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7618
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9837"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7618")]
    public abstract class T7618_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7618_SharedSku_Fixture>
    {
        protected readonly T7618_SharedSku_Fixture Fixture;

        protected T7618_DesktopBase(ITestOutputHelper output, T7618_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            //Arrange: User has identified a SKU that has a PDP.
            InitializeVisualTest(config);

            /*Act:
             User navigates to the PDP for the SKU identified in the pre-conditions.
             Once the PDP has loaded, select one of the images from teh thumbnail carousel.
            */
            ProductDetail.NavigateToProductDetailByShortSku(Fixture.RandomSku);

            if (Settings.IsBaseLine)
            {
                var index = MathHelper.GetRandomNumber(ProductDetail.GetNumberOfThumbnailImages());
                Fixture.ThumbnailNumber = index;
                ProductDetail.SelectThumbnailImage(Fixture.ThumbnailNumber);
            }
            else
            {
                ProductDetail.SelectThumbnailImage(Fixture.ThumbnailNumber);
            }

            //Act: Capture a screenshot of the visible screen.
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ProductDetail.IgnoreStockCheckWrapper() });

            //Act: Click on one of the thumbnail images.
            ProductDetail.OpenThumbnailModal();

            //Act: Capture a screenshot of the modal element.
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, Modal.GetLpModalContent());

            //Act: Inside the modal, select a different image from the thumbnails.
            ProductDetail.SelectDifferentThumbnailInsideModal();

            //Act: Capture a screenshot of the modal element.
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, Modal.GetLpModalContent());

            //Act: Select the Customer Photos tab.
            ProductDetail.OpenCustomerPhotosTab();

            //Act: Capture a screenshot of the visible screen.
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl,new List<IElement>{ProductDetail.IgnoreCustomerPhotoTab()});
        }
    }
}
