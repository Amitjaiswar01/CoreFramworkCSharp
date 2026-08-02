using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.Unit.Users
{

    public class UserActions : TestsBase
    {
        public UserActions(ITestOutputHelper output, bool enableRealTimeLogging = false) : base(output, enableRealTimeLogging)
        {
        }

        [Trait(LpTraits.Keys.Category, LpTraits.AccountActions.EnableDisabledTestAccountsWindows)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void EnableDisabledUsersWindows(string config)
        {
            InitializeFramework(config, string.Empty, false, true, true, visualTestAccount: false);
            Skip.IfNot(DevEnvInformation.DatabaseSymbol == "T", "This test can only be executed against DBTEST.");

            AccountActions.ActivateUsersByFirstName("Auto-CSI");
        }

        [Trait(LpTraits.Keys.Category, LpTraits.AccountActions.EnableDisabledTestAccountsIos)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void EnableDisabledUsersIos(string config)
        {
            InitializeFramework(config, string.Empty, false, true, true, visualTestAccount: false);
            Skip.IfNot(DevEnvInformation.DatabaseSymbol == "T", "This test can only be executed against DBTEST.");

            AccountActions.ActivateUsersByFirstName("Auto-CSI");
        }
    }
}