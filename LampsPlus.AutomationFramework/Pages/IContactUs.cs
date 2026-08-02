using Automation.Framework;
using Automation.Framework.Utilities;

namespace LampsPlus.AutomationFramework.Pages
{
    /// <summary>
    /// Common behavior between desktop and mobile views.
    /// </summary>
    public interface IContactUs
    {
        #region Page Elements
        string ContactPanesClass { get; }
        string ContactConnectClass { get; }
        string FieldCheckboxClass { get; }
        string EmailCategoryXpath { get; }
        string EmailOptInId { get; }
        string EmailSubCategoryXpath { get; }
        string EmailSubmitSelector { get; }
        string EmailUsId { get; }
        string ContactEmailClass { get; }
        string SendEmailBtnId { get; }
        string ThanksForUsingOurCustomerServiceCenterMessage { get; }

        IElement CategoryDropdown { get; }
        IElement CommentsInput { get; }
        IElement EmailAddressInput { get; }
        IElement EmailOptInCheckbox { get; }
        IElement EmailUsButton { get; }
        IElement EmailUsModal { get; }
        IElement FirstNameInput { get; }
        IElement FormWrapper { get; }
        IElement LastNameInput { get; }
        IElement SendEmailButton { get; }
        IElement SendEmailButtonModal { get; }
        IElement SubCategoryDropdown { get; }
        IElement SubjectInput { get; }
        IElement SubmitButton { get; }
        #endregion

        /// <summary>
        /// Log class to update log messages.
        /// </summary>
        Log Log { get; }

        /// <summary>
        /// Instance of a Browser to enable browser specific UI testing.
        /// </summary>
        IBrowser Browser { get; }

        /// <summary>
        /// Navigate to the given URL.
        /// </summary>
        /// <param name="url">URL to navigate to. This must be begin with http:// or https://.</param>
        void Navigate(string url);

        /// <summary>
        /// Fill out the contact us email form
        /// </summary>
        void FillOutContactUsEmailForm();
    }
}
