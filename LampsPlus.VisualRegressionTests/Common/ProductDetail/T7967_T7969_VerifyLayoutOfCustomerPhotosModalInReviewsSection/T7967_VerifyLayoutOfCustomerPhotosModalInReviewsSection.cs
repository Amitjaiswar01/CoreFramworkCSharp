using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail.T7967_T7969_VerifyLayoutOfCustomerPhotosModalInReviewsSection
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7967_Window_VerifyLayoutOfCustomerPhotosModalInReviewsSection : T7967_DesktopBase
    {
        public T7967_Window_VerifyLayoutOfCustomerPhotosModalInReviewsSection(ITestOutputHelper output, T7967_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyLayoutOfCustomerPhotosModalInReviewsSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7967_Window_VerifyLayoutOfCustomerPhotosModalInReviewsSectionForCustomerSignedIn : T7967_DesktopBase
    {
        public T7967_Window_VerifyLayoutOfCustomerPhotosModalInReviewsSectionForCustomerSignedIn(ITestOutputHelper output, T7967_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void VerifyLayoutOfCustomerPhotosModalInReviewsSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7967_Mac_VerifyLayoutOfCustomerPhotosModalInReviewsSection : T7967_DesktopBase
    {
        public T7967_Mac_VerifyLayoutOfCustomerPhotosModalInReviewsSection(ITestOutputHelper output, T7967_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfCustomerPhotosModalInReviewsSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7967_Mac_VerifyLayoutOfCustomerPhotosModalInReviewsSectionForCustomerSignedIn : T7967_DesktopBase
    {
        public T7967_Mac_VerifyLayoutOfCustomerPhotosModalInReviewsSectionForCustomerSignedIn(ITestOutputHelper output, T7967_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI)]
        public void VerifyLayoutOfCustomerPhotosModalInReviewsSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7967_iPad_VerifyLayoutOfCustomerPhotosModalInReviewsSection : T7967_DesktopBase
    {
        public T7967_iPad_VerifyLayoutOfCustomerPhotosModalInReviewsSection(ITestOutputHelper output, T7967_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfCustomerPhotosModalInReviewsSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7967_iPad_VerifyLayoutOfCustomerPhotosModalInReviewsSectionForCustomerSignedIn : T7967_DesktopBase
    {
        public T7967_iPad_VerifyLayoutOfCustomerPhotosModalInReviewsSectionForCustomerSignedIn(ITestOutputHelper output, T7967_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI)]
        public void VerifyLayoutOfCustomerPhotosModalInReviewsSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7967_TabletEmulator_VerifyLayoutOfCustomerPhotosModalInReviewsSection : T7967_DesktopBase
    {
        public T7967_TabletEmulator_VerifyLayoutOfCustomerPhotosModalInReviewsSection(ITestOutputHelper output, T7967_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyLayoutOfCustomerPhotosModalInReviewsSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7967_TabletEmulator_VerifyLayoutOfCustomerPhotosModalInReviewsSectionForCustomerSignedIn : T7967_DesktopBase
    {
        public T7967_TabletEmulator_VerifyLayoutOfCustomerPhotosModalInReviewsSectionForCustomerSignedIn(ITestOutputHelper output, T7967_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI)]
        public void VerifyLayoutOfCustomerPhotosModalInReviewsSection(string config) => Validate(Validate, config);
    }


    public class T7967_SharedSkus_Fixture : FixtureBase
    {
        public string RandomSku { get; }

        public T7967_SharedSkus_Fixture()
        {
            var random = new Random();
            var list = new List<string> { "1D961", "W9781", "1F028", "71F57", "19X62", "80M47", "58M48", "78J32", "7C289"};
            int index = random.Next(list.Count);
            RandomSku = list[index];
        }
    }


    /// <summary>
    /// Verify the Layout of Customer Photos Modal in the 'Reviews' Section
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10768
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7967
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10768"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7967")]
    public abstract class T7967_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7967_SharedSkus_Fixture>
    {
        protected readonly T7967_SharedSkus_Fixture Fixture;

        protected T7967_DesktopBase(ITestOutputHelper output, T7967_SharedSkus_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            //Arrange: Identify a ShortSKU that has photo modal in the Review section
            InitializeVisualTest(config);
            var shortSku = Fixture.RandomSku;

            //Act: Navigate to the PDP by ShortSku
            ProductDetail.NavigateToProductDetailByShortSku(shortSku);
            Assert.True(ProductDetail.IsCurrentPage, "User is Not on PDP Page");

            //Act: Click on Review Stars Link below Product Name
            ProductDetail.MoveToReviewsSection();

            //Act: Select photo from one of the reviews and capture screenshot
            ProductDetail.OpenReviewPhotoModal();
            ScreenCapturer.CaptureWholeOverlayModal(Browser.PageUrl, ProductDetail.GetMediaModalContentModal());
        }
    }
}