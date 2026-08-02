using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Payment;
using LampsPlus.AutomationFramework.Workflow.Base;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace LampsPlus.AutomationFramework.Workflow.Mobile
{
    /// <summary>
    /// Common behavior for managing account.
    /// </summary>
    public class MobileManageAccountWorkflow : ManageAccountWorkflowBase
    {
        public MobileManageAccountWorkflow(TestsBase testsBase) : base(testsBase) { }

        public const string StateShippingParentBaseSelector = "#lpSelectMobileDrawer__ddlState";
        public const string StateShippingParentSelector = "#lpSelectMobileDrawer__ddlState > div > div.lpScrollContainer > ul";
        public const string NewStateShippingParentSelector = "#lpSelectMobileDrawer__singleShippingState > div > div.lpScrollContainer > ul";
        public const string CountryDropDownSelector = "//*[@for='ddlCountry']//following-sibling::div[1]/button";


        /// <inheritdoc />
        public override void AddNewPaymentMethod(CreditCard creditCard, Address address)
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(TestsBase.ManageAccount.AddPaymentOptionClass.ToCssClassSelector()));
            Browser.ClickByJs(TestsBase.ManageAccount.AddPaymentOptionButton);
            Browser.Wait.ForMobileModalToFullyOpen(TestsBase.ManageAccount.PaymentOptionDrawer);

            TestsBase.ManageAccount.SetPaymentCard(creditCard);
            TestsBase.ManageAccount.SetPaymentAddress(address);

            Browser.ScrollIntoView(TestsBase.ManageAccount.SavePaymentBtn);
            Browser.Wait.IsVisibleElement(By.CssSelector(TestsBase.ManageAccount.SavePaymentOptionButtonClass.ToCssClassSelector()));
            TestsBase.ManageAccount.SavePaymentBtn.Click();

            WaitForSavedPaymentOptionToRender();
        }

        /// <inheritdoc />
        public override void WaitForModalToFullyClose()
        {
            Browser.Wait.IsInvisibleElement(By.ClassName(TestsBase.Shipping.FedExAddressValidationClass));
        }

        private void WaitForSavedPaymentOptionToRender()
        {
            Browser.Wait.ForMobileModalToFullyClose(TestsBase.ManageAccount.PaymentOptionDrawer);

            // On Mobile, after saving payment option, the site (using CSS transition) changes the opacity of the container of old payment option to 0.
            // Then it updates the old payment option with new payment option, then it sets the container to opacity 1 to show the updated content.
            // That's why we have to wait for opacity 1 to make sure the updated payment option is fully rendered on the page.
            Browser.Wait.ForCondition(() => Browser.GetElementOpacity(TestsBase.ManageAccount.SavedPaymentOptions) == "1");
        }

        public override void AddShippingAddressFromModal(Address shippingAddress, bool isIntAddress = false)
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(TestsBase.ManageAccount.FirstNameClass.ToCssClassSelector()));
            TestsBase.ManageAccount.FirstNameField.SendKeys(shippingAddress.FirstName);
            TestsBase.ManageAccount.LastNameField.SendKeys(shippingAddress.LastName);
            TestsBase.ManageAccount.ShippingAddressLineOneField.SendKeys(shippingAddress.AddressLine1);
            TestsBase.ManageAccount.ShippingAddressLineTwoField.SendKeys(shippingAddress.AddressLine2);
            TestsBase.ManageAccount.ShippingCityField.SendKeys(shippingAddress.City);

            if (!string.IsNullOrWhiteSpace(shippingAddress.Country))
            {
                TestsBase.ManageAccount.ShowCountryLink.Click();
                new SelectElement(TestsBase.ManageAccount.DdlCountryField.InternalElement).SelectByValue(shippingAddress.Country);
            }

            IElement stateDropDown = Browser.Locate.ElementByXpath(TestsBase.ManageAccount.SelectStateSelector);
            stateDropDown.Click();


            if (!isIntAddress)
                GlobalLocators.ClickDropdownByValue(Browser.Locate.ElementBySelector(StateShippingParentSelector), shippingAddress.State);
            else
                new SelectElement(TestsBase.ManageAccount.ShippingStateField.InternalElement).SelectByText(shippingAddress.State);//TODO to test with Int address

            Browser.Wait.ForDomReady();
            TestsBase.ManageAccount.ShippingZipCodeField.SendKeys(shippingAddress.ZipCode);
            Browser.Wait.ForElement(TestsBase.ManageAccount.ShippingPhoneField);
            TestsBase.ManageAccount.ShippingPhoneField.SendKeys(shippingAddress.Phone);
            Browser.Wait.ForDomReady();
            Browser.ScrollIntoView(TestsBase.ManageAccount.BtnSaveShippingAddress);
            TestsBase.ManageAccount.BtnSaveShippingAddress.Click();

            Browser.Wait.ForClickableElement(TestsBase.ManageAccount.BtnAddShippingAddress);
        }

        public override void AddNewShippingAddressToModal(Address shippingAddress)
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(TestsBase.ManageAccount.TxtFirstNameId.ToCssIdSelector()));
            TestsBase.ManageAccount.ShippingFirstNameField.SendKeys(shippingAddress.FirstName);
            TestsBase.ManageAccount.ShippingLastNameField.SendKeys(shippingAddress.LastName);
            TestsBase.ManageAccount.ShippingAddressLineOneField.SendKeys(shippingAddress.AddressLine1);
            Browser.Wait.IsVisibleElement(By.CssSelector(TestsBase.ManageAccount.TxtAddress2Id.ToCssIdSelector()));
            TestsBase.ManageAccount.ShippingAddressLineTwoField.SendKeys(shippingAddress.AddressLine2);
            TestsBase.ManageAccount.ShippingCityField.SendKeys(shippingAddress.City);

            if (!string.IsNullOrWhiteSpace(shippingAddress.Country))
            {
                TestsBase.ManageAccount.ShowCountryLink.Click();
                TestsBase.ManageAccount.SelectShippingDropDownByValue(Browser.Locate.ElementByXpath(CountryDropDownSelector), shippingAddress.Country);
            }

            Browser.Wait.ForClickableElement(TestsBase.ManageAccount.ShippingStateField, 5);
            TestsBase.ManageAccount.ShippingStateField.Click();

            if (shippingAddress.State == "N/A")
            {
                Browser.Wait.IsVisibleElement(By.XPath($"//*[@data-text='{shippingAddress.State}']"));
                var valueElement = Browser.Locate.ElementByXpath($"//*[@data-text='{shippingAddress.State}']");
                Browser.ScrollIntoView(valueElement);
                valueElement.Click();
            }
            else
            {
                GlobalLocators.ClickDropdownByValue(Browser.Locate.ElementBySelector(StateShippingParentBaseSelector), shippingAddress.State);
            }

            Browser.Wait.IsInvisibleElement(By.CssSelector(StateShippingParentBaseSelector));
            Browser.Wait.IsVisibleElement(By.CssSelector(TestsBase.ManageAccount.TxtShippingZipCodeId.ToCssIdSelector()));
            TestsBase.ManageAccount.ShippingZipCodeField.SendKeys(shippingAddress.ZipCode);
            TestsBase.ManageAccount.ShippingPhoneField.SendKeys(shippingAddress.Phone);

            //handle fedex modal if present
            if (TestsBase.CustomerAddressInformation.DoesFedExModalShow())
            {
                TestsBase.CustomerAddressInformation.NoChangeAddressRadioElement.Click();
                TestsBase.CustomerAddressInformation.SubmitChangesElement.Click();
                TestsBase.CustomerAddressInformation.SaveAddressCheckboxInput.Click();//TODO Added Save button click
                TestsBase.ManageAccountWorkflow.WaitForModalToFullyClose();
            }
        }

        public override void DeleteAllSavedPaymentOptions()
        {
            TestsBase.Browser.Navigate(Urls.ManageAccountPageUrl);
            Browser.Wait.IsVisibleElement(By.XPath(TestsBase.ManageAccount.EditEmailPrefXpath));
            if (!TestsBase.ManageAccount.ManagePaymentOptionsLinkForElement.IsInitialized || !TestsBase.ManageAccount.ManagePaymentOptionsLinkForElement.Displayed) { return; }
            var rewardNumber = TestsBase.ManageAccount.RewardNumber;
            TestsBase.AccountActions.ResetPaymentOptions(rewardNumber);
        }
    }
}
