using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;
using System.Linq;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.Common.Homepage;

namespace LampsPlus.RegressionTests.DesktopTablet.OtherPages
{
    //[Collection(LpTraits.BatchGroup.Common.Homepage)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Homepage)]
    public class T197_Windows_VerifyPlaFunctionalityOnHomepage : T197_DesktopBase
    {
        public T197_Windows_VerifyPlaFunctionalityOnHomepage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Zephyr: T197. Rework - ACD-10904")]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void PlaFunctionalityOnHomepage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Homepage)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Homepage)]
    public class T197_Mac_VerifyPlaFunctionalityOnHomepage : T197_DesktopBase
    {
        public T197_Mac_VerifyPlaFunctionalityOnHomepage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Zephyr: T197. Rework - ACD-10904")]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void PlaFunctionalityOnHomepage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Homepage)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Homepage)]
    public class T197_iPad_VerifyPlaFunctionalityOnHomepage : T197_DesktopBase
    {
        public T197_iPad_VerifyPlaFunctionalityOnHomepage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void PlaFunctionalityOnHomepage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Homepage)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Homepage)]
    public class T197_TabletEmulator_VerifyPlaFunctionalityOnHomepage : T197_DesktopBase
    {
        public T197_TabletEmulator_VerifyPlaFunctionalityOnHomepage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void PlaFunctionalityOnHomepage(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the functionality of SFP Pages.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5088
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T197
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5088"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T197")]
    public abstract class T197_DesktopBase : T197_Base
    {
        protected T197_DesktopBase(ITestOutputHelper output) : base(output) { }
    }
    

    public abstract class T197_Base : HomepageTestsBase
    {
        protected T197_Base(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            InitializeFramework(config);

            var sku = ProductActions.GetPlaSkuWithStarsQAndA();
            var url = Urls.HomePageUrl;

            Assert.DatabaseObject(sku, "ProductActions.GetSkuForPla()");

            Browser.Navigate($"{url}/sfp/{sku}");

            Browser.Wait.IsVisibleElement(By.CssSelector(SortPla.TurnToTeaserBlockClass.ToCssClassSelector()));

            var isPlaReviewStarsVisible = (Home.PlaReviewStars.IsInitialized && Home.PlaReviewStars.Displayed);
            var isPlaReviewVisible = (Home.PlaReviews.IsInitialized && Home.PlaReviews.Displayed);

            Assert.True(isPlaReviewStarsVisible, "Product rating is not visible");
            Assert.True(isPlaReviewVisible, "Product reviews link is not visible");

            SortPla.SfpTurnToTeaserBlock.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

            Assert.True(Browser.PageUrl.Contains(Urls.ReadReviewsUrlFragment), $"SKU: {Urls.ReadReviewsUrlFragment} does not match a piece of the URL.");
            Assert.True(Browser.PageUrl.Contains(sku.ToLower()), $"SKU: {sku} is not part of the URL.");

            Browser.Wait.ForDomReady();
            Browser.Navigate($"{url}/sfp/{sku}");
            Browser.Wait.ForDomReady();

            SortFullPageCertona.FullPageCertonaSimilarDesignsItems.First().Click();

            Browser.Wait.ForDomReady();
            Assert.True(ProductDetail.IsProductDetailPage, "The user is not navigated to the PDP for the selected product.");

            Browser.Wait.ForDomReady();

            Browser.GoBack();

            Assert.True(IsMatchSkuPlaAndPdp(url, sku), $"SKU: {sku} does not match on Pla and PDP.");
            Assert.True(IsPlaSkuAddedToCart(url, sku), $"PLA SKU: {sku} was not added to the cart.");
        }

        private bool IsPlaSkuAddedToCart(string url, string sku)
        {
            Browser.Navigate($"{url}/sfp/{sku}");
            Browser.Wait.ForClickableElement(GlobalLocators.PlaAddToCartElement).Click();

            Browser.Wait.IsVisibleElement(By.ClassName(CartOverview.CheckOutNowClass));

            if (string.CompareOrdinal(sku, CartOverview.ProductSkuCart) == 0) { return true; }
            return false;

        }

        private bool IsMatchSkuPlaAndPdp(string url, string sku)
        {
            Browser.Navigate($"{url}/sfp/{sku}");
            Browser.Wait.IsVisibleElement(By.XPath(Home.PlaViewDetailsLinkXpath));
            Home.PlaViewDetailsLinkElement.Click();
            Browser.Wait.IsVisibleElement(By.XPath(ProductDetail.SkuOnPdpXpath));
            if (string.CompareOrdinal(sku, ProductDetail.SkuOnPdp) == 0) { return true; }

            return false;
        }
    }
}
