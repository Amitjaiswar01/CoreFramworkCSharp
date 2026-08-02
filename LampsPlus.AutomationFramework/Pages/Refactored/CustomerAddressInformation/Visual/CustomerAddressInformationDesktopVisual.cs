using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Refactored.Address;

namespace LampsPlus.AutomationFramework.Pages.Refactored.CustomerAddressInformation.Visual
{
    public class CustomerAddressInformationDesktopVisual : CustomerAddressInformationDesktop, ICustomerAddressInformationDesktopVisual
    {
        public CustomerAddressInformationDesktopVisual(IBrowser browser, Log log, SessionSettings settings, IAddress address) : base(browser, log, settings, address)
        {
        }

        public IElement GetFedExAddressValidationModal()
        {
            return FedExAddressValidationModal;
        }

        public IElement GetEmailField()
        {
            return EmailField;
        }
    }
}
