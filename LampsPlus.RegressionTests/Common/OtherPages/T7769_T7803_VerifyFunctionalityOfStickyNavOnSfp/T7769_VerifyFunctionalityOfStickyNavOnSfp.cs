using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.OtherPages.T7769_T7803_VerifyFunctionalityOfStickyNavOnSFP
{
    //[Collection(LpTraits.BatchGroup.Common.OtherPages)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OtherPages)]
    public class T7769_Windows_VerifyStickyNavOnSfp : T7769_DesktopBase
    {
        public T7769_Windows_VerifyStickyNavOnSfp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void StickyNavOnSfp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OtherPages)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OtherPages)]
    public class T7769_Mac_VerifyStickyNavOnSfp : T7769_DesktopBase
    {
        public T7769_Mac_VerifyStickyNavOnSfp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void StickyNavOnSfp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OtherPages)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OtherPages)]
    public class T7769_iPad_VerifyStickyNavOnSfp : T7769_DesktopBase
    {
        public T7769_iPad_VerifyStickyNavOnSfp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void StickyNavOnSfp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OtherPages)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OtherPages)]
    public class T7769_TabletEmulator_VerifyStickyNavOnSfp : T7769_DesktopBase
    {
        public T7769_TabletEmulator_VerifyStickyNavOnSfp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void StickyNavOnSfp(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the functionality of the Sticky Nav on the SFP
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9158
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7769
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9158"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7769")]
    public abstract class T7769_DesktopBase : TestsBaseDesktop
    {
        protected T7769_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected virtual void Validate(string config)
        {
            /*Arrange:
            User has identified a SKU that has a PDP.
            User has used the SKU to navigate to SFP page.
            */
            InitializeFunctionalTest(config);
            var sku = ProductActions.GetAnySkuWithProductDetailPage;
            Assert.DatabaseObject(sku, "ProductActions.GetAnySkuWithProductDetailPage()");
            ProductDetail.NavigateToPlaPageByShortSku(sku);


            /*Act:
            Once the user is on the SFP page from the pre-conditions, scroll down the page until the Sticky Nav appears.
            */
            var sfpUrl = Browser.PageUrl;
            Browser.ScrollToBottomOfPage(sfpUrl);

            //Assert: Verify the contents of the Sticky Nav.
            Assert.Displayed(ProductDetail.GetStickyNavContents()[0], "Sticky Nav Add To Cart Button Not Found on Sfp.");
            Assert.Displayed(ProductDetail.GetStickyNavContents()[1], "Sticky Nav Title Not Found on Sfp.");
            Assert.Displayed(ProductDetail.GetStickyNavContents()[2], "Sticky Nav Price Not Found on Sfp.");

            //Act: Click on the 'Add to Cart' button on the Sticky Nav.
            ProductDetail.StickyNavAddToCart();
            Assert.True(Cart.IsCurrentPage, "Current page is not Cart page");
            
            //Assert: The item is added to the cart.
            var cartSku = Cart.GetListOfCartSkus(Browser.PageUrl, 1)[0];
            Assert.Equals(cartSku, sku, "Product in Cart does not match Product on Sfp sticky nav");
        }
    }
}