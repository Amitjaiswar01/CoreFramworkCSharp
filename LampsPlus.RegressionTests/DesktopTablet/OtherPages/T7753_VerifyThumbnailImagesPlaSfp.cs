using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.DesktopTablet.ProductDetail;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.DesktopTablet.OtherPages
{
    //[Collection(LpTraits.BatchGroup.Desktop.OtherPages)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.OtherPages)]
    public class T7753_Windows_VerifyThumbnailImagePlaSfp : T7753_DesktopBase
    {
        public T7753_Windows_VerifyThumbnailImagePlaSfp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyFunctionalityOfThumbnails(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.OtherPages)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.OtherPages)]
    public class T7753_Mac_VerifyThumbnailImagePlaSfp : T7753_DesktopBase
    {
        public T7753_Mac_VerifyThumbnailImagePlaSfp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyFunctionalityOfThumbnails(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.OtherPages)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.OtherPages)]
    public class T7753_iPad_VerifyThumbnailImagePlaSfp : T7753_DesktopBase
    {
        public T7753_iPad_VerifyThumbnailImagePlaSfp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyFunctionalityOfThumbnails(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.OtherPages)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.OtherPages)]
    public class T7753_TabletEmulator_VerifyThumbnailImagePlaSfp : T7753_DesktopBase
    {
        public T7753_TabletEmulator_VerifyThumbnailImagePlaSfp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyFunctionalityOfThumbnails(string config) => Validate(config);
    }

    
    /// <summary>
    /// Verify the functionality of the thumbnail images on the PLA and SFP pages.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9040
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7753
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9040"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7753")]
    public abstract class T7753_DesktopBase : T7753_Base
    {
        protected T7753_DesktopBase(ITestOutputHelper output) : base(output) { }
    }   


    public abstract class T7753_Base : ProductDetailTestsBase
    {
        protected T7753_Base(ITestOutputHelper output) : base(output) { }

        protected virtual void Validate(string config)
        {
            InitializeFramework(config);

            var shortSku = ProductActions.GetOpenBoxShortSku;

            Assert.DatabaseObject(shortSku, "ProductActions.GetOpenBoxShortSku");

            Browser.Navigate(Urls.ProductFullPageBaseUrl + shortSku);
            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

            ImageCheck();

            GlobalLocators.LpModalCloseElement.Click();

            var url = SortActions.GetSortWithNoActiveAbTest();

            Browser.Navigate($"https://{url[0]["Url"]}?sfp={shortSku}");

            ImageCheck();
        }

        protected void ImageCheck()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(ProductDetail.ProductImageThumbnailId.ToCssIdSelector()));

            var totalCount = ProductDetail.MoreThumbnailImage;
            var index = MathHelper.GetRandomNumber(totalCount.Count);
            var selectedThumb = ProductDetail.MoreImages(index + 1);
            Browser.MouseOverOnElement(selectedThumb);

            var pdpThumbnailImage = ProductDetail.ProductThumbnailImagePath;
            var mainImage = ProductDetail.MainProductImage.GetAttribute(GlobalLocators.DataImgPathString);

            Assert.Equals(pdpThumbnailImage, mainImage, "The thumbnail image is not the same as the main product image.");

            ProductDetail.MoreImages(index + 1).Click();

            Browser.Wait.ForDomReady();
            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.LpModalId.ToCssIdSelector()));
            Browser.Wait.ForCondition(() => GlobalLocators.LpModalBackdrop.GetAttribute(GlobalLocators.StyleString).Contains("opacity: 1;"));

            var modalThumbnailImage = ProductDetail.ModalThumbnailImagePath;
            var modalMainImage = ProductDetail.ModalMainImagePath;

            ProductDetail.ModalProductImageThumbnail.Click();

            Browser.Wait.ForElementToStopAnimating(ProductDetail.ImageContainer);

            var modalDifferentThumbnailImage = ProductDetail.ModalThumbnailImagePath;
            var modalDifferentMainImage = ProductDetail.ModalMainImagePath;

            Browser.Wait.IsInvisibleElement(By.CssSelector(GlobalLocators.HiddenClass.ToCssClassSelector()));

            Assert.Equals(pdpThumbnailImage, modalThumbnailImage, "Image does not match");
            Assert.Equals(modalThumbnailImage, modalMainImage, "Image does not match");
            Assert.Equals(modalDifferentThumbnailImage, modalDifferentMainImage, "Image does not match");
        }
    }
}
