using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

using LampsPlus.RegressionTests.Desktop.ApplitoolsVisual;

using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.Mobile.ApplitoolsVisual
{
    public class T7244_VerifyTheHeaderMenusOnTheHomepageAppearCorrectlyWhenTheyAreClosed : ApplitoolsVisualTestsBase
    {
        public T7244_VerifyTheHeaderMenusOnTheHomepageAppearCorrectlyWhenTheyAreClosed(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Take Screenshot from Home page then upload to AppliTools.
        /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7185
        /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7244
        /// </summary>
        [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7185"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7244"), Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void Test(string config)
        {
            var isFullPageScreenshot = false;

            InitializeFramework(config);

            Browser.Navigate(Urls.HomePageUrl);

            Browser.SetBrowserViewPortSize("Mobile Home page", config);

            Browser.Wait.ForDomReady();

            Browser.ScrollToBottomOfPage(Browser.PageUrl);

            Browser.TakeScreenshotAndUploadToApplitools(Browser.PageUrl, isFullPageScreenshot);

            Browser.CloseAppliTools();
        }
    }
}
