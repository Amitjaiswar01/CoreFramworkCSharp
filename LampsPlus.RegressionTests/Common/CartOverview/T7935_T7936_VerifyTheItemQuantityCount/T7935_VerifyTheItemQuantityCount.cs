using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.CartOverview.T7935_T7936_VerifyTheItemQuantityCount
{
    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T7935_Windows_VerifyTheItemQuantityCount : T7935_DesktopBase
    {
        public T7935_Windows_VerifyTheItemQuantityCount(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyTheItemQuantityCount(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T7935_Mac_VerifyTheItemQuantityCount : T7935_DesktopBase
    {
        public T7935_Mac_VerifyTheItemQuantityCount(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyTheItemQuantityCount(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T7935_iPad_VerifyTheItemQuantityCount : T7935_DesktopBase
    {
        public T7935_iPad_VerifyTheItemQuantityCount(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyTheItemQuantityCount(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T7935_TabletEmulator_VerifyTheItemQuantityCount : T7935_DesktopBase
    {
        public T7935_TabletEmulator_VerifyTheItemQuantityCount(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyTheItemQuantityCount(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the Count on the Cart Overview Page is Correct.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10594
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7935
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10594"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7935")]
    public abstract class T7935_DesktopBase : TestsBaseDesktop
    {
        protected T7935_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            // Arrangement
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