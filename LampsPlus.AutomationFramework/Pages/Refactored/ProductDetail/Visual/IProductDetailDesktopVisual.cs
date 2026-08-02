using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.ProductDetail.Visual
{
    public interface IProductDetailDesktopVisual : IProductDetailDesktop
    {
        IElement IgnoreStockCheckWrapper();
        IElement IgnoreStickyHeader();
        IElement IgnoreQuestionsAndAnswersSection();
        IElement IgnoreReviewsSection();
        IElement IgnoreMoreYouMayLikeSection();
        IElement IgnoreCustomerPhotoTab();
        IElement GetMediaModalContentModal();
        IElement GetTurnToReviewModal();
        IElement GetEnergyInfoModal();
        IElement GetTurnToQuestionsAndAnswersSection();
    }
}