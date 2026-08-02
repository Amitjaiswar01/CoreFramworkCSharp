using xRetry;
using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Shipping.T7519_T7520_VerifyFedExSimilarVerifiedAddressOption
{
    //[Collection(LpTraits.BatchGroup.Mobile.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Shipping)]
    public class T7520_iPhone_VerifyFedExSimilarVerifiedAddressOption : T7520_MobileBase
    {
        public T7520_iPhone_VerifyFedExSimilarVerifiedAddressOption(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void FedExSimilarVerifiedAddressOption(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T7520_Emulator_VerifyFedExSimilarVerifiedAddressOption : T7520_MobileBase
    {
        public T7520_Emulator_VerifyFedExSimilarVerifiedAddressOption(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void FedExSimilarVerifiedAddressOption(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the user can use the 'Similar Verified Address' option from the Address Verification modal.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8686
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7520
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8686"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7520")]
    public abstract class T7520_MobileBase : TestsBaseMobile
    {
        protected T7520_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            /*Arrange:
            1. Add any item to cart.
            2. Proceed to the Shipping page. 
            */
            InitializeFunctionalTest(config);
            ShoppingCartWorkflow.AddMultipleItemsToCart(Urls.ContemporaryFloorLampsSortPageUrl, 1);
            Cart.CheckOut();
            Assert.True(Shipping.IsCurrentPage, "Current page is not a Shipping page");

            //Act: Open FexEx modal
            ShoppingCartWorkflow.ShowFedExValidationModal(enterApartment: false);


            //Act: USE SIMILAR VERIFIED ADDRESS option is selected.
            CustomerAddressInformation.UseSimilarVerifiedAddressOption();

            VerifyAddressVariables();
        }

        protected void VerifyAddressVariables()
        {
            //Arrange: Store the suggested address.
            var fedExSuggestedAddress = CustomerAddressInformation.GetFedExModalSuggestedAddressText["FedExSimilarAddress"];
            var fedExSuggestedCity = CustomerAddressInformation.GetFedExModalSuggestedAddressText["FedExSimilarCity"];
            var fedExSuggestedState = CustomerAddressInformation.GetFedExModalSuggestedAddressText["FedExSimilarState"];
            var fedExSuggestedZipCode = CustomerAddressInformation.GetFedExModalSuggestedAddressText["FedExSimilarZipCode"];

            //Act: Click the USE THIS ADDRESS button.
            CustomerAddressInformation.SubmitFedExModalChanges();
            Assert.True(Payment.IsCurrentPage, "Current page is not a Payment page");

            //Assert: Address address information on a Payment page
            var streetAddressText = Payment.GetSuggestedAddressText["StreetSuggestedAddressText"];
            var cityText = Payment.GetSuggestedAddressText["CitySuggestedAddressText"];
            var stateText = Payment.GetSuggestedAddressText["StateSuggestedAddressText"];
            var zipCodeText = Payment.GetSuggestedAddressText["ZipCodeSuggestedAddressText"];

            Assert.Equals(fedExSuggestedAddress, streetAddressText, "Street Address did not update when selecting 'Use Similar Verified Address' option");
            Assert.Equals(fedExSuggestedCity, cityText, "City did not update when selecting 'Use Similar Verified Address' option");
            Assert.Equals(fedExSuggestedState, stateText, "State did not update when selecting 'Use Similar Verified Address' option");
            Assert.Equals(fedExSuggestedZipCode, zipCodeText, "Zip/postal code did not update when selecting 'Use Similar Verified Address' option");
        }
    }
}
