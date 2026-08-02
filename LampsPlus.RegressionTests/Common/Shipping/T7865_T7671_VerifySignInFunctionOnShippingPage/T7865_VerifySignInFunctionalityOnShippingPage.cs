using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Shipping.T7865_T7671_VerifySignInFunctionOnShippingPage
{
    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T7865_Windows_VerifySignInFunctionOnShippingPage : T7865_DesktopBase
    {
        public T7865_Windows_VerifySignInFunctionOnShippingPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void SignInFunctionOnShippingPage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T7865_Mac_VerifySignInFunctionOnShippingPage : T7865_DesktopBase
    {
        public T7865_Mac_VerifySignInFunctionOnShippingPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void SignInFunctionOnShippingPage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T7865_iPad_VerifySignInFunctionOnShippingPage : T7865_DesktopBase
    {
        public T7865_iPad_VerifySignInFunctionOnShippingPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void SignInFunctionOnShippingPage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T7865_TabletEmulator_VerifySignInFunctionOnShippingPage : T7865_DesktopBase
    {
        public T7865_TabletEmulator_VerifySignInFunctionOnShippingPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void SignInFunctionOnShippingPage(string config) => Validate(config);
    }


    /// <summary>
    /// Verify User Can Sign In From Shipping Page
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10124
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7865
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10124"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7865")]
    public abstract class T7865_DesktopBase : TestsBaseDesktop
    {
        protected T7865_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            /* Arrange
            User has added item to cart
            User is on shipping page */
            InitializeFunctionalTest(config);
            var shortSku = ProductActions.GetSingleSkuBathroomLighting;
            Assert.DatabaseObject(shortSku, "ProductActions.GetSingleSkuBathroomLighting");

            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel { Sku = shortSku });
            var productName = Cart.GetProductNameOnCart();

            ShoppingCartWorkflow.ProceedToShippingPage();

            // Act : Sign in from header
            Assert.True(Shipping.IsCurrentPage, "Current page is not Pdp page");
            SignIn.SignInFromShippingHeader(LampsPlusAccounts.CustomerLoginAccount);
            Assert.True(Shipping.IsCurrentPage, "Current page is not Pdp page");

            // Assert : My Account link is visible or not
            Assert.True(SignIn.IsMyAccountLinkVisible(), "My Account link is not visible");

            // Assert : Sku value is same before and after the sign in 
            Assert.Equals(productName, Shipping.GetShortSkuOnShipping(),"Product Name is not same after sign in");
        }
    }
}
