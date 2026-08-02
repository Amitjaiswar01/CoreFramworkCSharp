using LampsPlus.AutomationFramework;

using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.Desktop.ApplitoolsVisual
{
    public class T7236_VerifyTheLayoutOfTheEntireHomepage : TestsBase
    {
        public T7236_VerifyTheLayoutOfTheEntireHomepage(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Take Screenshot from Home page then upload to AppliTools.
        /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7185
        /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7236
        /// </summary>
        [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7185"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7236"), Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop), Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void Test(string config)
        {
            var isFullPageScreenshot = true;

            InitializeFramework(config);
            
            Browser.Navigate(Urls.HomePageUrl);

            Browser.SetBrowserViewPortSize("Home page", config);

            Browser.Wait.ForDomReady();

            Browser.ScrollToBottomOfPage(Browser.PageUrl);

            Browser.TakeScreenshotAndUploadToApplitools(Browser.PageUrl, isFullPageScreenshot);

            Browser.CloseAppliTools();
        }
    }
}
