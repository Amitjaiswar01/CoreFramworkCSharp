using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
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
    public class T247_Windows_VerifySoldOutCallOut : T247_DesktopBase
	{
        public T247_Windows_VerifySoldOutCallOut(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
		[InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
		public void SoldOutCallOut(string config) => Validate(config);
	}


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T247_Mac_VerifySoldOutCallOut : T247_DesktopBase
    {
        public T247_Mac_VerifySoldOutCallOut(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void SoldOutCallOut(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T247_iPad_VerifySoldOutCallOut : T247_DesktopBase
    {
        public T247_iPad_VerifySoldOutCallOut(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void SoldOutCallOut(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T247_TabletEmulator_VerifySoldOutCallOut : T247_DesktopBase
    {
        public T247_TabletEmulator_VerifySoldOutCallOut(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void SoldOutCallOut(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.ProductDetail)]
	public class T469_iPhone_VerifySoldOutCallOut : T469_MobileBase
	{
		public T469_iPhone_VerifySoldOutCallOut(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
		public void SoldOutCallOut(string config) => Validate(config);
	}


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
	public class T469_Emulator_VerifySoldOutCallOut : T469_MobileBase
	{
        public T469_Emulator_VerifySoldOutCallOut(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
		[InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
		public void SoldOutCallOut(string config) => Validate(config);
	}


	/// <summary>
	/// Verify the 'Sold Out' call out shows when all available quantity is added to cart.
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5200
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T247
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5200"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T247")]
	public abstract class T247_DesktopBase : T247_T469_Base
	{
		protected T247_DesktopBase(ITestOutputHelper output) : base(output) { }
	}


	/// <summary>
	/// Verify the 'Sold Out' call out shows when all available quantity is added to cart.
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5103
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T469
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5103"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T469")]
	public abstract class T469_MobileBase : T247_T469_Base
	{
		protected T469_MobileBase(ITestOutputHelper output) : base(output) { }
	}


	public abstract class T247_T469_Base : ProductDetailTestsBase
    {
        protected T247_T469_Base(ITestOutputHelper output) : base(output) { }
        
        protected void Validate(string config)
        {
            InitializeFramework(config, Urls.HomePageUrl);

            var shortSku = ProductActions.GetShortSkuOnClearance;

            Assert.DatabaseObject(shortSku, "ProductActions.GetShortSkuOnClearance()");

            ProductDetail.NavigateToProductDetailByShortSku(shortSku);
            ProductDetail.AddMaxQuantityToCart();
			Browser.Wait.IsVisibleElement(By.CssSelector(CartOverview.CheckOutNowClass.ToCssClassSelector()));
			ProductDetail.NavigateToProductDetailByShortSku(shortSku);

            Assert.Displayed(ProductDetail.SoldOutLabel, "Product call out is not displayed");
            Assert.True(!GlobalLocators.PlaAddToCartElement.IsInitialized, "Add to cart button should not be displayed.");
        }
    }
}
