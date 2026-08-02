using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Payment;
using LampsPlus.AutomationFramework.Workflow.Base;
using OpenQA.Selenium;

namespace LampsPlus.AutomationFramework.Workflow.Desktop
{
    /// <summary>
    /// Common behavior for managing account.
    /// </summary>
    public class ManageAccountWorkflow : ManageAccountWorkflowBase
    {
        public ManageAccountWorkflow(TestsBase testsBase) : base(testsBase) { }

        public override void AddNewPaymentMethod(CreditCard creditCard, Address address)
        {
            Browser.Wait.ForClickableElement(TestsBase.ManageAccount.AddPaymentOptionButton, 30).Click();
            Browser.Wait.IsVisibleElement(By.Id(GlobalLocators.LpModalId));

            Browser.Wait.IsVisibleElement(By.Id(GlobalLocators.LpModalId));

            TestsBase.ManageAccount.SetPaymentCard(creditCard);
            TestsBase.ManageAccount.SetPaymentAddress(address);

            TestsBase.ManageAccount.SavePaymentBtn.Click();

            Browser.Wait.UntilElementUnloads(GlobalLocators.Iframe);
        }

        /// <inheritdoc />
        public override void WaitForModalToFullyClose()
        {
            Browser.Wait.UntilElementDoesntExist(GlobalLocators.LpModalId);
        }

        public override void DeleteAllSavedPaymentOptions()
        {
            TestsBase.Browser.Navigate(Urls.ManageAccountPageUrl);
            Browser.Wait.IsVisibleElement(By.CssSelector(TestsBase.ManageAccount.EmailPreferencesLinkId.ToCssIdSelector()));
            if (!TestsBase.ManageAccount.ManagePaymentOptionsLinkForElement.IsInitialized || !TestsBase.ManageAccount.ManagePaymentOptionsLinkForElement.Displayed) { return; }
            var rewardNumber = TestsBase.ManageAccount.RewardNumber;
            TestsBase.AccountActions.ResetPaymentOptions(rewardNumber);
        }
    }
}
