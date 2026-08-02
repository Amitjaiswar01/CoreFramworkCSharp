using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.DesktopTablet.OrderConfirmation
{
    /// <summary>
    /// Base class for Submitting Orders specific tests.
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Regression)]
    [Trait(LpTraits.Keys.Feature, LpTraits.RegressionFeatureTags.OrderConfirmation)]
    public class OrderConfirmationTestsBase : TestsBase
    {
        /// <summary>
        /// Common functionality for Submitting Orders tests.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public OrderConfirmationTestsBase(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify Shipping Label on Order Confirmation Page
        /// </summary>
        /// <param name="element">Item element from order details.</param>
        /// <param name="label">Correct string of copy to compare to.</param>
        public void VerifyOrderShippingLabel(IWebElement element, string label)
        {
            Assert.True(element.Text == label, $"{element.Text} does not match {label}");
        }
    }
}
