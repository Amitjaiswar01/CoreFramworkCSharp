using Xunit;
using Xunit.Abstractions;
using xRetry;
using System.Collections.Generic;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Payment.T136_T7032_VerifyCanNotSubmitIntOrderUnderTwoHundredDollars
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T7032_iPhone_VerifyCanNotSubmitIntOrderUnderTwoHundred : T7032_MobileBase
    {
        public T7032_iPhone_VerifyCanNotSubmitIntOrderUnderTwoHundred(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void UserCanNotSubmitIntOrderUnderTwoHundred(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T7032_Emulator_VerifyCanNotSubmitIntOrderUnderTwoHundred : T7032_MobileBase
    {
        public T7032_Emulator_VerifyCanNotSubmitIntOrderUnderTwoHundred(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void UserCanNotSubmitIntOrderUnderTwoHundred(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that there is validation for all payment fields and the error message for an order under $200 for an international order.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-6518
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7032
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-6518"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7032")]
    public abstract class T7032_MobileBase : TestsBaseMobile
    {
        protected T7032_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            /*Arrange
            User has found a SKU that is less than $200 and added to the cart.
            User has proceeded to the Payment page using a United States Shipping address.
            */
            InitializeFunctionalTest(config);

            var productLessThanTwoHundredDollars = ProductActions.GetSkuThatIsLessThanTwoHundredDollars;

            Assert.DatabaseObject(productLessThanTwoHundredDollars, "ProductActions.GetSkuThatIsLessThanTwoHundredDollars()");

            ShoppingCartWorkflow.ProceedToPaymentWithSingleProduct(productLessThanTwoHundredDollars);

            //Act. On the Payment page, uncheck the 'Same as Shipping box'. Click on the PLACE ORDER button.
            Payment.SelectSameAsShippingCheckbox();
            Payment.PlaceOrder();

            //Assert. Field validation messages appear for the free type fields.
            var emptyFreeTypeFields = Payment.GetPaymentPageFreeTypeFieldErrorMessages(numberOfFreeTypeFieldsOnPaymentPage:8);
            VerifyErrorMessageVisibleForFreeTypePaymentFields(emptyFreeTypeFields);

            //Assert. Field validation messages appear for the dropdown fields.
            var emptyDropdownFields = Payment.GetPaymentPageDropdownFieldErrorMessages();
            VerifyErrorMessageVisibleForDropdownPaymentFields(emptyDropdownFields);

            //Assert. No Error shown for phone number field
            Assert.Equals("", Payment.GetBillingPhoneNumberErrorMessage(), $"Error message should not displayed for the phone field.");

        }

        private void VerifyErrorMessageVisibleForFreeTypePaymentFields(Dictionary<string, string> freeTypeFields)
        {
            foreach (var field in freeTypeFields)
            {
                Assert.Equals(Payment.GetPaymentFieldErrorMessage(), field.Value, $"{field.Key} did not have the proper error message.");
            }
        }

        private void VerifyErrorMessageVisibleForDropdownPaymentFields(Dictionary<string, string> dropdownFields)
        {
            foreach (var field in dropdownFields)
            {
                Assert.Equals(Payment.GetDropdownFieldErrorMessage(), field.Value, $"{field.Key} did not have the proper error message.");
            }
        }
    }
}