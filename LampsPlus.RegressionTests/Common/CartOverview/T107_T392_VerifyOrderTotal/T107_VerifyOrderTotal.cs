using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Enums;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.Common.CartOverview.T107_T392_VerifyOrderTotal
{
    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T107_Windows_VerifyOrderTotalsCorrect : T107_DesktopBase
    {
        public T107_Windows_VerifyOrderTotalsCorrect(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void OrderTotalsCorrect(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T107_Mac_VerifyOrderTotalsCorrect : T107_DesktopBase
    {
        public T107_Mac_VerifyOrderTotalsCorrect(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void OrderTotalsCorrect(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T107_iPad_VerifyOrderTotalsCorrect : T107_DesktopBase
    {
        public T107_iPad_VerifyOrderTotalsCorrect(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void OrderTotalsCorrect(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T107_TabletEmulator_VerifyOrderTotalsCorrect : T107_DesktopBase
    {
        public T107_TabletEmulator_VerifyOrderTotalsCorrect(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void OrderTotalsCorrect(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the order total is correct.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5129
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T107
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5129"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T107")]
    public abstract class T107_DesktopBase : TestsBaseDesktop
    {
        protected T107_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected virtual void Validate(string config)
        {
            /*Arrangement
            Add a qualifying SKU to the Cart.
            */
            InitializeFunctionalTest(config);

            var shortSku = ProductActions.GetShortSkuWithShippingCharge(SubLocationCode.Lp);

            Assert.DatabaseObject(shortSku, "ProductActions.GetShortSkuWithShippingCharge(SubLocationCode.Lp)");

            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel { Sku = shortSku });

            /*Act
            In the cart, click on the 'Change options' link.
            Enter zip code '85003' in the 'Enter ZIP/Postal Code' and click the SEARCH button\
            Select any shipping option and click the UPDATE button
            */
            Cart.EnterCartZipCodeForShippingOption(CountryCodeList.US, ZipCodeList.Phoenix, 0);
            
            /*Assert
            The Order Total is correct.
            */
            Assert.Equals(Cart.GetOrderTotalWithoutDiscount(), Cart.GetOrderTotalCost(), "Order total is not correct.");
        }
    }
}
