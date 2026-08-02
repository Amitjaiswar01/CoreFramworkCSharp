using System.Collections.Generic;
using OpenQA.Selenium;
using xRetry;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7290_Window_VerifyLayoutOfFinishFamilyItem : T7290_DesktopBase
    {
        public T7290_Window_VerifyLayoutOfFinishFamilyItem(ITestOutputHelper output, FinishFamilySharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfFinishFamilyItem(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7290_Mac_VerifyLayoutOfFinishFamilyItem : T7290_DesktopBase
    {
        public T7290_Mac_VerifyLayoutOfFinishFamilyItem(ITestOutputHelper output, FinishFamilySharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfFinishFamilyItem(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7290_iPad_VerifyLayoutOfFinishFamilyItem : T7290_DesktopBase
    {
        public T7290_iPad_VerifyLayoutOfFinishFamilyItem(ITestOutputHelper output, FinishFamilySharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfFinishFamilyItem(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7290_TabletEmulator_VerifyLayoutOfFinishFamilyItem : T7290_DesktopBase
    {
        public T7290_TabletEmulator_VerifyLayoutOfFinishFamilyItem(ITestOutputHelper output, FinishFamilySharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfFinishFamilyItem(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7313_iPhone_VerifyLayoutOfFinishFamilyItem : T7313_MobileBase
    {
        public T7313_iPhone_VerifyLayoutOfFinishFamilyItem(ITestOutputHelper output, FinishFamilySharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void LayoutOfFinishFamilyItem(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7313_AndroidPhone_VerifyLayoutOfFinishFamilyItem : T7313_MobileBase
    {
        public T7313_AndroidPhone_VerifyLayoutOfFinishFamilyItem(ITestOutputHelper output, FinishFamilySharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfFinishFamilyItem(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7313_Emulator_VerifyLayoutOfFinishFamilyItem : T7313_MobileBase
    {
        public T7313_Emulator_VerifyLayoutOfFinishFamilyItem(ITestOutputHelper output, FinishFamilySharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfFinishFamilyItem(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout for a Finish Family item.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7388
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7290
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7388"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7290")]
    public abstract class T7290_DesktopBase : T7290_T7313_Base
    {
        protected T7290_DesktopBase(ITestOutputHelper output, FinishFamilySharedSku_Fixture fixture) : base(output, fixture) { }
    }


    /// <summary>
    /// Verify the layout for a Finish Family item.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7388
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7313
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7388"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7313")]
    public abstract class T7313_MobileBase : T7290_T7313_Base
    {
        protected T7313_MobileBase(ITestOutputHelper output, FinishFamilySharedSku_Fixture fixture) : base(output, fixture) { }

        protected override void Validate(string config)
        {
            InitializeVisualTest(config);

            var sku = Fixture.ShortSku;

            Assert.DatabaseObject(sku, "ProductActions.GetSkuThatHasFinishFamily()");

            ProductDetail.NavigateToProductDetailByShortSku(sku);

            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ProductDetail.StockCheckElement, HeaderFooter.Footer, ProductDetail.MoreYouMayLikeContainer }, true, true);
        }
    }


    public class FinishFamilySharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public FinishFamilySharedSku_Fixture()
        {
            ShortSku = ProductActions.GetSkuThatHasFinishFamily();
        }
    }


    public abstract class T7290_T7313_Base : VisualTestsBase, IClassFixture<FinishFamilySharedSku_Fixture>
    {
        protected readonly FinishFamilySharedSku_Fixture Fixture;

        protected T7290_T7313_Base(ITestOutputHelper output, FinishFamilySharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            InitializeVisualTest(config);

            var sku = Fixture.ShortSku;

            Assert.DatabaseObject(sku, "ProductActions.GetSkuThatHasFinishFamily()");

            ProductDetail.NavigateToProductDetailByShortSku(sku);

            Browser.Wait.ForDomReady();

            ScreenCapturer.CaptureScreenRegionWithIgnoredLayouts(Browser.PageUrl, ProductDetail.TopContentProductDetail, new List<IElement> { ProductDetail.StockCheckWrapper },true);
        }
    }
}
