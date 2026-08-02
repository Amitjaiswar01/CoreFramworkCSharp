using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Pages.Base;
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
    public class T249_Windows_VerifyFreeShippingAndReturnCalloutOnPdp : T249_DesktopBase
	{
        public T249_Windows_VerifyFreeShippingAndReturnCalloutOnPdp(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
		public void FreeShippingAndReturnCalloutOnPdp(string config= TestConfiguration.Windows_Chrome_SNIS_UNSI) => Validate(config);
	}


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T249_Mac_VerifyFreeShippingAndReturnCalloutOnPdp : T249_DesktopBase
    {
        public T249_Mac_VerifyFreeShippingAndReturnCalloutOnPdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void FreeShippingAndReturnCalloutOnPdp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T249_iPad_VerifyFreeShippingAndReturnCalloutOnPdp : T249_DesktopBase
    {
        public T249_iPad_VerifyFreeShippingAndReturnCalloutOnPdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void FreeShippingAndReturnCalloutOnPdp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T249_TabletEmulator_VerifyFreeShippingAndReturnCalloutOnPdp : T249_DesktopBase
    {
        public T249_TabletEmulator_VerifyFreeShippingAndReturnCalloutOnPdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void FreeShippingAndReturnCalloutOnPdp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.ProductDetail)]
	public class T471_iPhone_VerifyFreeShippingAndReturnCalloutOnPdp : T471_MobileBase
	{
		public T471_iPhone_VerifyFreeShippingAndReturnCalloutOnPdp(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
		public void FreeShippingAndReturnCalloutOnPdp(string config) => Validate(config);
	}


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
	public class T471_Emulator_VerifyFreeShippingAndReturnCalloutOnPdp : T471_MobileBase
	{
        public T471_Emulator_VerifyFreeShippingAndReturnCalloutOnPdp(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
		public void FreeShippingAndReturnCalloutOnPdp(string config = TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI) => Validate(config);
	}


    /// <summary>
    /// Verify that the 'Free Shipping & Free Returns' call out appears for certain categories. 
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5153
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T249
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5153"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T249")]
	public abstract class T249_DesktopBase : T249_T471_Base
	{
		protected T249_DesktopBase(ITestOutputHelper output) : base(output) { }
	}


    /// <summary>
    /// Verify that the 'Free Shipping & Free Returns' call out appears for certain categories. 
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5137
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T471
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5137"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T471")]
	public abstract class T471_MobileBase : T249_T471_Base
	{
		protected T471_MobileBase(ITestOutputHelper output) : base(output) { }
	}


	public abstract class T249_T471_Base : ProductDetailTestsBase
    {
        protected T249_T471_Base(ITestOutputHelper output) : base(output) { }
        
        protected void Validate(string config)
        {
            InitializeFramework(config);

            var freeShippingAndReturnShortSkus = ProductActions.GetFreeShippingAndReturnShortSkus;

            Assert.DatabaseObject(freeShippingAndReturnShortSkus, "ProductActions.GetFreeShippingAndReturnShortSkus");

            Browser.NavigateToPdp(freeShippingAndReturnShortSkus);

            Browser.Wait.IsVisibleElement(By.CssSelector(ProductDetail.LblFreeReturnsBottomId.ToCssIdSelector()));

            Assert.Displayed(ProductDetail.FreeShippingAndReturnElement, "Free Shipping & Free Returns Callout is not displayed on page");

            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));
            Browser.ScrollIntoView(GlobalLocators.AddToCartButton);
            Browser.Wait.ForDomReady();
            GlobalLocators.AddToCartButton.Click();
            Browser.Wait.IsVisibleElement(By.ClassName(CartOverview.CheckOutNowClass));

            Assert.Equals(CartOverview.FreeShippingFreeReturns, CartOverview.ShippingReturnType.Text.Replace("\r\n", " "), "Shipping and Return text does not match.");
        }
    }
}
