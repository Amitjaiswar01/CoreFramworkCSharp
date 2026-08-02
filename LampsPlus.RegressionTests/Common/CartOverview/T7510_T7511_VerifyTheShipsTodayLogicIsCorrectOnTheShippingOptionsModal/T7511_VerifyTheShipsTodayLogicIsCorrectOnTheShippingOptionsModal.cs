using Xunit;
using Xunit.Abstractions;
using xRetry;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.CartOverview.T7510_T7511_VerifyTheShipsTodayLogicIsCorrectOnTheShippingOptionsModal
{

    public class T7511_VerifyTheShipsTodayLogicIsCorrect
    {
        //[Collection(LpTraits.BatchGroup.Mobile.CartOverview)]
        [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.CartOverview)]
        public class T7511_iPhone_VerifyTheShipsTodayLogicIsCorrect : T7511_MobileBase
        {
            public T7511_iPhone_VerifyTheShipsTodayLogicIsCorrect(ITestOutputHelper output) : base(output) { }

            [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
            [RetryTheory(3)]
            [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
            public void ShipTodayLogicOnShippingModal(string config) => Validate(config);
        }


        //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
        [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
        public class T7511_Emulator_VerifyTheShipsTodayLogicIsCorrect : T7511_MobileBase
        {
            public T7511_Emulator_VerifyTheShipsTodayLogicIsCorrect(ITestOutputHelper output) : base(output) { }

            [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
            [SkippableTheory]
            [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
            public void ShipTodayLogicOnShippingModal(string config) => Validate(config);
        }

        /// <summary>
        /// Verify that the user can add a valid promo code to the cart.
        /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8581
        /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7511
        /// </summary>
        [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
        [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8581"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7511")]
        public abstract class T7511_MobileBase : TestsBaseMobile
        {
            protected T7511_MobileBase(ITestOutputHelper output) : base(output) { }

            protected void Validate(string config)
            {
                /*Arrangement
                User is on the Product Detail page of a single SKU (not combo SKU)
                */
                InitializeFunctionalTest(config);
                var shortSku = ProductActions.GetSingleSkuBathroomLighting;
                Browser.Navigate(Urls.LampsPlusProductsUrl + shortSku);
                Assert.True(ProductDetail.IsCurrentPage, "Current page is not Pdp page");

                /*Act
                Add the item to the cart.
                */
                ProductDetail.AddToCart();
                Assert.True(Cart.IsCurrentPage, "Current page is not Cart page");

                /*Act
                Open Shipping options modal.
                */
                Cart.OpenShippingOptions();

                /*Act
                Apply Zip code.
                */
                Cart.ApplyZipCode("91311");

                /*Assert
                Under the shipping level there is verbiage that matches, or closely relates to, the shipping time-frame message on the PDP .
                */
                Cart.VerifyShippingVerbiage();
                Cart.VerifySecondDayShippingVerbiage();
            }
        }
    }
}