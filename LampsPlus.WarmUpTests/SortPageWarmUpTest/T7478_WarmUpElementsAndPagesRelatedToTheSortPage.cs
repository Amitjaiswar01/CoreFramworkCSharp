using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Environment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.Common.Sort;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.WarmUpTests.SortPageWarmUpTest
{
    public class T7478_Window_WarmUpElementsAndPagesRelatedToTheSortPage : T7478_DesktopBase
    {
        public T7478_Window_WarmUpElementsAndPagesRelatedToTheSortPage(ITestOutputHelper output) : base(output) { }
        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void ElementsAndPagesRelatedToTheSortPage(string config = TestConfiguration.Windows_Chrome_SNIS_UNSI) => Validate(config);
    }


    // <Summary>
    // Warm up elements and pages related to the Sort page.
    // Jira Task link: https://lampstrack.lampsplus.com:8443/browse/ACD-8403
    // Test case link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7478
    // </Summary>
    [Trait(LpTraits.Keys.Category,LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId , "ACD-8403"),Trait(LpTraits.RequiredTestCaseTags.TId, "T7478") ]
    public abstract class T7478_DesktopBase : SortTestsBase
    {
        protected T7478_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            var setup = new TestSetup(config);
            InitializeFramework(config, setup: setup);

            Browser.OpenNewTab(Urls.NotFooSearchPageUrl);
            Browser.OpenNewTab(Urls.ColorPlusPageUrl);
            Browser.OpenNewTab(Urls.RoomInspirationUrl);
            Browser.OpenNewTab(Urls.ShopByTrendUrl);          
        }
    }
}
