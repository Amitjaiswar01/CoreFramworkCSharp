using System.Linq;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;
using xRetry;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.Common.Sort;

namespace LampsPlus.RegressionTests.Common.OtherPages
{
    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7056_Windows_VerifySortPagePathPositionIsRecordedInDb : T7056_DesktopBase

    {
        public T7056_Windows_VerifySortPagePathPositionIsRecordedInDb(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void SortPagePathPositionIsCorrect(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7056_Mac_VerifySortPagePathPositionIsRecordedInDb : T7056_DesktopBase
    {
        public T7056_Mac_VerifySortPagePathPositionIsRecordedInDb(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void SortPagePathPositionIsCorrect(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7056_iPad_VerifySortPagePathPositionIsRecordedInDb : T7056_DesktopBase
    {
        public T7056_iPad_VerifySortPagePathPositionIsRecordedInDb(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void SortPagePathPositionIsCorrect(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7056_TabletEmulator_VerifySortPagePathPositionIsRecordedInDb : T7056_DesktopBase
    {
        public T7056_TabletEmulator_VerifySortPagePathPositionIsRecordedInDb(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void SortPagePathPositionIsCorrect(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Sort)]
    public class T7057_iPhone_VerifySortPagePathPositionIsRecordedInDb : T7057_MobileBase
    {
        public T7057_iPhone_VerifySortPagePathPositionIsRecordedInDb(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void SortPagePathPositionIsCorrect(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7057_Emulator_VerifySortPagePathPositionIsRecordedInDb : T7057_MobileBase
    {
        public T7057_Emulator_VerifySortPagePathPositionIsRecordedInDb(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void SortPagePathPositionIsCorrect(string config) => Validate(config);
    }


    /// <summary>
	/// Verify that the sort page path and position is recorded in the DB for items from Full SFP Pages that are placed in cart.
	/// JIRA Task ID: https://lampstrack.lampsplus.com:8443/browse/ACD-6650
	/// Test Case ID: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7056
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-6650"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7056")]
    public abstract class T7056_DesktopBase : T7056_T7057_Base
    {
        protected T7056_DesktopBase(ITestOutputHelper output) : base(output) { }

        public override void ClickOnNthProductFromSort(int position)
        {
            Browser.Wait.ForDomReady();
            Browser.ScrollIntoView(Sort.NthDisplayedProductElementForCertonaWidgetNoDiv(position));
            Sort.NthDisplayedProductElementForCertonaWidgetNoDiv(position).Click();
        }
    }


    /// <summary>
    /// Verify that the sort page path and position is recorded in the DB for items from Full SFP Pages that are placed in cart.
    /// Jira Task ID: https://lampstrack.lampsplus.com:8443/browse/ACD-6651
    /// Test Case ID: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7057
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-6651"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7057")]
    public abstract class T7057_MobileBase : T7056_T7057_Base
    {
        protected T7057_MobileBase(ITestOutputHelper output) : base(output) { }

        public override void ClickOnNthProductFromSort(int position)
        {
            Browser.Wait.IsVisibleElement(By.XPath(SortPla.PlaCertonaImageLoadedXpath));
            Browser.ScrollToBottomOfWindow();
            Browser.ScrollToTopOfWindow();
            Browser.ScrollIntoView(GlobalLocators.AddToCartButton);
            Sort.NthDisplayedProductElementForCertonaWidgetNoDiv(position).Click();
        }
    }

    public abstract class T7056_T7057_Base : SortTestsBase
    {
        protected T7056_T7057_Base(ITestOutputHelper output) : base(output) { }
        
        protected virtual void Validate(string config)
        {
            InitializeFramework(config);

            var sku = ProductActions.GetAnySkuWithProductDetailPage;
            Assert.DatabaseObject(sku, "ProductActions.GetAnySkuWithProductDetailPage()");

            var testParam = "?test=junk";

            Browser.Navigate(Urls.ProductFullPageBaseUrl + sku + "/" + testParam);
            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));
            var sortPath1 = "/sfp/" + sku + "/";

            var position1 = 2;
            ClickOnNthProductFromSort(position1);

            Browser.Wait.ForDomReady();
            Browser.Wait.IsVisibleElement(By.CssSelector(ProductDetail.PdProdSkuId.ToCssIdSelector()));
            var sku1 = ProductDetail.GetTitleSku;

            ClickAddToCartButton();

            Browser.Navigate(Urls.OutdoorLightingSortUrl + testParam + "&sfp=" + sku);
            Browser.Wait.IsVisibleElement(By.Id(GlobalLocators.PdAddToCartId));
            var sortPath2 = "/products/outdoor-lighting/?sfp=" + sku;

            var position2 = 3;
            Sort.NthDisplayedProductElement(position2).Click();

            Browser.Wait.ForDomReady();
            Browser.Wait.IsVisibleElement(By.CssSelector(ProductDetail.PdProdSkuId.ToCssIdSelector()));
            var sku2 = ProductDetail.GetTitleSku;

            ClickAddToCartButton();

            var cartId = CartOverview.CartId;
            var cartItems = SortActions.GetSortPathPositionCartItems(cartId);

            if (cartItems.Any(x => x.ShortSku == sku1))
            {
                Assert.Equals(sortPath1, cartItems.First(x => x.ShortSku == sku1).SortPath, "Sort Path does not match");
                Assert.Equals(position1, cartItems.First(x => x.ShortSku == sku1).SortPosition, "Sort Position does not match");
            }

            Assert.True(cartItems.Any(x => x.ShortSku == sku2), "Sku " + sku2 + " does not match");
            Assert.Equals(sortPath2, cartItems.First(x => x.ShortSku == sku2).SortPath, "Sort Path does not match");
            Assert.Equals(position2, cartItems.First(x => x.ShortSku == sku2).SortPosition, "Sort Position does not match");
        }

        public abstract void ClickOnNthProductFromSort(int position);

        protected void ClickAddToCartButton()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));
            Browser.ScrollIntoView(GlobalLocators.AddToCartButton);
            Browser.Wait.ForDomReady();
            GlobalLocators.AddToCartButton.Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(CartOverview.CheckOutNowClass.ToCssClassSelector()));
        }
    }
}
