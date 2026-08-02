using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.DesktopTablet.SignIn
{
    /// <summary>
    /// Base class for Secure Sign In specific tests.
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Regression)]
    [Trait(LpTraits.Keys.Feature, LpTraits.RegressionFeatureTags.SignIn)]
    public class SignInTestsBase : TestsBase
    {
        /// <summary>
        /// Common functionality to for Secure Sign In tests.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public SignInTestsBase(ITestOutputHelper output) : base(output) { }
    }
}
