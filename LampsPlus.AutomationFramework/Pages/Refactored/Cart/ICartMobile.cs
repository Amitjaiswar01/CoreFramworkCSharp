
namespace LampsPlus.AutomationFramework.Pages.Refactored.Cart
{
    public interface ICartMobile : ICartDesktop
    {
        void CheckOutFromCartPage();
        void CloseEmailModal();
        void UpdateZipCodeFlow(string zipCode);
        decimal GetPromoCodeDiscountDisplayed();
        bool IsPromoCodeMessageVisible();
        void ScrollToPromoCodeSection();
    }
}