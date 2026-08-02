using System.Collections.Generic;
using OpenQA.Selenium;
using xRetry;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.VisualRegressionTests.Common.OtherPages
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7378_Window_VerifyLayoutOfSfpPage : T7378_DesktopBase
    {
        public T7378_Window_VerifyLayoutOfSfpPage(ITestOutputHelper output, SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfSfpPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7378_Mac_VerifyLayoutOfSfpPage : T7378_DesktopBase
    {
        public T7378_Mac_VerifyLayoutOfSfpPage(ITestOutputHelper output, SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfSfpPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7378_iPad_VerifyLayoutOfSfpPage : T7378_DesktopBase
    {
        public T7378_iPad_VerifyLayoutOfSfpPage(ITestOutputHelper output, SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfSfpPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7378_TabletEmulator_VerifyLayoutOfSfpPage : T7378_DesktopBase
    {
        public T7378_TabletEmulator_VerifyLayoutOfSfpPage(ITestOutputHelper output, SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfSfpPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7379_iPhone_VerifyLayoutOfSfpPage : T7379_MobileBase
    {
        public T7379_iPhone_VerifyLayoutOfSfpPage(ITestOutputHelper output, SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth)]
        public void LayoutOfSfpPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7379_AndroidPhone_VerifyLayoutOfSfpPage : T7379_MobileBase
    {
        public T7379_AndroidPhone_VerifyLayoutOfSfpPage(ITestOutputHelper output, SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfSfpPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7379_Emulator_VerifyLayoutOfSfpPage : T7379_MobileBase
    {
        public T7379_Emulator_VerifyLayoutOfSfpPage(ITestOutputHelper output, SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfSfpPage(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Sort page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7515
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7378
    /// </summary>
    //[Collection(LpTraits.UserRole.Anonymous)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7515"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7378")]
    public abstract class T7378_DesktopBase : T7378_T7379_Base
    {
        protected T7378_DesktopBase(ITestOutputHelper output, SharedSkus_Fixture fixture) : base(output, fixture) { }

        protected override void CapturePage()
        {
            ScreenCapturer.CaptureScreenRegionWithIgnoredLayouts(Browser.PageUrl, HeaderFooter.BodyElement, new List<IElement> { ProductDetail.StockCheckWrapper, SortFullPageCertona.FullPageCertonaSimilarDesignsContainer }, true);
        }
    }


    /// <summary>
    /// Verify the layout of the Sort page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7515
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7379
    /// </summary>
    //[Collection(LpTraits.UserRole.Anonymous)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7515"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7379")]
    public abstract class T7379_MobileBase : T7378_T7379_Base
    {
        protected T7379_MobileBase(ITestOutputHelper output, SharedSkus_Fixture fixture) : base(output, fixture) { }

        protected override void CapturePage()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(ProductDetail.PdHeroSpotId.ToCssIdSelector()));
            ScreenCapturer.CaptureScreenRegionWithIgnoredLayouts(Browser.PageUrl, HeaderFooter.BodyElement, new List<IElement> { SortFullPageCertona.FullPageCertonaSimilarDesignsContainer }, true, true);
        }
    }


    public class SharedSkus_Fixture : FixtureBase
    {
        public string PlaSku { get; }

        public SharedSkus_Fixture()
        {
            PlaSku = ProductActions.GetPlaSkuWithStarsQAndA();
        }
    }


    public abstract class T7378_T7379_Base : VisualTestsBase, IClassFixture<SharedSkus_Fixture>
    {
        protected readonly SharedSkus_Fixture Fixture;

        protected T7378_T7379_Base(ITestOutputHelper output, SharedSkus_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            InitializeVisualTest(config);

            var shortSku = Fixture.PlaSku;

            Browser.Navigate(Urls.ProductFullPageBaseUrl + shortSku);

            Browser.Wait.ForDomReady(30);

            CapturePage();
        }

        protected abstract void CapturePage();
    }
}