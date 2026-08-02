using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.CartOverview.T108_VerifyEmployeeCanDeleteCart
{
    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.CartOverview)]
    public class T108_Windows_VerifyEmployeeCanDeleteCart : T108_DesktopBase
    {
        public T108_Windows_VerifyEmployeeCanDeleteCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void VerifyEmployeeCanDeleteCart(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T108_Mac_VerifyEmployeeCanDeleteCart : T108_DesktopBase
    {
        public T108_Mac_VerifyEmployeeCanDeleteCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void VerifyEmployeeCanDeleteCart(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T108_iPad_VerifyEmployeeCanDeleteCart : T108_DesktopBase
    {
        public T108_iPad_VerifyEmployeeCanDeleteCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void VerifyEmployeeCanDeleteCart(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T108_TabletEmulator_VerifyEmployeeCanDeleteCart : T108_DesktopBase
    {
        public T108_TabletEmulator_VerifyEmployeeCanDeleteCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void VerifyEmployeeCanDeleteCart(string config) => Validate(config);
    }


    /// <summary>
    /// Verify a user can delete the cart.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9918
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T108 
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop),
     Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen),
     Trait(LpTraits.Keys.Category, LpTraits.Categories.CRUD)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9918"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T108")]
    public abstract class T108_DesktopBase : TestsBaseDesktop
    {
        protected T108_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            InitializeFunctionalTest(config);

            // Arrange : Add items to cart and check the cart item count
            ShoppingCartWorkflow.AddMultipleItemsToCart(Urls.TableLampssOnSaleUrl, 1);
            Assert.True(Cart.IsCurrentPage, "Current page is not Cart page");
            
            // Act : Delete the cart 
            Cart.DeleteCart();
            Assert.True(Home.IsCurrentPage, "Current page is not home page");
            var cartItemCount = HeaderFooter.CartItemCount;
            const int expectedCartCount = 0;

            // Assert : User should be routed to HomePage and Cart item count not changed.
            Assert.Equals(Urls.HomePageUrl, Browser.PageUrl, "Home url does not match");
            Assert.Equals(expectedCartCount, HeaderFooter.CartItemCount, "Cart count does not match.");
        }
    }
}

