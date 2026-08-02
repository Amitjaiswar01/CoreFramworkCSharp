using Xunit;
using Xunit.Abstractions;
using OpenQA.Selenium;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.DesktopTablet.ProductDetail;

using ProductModel = LampsPlus.AutomationFramework.Databases.Entities.ProductModel;

namespace LampsPlus.RegressionTests.Common.ProductDetail
{
    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T248_Windows_VerifyClearanceItemsSaveAmountDisplay : T248_DesktopBase
	{
        public T248_Windows_VerifyClearanceItemsSaveAmountDisplay(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
		[InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
		public void ClearanceItemsSaveAmountDisplay(string config) => Validate(config);
	}


	//[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
	[Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
	public class T248_Windows_Kiosk_VerifyClearanceItemsSaveAmountDisplay : T248_DesktopBase
	{
		public T248_Windows_Kiosk_VerifyClearanceItemsSaveAmountDisplay(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
		[SkippableTheory]
		[InlineData(TestConfiguration.Windows_Chrome_SIS_UNSI)]
		public void ClearanceItemsSaveAmountDisplay(string config) => Validate(config);
	}


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T248_Mac_VerifyClearanceItemsSaveAmountDisplay : T248_DesktopBase
    {
        public T248_Mac_VerifyClearanceItemsSaveAmountDisplay(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void ClearanceItemsSaveAmountDisplay(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T248_iPad_VerifyClearanceItemsSaveAmountDisplay : T248_DesktopBase
    {
        public T248_iPad_VerifyClearanceItemsSaveAmountDisplay(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void ClearanceItemsSaveAmountDisplay(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T248_TabletEmulator_VerifyClearanceItemsSaveAmountDisplay : T248_DesktopBase
    {
        public T248_TabletEmulator_VerifyClearanceItemsSaveAmountDisplay(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void ClearanceItemsSaveAmountDisplay(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.ProductDetail)]
	public class T470_iPhone_VerifyClearanceItemsSaveAmountDisplay : T470_MobileBase
	{
		public T470_iPhone_VerifyClearanceItemsSaveAmountDisplay(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void ClearanceItemsSaveAmountDisplay(string config) => Validate(config);
	}


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
	public class T470_Emulator_VerifyClearanceItemsSaveAmountDisplay : T470_MobileBase
	{
        public T470_Emulator_VerifyClearanceItemsSaveAmountDisplay(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
		[SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
		public void ClearanceItemsSaveAmountDisplay(string config) => Validate(config);
	}


	/// <summary>
	/// Verify that Clearance items always display 'Save $amount' unless amount is greater than Five dollars.
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5504
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T248
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5504"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T248")]
	public abstract class T248_DesktopBase : T248_T470_Base
	{
		protected T248_DesktopBase(ITestOutputHelper output) : base(output) { }

		protected override void GetClearanceCallout(string clearanceCallout)
        {
            Assert.True(clearanceCallout.CaseInsensitiveContains("CLEARANCE"), "Clearance call out is not displayed");
		}

        protected override void VerifySticky(ProductModel skuSavePriceFiveAndOver, string formattedMainPrice, string formattedStruckPrice, string formattedSavingsPrice)
        {
			Browser.Wait.ForDisplayedElement(ProductDetail.StickyCallOut);
			var stickyCallout = TextActions.RegexNoTabsAndNewLines(ProductDetail.StickyCallOut.Text);
			var stickyMainPrice = TextActions.RemoveDollarSign(ProductDetail.StickyCallOut.Text.ToLower()).Replace("clearance", string.Empty).Trim();
			
			Browser.Wait.ForDisplayedElement(ProductDetail.OrigPrice);
            var stickyStruckThroughPrice = TextActions.GetPriceTextOnly(ProductDetail.OrigPrice.Text);
			Browser.Wait.ForDisplayedElement(ProductDetail.StickySaveCallout);
			var stickySaveVerbiage = ProductDetail.StickySaveCallout.Text;
			var stickySaveCalloutPrice = TextActions.RemoveDollarSign(ProductDetail.StickySaveCallout.Text).Replace("Save", string.Empty).Trim();

			// verify the mainPrice, struckThroughPrice, savingsPrice are visible
			Assert.StringContains(stickyCallout, "CLEARANCE", "Clearance call out is not displayed.");
			Assert.Equals(formattedMainPrice, stickyMainPrice, "Clearance price on UI doesn't not match RetailPrice or InitialRetailPrice in the database.");
			Assert.Equals(formattedStruckPrice, stickyStruckThroughPrice, "The Clearance value does not match with the InitialRetailPrice column from the database query.");
			Assert.StringContains(stickySaveVerbiage,"Save", "Save call out is not displayed.");
			Assert.Equals(formattedSavingsPrice, stickySaveCalloutPrice, "The Clearance value does not match with the Savings column from the database query.");	
		}
    }


	/// <summary>
	/// Verify that Clearance items always display 'Save $amount' unless amount is greater than Five dollars.
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5263
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T470
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5263"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T470")]
	public abstract class T470_MobileBase : T248_T470_Base
	{
		protected T470_MobileBase(ITestOutputHelper output) : base(output) { }

		protected override void GetClearanceCallout(string clearanceCallout)
		{
			Assert.Equals(clearanceCallout, "Clearance", "Clearance call out is not displayed");
		}

        protected override void VerifySticky(ProductModel skuSavePriceFiveAndOver, string formattedMainPrice, string formattedStruckPrice, string formattedSavingsPrice)
		{ 
			Browser.Wait.ForDisplayedElement(ProductDetail.StickyCallOut);
			
			var clearanceCallOut = ProductDetail.StickyCallOut.Text;
			var stickyMainPrice = ProductDetail.StickyCallOut.Text.Replace("CLEARANCE\r\n$", string.Empty).Trim();

			//Verify the mainPrice, clearance callout are visible
			Assert.StringContains(clearanceCallOut, "CLEARANCE", "Clearance call out is not displayed.");
			Assert.Equals(formattedMainPrice, stickyMainPrice, "Clearance price on UI doesn't not match RetailPriceInternet in the database.");
        }
    }


	public abstract class T248_T470_Base : ProductDetailTestsBase
    {
        protected T248_T470_Base(ITestOutputHelper output) : base(output) { }

		/// <summary>
		/// Verify that the Clearance items always display 'Save $amount' when amount is greater than Five  dollar
		/// </summary>
		protected void Validate(string config)
		{
			InitializeFramework(config);

			var skuSavePriceFiveAndOver = ProductActions.GetSkuSavePriceFiveAndOver;

			var sku = skuSavePriceFiveAndOver.ShortSku;
			
			Assert.DatabaseObject(skuSavePriceFiveAndOver, "ProductActions.GetSkuSavePriceFiveAndOver()");

			//Navigate to product page
			Browser.NavigateToPdp(sku);

			Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

			//Verify the clearance callout
			var clearanceCallout = ProductDetail.ProductCallOut.Text.Trim();  
			GetClearanceCallout(clearanceCallout);

            var mainPrice = TextActions.RemoveDollarSign(ProductDetail.ItemPriceText).Replace("Clearance", string.Empty).Trim();
            mainPrice = TextActions.RemoveTextBeforeAndIncludingCharacter(mainPrice, ':').Trim();

            var formattedMainPrice = TextActions.FormatToTwoDecimals(skuSavePriceFiveAndOver.RetailPriceInternet);

			//Check whether kiosk is logged in or not
			if (ProductDetail.IsLoggedInAsKiosk)
            {
                mainPrice = TextActions.RemoveDollarSign(ProductDetail.ItemPriceText).Replace("\r\n", "").Replace("Price:", string.Empty).Replace("Clearance", string.Empty);
				formattedMainPrice = TextActions.FormatToTwoDecimals(skuSavePriceFiveAndOver.RetailPrice);
			}

			Browser.Wait.ForDisplayedElement(ProductDetail.OrigPrice);
            var struckThroughPrice = TextActions.GetPriceTextOnly(ProductDetail.OrigPrice.Text);

            var formattedStruckPrice = TextActions.FormatToTwoDecimals(skuSavePriceFiveAndOver.InitialRetailPrice);
			var formattedSavingsPrice = TextActions.FormatToTwoDecimals(skuSavePriceFiveAndOver.Savings);

			//Verify the mainPrice, struckThroughPrice, savingsPrice are visible
			Assert.Equals(formattedMainPrice, mainPrice, "Clearance price on UI doesn't not match RetailPrice or InitialRetailPrice in the database.");
			Assert.Equals(formattedStruckPrice, struckThroughPrice, "The Clearance value does not match with the InitialRetailPrice column from the database query.");

			//Verify that endDate, saleVerbiage is not visible
            Assert.False(ProductDetail.IsCheckEndDateCallOut, "End Callout should not display on site");
			Assert.False(ProductDetail.IsSaleVerbiageVisible, "Save verbiage and Save value should not display"); 

			//Scroll the page to the bottom of the window 
			Browser.ScrollToBottomOfWindow();

			//Verify the call out on sticky section
			VerifySticky(skuSavePriceFiveAndOver, formattedMainPrice, formattedStruckPrice, formattedSavingsPrice);
		}


		protected abstract void VerifySticky(ProductModel skuSavePriceFiveAndOver, string formattedMainPrice, string formattedStruckPrice, string formattedSavingsPrice);

		protected abstract void GetClearanceCallout(string clearanceCallout);
    }
}
