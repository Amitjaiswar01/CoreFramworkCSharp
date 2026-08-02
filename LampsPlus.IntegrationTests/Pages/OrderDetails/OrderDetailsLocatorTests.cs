using System.Linq;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.IntegrationTests.Pages.OrderDetails
{
    public class OrderDetailsLocatorDesktopTest : OrderDetailsLocatorTests
    {
        public OrderDetailsLocatorDesktopTest(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Integration.PageObjectModel, "OrderDetails")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void LocateOrderDetailsElementsTest(string config) => Locate(config);

        protected override void NavigateToOrderDetails()
        {
            OrderHistory.GetOrderPreview(OrderHistory.OrderPreviewElements[0]).OrderIdElement.Click();            

            VerifyElementDisplayed(() => OrderDetails.OrderSummaryContainer);
            VerifyElementDisplayed(() => OrderDetails.OrderIdHeader);
            VerifyElementDisplayed(() => OrderDetails.OrderDateHeader);
            VerifyElementDisplayed(() => OrderDetails.RewardNumber);
        }
    }


    public class OrderDetailsLocatorMobileTest : OrderDetailsLocatorTests
    {
        public OrderDetailsLocatorMobileTest(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Integration.PageObjectModel, "OrderDetails")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void LocateOrderDetailsElementsTest(string config) => Locate(config);

        protected override void NavigateToOrderDetails()
        {
            OrderHistory.GetOrderPreview(OrderHistory.OrderPreviewElements[0]).OrderIdElement.Click();

            VerifyElementDisplayed(() => OrderDetails.OrderSummaryContainer);
	        VerifyElementDisplayed(() => OrderDetails.OrderIdHeader);
	        VerifyElementDisplayed(() => OrderDetails.OrderDateHeader);
            VerifyElementNotImplemented(() => OrderDetails.RewardNumber);
        }
    }


    /// <summary>
    /// Tests to ensure all IElements and Lists of IElements can be found on the Search page.
    /// </summary>
    [Trait(LpTraits.Integration.PageObjectModel, "OrderDetails")]
    public abstract class OrderDetailsLocatorTests : PageObjectTestsBase
    {
        protected OrderDetailsLocatorTests(ITestOutputHelper output) : base(output) { }

        public void Locate(string config)
        {
            InitializeFramework(config, Urls.OrderHistoryPageUrl);

	        BuildElementsList(OrderDetails);

            NavigateToOrderDetails();

	        VerifyElementDisplayed(() => OrderDetails.OrderDetailsContainer);
            VerifyElementDisplayed(() => OrderDetails.OrderDetailTableRows);
            VerifyElementDisplayed(() => OrderDetails.OrderDetailTableFirstRow);

            VerifyElementDisplayed(() => OrderDetails.PaymentMethod);
            VerifyElementDisplayed(() => OrderDetails.PaymentMethodElement);
            VerifyElementDisplayed(() => OrderDetails.OrderItemTotal);
            VerifyElementDisplayed(() => OrderDetails.OrderTax);
            VerifyElementDisplayed(() => OrderDetails.OrderTotal);

            VerifyElementDisplayed(() => OrderDetails.OrderSummarySection);
            VerifyElementDisplayed(() => OrderDetails.ShippingProcessing);
            VerifyElementDisplayed(() => OrderDetails.Details);
            VerifyElementDisplayed(() => OrderDetails.OrderDetailsSections);
            VerifyElementDisplayed(() => OrderDetails.TableContainer);

            var order = OrderActions.GetOrderDetailsForOrderHistory().First();

            Browser.Navigate($"{Urls.OrderDetailsPageUrl}{order.OrderId}/{order.RewardNumber}/");

	        var lincInfo = OrderActions.GetLincInfo(order.OrderId);

	        if (lincInfo != null && lincInfo.Count > 0 && lincInfo.All(i => i.LincCompatible == true && i.ItemStatus != "Canceled"))
	        {
		        VerifyElementDisplayed(() => OrderDetails.TrackItemLink);
	        }
	        else
	        {
		        VerifyElementNotDisplayed("TrackItemLink");
			}
        }

        protected abstract void NavigateToOrderDetails();
    }
}
