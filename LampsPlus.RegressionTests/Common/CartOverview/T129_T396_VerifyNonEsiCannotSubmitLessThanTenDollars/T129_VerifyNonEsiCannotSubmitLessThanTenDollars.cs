using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;

namespace LampsPlus.RegressionTests.Common.CartOverview.T129_T396_VerifyNonEsiCannotSubmitLessThanTenDollars
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T129_Windows_VerifyNonEsiCannotSubmitLessThanTenDollars : T129_DesktopBase
    {
        public T129_Windows_VerifyNonEsiCannotSubmitLessThanTenDollars(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T129. Rework - ACD-10799")]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void NonEsiCannotSubmitLessThanTenDollars(string config) => Validate(config);
    }


    /// <summary>
    /// Verify Non Esi cannot submit less than ten dollars.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9907
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T129
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9907"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T129")]
    public abstract class T129_DesktopBase : TestsBaseDesktop
    {
        protected T129_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            /* Arrange - 
             * User is on the product detail page of a single SKU.
             * Add the item less than ten dollar in Cart.
             */
            InitializeFramework(config);
            var shortSku = ProductActions.GetLessThanTenDollarItem;
            Assert.DatabaseObject(shortSku, "ProductActions.GetLessThanTenDollarItem()");
            ProductDetail.AddSingleProductToCart(shortSku);
            Assert.True(Cart.IsCurrentPage, "Current page is not Cart page");

            /* Act 
             * Open the shipping option menu.
             * Apply Zip code.
             * Click on the Standard shipping update button.*/
            Cart.EnterCartZipCodeForShippingOption(CountryCodeList.US, ZipCodeList.NorthSmithfield, 0);

            // Assert : Check whether Checkout Now button is disable
            Assert.True(Cart.IsCheckOutNowButtonDisabled, "Checkout Now button not disabled");

            // Act : Hover over Check Out Now button to check warning message
            ShoppingCartWorkflow.EnableTooltip(Cart.GetCheckOutNowButton());

            // Assert : Verify the tooltip is displayed.
            Assert.Displayed(Cart.GetToolTip(), "Tooltip is not displayed");

            // Assert : Check whether tooltip message get displayed when hovering over checkout now button
            Assert.StringContains(Cart.GetToolTip().Text, Messages.CartMessages.TenPerOrderMsg, "$10 minimum order message is not displayed");

            // Act : Hover over Paypal button to check warning message
            ShoppingCartWorkflow.EnableTooltip(Cart.GetPaypalButton());

            // Assert : Check whether tooltip message get displayed when hovering over paypal button
            Assert.StringContains(Cart.GetToolTip().Text, Messages.CartMessages.TenPerOrderMsg, "$10 minimum order message is not displayed");              
        }
    }
}