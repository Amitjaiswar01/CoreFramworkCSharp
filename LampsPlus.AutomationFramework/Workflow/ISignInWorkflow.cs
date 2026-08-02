using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Environment;

namespace LampsPlus.AutomationFramework.Workflow
{
    /// <summary>
    /// Common behavior for the Sign In workflow.
    /// </summary>
    public interface ISignInWorkflow
    {
        /// <summary>
        /// Account that is used during Test.
        /// </summary>
        LampsPlusAccount Account { get; set; }

        /// <summary>
        /// Is the current user a customer service user?
        /// </summary>
        bool IsLoggedInAsCustomerService { get; }

        /// <summary>
        /// Is the current user in kiosk mode?
        /// </summary>
        bool IsLoggedInAsKiosk { get; }

        /// <summary>
        /// Is the current user logged in?
        /// </summary>
        bool IsLoggedInUser { get; }

        /// <summary>
        /// Tells whether Password was changed.
        /// </summary>
        bool IsPasswordChanged { get; set; }

        /// <summary>
        /// Check if the user is signed in if yes, log out by clicking the sign out button.
        /// </summary>
        void EnsureUserSignedOut();

        /// <summary>
        /// Sign In with the given user account.
        /// </summary>
        /// <param name="loginAccount">Account to use to sign in.</param>
        /// <param name="selectKeepMeLoggedIn">Flag to verify "Keep Me Logged In" option is selected when logging in.</param>
        void SignIn(LampsPlusAccount loginAccount, bool selectKeepMeLoggedIn = false);

        /// <summary>
        /// Sign in with the default account based on the user role in the provided setup object.
        /// </summary>
        /// <param name="setup">Configuration object for test setup and teardown.</param>
        void SignInWithUserRole(TestSetup setup);

        /// <summary>
        /// Sign Out from the given user account.
        /// </summary>
        void SignOut();

        /// <summary>
        /// Show Lampsplus menu.
        /// </summary>
        void ShowLpMenu();
    }
}
