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
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7356_Windows_VerifyStickyNavOnThePdp : T7356_DesktopBase
	{
		public T7356_Windows_VerifyStickyNavOnThePdp(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
		[SkippableTheory]
		[InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
		public void StickyNavOnThePdp(string config) => Validate(config);
	}


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7356_Mac_VerifyStickyNavOnThePdp : T7356_DesktopBase
    {
        public T7356_Mac_VerifyStickyNavOnThePdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void StickyNavOnThePdp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7356_iPad_VerifyStickyNavOnThePdp : T7356_DesktopBase
    {
        public T7356_iPad_VerifyStickyNavOnThePdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void StickyNavOnThePdp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7356_TabletEmulator_VerifyStickyNavOnThePdp : T7356_DesktopBase
    {
        public T7356_TabletEmulator_VerifyStickyNavOnThePdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void StickyNavOnThePdp(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.ProductDetail)]
    public class T7357_iPhone_VerifyStickyNavOnThePdp : T7357_MobileBase
	{
        public T7357_iPhone_VerifyStickyNavOnThePdp(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
		public void StickyNavOnThePdp(string config) => Validate(config);
	}


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7357_Emulator_VerifyStickyNavOnThePdp : T7357_MobileBase
	{
		public T7357_Emulator_VerifyStickyNavOnThePdp(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
		[SkippableTheory]
		[InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
		public void StickyNavOnThePdp(string config) => Validate(config);
	}


	/// <summary>
	/// Verify the functionality of the Sticky Nav on the PDP.
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7458
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7356
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7458"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7356")]
	public abstract class T7356_DesktopBase : T7356_T7357_Base
	{
		protected T7356_DesktopBase(ITestOutputHelper output) : base(output) { }
        protected override void VerifyStickyNavElements()
        {
            Assert.Displayed(ProductDetail.StickyTitle, "Sticky Nav Title Not Found on PDP.");
            Assert.Displayed(ProductDetail.StickyPrice, "Sticky Nav Price Not Found on PDP.");
        }

        protected override string GetProductTitle()
        {
            return ProductDetail.StickyTitle.Text;
        }
    }


	/// <summary>
	/// Verify the functionality of the Sticky Nav on the PDP.
	/// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7458
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7357
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7458"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7357")]
	public abstract class T7357_MobileBase : T7356_T7357_Base
	{
		protected T7357_MobileBase(ITestOutputHelper output) : base(output) { }
        protected override void VerifyStickyNavElements()
        {
            Assert.Displayed(ProductDetail.StickyImage, "Sticky Nav Image Not Found on PDP.");
        }

        protected override string GetProductTitle()
        {
           return ProductDetail.ProductName;
        }
    }


	public abstract class T7356_T7357_Base : ProductDetailTestsBase 
	{
		protected T7356_T7357_Base(ITestOutputHelper output) : base(output) { }
        
        protected void Validate(string config)
        {
            InitializeFramework(config);

            // SKU with related items
            ProductDetail.NavigateToProductDetailByShortSku(ProductActions.GetShortSkuThatHasRelatedProducts);

            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));
            //some products don't have an add to cart button
            Assert.True(GlobalLocators.AddToCartButton.IsInitialized, "Product selected cannot be added to cart");
            Browser.ScrollToBottomOfWindow();
            Browser.Wait.ForDisplayedElement(ProductDetail.StickyWrapper);
            Assert.Displayed(ProductDetail.StickyWrapper, "Sticky Nav Not Found on PDP.");
            VerifyStickyNavElements();
            
            Assert.Displayed(ProductDetail.StickyAddToCart, "Sticky Nav Add To Cart Button Not Found on PDP.");
            var productTitle = GetProductTitle();
            ProductDetail.StickyAddToCart.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(CartOverview.CheckOutNowClass.ToCssClassSelector()));

            Assert.Equals(CartOverview.ProductName(0), productTitle, "Product title in Cart does not match Product title in PDP sticky nav");
        }

        protected abstract void VerifyStickyNavElements();

        protected abstract string GetProductTitle();
    }
}
