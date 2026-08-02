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
    public class T263_Windows_VerifyFanHasEnergyGuideIcon : T263_DesktopBase
	{
        public T263_Windows_VerifyFanHasEnergyGuideIcon(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
		[SkippableTheory]
		[InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
		public void VerifyFanHasEnergyGuideIco(string config) => Validate(config);
	}


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T263_Mac_VerifyFanHasEnergyGuideIcon : T263_DesktopBase
    {
        public T263_Mac_VerifyFanHasEnergyGuideIcon(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyFanHasEnergyGuideIco(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T263_iPad_VerifyFanHasEnergyGuideIcon : T263_DesktopBase
    {
        public T263_iPad_VerifyFanHasEnergyGuideIcon(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyFanHasEnergyGuideIco(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T263_TabletEmulator_VerifyFanHasEnergyGuideIcon : T263_DesktopBase
    {
        public T263_TabletEmulator_VerifyFanHasEnergyGuideIcon(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyFanHasEnergyGuideIco(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.ProductDetail)]

    public class T478_iPhone_VerifyFanHasEnergyGuideIcon : T478_MobileBase
	{
		public T478_iPhone_VerifyFanHasEnergyGuideIcon(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
		public void VerifyFanHasEnergyGuideIco(string config) => Validate(config);
	}


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
	public class T478_Emulator_VerifyFanHasEnergyGuideIcon : T478_MobileBase
	{
        public T478_Emulator_VerifyFanHasEnergyGuideIcon(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
		public void VerifyFanHasEnergyGuideIco(string config) => Validate(config);
	}


	/// <summary>
	///  Verify that all fans have Energy information.
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5030
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T263
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5030"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T263")]
	public abstract class T263_DesktopBase : T263_T478_Base
	{
		protected T263_DesktopBase(ITestOutputHelper output) : base(output) { }

		protected override void ClickProductDescription() {}
	}


	/// <summary>
	///  Verify that all fans have Energy information.
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5533
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T478
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5533"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T478")]
	public abstract class T478_MobileBase : T263_T478_Base
	{
		protected T478_MobileBase(ITestOutputHelper output) : base(output) { }

		protected override void ClickProductDescription()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(ProductDetail.ProductDescId.ToCssIdSelector()));
            Browser.ScrollIntoView(ProductDetail.ProductDescDropDown);
            Browser.ExecuteJs("window.scrollBy(0,-75)");
            ProductDetail.ProductDescDropDown.Click();
		}
	}


	public abstract class T263_T478_Base : ProductDetailTestsBase
    {        
        protected T263_T478_Base(ITestOutputHelper output) : base(output) { }
        
        protected void Validate(string config)
        {
            InitializeFramework(config);

            var shortSku = ProductActions.GetFanWithEnergyGuideIconShortSku;

            Assert.DatabaseObject(shortSku, "ProductActions.GetFanWithEnergyGuideIconShortSku()");

            ProductDetail.NavigateToProductDetailByShortSku(shortSku);

            ClickProductDescription();

            Browser.Wait.IsVisibleElement(By.CssSelector(ProductDetail.EnergyGuideIconId.ToCssIdSelector()));

            Assert.Displayed(Browser.Wait.ForDisplayedElement(ProductDetail.EnergyGuideIcon), "Energy Guide Icon did not displayed");

            ProductDetail.EnergyGuideIcon.Click();

            Assert.Displayed(Browser.Wait.ForDisplayedElement(ProductDetail.EnergyInfoModal), "Energy Guide Modal Did not Pop Up");
        }

	    protected abstract void ClickProductDescription();
    }
}
