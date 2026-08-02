using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Environment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.DesktopTablet.CartOverview.T130_VerifyCartShowsShippingInfoForMarshallIslandForStoreInSession
{
    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.CartOverview)]
    public class T130_Windows_VerifyCartShowsShippingInfoForMarshallIslandForStoreInSession : T130_DesktopBase
    {
        public T130_Windows_VerifyCartShowsShippingInfoForMarshallIslandForStoreInSession(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_ESI)]
        public void VerifyCartShowsShippingInfoForMarshallIslandForStoreInSession(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T130_Mac_VerifyCartShowsShippingInfoForMarshallIslandForStoreInSession : T130_DesktopBase
    {
        public T130_Mac_VerifyCartShowsShippingInfoForMarshallIslandForStoreInSession(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SIS_ESI)]
        public void VerifyCartShowsShippingInfoForMarshallIslandForStoreInSession(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T130_iPad_VerifyCartShowsShippingInfoForMarshallIslandForStoreInSession : T130_DesktopBase
    {
        public T130_iPad_VerifyCartShowsShippingInfoForMarshallIslandForStoreInSession(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SIS_ESI)]
        public void VerifyCartShowsShippingInfoForMarshallIslandForStoreInSession(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T130_TabletEmulator_VerifyCartShowsShippingInfoForMarshallIslandForStoreInSession : T130_DesktopBase
    {
        public T130_TabletEmulator_VerifyCartShowsShippingInfoForMarshallIslandForStoreInSession(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SIS_ESI)]
        public void VerifyCartShowsShippingInfoForMarshallIslandForStoreInSession(string config) => Validate(config);
    }


    /// <summary>
    /// Verify Sale Ends In Callout On CartOverview Page
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9933
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T130
    /// </summary>
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9933"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T130")]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]

    public abstract class T130_DesktopBase : TestsBaseDesktop
    {
        protected T130_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected virtual void Validate(string config)
        {
            // Arrange : Store is in session and there are items in the cart.
            var setup = new TestSetup(config) { AccountConfig = { StoreInSessionStoreNumber = "12" } };
            InitializeFunctionalTest(config, setup: setup);

            const int numberOfProductsToAddToCart = 2;
            ShoppingCartWorkflow.AddMultipleItemsToCart(Urls.TableLampsSortPageUrl, numberOfProductsToAddToCart);
            Assert.True(Cart.IsCurrentPage, "User is not on Cart Page");

            /* Act : Uncheck the POS box for all items in the cart
            Click the 'Standard Shipping' link.
            Add Marshall Island Zip Code
            */
            var productCountInCart = Cart.GetCountOfAllProductsInCart();
            Assert.False(Cart.IsAllPosCheckboxesUnchecked, "POS Checkboxes are Checked");

            Cart.OpenShippingOptions();
            Cart.ApplyZipCode(ZipCodeList.MarshallIslands);

            // Assert : Verify the Shipping Options Error Message
            Assert.True(Cart.GetShippingOptionsErrorText()== Messages.CartMessages.ShippingErrorMessageForMarshallIsland, "Error message not properly displayed when entering Marshall Islands zip code");

            // Act : Check All POS Check Boxes
            Cart.ShippingUpdate();
            Cart.CheckPosBoxForAllCartSkus(productCountInCart);

            //Assert : Verify Shipping Zone Fields is Removed and Check Out Now Button is Disabled
            Assert.True(Cart.AreShippingZoneFieldsRemoved(), "Empty shipping text not shown after changing zip.");
            Assert.True(Cart.IsCheckOutNowButtonDisabled, "Checkout Now button is enabled");
            Assert.True(Cart.IsShippingAndProcessingDisabled,"Shipping And Processing Is Not Disabled");
        }
    }
}