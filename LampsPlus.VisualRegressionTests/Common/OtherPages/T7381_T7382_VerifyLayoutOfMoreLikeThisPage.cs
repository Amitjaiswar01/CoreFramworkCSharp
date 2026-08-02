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
    public class T7381_Window_VerifyLayoutOfMoreLikeThisPage : T7381_DesktopBase
    {
        public T7381_Window_VerifyLayoutOfMoreLikeThisPage(ITestOutputHelper output, T7381_T7382_SharedUrl_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfMoreLikeThisPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7381_Mac_VerifyLayoutOfMoreLikeThisPage : T7381_DesktopBase
    {
        public T7381_Mac_VerifyLayoutOfMoreLikeThisPage(ITestOutputHelper output, T7381_T7382_SharedUrl_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfMoreLikeThisPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7381_iPad_VerifyLayoutOfMoreLikeThisPage : T7381_DesktopBase
    {
        public T7381_iPad_VerifyLayoutOfMoreLikeThisPage(ITestOutputHelper output, T7381_T7382_SharedUrl_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfMoreLikeThisPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7381_TabletEmulator_VerifyLayoutOfMoreLikeThisPage : T7381_DesktopBase
    {
        public T7381_TabletEmulator_VerifyLayoutOfMoreLikeThisPage(ITestOutputHelper output, T7381_T7382_SharedUrl_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfMoreLikeThisPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7382_iPhone_VerifyLayoutOfMoreLikeThisPage : T7382_MobileBase
    {
        public T7382_iPhone_VerifyLayoutOfMoreLikeThisPage(ITestOutputHelper output, T7381_T7382_SharedUrl_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth)]
        public void LayoutOfMoreLikeThisPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7382_AndroidPhone_VerifyLayoutOfMoreLikeThisPage : T7382_MobileBase
    {
        public T7382_AndroidPhone_VerifyLayoutOfMoreLikeThisPage(ITestOutputHelper output, T7381_T7382_SharedUrl_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfMoreLikeThisPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7382_Emulator_VerifyLayoutOfMoreLikeThisPage : T7382_MobileBase
    {
        public T7382_Emulator_VerifyLayoutOfMoreLikeThisPage(ITestOutputHelper output, T7381_T7382_SharedUrl_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfMoreLikeThisPage(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the More Like This page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7516
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7381
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7516"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7381")]
    public abstract class T7381_DesktopBase : T7381_T7382_Base
    {
        protected T7381_DesktopBase(ITestOutputHelper output, T7381_T7382_SharedUrl_Fixture fixture) : base(output, fixture) { }
    }


    /// <summary>
    /// Verify the layout of the More Like This page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7516
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7382
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7516"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7382")]
    public abstract class T7382_MobileBase : T7381_T7382_Base
    {
        protected T7382_MobileBase(ITestOutputHelper output, T7381_T7382_SharedUrl_Fixture fixture) : base(output, fixture) { }
    }


    public class T7381_T7382_SharedUrl_Fixture : FixtureBase
    {
        public string Sku { get; }

        public T7381_T7382_SharedUrl_Fixture()
        {
            Sku = ProductActions.GetAnySkuWithProductDetailPage;
        }
    }

    public abstract class T7381_T7382_Base : VisualTestsBase, IClassFixture<T7381_T7382_SharedUrl_Fixture>
    {
        protected readonly T7381_T7382_SharedUrl_Fixture Fixture;

        protected T7381_T7382_Base(ITestOutputHelper output, T7381_T7382_SharedUrl_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }
        
        protected void Validate(string config)
        {
            InitializeVisualTest(config);

            Browser.Navigate(Urls.MoreLikeThisPageBaseUrl + Fixture.Sku);

            Browser.Wait.IsVisibleElement(By.CssSelector(Sort.MoreLikeThisClass.ToCssClassSelector()));

            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { HeaderFooter.FooterContainer }, true, true);
        }
    }
}
