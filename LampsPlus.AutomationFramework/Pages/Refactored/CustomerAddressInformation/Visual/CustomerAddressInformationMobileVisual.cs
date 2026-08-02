using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Refactored.Address;

namespace LampsPlus.AutomationFramework.Pages.Refactored.CustomerAddressInformation.Visual
{
    public class CustomerAddressInformationMobileVisual : CustomerAddressInformationMobile, ICustomerAddressInformationMobileVisual
    {
        public CustomerAddressInformationMobileVisual(IBrowser browser, Log log, SessionSettings settings, IAddress address) : base(browser, log, settings, address)
        {
        }

        public IElement GetEmailField()
        {
            return EmailField;
        }
    }
}
