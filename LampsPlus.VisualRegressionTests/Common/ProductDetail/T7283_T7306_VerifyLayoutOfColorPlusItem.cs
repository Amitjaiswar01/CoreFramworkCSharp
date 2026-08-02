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
    public class T7283_Window_VerifyLayoutOfColorPlusItem : T7283_DesktopBase
    {
        public T7283_Window_VerifyLayoutOfColorPlusItem(ITestOutputHelper output, ColorPlusSharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfColorPlusItem(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7283_Mac_VerifyLayoutOfColorPlusItem : T7283_DesktopBase
    {
        public T7283_Mac_VerifyLayoutOfColorPlusItem(ITestOutputHelper output, ColorPlusSharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfColorPlusItem(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7283_iPad_VerifyLayoutOfColorPlusItem : T7283_DesktopBase
    {
        public T7283_iPad_VerifyLayoutOfColorPlusItem(ITestOutputHelper output, ColorPlusSharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfColorPlusItem(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7283_TabletEmulator_VerifyLayoutOfColorPlusItem : T7283_DesktopBase
    {
        public T7283_TabletEmulator_VerifyLayoutOfColorPlusItem(ITestOutputHelper output, ColorPlusSharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfColorPlusItem(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7306_iPhone_VerifyLayoutOfColorPlusItem : T7306_MobileBase
    {
        public T7306_iPhone_VerifyLayoutOfColorPlusItem(ITestOutputHelper output, ColorPlusSharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth)]
        public void LayoutOfColorPlusItem(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7306_AndroidPhone_VerifyLayoutOfColorPlusItem : T7306_MobileBase
    {
        public T7306_AndroidPhone_VerifyLayoutOfColorPlusItem(ITestOutputHelper output, ColorPlusSharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfColorPlusItem(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7306_Emulator_VerifyLayoutOfColorPlusItem : T7306_MobileBase
    {
        public T7306_Emulator_VerifyLayoutOfColorPlusItem(ITestOutputHelper output, ColorPlusSharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfColorPlusItem(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout for a Color Plus item.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7379
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7283
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7379"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7283")]
    public abstract class T7283_DesktopBase : T7283_T7306_Base
    {
        protected T7283_DesktopBase(ITestOutputHelper output, ColorPlusSharedSku_Fixture fixture) : base(output, fixture) { }
    }


    /// <summary>
    /// Verify the layout for a Color Plus item.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7379
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7306
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7379"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7306")]
    public abstract class T7306_MobileBase : T7283_T7306_Base
    {
        protected T7306_MobileBase(ITestOutputHelper output, ColorPlusSharedSku_Fixture fixture) : base(output, fixture) { }
    }


    public class ColorPlusSharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public ColorPlusSharedSku_Fixture()
        {
            ShortSku = ProductActions.GetColorPlusSku;
        }
    }


    public abstract class T7283_T7306_Base : VisualTestsBase, IClassFixture<ColorPlusSharedSku_Fixture>
    {
        protected readonly ColorPlusSharedSku_Fixture Fixture;

        protected T7283_T7306_Base(ITestOutputHelper output, ColorPlusSharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            InitializeVisualTest(config);

            var sku = Fixture.ShortSku;

            Assert.DatabaseObject(sku, "ProductActions.GetColorPlusSku()");

            ProductDetail.NavigateToProductDetailByShortSku(sku);

            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

            Browser.ScrollToTopOfWindow();

            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ProductDetail.MoreYouMayLikeContainer }, true, true);
        }
    }
}
