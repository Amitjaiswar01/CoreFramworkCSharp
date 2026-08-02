using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Sort.T212_T450_VerifyCanonicalOrderingOfSortFilterAttributes
{
    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T212_Windows_VerifyCanonicalOrderingOfSortFilterAttributes : T212_DesktopBase
    {
        public T212_Windows_VerifyCanonicalOrderingOfSortFilterAttributes(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_ElasticSearch)]
        public void CanonicalOrderingOfSortFilterAttributes(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T212_Mac_VerifyCanonicalOrderingOfSortFilterAttributes : T212_DesktopBase
    {
        public T212_Mac_VerifyCanonicalOrderingOfSortFilterAttributes(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void CanonicalOrderingOfSortFilterAttributes(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T212_iPad_VerifyCanonicalOrderingOfSortFilterAttributes : T212_DesktopBase
    {
        public T212_iPad_VerifyCanonicalOrderingOfSortFilterAttributes(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void CanonicalOrderingOfSortFilterAttributes(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T212_TabletEmulator_VerifyCanonicalOrderingOfSortFilterAttributes : T212_DesktopBase
    {
        public T212_TabletEmulator_VerifyCanonicalOrderingOfSortFilterAttributes(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void CanonicalOrderingOfSortFilterAttributes(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the Sort page breadcrumb trail matches URL order for 'in order' attributes.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10076
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T212 
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10076"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T212")]
    public abstract class T212_DesktopBase : TestsBaseDesktop
    {
        protected T212_DesktopBase(ITestOutputHelper output) : base(output) { }

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

            Sort.ExpandSortPageBreadcrumbList();

            // Assert: The breadcrumb trail's order of attributes should be: Table lamps / finish / color / type / price
            Assert.StringContains(Sort.GetBreadcrumbHomeLink(), "/", "Bread crumb does not contain home link.");
            Assert.Equals(Sort.TableLampsString, Sort.GetIndividualBreadcrumbNames(0), "Bread crumb does not match the text table lamps.");
            Assert.Equals(selectedFinish, Sort.GetIndividualBreadcrumbNames(1), "Bread crumb does not match the text finish.");
            Assert.Equals(selectedColor, Sort.GetIndividualBreadcrumbNames(2), "Bread crumb does not match the text color.");
            Assert.Equals(selectedType, Sort.GetIndividualBreadcrumbNames(3), "Bread crumb does not match the text accent");
            Assert.Equals(selectedPrice, Sort.GetIndividualBreadcrumbNames(4), "Bread crumb does not match the price range.");

            var breadcrumbSelectedFilters = Sort.GetBreadCrumbText(false).Trim();
            
            // Act: Click on the first product on the Sort page. Once the PDP loads, compare the breadcrumb trail on the PDP to the Sort page.
            Sort.SelectFirstProductOnSortPage();
            Assert.True(ProductDetail.IsCurrentPage, "User is not on PDP.");

            // Assert: The breadcrumb trails match between the Sort page and PDP (not including the Style # on the PDP).
            Assert.Equals(breadcrumbSelectedFilters, ProductDetail.GetBreadcrumbText(), "Sort and PDP breadcrumbs do not match");
        }
    }
}