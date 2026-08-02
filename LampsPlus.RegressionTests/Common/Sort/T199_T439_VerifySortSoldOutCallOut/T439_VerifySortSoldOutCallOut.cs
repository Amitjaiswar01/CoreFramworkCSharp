using System;
using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using xRetry;
using Skip = Xunit.Skip;

namespace LampsPlus.RegressionTests.Common.Sort.T199_T439_VerifySortSoldOutCallOut
{
    //[Collection(LpTraits.BatchGroup.Mobile.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.RunEnvironment.ProductionOnly)]
    public class T439_iPhone_VerifySortSoldOutCallOut : T439_MobileBase
    {
        public T439_iPhone_VerifySortSoldOutCallOut(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_ElasticSearch)]
        public void SortSoldOutCallOut(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.RunEnvironment.ProductionOnly)]
    public class T439_Emulator_VerifySortSoldOutCallOut : T439_MobileBase
    {
        public T439_Emulator_VerifySortSoldOutCallOut(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void SortSoldOutCallOut(string config) => Validate(config);
    }

    /// <summary>
    /// Verify that the 'Sold Out' callout is displayed for the appropriate items for ALL users.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5236
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T439
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5236"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T439")]
    public abstract class T439_MobileBase : TestsBaseMobile
    {
        protected T439_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            /*Arrange
             User is on the homepage
             User identified a qualifying item.
            */
            InitializeFunctionalTest(config);
            Skip.IfNot(DevEnvInformation.DatabaseSymbol == "P", "This test can only be executed against DBCLUST.");
            var shortSku = ProductActions.GetRandomSoldOutItemSku;
            Assert.True(!string.IsNullOrEmpty(shortSku), "No Sold Out item found in the database at this time.");

            //Act: navigate to the PDP by the SKU
            ProductDetail.NavigateToProductDetailByShortSku(shortSku);

            //Act: store the Product Name and Price
            var price = ProductDetail.GetProductPriceText();

            //Act: Click on the second-to-last breadcrumb (first linkable one).
            ProductDetail.ClickOnLastBreadcrumb();
            Sort.WaitForFilter();

            //Act: Once the Sort page loads, search for the Product Name by price.
            var url = Browser.PageUrl;
            Sort.NavigateToPriceFilteredSortPage(url, Convert.ToDecimal(price));

            //Act: Search for the Product Name 
            Sort.SearchPageForSku(shortSku.ToLower());

            //Assert:There is a callout called 'SOLD OUT' on the product image.  
            Assert.True(Sort.DoesSkuExistOnSortPage(shortSku.ToLower()), $"Sku '{shortSku.ToLower()}' was NOT FOUND on any sort pages");
            Assert.True(Sort.HasSoldOutCallOut(shortSku), $"Sku '{shortSku}' HAS NO Sold Out callout");
        }
    }
}
