using System.Web.UI;
using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Shipping.T7521_T7522_VerifyUserCanEditAddressFedExModal
{
    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T7521_Windows_VerifyUserCanEditAddressFedExModal : T7521_DesktopBase
    {
        public T7521_Windows_VerifyUserCanEditAddressFedExModal(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void UserCanEditAddressInFedExModal(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T7521_Mac_VerifyUserCanEditAddressFedExModal : T7521_DesktopBase
    {
        public T7521_Mac_VerifyUserCanEditAddressFedExModal(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void UserCanEditAddressInFedExModal(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T7521_iPad_VerifyUserCanEditAddressFedExModal : T7521_DesktopBase
    {
        public T7521_iPad_VerifyUserCanEditAddressFedExModal(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void UserCanEditAddressInFedExModal(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T7521_TabletEmulator_VerifyUserCanEditAddressFedExModal : T7521_DesktopBase
    {
        public T7521_TabletEmulator_VerifyUserCanEditAddressFedExModal(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void UserCanEditAddressInFedExModal(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the user can edit the address in the Address Verification modal.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8686
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7521
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8686"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7521")]
    public abstract class T7521_DesktopBase : TestsBaseDesktop
    {
        protected T7521_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected virtual void Validate(string config)
        {
            /*Arrange:
            Add any item to cart.
            Proceed to the Shipping page.
            */
            InitializeFunctionalTest(config);
            ShoppingCartWorkflow.AddMultipleItemsToCart(Urls.ContemporaryFloorLampsSortPageUrl, 1);
            Cart.CheckOut();
            Assert.True(Shipping.IsCurrentPage, "Current page is not a Shipping page");

            //Act: Open FexEx modal
            ShoppingCartWorkflow.ShowFedExValidationModal();
            
            //Add: Keep current address
            CustomerAddressInformation.KeepCurrentAddressAtFedExModal(editButton:true);

            //Act: Clear FedEx modal fields
            CustomerAddressInformation.ClearFedExModalFields();

            //Assert: NewFedExAddress
            NewFedExAddressVerification();
        }

        protected void NewFedExAddressVerification()
        {
            //Enter a new address into FedEx modal
            var newAddress1 = "116 Ardmore Ave";
            var newAddress2 = "#16";
            var newCity = "Ardmore";
            var newState = "PA";
            var newZip = "19003";

            CustomerAddressInformation.FillFormControlByText(CustomerAddressInformation.GetFedExModalAddressElements["FedExShippingAddress1"], newAddress1);
            CustomerAddressInformation.FillFormControlByText(CustomerAddressInformation.GetFedExModalAddressElements["FedExShippingAddress2"], newAddress2);
            CustomerAddressInformation.FillFormControlByText(CustomerAddressInformation.GetFedExModalAddressElements["FedExShippingCity"], newCity);
            CustomerAddressInformation.FillFormControlByText(CustomerAddressInformation.GetFedExModalAddressElements["FedExShippingState"], newState);
            CustomerAddressInformation.FillFormControlByText(CustomerAddressInformation.GetFedExModalAddressElements["FedExShippingZipCode"], newZip);

            CustomerAddressInformation.SubmitFedExModalChanges();

            Assert.Equals(newAddress1, CustomerAddressInformation.ShippingElementsCollection["StreetAddressField"].GetAttribute(HtmlTextWriterAttribute.Value.ToString().ToLower()), "Street Address did not update when selecting 'Modify Entered Address' option");
            Assert.Equals(newAddress2, CustomerAddressInformation.ShippingElementsCollection["ApartmentSuiteOtherField"].GetAttribute(HtmlTextWriterAttribute.Value.ToString().ToLower()), "Apartment/Suite/Other field did not update when selecting 'Modify Entered Address' option");
            Assert.Equals(newCity, CustomerAddressInformation.ShippingElementsCollection["CityField"].GetAttribute(HtmlTextWriterAttribute.Value.ToString().ToLower()), "City did not update when selecting 'Modify Entered Address' option");
            Assert.Equals(newState, CustomerAddressInformation.ShippingElementsCollection["StateField"].GetAttribute(HtmlTextWriterAttribute.Value.ToString().ToLower()), "State did not update when selecting 'Modify Entered Address' option");
            Assert.Equals(newZip, CustomerAddressInformation.ShippingElementsCollection["ZipPostalCodeField"].GetAttribute(HtmlTextWriterAttribute.Value.ToString().ToLower()), "Zip/postal code did not update when selecting 'Modify Entered Address' option");
        }
    }
}
