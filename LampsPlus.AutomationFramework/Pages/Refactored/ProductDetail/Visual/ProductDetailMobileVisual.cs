using Automation.Framework;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Databases.Actions;
using LampsPlus.AutomationFramework.Pages.Refactored.Modal;
using OperatingSystem = LampsPlus.AutomationFramework.Utilities.TestConfiguration.OperatingSystem;

namespace LampsPlus.AutomationFramework.Pages.Refactored.ProductDetail.Visual
{
    public class ProductDetailMobileVisual : ProductDetailMobile, IProductDetailMobileVisual
    {
        public ProductDetailMobileVisual(IBrowser browser, ProductActions productActions, IAssert assert,
            OperatingSystem operatingSystem, IModalDesktop modal) : base(browser, productActions, assert,
            operatingSystem, modal)
        {
        }

        public IElement IgnoreStockCheckWrapper()
        {
            return StockCheckWrapper;
        }

        public IElement IgnoreCertonaDrawerName()
        {
            return CertonaDrawerName;
        }

        public IElement IgnoreStickyHeader()
        {
            return StickyWrapper;
        }

        public IElement CompleteLookSection()
        {
            return CompleteTheLookSection;
        }

        public IElement IgnoreMoreYouMayLikeContainer()
        {
            return MoreYouMayLikeContainer;
        }

        public IElement GetMediaModalContentModal()
        {
            return MediaModalContentModal;
        }

        public IElement GetTurnToReviewModal()
        {
            return TurnToReviewModal;
        }

        public IElement GetEnergyInfoModal()
        {
            return EnergyInfoModal;
        }

        public IElement GetTurnToQuestionsAndAnswersSection()
        {
            return TurnToQuestionsAndAnswersSection;
        }

        public IElement IgnoreCustomerPhotoTab()
        {
            return CustomerPhotoTab;
        }
    }
}