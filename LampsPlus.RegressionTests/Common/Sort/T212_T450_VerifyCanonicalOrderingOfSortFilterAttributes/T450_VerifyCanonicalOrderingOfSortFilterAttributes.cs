using System.Web.UI;
using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using xRetry;

namespace LampsPlus.RegressionTests.Common.Sort.T212_T450_VerifyCanonicalOrderingOfSortFilterAttributes
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T450_iPhone_VerifyCanonicalOrderingOfSortFilterAttributes : T450_MobileBase
    {
        public T450_iPhone_VerifyCanonicalOrderingOfSortFilterAttributes(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T450. Rework - ACD-10924")]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_ElasticSearch)]
        public void CanonicalOrderingOfFilterAttributes(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T450_Emulator_VerifyCanonicalOrderingOfSortFilterAttributes : T450_MobileBase
    {
        public T450_Emulator_VerifyCanonicalOrderingOfSortFilterAttributes(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void CanonicalOrderingOfFilterAttributes(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the Sort page breadcrumb trail matches URL order for 'in order' attributes.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10076
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T450
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10076"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T450")]
    public abstract class T450_MobileBase : TestsBaseMobile
    {
        protected T450_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            // Arrange: User is on https://www.lampsplus.com/products/TABLE-LAMPS/
            InitializeFunctionalTest(config, Urls.TableLampsSortPageUrl);
            Assert.True(Sort.IsCurrentPage, "User is not on a Sort page.");

            // Act: Select filter attributes ‘in-order’. Select the first option for each filter. Compare the breadcrumb trail on the Sort page to order they were selected in.
            var filterOptions = Sort.ApplyFilters(0, true);
            var selectedFinish = filterOptions[0][Sort.FinishString];
            var selectedColor = filterOptions[0][Sort.ColorString];
            var selectedType = filterOptions[0][Sort.TypeString];
            var selectedPrice = filterOptions[0][Sort.PriceString];

            Assert.True(Sort.IsCurrentPage, "User is not on a Sort page.");

            // Assert: The breadcrumb trail's order of attributes should be: Table lamps / finish / color / type.
            Assert.StringContains(Sort.GetEntireBreadcrumbTrail()[0].GetAttribute(HtmlTextWriterAttribute.Href.ToString()), "/", "Bread crumb does not contain home link.");
            Assert.Equals(Sort.TableLampsString, Sort.GetIndividualBreadcrumbNames(0), "Bread crumb does not match the text table lamps.");
            Assert.Equals(selectedFinish, Sort.GetIndividualBreadcrumbNames(1), "Bread crumb does not match the text finish.");
            Assert.Equals(selectedColor, Sort.GetIndividualBreadcrumbNames(2), "Bread crumb does not match the text color.");
            Assert.Equals(selectedType, Sort.GetIndividualBreadcrumbNames(3), "Bread crumb does not match the text accent");

            var sortBreadcrumb = Sort.GetBreadCrumbText(false);

            // Act: Click on the first product on the Sort page. Once the PDP loads, compare the breadcrumb trail on the PDP to the Sort page.
            Sort.SelectFirstProductOnSortPage();
            Assert.True(ProductDetail.IsCurrentPage, "User is not on PDP");

            var pdpBreadcrumbs = ProductDetail.GetBreadcrumbText();

            Assert.True(pdpBreadcrumbs.Contains(sortBreadcrumb), "PDP breadcrumb trail does not contain Sort Page breadcrumbs.");
            Assert.True(pdpBreadcrumbs.Contains(selectedPrice), "PDP breadcrumb trail does not contain Price filter value.");
        }
    }
}
