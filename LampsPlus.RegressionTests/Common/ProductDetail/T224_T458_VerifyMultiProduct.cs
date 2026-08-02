using System;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.DesktopTablet.ProductDetail;
using xRetry;

namespace LampsPlus.RegressionTests.Common.ProductDetail
{
    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T224_Windows_VerifyMultiProduct : T224_DesktopBase
	{
        public T224_Windows_VerifyMultiProduct(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void MultiProduct(string config) => Validate(config);
	}


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T224_Mac_VerifyMultiProduct : T224_DesktopBase
    {
        public T224_Mac_VerifyMultiProduct(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T224. Rework - ACD-10314")]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void MultiProduct(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T224_iPad_VerifyMultiProduct : T224_DesktopBase
    {
        public T224_iPad_VerifyMultiProduct(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T224. Rework - ACD-10314")]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void MultiProduct(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T224_TabletEmulator_VerifyMultiProduct : T224_DesktopBase
    {
        public T224_TabletEmulator_VerifyMultiProduct(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T224. Rework - ACD-10314")]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void MultiProduct(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.ProductDetail)]
    public class T458_iPhone_VerifyMultiProduct : T458_MobileBase
	{
		public T458_iPhone_VerifyMultiProduct(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void MultiProduct(string config) => Validate(config);
	}


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
	public class T458_Emulator_VerifyMultiProduct : T458_MobileBase
	{
        public T458_Emulator_VerifyMultiProduct(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
		[SkippableTheory]
		[InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void MultiProduct(string config) => Validate(config);
	}


	/// <summary>
	/// Verify that a product on the PDP page is eligible to qualify as a Multi-Product.
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5496
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T224
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5496"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T224")]
	public abstract class T224_DesktopBase : T224_T458_Base
	{
		protected T224_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected override void WaitForMultiProductMenuToOpen()
        {
            Browser.Wait.ForDomReady();
        }

        protected override void SelectUnselectedMultiProductDropdownOption(IElement element)
        {
            element.Click();
        }

        protected override void OpenMultiProductMenu()
        {
            ProductDetailMultiProduct.SelectedMultiProductDropdownOption.Click();
        }
    }


	/// <summary>
	/// Verify that a product on the PDP page is eligible to qualify as a Multi-Product.
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5549
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T458
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5549"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T458")]
	public abstract class T458_MobileBase : T224_T458_Base
	{
		protected T458_MobileBase(ITestOutputHelper output) : base(output) { }

        protected override void WaitForMultiProductMenuToOpen()
        {
            Browser.Wait.IsVisibleElement(By.XPath(GlobalLocators.SubMenuCloseButtonXpath));
        }

        protected override void SelectUnselectedMultiProductDropdownOption(IElement element)
        {
            if (Browser.Device != null)
            {
                if (Browser.Device.IsIphone)
                {
                    Browser.ClickWithTapByElementCoordinates(element);
                }
                else
                {
                    element.Click();
                }
            }
        }

        protected override void OpenMultiProductMenu()
        {
            Browser.ScrollIntoView(ProductDetailMultiProduct.SelectedMultiProductDropdownOption);
            Browser.ClickOnButtonMultipleTimes(ProductDetailMultiProduct.SelectedMultiProductDropdownOption, 5, ProductDetailMultiProduct.IsMultiProductOverlayOpen);
        }
    }


	public abstract class T224_T458_Base : ProductDetailTestsBase
    {
        protected T224_T458_Base(ITestOutputHelper output) : base(output) { }
		
		protected void Validate(string config)
        {
            InitializeFramework(config, Urls.HomePageUrl);

            var shortSku = ProductActions.GetMultiProductShortSku;

            Assert.DatabaseObject(shortSku, "Database: ProductActions.GetMultiProductShortSku()");

            ProductDetail.NavigateToProductDetailByShortSku(shortSku);

            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.AddToCartMultiproductId.ToCssIdSelector()));

            Assert.True(ProductDetailMultiProduct.MultiProductAvailableOptionsText.Equals(ProductDetailMultiProduct.AvailableOptionsSectionTitle.Text, StringComparison.OrdinalIgnoreCase) && ProductDetailMultiProduct.AvailableOptionsSectionTitle.Displayed, "Available Options Header incorrect.");

            var priceBeforeChange = ProductDetail.ItemPriceText;

            var multiProductOptions = ProductDetailMultiProduct.MultiProductDropdownOptions;

            Assert.True(multiProductOptions.Count > 0, "MultiProduct does not show multiple radio options");

            OpenMultiProductMenu();

            WaitForMultiProductMenuToOpen();

            VerifyOptionNamesAndPricesAreCorrect();

            var unselectedMultiProductDropdownOption = ProductDetailMultiProduct.UnselectedMultiProductDropdownOption;

            SelectUnselectedMultiProductDropdownOption(unselectedMultiProductDropdownOption);

            var productDetailItemPrice = ProductDetail.ItemPriceText;

            Assert.True(priceBeforeChange != productDetailItemPrice, "MultiProduct price did not change after dropdown option selection.");
        }

        protected void VerifyOptionNamesAndPricesAreCorrect()
        {
            foreach (var optionName in ProductDetailMultiProduct.MultiProductOptionNames)
            {
                Assert.True(optionName.Displayed && optionName.Text != string.Empty, "Dropdown option name empty or not displayed.");
            }

            foreach (var optionPrice in ProductDetailMultiProduct.MultiProductPrices)
            {
                Assert.True(optionPrice.Displayed && optionPrice.Text.Contains("$"), "Dropdown price empty or not displayed.");
            }
        }

        protected abstract void OpenMultiProductMenu();

        protected abstract void WaitForMultiProductMenuToOpen();

        protected abstract void SelectUnselectedMultiProductDropdownOption(IElement element);
    }
}
