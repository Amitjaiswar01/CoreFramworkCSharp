using Automation.Framework;
using Automation.Framework.Utilities;
using OpenQA.Selenium;

namespace LampsPlus.AutomationFramework.Pages.Refactored.ContactUs
{
    public class ContactUsDesktop : IContactUsDesktop
    {
        //Class members
        private string _emailUsButtonClass = "contact-email";

        //Instances
        protected IBrowser Browser;

        public ContactUsDesktop(IBrowser browser)
        {
            Browser = browser;
        }

        //Interface implementation
        public string PageTitle { get; }
        public string PageUrl { get; }
        public virtual bool IsCurrentPage => Browser.Wait.IsVisibleElement(By.CssSelector(_emailUsButtonClass.ToCssClassSelector()));
    }
}