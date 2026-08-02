using Automation.Framework.Core;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.Unit.Mobile
{
    public class iPadUnitTests : TestsBase
    {
        public iPadUnitTests(ITestOutputHelper output, bool enableRealTimeLogging = false) : base(output, enableRealTimeLogging)
        {
        }

        [Trait(LpTraits.Keys.Category, LpTraits.Unit.MobileIpadSafari)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void ClearSafariValidation(string config)
        {
            InitializeFramework(config, string.Empty, false, true, true);
            ((IphoneBrowser)Browser).ClearBrowserHistoryAndWebsiteData();
        }

        [Trait(LpTraits.Keys.Category, LpTraits.Unit.SwitchLpInstanceIpad)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void SwitchLpInstance(string config)
        {
            InitializeFramework(config, string.Empty, false, true, true, visualTestAccount: false, isInstanceSwitchMobile: true);
        }
    }
}