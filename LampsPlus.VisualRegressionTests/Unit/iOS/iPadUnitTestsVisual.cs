using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;

namespace LampsPlus.VisualRegressionTests.Unit.iOS
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class iPadUnitTestsVisual : VisualTestsBase, IClassFixture<FixtureBase>
    {
        protected readonly FixtureBase Fixture;

        public iPadUnitTestsVisual(ITestOutputHelper output, FixtureBase fixtureBase) : base(output, fixtureBase)
        {
            Fixture = fixtureBase;
        }

        [Trait(LpTraits.Keys.Category, LpTraits.Unit.SwitchLpInstanceIpadVisual)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void SwitchLpInstanceVisual(string config) => Validate(Validate, config);

        protected void Validate(string config)
        {
            InitializeVisualTest(config, string.Empty, false, true, isVisualInstanceSwitchTest: true);
        }
    }
}