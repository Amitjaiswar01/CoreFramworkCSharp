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
    public class T242_Windows_VerifyQpInputDoesNotShowForNonKiosk : T242_DesktopBase
	{
        public T242_Windows_VerifyQpInputDoesNotShowForNonKiosk(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
		[SkippableTheory]
		[InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
		public void QpInputDoesNotShowForNonKiosk(string config) => Validate(config);
	}


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T242_Mac_VerifyQpInputDoesNotShowForNonKiosk : T242_DesktopBase
    {
        public T242_Mac_VerifyQpInputDoesNotShowForNonKiosk(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void QpInputDoesNotShowForNonKiosk(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T242_iPad_VerifyQpInputDoesNotShowForNonKiosk : T242_DesktopBase
    {
        public T242_iPad_VerifyQpInputDoesNotShowForNonKiosk(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void QpInputDoesNotShowForNonKiosk(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T242_TabletEmulator_VerifyQpInputDoesNotShowForNonKiosk : T242_DesktopBase
    {
        public T242_TabletEmulator_VerifyQpInputDoesNotShowForNonKiosk(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void QpInputDoesNotShowForNonKiosk(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.ProductDetail)]
    public class T466_iPhone_VerifyQpInputDoesNotShowForNonKiosk : T466_MobileBase
	{
		public T466_iPhone_VerifyQpInputDoesNotShowForNonKiosk(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
		public void QpInputDoesNotShowForNonKiosk(string config) => Validate(config);
	}


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
	public class T466_Emulator_VerifyQpInputDoesNotShowForNonKiosk : T466_MobileBase
	{
        public T466_Emulator_VerifyQpInputDoesNotShowForNonKiosk(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
		[SkippableTheory]
		[InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
		public void QpInputDoesNotShowForNonKiosk(string config) => Validate(config);
	}


	/// <summary>
	/// Verify that the QP input box or QP link does NOT show for non-kiosk mode.
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5196
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T242
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5196"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T242")]
	public abstract class T242_DesktopBase : T242_T466_Base
	{
		protected T242_DesktopBase(ITestOutputHelper output) : base(output) { }
	}


	/// <summary>
	/// Verify that the QP input box or QP link does NOT show for non-kiosk mode.
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5286
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T466
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5286"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T466")]
	public abstract class T466_MobileBase : T242_T466_Base
	{
		protected T466_MobileBase(ITestOutputHelper output) : base(output) { }
	}


	public abstract class T242_T466_Base : ProductDetailTestsBase
    {        
        protected T242_T466_Base(ITestOutputHelper output) : base(output) { }
        
        protected void Validate(string config)
        {
            InitializeFramework(config);

            var sku = ProductActions.GetAnySkuWithProductDetailPage;

            Assert.DatabaseObject(sku, "ProductActions.GetAnySkuWithProductDetailPage()");

            ProductDetail.NavigateToProductDetailByShortSku(sku);

            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

            Assert.False(ProductDetail.IsQuickPrintLinkVisible, "Quick Print Link should not be displayed.");
            Assert.False(ProductDetail.IsQuickPrintInputVisible, "Quick Print input should not be displayed.");
        }
    }
}
