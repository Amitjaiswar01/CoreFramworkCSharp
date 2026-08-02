using Xunit;
using Xunit.Abstractions;
using xRetry;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Certona.T314_T507_VerifyCertonaSchemaForHomepage
{
    //[Collection(LpTraits.BatchGroup.Mobile.Certona)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Certona)]
    public class T507_iPhone_VerifyCertonaSchemaForHomepage : T507_MobileBase
    {
        public T507_iPhone_VerifyCertonaSchemaForHomepage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifyCertonaSchemaForHomepage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.Certona)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Certona)]
    public class T507_Emulator_VerifyCertonaSchemaForHomepage : T507_MobileBase
    {
        public T507_Emulator_VerifyCertonaSchemaForHomepage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyCertonaSchemaForHomepage(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the correct schemes are being called to populate the Certona widgets on the home page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-6487
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T507
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Certona)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    //[Collection(LpTraits.BatchGroup.Common.Certona)
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-6487"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T507")]
    public class T507_MobileBase : TestsBaseMobile
    {
        protected T507_MobileBase(ITestOutputHelper output) : base(output) { }

        protected virtual void Validate(string config)
        {
            InitializeFunctionalTest(config);

            // Arrangement: User has visited the PDP for 4 items.
            CertonaWorkflow.VisitMultiplePages(4);

            // Act: Navigate to Home Page
            Browser.Navigate(Urls.HomePageUrl);

            // Assert: Check Recently Viewed widget showing or not. 
            Assert.True(Home.IsRecentlyViewedWidgetVisible, "The 'Recently Viewed' widget is not displayed on Home Page.");

            // Assert: Check Recently Viewed Section not empty.
            Assert.False(string.IsNullOrWhiteSpace(Home.GetCertonaWidgetSku()), "No SKU displayed in Recently Viewed Section.");
        }
    }
}
