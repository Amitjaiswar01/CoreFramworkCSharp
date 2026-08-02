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
    public class T7969_iPhone_VerifyLayoutOfCustomerPhotosModalInReviewsSection : T7969_MobileBase
    {
        public T7969_iPhone_VerifyLayoutOfCustomerPhotosModalInReviewsSection(ITestOutputHelper output, T7969_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfCustomerPhotosModalInReviewsSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7969_iPhone_VerifyLayoutOfCustomerPhotosModalInReviewsSectionForCustomerSignedIn : T7969_MobileBase
    {
        public T7969_iPhone_VerifyLayoutOfCustomerPhotosModalInReviewsSectionForCustomerSignedIn(ITestOutputHelper output, T7969_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
        public void VerifyLayoutOfCustomerPhotosModalInReviewsSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7969_Android_VerifyLayoutOfCustomerPhotosModalInReviewsSection : T7969_MobileBase
    {
        public T7969_Android_VerifyLayoutOfCustomerPhotosModalInReviewsSection(ITestOutputHelper output, T7969_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void VerifyLayoutOfCustomerPhotosModalInReviewsSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7969_Android_VerifyLayoutOfCustomerPhotosModalInReviewsSectionForCustomerSignedIn : T7969_MobileBase
    {
        public T7969_Android_VerifyLayoutOfCustomerPhotosModalInReviewsSectionForCustomerSignedIn(ITestOutputHelper output, T7969_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_NPCSI)]
        public void VerifyLayoutOfCustomerPhotosModalInReviewsSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7969_Emulator_VerifyLayoutOfCustomerPhotosModalInReviewsSection : T7969_MobileBase
    {
        public T7969_Emulator_VerifyLayoutOfCustomerPhotosModalInReviewsSection(ITestOutputHelper output, T7969_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyLayoutOfCustomerPhotosModalInReviewsSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7969_Emulator_VerifyLayoutOfCustomerPhotosModalInReviewsSectionForCustomerSignedIn : T7969_MobileBase
    {
        public T7969_Emulator_VerifyLayoutOfCustomerPhotosModalInReviewsSectionForCustomerSignedIn(ITestOutputHelper output, T7969_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void VerifyLayoutOfCustomerPhotosModalInReviewsSection(string config) => Validate(Validate, config);
    }


    public class T7969_SharedSkus_Fixture : FixtureBase
    {
        public string RandomSku { get; }

        public T7969_SharedSkus_Fixture()
        {
            var random = new Random();
            var list = new List<string> { "1D961", "W9781", "1F028", "71F57", "19X62", "80M47", "58M48", "78J32", "7C289" };
            int index = random.Next(list.Count);
            RandomSku = list[index];
        }
    }


    /// <summary>
    /// Verify the Layout of Customer Photos Modal in the 'Reviews' Section
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10768
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7969
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10768"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7969")]
    public abstract class T7969_MobileBase : VisualTestsBaseMobile, IClassFixture<T7969_SharedSkus_Fixture>
    {
        protected readonly T7969_SharedSkus_Fixture Fixture;

        protected T7969_MobileBase(ITestOutputHelper output, T7969_SharedSkus_Fixture fixture) : base(output, fixture)
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

            /*Act: Open Drawer Labeled 'Reviews'
            Select Photo from one of the Reviews and capture screenshot
            */
            ProductDetail.OpenReviewPhotoModal();
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, ProductDetail.GetMediaModalContentModal());
        }
    }
}