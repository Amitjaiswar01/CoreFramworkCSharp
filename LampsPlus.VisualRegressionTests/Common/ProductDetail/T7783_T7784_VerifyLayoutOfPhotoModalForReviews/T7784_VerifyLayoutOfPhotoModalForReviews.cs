using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail.T7783_T7784_VerifyLayoutOfPhotoModalForReviews
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7784_iPhone_VerifyLayoutOfPhotoModalForReviews : T7784_MobileBase
    {
        public T7784_iPhone_VerifyLayoutOfPhotoModalForReviews(ITestOutputHelper output, T7784_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void LayoutOfPhotoModalInReviews(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7784_AndroidPhone_VerifyLayoutOfPhotoModalForReviews : T7784_MobileBase
    {
        public T7784_AndroidPhone_VerifyLayoutOfPhotoModalForReviews(ITestOutputHelper output, T7784_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfPhotoModalInReviews(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7784_Emulator_VerifyLayoutOfPhotoModalForReviews : T7784_MobileBase
    {
        public T7784_Emulator_VerifyLayoutOfPhotoModalForReviews(ITestOutputHelper output, T7784_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfPhotoModalInReviews(string config) => Validate(Validate, config);
    }


    public class T7784_SharedSku_Fixture : FixtureBase
    {
        public string RandomSku { get; }

        public T7784_SharedSku_Fixture()
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
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7784
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9844"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T77684")]
    public abstract class T7784_MobileBase : VisualTestsBaseMobile, IClassFixture<T7784_SharedSku_Fixture>
    {
        protected readonly T7784_SharedSku_Fixture Fixture;

        protected T7784_MobileBase(ITestOutputHelper output, T7784_SharedSku_Fixture fixture) : base(output, fixture)
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

            //Act: Once the PDP loads, scroll down to the 'Customer Reviews' section, expand the 'Customer Reviews' section and tap on the photo to launch the photo modal.
            ProductDetail.OpenReviewPhotoModal();

            //Act: Capture a screenshot of the 'Photo Modal' section element.
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, ProductDetail.GetMediaModalContentModal());
        }
    }
}
