using Xunit;
using Xunit.Abstractions;
using xRetry;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.ManageAccount.T303_T503_VerifyUserCanAddShippingAddress
{
    public class T503_VerifyUserCanAddShippingAddress
    {
        [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.ManageAccount)]
        public class T503_iPhone_VerifyUserCanAddShippingAddress : T503_MobileBase
        {
            public T503_iPhone_VerifyUserCanAddShippingAddress(ITestOutputHelper output) : base(output) { }

            [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
            [RetryTheory(3)]
            [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
            public void UserCanAddShippingAddress(string config) => Validate(config);
        }


        [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ManageAccount)]
        public class T503_Emulator_VerifyUserCanAddShippingAddress : T503_MobileBase
        {
            public T503_Emulator_VerifyUserCanAddShippingAddress(ITestOutputHelper output) : base(output) { }

            [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
            [SkippableTheory]
            [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
            public void UserCanAddShippingAddress(string config) => Validate(config);
        }


        /// <summary>
        /// Verify that a user can add a shipping address.
        /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9033
        /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T503
        /// </summary>
        [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
        [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9903"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T503")]
        public abstract class T503_MobileBase : TestsBaseMobile
        {
            protected T503_MobileBase(ITestOutputHelper output) : base(output) { }

            protected void Validate(string config)
            {
                /*Arrangement
                User is signed in as a customer.
                User has navigate to the 'Manage Account' info page.
                */
                InitializeFunctionalTest(config, Urls.ManageAccountPageUrl);
                Assert.True(ManageAccount.IsCurrentPage, "User is not on Manage Account page.");

                /*Act
                In the Preferred Shipping Address section, click on the 'Manage' link.
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
}
