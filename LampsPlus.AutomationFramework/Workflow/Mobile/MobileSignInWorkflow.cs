using OpenQA.Selenium;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Workflow.Base;


namespace LampsPlus.AutomationFramework.Workflow.Mobile
{
    /// <summary>
    /// Provides a common way to sign in to the website.
    /// </summary>
    public class MobileSignInWorkflow : SignInWorkflowBase
    {
        public MobileSignInWorkflow(TestsBase testsBase) : base(testsBase) { }

        private string MyAccountXpathLocator { get; } = "//button[contains (@class,'lpmmLoginStatus__link')]";

        public override void ShowLpMenu()
        {
            if (!ElementActions.HasClass(TestsBase.Home.BodyElement, "lpmmMenuOpen"))
                Browser.ExecuteJs("arguments[0].click()", TestsBase.HeaderFooter.HamburgerMenu.InternalElement);

            Browser.Wait.ForElementToStopAnimating(GlobalLocators.MobileDrawerMenuInnerContainer);
        }

        /// <inheritdoc />
        public override void SignOut()
        {
            if (Browser.PageUrl.Equals(Urls.OrderConfirmationPageUrl))
                Browser.Navigate(Urls.HomePageUrl);

            Browser.Wait.ForDomReady(30);

            ShowLpMenu();

            //Show My Account menu
            Browser.Wait.IsVisibleElement(By.XPath(MyAccountXpathLocator));
            Browser.Locate.ElementByXpath(MyAccountXpathLocator).Click();

            //Click Sign Out
            Browser.Wait.IsVisibleElement(By.CssSelector(TestsBase.HeaderFooter.HdrSignOutId.ToCssIdSelector()));
	        TestsBase.HeaderFooter.SignOutLink.Click();

            Browser.Wait.IsInvisibleElement(By.CssSelector(TestsBase.HeaderFooter.HeaderAccountClass.ToCssClassSelector()));
            Browser.Wait.ForDomReady(30);
        }
        
        /// <inheritdoc />
        public override void EnsureUserSignedOut()
        {
            if (TestsBase.HeaderFooter.SignOutLink.IsInitialized)
            {
                Browser.Navigate(Urls.HomePageUrl);
                SignOut();
            }
        }
    }
}
