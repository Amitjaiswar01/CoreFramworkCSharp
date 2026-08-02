using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Environment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.CartOverview.T103_VerifyOnlyShipItemPricesOnOrderSummary
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.CartOverview)]
    public class T103_Windows_VerifyStoreShipOptionsBillingPage : T103_DesktopBase
    {
        public T103_Windows_VerifyStoreShipOptionsBillingPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_ESI)]
        public void StoreShipOptionsBillingPage(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T103_Mac_VerifyStoreShipOptionsBillingPage : T103_DesktopBase
    {
        public T103_Mac_VerifyStoreShipOptionsBillingPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SIS_ESI)]
        public void StoreShipOptionsBillingPage(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T103_iPad_VerifyStoreShipOptionsBillingPage : T103_DesktopBase
    {
        public T103_iPad_VerifyStoreShipOptionsBillingPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SIS_ESI)]
        public void StoreShipOptionsBillingPage(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T103_TabletEmulator_VerifyStoreShipOptionsBillingPage : T103_DesktopBase
    {
        public T103_TabletEmulator_VerifyStoreShipOptionsBillingPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SIS_ESI)]
        public void StoreShipOptionsBillingPage(string config) => Validate(config);
    }


    /// <summary>
    /// Verify if 'In Store' and 'Ship' options are in cart, only ship item prices are on billing page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5046
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T103
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop), Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen), Trait(LpTraits.Keys.Category, LpTraits.Categories.CRUD)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5046"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T103")]
    public abstract class T103_DesktopBase : TestsBaseDesktop
    {
        protected T103_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            /*Arrange:
            As SIS-ESI add 2 items to Cart.
            In the cart, check the POS box for the first item.
            */
            var setup = new TestSetup(config) { AccountConfig = { StoreInSessionStoreNumber = "12" } };
            InitializeFunctionalTest(config, setup: setup);
            const int numberOfProductsToAddToCart = 2;
            ShoppingCartWorkflow.AddMultipleItemsToCart(Urls.TableLampsSortPageUrl, numberOfProductsToAddToCart);
            CsrBlock.SetSaleSourceValue();
            ShoppingCartWorkflow.WaitForOrderStatusKioskPriceUpdate();

            /* Act:
            Get the value from the Order Summary for the Kiosk Product Total.
            Proceed to Shipping Page.
            */
            var kioskTotal = OrderSummaryBlock.GetKioskProductTotalPrice();

            ShoppingCartWorkflow.ProceedToShippingPage();
            Assert.True(Shipping.IsCurrentPage, "User is not on Shipping page.");

            //Assert: On Shipping page, "ORDER SUMMARY" section has Price only for item to be shipped.
            Assert.Equals(kioskTotal, Cart.GetProductTotal(), "Shipping Page Order Summary Order total not equal to the kiosk ship only items total in cart.");

            /*Act:
            Provide shipping information.
            Proceed to payment page.
            */
            CustomerAddressInformation.EnterShippingAddress(Address);
            Shipping.ProceedToPayment();
            Assert.True(Payment.IsCurrentPage, "User is not on Payment page.");

            //Assert: On Payment page, "ORDER SUMMARY" section has Price only for item to be shipped.*/
            Assert.Equals(kioskTotal, Cart.GetProductTotal(), "Payment Page Order Summary Order total not equal to the kiosk ship only items total in cart.");
        }
    }
}
