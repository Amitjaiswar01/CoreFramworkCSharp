using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using Automation.Framework;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail.T7618_T7619_VerifyLayoutOfThumbnails
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7619_iPhone_VerifyLayoutOfThumbnails : T7619_MobileBase
    {
        public T7619_iPhone_VerifyLayoutOfThumbnails(ITestOutputHelper output, T7619_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfThumbnails(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7619_Emulator_VerifyLayoutOfThumbnails : T7619_MobileBase
    {
        public T7619_Emulator_VerifyLayoutOfThumbnails(ITestOutputHelper output, T7619_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyLayoutOfThumbnails(string config) => Validate(Validate, config);
    }


    public class T7619_SharedSku_Fixture : FixtureBase
    {
        public string RandomSku { get; }

        public T7619_SharedSku_Fixture()
        {
            var random = new Random();
            var list = new List<string> { "38E40", "8G405", "1D961" };
            var index = random.Next(list.Count);
            RandomSku = list[index];
        }
    }


    /// <summary>
    /// Verify the Layout of the Image Modal of the PDP
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9837
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7619
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9837"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7619")]
    public abstract class T7619_MobileBase : VisualTestsBaseMobile, IClassFixture<T7619_SharedSku_Fixture>
    {
        protected readonly T7619_SharedSku_Fixture Fixture;

        protected T7619_MobileBase(ITestOutputHelper output, T7619_SharedSku_Fixture fixture) : base(output, fixture)
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
                var index = MathHelper.GetRandomNumber(ProductDetail.GetNumberOfThumbnailImages()) + 1;
                Fixture.ThumbnailNumber = index;
                ProductDetail.SelectThumbnailImage(Fixture.ThumbnailNumber);
            }
            else
            {
                ProductDetail.SelectThumbnailImage(Fixture.ThumbnailNumber);
            }

            //Act: Capture a screenshot of the visible screen.
            Browser.ScrollToTopOfWindow();
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            /*Act:
             Tap on the '+' icon on the main product image.
             Inside the modal, select Customer Photos tab.
             */
            ProductDetail.OpenCustomerPhotosTab();

            //Act: Capture a screenshot of the visible screen.
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement>{ProductDetail.IgnoreCustomerPhotoTab()});  
        }
    }
}