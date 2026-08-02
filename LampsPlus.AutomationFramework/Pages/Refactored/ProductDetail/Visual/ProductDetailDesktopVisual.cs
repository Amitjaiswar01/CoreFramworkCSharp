using Automation.Framework;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Databases.Actions;
using LampsPlus.AutomationFramework.Pages.Refactored.Modal;
using OperatingSystem = LampsPlus.AutomationFramework.Utilities.TestConfiguration.OperatingSystem;

namespace LampsPlus.AutomationFramework.Pages.Refactored.ProductDetail.Visual
{
    public class ProductDetailDesktopVisual : ProductDetailDesktop, IProductDetailDesktopVisual
    {
        public ProductDetailDesktopVisual(IBrowser browser, ProductActions productActions, IAssert assert, OperatingSystem operatingSystem, IModalDesktop modal) : base(browser, productActions, assert, operatingSystem, modal)
        {
        }

        public IElement IgnoreStockCheckWrapper()
        {
            return StockCheckWrapper;
        }

        public IElement IgnoreStickyHeader()
        {
            return StickyWrapper;
        }

        public IElement IgnoreQuestionsAndAnswersSection()
        {
            return QuestionsAndAnswersSection;
        }

        public IElement IgnoreReviewsSection()
        {
            return TurnToReviewSection;
        }

        public IElement IgnoreMoreYouMayLikeSection()
        {
            return PdMymlSection;
        }

        public IElement IgnoreCustomerPhotoTab()
        {
            return ImageTabContent;
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

        public virtual IElement GetTurnToQuestionsAndAnswersSection()
        {
            Browser.ScrollIntoView(TurnToQuestionsAndAnswersSection);
            return TurnToQuestionsAndAnswersSection;
        }
    }
}