using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Payment;
using OpenQA.Selenium.Support.UI;

namespace LampsPlus.AutomationFramework.Workflow.Base
{
	/// <summary>
	/// Common behavior for managing account.
	/// </summary>
	public abstract class ManageAccountWorkflowBase : WorkflowBase, IManageAccountWorkflow
    {
        protected ManageAccountWorkflowBase(TestsBase testsBase) : base(testsBase)
        {
            Framework = testsBase;

        }

        internal TestsBase Framework;

        /// <inheritdoc />
        public void DeleteAllSavedAddresses()
		{
            TestsBase.Browser.Navigate(Urls.ManageAccountPageUrl);

            Browser.ScrollToElement(TestsBase.ManageAccount.ManageShippingAddressesLinkForElement);
            Browser.Wait.ForDisplayedElement(TestsBase.ManageAccount.ManageShippingAddressesLinkForElement,30);

            if (!TestsBase.ManageAccount.ManageShippingAddressesLinkForElement.Displayed) { return; }

			var rewardNumber = TestsBase.ManageAccount.RewardNumber;
			TestsBase.AccountActions.ResetShippingAddresses(rewardNumber);
		}

        /// <inheritdoc />        
        public virtual void AddShippingAddressFromModal(Address shippingAddress, bool isIntAddress = false)
		{
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

			if (!isIntAddress)
				new SelectElement(TestsBase.ManageAccount.ShippingStateField.InternalElement).SelectByValue(shippingAddress.State);
			else
				new SelectElement(TestsBase.ManageAccount.ShippingStateField.InternalElement).SelectByText(shippingAddress.State);

			TestsBase.ManageAccount.ShippingZipCodeField.SendKeys(shippingAddress.ZipCode);
			TestsBase.ManageAccount.ShippingPhoneField.SendKeys(shippingAddress.Phone);
			Browser.Wait.ForClickableElement(TestsBase.ManageAccount.BtnSaveShippingAddress).Click();

		    WaitForModalToFullyClose();
            Browser.Wait.ForClickableElement(TestsBase.ManageAccount.BtnAddShippingAddress);
        }

        public virtual void AddNewShippingAddressToModal(Address shippingAddress)
        {
            TestsBase.ManageAccount.ShippingFirstNameField.SendKeys(shippingAddress.FirstName);
            TestsBase.ManageAccount.ShippingLastNameField.SendKeys(shippingAddress.LastName);
            TestsBase.ManageAccount.ShippingAddressLineOneField.SendKeys(shippingAddress.AddressLine1);
            TestsBase.ManageAccount.ShippingAddressLineTwoField.SendKeys(shippingAddress.AddressLine2);
            TestsBase.ManageAccount.ShippingCityField.SendKeys(shippingAddress.City);

            if (!string.IsNullOrWhiteSpace(shippingAddress.Country))
            {
                TestsBase.ManageAccount.ShowCountryLink.Click();
                new SelectElement(TestsBase.ManageAccount.ShippingCountryOption.InternalElement).SelectByValue(shippingAddress.Country);
            }

            Browser.Wait.ForClickableElement(TestsBase.ManageAccount.ShippingStateField, 5);

            if (shippingAddress.State == "N/A")
            {
                TestsBase.ManageAccount.ShippingStateField.Click();
                Browser.Wait.ForElementToStopAnimating(TestsBase.ManageAccount.ShippingStateField);
                new SelectElement(TestsBase.ManageAccount.ShippingStateField.InternalElement).SelectByText("N/A");
            }
            else
                new SelectElement(TestsBase.ManageAccount.ShippingStateField.InternalElement).SelectByValue(shippingAddress.State);


            TestsBase.ManageAccount.ShippingZipCodeField.SendKeys(shippingAddress.ZipCode);
            TestsBase.ManageAccount.ShippingPhoneField.SendKeys(shippingAddress.Phone);

            //handle fedex modal if present
            if (TestsBase.CustomerAddressInformation.DoesFedExModalShow())
            {
                TestsBase.CustomerAddressInformation.NoChangeAddressRadioElement.Click();
                TestsBase.CustomerAddressInformation.SubmitChangesElement.Click();
                TestsBase.CustomerAddressInformation.SaveAddressCheckboxInput.Click();
                TestsBase.ManageAccountWorkflow.WaitForModalToFullyClose();
            }
        }

		/// <inheritdoc />
		public void AddNewDefaultPaymentMethod() => AddNewPaymentMethod(CreditCards.TestVisaCard, new Address());

		public abstract void AddNewPaymentMethod(CreditCard creditCard, Address address);

	    /// <inheritdoc />
	    public abstract void WaitForModalToFullyClose();

        /// <inheritdoc />
        public abstract void DeleteAllSavedPaymentOptions();
    }
}
