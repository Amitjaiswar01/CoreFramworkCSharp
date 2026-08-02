using Xunit;
using Xunit.Abstractions;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Shipping.T165_T415_VerifyFedExMaintainEnteredAddressOption
{
    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T165_Windows_VerifyFedExMaintainEnteredAddressOption : T165_DesktopBase
    {
        public T165_Windows_VerifyFedExMaintainEnteredAddressOption(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void FormUsesFedExValidation(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T165_Mac_VerifyFedExMaintainEnteredAddressOption : T165_DesktopBase
    {
        public T165_Mac_VerifyFedExMaintainEnteredAddressOption(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void FormUsesFedExValidation(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T165_iPad_VerifyFedExMaintainEnteredAddressOption : T165_DesktopBase
    {
        public T165_iPad_VerifyFedExMaintainEnteredAddressOption(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void FormUsesFedExValidation(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T165_TabletEmulator_VerifyFedExMaintainEnteredAddressOption : T165_DesktopBase
    {
        public T165_TabletEmulator_VerifyFedExMaintainEnteredAddressOption(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void FormUsesFedExValidation(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the user can use the 'Maintain Entered Address' option from the Address Verification modal.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5168
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T165
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5168"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T165")]
    public abstract class T165_DesktopBase : TestsBaseDesktop
    {
        protected T165_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            /* Arrange:
            1. Add any item to cart.
            2. Proceed to the Shipping page. 
            */
            InitializeFunctionalTest(config);
            ShoppingCartWorkflow.AddMultipleItemsToCart(Urls.ContemporaryFloorLampsSortPageUrl, 1);
            Cart.CheckOut();
            Assert.True(Shipping.IsCurrentPage, "Current page is not a Shipping page");

            //Act: Open FexEx modal
            ShoppingCartWorkflow.ShowFedExValidationModal(enterApartment: false);

            //Assert: FedEx modal messages
            Assert.True(CustomerAddressInformation.GetFedExModalElements["SuggestedAddressRadioElement"].Text.Trim().CaseInsensitiveContains("use similar verified address"), "Use similar verified address option text is incorrect");
            Assert.True(CustomerAddressInformation.GetFedExModalElements["NoChangeAddressRadioElement"].Text.Trim().CaseInsensitiveContains("maintain current address"), "Maintain entered address option text is incorrect");
            Assert.True(CustomerAddressInformation.GetFedExModalMaintainAddressText["MaintainMessage"].CaseInsensitiveContains("We may not have the correct address for you."), "Correct address text is incorrect");
            Assert.True(CustomerAddressInformation.GetFedExModalMaintainAddressText["AddressCorrectionsMessage"].CaseInsensitiveContains("Suggested address corrections are in RED."), "AddressCorrectionsMessage text is incorrect");
            Assert.Displayed(CustomerAddressInformation.GetFedExModalElements["SubmitChanges"], "SUBMIT is not displayed");
            Assert.True(CustomerAddressInformation.IsSimilarVerifiedAddressDisplayed == false, "Use similar address option is displayed");

            //Act: Click the MAINTAIN CURRENT ADDRESS option.
            CustomerAddressInformation.KeepCurrentAddressAtFedExModal();

            //Act: Store the Address values
            var fedExMaintainAddress = CustomerAddressInformation.GetFedExModalMaintainAddressText["FedExMaintainAddress"];
            var fedExMaintainCity = CustomerAddressInformation.GetFedExModalMaintainAddressText["FedExMaintainCity"];
            var fedExMaintainState = CustomerAddressInformation.GetFedExModalMaintainAddressText["FedExMaintainState"];
            var fedExMaintainZipCode = CustomerAddressInformation.GetFedExModalMaintainAddressText["FedExMaintainZipCode"];

            //Act: Click the USE THIS ADDRESS button.
            CustomerAddressInformation.SubmitFedExModalChanges();
            Assert.True(Payment.IsCurrentPage, "Current page is not a Payment page");

            //Assert: Compare the values that were stored from the modal to the corresponding fields on the Shipping section of the Payment page. 
            var streetAddressText = Payment.GetSuggestedAddressText["StreetSuggestedAddressText"];
            var cityText = Payment.GetSuggestedAddressText["CitySuggestedAddressText"];
            var stateText = Payment.GetSuggestedAddressText["StateSuggestedAddressText"];
            var zipCodeText = Payment.GetSuggestedAddressText["ZipCodeSuggestedAddressText"];

            Assert.Equals(fedExMaintainAddress, streetAddressText, "Street Address changed when selecting 'Maintain Entered Address' option");
            Assert.Equals(fedExMaintainCity, cityText, "City changed when selecting 'Maintain Entered Address' option");
            Assert.Equals(fedExMaintainState, stateText, "State changed when selecting 'Maintain Entered Address' option");
            Assert.Equals(fedExMaintainZipCode, zipCodeText, "Zip/postal code changed when selecting 'Maintain Entered Address' option");
        }
    }
}
