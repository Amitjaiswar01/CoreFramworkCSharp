using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Utilities;

namespace LampsPlus.AutomationFramework.Pages.Refactored.ContactUs
{
    public class ContactUsMobile : ContactUsDesktop, IContactUsMobile
    {
        //Class members
        private string _hoursContainerClass = "hoursContainer";

        public ContactUsMobile(IBrowser browser) : base(browser) { }

        //Interface implementation
        public override bool IsCurrentPage => Browser.Wait.IsVisibleElement(By.CssSelector(_hoursContainerClass.ToCssClassSelector()));
    }
}