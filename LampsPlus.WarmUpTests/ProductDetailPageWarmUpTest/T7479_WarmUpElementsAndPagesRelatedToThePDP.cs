using Automation.Framework.Exceptions;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Environment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.DesktopTablet.ProductDetail;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.WarmUpTests.ProductDetailPageWarmUpTest
{
    public class T7479_WarmUpElementsAndPagesRelatedToThePdp : T7479_TestBase
    {
        public T7479_WarmUpElementsAndPagesRelatedToThePdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void WarmUpTestForPdp(string config) => Validate(config);
    }


    /// <summary>
    /// Warm up elements and pages related to the PDP page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8404
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7479
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8404"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7479")]
    public abstract class T7479_TestBase : ProductDetailTestsBase
    {
        protected T7479_TestBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            var setup = new TestSetup(config);
            InitializeFramework(config, setup: setup);

            var shortSku = ProductActions.GetSkuWithViewInRoomOnPdp;
            Assert.DatabaseObject(shortSku, ProductActions.GetSkuWithViewInRoomOnPdp);

            ProductDetail.NavigateToProductDetailByShortSku(shortSku);
            Browser.Wait.ForDisplayedElement(ProductDetail.AddToWishListButton);
            ProductDetail.AddToWishListButton.Click();

            Browser.Navigate(Urls.WishListPageUrl);
            ProductDetail.NavigateToProductDetailByShortSku(shortSku);

            //Check Store Availability
            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));
            Browser.Wait.ForClickableElement(ProductDetail.EmailLink).Click();
            Browser.Wait.IsVisibleElement(By.Id(GlobalLocators.LpModalId));
            CloseLpModal();
            Browser.RefreshPage();
            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

            //View in your room
            Browser.ScrollToTopOfWindow();
            Browser.ScrollIntoView(Browser.Locate.ElementByXpath(ProductDetail.ViewInYourRoomXpath), true);
            Browser.Wait.ForDisplayedElement(Browser.Locate.ElementByXpath(ProductDetail.ViewInYourRoomXpath)).Click();
            Browser.Wait.ForDomReady();
            Browser.SwitchFocusToIframe(Browser.Locate.ElementBySelector(GlobalLocators.ModalIframeId.ToCssIdSelector()));

            //Select "Use sample room"
            Browser.Wait.IsVisibleElement(By.CssSelector(ProductDetail.SampleRoomBtnClass.ToCssClassSelector()),50);
            Browser.Locate.ElementBySelector(ProductDetail.SampleRoomBtnClass.ToCssClassSelector()).Click();
            Browser.Wait.ForDomReady();
            Browser.SwitchToDefaultContent();
            Browser.SwitchFocusToIframe(Browser.Locate.ElementBySelector(GlobalLocators.ModalIframeId.ToCssIdSelector()));
            Browser.Wait.ForDomReady();
            Browser.Wait.IsVisibleElement(By.XPath(ProductDetail.ViewInYourRoomSelectPhotoXpath),30);
            Browser.Wait.IsVisibleElement(By.XPath(ProductDetail.ViewInYourRoomSampleImageXpath), 30);
            Browser.Wait.ForDisplayedElement(Browser.Locate.ElementByXpath(ProductDetail.ViewInYourRoomSampleImageXpath)).Click();
            Browser.Wait.ForDomReady();

            //Write a review
            var shortSkuReview = ProductActions.GetSkuThatQualifiesForReviews;
            Assert.DatabaseObject(shortSkuReview, ProductActions.GetSkuThatQualifiesForReviews);
            ProductDetail.NavigateToProductDetailByShortSku(shortSkuReview);
            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));
            ClickWriteReview();
            Browser.Locate.ElementByXpath(ProductDetail.WriteReviewModalXpath);
            Browser.Wait.ForClickableElement(Browser.Locate.ElementBySelector(ProductDetail.WriteReviewModalCloseCssSelector)).Click();
            Browser.Wait.ForDomReady();

            Browser.Navigate(Urls.RoomsPageUrl);
            Browser.Navigate(Urls.RecentlyViewedUrl);
        }


        private void ClickWriteReview()
        {
            var i = 0;
            var pixelsScroll = 200;

            do
            {
                Browser.ScrollToByPixelsVertical(pixelsScroll.ToString());
                if (Browser.Locate.ElementImmediately(ProductDetail.WriteReviewBtnSelector).IsInitialized)
                {
                    Browser.Wait.ForDisplayedElement(ProductDetail.BuildFullSystemTitle);
                    Browser.Wait.ForElement(Browser.Locate.ElementBySelector(ProductDetail.WriteReviewBtnSelector)).Click();
                    break;
                }

                Log.Message($"Scrolling to element, scroll# {i}");
                pixelsScroll += pixelsScroll;
             

                if(i == 9) throw new FrameworkWaitException($"Element {ProductDetail.WriteReviewBtnSelector} is not found");

            } while (i < 10);
        }
    }
}
