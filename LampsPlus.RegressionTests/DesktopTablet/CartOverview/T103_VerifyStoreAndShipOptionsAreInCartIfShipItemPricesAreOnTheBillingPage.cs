using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Environment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.DesktopTablet.CartOverview
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.CartOverview)]
    //[Collection(LpTraits.UserRole.EmployeeKiosk)]
    public class T103_Windows_VerifyStoreShipOptionsBillingPage : T103_DesktopBase
    {
        public T103_Windows_VerifyStoreShipOptionsBillingPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_ESI)]
        public void StoreShipOptionsBillingPage(string config) => Validate(config);
    }


    /// <summary>
    /// Verify if 'In Store' and 'Ship' options are in cart, only ship item prices are on billing page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5046
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T103
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop), Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen), Trait(LpTraits.Keys.Category, LpTraits.Categories.CRUD)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5046"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T103")]
    public abstract class T103_DesktopBase : ShoppingCartTestsBase
    {
        protected T103_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            var setup = new TestSetup(config) { AccountConfig = { StoreInSessionStoreNumber = "12" } };
            InitializeFramework(config, setup: setup);

            ShoppingCartWorkflow.AddMultipleItemsToCart(Urls.AllChandeliersSortPageUrl, 2);

            Browser.Wait.ForDisplayedElement(CsrBlock.CsrPanelElement);
            Browser.Wait.ForDisplayedElement(CartOverview.CheckOutNowButton);

            Browser.Locate.ClickDropdownByValue(CsrBlock.SaleSourceField, "1");
            CartOverview.PosCheckBoxAndLabel.Click();

            OrderSummaryBlock.WaitForKioskPriceUpdate();

            var kioskTotal = OrderSummaryBlock.OrderSummaryBlockElement(7).Text;

            CartOverview.ClickCheckOutNowButton();

            Browser.Wait.IsVisibleElement(By.CssSelector(Shipping.ProceedPaymentId.ToCssIdSelector()));

            ShoppingCartWorkflow.EnterDefaultShippingAddress();
            CustomerAddressInformation.ProceedToPaymentButton.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(Payment.PlaceYourOrderButtonId.ToCssIdSelector()));

            Assert.Equals(TextActions.FormatPrice(kioskTotal), CartOverview.GetProductTotal(), "Order total not equal to the kiosk ship only items total in cart.");
        }
    }
}
