using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.IntegrationTests.Pages.AugmentedReality
{
    public class AugmentedRealityLocatorDesktopTest : AugmentedRealityLocatorTests
    {
        public AugmentedRealityLocatorDesktopTest(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Integration.PageObjectModel, "AugmentedReality")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LocateAugmentedRealityElementsTest(string config) => Locate(config);

        protected override void VerifyElementOnRoomTest()
        {
            VerifyElementDisplayed(() => AugmentedReality.AugmentedRealityAddToWishlistButton);
            VerifyElementNotImplemented(() => AugmentedReality.Wishlist);

            var shortSku = ProductActions.GetSkuWithViewInRoomOnPdp;

            ProductDetail.NavigateToProductDetailByShortSku(shortSku);

            Browser.Wait.ForClickableElement(ProductDetail.ShowInRoomBtn);

            ProductDetail.ShowInRoomBtn.Click();

            Browser.Wait.WaitForIframeAndSwitchToIt(GlobalLocators.ModalIframeId);

            Browser.Wait.ForDisplayedElement(ProductDetail.SampleRoomBtn);

            ProductDetail.SampleRoomBtn.Click();


            Browser.SwitchToDefaultContent();
            Browser.Wait.WaitForIframeAndSwitchToIt(GlobalLocators.ModalIframeId);

            Browser.Wait.ForDisplayedElement(ProductDetail.SamplePhotosTab).Click();

            Browser.Wait.ForElements(ProductDetail.SamplePhotos);
            Browser.Wait.ForDisplayedElement(ProductDetail.SamplePhotos[0]).Click();

            Browser.Wait.ForPage(Urls.AugmentedReality);

            Browser.Wait.ForDomReady(2);

            Browser.Navigate(Urls.RoomsPageUrl);

            Browser.Wait.ForDomReady(1500);

            Browser.Wait.ForDisplayedElement(AugmentedReality.ActiveRoom);

            VerifyElementDisplayed(() => AugmentedReality.ActiveRoom);
            VerifyElementNotImplemented(() => AugmentedReality.MoreButton);
            VerifyElementNotImplemented(() => AugmentedReality.ProceedButton);

            ProductDetail.NavigateToProductDetailByShortSku(shortSku);

            Browser.Wait.ForClickableElement(ProductDetail.ShowInRoomBtn);

            ProductDetail.ShowInRoomBtn.Click();

            Browser.Wait.WaitForIframeAndSwitchToIt(GlobalLocators.ModalIframeId);

            Browser.Wait.ForDomReady(2500);

            VerifyElementDisplayed(() => AugmentedReality.AddToCurrentRoom);

            Browser.ClearAllCookies();

            ProductDetail.NavigateToProductDetailByShortSku(shortSku);

            Browser.Wait.ForClickableElement(ProductDetail.ShowInRoomBtn);

            ProductDetail.ShowInRoomBtn.Click();

            Browser.Wait.WaitForIframeAndSwitchToIt(GlobalLocators.ModalIframeId);

            Browser.Wait.ForDisplayedElement(ProductDetail.SampleRoomBtn);            

            Browser.Locate.ElementBySelector(GlobalLocators.InputTypeFileAttribute.ToInputTypeCssSelector()).SendKeys(FileUpload.TurnToReviewPhotoUploadPath);

            Browser.SwitchToDefaultContent();

            Browser.Wait.WaitForIframeAndSwitchToIt(GlobalLocators.ModalIframeId);

            VerifyElementDisplayed(() => AugmentedReality.ProceedButton);

            AugmentedReality.ProceedButton.Click();

            Browser.Wait.ForDisplayedElement(AugmentedReality.ProgressBar);

            VerifyElementDisplayed(() => AugmentedReality.ProgressBar);
        }
    }


    public class AugmentedRealityLocatorMobileTest : AugmentedRealityLocatorTests
    {
        public AugmentedRealityLocatorMobileTest(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Integration.PageObjectModel, "AugmentedReality")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LocateAugmentedRealityElementsTest(string config) => Locate(config);

        protected override void VerifyElementOnRoomTest()
        {
            VerifyElementDisplayed(() => AugmentedReality.MoreButton);

            Browser.Wait.ForClickableElement(AugmentedReality.MoreButton);

            AugmentedReality.MoreButton.Click();

            Browser.Wait.ForDomReady(15);
            Browser.Wait.ForDisplayedElement(AugmentedReality.Wishlist);

            VerifyElementDisplayed(() => AugmentedReality.Wishlist);

            VerifyElementNotImplemented(() => AugmentedReality.AugmentedRealityAddToWishlistButton);

            var shortSku = ProductActions.GetSkuWithViewInRoomOnPdp;

            ProductDetail.NavigateToProductDetailByShortSku(shortSku);

            Browser.Wait.ForDomReady(15);

            ProductDetail.ClickViewInYourRoomJs();

            Browser.Wait.ForDomReady(15);

            Browser.SwitchFocusToIframe(ProductDetail.GetYourPhotoFrame);

            Browser.Wait.ForCondition(() => false, -13, true);

            Browser.Locate.ElementBySelector(GlobalLocators.InputTypeFileAttribute.ToInputTypeCssSelector()).SendKeys(FileUpload.TurnToReviewPhotoUploadPath);

            Browser.Wait.ForCondition(() => false, -5, true);

            VerifyElementDisplayed(() => AugmentedReality.ProceedButton);

            AugmentedReality.ProceedButton.Click();

            Browser.Wait.ForDisplayedElement(AugmentedReality.ProgressBar);

            VerifyElementDisplayed(() => AugmentedReality.ProgressBar);

            Browser.Wait.ForCondition(() => false, -5, true);

            Browser.Navigate(Urls.RoomsPageUrl);

            Browser.Wait.ForDomReady(15);
            Browser.Wait.ForDisplayedElement(AugmentedReality.ActiveRoom);

            VerifyElementDisplayed(() => AugmentedReality.ActiveRoom);

            ProductDetail.NavigateToProductDetailByShortSku(shortSku);

            Browser.Wait.ForDomReady(2);

            ProductDetail.ClickViewInYourRoomJs();

            Browser.Wait.ForDomReady(3);

            Browser.SwitchFocusToIframe(ProductDetail.GetYourPhotoFrame);

            VerifyElementDisplayed(() => AugmentedReality.AddToCurrentRoom);
        }
    }


    /// <summary>
    /// Tests to ensure all IElements and Lists of IElements can be found on the Augmented Reality page.
    /// </summary>
    [Trait(LpTraits.Integration.PageObjectModel, "AugmentedReality")]
    public abstract class AugmentedRealityLocatorTests : PageObjectTestsBase
    {
        protected AugmentedRealityLocatorTests(ITestOutputHelper output) : base(output) { }

        public void Locate(string config)
        {
            InitializeFramework(config, Urls.AugmentedRealityUrl);
            BuildElementsList(AugmentedReality);

            VerifyElementDisplayed(() => AugmentedReality.AugmentedRealityAddToCartButton);

            VerifyElementOnRoomTest();
        }

        protected abstract void VerifyElementOnRoomTest();
    }
}
