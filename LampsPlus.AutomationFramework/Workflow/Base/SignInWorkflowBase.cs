using System.Web.UI;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Environment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using OpenQA.Selenium;

namespace LampsPlus.AutomationFramework.Workflow.Base
{
    /// <summary>
    /// Provides a common way to sign in to the website.
    /// </summary>
    public abstract class SignInWorkflowBase : WorkflowBase, ISignInWorkflow
    {
        protected SignInWorkflowBase(TestsBase testsBase) : base(testsBase) { }

        private static string DataActionAttributeName => "data-action";
        private static string ReturnString => "return";

        private bool IsOnSignInPage => Browser.PageUrl == Urls.SignInPageUrl;

        private void LogIn(LampsPlusAccount loginAccount)
        {
            TestsBase.Browser.Navigate(Urls.SignInPageUrl);
            TestsBase.Browser.Wait.IsVisibleElement(By.XPath(TestsBase.SignIn.AccountSignInXpath), 30);
            TestsBase.SignIn.EmailField.SendKeys(loginAccount.UserName);
            TestsBase.SignIn.PasswordField.SendKeys(loginAccount.Password);
            Browser.ExecuteJs("document.querySelector('#submitFormBtn').click()");
            Account = loginAccount;
        }

        /// <inheritdoc />
        public bool IsPasswordChanged { get; set; }

        /// <inheritdoc />
        public LampsPlusAccount Account { get; set; }

        /// <inheritdoc />
        public bool IsLoggedInAsCustomerService => (bool)Browser.ExecuteJs("return window.lp.globals.isCustomerService");

        /// <inheritdoc />
        public bool IsLoggedInAsKiosk => (bool)Browser.ExecuteJs("return window.lp.globals.isKiosk");

        /// <inheritdoc />
        public bool IsLoggedInUser => (bool)Browser.ExecuteJs("return window.lp.globals.isLoggedIn");

        public static LampsPlusAccount GetDefaultLoginTypeByUserRole(UserRole userRole, bool useEmployeeManagerAccount)
        {
            var account = new LampsPlusAccount(string.Empty, string.Empty);

            switch (userRole)
            {
                case UserRole.SIS_ESI:
                case UserRole.SIS_ESI_CIC:
                case UserRole.SNIS_ESI:
                case UserRole.SNIS_ESI_CIC: // Employee 
                {
                    account = useEmployeeManagerAccount
                        ? LampsPlusAccounts.CustomerServiceManagerLoginAccount
                        : LampsPlusAccounts.CustomerServiceRegularLoginAccount;
                    break;
                }
                case UserRole.SNIS_PCSI: // Pro
                {
                    account = LampsPlusAccounts.ProfessionalLoginAccount;
                    break;
                }
                case UserRole.SNIS_NPCSI: // Customer
                {
                    account = LampsPlusAccounts.CustomerLoginAccount;
                    break;
                }
                case UserRole.SIS_UNSI: // Kiosk mode
                {
                    // No Login required.

                    break;
                }
                case UserRole.SNIS_HCSI: // Hospitality
                {
                    account = LampsPlusAccounts.HospitalityLoginAccount;
                    break;
                }
            }

            return account;
        }

        /// <inheritdoc />
        public void SignIn(LampsPlusAccount loginAccount, bool selectKeepMeLoggedIn = false)
        {
            if (loginAccount.IsAccountValid)
            {
                LogIn(loginAccount);

                if (!TestsBase.Settings.IsMobileView)
                    Browser.Wait.IsVisibleElement(By.CssSelector("#userName"),30);
                else
                    Browser.Wait.IsVisibleElement(By.CssSelector(".hpBanner > a > img"),30);

                TestsBase.Log.Message($"Signed In with user: {loginAccount.UserName}");

                ReturnToActiveSession();

                if (IsOnSignInPage)
                {
                    TestsBase.Assert.NotDisplayed(TestsBase.GlobalLocators.ErrorMessageElement,
                        $"Unable to login with user name {loginAccount.UserName} and password {loginAccount.Password}");
                }

                if (TestsBase.Home.IsStoreInSession())
                {
                    TestsBase.Home.ClearStoreInSession();
                    TestsBase.Log.Message("Store in session cleared");
                }
            }
        }

        /// <inheritdoc />
        public abstract void SignOut();
        
        /// <inheritdoc />
        public void SignInWithUserRole(TestSetup setup)
        {
            SignIn(setup.AccountConfig.AccountUnderTest);
        }

        /// <inheritdoc />
        public abstract void EnsureUserSignedOut();

        /// <summary>
        /// Return to last active session when max limit of sessions is reached.
        /// </summary>
        private void ReturnToActiveSession()
        {
	        if (Browser.PageUrl == Urls.MaxSessionLimitPageUrl)
	        {
		        Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.A, DataActionAttributeName, ReturnString).Click();
		        Browser.Wait.ForPage(Urls.HomePageUrl, 5);

		        TestsBase.Log.Message("Return active session");
	        }
		}

        internal TestsBase Framework;

        /// <inheritdoc />
        public virtual void ShowLpMenu() { }
    }
}
