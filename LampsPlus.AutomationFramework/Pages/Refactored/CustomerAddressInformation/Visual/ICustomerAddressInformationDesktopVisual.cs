using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.CustomerAddressInformation.Visual
{
    public interface ICustomerAddressInformationDesktopVisual : ICustomerAddressInformationDesktop
    {
        IElement GetFedExAddressValidationModal();

        IElement GetEmailField();
    }
}
