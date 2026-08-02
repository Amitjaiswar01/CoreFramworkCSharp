using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.DesktopTablet.CreateAccount
{
    /// <summary>
    /// Base class for Create Account specific tests.
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Regression)]
    [Trait(LpTraits.Keys.Feature, LpTraits.RegressionFeatureTags.CreateAccount)]
    public class CreateAccountTestsBase : TestsBase
    {
        /// <summary>
        /// Base class for Create Account specific tests.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public CreateAccountTestsBase(ITestOutputHelper output) : base(output) { }
    }
}
