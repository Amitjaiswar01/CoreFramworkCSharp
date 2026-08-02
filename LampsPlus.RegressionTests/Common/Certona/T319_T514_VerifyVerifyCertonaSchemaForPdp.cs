using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;
using xRetry;

namespace LampsPlus.RegressionTests.Common.Certona
{
    //[Collection(LpTraits.BatchGroup.Common.Certona)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Certona)]
	public class T319_Windows_VerifyCertonaSchemaForPdp : T319_DesktopBase
	{
		public T319_Windows_VerifyCertonaSchemaForPdp(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
		public void VerifyCertonaSchemaForPdp(string config) => Validate(config);
	}


    //[Collection(LpTraits.BatchGroup.Common.Certona)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Certona)]
    public class T319_Mac_VerifyCertonaSchemaForPdp : T319_DesktopBase
    {
        public T319_Mac_VerifyCertonaSchemaForPdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI)]
        public void VerifyCertonaSchemaForPdp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Certona)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Certona)]
    public class T319_iPad_VerifyCertonaSchemaForPdp : T319_DesktopBase
    {
        public T319_iPad_VerifyCertonaSchemaForPdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [Theory(Skip = "Bug - LP-60441")]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI)]
        public void VerifyCertonaSchemaForPdp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Certona)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Certona)]
    public class T319_TabletEmulator_VerifyCertonaSchemaForPdp : T319_DesktopBase
    {
        public T319_TabletEmulator_VerifyCertonaSchemaForPdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [Theory(Skip = "Bug - LP-60441")]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI)]
        public void VerifyCertonaSchemaForPdp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.Certona)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Certona)]
	public class T514_iPhone_VerifyCertonaSchemaForPdp : T514_MobileBase
	{
		public T514_iPhone_VerifyCertonaSchemaForPdp(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
		[InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
		public void VerifyCertonaSchemaForPdp(string config) => Validate(config);
	}


    //[Collection(LpTraits.BatchGroup.Common.Certona)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Certona)]
	public class T514_Emulator_VerifyCertonaSchemaForPdp : T514_MobileBase
	{
		public T514_Emulator_VerifyCertonaSchemaForPdp(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
		[SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
		public void VerifyCertonaSchemaForPdp(string config) => Validate(config);
	}


	/// <summary>
	/// Verify the correct scheme is being called to populate the certona widgets on the PDP.
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5335
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T319
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5335"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T319")]
    public abstract class T319_DesktopBase : T319_T514_Base
	{
		protected T319_DesktopBase(ITestOutputHelper output) : base(output) { }
    }


	/// <summary>
	/// Verify the correct scheme is being called to populate the certona widgets on the PDP.
	/// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-6480
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T514
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-6480"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T514")]
    public abstract class T514_MobileBase : T319_T514_Base
	{
		protected T514_MobileBase(ITestOutputHelper output) : base(output) { }
    }


    //[Collection(LpTraits.UserRole.Customer)]
    public abstract class T319_T514_Base : TestsBase 
	{
		protected T319_T514_Base(ITestOutputHelper output) : base(output) { }
		
		protected void Validate(string config)
		{
		    InitializeFramework(config);

		    CertonaUtilities.VisitMultiplePages();

            var shortSku = ProductActions.GetShortSkuThatHasLessThanOrEqualToTenCoordinatingProducts();
            Assert.DatabaseObject(shortSku, "ProductActions.GetShortSkuThatHasLessThanOrEqualToTenCoordinatingProducts()");

            ProductDetail.NavigateToProductDetailByShortSku(shortSku);

		    Assert.Displayed(ProductDetail.PdMymlSection, "More You May Like section not displayed on product detailed page.");
            Assert.Displayed(ProductDetail.RelatedItemSection, "Related Items section not displayed on product detailed page.");
		    Assert.Displayed(Sort.RecentlyViewedSection, "Recently Viewed section not displayed on product detailed page.");
            Browser.ScrollToBottomOfPageJs();
            Assert.False(string.IsNullOrWhiteSpace(ProductDetail.PdMymlSectionItem.GetAttribute("data-certonasku")), "No SKU displayed in More You May Like section.");
            Assert.False(string.IsNullOrWhiteSpace(ProductDetail.RelatedItemSku), "No SKU displayed in Related Item section.");
		    Assert.False(string.IsNullOrWhiteSpace(ProductDetail.PdRecentlyViewedSectionItem.GetAttribute("data-certonasku")), "No SKU displayed in Recently Viewed section.");
		}
    }
}
