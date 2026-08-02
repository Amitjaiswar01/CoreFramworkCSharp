using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;
using xRetry;

namespace LampsPlus.RegressionTests.Common.Certona
{
    //[Collection(LpTraits.BatchGroup.Common.Certona)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Certona)]
	public class T318_Windows_VerifyCertonaSchemaForSortPage : T318_DesktopBase
	{
		public T318_Windows_VerifyCertonaSchemaForSortPage(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
		[SkippableTheory]
		[InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
		public void VerifyCertonaSchemaForSortPage(string config) => Validate(config);
	}


    //[Collection(LpTraits.BatchGroup.Common.Certona)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Certona)]
    public class T318_Mac_VerifyCertonaSchemaForSortPage : T318_DesktopBase
    {
        public T318_Mac_VerifyCertonaSchemaForSortPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI)]
        public void VerifyCertonaSchemaForSortPage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Certona)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Certona)]
    public class T318_iPad_VerifyCertonaSchemaForSortPage : T318_DesktopBase
    {
        public T318_iPad_VerifyCertonaSchemaForSortPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI)]
        public void VerifyCertonaSchemaForSortPage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Certona)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Certona)]
    public class T318_TabletEmulator_VerifyCertonaSchemaForSortPage : T318_DesktopBase
    {
        public T318_TabletEmulator_VerifyCertonaSchemaForSortPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI)]
        public void VerifyCertonaSchemaForSortPage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.Certona)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Certona)]
	public class T513_iPhone_VerifyCertonaSchemaForSortPage : T513_MobileBase
	{
		public T513_iPhone_VerifyCertonaSchemaForSortPage(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
		public void VerifyCertonaSchemaForSortPage(string config) => Validate(config);
	}


    //[Collection(LpTraits.BatchGroup.Common.Certona)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Certona)]
	public class T513_Emulator_VerifyCertonaSchemaForSortPage : T513_MobileBase
	{
		public T513_Emulator_VerifyCertonaSchemaForSortPage(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
		[SkippableTheory]
		[InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
		public void VerifyCertonaSchemaForSortPage(string config) => Validate(config);
	}


    /// <summary>
    /// Verify the correct scheme is being called to populate the certona widgets on the Sort Page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5354
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T318
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5354"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T318")]
    public abstract class T318_DesktopBase : T318_T513_Base
	{
		protected T318_DesktopBase(ITestOutputHelper output) : base(output) { }
	}


	/// <summary>
	/// Verify the correct scheme is being called to populate the certona widgets on the Sort Page.
	/// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-6479
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T513
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-6479"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T513")]
    public abstract class T513_MobileBase : T318_T513_Base
	{
		protected T513_MobileBase(ITestOutputHelper output) : base(output) { }

        protected override void Validate(string config)
        {
            InitializeFramework(config);

            CertonaUtilities.VisitMultiplePages();

            Browser.Navigate(Urls.AllChandeliersSortPageUrl);

            Browser.Wait.IsVisibleElement(By.CssSelector(Sort.ToggleSortMenuClass.ToCssClassSelector()));

            Assert.Displayed(Sort.MymlSection, "MYML section not displayed on sort page.");
            Assert.Displayed(Sort.RecentlyViewedSection, "Recently Viewed section not displayed on sort page.");
            Assert.True(Sort.MymlItem.GetAttribute("data-certonasku") != string.Empty, "No SKU display in MYML section.");
            Assert.True(Sort.RecentlyViewedItem.GetAttribute("data-qa-sku-source") != string.Empty, "No SKU displayed in Recently Viewed section.");
        }

    }


    //[Collection(LpTraits.UserRole.Customer)]
    public abstract class T318_T513_Base : TestsBase 
	{
		protected T318_T513_Base(ITestOutputHelper output) : base(output) { }
		
		protected virtual void Validate(string config)
        {
            InitializeFramework(config);

            CertonaUtilities.VisitMultiplePages();  

            Browser.Navigate(Urls.AllChandeliersSortPageUrl);

            Browser.Wait.IsVisibleElement(By.CssSelector(Sort.SortFilterDisplaySetDropdownsClass.ToCssClassSelector()));
            
            Assert.Displayed(Sort.MymlSection, "MYML section not displayed on sort page.");
            Assert.Displayed(Sort.RecentlyViewedSection, "Recently Viewed section not displayed on sort page.");
            Assert.True(Sort.MymlItem.GetAttribute("data-certonasku") != string.Empty, "No SKU display in MYML section.");
            Assert.True(Sort.RecentlyViewedItem.GetAttribute("data-qa-sku-source") != string.Empty, "No SKU displayed in Recently Viewed section.");
        }
    }
}
