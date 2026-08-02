using System.Collections.Generic;
using OpenQA.Selenium;
using xRetry;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.VisualRegressionTests.Common.Sort
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7370_Windows_VerifyLayoutOfFilterMenuContents : T7370_DesktopBase
    {
        public T7370_Windows_VerifyLayoutOfFilterMenuContents(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_ElasticSearch)]
        public void LayoutOfFilterMenuContents(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7370_Mac_VerifyLayoutOfFilterMenuContents : T7370_DesktopBase
	{
		public T7370_Mac_VerifyLayoutOfFilterMenuContents(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
		[SkippableTheory]
		[InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
		[InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
		public void LayoutOfFilterMenuContents(string config) => Validate(Validate, config);
	}


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7370_iPad_VerifyLayoutOfFilterMenuContents : T7370_DesktopBase
    {
        public T7370_iPad_VerifyLayoutOfFilterMenuContents(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfFilterMenuContents(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7370_TabletEmulator_VerifyLayoutOfFilterMenuContents : T7370_DesktopBase
    {
        public T7370_TabletEmulator_VerifyLayoutOfFilterMenuContents(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfFilterMenuContents(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7371_iPhone_VerifyLayoutOfFilterMenuContents : T7371_MobileBase
    {
        public T7371_iPhone_VerifyLayoutOfFilterMenuContents(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_ElasticSearch)]
        public void LayoutOfFilterMenuContents(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7371_AndroidPhone_VerifyLayoutOfFilterMenuContents : T7371_MobileBase
    {
        public T7371_AndroidPhone_VerifyLayoutOfFilterMenuContents(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfFilterMenuContents(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7371_Emulator_VerifyLayoutOfFilterMenuContents : T7371_MobileBase
    {
        public T7371_Emulator_VerifyLayoutOfFilterMenuContents(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void LayoutOfFilterMenuContents(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Filter menu contents.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7512
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7370
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7512"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7370")]
    public abstract class T7370_DesktopBase : T7370_T7371_Base
    {
        protected T7370_DesktopBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }
    }


    /// <summary>
    /// Verify the layout of the Filter menu contents.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7512
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7371
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7512"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7371")]
    public abstract class T7371_MobileBase : T7370_T7371_Base
    {
        protected T7371_MobileBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected override void Validate(string config)
        {
            InitializeVisualTest(config);

            Browser.Navigate(Urls.NotFooSearchPageUrl);
            Browser.Wait.ForDomReady();
            Browser.Wait.IsVisibleElement(By.CssSelector(Sort.ToggleSortMenuClass.ToCssClassSelector()));

            CaptureScreenForFilter(Sort.FinishString);
            CaptureScreenForFilter(Sort.StyleString);
            CaptureScreenForFilter(Sort.ColorString);
            CaptureScreenForFilter(Sort.HeightString);
            CaptureScreenForFilter(Sort.SizeString);
            CaptureScreenForFilter(Sort.TypeString);
            CaptureScreenForFilter(Sort.CategoryString);
        }

		private void CaptureScreenForFilter(string filterElement)
		{
            Browser.Wait.ForElementToStopAnimating(Sort.SortResultProducts);
            Browser.Locate.ElementBySelector(Sort.SortFilterButtonTriggerSelector).Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(Sort.SortFilterAppliedFiltersCollapsibleClass.ToCssClassSelector()));

            var selectedFilterAttribute = Browser.Locate.ElementByXpath($"{Sort.FilterAttributeParentElement}//div[text()='{filterElement}']");
            Browser.ClickByJs(selectedFilterAttribute);
            Browser.Wait.IsVisibleElement(By.CssSelector(Sort.SortFilterAttributeGroupClass.ToCssClassSelector()));
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            Sort.FilterOptionCloseButton.Click();

            Browser.Wait.UntilElementDoesntExist(Sort.OverlayContentWrapperCloseButtonClass);
        }
    }

    public abstract class T7370_T7371_Base : VisualTestsBase, IClassFixture<FixtureBase>
    {
        protected T7370_T7371_Base(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected virtual void Validate(string config)
        {
            InitializeVisualTest(config);

            Browser.Navigate(Urls.NotFooSearchPageUrl);

            Browser.Wait.IsVisibleElement(By.CssSelector(Sort.MoreFiltersBtnClass.ToCssClassSelector()));

            Sort.ExpandAllFilters();
            Browser.Wait.ForDomReady();

            OpenFilterDropDown(Sort.FinishFilterElement);
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement> {Sort.SortPageH1Tag });

            OpenFilterDropDown(Sort.StyleFilterElement);
            CaptureElement(Sort.StyleFilterDropdownElement);

            OpenFilterDropDown(Sort.ColorFilterElement);
            CaptureElement(Sort.ColorFilterDropdownElement);

            OpenFilterDropDown(Sort.SaleFilterElement);
            CaptureElement(Sort.SaleFilterDropdownElement);

            OpenFilterDropDown(Sort.PriceFilterElement);
            CaptureElement(Sort.PriceFilterDropdownElement);

            OpenFilterDropDown(Sort.CategoryFilterElement);
            CaptureElement(Sort.CategoryFilterDropdownElement);

            OpenFilterDropDown(Sort.SpecialsFilterElement);
            CaptureElement(Sort.SpecialsFilterDropdownElement);
        }

        private void OpenFilterDropDown(IElement element)
        {
            element.Click();
            Browser.Wait.IsVisibleElement(By.ClassName(Sort.SortFilterDropdownContentClass));
        }

        private void CaptureElement(IElement element)
        {
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, Browser.Wait.ForDisplayedElement(element));
        }
    }
}
