using OpenQA.Selenium;
using xRetry;
using Xunit;
using Xunit.Abstractions;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.DesktopTablet.ProductDetail;

namespace LampsPlus.RegressionTests.Common.ProductDetail
{
	//[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
	[Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
	public class T7759_Windows_VerifyProCustomersSeeCorrectPriceAndTradeSavingsForItems : T7759_DesktopBase
	{
		public T7759_Windows_VerifyProCustomersSeeCorrectPriceAndTradeSavingsForItems(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
		[InlineData(TestConfiguration.Windows_Chrome_SNIS_PCSI)]
		public void ProCustomersSeeCorrectPrice(string config) => Validate(config);
	}


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7759_Mac_VerifyProCustomersSeeCorrectPriceAndTradeSavingsForItems : T7759_DesktopBase
    {
        public T7759_Mac_VerifyProCustomersSeeCorrectPriceAndTradeSavingsForItems(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
		[InlineData(TestConfiguration.Mac_Safari_SNIS_PCSI)]
        public void ProCustomersSeeCorrectPrice(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7759_iPad_VerifyProCustomersSeeCorrectPriceAndTradeSavingsForItems : T7759_DesktopBase
    {
        public T7759_iPad_VerifyProCustomersSeeCorrectPriceAndTradeSavingsForItems(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
		[InlineData(TestConfiguration.iPad_Safari_SNIS_PCSI)]
        public void ProCustomersSeeCorrectPrice(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7759_TabletEmulator_VerifyProCustomersSeeCorrectPriceAndTradeSavingsForItems : T7759_DesktopBase
    {
        public T7759_TabletEmulator_VerifyProCustomersSeeCorrectPriceAndTradeSavingsForItems(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
		[InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_PCSI)]
        public void ProCustomersSeeCorrectPrice(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.ProductDetail)]
	public class T6993_iPhone_VerifyProCustomersSeeCorrectPriceAndTradeSavingsForItems : T6993_MobileBase
	{
		public T6993_iPhone_VerifyProCustomersSeeCorrectPriceAndTradeSavingsForItems(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
		[InlineData(TestConfiguration.iPhone_Safari_SNIS_PCSI)]
		public void ProCustomersSeeCorrectPrice(string config) => Validate(config);
	}


	//[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
	[Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
	public class T6993_Emulator_VerifyProCustomersSeeCorrectPriceAndTradeSavingsForItems : T6993_MobileBase
	{
		public T6993_Emulator_VerifyProCustomersSeeCorrectPriceAndTradeSavingsForItems(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
		[InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_PCSI)]
		public void ProCustomersSeeCorrectPrice(string config) => Validate(config);
	}


	/// <summary>
	/// Verify that Pro Customers see the correct price and Trade Savings for items
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9119
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7759
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9119"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7759")]
	public abstract class T7759_DesktopBase : T7759_T6993_Base
	{
		protected T7759_DesktopBase(ITestOutputHelper output) : base(output) { }
	}


	/// <summary>
	/// Verify that Pro Customers see the correct price and Trade Savings for items
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9119
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T6993
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9119"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T6993")]
	public abstract class T6993_MobileBase : T7759_T6993_Base
	{
		protected T6993_MobileBase(ITestOutputHelper output) : base(output) { }
		protected override void Validate(string config)
		{
			InitializeFramework(config);

			var shortsku = ProductActions.GetProductTradeData();
			var sku = shortsku.ShortSku;

			Browser.NavigateToPdp(sku);

			var dbRetailPrice = TextActions.FormatToTwoDecimals(shortsku.RetailPriceInternet);
			var dbTradePrice = TextActions.FormatToTwoDecimals(shortsku.SpecialDiscount);
			var dbSavings = TextActions.FormatToTwoDecimals(shortsku.Savings);

			Browser.Wait.IsVisibleElement(By.CssSelector(ProductDetail.ProsTradePriceId.ToCssIdSelector()));

			var retailPrice = ProductDetail.ProsRetailPrice.Text.Replace("$", string.Empty).TrimStart().Replace("\r\n", string.Empty).Replace("i", string.Empty);
			var tradePrice = ProductDetail.ProsTradePrice.Text.Replace("$", string.Empty).TrimStart();
			var sale = ProductDetail.ProsSaving.Text.Replace("Your Savings $", string.Empty).TrimStart();
			var yourSavingPrice = ProductDetail.ProsSaving.Text;
			var yourTradePrice = ProductDetail.ProsSpecialPriceCallout.Text;

			Assert.Equals(dbTradePrice, tradePrice, "Trade Price not matching");
			Assert.Equals(dbRetailPrice, retailPrice, "Retail Price not matching");
			Assert.Equals(dbSavings, sale, "Saving Price not matching");
			Assert.True(yourTradePrice.CaseInsensitiveContains("PROS SPECIAL PRICE"), "Pros Special Price Text not matching");
			Assert.Equals(yourSavingPrice, "Your Savings $" + sale, "Saving Price Text not matching");
		}
	}


	public abstract class T7759_T6993_Base : ProductDetailTestsBase
	{
		protected T7759_T6993_Base(ITestOutputHelper output) : base(output) { }

		protected virtual void Validate(string config)
		{
			InitializeFramework(config);

			var shortsku = ProductActions.GetProductTradeData();
			var sku = shortsku.ShortSku;
			
			Browser.NavigateToPdp(sku);

            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

			var dbRetailPrice = TextActions.FormatToTwoDecimals(shortsku.RetailPriceInternet); 
			var dbTradePrice = TextActions.FormatToTwoDecimals(shortsku.SpecialDiscount);
			var dbSavings = TextActions.FormatToTwoDecimals(shortsku.Savings);
            var retailPrice = TextActions.RegexNoTabsAndNewLines(ProductDetail.ProsRetailPrice.Text.Split(' ')[0].Replace("$", string.Empty).Replace("i", string.Empty).TrimEnd());
			var tradePrice = ProductDetail.ProsTradePrice.Text.Replace("$", string.Empty).TrimEnd();
			var sale = ProductDetail.ProsSaving.Text.Replace("Your Savings $", string.Empty).TrimStart();
			var yourSavingPrice = ProductDetail.ProsSaving.Text;
			var yourTradePrice = ProductDetail.PriceType.Text;

			Assert.Equals(dbTradePrice, tradePrice, "Trade Price not matching");
			Assert.Equals(dbRetailPrice, retailPrice, "Retail Price not matching");
			Assert.Equals(dbSavings, sale, "Saving Price not matching");
            Assert.True(yourTradePrice.CaseInsensitiveContains("PROS SPECIAL PRICE"), "Pros Special Price Text not matching");
            Assert.Equals(yourSavingPrice, "Your Savings $" + sale, "Saving Price Text not matching");
		}
	}
}
