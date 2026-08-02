using System.Threading;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.IntegrationTests.Pages.Shipping
{
    public class ShippingInfoLocatorDesktopTest : ShippingInfoLocatorTest
    {
        /// <summary>
        /// Tests to ensure this page can find all its IElements.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public ShippingInfoLocatorDesktopTest(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the Shipping Info page elements could be located.
        /// </summary>
        [Trait(LpTraits.Integration.PageObjectModel, "Shipping")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void LocateElementsOnShippingInfoPageTest(string config) => Locate(config);

        protected override void VerifyAddNewAddressModalElements()
        {
            //Verifying "Add New Address" Modal Elements
            VerifyElementDisplayed(() => CustomerAddressInformation.ShipToDifferentAddressButton);
            Browser.Wait.ForClickableElement(CustomerAddressInformation.ShipToDifferentAddressButton).Click();
            VerifyElementDisplayed(() => CustomerAddressInformation.ChangeShippingApplyButton);
            VerifyElementDisplayed(() => CustomerAddressInformation.SelectShippingAddressModal);

            VerifyElementNotImplemented(() => Shipping.ShippingPageCartInfo);
            VerifyElementNotImplemented(() => Shipping.ShippingPageCartNumber);
            VerifyElementNotImplemented(() => Shipping.NewShippingAddressFormContainer);
            VerifyElementNotImplemented(() => Shipping.ShippingInformationPageContainer);
            VerifyElementNotImplemented(() => Shipping.CloseShippingPage);
            VerifyElementNotImplemented(() => Shipping.SelectNonDefaultAddress);
            VerifyElementNotImplemented(() => Shipping.NewShippingAddressFormFullContent);
        }

        protected override void VerifyNewShippingInformation()
        {
            VerifyElementDisplayed(() => CustomerAddressInformation.ShippingInformationModal);

            CustomerAddressInformation.FirstNameField.SendKeys("Lptest");
            CustomerAddressInformation.LastNameField.SendKeys("Lptest");
            CustomerAddressInformation.StreetAddressField.SendKeys("1 Main St.");
            CustomerAddressInformation.CityField.SendKeys("Los Angeles");
            CustomerAddressInformation.SelectState(CustomerAddressInformation.StateField, StateCodeListUnitedStates.CA);
            CustomerAddressInformation.ZipPostalCodeField.SendKeys("90001");
            CustomerAddressInformation.PhoneField.SendKeys("1234567890");

            CustomerAddressInformation.SaveAddressFromModalButton.Click();

            Thread.Sleep(2000);

            CustomerAddressInformation.ShipToDifferentAddressButton.Click();

            Browser.Wait.ForDisplayedElement(GlobalLocators.Iframe);

            VerifyElementDisplayed(() => CustomerAddressInformation.ShippingAddressOption);
        }

        protected override void SignOut()
        {
            SignInWorkflow.SignOut();
        }

        protected override void EnterZipCodeOnShippingOptionsModal()
        {
            CartOverview.ShipZipField.Clear();
            CartOverview.ShipZipField.SendKeys(ZipCodeList.Chatsworth);
        }

        protected override void CountrySelection()
        {
            VerifyElementNotImplemented(() => CustomerAddressInformation.CountrySelection);
        }

        protected override void VerifyShippingOptionsChanged()
        {
            CustomerAddressInformation.FillFormControlByText(CustomerAddressInformation.ZipPostalCodeField, ZipCodeList.Chatsworth);

            CustomerAddressInformation.ShowStateLink.Click();
            CustomerAddressInformation.FillFormSelectByValue(CustomerAddressInformation.StateField, StateCodeListUnitedStates.CA);
            VerifyElementNotImplemented(() => CustomerAddressInformation.StateSelection);

            Thread.Sleep(5000);

            CustomerAddressInformation.FillFormSelectByValue(CustomerAddressInformation.StateField, StateCodeListUnitedStates.AK);

            Browser.Wait.ForDisplayedElement(CustomerAddressInformation.ShippingOptionsChangedMessage);

            VerifyElementDisplayed(() => Shipping.ShippingCellShippingCost);
            VerifyElementDisplayed(() => CustomerAddressInformation.ShippingOptionsChangedMessage);
        }

        protected override void VerifySaveAddressCheckboxInput()
        {
            VerifyElementNotImplemented(() => CustomerAddressInformation.SaveAddressCheckboxInput);
        }
    }


    public class ShippingInfoLocatorMobileTest : ShippingInfoLocatorTest
    {
        /// <summary>
        /// Tests to ensure this page can find all its IElements.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public ShippingInfoLocatorMobileTest(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the Shipping Info page elements could be located.
        /// </summary>
        [Trait(LpTraits.Integration.PageObjectModel, "Shipping")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void LocateElementsOnShippingInfoPageTest(string config) => Locate(config);

        protected override void VerifyAddNewAddressModalElements()
        {
            VerifyElementNotImplemented(() => CustomerAddressInformation.ShipToDifferentAddressButton);
            VerifyElementNotImplemented(() => CustomerAddressInformation.ChangeShippingApplyButton);
            CustomerAddressInformation.ShippingAddressInfoContainer.Click();
            Browser.Wait.ForClickableElement(CustomerAddressInformation.AddNewAddressButton);

            VerifyElementDisplayed(() => Shipping.NewShippingAddressFormContainer);
            VerifyElementDisplayed(() => Shipping.CloseShippingPage);
            VerifyElementDisplayed(() => Shipping.NewShippingAddressFormFullContent);
        }

        protected override void SignOut()
        {
            Browser.Navigate("https://www.lampsplus.com/account/sign-out");
        }

        protected override void EnterZipCodeOnShippingOptionsModal()
        {
            CartOverview.ShippingZipField.Clear();
            CartOverview.ShippingZipField.SendKeys(ZipCodeList.Chatsworth);
        }

        protected override void CountrySelection()
        {
            CustomerAddressInformation.CountryField.Click();
            Browser.Wait.ForElementToStopAnimating(GlobalLocators.CountryDropdown);
            VerifyElementDisplayed(() => CustomerAddressInformation.CountrySelection);
            CustomerAddressInformation.CountrySelection.Click();
        }

        protected override void VerifyShippingOptionsChanged()
        {
            VerifyElementDisplayed(() => Shipping.ShippingPageCartInfo);
            VerifyElementDisplayed(() => Shipping.ShippingPageCartNumber);

            var shippingAddressAk = new Address() { State = StateCodeListUnitedStates.AK };

            CustomerAddressInformation.FillFormControlByText(CustomerAddressInformation.ZipPostalCodeField, ZipCodeList.Chatsworth);
            CustomerAddressInformation.ShowStateLink.Click();

            Browser.Wait.ForElementToStopAnimating(GlobalLocators.StateDropdown);
            VerifyElementDisplayed(() => CustomerAddressInformation.StateSelection);
            CustomerAddressInformation.StateSelection.Click();

            CustomerAddressInformation.ZipPostalCodeField.Clear();
            CustomerAddressInformation.FillFormControlByText(CustomerAddressInformation.ZipPostalCodeField, ZipCodeList.Anchorage);
            CustomerAddressInformation.ShowStateLink.Click();
            ShoppingCartWorkflow.SelectState(shippingAddressAk);

            CustomerAddressInformation.ZipPostalCodeField.Clear();
            CustomerAddressInformation.FillFormControlByText(CustomerAddressInformation.ZipPostalCodeField, ZipCodeList.Chatsworth);
            CustomerAddressInformation.ShowStateLink.Click();

            Browser.Wait.ForDisplayedElement(CustomerAddressInformation.ShippingOptionsChangedMessage);

            VerifyElementDisplayed(() => Shipping.ShippingCellShippingCost);
            VerifyElementDisplayed(() => CustomerAddressInformation.ShippingOptionsChangedMessage);
        }

        protected override void VerifyNewShippingInformation()
        {
            CustomerAddressInformation.FirstNameField.SendKeys("Lptest");
            CustomerAddressInformation.LastNameField.SendKeys("Lptest");
            CustomerAddressInformation.StreetAddressField.SendKeys("1 Main St.");
            CustomerAddressInformation.CityField.SendKeys("Los Angeles");
            CustomerAddressInformation.SelectState(CustomerAddressInformation.StateField, StateCodeListUnitedStates.CA);
            CustomerAddressInformation.ZipPostalCodeField.SendKeys("90001");
            CustomerAddressInformation.PhoneField.SendKeys("1234567890");

            CustomerAddressInformation.SaveAddressFromModalButton.Click();

            Thread.Sleep(2000);

            VerifyElementNotImplemented(() => CustomerAddressInformation.SelectShippingAddressModal);
            VerifyElementNotImplemented(() => CustomerAddressInformation.ShippingAddressOption);
            VerifyElementNotImplemented(() => CustomerAddressInformation.ShippingInformationModal);

            VerifyElementDisplayed(() => Shipping.SelectNonDefaultAddress);
        }

        protected override void VerifySaveAddressCheckboxInput()
        {
            VerifyElementDisplayed(() => Shipping.ShippingInformationPageContainer);
            VerifyElementExists(() => CustomerAddressInformation.SaveAddressCheckboxInput);
        }
    }


    public abstract class ShippingInfoLocatorTest : PageObjectTestsBase
    {
        /// <summary>
        /// Tests to ensure this page can find all its IElements.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        protected ShippingInfoLocatorTest(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the Shipping Info page elements could be located.
        /// </summary>
        public void Locate(string config)
        {
            InitializeFramework(config);

            // Obtain a list of elements on the Shipping Info page
            BuildElementsList(Shipping);
            BuildElementsList(CustomerAddressInformation);

            //Navigate to Shipping Info Page
            ShoppingCartWorkflow.AddMultipleItemsToCart(Urls.AllChandeliersSortPageUrl, 2);

            Browser.Wait.ForClickableElement(CartOverview.CheckOutNowButton);

            Browser.Navigate(Urls.ShippingPageUrl);
            Browser.Wait.ForDomReady();

            //Verify Customer Information's Elements
            VerifyElementDisplayed(() => CustomerAddressInformation.AddressContainerElement);
            VerifyElementDisplayed(() => CustomerAddressInformation.FirstNameField);
            VerifyElementDisplayed(() => CustomerAddressInformation.LastNameField);
            VerifyElementDisplayed(() => CustomerAddressInformation.StreetAddressField);
            VerifyElementDisplayed(() => CustomerAddressInformation.ApartmentSuiteOtherField);
            VerifyElementDisplayed(() => CustomerAddressInformation.CityField);
            VerifyElementDisplayed(() => CustomerAddressInformation.StateField);
            VerifyElementDisplayed(() => CustomerAddressInformation.ShowStateLink);
            VerifyElementDisplayed(() => CustomerAddressInformation.ZipPostalCodeField);
            VerifyElementDisplayed(() => CustomerAddressInformation.PhoneField);
            VerifyElementDisplayed(() => CustomerAddressInformation.ProceedToPaymentButton);
            VerifyElementDisplayed(() => CustomerAddressInformation.ShowCountryLink);

            Browser.Wait.ForClickableElement(CustomerAddressInformation.ShowCountryLink).Click();

            VerifyElementDisplayed(() => CustomerAddressInformation.CountryField);

            CountrySelection();

            VerifyElementDisplayed(() => CustomerAddressInformation.SaveAddressCheckbox);

            // Verifying ShippingOptionsChangedMessage Element
            VerifyShippingOptionsChanged();

            // Verifying FedExAddressValidationModal Element
            ShoppingCartWorkflow.ShowFedExValidationModal();

            Thread.Sleep(2000); // Required to avoid stale element exception. None of the existing wait methods works here.

            VerifyElementDisplayed(() => CustomerAddressInformation.FedExAddressValidationModal);
            VerifyElementDisplayed(() => CustomerAddressInformation.FedExAddressValidationHeader);
            VerifyElementDisplayed(() => CustomerAddressInformation.FedExShippingAddress1);
            VerifyElementDisplayed(() => CustomerAddressInformation.FedExShippingAddress2);
            VerifyElementDisplayed(() => CustomerAddressInformation.FedExShippingCity);
            VerifyElementDisplayed(() => CustomerAddressInformation.FedExShippingState);
            VerifyElementDisplayed(() => CustomerAddressInformation.FedExShippingZipCode);
            VerifyElementDisplayed(() => CustomerAddressInformation.EditMaintainCurrentAddressLink);
            VerifyElementDisplayed(() => CustomerAddressInformation.NoChangeAddressRadioElement);
            VerifyElementDisplayed(() => CustomerAddressInformation.SuggestedAddressRadioElement);
            VerifyElementDisplayed(() => CustomerAddressInformation.SuggestedAddressElement);
            VerifyElementDisplayed(() => CustomerAddressInformation.SubmitChangesElement);

            // Verifying GoogleAutocompleteElement Element
            CustomerAddressInformation.FillStreetAddressFieldAndLetGoogleSuggestionAct("Am");
            VerifyElementDisplayed(() => CustomerAddressInformation.GoogleAutocompleteElement);
            
            // Verifying Saved Address Elements
            ShoppingCartWorkflow.CreateNewSavedAddress(new Address { State = StateCodeListUnitedStates.CA },
                false);

            Browser.Wait.ForClickableElement(Payment.PlaceOrderButton);

            Browser.Navigate(Urls.ShippingPageUrl);

            // Ship to multiple addresses.
            VerifyElementDisplayed(() => Shipping.ShipToMultipleAddressesButton);
            VerifySaveAddressCheckboxInput();

            VerifyElementDisplayed(() => CustomerAddressInformation.SavedAddressFullName);
            VerifyElementDisplayed(() => CustomerAddressInformation.SavedAddressShippingInfo);
            VerifyElementDisplayed(() => CustomerAddressInformation.ShippingAddressInfoContainer);

            VerifyAddNewAddressModalElements();

            VerifyElementDisplayed(() => CustomerAddressInformation.AddNewAddressButton);
            Browser.Wait.ForClickableElement(CustomerAddressInformation.AddNewAddressButton).Click();
            Thread.Sleep(2000);
            VerifyElementDisplayed(() => CustomerAddressInformation.SaveAddressFromModalButton);

            VerifyNewShippingInformation();

            // Verifying the rest of Shipping Page objects 
            // Adding White Glove Product to check Shipping Notification Elements
            Browser.Navigate(Urls.HomePageUrl);

            SignOut();

            var response = ProductActions.GetSkuWithWhiteGloveShipping;
            var url = $"https://www.lampsplus.com/products/{response}";

            Browser.Navigate(url);
            Browser.Wait.ForClickableElement(GlobalLocators.AddToCartButton).Click();
            Browser.Wait.ForClickableElement(CartOverview.ChangeShippingOptionsLink).Click();

            EnterZipCodeOnShippingOptionsModal();

            Browser.Wait.ForClickableElement(CartOverview.ShipTabSearchButton).Click();
            CartOverview.WhiteGloveShippingOption.Click();
            Browser.Wait.ForClickableElement(CartOverview.UpdateShipButton, 20).Click();

            Browser.Wait.UntilElementDoesntExist(GlobalLocators.LpModalId);

            Browser.Navigate(Urls.ShippingPageUrl);

            Browser.Wait.ForElement((Shipping.ShippingPage), 10);
            VerifyElementDisplayed(() => Shipping.ShippingPage);
            VerifyElementDisplayed(() => CustomerAddressInformation.EmailField);
            VerifyElementDisplayed(() => Shipping.EmailField);
            VerifyElementDisplayed(() => Shipping.ProceedToPaymentElement);

            VerifyCreateNewSavedAddress(new Address { State = StateCodeListUnitedStates.CA });
        }

        protected void VerifyCreateNewSavedAddress(Address address = null)
        {
            CustomerAddressInformation.Navigate(Urls.ShippingPageUrl);

            var shippingAddress = address ?? new Address();
            shippingAddress.SaveToProfile = true;

            CustomerAddressInformation.EnterShippingAddress(shippingAddress);

            // clicking Proceed to Payment button enables the full address to be saved
            Browser.Wait.ForClickableElement(CustomerAddressInformation.ProceedToPaymentButton).Click();

            Browser.Wait.UntilElementUnloads(CustomerAddressInformation.ProceedToPaymentButton);
            Browser.Wait.ForDomReady();

            if (Browser.PageUrl == Urls.ShippingNotificationPageUrl)
            {
                VerifyElementDisplayed(() => Shipping.ShippingNotification);
                VerifyElementDisplayed(() => Shipping.ShippingNotificationProceedToPaymentButton);
                Shipping.ShippingNotificationProceedToPaymentButton.Click();
            }
        }

        protected abstract void VerifyAddNewAddressModalElements();

        protected abstract void SignOut();

        protected abstract void EnterZipCodeOnShippingOptionsModal();

        protected abstract void CountrySelection();

        protected abstract void VerifyShippingOptionsChanged();

        protected abstract void VerifySaveAddressCheckboxInput();

        protected abstract void VerifyNewShippingInformation();
    }
}
