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
    public class T246_Windows_VerifyLimitedQuantity : T246_DesktopBase
	{
        public T246_Windows_VerifyLimitedQuantity(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
		public void LimitedQuantity(string config) => VerifyLimitedQuantity(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T246_Mac_VerifyLimitedQuantity : T246_DesktopBase
    {
        public T246_Mac_VerifyLimitedQuantity(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LimitedQuantity(string config) => VerifyLimitedQuantity(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T246_iPad_VerifyLimitedQuantity : T246_DesktopBase
    {
        public T246_iPad_VerifyLimitedQuantity(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LimitedQuantity(string config) => VerifyLimitedQuantity(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T246_TabletEmulator_VerifyLimitedQuantity : T246_DesktopBase
    {
        public T246_TabletEmulator_VerifyLimitedQuantity(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LimitedQuantity(string config) => VerifyLimitedQuantity(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.ProductDetail)]
    public class T468_iPhone_VerifyLimitedQuantity : T468_MobileBase
	{
		public T468_iPhone_VerifyLimitedQuantity(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
		public void LimitedQuantity(string config) => VerifyLimitedQuantity(config);
	}


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T468_Emulator_VerifyLimitedQuantity : T468_MobileBase
	{
        public T468_Emulator_VerifyLimitedQuantity(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
		[SkippableTheory]
		[InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
		public void LimitedQuantity(string config) => VerifyLimitedQuantity(config);
	}


	/// <summary>
	/// Verify that products with limited inventory shows the 'Only_ Left!' call out.
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5255
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T246
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5255"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T246")]
	public abstract class T246_DesktopBase : T246_T468_Base
	{
		protected T246_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected override void VerifyCallout()
        {          
            Assert.Equals(CartOverview.AlmostSoldOut, CartOverview.AlmostSoldOutCallout.Text, "Almost Sold Out callout does not match expected string.");
        }
    }


	/// <summary>
	/// Verify that products with limited inventory shows the 'Only_ Left!' call out.
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5291
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T468
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5291"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T468")]
	public abstract class T468_MobileBase : T246_T468_Base
	{
		protected T468_MobileBase(ITestOutputHelper output) : base(output) { }

        protected override void VerifyCallout()
        {
            Assert.Equals(CartOverview.AlmostSoldOut, CartOverview.AlmostSoldOutCallout.Text, "Almost Sold Out callout does not match expected string.");
        }
    }

	
	public abstract class T246_T468_Base : ProductDetailTestsBase
    {
        protected T246_T468_Base(ITestOutputHelper output) : base(output) { }

        protected void VerifyLimitedQuantity(string config)
        {
            InitializeFramework(config);

            var productWithLimitedInventory = ProductActions.GetProductWithLimitedInventory();

            Assert.DatabaseObject(productWithLimitedInventory,
                "ProductActions.GetProductWithLimitedInventory()");

            ProductDetail.NavigateToProductDetailByShortSku(productWithLimitedInventory.Sku);

            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

            var isQuantityLeftShows = ProductDetail.IsQuantityLeftShows;

            Assert.True(isQuantityLeftShows, "Quantity Left call out is not displayed.");

            var quantity = ProductDetail.QuantityLeft;

            Assert.Equals(int.Parse(quantity), productWithLimitedInventory.CurrentInventory,
                "Quantity do not match.");

            var quantityInDropbox = ProductDetail.MaxAvailableQuantity;

            Assert.Equals(int.Parse(quantityInDropbox), productWithLimitedInventory.CurrentInventory,
                "Quantity in drop box do not match.");

            Browser.ClickByJs(GlobalLocators.AddToCartButton);
            Browser.Wait.IsVisibleElement(By.ClassName(CartOverview.CheckOutNowClass));

            VerifyCallout();
        }

        protected abstract void VerifyCallout();
    }
}
