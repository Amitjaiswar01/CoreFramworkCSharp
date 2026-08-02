using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.Sort.T213_VerifyCustomPriceRange
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Sort)]
    //[Collection(LpTraits.BatchGroup.Desktop.Sort)]
    public class T213_Windows_VerifyCustomPriceRangeFunctionality : T213_DesktopBase
    {
        public T213_Windows_VerifyCustomPriceRangeFunctionality(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_ElasticSearch)]
        public void VerifyCustomPriceRangeFunctionality(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Sort)]
    //[Collection(LpTraits.BatchGroup.Desktop.Sort)]
    public class T213_Mac_VerifyCustomPriceRangeFunctionality : T213_DesktopBase
    {
        public T213_Mac_VerifyCustomPriceRangeFunctionality(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyCustomPriceRangeFunctionality(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Sort)]
    //[Collection(LpTraits.BatchGroup.Desktop.Sort)]
    public class T213_iPad_VerifyCustomPriceRangeFunctionality : T213_DesktopBase
    {
        public T213_iPad_VerifyCustomPriceRangeFunctionality(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyCustomPriceRangeFunctionality(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Sort)]
    //[Collection(LpTraits.BatchGroup.Desktop.Sort)]
    public class T213_TabletEmulator_VerifyCustomPriceRangeFunctionality : T213_DesktopBase
    {
        public T213_TabletEmulator_VerifyCustomPriceRangeFunctionality(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyCustomPriceRangeFunctionality(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the functionality of the 'Custom Price Range' option in the 'Price' attribute dropdown.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10096
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T213        
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10096"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T213")]
    public abstract class T213_DesktopBase : TestsBaseDesktop
    {
        protected T213_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange : Navigate to Sort page for any product category
            InitializeFunctionalTest(config);

            Browser.Navigate(Urls.CrystalChandeliersUrl);

            /*Act :
            Locate custom price range option in 'Price' attribute filter.
            Enter a value in the 'Min' and 'Max' fields and execute the search.
             */
            var minPrice = "500";
            var maxPrice = "1000";

            decimal.TryParse(minPrice, out var minPriceExpectedValue);
            decimal.TryParse(maxPrice, out var maxPriceExpectedValue);

            Sort.ApplyCustomPrice(minPriceExpectedValue, maxPriceExpectedValue);

            Assert.NotNull(Sort.GetListOfSaleProducts(), "Unable to get a list of sale products. Check the results. This could be a timing issue.");

            //Assert : Verify the sort page show products falling within that price range
            foreach (var saleProduct in Sort.GetListOfSaleProducts())
            {
                var itemPriceString = saleProduct.Text.Replace("$", string.Empty);

                if (itemPriceString.Contains(" "))
                {
                    itemPriceString = itemPriceString.Substring(0, itemPriceString.IndexOf(' '));
                }

                decimal.TryParse(itemPriceString, out var itemPriceValue);

                Assert.True(minPriceExpectedValue < itemPriceValue || itemPriceValue < maxPriceExpectedValue, $"{itemPriceValue} does not fall within the filtered range {minPrice} - {maxPrice}");
            }
        }
    }
}