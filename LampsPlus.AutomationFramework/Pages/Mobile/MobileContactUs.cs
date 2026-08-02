using System;
using System.Web.UI;
using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities;
using OpenQA.Selenium;

namespace LampsPlus.AutomationFramework.Pages.Mobile
{
    public class MobileContactUs : ContactUsBase
    {
        public MobileContactUs(IBrowser browser) : base(browser) { }

        #region CSS Selectors
        public override string ContactConnectClass { get; } = "contactConnect";
        public override string ContactPanesClass { get; } = "contentWrp";
        public override string EmailCategoryXpath { get; } = "//*[@for='EmailCategory']/following-sibling::div[1]/button";
        public override string EmailSubCategoryXpath { get; } = "//*[@for='emailSubCategory']/following-sibling::div[1]/button";
        public override string EmailSubmitSelector { get; } = "#sendEmailBtn";
        public override string FieldCheckboxClass { get; } = "fieldCheckbox";
        public override string SendEmailBtnId { get; } = "sendEmailBtn";

        public override string ContactEmailClass => throw new NotImplementedException();
        public override string EmailOptInId => throw new NotImplementedException();
        public override string EmailUsButtonClass => throw new NotImplementedException();
        public override string EmailUsModalId => throw new NotImplementedException();
        public override string EmailUsId => throw new NotImplementedException();
        #endregion

        #region Page Elements
        public override IElement CategoryDropdown => Browser.Locate.ElementByXpath(EmailCategoryXpath);
        public override IElement EmailOptInCheckbox => Browser.Locate.ElementByClassName(FieldCheckboxClass);
        public override IElement FormWrapper => Browser.Locate.ElementByClassName(ContactPanesClass);
        public override IElement SendEmailButton => Browser.Locate.ElementByAttributeEquals(HtmlTextWriterAttribute.Value, "Send Email");
        public override IElement SendEmailButtonModal => Browser.Locate.ElementById(SendEmailBtnId);
        public override IElement SubCategoryDropdown => Browser.Locate.ElementByXpath(EmailSubCategoryXpath);
        public override IElement SubmitButton => Browser.Locate.ElementBySelector(EmailSubmitSelector);
        public override IElement EmailUsButton => throw new NotImplementedException();
        public override IElement EmailUsModal => throw new NotImplementedException();
        #endregion

        public override void FillOutContactUsEmailForm()
        {
            var userAccountUnderTest = LampsPlusAccounts.CustomerLoginAccount;
            FirstNameInput.SendKeys(userAccountUnderTest.FirstName);
            LastNameInput.SendKeys(userAccountUnderTest.LastName);
            EmailAddressInput.SendKeys(userAccountUnderTest.UserName);
            //Select Email Category dropdown option
            Browser.Wait.IsVisibleElement(By.XPath(EmailCategoryXpath));
            ContactUsDropdownOptionSelect(CategoryDropdown, "Payment and Billing");

            //Select Email SubCategory dropdown option
            Browser.Wait.IsVisibleElement(By.XPath(EmailSubCategoryXpath));
            ContactUsDropdownOptionSelect(SubCategoryDropdown, "Where Is My Refund");

            Browser.Wait.WaitForAjaxComplete(10);
            Browser.ScrollIntoView(SubjectInput);
            SubjectInput.Clear();
            SubjectInput.SendKeys("Test Automation Testing");
            CommentsInput.SendKeys("DISREGARD: This is a test from test automation.");
            EmailOptInCheckbox.Click();
            SubmitButton.Click();
        }

        public void ContactUsDropdownOptionSelect(IElement contactUsDropDown, string contactUsOption)
        {
            Browser.ScrollIntoView(contactUsDropDown, true);
            contactUsDropDown.Click();
            Browser.Wait.IsVisibleElement(By.XPath($"(//*[text()='{contactUsOption}'])[2]"));
            Browser.Locate.ElementByXpath($"(//*[text()='{contactUsOption}'])[2]").Click();
        }
    }
}
