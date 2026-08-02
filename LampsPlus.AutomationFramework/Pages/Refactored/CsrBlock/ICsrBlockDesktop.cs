using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.CsrBlock
{
    public interface ICsrBlockDesktop : IPageObjectModel
    {
        void SetSaleSourceValue();
        void ApplyCartLevelDiscount(decimal percentDiscount);
        void SetReasonCodeValue();
        void ApplyShippingAndProcessingCost(string value);
        IElement GetMdPercentageButton();
        bool IsManualDiscountManagerApprovalFormDisplayed { get; }
        bool IsSaleSourceFieldDisplayed { get; }
        bool IsCsrPanelVisible { get; }
    }
}
