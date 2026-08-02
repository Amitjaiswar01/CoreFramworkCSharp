using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using xRetry;

namespace LampsPlus.RegressionTests.Common.Shipping.T7865_T7671_VerifySignInFunctionOnShippingPage
{
    public class T7671_VerifySignInFunctionalityOnShippingPage
    {
        [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Shipping)]
        public class T7671_iPhone_VerifySignInFunctionalityOnShippingPage : T7671_MobileBase
        {
            public T7671_iPhone_VerifySignInFunctionalityOnShippingPage(ITestOutputHelper output) : base(output) { }
            
            [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
            [RetryTheory(3)]
            [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
            public void UserCanSignInFromShippingPage(string config) => Validate(config);
        }


        [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
        public class T7671_Emulator_VerifySignInFunctionalityOnShippingPage : T7671_MobileBase
        {
            public T7671_Emulator_VerifySignInFunctionalityOnShippingPage(ITestOutputHelper output) : base(output) { }

            [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
            [SkippableTheory]
            [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
            public void UserCanSignInFromShippingPage(string config) => Validate(config);
        }

        /// <summary>
        /// Verify User Can Sign In From Shipping Page
        /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10124
        /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7671
        /// </summary>
        [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
        [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5126"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7671")]
        public abstract class T7671_MobileBase : TestsBaseMobile
        {
            protected T7671_MobileBase(ITestOutputHelper output) : base(output) { }

            protected void Validate(string config)
            {
                /*Arrangement
                User has added item to cart
                User is on shipping page */
                InitializeFunctionalTest(config);
                var shortSku = ProductActions.GetSingleSkuBathroomLighting;
                Assert.DatabaseObject(shortSku, "ProductActions.GetSingleSkuBathroomLighting");
                ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel { Sku = shortSku });
                Cart.CheckOut();

                // Act : Sign in from header
                Shipping.WaitForShippingPageToLoad();
                SignIn.SignInFromHeader(LampsPlusAccounts.CustomerLoginAccount);
                Shipping.WaitForShippingPageToLoad();
                Assert.True(Shipping.IsCurrentPage, "User is not on the shipping Page");

                // Assert : Check whether only Sign Out option is visible
                Assert.True(SignIn.CheckSignOutIcon(), "Sign Out Option is Not Visible");
            }
        }
    }
}