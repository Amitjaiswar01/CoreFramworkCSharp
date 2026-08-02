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
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T167_Windows_VerifySvdShippingAddrAvailCanBeUsed : T167_DesktopBase
    {
        public T167_Windows_VerifySvdShippingAddrAvailCanBeUsed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void SvdShippingAddrAvailCanBeUsed(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Shipping)]
    public class T167_Mac_VerifySvdShippingAddrAvailCanBeUsed : T167_DesktopBase
    {
        public T167_Mac_VerifySvdShippingAddrAvailCanBeUsed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI)]
        public void SvdShippingAddrAvailCanBeUsed(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T167_iPad_VerifySvdShippingAddrAvailCanBeUsed : T167_DesktopBase
    {
        public T167_iPad_VerifySvdShippingAddrAvailCanBeUsed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI)]
        public void SvdShippingAddrAvailCanBeUsed(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T167_TabletEmulator_VerifySvdShippingAddrAvailCanBeUsed : T167_DesktopBase
    {
        public T167_TabletEmulator_VerifySvdShippingAddrAvailCanBeUsed(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void SvdShippingAddrAvailCanBeUsed(string config) => Validate(config);
    }


    /// <summary>
    /// Verify a saved shipping address is available and can be used.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5300
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T167
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5300"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T167")]
    //[Collection(LpTraits.UserRole.Customer)]
    [Trait(LpTraits.Keys.Category, LpTraits.Categories.CRUD)]
    public abstract class T167_DesktopBase : TestsBaseDesktop
    {
        protected T167_DesktopBase(ITestOutputHelper output) : base(output)
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
            var shippingAddress = ShoppingCartWorkflow.CreateNewSavedAddress(new Address { State = StateCodeListUnitedStates.CA },true);

            /*Assert:
            1.The saved shipping address is available with the following options:
            Customer can use the current address on the page
            A button labeled 'Ship to A Different Address'
            2.The Saved shipping address matches the entered address.
            */
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
            expectedShippingAddress = TextActions.RemoveWhitespace(expectedShippingAddress);

            var actualShippingAddress = CustomerAddressInformation.GetSavedAddressShippingInfo().Trim();
            actualShippingAddress = TextActions.RemoveWhitespace(actualShippingAddress);

            var expectedShipToDifferentAddressLabel = "ship to a different address";
            var actualShipToDifferentAddressLabel = TextActions.RegexNoTabsAndNewLines(CustomerAddressInformation.GetShipToDifferentAddressButtonLabel().ToLower().Trim());

            Assert.Equals(expectedShippingAddress, actualShippingAddress, "Shipping address does not match.");
            Assert.Equals(expectedShipToDifferentAddressLabel, actualShipToDifferentAddressLabel, "Text on shipping address button is in correct.");
        }
    }
}
