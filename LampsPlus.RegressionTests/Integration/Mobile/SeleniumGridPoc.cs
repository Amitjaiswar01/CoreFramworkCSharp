using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.Integration.Mobile
{
    /// <summary>
    /// Temporary Test Class to support https://lampstrack.lampsplus.com:8443/browse/ACD-6241
    /// </summary>
    public class SeleniumGridPoc : TestsBase
    {
        public SeleniumGridPoc(ITestOutputHelper output) : base(output) { }

        /// <summary> 
        /// Provide an in context example of how to use an Appium driver.
        /// </summary>
        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        [SkippableTheory]
        public void AppiumOSDriverTest(string config)
        {
            InitializeFramework(config, string.Empty, true, true);

            Browser.Navigate(Urls.HomePageUrl);
        }
    }
}
