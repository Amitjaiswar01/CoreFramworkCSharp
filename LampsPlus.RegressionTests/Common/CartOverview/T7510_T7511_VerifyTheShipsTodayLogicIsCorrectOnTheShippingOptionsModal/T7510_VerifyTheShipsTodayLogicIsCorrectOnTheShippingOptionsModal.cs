using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.CartOverview.T7510_T7511_VerifyTheShipsTodayLogicIsCorrectOnTheShippingOptionsModal
{

    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T7510_Windows_VerifyTheShipsTodayLogicIsCorrect : T7510_DesktopBase
    {
        public T7510_Windows_VerifyTheShipsTodayLogicIsCorrect(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void ShipTodayLogicOnShippingModal(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T7510_Mac_VerifyTheShipsTodayLogicIsCorrect : T7510_DesktopBase
    {
        public T7510_Mac_VerifyTheShipsTodayLogicIsCorrect(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void ShipTodayLogicOnShippingModal(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T7510_iPad_VerifyTheShipsTodayLogicIsCorrect : T7510_DesktopBase
    {
        public T7510_iPad_VerifyTheShipsTodayLogicIsCorrect(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void ShipTodayLogicOnShippingModal(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T7510_TabletEmulator_VerifyTheShipsTodayLogicIsCorrect : T7510_DesktopBase
    {
        public T7510_TabletEmulator_VerifyTheShipsTodayLogicIsCorrect(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void ShipTodayLogicOnShippingModal(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the user can add a valid promo code to the cart.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8581
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7510
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8581"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7510")]
    public abstract class T7510_DesktopBase : TestsBaseDesktop
    {
        protected T7510_DesktopBase(ITestOutputHelper output) : base(output) { }

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