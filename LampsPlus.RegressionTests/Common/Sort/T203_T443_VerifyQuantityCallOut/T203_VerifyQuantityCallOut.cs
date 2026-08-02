using System;
using Xunit;
using Xunit.Abstractions;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Sort.T203_T443_VerifyQuantityCallOut
{
    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T203_Windows_VerifyQuantityCallOut : T203_DesktopBase
    {
        public T203_Windows_VerifyQuantityCallOut(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_ElasticSearch)]
        public void QuantityCallOut(string config) => Validate(config);
    }

    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T203_Mac_VerifyQuantityCallOut : T203_DesktopBase
    {
        public T203_Mac_VerifyQuantityCallOut(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void QuantityCallOut(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T203_iPad_VerifyQuantityCallOut : T203_DesktopBase
    {
        public T203_iPad_VerifyQuantityCallOut(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void QuantityCallOut(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T203_TabletEmulator_VerifyQuantityCallOut : T203_DesktopBase
    {
        public T203_TabletEmulator_VerifyQuantityCallOut(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void QuantityCallOut(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the Sort page QTY callout matches the PDP page's QTY dropdown.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10078
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T203
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10078"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T203")]
    public abstract class T203_DesktopBase : TestsBaseDesktop
    {
        protected T203_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            InitializeFunctionalTest(config);

            var productWithQuantityCallOut = ProductActions.GetProductWithCurrentInventory();

            Assert.DatabaseObject(productWithQuantityCallOut, "ProductActions.GetProductWithQuantityCallOut()");

            ProductDetail.NavigateToProductDetailByShortSku(productWithQuantityCallOut.ShortSku);

            var pdpQuantity = ProductDetail.GetProductCallOutQuantity();
            var price = Convert.ToDecimal(TextActions.RemoveDollarSign((ProductDetail.GetProductPriceOnPdp()).Replace("Clearance", "").Replace("Price:\r\n$", string.Empty).Trim()));

            ProductDetail.ClickOnLastBreadcrumb();
            Sort.NavigateToPriceFilteredSortPage(Browser.PageUrl, price);

            var sortQuantity = Sort.GetQuantityLeftForSkuOnSort(productWithQuantityCallOut.ShortSku).Replace("LEFT", "").Trim();

            Assert.True(productWithQuantityCallOut.CurrentInventory == pdpQuantity && pdpQuantity == sortQuantity,
                $"The quantities between the db ({productWithQuantityCallOut.CurrentInventory}), pdp ({pdpQuantity}), and sort ({sortQuantity}) do not match.");
        }
    }
}