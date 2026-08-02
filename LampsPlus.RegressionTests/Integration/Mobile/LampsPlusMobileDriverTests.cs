using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.Integration.Mobile
{
    /// <summary>
    /// Provide a example(s) of how to use a "Mobile View". This will simulate a mobile view in Chrome.
    /// This is primarily used for test creation.
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Poc)]
    public class LampsPlusMobileDriverTests : TestsBase
    {
        /// <summary>
        /// Construct a MobileViewTest object.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public LampsPlusMobileDriverTests(ITestOutputHelper output) : base(output) { }

        /// <summary> 
        /// Provide an in context example of how to use an Appium driver.
        /// </summary>
        //[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        //[InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        //[InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        //[InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        //[InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        [SkippableTheory]
        public void AppiumOSDriverTest(string config)
        {
            InitializeFramework(config, string.Empty, true, true);

            Browser.Navigate(Urls.HomePageUrl);


            Assert.PageUrl(Urls.HomePageUrl, Browser.PageUrl, "The expected page was not found");

           // Browser.Wait.ForCondition(() => false, -1, true); // Stay on the home page for the implicit wait - 1 seconds.
        }
    }
}
