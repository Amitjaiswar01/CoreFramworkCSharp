using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;

using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.Desktop.ApplitoolsVisual
{
    /// <summary>
    /// Base class for AppliTool Visual Testing POC tests.
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Regression)]
    //[Trait(LpTraits.Feature, LpTraits.AppliToolsVisualTestingPoc)]
    public class ApplitoolsVisualTestsBase : TestsBase
    {
        public ApplitoolsVisualTestsBase(ITestOutputHelper output) : base(output) { }
    }
}
