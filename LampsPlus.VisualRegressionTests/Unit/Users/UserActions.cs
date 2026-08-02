using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.VisualRegressionTests.Unit.Users
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class UserActions : VisualTestsBase, IClassFixture<FixtureBase>
    {
        protected readonly FixtureBase Fixture;

        public UserActions(ITestOutputHelper output, FixtureBase fixtureBase) : base(output, fixtureBase)
        {
            Fixture = fixtureBase;
        }

        [Trait(LpTraits.Keys.Category, LpTraits.AccountActions.EnableDisabledTestAccountsWindows)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        public void EnableDisabledUsersIos(string config) => Validate(Validate, config);


        [Trait(LpTraits.Keys.Category, LpTraits.AccountActions.EnableDisabledTestAccountsWindows)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        public void EnableDisabledUsersWindows(string config) => Validate(Validate, config);

        protected void Validate(string config)
        {
            InitializeVisualTest(config, string.Empty, false, true);
            Skip.IfNot(DevEnvInformation.DatabaseSymbol == "T", "This test can only be executed against DBTEST.");

            AccountActions.ActivateUsersByFirstName("Auto-CSI");
        }
    }
}