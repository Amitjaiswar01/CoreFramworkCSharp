using Xunit;
using Xunit.Abstractions;
using System.Collections.Generic;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Databases.Entities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.AddingToCartAndWishList.T352_T7576_VerifyAddingItemsWithBuildFullSystemToCart
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.AddingToCartAndWishList)]
    //[Collection(LpTraits.BatchGroup.Mobile.AddingToCartAndWishList)]
    public class T7576_iPhone_VerifyAddingBuildFullSysToCart : T7576_MobileBase
    {
        public T7576_iPhone_VerifyAddingBuildFullSysToCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void AddingBuildFullSysToCart(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    public class T7576_Emulator_VerifyAddingBuildFullSysToCart : T7576_MobileBase
    {
        public T7576_Emulator_VerifyAddingBuildFullSysToCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void AddingBuildFullSysToCart(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the user can add components from the 'Build Full System' tab on the PDP.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8785
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7576
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8785"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7576")]
    public abstract class T7576_MobileBase : TestsBaseMobile
    {
        protected T7576_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            /*Arrange
            Identify an item that includes a 'Build Full System' section.
            Navigate to PDP page for the found item.
            Once the PDP page loads, scroll down the navigate to 'Build Full System' section.
            */
            InitializeFunctionalTest(config);
            var productWithBuildFullSystemSkus = ProductActions.GetProductWithBuildFullSystemSkus();
            Assert.DatabaseObject(productWithBuildFullSystemSkus, "ProductActions.GetProductWithBuildFullSystemSkus()");
            ProductDetail.NavigateToProductDetailByShortSku(productWithBuildFullSystemSkus.PrimarySku);
            ProductDetailDimmers.NavigateToBuildFullSystemSection();
            Assert.True(ProductDetailDimmers.IsBuildFullSystemDisplayed(), "Full System Tab is not Displayed");

            //Assert: The products listed in the section after the first SKU should match the item SKUs in the 'BuildFullSystemSKUS' column from the query.
            Assert.Equals("Build Full System", ProductDetailDimmers.GetBuildFullSystemSectionTitle, "Build Full system text title do not match.");
            Assert.Equals(productWithBuildFullSystemSkus.PrimarySku, ProductDetailDimmers.GetBuildFullSystemTableFirstSku,
                "The very first SKU in the table do not match the value in the PrimarySKU column from the database query.");

            //Assert: The very first SKU in the list will match the SKU of the PDP and the value in the 'PrimarySKU' column.
            var byoDimmerItemOptionsTable = ProductDetailDimmers.GetListOfFullSystemSkus;
            Assert.True(byoDimmerItemOptionsTable[0] == productWithBuildFullSystemSkus.PrimarySku, "First item in table isn't the primary sku from the database.");
            VerifyByoItemOptionsAppearOnPdp(productWithBuildFullSystemSkus.BuildFullSystemProducts, byoDimmerItemOptionsTable);

            /*Act
            1. Enter in a quantity for several of the related items.
            2.Click the red 'Add to Cart' button.
            */
            var addedProducts = ProductDetail.AddAllBuildFullSystemSkusToCart();

            //Assert: The user is re-directed to the cart. 
            Assert.True(Cart.IsCurrentPage, "Current page is not Cart page");

            //Assert: The items that were selected in the 'Build Full System' tab as well as the original items on the PDP appear in the cart with the correct quantity.​​​​
            Assert.True(Cart.DoesCartMatchAddedProducts(addedProducts), "Shopping cart doesn't match the products that were added.");
        }

        private void VerifyByoItemOptionsAppearOnPdp(List<BuildFullSystemProductModel> dbDimmerOptions, List<string> byoDimmerItemOptionsTable)
        {
            foreach (var option in dbDimmerOptions)
            {
                Assert.True(byoDimmerItemOptionsTable.Contains(option.BuildFullSystemSku), "byoDimmerItemOptionsTable do not match.");
            }
        }
    }
}
