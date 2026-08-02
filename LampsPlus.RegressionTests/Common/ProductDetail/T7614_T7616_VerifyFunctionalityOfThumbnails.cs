using System.Collections.Generic;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.DesktopTablet.ProductDetail;
using OpenQA.Selenium;
using Automation.Framework.Utilities;
using xRetry;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.Common.ProductDetail
{
    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7614_Windows_VerifyFunctionalityOfThumbnails : T7614_DesktopBase
    {
        public T7614_Windows_VerifyFunctionalityOfThumbnails(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyFunctionalityOfThumbnails(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7614_Mac_VerifyFunctionalityOfThumbnails : T7614_DesktopBase
    {
        public T7614_Mac_VerifyFunctionalityOfThumbnails(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyFunctionalityOfThumbnails(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7614_iPad_VerifyFunctionalityOfThumbnails : T7614_DesktopBase
    {
        public T7614_iPad_VerifyFunctionalityOfThumbnails(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyFunctionalityOfThumbnails(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7614_TabletEmulator_VerifyFunctionalityOfThumbnails : T7614_DesktopBase
    {
        public T7614_TabletEmulator_VerifyFunctionalityOfThumbnails(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyFunctionalityOfThumbnails(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.ProductDetail)]
    public class T7616_iPhone_VerifyFunctionalityOfThumbnails : T7616_MobileBase
    {
        public T7616_iPhone_VerifyFunctionalityOfThumbnails(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifyFunctionalityOfThumbnails(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.ProductDetail)]
    public class T7616_Emulator_VerifyFunctionalityOfThumbnails : T7616_MobileBase
    {
        public T7616_Emulator_VerifyFunctionalityOfThumbnails(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyFunctionalityOfThumbnails(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the functionality of the thumbnails below the main product image.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8818
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7614
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8818"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7614")]
    public abstract class T7614_DesktopBase : T7614_T7616_Base
    {
        protected T7614_DesktopBase(ITestOutputHelper output) : base(output) { }
    }   


    /// <summary>
    /// Verify the functionality of the thumbnails below the main product image.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8818
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7616
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8818"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7616")]
    public abstract class T7616_MobileBase : T7614_T7616_Base
    {
        protected T7616_MobileBase(ITestOutputHelper output) : base(output) { }

        protected override void Validate(string config)
        {
            InitializeFramework(config);

            var CustomerPhotos = new List<string> { "m7013", "8g405", "v8455" };
            var shortSku = ProductDetail.GetRandomSku(CustomerPhotos);

            ProductDetail.NavigateToProductDetailByShortSku(shortSku);

            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

            var totalCount = ProductDetail.ThumbnailImageCarousel;
            
            var index = ProductDetail.ShowInRoomBtn == null ? MathHelper.GetRandomNumber(totalCount.Count) : MathHelper.GetRandomNumber(totalCount.Count - 1);
            var indexLatest = index == 0 ? index + 1 : index; 
            
            ProductDetail.CarouselImage(indexLatest).Click();

            var thumbnailImage = ProductDetail.CarouselImage(indexLatest).FindElement(By.TagName("img")).GetAttribute(GlobalLocators.DataImgPathString);
            var mainImage = ProductDetail.MainImagePath(indexLatest).FindElement(By.TagName("img")).GetAttribute(GlobalLocators.DataImgPathString);

            ProductDetail.ZoomIcon.Click();

            Browser.Wait.ForDisplayedElement(ProductDetail.CustomerPhotos);

            var modalThumbnailImage = ProductDetail.ModalThumbnailImageSrc.Split('?')[0];
            var modalMainImage = ProductDetail.ModalMainImageSrc.Split('?')[0];

            var totalthumbnailCount = ProductDetail.MoreThumbnailImage.Count;
            var index2 = MathHelper.GetRandomNumber(totalthumbnailCount);
            ProductDetail.MoreImages(index2 + 1).Click();

            var modalDifferentThumbnailImage = ProductDetail.ModalThumbnailImageSrc.Split('?')[0];
            var modalDifferentMainImage = ProductDetail.ModalMainImageSrc.Split('?')[0];

            ProductDetail.CustomerPhotos.Click();

            var modalCustomerImage = ProductDetail.ModalCustomerPhotosSrc.Substring(0, 44);
            var modalCustomerThumbnailImage = ProductDetail.ModalCustomerPhotosThumbnailSrc.Substring(0, 44);

            Assert.Equals(thumbnailImage, mainImage, "Image does not match");
            Assert.Equals(modalThumbnailImage, modalMainImage, "Image does not match");
            Assert.Equals(modalDifferentThumbnailImage, modalDifferentMainImage, "Image does not match");
            Assert.Equals(modalCustomerImage, modalCustomerThumbnailImage, "Customer Photo does not match");
        }
    }


    public abstract class T7614_T7616_Base : ProductDetailTestsBase
    {
        protected T7614_T7616_Base(ITestOutputHelper output) : base(output) { }

        protected virtual void Validate(string config)
        {
            InitializeFramework(config);

            var CustomerPhotos = new List<string> { "m7013", "8g405" };
            var shortSku = ProductDetail.GetRandomSku(CustomerPhotos);

            ProductDetail.NavigateToProductDetailByShortSku(shortSku);

            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

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

            var modalDifferentThumbnailImage = ProductDetail.ModalDiffrentThumbnailImagePath;
            var modalDifferentMainImage = ProductDetail.ModalMainImagePath;

            Browser.Wait.IsInvisibleElement(By.CssSelector(GlobalLocators.HiddenClass.ToCssClassSelector()));
            
            Browser.Wait.ForDisplayedElement(ProductDetail.CustomerPhotos);
            ProductDetail.CustomerPhotos.Click();
            Browser.Wait.ForDomReady();

            var customerThumbnailImage = ProductDetail.CustomerThumbnailImagePath;
            var customerMainImage = ProductDetail.CustomerMainImagePath;

            Assert.Equals(pdpThumbnailImage, modalThumbnailImage, "Image does not match");
            Assert.Equals(modalThumbnailImage, modalMainImage, "Image does not match");
            Assert.Equals(modalDifferentThumbnailImage, modalDifferentMainImage, "Image does not match");
            Assert.Equals(customerThumbnailImage, customerMainImage, "Customer Photo does not match");
        }
    }
}