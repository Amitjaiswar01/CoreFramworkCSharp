using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.Sort.T7745_VerifyPresenceOfLampsPlusChoiceCalloutOnSortPage
{
    //[Collection(LpTraits.BatchGroup.Desktop.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Sort)]
    public class T7745_Windows_VerifyPresenceOfLampsPlusChoiceCalloutOnSortPage : T7745_DesktopBase
    {
        public T7745_Windows_VerifyPresenceOfLampsPlusChoiceCalloutOnSortPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_ElasticSearch)]
        public void VerifyLampsPlusChoiceCallout(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Sort)]
    public class T7745_Mac_VerifyPresenceOfLampsPlusChoiceCalloutOnSortPage : T7745_DesktopBase
    {
        public T7745_Mac_VerifyPresenceOfLampsPlusChoiceCalloutOnSortPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyLampsPlusChoiceCallout(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Sort)]
    public class T7745_iPad_VerifyPresenceOfLampsPlusChoiceCalloutOnSortPage : T7745_DesktopBase
    {
        public T7745_iPad_VerifyPresenceOfLampsPlusChoiceCalloutOnSortPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyLampsPlusChoiceCallout(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Sort)]
    public class T7745_TabletEmulator_VerifyPresenceOfLampsPlusChoiceCalloutOnSortPage : T7745_DesktopBase
    {
        public T7745_TabletEmulator_VerifyPresenceOfLampsPlusChoiceCalloutOnSortPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyLampsPlusChoiceCallout(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the presence of the 'Lamps Plus Choice' callout on the Sort page
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10089
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7745
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10089"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7745")]
    public abstract class T7745_DesktopBase : TestsBaseDesktop
    {
        protected T7745_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            // Arrange : Identify a qualifying item.
            InitializeFunctionalTest(config);

            var product = ProductActions.GetLampsPlusChoiceSku();
            // Act: Navigate to the PDP by the Sku
            ProductDetail.NavigateToProductDetailByShortSku(product.ShortSku);

            // Arrange : On the PDP, make note of SKU and Price
            var price = ProductDetail.GetProductPrice();
            var sku = product.ShortSku;

            // Act : Search for the chosen Sku by constructing Url
            Sort.SearchLampsPlusChoiceProduct(product, price);

            //Assert : Verify Lamps Plus Choice Badge displays and Sku matches
            Assert.True(Sort.DoesLampsPlusChoiceBadgeDisplay(), "Lamps Plus Choice badge is not displayed");
            Assert.True(Sort.DoesSkuExistOnSortPage(sku), $"Sku '{sku}' was NOT FOUND on any sort pages");
        }
    }
}