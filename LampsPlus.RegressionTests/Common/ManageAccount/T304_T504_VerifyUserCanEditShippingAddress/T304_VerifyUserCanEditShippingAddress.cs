using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.ManageAccount.T304_T504_VerifyUserCanEditShippingAddress
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ManageAccount)]
    public class T304_Windows_VerifyUserCanEditShippingAddress : T304_DesktopBase
    {
        public T304_Windows_VerifyUserCanEditShippingAddress(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void UserCanEditShippingAddress(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ManageAccount)]
    public class T304_Mac_VerifyUserCanEditShippingAddress : T304_DesktopBase
    {
        public T304_Mac_VerifyUserCanEditShippingAddress(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI)]
        public void UserCanEditShippingAddress(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ManageAccount)]
    public class T304_iPad_VerifyUserCanEditShippingAddress : T304_DesktopBase
    {
        public T304_iPad_VerifyUserCanEditShippingAddress(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI)]
        public void UserCanEditShippingAddress(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ManageAccount)]
    public class T304_TabletEmulator_VerifyUserCanEditShippingAddress : T304_DesktopBase
    {
        public T304_TabletEmulator_VerifyUserCanEditShippingAddress(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI)]
        public void UserCanEditShippingAddress(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that a user can edit an existing shipping address.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9900
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T304
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9900"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T304")]
    public abstract class T304_DesktopBase : TestsBaseDesktop
    {
        protected T304_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected virtual void Validate(string config)
        {
            /*Arrange:
            User has a saved Shipping Address
            User has navigated to the 'Manage Account' info page: https://www.lampsplus.com/account/profile/ 
             */
            InitializeFunctionalTest(config);
            var expectedLandingPage = ManageAccount.PageUrl + ManageAccount.ShippingAddressOptionsUrl;
            var browser = ManageAccount.Navigate(ManageAccount.ShippingAddressOptionsUrl);
            Assert.Equals(expectedLandingPage, browser.PageUrl, $"{expectedLandingPage} is expected, but actual url is {browser.PageUrl}");

            var addShippingAddress = RandomAddressGenerator.RandomUsAddress();
            var editShippingAddress = RandomAddressGenerator.RandomUsAddress();

            ManageAccount.OpenShippingAddressForm();
            ManageAccount.AddNewShippingAddressToModal(addShippingAddress);
            ManageAccount.SaveShippingAddress();

            /*Act:
            Under the Manage Account section, click on the 'Shipping Addresses' link.
            Click the 'Edit' link and update the info in the popup.
            Click the 'Save' button.
             */
            ManageAccount.OpenEditShippingAddressModal();
            ManageAccount.ClearAccountShippingFormFields();

            ManageAccount.AddNewShippingAddressToModal(editShippingAddress);
            ManageAccount.SaveShippingAddress();

            //Assert: The modified shipping information is displayed.
            Browser.RefreshPage(); //TODO: This is required for now because the Default header doesn't always appear. Need to investigate more.
            var actualShippingAddress = ManageAccount.GetFirstSavedShippingAddress();
            var expectedFullName = ManageAccount.GetShippingAddressFullName();
            var expectedAddress = ManageAccount.GetShippingAddressCityStateZipName();

            Assert.Equals(expectedFullName, $"{editShippingAddress.FirstName} {editShippingAddress.LastName}", $"Name does not match. Expected '{expectedFullName}'. Actual '{editShippingAddress.FirstName} {editShippingAddress.LastName}.");
            Assert.Equals(editShippingAddress.AddressLine1, actualShippingAddress.AddressLine1, $"Address Line 1 does not match. Expected '{editShippingAddress.AddressLine1}'. Actual '{actualShippingAddress.AddressLine1}'.");
            Assert.Equals(editShippingAddress.AddressLine2, actualShippingAddress.AddressLine2, $"Address Line 2 does not match. Expected '{editShippingAddress.AddressLine2}'. Actual '{actualShippingAddress.AddressLine2}'.");
            Assert.Equals(editShippingAddress.Phone, actualShippingAddress.Phone, $"Phone does not match. Expected '{editShippingAddress.Phone}'. Actual '{actualShippingAddress.Phone}'.");
            Assert.Equals(expectedAddress, actualShippingAddress.City.Trim(), $"'City, State Zip [Country]' do not match. Expected {expectedAddress}. Actual {actualShippingAddress.City.Trim()}");
        }
    }
}
