using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Environment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.CartOverview.T120_VerifyProductMarginUserEditLink
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.CartOverview)]
    //[Collection(LpTraits.UserRole.Employee)]
    public class T120_Windows_VerifyProductMarginUserEditLink : T120_DesktopBase
    {
        public T120_Windows_VerifyProductMarginUserEditLink(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void ProductMarginUserEditLink(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T120_Mac_VerifyProductMarginUserEditLink : T120_DesktopBase
    {
        public T120_Mac_VerifyProductMarginUserEditLink(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void VerifyAPopUpIsDisplayedWhenAnEmployeeEntersAPriceBelowUmrp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T120_iPad_VerifyProductMarginUserEditLink : T120_DesktopBase
    {
        public T120_iPad_VerifyProductMarginUserEditLink(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void VerifyAPopUpIsDisplayedWhenAnEmployeeEntersAPriceBelowUmrp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T120_TabletEmulator_VerifyProductMarginUserEditLink : T120_DesktopBase
    {
        public T120_TabletEmulator_VerifyProductMarginUserEditLink(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void VerifyAPopUpIsDisplayedWhenAnEmployeeEntersAPriceBelowUmrp(string config) => Validate(config);
    }

    /// <summary>
    /// Verify the edit link below a price has margin information for certain roles
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9926
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T120 
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop),
     Trait(LpTraits.Keys.Category, LpTraits.Categories.CRUD)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9926"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T120")]
    public abstract class T120_DesktopBase : TestsBaseDesktop
    {
        protected T120_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            // Arrange : Use the Manager employee credential for login and add item to cart 
            var setup = new TestSetup(config, useEmployeeManagerAccount: true);
            InitializeFunctionalTest(config, setup: setup);
            ShoppingCartWorkflow.EmptyCart();
            var shortSku = ProductActions.GetAnySkuWithProductDetailPage;
            Assert.DatabaseObject(shortSku, "ProductActions.GetAnySkuWithProductDetailPage");
            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel(shortSku));

            // Act : Open the edit Price Modal
            Cart.OpenEditPriceModal();

            // Assert : Verify that Margin field is displayed 
            Assert.True(Cart.IsMarginDisplayedOnEditPriceModal, "Margin field is not displayed.");
            Assert.True(!string.IsNullOrEmpty(Cart.GetMarginTextValue()), "Text margin Value is null");
        }
    }
}