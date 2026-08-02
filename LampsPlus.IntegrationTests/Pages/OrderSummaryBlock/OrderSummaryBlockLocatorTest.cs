using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.IntegrationTests.Pages.OrderSummaryBlock
{
    /// <summary>
    /// Tests to ensure all IElements and Lists of IElements can be found in the Order Summary Block.
    /// </summary>
    [Trait(LpTraits.Integration.PageObjectModel, "OrderSummaryBlock")]
    public class OrderSummaryBlockLocatorTest : PageObjectTestsBase
    {
        /// <summary>
        /// Tests to ensure this page can find all its IElements.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public OrderSummaryBlockLocatorTest(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the requested Order Summary Block elements could be located on the given page.
        /// </summary>
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void LocateOrderSummaryBlockElementsTest(string config)
        {
            InitializeFramework(config, Urls.HomePageUrl);
            BuildElementsList(OrderSummaryBlock);

            Home.EnterStoreInSession("12");
            Home.StoreNumberField.SendKeys(Keys.Enter);

            ShoppingCartWorkflow.AddSingleItemToCart();

            CartOverview.RemoveProfessionalAccount();
            
            Home.ClearStoreInSession();
            Home.StoreNumberField.SendKeys(Keys.Enter);

            Browser.Locate.ClickDropdownByValue(CsrBlock.SaleSourceField, "1");
            CartOverview.CheckOutNowButton.Click();
            Browser.Wait.ForPage(Urls.ShippingPageUrl);
            WaitForGlobalSpinnerToClose();
            ShoppingCartWorkflow.EnterDefaultShippingAddress();

            VerifyElementDisplayed(() => OrderSummaryBlock.ProceedToPaymentButton);

            ShoppingCartWorkflow.ProceedToPayment();

            VerifyElementDisplayed(() => OrderSummaryBlock.EditOrderButton);
            VerifyElementDisplayed(() => OrderSummaryBlock.OrderSummaryBlockContainer);
            VerifyElementDisplayed(() => OrderSummaryBlock.OrderSummaryElement);
            VerifyElementDisplayed(() => OrderSummaryBlock.OrderTotal);
            VerifyElementDisplayed(() => OrderSummaryBlock.OrderTotalValue);
            VerifyElementDisplayed(() => OrderSummaryBlock.ProductTotalLabel);
            VerifyElementDisplayed(() => OrderSummaryBlock.ProductTotalValue);
            VerifyElementDisplayed(() => OrderSummaryBlock.PosProductTotal);
            VerifyElementDisplayed(() => OrderSummaryBlock.TaxLabel);
            VerifyElementDisplayed(() => OrderSummaryBlock.TaxValue);
            VerifyElementDisplayed(() => OrderSummaryBlock.ShippingAndProcessingLabel);
            VerifyElementDisplayed(() => OrderSummaryBlock.ShippingAndProcessingValue);
        }
    }
}
