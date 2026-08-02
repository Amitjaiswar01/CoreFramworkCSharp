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
    public class T250_Windows_VerifyFreeForOrdersOver49CallOut : T250_DesktopBase
	{
        public T250_Windows_VerifyFreeForOrdersOver49CallOut(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
		[SkippableTheory]
		[InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
		public void FreeForOrdersOver49CallOut(string config) => Validate(config);
	}


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T250_Mac_VerifyFreeForOrdersOver49CallOut : T250_DesktopBase
    {
        public T250_Mac_VerifyFreeForOrdersOver49CallOut(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void FreeForOrdersOver49CallOut(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T250_iPad_VerifyFreeForOrdersOver49CallOut : T250_DesktopBase
    {
        public T250_iPad_VerifyFreeForOrdersOver49CallOut(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void FreeForOrdersOver49CallOut(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T250_TabletEmulator_VerifyFreeForOrdersOver49CallOut : T250_DesktopBase
    {
        public T250_TabletEmulator_VerifyFreeForOrdersOver49CallOut(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void FreeForOrdersOver49CallOut(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.ProductDetail)]
	public class T472_iPhone_VerifyFreeForOrdersOver49CallOut : T472_MobileBase
	{
        public T472_iPhone_VerifyFreeForOrdersOver49CallOut(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
		public void FreeForOrdersOver49CallOut(string config) => Validate(config);
	}


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
	public class T472_Emulator_VerifyFreeForOrdersOver49CallOut : T472_MobileBase
	{
		public T472_Emulator_VerifyFreeForOrdersOver49CallOut(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
		[SkippableTheory]
		[InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
		public void FreeForOrdersOver49CallOut(string config) => Validate(config);
	}


	/// <summary>
	/// Verify the "Ships Free With Orders Over $49" call out appears for certain categories.
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5331
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T250
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5331"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T250")]
	public abstract class T250_DesktopBase : T250_T472_Base
	{
		protected T250_DesktopBase(ITestOutputHelper output) : base(output) { }
	}


	/// <summary>
	/// Verify the "Ships Free With Orders Over $49" call out appears for certain categories.
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5471
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T472
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5471"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T472")]
	public abstract class T472_MobileBase : T250_T472_Base
	{
		protected T472_MobileBase(ITestOutputHelper output) : base(output) { }
	}


	public abstract class T250_T472_Base : ProductDetailTestsBase
    {
        protected T250_T472_Base(ITestOutputHelper output) : base(output) { }
        
        protected void Validate(string config)
        {
            InitializeFramework(config);

            var shortSku = ProductActions.GetShipsFreeOnOrdersOver49CallOutShortSku;

            Assert.DatabaseObject(shortSku, "ProductActions.GetShipsFreeOnOrdersOver49CallOutShortSku()");

            ProductDetail.NavigateToProductDetailByShortSku(shortSku);

            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

            Assert.Equals("SHIPS FREE WITH ORDERS OVER $49*", ProductDetailMultiProduct.GetShippingCallOut().ToUpper().Trim().Replace("  ", " "), "Shipping call out do not match.");
        }
    }
}
