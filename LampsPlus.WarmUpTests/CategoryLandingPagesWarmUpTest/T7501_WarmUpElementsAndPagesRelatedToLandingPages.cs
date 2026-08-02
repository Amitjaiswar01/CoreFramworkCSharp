using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Environment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.Common.Sort;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.WarmUpTests.CategoryLandingPagesWarmUpTest
{
    public class T7501_WarmUpElementsAndPagesRelatedToLandingPages : T7501_DesktopBase
    {
        public T7501_WarmUpElementsAndPagesRelatedToLandingPages(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void WarmUpTestForLandingPages(string config) => Validate(config);
    }

    /// <summary>
    /// Warm up elements and pages related to Landing Pages
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8398
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7501
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8398"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7501")]
    public abstract class T7501_DesktopBase : SortTestsBase
    {
        protected T7501_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            var setup = new TestSetup(config);

            InitializeFramework(config, setup: setup);

            Browser.OpenNewTab(Urls.CeilingFansUrl);
            Browser.OpenNewTab(Urls.SubCategoryUrls["Ceiling Lighting"]);
            Browser.OpenNewTab(Urls.SubCategoryUrls["Chandeliers"]);
            Browser.OpenNewTab(Urls.SubCategoryUrls["Floor Lamps"]);
            Browser.OpenNewTab(Urls.SubCategoryUrls["Furniture"]);
            Browser.OpenNewTab(Urls.SubCategoryUrls["Home Decor"]);
            Browser.OpenNewTab(Urls.SubCategoryUrls["Kitchen Lighting"]);
            Browser.OpenNewTab(Urls.SubCategoryUrls["Landscape Lighting"]);
            Browser.OpenNewTab(Urls.SubCategoryUrls["Lamps"]);
            Browser.OpenNewTab(Urls.SubCategoryUrls["Lamp Shades"]);
            Browser.OpenNewTab(Urls.SubCategoryUrls["Lighting Fixtures"]);
            Browser.OpenNewTab(Urls.SubCategoryUrls["Mirrors"]);
            Browser.OpenNewTab(Urls.SubCategoryUrls["Outdoor Lighting"]);
            Browser.OpenNewTab(Urls.SubCategoryUrls["Pendant Lighting"]);
            Browser.OpenNewTab(Urls.SubCategoryUrls["Table Lamps"]);
            Browser.OpenNewTab(Urls.SubCategoryUrls["Wall Lights"]);
        }
    }
}  
