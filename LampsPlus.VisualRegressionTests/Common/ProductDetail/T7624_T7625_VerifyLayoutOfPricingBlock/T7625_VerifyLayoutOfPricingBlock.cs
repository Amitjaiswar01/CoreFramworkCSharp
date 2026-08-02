using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail.T7624_T7625_VerifyLayoutOfPricingBlock
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7625_iPhone_VerifyLayoutOfPricingBlockOnPdp : T7625_MobileBase
    {
        public T7625_iPhone_VerifyLayoutOfPricingBlockOnPdp(ITestOutputHelper output, T7625_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfPricingBlockOnPdp(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7625_Android_VerifyLayoutOfPricingBlockOnPdp : T7625_MobileBase
    {
        public T7625_Android_VerifyLayoutOfPricingBlockOnPdp(ITestOutputHelper output, T7625_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void VerifyLayoutOfPricingBlockOnPdp(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7625_Emulator_VerifyLayoutOfPricingBlockOnPdp : T7625_MobileBase
    {
        public T7625_Emulator_VerifyLayoutOfPricingBlockOnPdp(ITestOutputHelper output, T7625_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyLayoutOfPricingBlockOnPdp(string config) => Validate(Validate, config);
    }


    public class T7625_SharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7625_SharedSku_Fixture()
        {
            ShortSku = ProductActions.GetSkuForPricingBlock;
        }
    }


    /// <summary>
    /// Verify the layout of the Pricing Block values on the SFP.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9838
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7625
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9838"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7625")
    ]
    public abstract class T7625_MobileBase : VisualTestsBaseMobile, IClassFixture<T7625_SharedSku_Fixture>
    {
        protected readonly T7625_SharedSku_Fixture Fixture;

        protected T7625_MobileBase(ITestOutputHelper output, T7625_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            //Arrange: User has identified a viable SKU.
            InitializeVisualTest(config);
            var sku = Fixture.ShortSku;
            Assert.DatabaseObject(sku, "ProductActions.GetSkuForPricingBlock");

            //Act: User has navigated to the PDP for the SKU identified in the pre-conditions
            ProductDetail.NavigateToProductDetailByShortSku(sku);

            //Act: Capture a screenshot of the entire page.
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ProductDetail.IgnoreStockCheckWrapper(), ProductDetail.IgnoreCertonaDrawerName() }, true, true);
        }
    }
}
