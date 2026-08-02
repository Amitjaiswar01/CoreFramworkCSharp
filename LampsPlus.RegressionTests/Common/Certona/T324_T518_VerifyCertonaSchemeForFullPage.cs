using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;
using xRetry;

namespace LampsPlus.RegressionTests.Common.Certona
{
    //[Collection(LpTraits.BatchGroup.Common.Certona)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Certona)]
    public class T324_Windows_VerifySchemesPopulateFullPageCertona : T324_DesktopBase
    {
        public T324_Windows_VerifySchemesPopulateFullPageCertona(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void SchemesPopulateFullPageCertona(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Certona)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Certona)]
    public class T324_Mac_VerifySchemesPopulateFullPageCertona : T324_DesktopBase
    {
        public T324_Mac_VerifySchemesPopulateFullPageCertona(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI)]
        public void SchemesPopulateFullPageCertona(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Certona)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Certona)]
    public class T324_iPad_VerifySchemesPopulateFullPageCertona : T324_DesktopBase
    {
        public T324_iPad_VerifySchemesPopulateFullPageCertona(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [Theory(Skip = "Bug - LP-60441")]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI)]
        public void SchemesPopulateFullPageCertona(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Certona)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Certona)]
    public class T324_TabletEmulator_VerifySchemesPopulateFullPageCertona : T324_DesktopBase
    {
        public T324_TabletEmulator_VerifySchemesPopulateFullPageCertona(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [Theory(Skip = "Bug - LP-60441")]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI)]
        public void SchemesPopulateFullPageCertona(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.Certona)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Certona)]
    public class T518_iPhone_VerifySchemesPopulateFullPageCertona : T518_MobileBase
    {
        public T518_iPhone_VerifySchemesPopulateFullPageCertona(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
        public void SchemesPopulateFullPageCertona(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Certona)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Certona)]
    public class T518_Emulator_VerifySchemesPopulateFullPageCertona : T518_MobileBase
    {
        public T518_Emulator_VerifySchemesPopulateFullPageCertona(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void SchemesPopulateFullPageCertona(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the correct schemes are being called to populate the widgets on full page certona.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5536
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T324
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5536"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T324")]
    public abstract class T324_DesktopBase : T324_T518_Base
    {
        protected T324_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected override void VerifyItemDisplayedInRecentlyViewedSection()
        {
            Assert.False(string.IsNullOrWhiteSpace(Sort.RecentlyViewedContainer.GetAttribute("data-qa-sku-source")), "No SKU displayed in Recently Viewed section section.");
        }
    }


    /// <summary>
    /// Verify the correct schemes are being called to populate the widgets on full page certona.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-6481
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T518
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-6481"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T518")]
    public abstract class T518_MobileBase : T324_T518_Base
    {
        protected T518_MobileBase(ITestOutputHelper output) : base(output) { }

        protected override void VerifyItemDisplayedInRecentlyViewedSection()
        {
            Assert.False(string.IsNullOrWhiteSpace(Sort.RecentlyViewedItem.GetAttribute("data-certonasku")), "No SKU displayed in Recently Viewed section section.");
        }
    }


    public abstract class T324_T518_Base : TestsBase
    {
        protected T324_T518_Base(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            InitializeFramework(config);

            CertonaUtilities.VisitMultiplePages();

            var shortSku = ProductActions.GetListableInStockShortSku();
            Assert.DatabaseObject(shortSku, "ProductActions.GetListableInStockShortSku()");

            Browser.Navigate($"{Urls.ProductFullPageBaseUrl}{shortSku}");

            Assert.Displayed(SortFullPageCertona.FullPageCertonaSimilarDesignsTitleElement, "Similar Design Section not Displayed on SFP page");
            Assert.Displayed(Sort.RecentlyViewedSection, "Recently Viewed section not displayed on SFP page.");
            Assert.False(string.IsNullOrWhiteSpace(SortFullPageCertona.FullPageCertonaItemInSimilarDesignsSection.GetAttribute("data-sku")), "No SKU displayed in Similar Design section.");

            VerifyItemDisplayedInRecentlyViewedSection();
        }

        protected abstract void VerifyItemDisplayedInRecentlyViewedSection();
    }
}
