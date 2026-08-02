using Xunit;
using Xunit.Abstractions;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Pages.Refactored.Address;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Shipping.T7523_T7524_VerifyFedExModalShowsAptOptions
{
    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T7523_Windows_VerifyFedExModalShowsAptOptions : T7523_DesktopBase
    {
        public T7523_Windows_VerifyFedExModalShowsAptOptions(ITestOutputHelper output) : base(output)
        {
        }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void FedExModalApartmentOptions(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T7523_Mac_VerifyFedExModalShowsAptOptions : T7523_DesktopBase
    {
        public T7523_Mac_VerifyFedExModalShowsAptOptions(ITestOutputHelper output) : base(output)
        {
        }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void FedExModalApartmentOptions(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T7523_iPad_VerifyFedExModalShowsAptOptions : T7523_DesktopBase
    {
        public T7523_iPad_VerifyFedExModalShowsAptOptions(ITestOutputHelper output) : base(output)
        {
        }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void FedExModalApartmentOptions(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T7523_TabletEmulator_VerifyFedExModalShowsAptOptions : T7523_DesktopBase
    {
        public T7523_TabletEmulator_VerifyFedExModalShowsAptOptions(ITestOutputHelper output) : base(output)
        {
        }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void FedExModalApartmentOptions(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the Address Verification modal displays the correct options for apartments.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8686
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7523
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8686"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7523")]
    public abstract class T7523_DesktopBase : TestsBaseDesktop
    {
        protected T7523_DesktopBase(ITestOutputHelper output) : base(output)
        {
        }

        protected void Validate(string config)
        {
            /* Arrange
            1.Add any item to cart.
            2.Proceed to the Shipping page. 
            */
            InitializeFunctionalTest(config);
            ShoppingCartWorkflow.AddMultipleItemsToCart(Urls.ContemporaryFloorLampsSortPageUrl, 1);
            Cart.CheckOut();
            Assert.True(Shipping.IsCurrentPage, "Current page is not a Shipping page");

            //Act: Fill out Shipping information
            EnterShippingInformation();

            //Act: Proceed to payment
            Shipping.ProceedToPayment();

            //Assert: FedEx modal is opened
            Assert.True(Shipping.DoesFedExModalShow(), "Fed Ex address validation modal is not displayed");

            //Assert: FedEx modal messages
            Assert.Equals("We may need an Apartment/Suite Number for you.", CustomerAddressInformation.GetFedExModalElements["FedExAddressValidationHeader"].Text.Trim(), "FedEx address validation header text is incorrect");
            Assert.True(CustomerAddressInformation.GetFedExModalElements["DefaultAddressRadioElement"].Text.Trim().
                CaseInsensitiveContains("ENTER APARTMENT/SUITE NUMBER"),"ENTER APARTMENT/SUITE NUMBER option text is incorrect");
            Assert.True(CustomerAddressInformation.GetFedExModalElements["NoChangeAddressRadioElement"].Text.Trim().
                CaseInsensitiveContains("MAINTAIN CURRENT ADDRESS"), "Maintain entered address option text is incorrect");
            Assert.Displayed(CustomerAddressInformation.GetFedExModalElements["SubmitChanges"], "SUBMIT is not displayed");
            Assert.True(CustomerAddressInformation.IsSimilarVerifiedAddressDisplayed == false, "Use similar address option is displayed");

            //Act: Add Appartment, get modal Address data, and Submit FedEx modal changes.
            var apartmentNumber = "704";
            CustomerAddressInformation.EnterApartmentAddress(apartmentNumber);
            var fedExSuggestedAddress = CustomerAddressInformation.GetFedExModalApartmentActiveAddressText["FedExSuggestedAddress"];
            var fedExSuggestedCity = CustomerAddressInformation.GetFedExModalApartmentActiveAddressText["FedExSuggestedCity"];
            var fedExSuggestedState = CustomerAddressInformation.GetFedExModalApartmentActiveAddressText["FedExSuggestedState"];
            var fedExSuggestedZipCode = CustomerAddressInformation.GetFedExModalApartmentActiveAddressText["FedExSuggestedZipCode"];

            CustomerAddressInformation.SubmitFedExModalChanges();
            Assert.True(Payment.IsCurrentPage,"Current page is not a Payment page");

            //Assert: Address information on a Payment page
            var streetAddressText = Payment.GetAddressTextWithApartmentFieldActive["StreetAddressText"];
            var apartmentText = Payment.GetAddressTextWithApartmentFieldActive["ApartmentText"];
            var cityText = Payment.GetAddressTextWithApartmentFieldActive["CityTextWithApartmentFieldActive"];
            var stateText = Payment.GetAddressTextWithApartmentFieldActive["StateTextWithApartmentFieldActive"];
            var zipCodeText = Payment.GetAddressTextWithApartmentFieldActive["ZipCodeTextWithApartmentFieldActive"];

            Assert.Equals(fedExSuggestedAddress, streetAddressText, "Street Address changed when selecting 'Maintain Entered Address' option");
            Assert.Equals(apartmentNumber, apartmentText, "Apartment number changed from FedEx modal.");
            Assert.Equals(fedExSuggestedCity, cityText, "City changed when selecting 'Maintain Entered Address' option");
            Assert.Equals(fedExSuggestedState, stateText, "State changed when selecting 'Maintain Entered Address' option");
            Assert.Equals(fedExSuggestedZipCode, zipCodeText, "Zip/postal code changed when selecting 'Maintain Entered Address' option");

            //Assert: Navigate back to the Shipping page. Confirm the FedEx modal does NOT re-launch.
            Shipping.Navigate();
            Assert.True(Shipping.IsCurrentPage, "Current page is not a Shipping page");
            Shipping.ProceedToPayment();
            Assert.True(Payment.IsCurrentPage, "FedEx modal launched when attempting to navigate to the Payment page the second time.");
        }

        private void EnterShippingInformation()
        {
            //Creating custom address
            var addressCustom = new Address
            {
                FirstName = "Test - T7523",
                LastName = "Test - T7523",
                AddressLine1 = "607 East Providencia Ave",
                City = "Burbank",
                ZipCode = "91501"
            };

            CustomerAddressInformation.FillFormControlByText(CustomerAddressInformation.ShippingElementsCollection["FirstNameField"], addressCustom.FirstName);
            CustomerAddressInformation.FillFormControlByText(CustomerAddressInformation.ShippingElementsCollection["LastNameField"], addressCustom.LastName);
            CustomerAddressInformation.FillFormControlByText(CustomerAddressInformation.ShippingElementsCollection["StreetAddressField"], addressCustom.AddressLine1);
            CustomerAddressInformation.FillFormControlByText(CustomerAddressInformation.ShippingElementsCollection["CityField"], addressCustom.City);
            CustomerAddressInformation.SelectState("CA");//state selected
            CustomerAddressInformation.FillFormControlByText(CustomerAddressInformation.ShippingElementsCollection["ZipPostalCodeField"], addressCustom.ZipCode);
            CustomerAddressInformation.FillFormControlByText(CustomerAddressInformation.ShippingElementsCollection["EmailField"], addressCustom.Email);
            CustomerAddressInformation.FillFormControlByText(CustomerAddressInformation.ShippingElementsCollection["PhoneField"], addressCustom.Phone);
        }
    }
}
