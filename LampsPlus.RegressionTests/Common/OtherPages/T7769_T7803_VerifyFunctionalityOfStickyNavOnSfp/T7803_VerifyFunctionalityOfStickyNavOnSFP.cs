using Xunit;
using Xunit.Abstractions;
using xRetry;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.OtherPages.T7769_T7803_VerifyFunctionalityOfStickyNavOnSFP
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OtherPages)]
    public class T7803_iPhone_VerifyFunctionalityOfStickyNavOnSfp : T7803_MobileBase
    {
        public T7803_iPhone_VerifyFunctionalityOfStickyNavOnSfp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifyFunctionalityOfStickyNavOnSfp(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OtherPages)]
    public class T7803_Emulator_VerifyStickyNavFunctionalityOnSfp : T7803_MobileBase
    {
        public T7803_Emulator_VerifyStickyNavFunctionalityOnSfp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyFunctionalityOfStickyNavOnSfp(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the Functionality of the Sticky Nav on the SFP Page
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10098
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7803
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10098"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-7803")]
    public abstract class T7803_MobileBase : TestsBaseMobile
    {
        protected T7803_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange : Identify a Sku and navigate to its Sfp page
            InitializeFunctionalTest(config);

            var sku = ProductActions.GetAnySkuWithProductDetailPage;
            Assert.DatabaseObject(sku, "ProductActions.GetAnySkuWithProductDetailPage()");

            ProductDetail.NavigateToPlaPageByShortSku(sku);

            //Act : scroll down the page until the Sticky Nav appears
            Browser.ScrollToBottomOfPage(Browser.PageUrl);

            //Assert : Verify the contents of the Sticky Nav.
            Assert.Displayed(ProductDetail.GetStickyNavContents()[0], "Sticky Nav Image Not Found on Sfp.");
            Assert.Displayed(ProductDetail.GetStickyNavContents()[1], "Sticky Nav Price Not Found on Sfp.");
            Assert.Displayed(ProductDetail.GetStickyNavContents()[2], "Sticky Nav Add To Cart Button Not Found on Sfp.");

            //Act : Click on the 'Add to Cart' button on the Sticky Nav.
            ProductDetail.StickyNavAddToCart();
            Assert.True(Cart.IsCurrentPage, "Current page is not Cart page");

            //Assert : The item is added to the cart.
            var cartSku = Cart.GetListOfCartSkus(Browser.PageUrl, 1)[0];
            Assert.Equals(cartSku, sku, "Product in Cart does not match Product on Sfp sticky nav");
        }
    }
}