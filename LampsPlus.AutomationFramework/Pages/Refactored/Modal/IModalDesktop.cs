using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Modal
{
    public interface IModalDesktop : IPageObjectModel
    {
        string LpModalCloseId { get; }
        string LpModalId { get; }
        string LpModalIframeId { get; }
        bool IsModalVisible();
        bool IsModalNotVisible();
        bool IsModalWindowInitialized();
        IElement GetLpModal();
        IElement GetIframeModal();
        IElement GetLpModalContent();
        IElement GetLpModalClose();
        void CloseLpModal();
        void SwitchFocusToModal();
        IElement GetDiscountToolTipModal();
        void PrintModal();
        void WaitForModalContentToLoad();
        IElement GetIframe();
    }
}