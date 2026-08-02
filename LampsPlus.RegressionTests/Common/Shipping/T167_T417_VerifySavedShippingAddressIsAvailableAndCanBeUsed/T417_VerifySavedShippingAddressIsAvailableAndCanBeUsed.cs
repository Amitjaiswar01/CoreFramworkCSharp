using xRetry;
using Xunit;
using Xunit.Abstractions;
using Automation.Framework.Core;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Pages.Refactored.Address;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Environment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Shipping.T167_T417_VerifySavedShippingAddressIsAvailableAndCanBeUsed
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Shipping)]
    public class T417_iPhone_VerifySvdShippingAddrAvailCanBeUsed : T417_MobileBase
    {
        public T417_iPhone_VerifySvdShippingAddrAvailCanBeUsed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
        public void SvdShippingAddrAvailCanBeUsed(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Shipping)]
    public class T417_AndroidPhone_VerifySvdShippingAddrAvailCanBeUsed : T417_MobileBase
    {
        public T417_AndroidPhone_VerifySvdShippingAddrAvailCanBeUsed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_NPCSI)]
        public void SvdShippingAddrAvailCanBeUsed(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T417_Emulator_VerifySvdShippingAddrAvailCanBeUsed : T417_MobileBase
    {
        public T417_Emulator_VerifySvdShippingAddrAvailCanBeUsed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void SvdShippingAddrAvailCanBeUsed(string config) => Validate(config);
    }


    /// <summary>
    /// Verify a saved shipping address is available and can be used.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5028
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T417
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5028"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T417")]
    //[Collection(LpTraits.UserRole.Customer)]
    public abstract class T417_MobileBase : TestsBaseMobile
    {
        protected T417_MobileBase(ITestOutputHelper output) : base(output)
        {
        }

        protected void Validate(string config)
        {
            //Arrange: Consumer is signed in with no saved address
            var setup = new TestSetup(config);
            InitializeFunctionalTest(config, setup: setup);

            //Arrange: identify a shortsku.
            var shortSku = ProductActions.GetShortSkuThatMeetsMinimumOrder;
            Assert.DatabaseObject(shortSku, "ProductActions.GetShortSkuThatMeetsMinimumOrder()");

            /*Act:
            1. Add item to cart
            2. Proceed to checkout
            3. On the Shipping page, fill out the Shipping Information form.
            4. Click the 'Proceed to Payment' button.
            5. Click on the SHIPPING link at the top of the page in SHIPPING > PAYMENT
            */
            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel { Sku = shortSku });
            var shippingAddress = ShoppingCartWorkflow.CreateNewSavedAddress(new Address { State = StateCodeListUnitedStates.CA }, true);

            //Assert:The Saved shipping address matches the entered address.
            Assert.True(Shipping.IsCurrentPage, "User is not on Shipping page.");
            Assert.Equals($"{shippingAddress.FirstName} {shippingAddress.LastName}",
                CustomerAddressInformation.GetSavedAddressFullName().Trim(), "First and last name do not match.");

            var expectedShippingAddress =
                $@"{shippingAddress.AddressLine1}{Page.NewLineSequenceString}{shippingAddress.AddressLine2}{Page.NewLineSequenceString}{shippingAddress.City},
                {shippingAddress.State} {shippingAddress.ZipCode} {shippingAddress.Country}";

            VerifyShippingInfoElements(expectedShippingAddress);
        }

        private void VerifyShippingInfoElements(string expectedShippingAddress)
        {
            expectedShippingAddress = TextActions.RegexNoTabsAndNewLines(expectedShippingAddress);
            var actualShippingAddress =
                TextActions.RegexNoTabsAndNewLines(CustomerAddressInformation.GetSavedAddressShippingInfo()
                    .TrimStart());
            Assert.Equals(expectedShippingAddress, actualShippingAddress, "Shipping address does not match.");
            Assert.True(CustomerAddressInformation
                .GetSavedAddressShippingInfo() != null, "Shipping Address Info container not displayed");
        }
    }
}
