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
    public class T261_Windows_VerifyMarginLinkNotDisplay : T261_DesktopBase
	{
        public T261_Windows_VerifyMarginLinkNotDisplay(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
		[SkippableTheory]
		[InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
		public void MarginLinkNotDisplay(string config) => Validate(config);
	}


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T261_Mac_VerifyMarginLinkNotDisplay : T261_DesktopBase
    {
        public T261_Mac_VerifyMarginLinkNotDisplay(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void FreeShippingOnProduct(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T261_iPad_VerifyMarginLinkNotDisplay : T261_DesktopBase
    {
        public T261_iPad_VerifyMarginLinkNotDisplay(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void FreeShippingOnProduct(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T261_TabletEmulator_VerifyMarginLinkNotDisplay : T261_DesktopBase
    {
        public T261_TabletEmulator_VerifyMarginLinkNotDisplay(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void FreeShippingOnProduct(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.ProductDetail)]
	public class T477_iPhone_VerifyMarginLinkNotDisplay : T477_MobileBase
	{
		public T477_iPhone_VerifyMarginLinkNotDisplay(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
		public void MarginLinkNotDisplay(string config) => Validate(config);
	}


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
	public class T477_Emulator_VerifyMarginLinkNotDisplay : T477_MobileBase
	{
        public T477_Emulator_VerifyMarginLinkNotDisplay(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
		[SkippableTheory]
		[InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
		public void MarginLinkNotDisplay(string config) => Validate(config);
	}


	/// <summary>
	/// Verify that the 'Show margin' feature is NOT available to non-employees.
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5083
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T261
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5083"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T261")]
	public abstract class T261_DesktopBase : T261_T477_Base
	{
		protected T261_DesktopBase(ITestOutputHelper output) : base(output) { }
	}


	/// <summary>
	/// Verify that the 'Show margin' feature is NOT available to non-employees.
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5378
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T477
	/// </summary>
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5378"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T477")]
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
	public abstract class T477_MobileBase : T261_T477_Base
	{
		protected T477_MobileBase(ITestOutputHelper output) : base(output) { }
	}


	public abstract class T261_T477_Base : ProductDetailTestsBase
    {
        protected T261_T477_Base(ITestOutputHelper output) : base(output) { }
		
		protected void Validate(string config)
        {
            InitializeFramework(config);

            var sku = ProductActions.GetAnySkuWithProductDetailPage;

            Assert.DatabaseObject(sku, "ProductActions.GetAnySkuWithProductDetailPage()");

            ProductDetail.NavigateToProductDetailByShortSku(sku);

            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

            Assert.False(ProductDetail.IsMarginLinkVisible, "Margin link should not be displayed.");
        }
    }
}
