using Xunit;
using Xunit.Abstractions;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.AddingToCartAndWishList.T355_T7577_VerifyAddingItemsWithBuildFullSystemToWishList
{
    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    public class T355_Windows_VerifyAddingItemsWithBuildFullSystemToWishList : T355_DesktopBase
    {
        public T355_Windows_VerifyAddingItemsWithBuildFullSystemToWishList(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyAddingItemsWithBuildFullSystemToWishList(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.AddingToCartAndWishList)]
    public class T355_Mac_VerifyAddingItemsWithBuildFullSystemToWishList : T355_DesktopBase
    {
        public T355_Mac_VerifyAddingItemsWithBuildFullSystemToWishList(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyAddingItemsWithBuildFullSystemToWishList(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.AddingToCartAndWishList)]
    public class T355_iPad_VerifyAddingItemsWithBuildFullSystemToWishList : T355_DesktopBase
    {
        public T355_iPad_VerifyAddingItemsWithBuildFullSystemToWishList(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyAddingItemsWithBuildFullSystemToWishList(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.AddingToCartAndWishList)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.AddingToCartAndWishList)]
    public class T355_TabletEmulator_VerifyAddingItemsWithBuildFullSystemToWishList : T355_DesktopBase
    {
        public T355_TabletEmulator_VerifyAddingItemsWithBuildFullSystemToWishList(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyAddingItemsWithBuildFullSystemToWishList(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the user can add components from the 'Build Full System' tab to Wish List
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10105
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T355
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10105"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T355")]
    public abstract class T355_DesktopBase : TestsBaseDesktop
    {
        protected T355_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange : Identify a Build Full System SKU.
            InitializeFunctionalTest(config); 
            var sku = ProductActions.GetProductWithBuildFullSystemSkus();

            //Act : Navigate to PDP
            ProductDetail.NavigateToProductDetailByShortSku(sku.PrimarySku);

            //Act : Scroll down to Build Full System tab, add components to Wish List with Quantity 
            var addedProducts = ProductDetail.AddAllBuildFullSystemSkusToWishList(MathHelper.GetRandomNumber(2, 5));
            Browser.Wait.ForDomReady();
            WishList.Navigate();
            Assert.True(WishList.IsCurrentPage,"User is not on WishList Page" );

            var wishListContent = WishList.GetWishListItemsContent();

            //Assert : Verify Quantity and SKU on PDP matches with that on Wish List 
            Assert.True(WishList.DoesWishListMatchAddedProducts(addedProducts, wishListContent), "Quantity and SKU does not match");
        }
    }
}
