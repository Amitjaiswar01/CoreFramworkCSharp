using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Environment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.WarmUpTests.FooterWarmUpTest
{
    public class T7500_WarmUpElementsAndPagesRelatedToTheFooter : T7500_DesktopBase
    {
        public T7500_WarmUpElementsAndPagesRelatedToTheFooter(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void WarmUpTestForFooter(string config) => Validate(config);
    }


    /// <summary>
    /// Warm up elements and pages related to the Footer.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8399
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7500
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8399"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7500")]
    public abstract class T7500_DesktopBase : TestsBase
    {
        protected T7500_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            var setup = new TestSetup(config);
            InitializeFramework(config, setup: setup);

            Browser.OpenNewTab(Urls.AboutUsPageUrl);
            Browser.OpenNewTab(Urls.StoresPageUrl);
            Browser.OpenNewTab(Urls.CareersPageUrl);
            Browser.OpenNewTab(Urls.LightingDesignServicesPageUrl);
            Browser.OpenNewTab(Urls.ProfessionalsPageUrl);
            Browser.OpenNewTab(Urls.HospitalityPageUrl);
            Browser.OpenNewTab(Urls.HelpAndPoliciesPageUrl);
            Browser.OpenNewTab(Urls.ContactUsPageUrl);
            Browser.OpenNewTab(Urls.OrderHistoryPageUrl);
            Browser.OpenNewTab(Urls.ReturnsPolicyPageUrl);
            Browser.OpenNewTab(Urls.IdeasAdviceUrlProd);
            Browser.OpenNewTab(Urls.CatalogsPageUrl);
            Browser.OpenNewTab(Urls.GiftCardLandingPageUrl);
            Browser.OpenNewTab(Urls.ManageAccountPageUrl);
            Browser.OpenNewTab(Urls.NewHomeOwnerPageUrl);
            Browser.OpenNewTab(Urls.ContactUsPageEmailUrl);
            Browser.OpenNewTab(Urls.TermsOfUsePageUrl);
            Browser.OpenNewTab(Urls.AccessibilityPageUrl);
            Browser.OpenNewTab(Urls.PrivacyPolicyPageUrl);
            Browser.OpenNewTab(Urls.SiteMapPageUrl);
            Browser.OpenNewTab(Urls.SeeOurPolicyUrl);
            Browser.OpenNewTab(Urls.ShippingPolicyPageUrl);
        }
    }
}
