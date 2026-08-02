using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.ProductDetail
{
    public interface IProductDetailMobile : IProductDetailDesktop
    {
        string GetProductPriceText();
        void ToggleTurnToQuestionsAndAnswersSection();
        void ToggleProductReviewsSection();
        void DisplayProductHelpLink();
        void OpenCallDialog();
        void OpenChat();
        void OpenBuildFullSystemDrawer();
        void ConfirmClosingOfChatAssistant();
        void CloseChatAssistant();
        void OpenProductDetailsDrawer();
        void OpenSpecificationTableDrawer();
        void FocusCustomerReviewsSection();
        bool IsChatModalVisible();
        bool IsCustomerServiceNumberVisible();
        bool IsChatIconVisible { get; }
        bool AreRelatedItemsVisible();
        bool IsDrawerNameVisibleInViewport();
        IElement GetHaveAQuestionSection();
        IElement GetCertonaDrawerName();
    }
}