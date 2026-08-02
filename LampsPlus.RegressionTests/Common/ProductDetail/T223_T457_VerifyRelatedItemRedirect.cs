using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.DesktopTablet.ProductDetail;
using OpenQA.Selenium;
using xRetry;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.Common.ProductDetail
{
    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T223_Windows_VerifyRelatedItemRedirect : T223_DesktopBase
    {
        public T223_Windows_VerifyRelatedItemRedirect(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void RelatedItemRedirect(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T223_Mac_VerifyRelatedItemRedirect : T223_DesktopBase
    {
        public T223_Mac_VerifyRelatedItemRedirect(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void RelatedItemRedirect(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T223_iPad_VerifyRelatedItemRedirect : T223_DesktopBase
    {
        public T223_iPad_VerifyRelatedItemRedirect(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void RelatedItemRedirect(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T223_TabletEmulator_VerifyRelatedItemRedirect : T223_DesktopBase
    {
        public T223_TabletEmulator_VerifyRelatedItemRedirect(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void RelatedItemRedirect(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.ProductDetail)]
    public class T457_iPhone_VerifyRelatedItemRedirect : T457_MobileBase
    {
        public T457_iPhone_VerifyRelatedItemRedirect(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void RelatedItemRedirect(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T457_Emulator_VerifyRelatedItemRedirect : T457_MobileBase
    {
        public T457_Emulator_VerifyRelatedItemRedirect(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void RelatedItemRedirect(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that clicking on a Related Item re-directs to that product's PDP.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5166
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T223
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5166"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T223")]
    public abstract class T223_DesktopBase : T223_T457_Base
    {
        protected T223_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected override void ClickRelatedItems() { }

        protected override void ScrollToRelatedItemsSection()
        {
            Browser.ScrollIntoView(ProductDetail.RelatedItemsSection);
        }
    }


    /// <summary>
    /// Verify that clicking on a Related Item re-directs to that product's PDP.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5256
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T457
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5256"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T457")]
    public abstract class T457_MobileBase : T223_T457_Base
    {
        protected T457_MobileBase(ITestOutputHelper output) : base(output) { }

        protected override void ClickRelatedItems()
        {
            Browser.Wait.ForClickableElement(ProductDetail.RelatedItemDropdown).Click();
        }

        protected override void ScrollToRelatedItemsSection()
        {
            Browser.ScrollIntoView(ProductDetail.RelatedItemDropdown);
        }
    }


    public abstract class T223_T457_Base : ProductDetailTestsBase
    {
        protected T223_T457_Base(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            InitializeFramework(config);

            var shortSku = ProductActions.GetShortSkuThatHasRelatedProducts;

            Assert.DatabaseObject(shortSku, "ProductActions.GetShortSkuThatHasRelatedProducts()");
            ProductDetail.NavigateToProductDetailByShortSku(shortSku);
            Browser.Wait.IsVisibleElement(By.Id(GlobalLocators.PdAddToCartId));

            var relatedProductUrl = ProductDetail.RelatedItemUrl;

            Assert.Equals(ProductDetail.GetTitleSku, shortSku, "Sku from database does not match the sku on the web page.");

            Browser.Wait.IsVisibleElement(By.CssSelector(ProductDetail.PdRelItmsContainerId.ToCssIdSelector()));

            Browser.Wait.ForDomReady();
            ClickRelatedItems();
            ScrollToRelatedItemsSection();

            var sku = ProductDetail.RelatedItems[0].GetAttribute("data-certonasku").ToLower();

            ProductDetail.RelatedItems[0].Click();
            Browser.Wait.ForCondition(() => Browser.PageUrl.Contains(sku));

            Assert.Equals(relatedProductUrl, Browser.PageUrl, "The user is not re-directed to the Product page for the item that was clicked on."); //verify the sku on new PDP matches sku clicked in Related Items section
        }

        protected abstract void ClickRelatedItems();

        protected abstract void ScrollToRelatedItemsSection();
    }
}
