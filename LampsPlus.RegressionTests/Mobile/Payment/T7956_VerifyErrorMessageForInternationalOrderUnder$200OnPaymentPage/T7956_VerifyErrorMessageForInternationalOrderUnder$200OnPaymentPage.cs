using xRetry;
using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Mobile.Payment.T7956_VerifyErrorMessageForInternationalOrderUnder_200OnPaymentPage
{

    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T7956_iPhone_VerifyErrorMessageForInternationalOrderUnder200OnPaymentPage : T7956_MobileBase
    {
        public T7956_iPhone_VerifyErrorMessageForInternationalOrderUnder200OnPaymentPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifyErrorMessageForInternationalOrder(string config) => Validate(config);
    }


    public class T7956_Emulator_VerifyErrorMessageForInternationalOrderUnder200OnPaymentPage : T7956_MobileBase
    {
        public T7956_Emulator_VerifyErrorMessageForInternationalOrderUnder200OnPaymentPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyErrorMessageForInternationalOrder(string config) => Validate(config);
    }


    /// <summary>
    ///Verify Error Message for an International Order Under $200 on Payment Page
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10689
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7955
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10689"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7956")]
    public abstract class T7956_MobileBase : TestsBaseMobile
    {
        protected T7956_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            /*Arrange:
             Add an item with an amount less then $200 to the cart
             Proceed to the Payment page.
            */
            InitializeFunctionalTest(config);

            var getSkuLessThanTwoHundredDollars = ProductActions.GetSkuThatIsLessThanTwoHundredDollars;
            Assert.DatabaseObject(getSkuLessThanTwoHundredDollars, "ProductActions.GetSkuThatIsLessThanTwoHundredDollars()");

            ShoppingCartWorkflow.ProceedToPaymentWithSingleProduct(getSkuLessThanTwoHundredDollars);

            /*Act: 
            Click on My Address Outside US
            Select the Country other than US & Canada
            */
            Payment.SelectSameAsShippingCheckbox();

            CustomerAddressInformation.EnterBillingAddress(Address);
            Address.Country = CountryCodeList.GB;
            CustomerAddressInformation.ChangeBillingCountry(Address);

            //Assert. Verify the Minimum Order Value Not Met error appears
            Assert.True(Payment.IsMinimumOrderMessageVisible, "Minimum Order Error did Not Displayed");
        }
    }
}

