using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail.T7788_T7789_VerifyLayoutOfReplacementPartsModal
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7789_iPhone_VerifyLayoutOfReplacementPartsModal : T7789_MobileBase
    {
        public T7789_iPhone_VerifyLayoutOfReplacementPartsModal(ITestOutputHelper output, T7789_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfReplacementPartsModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7789_AndroidPhone_VerifyLayoutOfReplacementPartsModal : T7789_MobileBase
    {
        public T7789_AndroidPhone_VerifyLayoutOfReplacementPartsModal(ITestOutputHelper output, T7789_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void VerifyLayoutOfReplacementPartsModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7789_Emulator_VerifyLayoutOfReplacementPartsModal : T7789_MobileBase
    {
        public T7789_Emulator_VerifyLayoutOfReplacementPartsModal(ITestOutputHelper output, T7789_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyLayoutOfReplacementPartsModal(string config) => Validate(Validate, config);
    }


    public class T7789_SharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7789_SharedSku_Fixture()
        {
            ShortSku = ProductActions.GetReplacementParentSku.ParentSkuString;
        }
    }


    /// <summary>
    /// Verify the layout of the Replacement Parts Modal 
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9845
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7789
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9845"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7789")]
    public abstract class T7789_MobileBase : VisualTestsBaseMobile, IClassFixture<T7789_SharedSku_Fixture>
    {
        protected readonly T7789_SharedSku_Fixture Fixture;

        protected T7789_MobileBase(ITestOutputHelper output, T7789_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            //Arrange: User has identified a qualifying SKU.
            InitializeVisualTest(config);
            var sku = Fixture.ShortSku;
            Assert.DatabaseObject(sku, "ProductActions.ReplacementPartShortSku");

            /*Act:
             Use the SKU in the pre-conditions and enter it at the end of the following URL: https://www.lampsplus.com/products/<SKU>.
             Open the drawer labeled Product Details.
            */
            ProductDetail.NavigateToProductDetailByShortSku(sku);
            ProductDetail.OpenProductDetailsDrawer();

            //Act: Capture a screenshot of the entire page.
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ProductDetail.IgnoreStockCheckWrapper(), ProductDetail.IgnoreMoreYouMayLikeContainer() }, true, true);

            //Act: Tap on the Bulbs & Replacement Parts for Style#<SKU> link in the product details section.
            ProductDetail.OpenBulbAndReplacementPartsModal();

            //Act: Capture a screenshot of the modal element.
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, ProductDetail.GetMediaModalContentModal());
        }
    }
}
