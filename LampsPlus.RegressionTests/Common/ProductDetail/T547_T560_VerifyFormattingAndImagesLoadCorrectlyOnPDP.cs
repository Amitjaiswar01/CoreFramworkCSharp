using System;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.DesktopTablet.ProductDetail;
using OpenQA.Selenium;
using xRetry;
using Xunit;
using Xunit.Abstractions;
using OperatingSystem = LampsPlus.AutomationFramework.Utilities.TestConfiguration.OperatingSystem;

namespace LampsPlus.RegressionTests.Common.ProductDetail
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T547_Windows_VerifyFormattingAndImagesLoadCorrectlyOnPdp : T547_DesktopBase
	{
		public T547_Windows_VerifyFormattingAndImagesLoadCorrectlyOnPdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void FormattingAndImagesLoadCorrectlyOnPdp(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T547_Windows_Pcsi_VerifyFormattingAndImagesLoadCorrectlyOnPdp : T547_DesktopBase
    {
        public T547_Windows_Pcsi_VerifyFormattingAndImagesLoadCorrectlyOnPdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_PCSI)]
        public void FormattingAndImagesLoadCorrectlyOnPdp(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T547_Mac_VerifyFormattingAndImagesLoadCorrectlyOnPdp : T547_DesktopBase
	{
		public T547_Mac_VerifyFormattingAndImagesLoadCorrectlyOnPdp(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
		[Theory(Skip = "Will be fixed in ACD-10024")]
		[InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
		public void FormattingAndImagesLoadCorrectlyOnPdp(string config) => Validate(config);
	}


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T547_iPad_VerifyFormattingAndImagesLoadCorrectlyOnPdp : T547_DesktopBase
	{
		public T547_iPad_VerifyFormattingAndImagesLoadCorrectlyOnPdp(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
		[SkippableTheory]
		[InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
		public void FormattingAndImagesLoadCorrectlyOnPdp(string config) => Validate(config);
	}


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T547_TabletEmulator_VerifyFormattingAndImagesLoadCorrectlyOnPdp : T547_DesktopBase
    {
        public T547_TabletEmulator_VerifyFormattingAndImagesLoadCorrectlyOnPdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void FormattingAndImagesLoadCorrectlyOnPdp(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T560_iPhone_VerifyFormattingAndImagesLoadCorrectlyOnPdp : T560_MobileBase
	{
		public T560_iPhone_VerifyFormattingAndImagesLoadCorrectlyOnPdp(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void FormattingAndImagesLoadCorrectlyOnPdp(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T560_AndroidPhone_VerifyFormattingAndImagesLoadCorrectlyOnPdp : T560_MobileBase
	{
		public T560_AndroidPhone_VerifyFormattingAndImagesLoadCorrectlyOnPdp(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
		[SkippableTheory]
		[InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
		public void FormattingAndImagesLoadCorrectlyOnPdp(string config) => Validate(config);
	}


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T560_Emulator_VerifyFormattingAndImagesLoadCorrectlyOnPdp : T560_MobileBase
	{
		public T560_Emulator_VerifyFormattingAndImagesLoadCorrectlyOnPdp(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void FormattingAndImagesLoadCorrectlyOnPdp(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T560_iPhone_Pcsi_VerifyFormattingAndImagesLoadCorrectlyOnPdp : T560_MobileBase
	{
		public T560_iPhone_Pcsi_VerifyFormattingAndImagesLoadCorrectlyOnPdp(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [Theory(Skip = "Bug - LP-62620")]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_PCSI)]
        public void FormattingAndImagesLoadCorrectlyOnPdp(string config) => Validate(config);
	}


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T560_AndroidPhone_Pcsi_VerifyFormattingAndImagesLoadCorrectlyOnPdp : T560_MobileBase
	{
		public T560_AndroidPhone_Pcsi_VerifyFormattingAndImagesLoadCorrectlyOnPdp(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
		[SkippableTheory]
		[InlineData(TestConfiguration.Android_Chrome_SNIS_PCSI)]
		public void FormattingAndImagesLoadCorrectlyOnPdp(string config) => Validate(config);
	}


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T560_Emulator_Pcsi_VerifyFormattingAndImagesLoadCorrectlyOnPdp : T560_MobileBase
	{
		public T560_Emulator_Pcsi_VerifyFormattingAndImagesLoadCorrectlyOnPdp(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_PCSI)]
        public void FormattingAndImagesLoadCorrectlyOnPdp(string config) => Validate(config);
	}


	/// <summary>
	/// Verify that the formatting is correct and all images load correctly on the PDP.
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7729
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T547
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7729"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T547")]
	public abstract class T547_DesktopBase : T547_T560_Base
	{
		protected T547_DesktopBase(ITestOutputHelper output) : base(output) { }
    }


	/// <summary>
	/// Verify that the formatting is correct and all images load correctly on the PDP.
	/// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7729
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T560
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7729"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T560")]
	public abstract class T560_MobileBase : T547_T560_Base
	{
		protected T560_MobileBase(ITestOutputHelper output) : base(output) { }

        protected override void Validate(string config)
        {
            InitializeFramework(config);

            var sku = ProductActions.GetAnySkuWithProductDetailPage;

            Assert.DatabaseObject(sku, "ProductActions.GetAnySkuWithProductDetailPage()");

            Browser.NavigateToPdp(sku);

            Browser.Wait.IsVisibleElement(By.Id(GlobalLocators.PdAddToCartId));
            Browser.ScrollIntoView(GlobalLocators.AddToCartButton);
            
            Assert.Displayed(GlobalLocators.AddToCartButton, "Add to Cart Button is not displayed");
            Assert.Displayed(ProductDetail.AddToWishListButton, "Add to Wishlist Button is not Displayed");

            Browser.ClickByJs(GlobalLocators.AddToCartButton);

            Browser.Wait.IsVisibleElement(By.ClassName(CartOverview.CheckOutNowClass));
            Browser.Wait.IsVisibleElement(By.Id(CartOverview.CartSuggestedProductsContainerId));

            var quantityFieldValue = CartOverview.ProductQtyField.GetAttribute("value");

            Assert.Equals("1", quantityFieldValue, "Cart Quantity does not match the selected quantity");
            Assert.Equals(sku, CartOverview.ProductSku(0), "Cart Sku does not match selected sku");

            Browser.NavigateToPdp(sku);

            Browser.Wait.IsVisibleElement(By.Id(GlobalLocators.PdAddToCartId));
            
            Browser.ScrollIntoView(GlobalLocators.AddToCartButton, true);

            var productTitlePdp = ProductDetail.ProductName;
            Browser.ClickOnButtonMultipleTimes(ProductDetail.AddToWishListButton, 5, WishList.IsWishListAddToCartButtonVisible);

            Assert.Equals(sku, WishList.FirstProductSku, "Wishlist Sku does not match selected sku");
            Assert.Equals(Browser.PageUrl, Urls.WishListPageUrl, "Not on the wish list page");
            Assert.Equals("1", WishList.ProductQuantity(0), "Wishlist Quantity does not match the selected quantity");
            Assert.Equals(productTitlePdp, WishList.ProductNameMobile(0).Substring(0, WishList.ProductNameMobile(0).LastIndexOf("Style", StringComparison.Ordinal)).Trim(), "Product Name on Wishlist does not match");
        }
    }


	public abstract class T547_T560_Base : ProductDetailTestsBase 
	{
		protected T547_T560_Base(ITestOutputHelper output) : base(output) { }
		
		/// <summary>
		/// Verify that the formatting is correct and all images load correctly on the PDP.
		/// </summary>
		/// <param name="config"></param>
		protected virtual void Validate(string config)
		{
            InitializeFramework(config);

            var sku = ProductActions.GetAnySkuWithProductDetailPage;

            Assert.DatabaseObject(sku, "ProductActions.GetAnySkuWithProductDetailPage()");

            Browser.NavigateToPdp(sku);

            Browser.Wait.IsVisibleElement(By.Id(GlobalLocators.PdAddToCartId));

            Assert.Displayed(GlobalLocators.AddToCartButton, "Add to Cart Button is not displayed");
            Assert.Displayed(ProductDetail.AddToWishListButton, "Add to Wishlist Button is not Displayed");

            Browser.ScrollIntoView(GlobalLocators.AddToCartButton);
            Browser.ExecuteJs("window.scrollBy(0,-400)");

            GlobalLocators.AddToCartButton.Click();
            Browser.Wait.ForDomReady();
            Browser.Wait.IsVisibleElement(By.CssSelector(CartOverview.CheckOutNowClass.ToCssClassSelector()));
            
            var quantityFieldValue = CartOverview.ProductQtyDropdownField.GetAttribute("value");

            Assert.Equals("1", quantityFieldValue, "Cart Quantity does not match the selected quantity");
            Assert.Equals(sku, CartOverview.ProductSku(0), "Cart Sku does not match selected sku");

            Browser.NavigateToPdp(sku);

            Browser.Wait.IsVisibleElement(By.Id(GlobalLocators.PdAddToCartId));
            Browser.ScrollIntoView(GlobalLocators.AddToCartButton);
            var productNamePdp = ProductDetail.ProductName;

            ProductDetail.AddToWishListButton.Click();

            Browser.Navigate(Urls.WishListPageUrl);

            Browser.RefreshPage();
			Browser.Wait.ForDomReady();
          
            Browser.Wait.IsVisibleElement(By.ClassName(WishList.LinkAddToCartClass));
            
            Assert.Equals(sku, WishList.FirstProductSku, "Wishlist Sku does not match selected sku");
            Assert.Equals(Browser.PageUrl, Urls.WishListPageUrl, "Not on the wish list page");
            Assert.Equals("1", WishList.ProductQuantity(0), "Wishlist Quantity does not match the selected quantity");
            Assert.Equals(productNamePdp, WishList.ProductName(0), "Product Name on Wishlist does not match");
        }
    }
}
