using Xunit;
using Xunit.Abstractions;
using OpenQA.Selenium;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.Shipping
{
    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T7317_Windows_VerifyMultipleShippingAddress : T7317_DesktopBase
    {
        public T7317_Windows_VerifyMultipleShippingAddress(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void VerifyMultiShipAddr(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T7317_Mac_VerifyMultipleShippingAddress : T7317_DesktopBase
    {
        public T7317_Mac_VerifyMultipleShippingAddress(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void VerifyMultiShipAddr(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T7317_iPad_VerifyMultipleShippingAddress : T7317_DesktopBase
    {
        public T7317_iPad_VerifyMultipleShippingAddress(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void VerifyMultiShipAddr(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Shipping)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Shipping)]
    public class T7317_TabletEmulator_VerifyMultipleShippingAddress : T7317_DesktopBase
    {
        public T7317_TabletEmulator_VerifyMultipleShippingAddress(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void VerifyMultiShipAddr(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that multiple shipping addresses can be used on the Shipping page and are reflected correctly in the database.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7415
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7317
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7415"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7317")]
    public abstract class T7317_DesktopBase : T7317_T7318_Base
    {
        protected T7317_DesktopBase(ITestOutputHelper output) : base(output) { }
    }


    public abstract class T7317_T7318_Base : Common.Shipping.ShippingInfoTestsBase
    {
        protected T7317_T7318_Base(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            InitializeFramework(config);

            var firstProductBetweenTenAndTwenty = ProductActions.GetSkuBetweenTenAndTwentyDollars;
            var secondProductBetweenTenAndTwenty = ProductActions.GetSkuBetweenTenAndTwentyDollars;

            Assert.DatabaseObject(firstProductBetweenTenAndTwenty, "ProductActions.GetSkuBetweenTenAndTwentyDollars()");
            Assert.DatabaseObject(secondProductBetweenTenAndTwenty, "ProductActions.GetSkuBetweenTenAndTwentyDollars()");

            ShoppingCartWorkflow.AddItemToCartBySku(new ProductModel { Sku = firstProductBetweenTenAndTwenty });
            ShoppingCartWorkflow.AddItemToCartBySku(new ProductModel { Sku = secondProductBetweenTenAndTwenty });

            var cartId = CartOverview.CartId;

            CsrBlock.SelectSaleSource(Sources.CartSources.SalesPhone);
            CartOverview.CheckOutNowButton.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(Shipping.ProceedPaymentId.ToCssIdSelector()));

            Shipping.ShipToMultipleAddressesButton.Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(Shipping.IsMultipleShippingClass.ToCssClassSelector()));
            Shipping.NewAddressButton(0).Click();

            Browser.SwitchFocusToIframe(GlobalLocators.Iframe);

            var firstShippingAddress = ShoppingCartWorkflow.CreateNewSavedAddressFromModal(new Address { State = StateCodeListUnitedStates.CA });

            Browser.Wait.UntilElementUnloads(GlobalLocators.Iframe);

            Browser.MouseOverOnElement(Shipping.NewAddressButton(1));

            Browser.Wait.ForDisplayedElement(Shipping.NewAddressButton(1)).Click();

            var secondShippingAddress = ShoppingCartWorkflow.CreateNewSavedAddressFromModal(new Address { AddressLine1 = "10 Main St", City = "Huntington Beach", State = StateCodeListUnitedStates.CA, ZipCode = "90740"});

            //Buffer before address is written to DB
            Browser.Wait.UntilElementUnloads(GlobalLocators.Iframe, 3);

            var savedAddresses = ProductActions.GetLastSavedAddressByCartId(cartId);
            
            Assert.Equals(savedAddresses[0].ShortSku, firstProductBetweenTenAndTwenty, "Sku does not match.");
            Assert.Equals(savedAddresses[0].FirstName, secondShippingAddress.FirstName, "First name does not match.");
            Assert.Equals(savedAddresses[0].LastName, secondShippingAddress.LastName, "Last name does not match.");
            Assert.Equals(savedAddresses[0].Address1, secondShippingAddress.AddressLine1, "AddressLine1 does not match.");
            Assert.Equals(savedAddresses[0].Address2, secondShippingAddress.AddressLine2, "AddressLine2 does not match.");
            Assert.Equals(savedAddresses[0].City, secondShippingAddress.City, "City does not match.");
            Assert.Equals(savedAddresses[0].State, secondShippingAddress.State, "State does not match.");
            Assert.Equals(savedAddresses[0].Zip, secondShippingAddress.ZipCode, "Zip Code does not match.");
            Assert.Equals(savedAddresses[0].Phone, secondShippingAddress.Phone, "Phone number does not match.");

            Assert.Equals(savedAddresses[1].ShortSku, secondProductBetweenTenAndTwenty, "Sku does not match.");
            Assert.Equals(savedAddresses[1].FirstName, firstShippingAddress.FirstName, "First name does not match.");
            Assert.Equals(savedAddresses[1].LastName, firstShippingAddress.LastName, "Last name does not match.");
            Assert.Equals(savedAddresses[1].Address1, firstShippingAddress.AddressLine1, "AddressLine1 does not match.");
            Assert.Equals(savedAddresses[1].Address2, firstShippingAddress.AddressLine2, "AddressLine2 does not match.");
            Assert.Equals(savedAddresses[1].City, firstShippingAddress.City, "City does not match.");
            Assert.Equals(savedAddresses[1].State, firstShippingAddress.State, "State does not match.");
            Assert.Equals(savedAddresses[1].Zip, firstShippingAddress.ZipCode, "Zip Code does not match.");
            Assert.Equals(savedAddresses[1].Phone, firstShippingAddress.Phone, "Phone number does not match.");
        }
    }
}
