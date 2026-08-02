using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.ManageAccount.T303_T503_VerifyUserCanAddShippingAddress
{
    //[Collection(LpTraits.BatchGroup.Common.ManageAccount)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ManageAccount)]
    public class T303_Windows_VerifyUserCanAddShippingAddress : T303_DesktopBase
    {
        public T303_Windows_VerifyUserCanAddShippingAddress(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void LoginAfterPasswordChange(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ManageAccount)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ManageAccount)]
    public class T303_Mac_VerifyUserCanAddShippingAddress : T303_DesktopBase
    {
        public T303_Mac_VerifyUserCanAddShippingAddress(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI)]
        public void LoginAfterPasswordChange(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ManageAccount)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ManageAccount)]
    public class T303_iPad_VerifyUserCanAddShippingAddress : T303_DesktopBase
    {
        public T303_iPad_VerifyUserCanAddShippingAddress(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI)]
        public void LoginAfterPasswordChange(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ManageAccount)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ManageAccount)]
    public class T303_TabletEmulator_VerifyUserCanAddShippingAddress : T303_DesktopBase
    {
        public T303_TabletEmulator_VerifyUserCanAddShippingAddress(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI)]
        public void LoginAfterPasswordChange(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that a user can add a shipping address.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9903
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T303
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9903"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T303")]

    public abstract class T303_DesktopBase : TestsBaseDesktop
    {
        protected T303_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            /*Arrange
            User is signed in as a customer.
            User has navigate to the 'Manage Account' info page.
            */
            InitializeFunctionalTest(config, Urls.ManageAccountPageUrl);
            Assert.True(ManageAccount.IsCurrentPage, "User is not on Manage Account page.");

            /*Act
            Under the Manager Account section, click on the 'Shipping Addresses' link.
            Click the 'ADD SHIPPING ADDRESS' button on the next page.
            Fill out the form completely and click the 'Save' button.
             */
            Address.AddressLine2 = string.Empty;
            ManageAccountWorkflow.FillOutShippingAddressForm(Address);

            /*Assert
            The new address appears in the available addresses list.
            */
            Assert.Equals(Address.FirstName + " " + Address.LastName, ManageAccount.GetShippingAddressFullName(), "Name does not match.");
            Assert.Equals(Address.AddressLine1, ManageAccount.GetShippingAddressStreetName(), "Address Line 1 does not match.");
            Assert.Equals(Address.City + ", " + Address.State + " " + Address.ZipCode, ManageAccount.GetShippingAddressCityStateZipName(), "Address Line 3 (City, ST Zip) does not match.");
            Assert.Equals(Address.Phone, ManageAccount.GetShippingAddressPhoneNumber(), "Phone does not match.");
        }
    }
}