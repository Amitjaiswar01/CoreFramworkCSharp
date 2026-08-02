using Xunit;
using Xunit.Abstractions;
using xRetry;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;

namespace LampsPlus.RegressionTests.Common.CartOverview.T129_T396_VerifyNonEsiCannotSubmitLessThanTenDollars
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T396_iPhone_VerifyNonEsiCannotSubmitLessThanTenDollars : T396_MobileBase
    {
        public T396_iPhone_VerifyNonEsiCannotSubmitLessThanTenDollars(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
        public void NonEsiCannotSubmitLessThanTenDollars(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T396_Emulator_VerifyNonEsiCannotSubmitLessThanTenDollars : T396_MobileBase
    {
        public T396_Emulator_VerifyNonEsiCannotSubmitLessThanTenDollars(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void NonEsiCannotSubmitLessThanTenDollars(string config) => Validate(config);
    }


    /// <summary>
    /// Verify Non Esi cannot submit less than ten dollars.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9907
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T396
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9907"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T396")]
    public abstract class T396_MobileBase : TestsBaseMobile
    {
        protected T396_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            // Arrange - User is on the product detail page of a single SKU.
            InitializeFramework(config);
            var shortSku = ProductActions.GetLessThanTenDollarItem;
            Assert.DatabaseObject(shortSku, "ProductActions.GetLessThanTenDollarItem()");

            // Act - Add the item less than ten dollar in Cart.
            ProductDetail.AddSingleProductToCart(shortSku);
            Assert.True(Cart.IsCurrentPage, "Current page is not Cart page");

            /* Act 
             * Open the shipping option menu.
             * Apply Zip code.
             * Click on the Standard shipping update button.*/
            Cart.EnterCartZipCodeForShippingOption(CountryCodeList.US, ZipCodeList.NorthSmithfield, 0);

            // Assert : Check whether Checkout Now and Paypal buttons are disable
            Assert.True(Cart.IsCheckOutNowButtonDisabled, "Checkout Now button not disabled");
            Assert.True(Cart.IsPaypalButtonDisabled, "Checkout Now button not disabled");
            Assert.Displayed(Cart.GetToolTip(), "Tooltip is not displayed");

            // Assert : Check whether tooltip message get displayed when hovering over checkout now button and paypal buton
            Assert.StringContains(Cart.GetToolTip().Text, Messages.CartMessages.TenPerOrderMsg, "$10 minimum order message is not displayed");
            Assert.StringContains(Cart.GetToolTip().Text, Messages.CartMessages.TenPerOrderMsg, "$10 minimum order message is not displayed");
        }
    }
}