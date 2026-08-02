using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Payment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using OpenQA.Selenium.Support.UI;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.IntegrationTests.Pages.ManageAccount
{
    public class ManageAccountLocatorDesktopTests : ManageAccountLocatorTests
    {
        /// <summary>
        /// Tests to ensure this page can find all its IElements.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public ManageAccountLocatorDesktopTests(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the requested elements could be located on the Manage Account page.
        /// </summary>
        [Trait(LpTraits.Integration.PageObjectModel, "ManageAccount")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void LocateManageAccountElementsTest(string config) => Locate(config);

        protected override void VerifyAccountDrawer() { }

        protected override void VerifyPaymentOptionDrawer() { }

        protected override void VerifyBreadcrumb()
        {
            VerifyElementDisplayed(() => ManageAccount.OrderHistoryBreadcrumb);
        }

        protected override void VerifyStateField()
        {
            VerifyElementDisplayed(() => ManageAccount.DdlStateProvinceField);
        }

        protected override void VerifyCountryField()
        {
            VerifyElementDisplayed(() => ManageAccount.ShippingCountryField);
            VerifyElementDisplayed(() => ManageAccount.ShippingCountryOption);
        }

        protected override void VerifyPreConfirmationMessage()
        {
            Browser.Wait.ForDisplayedElement(ManageAccount.PreConfirmationMessageElement);
            VerifyElementDisplayed(() => ManageAccount.PreConfirmationMessageElement);
            VerifyElementDisplayed(() => ManageAccount.ModalMessage);
        }

        protected override void VerifyShippingAddressDrawer() { }

        protected override void VerifyAddresses()
        {
            Browser.Wait.ForDisplayedElement(ManageAccount.Addresses[0]);
            VerifyElementDisplayed(() => ManageAccount.Addresses);
        }

        protected override void VerifyAddShippingAddress()
        {
            VerifyElementDisplayed(() => ManageAccount.AddShippingAddressLink);
        }

        protected override void VerifyFirstAddress()
        {
            VerifyElementDisplayed(() => ManageAccount.AddressLine1Element);
            VerifyElementDisplayed(() => ManageAccount.AddressLine2Element);
            VerifyElementDisplayed(() => ManageAccount.CityElement);
            VerifyElementDisplayed(() => ManageAccount.FirstNameElement);
            VerifyElementDisplayed(() => ManageAccount.PhoneElement);
        }

        protected override void CloseModal()
        {
            Browser.SwitchToDefaultContent();
            CloseLpModal();
            Browser.Wait.ForDomReady();
        }

        protected override void CloseAccountModal()
        {
            CloseModal();
        }

        protected override void CloseEmailPreferencesModal()
        {
            CloseModal();
        }

        protected override void CloseChangePasswordModal()
        {
            CloseModal();
        }

        protected override void LocateEmployeeElements()
        {
            SignInWorkflow.SignIn(LampsPlusAccounts.CustomerServiceManagerLoginAccount);

            Browser.Navigate(Urls.EmployeeToolsPageUrl);
            Browser.Wait.ForDisplayedElement(ManageAccount.MyOrdersLink);
            VerifyElementDisplayed(() => ManageAccount.MyOrdersLink);

            ManageAccount.MyOrdersLink.Click();

            VerifyElementDisplayed(() => ManageAccount.MyPastOrderSection);
            VerifyElementDisplayed(() => ManageAccount.RadioButton);

            ManageAccount.RadioButton.Click();

            VerifyElementDisplayed(() => ManageAccount.OrderId);
            Browser.MoveToElement(ManageAccount.OrderId);
        }

        protected override void VerifyNotImplementedElements()
        {
            VerifyElementNotImplemented(() => ManageAccount.AccountProfileDrawer);
            VerifyElementNotImplemented(() => ManageAccount.PaymentOptionDrawer);
            VerifyElementNotImplemented(() => ManageAccount.ShippingAddressDrawer);
            VerifyElementNotImplemented(() => ManageAccount.CloseMobileModalBtn);
            VerifyElementNotImplemented(() => ManageAccount.CloseEmailPreferencesModalBtn);
            VerifyElementNotImplemented(() => ManageAccount.CountryElement);
            VerifyElementNotImplemented(() => ManageAccount.DdlStateField);
        }
    }


    public class ManageAccountLocatorMobileTests : ManageAccountLocatorTests
    {
        /// <summary>
        /// Tests to ensure this page can find all its IElements.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public ManageAccountLocatorMobileTests(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the requested elements could be located on the Manage Account page.
        /// </summary>
        [Trait(LpTraits.Integration.PageObjectModel, "ManageAccount")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void LocateManageAccountElementsTest(string config) => Locate(config);

        protected override void VerifyAccountDrawer()
        {
            VerifyElementDisplayed(() => ManageAccount.AccountProfileDrawer);
        }

        protected override void VerifyPaymentOptionDrawer()
        {
            VerifyElementDisplayed(() => ManageAccount.PaymentOptionDrawer);
            Browser.Wait.ForDisplayedElement(ManageAccount.DdlStateProvinceField);
            VerifyElementDisplayed(() => ManageAccount.DdlStateProvinceField);
        }

        protected override void VerifyBreadcrumb() { }

        protected override void VerifyPreConfirmationMessage()
        {
            Browser.Wait.ForDisplayedElement(ManageAccount.ModalMessage);
            VerifyElementDisplayed(() => ManageAccount.ModalMessage);
        }

        protected override void VerifyStateField()
        {
            VerifyElementDisplayed(() => ManageAccount.DdlStateField);
        }

        protected override void VerifyCountryField()
        {
            VerifyElementDisplayed(() => ManageAccount.ShippingCountryOption);
        }

        protected override void VerifyShippingAddressDrawer()
        {
            VerifyElementDisplayed(() => ManageAccount.ShippingAddressDrawer);
        }

        protected override void VerifyAddresses() { }

        protected override void VerifyAddShippingAddress() { }

        protected override void VerifyFirstAddress() { }

        protected override void CloseModal()
        {
            Browser.SwitchToDefaultContent();
            Browser.Wait.ForClickableElement(ManageAccount.CloseMobileModalBtn).Click();
        }

        protected override void CloseAccountModal()
        {
            Browser.SwitchToDefaultContent();
            VerifyElementDisplayed(() => ManageAccount.CloseMobileModalBtn);
            Browser.Wait.ForClickableElement(ManageAccount.CloseMobileModalBtn).Click();
        }

        protected override void CloseEmailPreferencesModal()
        {
            Browser.SwitchToDefaultContent();
            VerifyElementDisplayed(() => ManageAccount.CloseEmailPreferencesModalBtn);
            Browser.Wait.ForClickableElement(ManageAccount.CloseEmailPreferencesModalBtn).Click();
        }

        protected override void CloseChangePasswordModal()
        {
            Browser.SwitchToDefaultContent();
            Browser.Wait.ForClickableElement(ManageAccount.ChangePasswordModalCloseButton).Click();
        }
        
        protected override void LocateEmployeeElements() { }

        protected override void VerifyNotImplementedElements()
        {
            VerifyElementNotImplemented(() => ManageAccount.PreConfirmationMessageElement);
            VerifyElementNotImplemented(() => ManageAccount.ShippingCountryField);
            VerifyElementNotImplemented(() => ManageAccount.Addresses);
            VerifyElementNotImplemented(() => ManageAccount.AddShippingAddressLink);
            VerifyElementNotImplemented(() => ManageAccount.AddressLine1Element);
            VerifyElementNotImplemented(() => ManageAccount.AddressLine2Element);
            VerifyElementNotImplemented(() => ManageAccount.CityElement);
            VerifyElementNotImplemented(() => ManageAccount.CountryElement);
            VerifyElementNotImplemented(() => ManageAccount.FirstNameElement);
            VerifyElementNotImplemented(() => ManageAccount.PhoneElement);
            VerifyElementNotImplemented(() => ManageAccount.MyOrdersLink);
            VerifyElementNotImplemented(() => ManageAccount.MyPastOrderSection);
            VerifyElementNotImplemented(() => ManageAccount.RadioButton);
            VerifyElementNotImplemented(() => ManageAccount.OrderId);
            VerifyElementNotImplemented(() => ManageAccount.OrderHistoryBreadcrumb);
        }
    }


    /// <summary>
    /// Tests to ensure all IElements and Lists of IElements can be found on the Search page.
    /// </summary>
    public abstract class ManageAccountLocatorTests : PageObjectTestsBase
    {
        /// <summary>
        /// Tests to ensure this page can find all its IElements.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        protected ManageAccountLocatorTests(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the requested elements could be located on the Manage Account page.
        /// </summary>
        public void Locate(string config)
        {
            InitializeFramework(config, Urls.ManageAccountPageUrl);
            BuildElementsList(ManageAccount);

            VerifyElementDisplayed(() => ManageAccount.AccountProfileEditInfo);
            VerifyElementDisplayed(() => ManageAccount.ChangeEmailPreferencesLink);
            VerifyElementDisplayed(() => ManageAccount.ChangePasswordLink);
            VerifyElementDisplayed(() => ManageAccount.ManagePaymentOptionsLinkForElement);
            VerifyElementDisplayed(() => ManageAccount.ManageShippingAddressesLinkForElement);
            VerifyElementDisplayed(() => ManageAccount.RewardsNumberElement);
            VerifyElementDisplayed(() => ManageAccount.LblFirstNameField);
            VerifyElementDisplayed(() => ManageAccount.LblPhoneField);
            VerifyBreadcrumb();

            // check for elements on edit info popup
            ManageAccount.AccountProfileEditInfo.Click();
            VerifyAccountDrawer();
            VerifyElementDisplayed(() => ManageAccount.ModalWindow);
            VerifyElementDisplayed(() => ManageAccount.SaveInfoBtn);
            VerifyElementDisplayed(() => ManageAccount.TextCellField);
            var email = ManageAccount.TextEmailField.GetAttribute("value");
            ManageAccount.TextEmailField.Clear();
            ManageAccount.TextEmailField.SendKeys(email);
            Browser.Wait.ForDisplayedElement(ManageAccount.TextEmailField);
            VerifyElementDisplayed(() => ManageAccount.TextEmailField);
            VerifyElementDisplayed(() => ManageAccount.TextFaxField);
            VerifyElementDisplayed(() => ManageAccount.TextPhoneField);

            CloseAccountModal();
            Browser.Wait.ForDisplayedElement(ManageAccount.ChangeEmailPreferencesLink);
            // check for elements on email preferences popup
            ManageAccount.ChangeEmailPreferencesLink.Click();
           
            VerifyElementDisplayed(() => ManageAccount.SubscribeRadioButton);
            VerifyElementDisplayed(() => ManageAccount.UnsubscribeRadioButton);
            VerifyElementDisplayed(() => ManageAccount.SaveEmailPreferencesBtn);

            ManageAccount.SelectNewOptionAndSave();

            VerifyPreConfirmationMessage();

            CloseEmailPreferencesModal();

            // check for elements on change password popup
            ManageAccount.ChangePasswordLink.Click();
            Browser.Wait.ForDisplayedElement(ManageAccount.SaveNewPasswordBtn);
            VerifyElementDisplayed(() => ManageAccount.ModalChangePasswordElement);
            VerifyElementDisplayed(() => ManageAccount.SaveNewPasswordBtn);
            VerifyElementDisplayed(() => ManageAccount.SecretQuestionField);
            VerifyElementDisplayed(() => ManageAccount.TextConfirmPasswordField);
            VerifyElementDisplayed(() => ManageAccount.TextNewPasswordField);
            VerifyElementDisplayed(() => ManageAccount.TextSecretAnswerField);
            
            var account = LampsPlusAccounts.CustomerLoginAccount;
            
            ManageAccount.TextNewPasswordField.SendKeys(account.Password);
            ManageAccount.TextConfirmPasswordField.SendKeys(account.Password);
            Browser.Wait.ForDomReady();
            new SelectElement(ManageAccount.SecretQuestionField.InternalElement).SelectByIndex(1);
            ManageAccount.TextSecretAnswerField.Clear();
            ManageAccount.TextSecretAnswerField.SendKeys("test123");

            ManageAccount.SaveNewPasswordBtn.Click();

            Browser.Wait.ForDisplayedElement(ManageAccount.ChangePasswordModalMessage);
            VerifyElementDisplayed(() => ManageAccount.ChangePasswordModalMessage);
            VerifyElementDisplayed(() => ManageAccount.ChangePasswordModalCloseButton);

            CloseChangePasswordModal();

            // check for elements on manage payment options
            ManageAccount.ManagePaymentOptionsLinkForElement.Click();

            VerifyElementDisplayed(() => ManageAccount.AddPaymentOptionButton);

            // check for elements on add payment popup
            ManageAccount.AddPaymentOptionButton.Click();

            Browser.Wait.ForDisplayedElement(ManageAccount.ModalWindow);
            VerifyPaymentOptionDrawer();
            VerifyElementDisplayed(() => ManageAccount.SavePaymentBtn);
            VerifyElementDisplayed(() => ManageAccount.PhoneNumberField);
            VerifyElementDisplayed(() => ManageAccount.TextAddressField);
            VerifyElementDisplayed(() => ManageAccount.TextAddress2Field);
            VerifyElementDisplayed(() => ManageAccount.TextCardNumberField);
            VerifyElementDisplayed(() => ManageAccount.TextCityField);
            VerifyElementDisplayed(() => ManageAccount.TextExpMonthField);
            VerifyElementDisplayed(() => ManageAccount.TextExpYearField);
            VerifyElementDisplayed(() => ManageAccount.TextNameOnCardField);
            VerifyElementDisplayed(() => ManageAccount.TextZipField);
            
            Browser.RefreshPage();
            
            // add a new payment method to check for the elements
            ManageAccountWorkflow.AddNewDefaultPaymentMethod();
            ManageAccountWorkflow.AddNewPaymentMethod(CreditCards.TestMasterCard, AddressGenerator.BillingAddressCaliforniaUniqueEmail());
            Browser.Wait.ForDomReady();
            VerifyElementDisplayed(() => ManageAccount.FirstCreditCardInfo);
            VerifyElementDisplayed(() => ManageAccount.EditPaymentOption);
            VerifyElementDisplayed(() => ManageAccount.FirstDeletedSavedPaymentLink);
            VerifyElementDisplayed(() => ManageAccount.SavedPaymentOptions);
            VerifyElementDisplayed(() => ManageAccount.SavedPaymentOptionsImmediately);

            Browser.Navigate(Urls.ManageAccountPageUrl);

            ManageAccount.ManageShippingAddressesLinkForElement.Click();

            // check for elements on add shipping addresses popup
            ManageAccount.OpenAddShippingAddressModal();
            ManageAccountWorkflow.AddShippingAddressFromModal(new Address { State = StateCodeListUnitedStates.NV });

            ManageAccount.OpenAddShippingAddressModal();
            ManageAccountWorkflow.AddShippingAddressFromModal(new Address { State = StateCodeListUnitedStates.CA });
            Browser.SwitchToDefaultContent();

            // check for elements on add shipping addresses popup
            ManageAccount.OpenAddShippingAddressModal();
            VerifyShippingAddressDrawer();
            Browser.Wait.ForDisplayedElement(ManageAccount.ShippingFirstNameField);
            VerifyElementDisplayed(() => ManageAccount.ShippingFirstNameField);
            VerifyElementDisplayed(() => ManageAccount.ShippingLastNameField);
            VerifyElementDisplayed(() => ManageAccount.ShippingZipCodeField);
            VerifyElementDisplayed(() => ManageAccount.ShippingPhoneField);
            VerifyElementDisplayed(() => ManageAccount.ShippingStateField);
            VerifyElementDisplayed(() => ManageAccount.ShippingAddressLineOneField);
            VerifyElementDisplayed(() => ManageAccount.ShippingAddressLineTwoField);
            VerifyElementDisplayed(() => ManageAccount.ShippingCityField);
            VerifyStateField();
            VerifyElementDisplayed(() => ManageAccount.ShowCountryLink);
            VerifyElementDisplayed(() => ManageAccount.TextFirstNameField);
            VerifyElementDisplayed(() => ManageAccount.TextLastNameField);
            VerifyElementDisplayed(() => ManageAccount.TextZipCodeField);
            ManageAccount.ShowCountryLink.Click();
            VerifyCountryField();
            VerifyElementDisplayed(() => ManageAccount.DdlCountryField);
            VerifyElementDisplayed(() => ManageAccount.BtnSaveShippingAddress);
            CloseModal();

            // check for elements on manage shipping addresses
            VerifyAddresses();
            
            VerifyElementDisplayed(() => ManageAccount.BtnAddShippingAddress);
            VerifyElementDisplayed(() => ManageAccount.DefaultShippingAddressItemHeader);
            VerifyAddShippingAddress();
            VerifyElementDisplayed(() => ManageAccount.EditShippingAddressLink);

            VerifyFirstAddress();

            VerifyElementDisplayed(() => ManageAccount.ShippingAddressFullnamePage);
            VerifyElementDisplayed(() => ManageAccount.ShippingAddressLineOnePage);
            VerifyElementDisplayed(() => ManageAccount.ShippingCityStateZipPage);
            VerifyElementDisplayed(() => ManageAccount.ShippingPhonePage);
            
            // check for elements on sign out
            Browser.Navigate(Urls.HomePageUrl);

            SignInWorkflow.SignOut();

            Browser.Navigate(Urls.CreateAccountPageUrl);

            Browser.Wait.ForDisplayedElement(ManageAccount.FacebookConnectButton);
            VerifyElementDisplayed(() => ManageAccount.FacebookConnectButton);
            
            VerifyNotImplementedElements();
            
            // check for elements on signed in employee
            LocateEmployeeElements();
        }

        protected abstract void VerifyAccountDrawer();

        protected abstract void VerifyPaymentOptionDrawer();

        protected abstract void VerifyBreadcrumb();

        protected abstract void VerifyPreConfirmationMessage();

        protected abstract void VerifyStateField();

        protected abstract void VerifyCountryField();

        protected abstract void VerifyShippingAddressDrawer();

        protected abstract void VerifyAddresses();

        protected abstract void VerifyAddShippingAddress();

        protected abstract void VerifyFirstAddress();

        protected abstract void CloseAccountModal();

        protected abstract void CloseModal();

        protected abstract void CloseEmailPreferencesModal();

        protected abstract void CloseChangePasswordModal();

        protected abstract void LocateEmployeeElements();

        protected abstract void VerifyNotImplementedElements();
    }
}
