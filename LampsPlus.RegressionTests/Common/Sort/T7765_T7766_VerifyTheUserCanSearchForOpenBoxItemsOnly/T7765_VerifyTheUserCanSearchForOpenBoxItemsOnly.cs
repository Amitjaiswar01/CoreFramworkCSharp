using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Sort.T7765_T7766_VerifyTheUserCanSearchForOpenBoxItemsOnly
{
    /// <summary>
    /// Verify search term persists on open box search field
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10204
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7765
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10204"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7765")]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7765_Windows_VerifyTheUserCanSearchForOpenBoxItemsOnly : TestsBaseDesktop
    {
        public T7765_Windows_VerifyTheUserCanSearchForOpenBoxItemsOnly(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        protected void VerifytheUserCanSearchforOpenBoxItemsOnly(string config)
        {
            /*Arrangement
              User is on the following page: https://www.lampsplus.com/products/openbox_view-open-box-items/
            */
            InitializeFunctionalTest(config, Urls.LampsPlusOpenBoxLinkFromSaleMenuUrl);

            /*Assert
             Verify all the sort page results have the callout "Open Box"
             Verify the breadcrumbs include "View Open Box Items"
            */
            Assert.True(Sort.DoesSortPageResultHaveOpenBoxCallout(), "All the sort page results don't have the callout 'Open Box'.");
            Assert.Equals(Sort.GetViewOpenBoxText(), Sort.GetBreadCrumbElementText(), "The breadcrumbs do not include 'View Open Box Items'.");

            /*Act
             Search Ramdom categories
            */
            Sort.SearchForCategory();

            /*Assert
             Verify the search term persists in the search field after searching
            */
            Assert.True(Sort.DoesSortPageResultHaveOpenBoxCallout(), "All the sort page results don't have the callout 'Open Box'.");
        }
    }
}