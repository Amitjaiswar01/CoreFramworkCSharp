using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.ProductDetail.Visual
{
    public interface IProductDetailMobileVisual : IProductDetailMobile
    {
        IElement IgnoreStockCheckWrapper();
        IElement IgnoreCertonaDrawerName();
        IElement IgnoreStickyHeader();
        IElement CompleteLookSection();
        IElement IgnoreMoreYouMayLikeContainer();
        IElement GetMediaModalContentModal();
        IElement GetTurnToReviewModal();
        IElement GetEnergyInfoModal();
        IElement GetTurnToQuestionsAndAnswersSection();
        IElement IgnoreCustomerPhotoTab();
    }
}