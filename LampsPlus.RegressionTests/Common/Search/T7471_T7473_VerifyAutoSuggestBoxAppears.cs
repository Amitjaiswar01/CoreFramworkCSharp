using System.Linq;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using OpenQA.Selenium;
using xRetry;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.Common.Search
{
    [Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7471_Windows_VerifyAutoSuggestBoxAppears : T7471_DesktopBase
	{
        public T7471_Windows_VerifyAutoSuggestBoxAppears(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_ElasticSearch)]
        public void VerifyAutoSuggestBoxAppears(string config) => Validate(config);
	}


    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7471_Mac_VerifyAutoSuggestBoxAppears : T7471_DesktopBase
    {
        public T7471_Mac_VerifyAutoSuggestBoxAppears(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyAutoSuggestBoxAppears(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7471_iPad_VerifyAutoSuggestBoxAppears : T7471_DesktopBase
    {
        public T7471_iPad_VerifyAutoSuggestBoxAppears(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyAutoSuggestBoxAppears(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7471_TabletEmulator_VerifyAutoSuggestBoxAppears : T7471_DesktopBase
    {
        public T7471_TabletEmulator_VerifyAutoSuggestBoxAppears(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyAutoSuggestBoxAppears(string config) => Validate(config);
    }
 

    [Collection(LpTraits.BatchGroup.Mobile.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Search)]
	public class T7473_iPhone_VerifyAutoSuggestBoxAppears : T7473_MobileBase
	{
        public T7473_iPhone_VerifyAutoSuggestBoxAppears(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_ElasticSearch)]
        public void VerifyAutoSuggestBoxAppears(string config) => Validate(config);
	}


    [Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
	public class T7473_Emulator_VerifyAutoSuggestBoxAppears : T7473_MobileBase
	{
		public T7473_Emulator_VerifyAutoSuggestBoxAppears(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void VerifyAutoSuggestBoxAppears(string config) => Validate(config);
	}


    /// <summary>
    /// Verify that the auto-suggest box appears with search options relevant to the search term and that the user is directed to the correct page after selecting an option.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8284
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7471
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8284"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7471")]
	public abstract class T7471_DesktopBase : T7471_T7473_Base
	{
		protected T7471_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected override void WaitForSortPage()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(Sort.MoreFiltersBtnClass.ToCssClassSelector()));
        }
    }


    /// <summary>
    /// Verify that the auto-suggest box appears with search options relevant to the search term and that the user is directed to the correct page after selecting an option.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5285
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7473
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8284"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7473")]
	public abstract class T7473_MobileBase : T7471_T7473_Base
	{
		protected T7473_MobileBase(ITestOutputHelper output) : base(output) { }

        protected override void Validate(string config)
        {
            InitializeFramework(config);

            Browser.Wait.IsVisibleElement(By.CssSelector(Search.GlobalSearchFieldId.ToCssIdSelector()));

            SearchForTextAndVerify();
        }

        protected override void WaitForSortPage()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(Sort.SortFilterButtonTriggerClass.ToCssClassSelector()));
        }
    }


	public abstract class T7471_T7473_Base : SearchTestsBase
    {
        protected T7471_T7473_Base(ITestOutputHelper output) : base(output) { }
		
		protected virtual void Validate(string config)
        {
            InitializeFramework(config);

            Browser.Wait.ForDomReady();

            SearchForTextAndVerify();
        }

        protected void SearchFindTextAndVerifyLink(string searchText, string textToFind, string urlToVerify)
        {
            Browser.Wait.ForDomReady();
            Search.SearchField.Clear();
            Search.SearchField.SendKeys(searchText);

            Browser.Wait.IsVisibleElement(By.CssSelector(Search.AutoSuggestDropDownResultClass.ToCssClassSelector()));

            var linkToClick = Search.AutoSuggestDropDownResults.FirstOrDefault(result => result.Text == textToFind);

            Assert.True(linkToClick != null, $"The following is not displayed in the list of options: {textToFind}.");
            
            if (linkToClick != null)
            {
                linkToClick.Click();

                WaitForSortPage();
                
                Browser.Wait.ForCondition(() => Sort.SortPageH1Tag.Text.ToLower().TrimEnd().Contains(searchText));

                Assert.Equals(urlToVerify, Browser.PageUrl, $"The user is not directed to the following page: {urlToVerify}");
            }

            Browser.Navigate(Urls.HomePageUrl);
            Browser.Wait.ForDomReady();
        }

        protected abstract void WaitForSortPage();

        protected void SearchForTextAndVerify()
        {
            SearchFindTextAndVerifyLink("lamp", "lamp shades", "https://www.lampsplus.com/products/s_lamp-shades/?s=1");
            SearchFindTextAndVerifyLink("bathroom", "bathroom vanity lights", "https://www.lampsplus.com/products/s_bathroom-vanity-lights/?s=1");
            //SearchFindTextAndVerifyLink("wall", "wall sconces", "https://www.lampsplus.com/products/s_wall-sconces/?s=1"); TODO: Awaiting response from architects about the way the H1 is constructed for this page.
            SearchFindTextAndVerifyLink("table", "table lamps", "https://www.lampsplus.com/products/s_table-lamps/?s=1");
            SearchFindTextAndVerifyLink("floor", "floor lamps", "https://www.lampsplus.com/products/s_floor-lamps/?s=1");
        }
    }
}
