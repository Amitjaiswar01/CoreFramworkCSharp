using Xunit;
using Xunit.Abstractions;
using xRetry;
using System.Linq;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.Payment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.ManageAccount.T301_T501_VerifyUserCanChangePaymentOption
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.ManageAccount)]
    public class T501_iPhone_VerifyUserCanChangePaymentOptions : T501_MobileBase
    {
        public T501_iPhone_VerifyUserCanChangePaymentOptions(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
        public void UserCanChangePaymentOptions(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ManageAccount)]
    public class T501_Emulator_VerifyUserCanChangePaymentOptions : T501_MobileBase
    {
        public T501_Emulator_VerifyUserCanChangePaymentOptions(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void UserCanChangePaymentOptions(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that a user can change their payment options.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9898
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T501
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9898"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T501")]
    public abstract class T501_MobileBase : TestsBaseMobile
    {
        protected T501_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            /*Arrange:
             User has a saved payment option.
             User has navigated to the 'Manage Account' info page: https://www.lampsplus.com/account/profile/ 
             */
            InitializeFunctionalTest(config);
            var expectedLandingPage = ManageAccount.PageUrl + ManageAccount.PaymentOptionsUrl;
            var browser = ManageAccount.Navigate(ManageAccount.PaymentOptionsUrl);
            Assert.Equals(expectedLandingPage, browser.PageUrl, $"{expectedLandingPage} is expected, but actual url is {browser.PageUrl}");
            ManageAccount.AddNewPaymentMethod(CreditCards.TestVisaCard, Address);

            /*Act:
             Under the Manage Account section, click on the 'Payment Options' link.
             Edit the payment information and click the 'Save' button.
             */
            var newCard = CreditCards.RandomTestCard();
            var newAddress = RandomAddressGenerator.RandomUsAddress();

            ManageAccount.EditPaymentOptionDetails(newCard, newAddress);

            var actualPhoneNumber = new string(ManageAccount.GetPaymentPhoneNumber().Where(char.IsDigit).ToArray());
            var actualLast4Digits = ManageAccount.GetCreditCardNumber().Substring(ManageAccount.GetCreditCardNumber().Length - 4);
            var expected4Digits = newCard.CardNumber.Substring(newCard.CardNumber.Length - 4);

            //Assert: The modified payment information is displayed.
            Assert.Equals(
                expected4Digits,
                actualLast4Digits,
                $"Expected last 4 digits {expected4Digits} but found {actualLast4Digits}");
            Assert.Equals(
                $"Name on Card: {newCard.NameOnCard}",
                ManageAccount.GetNameOnCreditCard(),
                $"Expected card holder {newCard.NameOnCard} but found {ManageAccount.GetNameOnCreditCard()}");
            Assert.Equals(
                $"Expiration: {newCard.ExpirationMonth}/{newCard.ExpirationYear}",
                ManageAccount.GetCreditCardExpirationDate(),
                $"Expected Expiration: {newCard.ExpirationMonth}/{newCard.ExpirationYear} but found {ManageAccount.GetCreditCardExpirationDate()}");
            Assert.Equals(
                $"{newAddress.FirstName} {newAddress.LastName}",
                ManageAccount.GetPaymentName(),
                $"Expected {newAddress.FirstName} {newAddress.LastName} but found {ManageAccount.GetPaymentName()}");
            Assert.Equals(
                newAddress.AddressLine1,
                ManageAccount.GetPaymentAddressField1(),
                $"Expected address {newAddress.AddressLine1} but found {ManageAccount.GetPaymentAddressField1()}");
            Assert.Equals(
                newAddress.AddressLine2,
                ManageAccount.GetPaymentAddressField2(),
                $"Expected address {newAddress.AddressLine2} but found {ManageAccount.GetPaymentAddressField2()}");
            Assert.Equals(
                $"{newAddress.City}, {newAddress.State} {newAddress.ZipCode}",
                ManageAccount.GetPaymentCity(),
                $"Expected address {newAddress.City}, {newAddress.State} {newAddress.ZipCode} but found {ManageAccount.GetPaymentCity()}");
            Assert.Equals(
                newAddress.Phone,
                actualPhoneNumber,
                $"Expected phone number {newAddress.Phone} but found {actualPhoneNumber}");

            //Data Cleanup
            ManageAccountWorkflow.DeleteAllSavedPaymentOptions();
        }
    }
}
