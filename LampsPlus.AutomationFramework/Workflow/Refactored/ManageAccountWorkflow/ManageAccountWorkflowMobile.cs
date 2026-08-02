using Automation.Framework;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Pages.Refactored.Address;
using LampsPlus.AutomationFramework.Pages.Refactored.HeaderFooter;
using LampsPlus.AutomationFramework.Pages.Refactored.ManageAccount;
using LampsPlus.AutomationFramework.Pages.Refactored.Shipping;
using LampsPlus.AutomationFramework.Pages.Refactored.SignIn;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Payment;

namespace LampsPlus.AutomationFramework.Workflow.Refactored.ManageAccountWorkflow
{
    public class ManageAccountWorkflowMobile : IManageAccountWorkflowMobile
    {
        public ManageAccountWorkflowMobile(IBrowser browser, IAssert assert, IHeaderFooterMobile headerFooter, ISignInMobile signInDesktop, IManageAccountMobile manageAccount, IShippingMobile shipping, IAddress address)
        {
            _browser = browser;
            _headerFooter = headerFooter;
            _signIn = signInDesktop;
            _manageAccount = manageAccount;
            _shipping = shipping;
            _address = address;
            _assert = assert;
        }

        //Mobile POM and Workflow instances
        private readonly ISignInMobile _signIn;
        private readonly IHeaderFooterMobile _headerFooter;
        private readonly IManageAccountMobile _manageAccount;
        private readonly IShippingDesktop _shipping;
        private readonly IAddress _address;
        
        //TestsBase instances 
        private readonly IBrowser _browser;
        private readonly IAssert _assert;

        //Interface implementation
        public void ChangeAccountPassword(string userName, string originalPassword, string newPassword)
        {

            if (_headerFooter.IsSignInLinkVisible)
            {
                _signIn.SignIn(userName, originalPassword);
            }

            _headerFooter.NavigateToManageAccount();

            _browser.Wait.ForDomReady();

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
                _manageAccount.IsRewardNumberVisible();
            }

            if (!_manageAccount.IsManagePaymentOptionsLinkVisible) { return; }

            _assert.True(_manageAccount.IsCurrentPage, "User is not on Manage Account page.");
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
            _manageAccount.OpenShippingAddressForm();
            _manageAccount.AddNewShippingAddressToModal(address);
            _shipping.HandleFedExModalIfPresent();
            _manageAccount.SaveShippingAddress();
        }

        public void AddMultipleShippingAddress(IAddress address1, IAddress address2)
        {
            FillOutShippingAddressForm(address1);
            _manageAccount.SelectAddShippingAddress();
            _manageAccount.AddNewShippingAddressToModal(address2);
            _shipping.HandleFedExModalIfPresent();
            _manageAccount.SaveShippingAddress();
        }
    }
}