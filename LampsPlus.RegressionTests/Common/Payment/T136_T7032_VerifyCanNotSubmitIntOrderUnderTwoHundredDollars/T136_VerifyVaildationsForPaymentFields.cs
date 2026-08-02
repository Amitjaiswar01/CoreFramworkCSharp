using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;


namespace LampsPlus.RegressionTests.Common.Payment.T136_T7032_VerifyCanNotSubmitIntOrderUnderTwoHundredDollars
{
    //[Collection(LpTraits.BatchGroup.Common.Payment)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T136_Windows_VerifyValidationsForPaymentFields : T136_DesktopBase
    {
        public T136_Windows_VerifyValidationsForPaymentFields(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void ValidationsErrorsForPaymentFields(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Payment)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T136_Mac_VerifyValidationsForPaymentFields : T136_DesktopBase
    {
        public T136_Mac_VerifyValidationsForPaymentFields(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void ValidationsErrorsForPaymentFields(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Payment)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T136_iPad_VerifyValidationsForPaymentFields : T136_DesktopBase
    {
        public T136_iPad_VerifyValidationsForPaymentFields(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void ValidationsErrorsForPaymentFields(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Payment)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Payment)]
    public class T136_TabletEmulator_VerifyValidationsForPaymentFields : T136_DesktopBase
    {
        public T136_TabletEmulator_VerifyValidationsForPaymentFields(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void ValidationsErrorsForPaymentFields(string config) => Validate(config);
    }


    /// <summary>
    /// Verify Validations for Payment Fields on Payment Page
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-6518
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T136
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-6518"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T136")]

    public class T136_DesktopBase : TestsBaseDesktop
    {
        public T136_DesktopBase (ITestOutputHelper output) : base(output) { }

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
            var emptyFreeTypeFields = Payment.GetPaymentPageFreeTypeFieldErrorMessages(numberOfFreeTypeFieldsOnPaymentPage:4);
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
