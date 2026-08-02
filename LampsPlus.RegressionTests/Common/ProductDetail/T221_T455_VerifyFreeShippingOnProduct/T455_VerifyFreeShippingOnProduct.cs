using System.Collections.Generic;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.DesktopTablet.ProductDetail;
using OpenQA.Selenium;
using xRetry;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.Common.ProductDetail.T221_T455_VerifyFreeShippingOnProduct
{
    //[Collection(LpTraits.BatchGroup.Mobile.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.ProductDetail)]
    public class T455_iPhone_VerifyFreeShippingOnProduct : T455_MobileBase
    {
        public T455_iPhone_VerifyFreeShippingOnProduct(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void FreeShippingOnProduct(string config) => Validate(config);
    }

    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T455_Emulator_VerifyFreeShippingOnProduct : T455_MobileBase
    {
        public T455_Emulator_VerifyFreeShippingOnProduct(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void FreeShippingOnProduct(string config) => Validate(config);
    }

    /// <summary>
    /// Verify that all items with the 'Free Shipping' attribute persist to the PDP page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5369
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T455
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5369"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T455")]
    public abstract class T455_MobileBase : TestsBaseMobile
    {
        protected T455_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            /*Arrangement
            User is on the following page: https://www.lampsplus.com/products/chandeliers/style_crystal/
            */
            InitializeFunctionalTest(config, Urls.CrystalChandeliersUrl);

            /*Act
            Navigate to Free Shipping Sort sub-page
            */
            var url = Browser.PageUrl;
            Browser.Navigate(url + Sort.FreeShippingUrlFragmentString);
            Assert.True(Sort.IsCurrentPage, "Current page is not Sort page");

            /*Act
            Get Free Shipping products skus
            */
            var numberOfGivenProducts = 3;
            var productLinks = Sort.GetLinksForGivenNumberOfProductsOnSortPage(numberOfGivenProducts);
            var freeShippingProductsSkus = ProductDetail.GetFreeShippingProductsSkus(productLinks);

            /*Assert
            The database returns a result for the given SKU.
            */
            foreach (var sku in freeShippingProductsSkus)
            {
                var freeShippingSkuData = ProductActions.GetFreeShippingSkuData(sku);

                Assert.DatabaseObject(freeShippingSkuData, "ProductActions.GetFreeShippingSkuData(sku)");
                Assert.False(string.IsNullOrWhiteSpace(freeShippingSkuData.ShortSku), "Related Item Sku Should Exist");
            }
        }


    }
}