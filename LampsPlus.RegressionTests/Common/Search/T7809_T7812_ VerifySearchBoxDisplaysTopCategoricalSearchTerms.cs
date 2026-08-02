using System;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.Common.Search
{
    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7809_Windows_VerifySearchBoxDisplaysTopCategorialSearchTerms : T7809_DesktopBase
    {
        public T7809_Windows_VerifySearchBoxDisplaysTopCategorialSearchTerms(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T7809. Rework - ACD-10051")]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_ElasticSearch)]
        public void VerifySearchBoxDisplaysTopCategorialSearchTerms(string config) => Validate(config);
    }
    

    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7809_Mac_VerifySearchBoxDisplaysTopCategorialSearchTerms : T7809_DesktopBase
    {
        public T7809_Mac_VerifySearchBoxDisplaysTopCategorialSearchTerms(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifySearchBoxDisplaysTopCategorialSearchTerms(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7809_iPad_VerifySearchBoxDisplaysTopCategorialSearchTerms : T7809_DesktopBase
    {
        public T7809_iPad_VerifySearchBoxDisplaysTopCategorialSearchTerms(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifySearchBoxDisplaysTopCategorialSearchTerms(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T7812_Emulator_VerifySearchBoxDisplaysTopCategorialSearchTerms : T7812_MobileBase
    {
        public T7812_Emulator_VerifySearchBoxDisplaysTopCategorialSearchTerms(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void VerifySearchBoxDisplaysTopCategorialSearchTerms(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Search)]
    public class T7812_iPhone_VerifySearchBoxDisplaysTopCategorialSearchTerms : T7812_MobileBase
    {
        public T7812_iPhone_VerifySearchBoxDisplaysTopCategorialSearchTerms(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T7812. Rework - ACD-10051")]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_ElasticSearch)]
        public void VerifySearchBoxDisplaysTopCategorialSearchTerms(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Search)]
    public class T7812_Android_VerifySearchBoxDisplaysTopCategorialSearchTerms : T7812_MobileBase
    {
        public T7812_Android_VerifySearchBoxDisplaysTopCategorialSearchTerms(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void VerifySearchBoxDisplaysTopCategorialSearchTerms(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that Search Box Displays Top Categorical Search Terms.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9404
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7809
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9404"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7809")]
    public abstract class T7809_DesktopBase : T7809_T7812_Base
    {
        protected T7809_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected override void ApplyRandomFilters()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(Sort.SortFilterDisplaySetDropdownsClass.ToCssClassSelector()));

            var attempts = MathHelper.GetRandomNumber(1, 2);
            var appliedFilters = Sort.ApplyFilters(attempts);

            Search.SearchField.Click();
        }
    }


    /// <summary>
    /// Verify that Search Box Displays Top Categorical Search Terms.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9404
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7812
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9404"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7812")]
    public abstract class T7812_MobileBase : T7809_T7812_Base
    {
        protected T7812_MobileBase(ITestOutputHelper output) : base(output) { }

        protected override void ApplyRandomFilters()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(Sort.SortFilterButtonTriggerClass.ToCssClassSelector()));
            Browser.ScrollToBottomOfWindow();
            Browser.ScrollToTopOfWindow();
            var attempts = MathHelper.GetRandomNumber(1, 2);
            Sort.ApplyFilters(attempts);

            Browser.Wait.IsInvisibleElement(By.CssSelector(ProductDetail.OverlayContentWrapperCloseButtonClass.ToCssClassSelector()));
            SearchWorkflow.EnableSearch();
        }

    }

    public abstract class T7809_T7812_Base : SearchTestsBase
    {
        protected T7809_T7812_Base(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            InitializeFramework(config);

            Browser.Navigate(Urls.AllChandeliersSortPageUrl);

            ApplyRandomFilters();

            Browser.Wait.IsInvisibleElement(By.CssSelector(Search.Ie11WrapperRecentSearchesHiddenClass.ToCssClassSelector()));

            var totalTopCategorySearchesCount = Search.ListOfTopSearchesProducts.Count;

            var topCategorySearchModalData = Search.TopSearchesModalContent.Text.Replace("Top Chandeliers Searches", string.Empty).TrimStart();
            var topSearchAllContent = topCategorySearchModalData.Replace(Environment.NewLine, " ");

            for (var topCategorySearchesTerm = 0; topCategorySearchesTerm < totalTopCategorySearchesCount; topCategorySearchesTerm++)
            {
                var topSearchValue = Search.TopSearchesDropDown(topCategorySearchesTerm).Text;

                Assert.StringContains(topSearchAllContent, topSearchValue, "Top Searches does not matches");
            }
        }
        protected abstract void ApplyRandomFilters();
    }
}
