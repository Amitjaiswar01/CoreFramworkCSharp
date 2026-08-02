using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.DesktopTablet.CartOverview
{
    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.CartOverview)]
    public class T113_Windows_VerifyEmpNotAllowedToAddSku99999 : T113_DesktopBase
    {
        public T113_Windows_VerifyEmpNotAllowedToAddSku99999(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void EmpNotAllowedToAddSku99999(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.CartOverview)]
    public class T113_Mac_VerifyEmpNotAllowedToAddSku99999 : T113_DesktopBase
    {
        public T113_Mac_VerifyEmpNotAllowedToAddSku99999(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void EmpNotAllowedToAddSku99999(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.CartOverview)]
    public class T113_iPad_VerifyEmpNotAllowedToAddSku99999 : T113_DesktopBase
    {
        public T113_iPad_VerifyEmpNotAllowedToAddSku99999(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void EmpNotAllowedToAddSku99999(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.CartOverview)]
    public class T113_TabletEmulator_VerifyEmpNotAllowedToAddSku99999 : T113_DesktopBase
    {
        public T113_TabletEmulator_VerifyEmpNotAllowedToAddSku99999(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_ESI)]
        public void EmpNotAllowedToAddSku99999(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that an employee is NOT allowed to add SKU 99999 through 'Add an Item'.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5240
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T113 
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5240"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T113")]
    public abstract class T113_DesktopBase : ShoppingCartTestsBase
    {
        protected T113_DesktopBase(ITestOutputHelper output) : base(output) { }
        
        protected void Validate(string config)
        {
            const string sku = "99999";

            InitializeFramework(config);

            var randomSku = ProductActions.GetAnySkuWithProductDetailPage;

            Assert.DatabaseObject(sku, "ProductActions.GetAnySkuWithProductDetailPage");

            ShoppingCartWorkflow.AddItemToCartBySku(new ProductModel { Sku = randomSku });

            var cartProductsBefore = CartOverview.GetListOfAllProductsOnPage();

            CartOverview.AddShortSkuElement.SendKeys(sku);
            CartOverview.AddSkuLinkElement.Click();

            var cartProductsAfter = CartOverview.GetListOfAllProductsOnPage();

            Assert.Equals("Please specify a SKU other than 99999.", CartOverview.CartErrorModalElement.Text, "User did not receive the message.Please specify a SKU other than 99999.");
            VerifyListsAreEqual(cartProductsBefore, cartProductsAfter);
        }
    }
}
