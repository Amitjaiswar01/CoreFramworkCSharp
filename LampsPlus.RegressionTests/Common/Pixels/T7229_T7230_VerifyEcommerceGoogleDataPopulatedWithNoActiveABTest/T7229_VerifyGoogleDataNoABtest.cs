using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using xRetry;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.Environment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Pixels.T7229_T7230_VerifyEcommerceGoogleDataPopulatedWithNoActiveABTest
{
    [Collection(LpTraits.RegressionFeatureTags.Pixel)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Pixels)]
    public class A_T7229_Windows_VerifyEcommerceGoogleDataPopulatedWithNoActiveABTest : T7229_DesktopBase
    {
        public A_T7229_Windows_VerifyEcommerceGoogleDataPopulatedWithNoActiveABTest(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T7229. Rework - ACD-10735")]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void EcommerceGoogleDataPopulated(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the Ecommerce Google Data is populated correctly (No Active A/B Test).
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7156
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7229
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7156"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7229"), Trait(LpTraits.Keys.Category, LpTraits.RegressionFeatureTags.Pixel)]
    public abstract class T7229_DesktopBase : TestsBaseDesktop
    {
        protected T7229_DesktopBase(ITestOutputHelper output) : base(output)
        {
        }

        protected void Validate(string config)
        {
            /*Arrange:
            1. Use the query to identify a qualifying Sort Page.
            2. Call the Sort Page determined by the query above        
            */
            var setup = new TestSetup(config) { IsNetworkLoggingTest = true };
            InitializeFunctionalTest(config, setup: setup);
            var sortAbTestInfo = SortActions.GetSortWithNoActiveAbTest();
            var sortPath = "https://" + sortAbTestInfo[0]["Url"];//using the first test returned by the query

            //Act: Add product to cart using different filter options.
            GoogleAnalyticsWorkflow.ValidateAbTestGaData(sortAbTestInfo, sortPath, 1);

            //Act: Submit order on two products added to cart above.
            Cart.CheckOut();

            //Act: Shipping Page Workflow
            Assert.True(Shipping.IsCurrentPage, "Current page is not Shipping page");
            CustomerAddressInformation.EnterShippingAddress(IntAddress, isIntAddress: true);

            //Act: Payment Page Workflow
            Shipping.ProceedToPayment();
            Payment.SelectInternationalAgreementAndPlaceOrder();

            //Act and Assert: Order Confirmation Page
            Assert.True(OrderConfirmation.IsCurrentPage, "Current page is not Order Confirmation page");

            var oCuTagValues = GoogleAnalyticsWorkflow.GetAndFormatUtagData();

            var expectedOcProdQueryStrings = new Dictionary<string, string>();

            expectedOcProdQueryStrings.Add("pr1cd16", $"{oCuTagValues["TestId"][0]}");
            expectedOcProdQueryStrings.Add("pr1cd17", $"{oCuTagValues["MmId"][0]}");
            expectedOcProdQueryStrings.Add("pr1cd18", $"{oCuTagValues["FormulaId"][0]}");
            expectedOcProdQueryStrings.Add("pr1cd19", $"{oCuTagValues["PinId"][0]}");
            expectedOcProdQueryStrings.Add("pr1cd20", $"{oCuTagValues["TestStartDate"][0]}");
            expectedOcProdQueryStrings.Add("pr1cd25", $"{sortAbTestInfo[0]["TestCompositionId"]}");
            expectedOcProdQueryStrings.Add("pr1cd35", $"{sortAbTestInfo[0]["FilterId"]}");

            Assert.True(NetworkLoggingUtility.RequestHasQueryParams("dt=order%20processing", expectedOcProdQueryStrings),
                "Order Confirmation is not sending expected product information.");
        }
    }
}