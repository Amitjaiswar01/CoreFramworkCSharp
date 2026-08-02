using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail.T7966_T7968_VerifyLayoutOfCustomerReviewsSection
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7966_Windows_VerifyLayoutOfCustomerReviewsSection : T7966_DesktopBase
    {
        public T7966_Windows_VerifyLayoutOfCustomerReviewsSection(ITestOutputHelper output, T7966_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyLayoutOfCustomerReviewsSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7966_Windows_VerifyLayoutOfCustomerReviewsSectionForCustomerSignedIn : T7966_DesktopBase
    {
        public T7966_Windows_VerifyLayoutOfCustomerReviewsSectionForCustomerSignedIn(ITestOutputHelper output, T7966_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void VerifyLayoutOfCustomerReviewsSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7966_Mac_VerifyLayoutOfCustomerReviewsSection : T7966_DesktopBase
    {
        public T7966_Mac_VerifyLayoutOfCustomerReviewsSection(ITestOutputHelper output, T7966_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfCustomerReviewsSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7966_Mac_VerifyLayoutOfCustomerReviewsSectionForCustomerSignedIn : T7966_DesktopBase
    {
        public T7966_Mac_VerifyLayoutOfCustomerReviewsSectionForCustomerSignedIn(ITestOutputHelper output, T7966_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI)]
        public void VerifyLayoutOfCustomerReviewsSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7966_iPad_VerifyLayoutOfCustomerReviewsSection : T7966_DesktopBase
    {
        public T7966_iPad_VerifyLayoutOfCustomerReviewsSection(ITestOutputHelper output, T7966_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfCustomerReviewsSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7966_iPad_VerifyLayoutOfCustomerReviewsSectionForCustomerSignedIn : T7966_DesktopBase
    {
        public T7966_iPad_VerifyLayoutOfCustomerReviewsSectionForCustomerSignedIn(ITestOutputHelper output, T7966_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI)]
        public void VerifyLayoutOfCustomerReviewsSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7966_TabletEmulator_VerifyLayoutOfCustomerReviewsSection : T7966_DesktopBase
    {
        public T7966_TabletEmulator_VerifyLayoutOfCustomerReviewsSection(ITestOutputHelper output, T7966_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyLayoutOfCustomerReviewsSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7966_TabletEmulator_VerifyLayoutOfCustomerReviewsSectionForCustomerSignedIn : T7966_DesktopBase
    {
        public T7966_TabletEmulator_VerifyLayoutOfCustomerReviewsSectionForCustomerSignedIn(ITestOutputHelper output, T7966_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI)]
        public void VerifyLayoutOfCustomerReviewsSection(string config) => Validate(Validate, config);
    }


    public class T7966_SharedSkus_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7966_SharedSkus_Fixture()
        {
            ShortSku = ProductActions.GetSkuThatQualifiesForReviews;
        }
    }


    /// <summary>
    /// Verify the Layout of the 'Customer Reviews' Section
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10767
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7966
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10767"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7966")]
    public abstract class T7966_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7966_SharedSkus_Fixture>
    {
        protected readonly T7966_SharedSkus_Fixture Fixture;

        protected T7966_DesktopBase(ITestOutputHelper output, T7966_SharedSkus_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            //Arrange: Identify a ShortSKU that has Customer Reviews section
            InitializeVisualTest(config);
            var shortSku = Fixture.ShortSku;
            Assert.DatabaseObject(Fixture.ShortSku, "ProductActions.GetSkuThatQualifiesForReviews");

            //Act: Navigate to the PDP by ShortSku
            ProductDetail.NavigateToProductDetailByShortSku(shortSku);
            Assert.True(ProductDetail.IsCurrentPage, "User is Not on PDP Page");

            //Act: Scroll down to Reviews section and capture screenshot
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, ProductDetail.GetTurnToReviewSection());
            
            //Act: Open Reviews Modal and capture screenshot
            ProductDetail.GetTurnToReviewSection();
            ProductDetail.OpenReviewsModal();
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, ProductDetail.GetTurnToReviewModal());
        }
    }
}