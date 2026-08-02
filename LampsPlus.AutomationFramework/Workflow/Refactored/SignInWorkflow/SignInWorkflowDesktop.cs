using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Refactored.Home;
using LampsPlus.AutomationFramework.Pages.Refactored.SignIn;
using LampsPlus.AutomationFramework.Utilities;

namespace LampsPlus.AutomationFramework.Workflow.Refactored.SignInWorkflow
{
    public class SignInWorkflowDesktop : ISignInWorkflowDesktop
    {
        //Class members
        private static string _dataActionAttributeName = "data-action";
        private static string _returnString = "return";

        /// <summary>
        /// Return to last active session when max limit of sessions is reached.
        /// </summary>
        private void ReturnToActiveSession()
        {
            if (_browser.PageUrl == Urls.MaxSessionLimitPageUrl)
            {
                _browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.A, _dataActionAttributeName, _returnString).Click();
                _browser.Wait.ForPage(Urls.HomePageUrl, 5);

                _log.Message("Return active session");
            }
        }

        //Desktop POM and Workflow instances
        private readonly ISignInDesktop _signIn;
        private readonly IHomeDesktop _home;

        //TestsBase instances 
        private readonly IBrowser _browser;
        private readonly Log _log;

        public SignInWorkflowDesktop(IBrowser browser,  Log log,  ISignInDesktop signInDesktop, IHomeDesktop home)
        {
            _browser = browser;
            _log = log;
            _signIn = signInDesktop;
            _home = home;
        }

        public void SignInAndClearSession(string userName, string password)
        {
            //Sign In
            _signIn.SignIn(userName, password);

            //Return to active session
            ReturnToActiveSession();

            //ClearStoreInSession
            if (_home.IsStoreInSession())
            {
                _home.ClearStoreInSession();
                _log.Message("Store in session cleared");
            }
        }
    }
}