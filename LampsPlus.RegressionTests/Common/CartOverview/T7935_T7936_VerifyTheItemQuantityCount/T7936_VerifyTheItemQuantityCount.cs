using Xunit;
using Xunit.Abstractions;
using xRetry;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.CartOverview.T7935_T7936_VerifyTheItemQuantityCount
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T7936_iPhone_VerifyTheItemQuantityCount : T7936_MobileBase
    {
        public T7936_iPhone_VerifyTheItemQuantityCount(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifyTheItemQuantityCount(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T7936_Emulator_VerifyTheItemQuantityCount : T7936_MobileBase
    {
        public T7936_Emulator_VerifyTheItemQuantityCount(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyTheItemQuantityCount(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the Count on the Cart Overview Page is Correct
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10594
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7936
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10594"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7936")]
    public abstract class T7936_MobileBase : TestsBaseMobile
    {
        protected T7936_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            // Arrange
            InitializeFunctionalTest(config);

            // Act - User adds two items in cart with random quantity 
            var quantityList = ShoppingCartWorkflow.SelectRandomQuantityAndAddToCart(2);
            var cartItemsList = Cart.GetListOfAllProductsOnCartPage();

            // Assert - Verify if the Quantity added in pdp matches with that on the Cart 
            Assert.Equals(quantityList[0], cartItemsList[1].Quantity.ToString(), "Quantity added in Cart does not match with quantity entered on Pdp");
            Assert.Equals(quantityList[1], cartItemsList[0].Quantity.ToString(), "Quantity added in Cart does not match with quantity entered on Pdp");
        }
    }
}