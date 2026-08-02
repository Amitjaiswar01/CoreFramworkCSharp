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
    public class T222_Windows_VerifyFreeShippingAndReturns : T222_DesktopBase
	{
        public T222_Windows_VerifyFreeShippingAndReturns(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
		public void FreeShippingAndReturns(string config) => Validate(config);
	}


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T222_Mac_VerifyFreeShippingAndReturns : T222_DesktopBase
    {
        public T222_Mac_VerifyFreeShippingAndReturns(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void FreeShippingAndReturns(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T222_iPad_VerifyFreeShippingAndReturns : T222_DesktopBase
    {
        public T222_iPad_VerifyFreeShippingAndReturns(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void FreeShippingAndReturns(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T222_TabletEmulator_VerifyFreeShippingAndReturns : T222_DesktopBase
    {
        public T222_TabletEmulator_VerifyFreeShippingAndReturns(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void FreeShippingAndReturns(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.ProductDetail)]
	public class T456_iPhone_VerifyFreeShippingAndReturns : T456_MobileBase
	{
		public T456_iPhone_VerifyFreeShippingAndReturns(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
		public void FreeShippingAndReturns(string config) => Validate(config);
	}


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
	public class T456_Emulator_VerifyFreeShippingAndReturns : T456_MobileBase
	{
        public T456_Emulator_VerifyFreeShippingAndReturns(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
		public void FreeShippingAndReturns(string config) => Validate(config);
	}


	/// <summary>
	/// Verify that all items with the 'Free Shipping and Free Returns' attribute persist to the PDP. 
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5323
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T222
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5323"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T222")]
	public abstract class T222_DesktopBase : T222_T456_Base
	{
		protected T222_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected override void WaitForPageLoad()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(Sort.SortFilterDisplaySetDropdownsClass.ToCssClassSelector()));
        }
	}


	/// <summary>
	/// Verify that all items with the 'Free Shipping and Free Returns' attribute persist to the PDP. 
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5488
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T456
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5488"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T456")]
	public abstract class T456_MobileBase : T222_T456_Base
	{
		protected T456_MobileBase(ITestOutputHelper output) : base(output) { }

        protected override void WaitForPageLoad()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(Sort.ToggleSortMenuClass.ToCssClassSelector()));
        }
    }


	public abstract class T222_T456_Base : ProductDetailTestsBase
    {
        protected T222_T456_Base(ITestOutputHelper output) : base(output) { }

		protected void Validate(string config)
        {
            InitializeFramework(config, Urls.PdpFreeShippingReturnsUrl);

            WaitForPageLoad();
           
            var links = Sort.FindLinksForGivenNumberOfProductsOnSortPage(3);
            Browser.Wait.ForDomReady();
            foreach (var link in links)
            {
                ProductDetail.Navigate(link);

                Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.ProductNameId.ToCssIdSelector()));
             
                Assert.Displayed(ProductDetail.FreeShippingAndReturnElement, "The Free Shipping and Free Return element was expected but not displayed on the screen.");
            }
        }

        protected abstract void WaitForPageLoad();
    }
}
