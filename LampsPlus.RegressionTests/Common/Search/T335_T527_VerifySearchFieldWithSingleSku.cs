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
    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T335_Windows_VerifySearchFieldWithSingleSku : T335_DesktopBase
    {
        public T335_Windows_VerifySearchFieldWithSingleSku(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_ElasticSearch)]
        public void SearchFieldWithSingleSku(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T335_Mac_VerifySearchFieldWithSingleSku : T335_DesktopBase
    {
        public T335_Mac_VerifySearchFieldWithSingleSku(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void SearchFieldWithSingleSku(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T335_iPad_VerifySearchFieldWithSingleSku : T335_DesktopBase
    {
        public T335_iPad_VerifySearchFieldWithSingleSku(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void SearchFieldWithSingleSku(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T335_TabletEmulator_VerifySearchFieldWithSingleSku : T335_DesktopBase
    {
        public T335_TabletEmulator_VerifySearchFieldWithSingleSku(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void SearchFieldWithSingleSku(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Search)]
    public class T527_iPhone_VerifySearchFieldWithSingleSku : T527_MobileBase
    {
        public T527_iPhone_VerifySearchFieldWithSingleSku(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_ElasticSearch)]
        public void SearchFieldWithSingleSku(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Search)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Search)]
    public class T527_Emulator_VerifySearchFieldWithSingleSku : T527_MobileBase
    {
        public T527_Emulator_VerifySearchFieldWithSingleSku(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void SearchFieldWithSingleSku(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the search field is cleared after successfully searching for a single SKU.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5237
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T335
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5237"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T335")]
    public abstract class T335_DesktopBase : T335_T527_Base
    {
        protected T335_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected override void VerifySearchContent(string sku)
        {
            Browser.Wait.IsVisibleElement(By.Id(GlobalLocators.PdAddToCartId));
            var searchBoxSku = Search.SearchField.GetAttribute(GlobalLocators.ValueString);
            Assert.Equals(searchBoxSku, string.Empty, "String is containing search term");
        }

        protected override void WaitForSearchField()
        {
            Browser.Wait.ForDomReady();
        }
    }


    /// <summary>
    /// Verify the search field is cleared after successfully searching for a single SKU.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5285
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T527
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5285"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T527")]
    public abstract class T527_MobileBase : T335_T527_Base
    {
        protected T527_MobileBase(ITestOutputHelper output) : base(output) { }

        protected override void VerifySearchContent(string sku)
        {
            Browser.Wait.WaitForAjaxComplete();
            Browser.Wait.ForDomReady();
            Browser.Wait.IsVisibleElement(By.Id(GlobalLocators.PdAddToCartId));
            Browser.Wait.IsVisibleElement(By.CssSelector(HeaderFooter.ToggleSearchClass.ToCssClassSelector()));
            HeaderFooter.SearchIcon.Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(Search.GlobalSearchFieldId.ToCssIdSelector()));
            var searchBoxSku = Search.SearchField.GetAttribute(GlobalLocators.ValueString);
            Assert.Equals(searchBoxSku, string.Empty, "String is containing search term");
        }

        protected override void WaitForSearchField()
        {
            Browser.Wait.ForClickableElement(Search.SearchButton);
        }
    }


    public abstract class T335_T527_Base : SearchTestsBase
    {
        protected T335_T527_Base(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            InitializeFramework(config, Urls.HomePageUrl);

            var sku = ProductActions.GetAnySkuWithProductDetailPage;
            Assert.DatabaseObject(sku, "ProductActions.GetAnySkuWithProductDetailPage()");

            WaitForSearchField();
            Search.SearchField.Click();
            Search.ExecuteSearch(sku);
            
            Browser.Wait.WaitForAjaxComplete();

            if (Browser.PageUrl.Contains("?s=1"))//TODO Verify if search returns multiple results
            {
                Sort.FirstDisplayedProductElement.Click();
                Browser.Wait.ForDomReady();
            }

            VerifySearchContent(sku);
        }

        protected abstract void VerifySearchContent(string sku);

        protected abstract void WaitForSearchField();
    }
}
