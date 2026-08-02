using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.VisualRegressionTests.Common.Samples
{
    public class HowToShareSkus : HowToShareSkus_Base
    {
        public HowToShareSkus(ITestOutputHelper output, HowToShareSkus_Fixture fixture) : base(output, fixture){}

        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)] // Have a Baseline first
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void Test_Sharing_Skus_Between_Test_Run(string config) => Validate(Validate, config);

    }


    /// <summary>
    /// In order to share data between test run, we will need to create a class inheriting from FixtureBase class
    /// Then, use the constructor to allocate or retrieve the data we want to share between test run.
    /// </summary>
    public class HowToShareSkus_Fixture : FixtureBase
    {

        public string ShortSku1 { get; }
        public string ShortSku2 { get; }

        public HowToShareSkus_Fixture()
        {
            // To share data between test run, the data needs to be retrieved in the constructor,
            // which will be invoked once regardless of how many Inlinedata attribute is used.
            ShortSku1 = ProductActions.GetAnySkuWithProductDetailPage;
            ShortSku2 = ProductActions.GetAnySkuWithProductDetailPage;
        }
    }


    /// <summary>
    /// Step 1: Create an abstract base class that ending with "_Base" suffix
    /// Step 2: This base class should inherit from VisualTestsBase
    /// Step 3: This base class should conform IClassFixture<T> where T is a class inheriting from FixtureBase
    /// </summary>    
    public abstract class HowToShareSkus_Base : VisualTestsBase, IClassFixture<HowToShareSkus_Fixture>
    {
        protected readonly HowToShareSkus_Fixture Fixture;
        
        protected HowToShareSkus_Base(ITestOutputHelper output, HowToShareSkus_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            // Arrange
            var expectedShortSku1 = Fixture.ShortSku1;
            var expectedShortSku2 = Fixture.ShortSku2;

            // Notice that we will need to use InitializeVisualTest method instead of InitializeFramework method
            InitializeVisualTest(config);

            // Act
            Log.Message($"TestName: {TestName}");
            Log.Message($"EnvironmentUnderTest: {TestSetup.TestConfiguration.EnvironmentUnderTest}");
            Log.Message($"AccountUnderTest: {TestSetup.AccountConfig.AccountUnderTest.UserName}");

            // Pretend that we take some actions
            Log.Message($"ShortSku1:{expectedShortSku1}");
            Log.Message($"ShortSku2:{expectedShortSku2}");
            // Assert
            // Pretend that we are doing some real assertion.
            Xunit.Assert.True(1 == 1);
        }
    }
}
