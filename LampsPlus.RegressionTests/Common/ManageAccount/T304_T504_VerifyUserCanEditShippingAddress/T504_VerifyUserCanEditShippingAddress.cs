using Xunit;
using Xunit.Abstractions;
using xRetry;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.ManageAccount.T304_T504_VerifyUserCanEditShippingAddress
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.ManageAccount)]
    public class T504_iPhone_VerifyUserCanEditShippingAddress : T504_MobileBase
    {
        public T504_iPhone_VerifyUserCanEditShippingAddress(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
        public void UserCanEditShippingAddress(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ManageAccount)]
    public class T504_Emulator_VerifyUserCanEditShippingAddress : T504_MobileBase
    {
        public T504_Emulator_VerifyUserCanEditShippingAddress(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void UserCanEditShippingAddress(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that a user can edit an existing shipping address.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9900
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T504
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9900"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T504")]
    public abstract class T504_MobileBase : TestsBaseMobile
    {
        protected T504_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            /*Arrange:
            User has a saved Shipping Address
            User has navigated to the 'Manage Account' info page: https://www.lampsplus.com/account/profile/ 
             */
            InitializeFunctionalTest(config, Urls.ManageAccountPageUrl);
            Assert.True(ManageAccount.IsCurrentPage, "User is not on Manage Account page.");

            var addShippingAddress = RandomAddressGenerator.RandomUsAddress();
            var editShippingAddress = RandomAddressGenerator.RandomUsAddress();

            ManageAccount.OpenShippingAddressForm();
            ManageAccount.AddNewShippingAddressToModal(addShippingAddress);
            ManageAccount.SaveShippingAddress();

            /*Act:
            In the Preferred Shipping Address section, click on the 'Manage' link.
            Click the 'Edit' link and update the info.
            Click the 'Save' button.
            */
            ManageAccount.OpenEditShippingAddressModal();
            ManageAccount.ClearAccountShippingFormFields();
            ManageAccount.ClearSelectedState();//Clear state at Shipping form
            ManageAccount.AddNewShippingAddressToModal(editShippingAddress);
            ManageAccount.SaveShippingAddress();

            //Assert: The modified shipping information is displayed.
            var actualShippingAddress = ManageAccount.GetFirstSavedShippingAddress();
            var expectedFullName = ManageAccount.GetShippingAddressFullName();
            var expectedAddress = ManageAccount.GetShippingAddressCityStateZipName();

            Assert.Equals(expectedFullName, $"{editShippingAddress.FirstName} {editShippingAddress.LastName}", $"Name does not match. Expected '{expectedFullName}'. Actual '{editShippingAddress.FirstName} {editShippingAddress.LastName}.");
            Assert.Equals(editShippingAddress.AddressLine1, actualShippingAddress.AddressLine1, $"Address Line 1 does not match. Expected '{editShippingAddress.AddressLine1}'. Actual '{actualShippingAddress.AddressLine1}'.");
            Assert.Equals(editShippingAddress.AddressLine2, actualShippingAddress.AddressLine2, $"Address Line 2 does not match. Expected '{editShippingAddress.AddressLine2}'. Actual '{actualShippingAddress.AddressLine2}'.");
            Assert.Equals(editShippingAddress.Phone, actualShippingAddress.Phone, $"Phone does not match. Expected '{editShippingAddress.Phone}'. Actual '{actualShippingAddress.Phone}'.");
            Assert.Equals(expectedAddress, actualShippingAddress.City, $"'City, State Zip' do not match. Expected {expectedAddress}. Actual {actualShippingAddress.City}");
        }
    }
}
