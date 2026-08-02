using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Enums;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;
using xRetry;

namespace LampsPlus.RegressionTests.Common.CartOverview.T107_T392_VerifyOrderTotal
{
    public class T392_VerifyOrderTotalsCorrect
    {
        //[Collection(LpTraits.BatchGroup.Mobile.CartOverview)]
        [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.CartOverview)]
        public class T392_IPhone_VerifyOrderTotalsCorrect : T392_MobileBase
        {
            public T392_IPhone_VerifyOrderTotalsCorrect(ITestOutputHelper output) : base(output) { }

            [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
            [RetryTheory(3)]
            [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
            public void OrderTotalsCorrect(string config) => Validate(config);
        }


        //[Collection(LpTraits.BatchGroup.Mobile.CartOverview)]
        [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.CartOverview)]
        public class T392_AndroidPhone_VerifyOrderTotalsCorrect : T392_MobileBase
        {
            public T392_AndroidPhone_VerifyOrderTotalsCorrect(ITestOutputHelper output) : base(output) { }

            [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
            [SkippableTheory]
            [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
            public void OrderTotalsCorrect(string config) => Validate(config);
        }


        //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
        [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
        public class T392_Emulator_VerifyOrderTotalsCorrect : T392_MobileBase
        {
            public T392_Emulator_VerifyOrderTotalsCorrect(ITestOutputHelper output) : base(output) { }

            [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
            [SkippableTheory]
            [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
            public void OrderTotalsCorrect(string config) => Validate(config);
        }


        /// <summary>
        /// Verify that the order total is correct.
        /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5478
        /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T392
        /// </summary>
        [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
        [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5478"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T392")]
        public abstract class T392_MobileBase : TestsBaseMobile
        {
            protected T392_MobileBase(ITestOutputHelper output) : base(output) { }

            protected void Validate(string config)
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
}
