using System;
using System.Web.UI;
using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities;

namespace LampsPlus.AutomationFramework.Pages.Desktop
{
    public class ContactUs : ContactUsBase
    {
        public ContactUs(IBrowser browser) : base(browser) { }

        #region CSS Selectors
        public override string ContactEmailClass { get; } = "contact-email";
        public override string EmailOptInId { get; } = "EmailOptIn";
        public override string EmailUsId { get; } = "emailUs";
        public override string EmailUsButtonClass { get; } = "contact-email";
        public override string EmailUsModalId { get; } = "lpModalContent";
        public override string SendEmailBtnId { get; } = "sendEmailBtn";

        public override string ContactPanesClass => throw new NotImplementedException();
        public override string ContactConnectClass => throw new NotImplementedException();
        public override string FieldCheckboxClass => throw new NotImplementedException();
        public override string EmailCategoryXpath => throw new NotImplementedException();
        public override string EmailSubCategoryXpath => throw new NotImplementedException();
        public override string EmailSubmitSelector => throw new NotImplementedException();
        #endregion

        #region Page Elements
        //Elements that exist in both Desktop and Mobile views but are located differently.
        public override IElement EmailOptInCheckbox => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Label, HtmlTextWriterAttribute.For, EmailOptInId);
        public override IElement FormWrapper => Browser.Locate.ElementById(EmailUsId);
        public override IElement SubmitButton => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Button, HtmlTextWriterAttribute.Id, ContactEmailClass);
        public override IElement SendEmailButton => Browser.Locate.ElementByAttributeEquals(HtmlTextWriterAttribute.Value, "Send");
        public override IElement SendEmailButtonModal => Browser.Locate.ElementById(SendEmailBtnId);
        public override IElement EmailUsButton => Browser.Locate.ElementByClassName(EmailUsButtonClass);
        public override IElement EmailUsModal => Browser.Locate.ElementById(EmailUsModalId);
        #endregion

        public override void FillOutContactUsEmailForm()
        {
            var userAccountUnderTest = LampsPlusAccounts.CustomerLoginAccount;
            FirstNameInput.SendKeys(userAccountUnderTest.FirstName);
            LastNameInput.SendKeys(userAccountUnderTest.LastName);
            EmailAddressInput.SendKeys(userAccountUnderTest.UserName);
            Browser.Locate.ClickDropdownByValue(CategoryDropdown, "Payment and Billing");
            Browser.Locate.ClickDropdownByValue(SubCategoryDropdown, "Where Is My Refund");
            SubjectInput.SendKeys("Test Automation Testing");
            CommentsInput.SendKeys("DISREGARD: This is a test from test automation.");
            EmailOptInCheckbox.Click();
            SubmitButton.Click();

            Browser.Wait.UntilElementUnloads(SubmitButton);
        }
    }
}
