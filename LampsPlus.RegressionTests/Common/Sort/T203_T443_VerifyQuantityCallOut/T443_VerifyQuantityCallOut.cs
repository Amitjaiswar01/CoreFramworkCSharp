using System;
using Xunit;
using Xunit.Abstractions;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Sort.T203_T443_VerifyQuantityCallOut
{
    //[Collection(LpTraits.BatchGroup.Mobile.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Sort)]
    public class T443_iPhone_VerifyQuantityCallOut : T443_MobileBase
    {
        public T443_iPhone_VerifyQuantityCallOut(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_ElasticSearch)]
        public void QuantityCallOut(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T443_Emulator_VerifyQuantityCallOut : T443_MobileBase
    {
        public T443_Emulator_VerifyQuantityCallOut(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void QuantityCallOut(string config) => Validate(config);
    }

    /// <summary>
    /// Verify that the Sort page QTY callout matches the PDP page's QTY dropdown.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10078
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T443
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10078"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T443")]
    public abstract class T443_MobileBase : TestsBaseMobile
    {
        protected T443_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            InitializeFunctionalTest(config);

            var productWithQuantityCallOut = ProductActions.GetProductWithCurrentInventory();

            Assert.DatabaseObject(productWithQuantityCallOut, "ProductActions.GetProductWithQuantityCallOut()");

            ProductDetail.NavigateToProductDetailByShortSku(productWithQuantityCallOut.ShortSku);

            var pdpQuantity = ProductDetail.GetProductCallOutQuantity();
            var price = Convert.ToDecimal(TextActions.GetPriceTextOnly(ProductDetail.GetProductPriceOnPdp()));

            ProductDetail.ClickOnLastBreadcrumb();
            Assert.True(Sort.IsCurrentPage, "User is not on the Sort page.");
            Sort.NavigateToPriceFilteredSortPage(Browser.PageUrl, price);

            var sortQuantity = Sort.GetQuantityLeftForSkuOnSort(productWithQuantityCallOut.ShortSku).Replace("LEFT", "").Trim();

            Assert.True(productWithQuantityCallOut.CurrentInventory == pdpQuantity && pdpQuantity == sortQuantity,
                $"The quantities between the db ({productWithQuantityCallOut.CurrentInventory}), pdp ({pdpQuantity}), and sort ({sortQuantity}) do not match.");
        }
    }
}
