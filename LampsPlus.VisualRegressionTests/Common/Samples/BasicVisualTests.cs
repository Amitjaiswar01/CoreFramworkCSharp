using LampsPlus.AutomationFramework.Utilities.Environment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.VisualRegressionTests.Common.Samples
{
    /// <summary>
    /// This class demonstrates the basic visual test.
    /// This test does use neither product data nor user account to login
    /// Notice that the constructor requires FixtureBase type of parameter.
    /// </summary>
    public class BasicVisualTest : BasicVisualTest_Base
    {
        public BasicVisualTest(ITestOutputHelper output, FixtureBase fixtureBase) : base(output, fixtureBase) { }

        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)] // Have a Baseline first
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void Test_Basic_VisualTest(string config) => Validate(config);

    }

    /// <summary>
    /// This class demonstrates the basic visual test, but requires user login.
    /// Notice that user login is being handled by FixtureBase class seamlessly.
    /// FixtureBase class ensures that the same user account will be used between test run.
    /// </summary>
    public class BasicVisualTest_UserAccount : BasicVisualTest_Base
    {
        public BasicVisualTest_UserAccount(ITestOutputHelper output, FixtureBase fixtureBase) : base(output, fixtureBase) { }

        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void Test_Basic_VisualTest_With_UserAccount(string config) => Validate(config);
    }

    /// <summary>
    /// This class demonstrate how to configure account configuration.
    /// </summary>
    public class BasicVisualTest_UserAccount_AccountConfiguration : BasicVisualTest_Base
    {
        public BasicVisualTest_UserAccount_AccountConfiguration(ITestOutputHelper output, FixtureBase fixtureBase) : base(output, fixtureBase) { }

        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void Test_Basic_VisualTest_With_UserAccount_And_AccountConfiguration(string config)
        {
            var accountConfiguration = new AccountConfiguration
            {
                ClearSavedPaymentOptionsOnSetup = false,
                ClearStoreInSessionOnSetup = true,
                // Set properties as you wish
            };

            Validate(config, accountConfiguration);
        }
    }

    /// <summary>
    /// Visualization Test Base class should inherits from VisualTestBase class
    /// and IClassFixture<FixtureBase>     
    /// </summary>
    public abstract class BasicVisualTest_Base : VisualTestsBase, IClassFixture<FixtureBase>
    {
        public BasicVisualTest_Base(ITestOutputHelper output, FixtureBase fixtureBase) : base(output, fixtureBase){}

        protected void Validate(string config, AccountConfiguration accountConfiguration = null)
        {
            // Arrange:
            InitializeVisualTest(config, accountConfiguration:accountConfiguration);

            // Act:
            Log.Message($"TestName: {TestName}");
            Log.Message($"EnvironmentUnderTest{TestSetup.TestConfiguration.EnvironmentUnderTest}");
            Log.Message($"AccountUnderTest: {TestSetup.AccountConfig.AccountUnderTest.UserName}");

            // Pretend that we take some actions.

            // Assert:
            // Pretend that we are doing some real assertion.
            Xunit.Assert.True(1 == 1);
        }
    }


}
