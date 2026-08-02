using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.IntegrationTests.Pages.ContactUs
{
    public class ContactUsLocatorDesktopTest : ContactUsLocatorTests
    {
        public ContactUsLocatorDesktopTest(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Integration.PageObjectModel, "ContactUs")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LocateContactUsElementsTest(string config) => Locate(config);

        protected override void VerifyAnonymousElements()
        {
            VerifyElementDisplayed(() => ContactUs.SubCategoryDropdown);
            VerifyElementDisplayed(() => ContactUs.SubjectInput);
            VerifyElementDisplayed(() => ContactUs.CommentsInput);
            VerifyElementDisplayed(() => ContactUs.EmailOptInCheckbox);
            VerifyElementDisplayed(() => ContactUs.SubmitButton);
            VerifyElementDisplayed(() => ContactUs.FormWrapper);
            VerifyElementDisplayed(() => ContactUs.SendEmailButton);
        }
    }


    public class ContactUsLocatorMobileTest : ContactUsLocatorTests
    {
        public ContactUsLocatorMobileTest(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Integration.PageObjectModel, "ContactUs")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LocateContactUsElementsTest(string config) => Locate(config);

        protected override void VerifyAnonymousElements()
        {
            Browser.Locate.ClickDropdownByValue(ContactUs.CategoryDropdown, "Payment and Billing");

            VerifyElementDisplayed(() => ContactUs.SubCategoryDropdown);
            VerifyElementDisplayed(() => ContactUs.SubjectInput);
            VerifyElementDisplayed(() => ContactUs.CommentsInput);
            VerifyElementDisplayed(() => ContactUs.EmailOptInCheckbox);
            VerifyElementDisplayed(() => ContactUs.SubmitButton);
            VerifyElementDisplayed(() => ContactUs.FormWrapper);

            Browser.ScrollToElement(ContactUs.SendEmailButton);
           
            VerifyElementDisplayed(() => ContactUs.SendEmailButton);
        }
    }


    /// <summary>
    /// Tests to ensure all IElements and Lists of IElements can be found on the given page object.
    /// </summary>
    [Trait(LpTraits.Integration.PageObjectModel, "ContactUs")]
    public abstract class ContactUsLocatorTests : PageObjectTestsBase
    {
        protected ContactUsLocatorTests(ITestOutputHelper output) : base(output) { }

        public void Locate(string config)
        {
            InitializeFramework(config, Urls.ContactUsPageUrl);
            BuildElementsList(ContactUs);

            VerifyElementDisplayed(() => ContactUs.FirstNameInput);
            VerifyElementDisplayed(() => ContactUs.LastNameInput);
            VerifyElementDisplayed(() => ContactUs.EmailAddressInput);
            VerifyElementDisplayed(() => ContactUs.CategoryDropdown);

            VerifyAnonymousElements();
        }

        protected abstract void VerifyAnonymousElements();
    }
}
