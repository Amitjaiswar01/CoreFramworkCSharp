using Automation.Framework;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Pages.Refactored.Address;
using LampsPlus.AutomationFramework.Pages.Refactored.HeaderFooter;
using LampsPlus.AutomationFramework.Pages.Refactored.ManageAccount;
using LampsPlus.AutomationFramework.Pages.Refactored.SignIn;
using LampsPlus.AutomationFramework.Pages.Refactored.Shipping;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Payment;

namespace LampsPlus.AutomationFramework.Workflow.Refactored.ManageAccountWorkflow
{
    public class ManageAccountWorkflowDesktop : IManageAccountWorkflowDesktop
    {
        public ManageAccountWorkflowDesktop(IBrowser browser, IAssert assert, IHeaderFooterDesktop headerFooter, ISignInDesktop signInDesktop, IManageAccountDesktop manageAccount, IShippingDesktop shipping, IAddress address)
        {
            _browser = browser;
            _headerFooter = headerFooter;
            _signIn = signInDesktop;
            _manageAccount = manageAccount;
            _shipping = shipping;
            _address = address;
            _assert = assert;
        }

        //Desktop POM and Workflow instances
        private readonly ISignInDesktop _signIn;
        private readonly IHeaderFooterDesktop _headerFooter;
        private readonly IManageAccountDesktop _manageAccount;
        private readonly IShippingDesktop _shipping;
        private readonly IAddress _address;
        private readonly IAssert _assert;

        //TestsBase instances
        private readonly IBrowser _browser;

        //Interface implementation
        public void ChangeAccountPassword(string userName, string originalPassword, string newPassword)
        {
            if (_headerFooter.IsSignInLinkVisible)
            {
                _signIn.SignIn(userName, originalPassword);
            }

            _browser.Navigate(Urls.ManageAccountPageUrl);
            _assert.True(_manageAccount.IsCurrentPage, "User is not on the Manage Account page.");
            _manageAccount.SaveNewPassword(newPassword);
        }

        public void DeleteAllSavedAddresses()
        {
            if (_browser.PageUrl != Urls.ManageAccountPageUrl)
            {
                _headerFooter.NavigateToManageAccount();
                _browser.Wait.ForDomReady();
            }
            
            if (!_manageAccount.IsManageShippingAddressesLinkVisible) { return; }

            _manageAccount.ResetAccountShippingAddresses();
        }

        public void DeleteAllSavedPaymentOptions()
        {
            if (_browser.PageUrl != Urls.ManageAccountPageUrl)
            {
                _headerFooter.NavigateToManageAccount();
                _browser.Wait.ForDomReady();
            }

            if (!_manageAccount.IsManagePaymentOptionsLinkVisible) { return; }

            _manageAccount.ResetAccountPaymentOptions();
        }

        public void AddNewDefaultPaymentMethod(CreditCard creditCard)
        {
            var expectedLandingPage = _manageAccount.PageUrl + _manageAccount.PaymentOptionsUrl;
            var browser = _manageAccount.Navigate(_manageAccount.PaymentOptionsUrl);
            _assert.Equals(expectedLandingPage, browser.PageUrl, $"{expectedLandingPage} is expected, but actual url is {browser.PageUrl}");

            _manageAccount.AddNewPaymentMethod(creditCard, _address);
        }

        public void FillOutShippingAddressForm(IAddress address)
        {
            _browser.Navigate(Urls.ManageShippingAddressPageUrl);
            _browser.Wait.ForDomReady();
            _manageAccount.OpenShippingAddressForm();
            _manageAccount.AddNewShippingAddressToModal(address);
            _shipping.HandleFedExModalIfPresent();
            _manageAccount.SaveShippingAddress();
        }
    }
}
