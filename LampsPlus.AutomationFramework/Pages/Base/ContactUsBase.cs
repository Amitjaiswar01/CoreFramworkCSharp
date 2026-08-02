using Automation.Framework;
using Automation.Framework.Core;

namespace LampsPlus.AutomationFramework.Pages.Base
{
    /// <summary>
    /// Base class for common behavior between desktop and mobile views.
    /// </summary>
    public abstract class ContactUsBase : Page, IContactUs
    {
        /// <inheritdoc />
        protected ContactUsBase(IBrowser browser) : base(browser) { }

        #region Class Setup
        public string ThanksForUsingOurCustomerServiceCenterMessage => @"Thanks for using our Customer Service CenterYou'll soon receive an e-mail from us with a tracking ticket number. Keep that ticket number handy for future use.Please note: Because of increased volume and the impact of COVID-19, you may experience a longer response time for us to respond to your inquiry.We appreciate your patience while we work hard to respond and apologize for any inconvenience.Thanks again,The Lamps Plus Customer Service Team"; //TODO: This is a temporary message due to Covid-19. The usual message is commented out below.

        //Thanks for using our Customer Service CenterYou'll soon receive an e-mail from us with a tracking ticket number. Keep that ticket number handy for future use.As to your question or comment, one of our reps is investigating and will respond to you as soon as possible.Thanks again!The Lamps Plus Customer Service Team 
        #endregion

        #region CSS Selector Strings
        private string EmailCategoryId { get; } = "EmailCategory";
        private string EmailCommentsId { get; } = "EmailComments";
        private string EmailEmailAddressId { get; } = "EmailEmailAddress";
        private string EmailFirstNameId { get; } = "EmailFirstName";
        private string EmailLastNameId { get; } = "EmailLastName";
        private string EmailSubCategoryId { get; } = "emailSubCategory";
        private string EmailSubjectId { get; } = "EmailSubject";

        public abstract string ContactEmailClass { get; }
        public abstract string ContactPanesClass { get; }
        public abstract string ContactConnectClass { get; }
        public abstract string EmailOptInId { get; }
        public abstract string EmailCategoryXpath { get; }
        public abstract string EmailSubCategoryXpath { get; }
        public abstract string EmailSubmitSelector { get; }
        public abstract string EmailUsButtonClass { get; }
        public abstract string EmailUsModalId { get; }
        public abstract string EmailUsId { get; }
        public abstract string FieldCheckboxClass { get; }
        public abstract string SendEmailBtnId { get; }
        #endregion

        #region Page Elements
        //Elements that are located the same way in both Desktop and Mobile views.
        public virtual IElement CategoryDropdown => Browser.Locate.ElementById(EmailCategoryId);
        public IElement CommentsInput => Browser.Locate.ElementById(EmailCommentsId);
        public IElement EmailAddressInput => Browser.Locate.ElementById(EmailEmailAddressId);
        public IElement FirstNameInput => Browser.Locate.ElementById(EmailFirstNameId);
        public IElement LastNameInput => Browser.Locate.ElementById(EmailLastNameId);
        public virtual IElement SubCategoryDropdown => Browser.Locate.ElementById(EmailSubCategoryId);
        public IElement SubjectInput => Browser.Locate.ElementById(EmailSubjectId);

        //Elements that exist in both Desktop and Mobile views but are located differently.
        public abstract IElement EmailOptInCheckbox { get; }
        public abstract IElement SendEmailButton { get; }
        public abstract IElement SendEmailButtonModal { get; }

        //Elements that exist in Desktop view and NOT Mobile view.
        public abstract IElement FormWrapper { get; }
        public abstract IElement SubmitButton { get; }
        public abstract IElement EmailUsButton { get; }
        public abstract IElement EmailUsModal { get; }
        #endregion

        public abstract void FillOutContactUsEmailForm();
    }
}
