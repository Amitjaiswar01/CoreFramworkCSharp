using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using Automation.Framework;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail.T7950_T7951_VerifyLayoutOfNeedHelpWithThisProductModalOnPdp
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7951_iPhone_VerifyLayoutOfNeedHelpWithThisProductModalOnPdp : T7951_MobileBase
    {
        public T7951_iPhone_VerifyLayoutOfNeedHelpWithThisProductModalOnPdp(ITestOutputHelper output, T7951_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfNeedHelpModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7951_iPhone_VerifyLayoutOfNeedHelpWithThisProductModalOnPdpPros: T7951_MobileBase
    {
        public T7951_iPhone_VerifyLayoutOfNeedHelpWithThisProductModalOnPdpPros(ITestOutputHelper output, T7951_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_PCSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_PCSI)]
        public void VerifyLayoutOfNeedHelpModal(string config) => Validate(Validate, config);
    }


    public class T7951_Android_VerifyLayoutOfNeedHelpWithThisProductModalOnPdp : T7951_MobileBase
    {
        public T7951_Android_VerifyLayoutOfNeedHelpWithThisProductModalOnPdp(ITestOutputHelper output, T7951_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void VerifyLayoutOfNeedHelpModal(string config) => Validate(Validate, config);
    }


    public class T7951_Android_VerifyLayoutOfNeedHelpWithThisProductModalOnPdpPros : T7951_MobileBase
    {
        public T7951_Android_VerifyLayoutOfNeedHelpWithThisProductModalOnPdpPros(ITestOutputHelper output, T7951_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_PCSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_PCSI)]
        public void VerifyLayoutOfNeedHelpModal(string config) => Validate(Validate, config);
    }

    public class T7951_Emulator_VerifyLayoutOfNeedHelpWithThisProductModalOnPDP : T7951_MobileBase
    {
        public T7951_Emulator_VerifyLayoutOfNeedHelpWithThisProductModalOnPDP(ITestOutputHelper output, T7951_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyLayoutOfNeedHelpModal(string config) => Validate(Validate, config);
    }


    public class T7951_SharedSkus_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7951_SharedSkus_Fixture()
        {
            ShortSku = ProductActions.GetSkuWithShipInOption;
        }
    }


    /// <summary>
    /// Verify the layout Need Help with this product modal Outside chat hours
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10668
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7951
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10668"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7951")]
    public abstract class T7951_MobileBase : VisualTestsBaseMobile, IClassFixture<T7951_SharedSkus_Fixture>
    {
        protected readonly T7951_SharedSkus_Fixture Fixture;

        protected T7951_MobileBase(ITestOutputHelper output, T7951_SharedSkus_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            // Arrange : Navigate to any product detail page
            InitializeVisualTest(config);
            var shortSku = Fixture.ShortSku;
            Assert.DatabaseObject(Fixture.ShortSku, "ProductActions.GetSkuWithShipInOption");

            // Act : Navigate to the PDP by shortSku
            ProductDetail.NavigateToProductDetailByShortSku(shortSku);
            Assert.True(ProductDetail.IsCurrentPage, "User is Not on PDP Page");

            // Act : Focus on the Product help link and Take Screenshot of the visible screen
            ProductDetail.DisplayProductHelpLink();
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement>{ProductDetail.IgnoreMoreYouMayLikeContainer()});

            // Act : Open the Need help with this product model and take Screenshot
            var isChatOpenTime = ProductDetail.IsChatIconEnabled();

            if (isChatOpenTime)
            {
                Log.Message("Chat is in business hours");
            }
            else
            {
                ProductDetail.OpenProductHelpAndStoreAvailabilityModal();
                ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
                ProductDetail.CloseNeedHelpModal();
            }
        }
    }
}