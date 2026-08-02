using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Workflow.Base;
using OpenQA.Selenium;

namespace LampsPlus.AutomationFramework.Workflow.Desktop
{
    /// <summary>
    /// Provides a common way to sign in to the website.
    /// </summary>
    public class SignInWorkflow : SignInWorkflowBase
    {
        public SignInWorkflow(TestsBase testsBase) : base(testsBase) { }

        /// <inheritdoc />
        public override void SignOut()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(TestsBase.HeaderFooter.UserNameId.ToCssIdSelector()),30);
            Browser.MouseOverOnElement(TestsBase.HeaderFooter.UserNameLink);
            Browser.Wait.IsVisibleElement(By.CssSelector(TestsBase.HeaderFooter.HrdSignOutId.ToCssIdSelector()),30);
            TestsBase.HeaderFooter.SignOutLink.Click();
        }

        /// <inheritdoc />
        public override void EnsureUserSignedOut()
        {
            if (IsLoggedInUser)
            {
                SignOut();
            }
        }
    }
}
