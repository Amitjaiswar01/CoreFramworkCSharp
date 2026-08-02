using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Pages.Refactored.Address;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Mobile.Shipping.T7990_VerifyLayoutOfShippingShipToDiffAddrAndAddAddress
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7990_iPhone_VerifyLayoutOfShippingShipToDiffAddrAndAddAddress : T7990_MobileBase
    {
        public T7990_iPhone_VerifyLayoutOfShippingShipToDiffAddrAndAddAddress(ITestOutputHelper output, T7990_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
        public void LayoutOfShippingShipToDiffAddrAndAddNewAddress(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7990_AndroidPhone_VerifyLayoutOfShippingShipToDiffAddrAndAddAddress : T7990_MobileBase
    {
        public T7990_AndroidPhone_VerifyLayoutOfShippingShipToDiffAddrAndAddAddress(ITestOutputHelper output, T7990_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_NPCSI)]
        public void LayoutOfShippingShipToDiffAddrAndAddNewAddress(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7990_Emulator_VerifyLayoutOfShippingShipToDiffAddrAndAddAddress : T7990_MobileBase
    {
        public T7990_Emulator_VerifyLayoutOfShippingShipToDiffAddrAndAddAddress(ITestOutputHelper output, T7990_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void LayoutOfShippingShipToDiffAddrAndAddNewAddress(string config) => Validate(Validate, config);
    }


    public class T7990_SharedSkus_Fixture : FixtureBase
    {
        public string Shortsku { get; }
        public Address ShippingAddress1 { get; }
        public Address ShippingAddress2 { get; }

        public T7990_SharedSkus_Fixture()
        {
            Shortsku = ProductActions.GetSkuGreaterThanTwoHundredDollars;
            ShippingAddress1 = new Address { };
            ShippingAddress2 = new Address { AddressLine1 = "9201 Winnetka Ave" };
        }
    }


    /// <summary>
    /// Verify the Layout of the  Shipping Page, Ship to a Different Address Button and Add New Address
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10852
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7990
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10852"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7990")]
    public abstract class T7990_MobileBase : VisualTestsBaseMobile, IClassFixture<T7990_SharedSkus_Fixture>
    {
        protected readonly T7990_SharedSkus_Fixture Fixture;

        protected T7990_MobileBase(ITestOutputHelper output, T7990_SharedSkus_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            // Arrange: User has two saved addresses & Add a Product to cart 
            InitializeVisualTest(config);
            ManageAccountWorkflow.DeleteAllSavedAddresses();
            ShoppingCartWorkflow.EmptyCart();

            Browser.Navigate(Urls.ManageAccountPageUrl);
            ManageAccountWorkflow.AddMultipleShippingAddress(Fixture.ShippingAddress1, Fixture.ShippingAddress2);

            var sku = Fixture.Shortsku;
            Assert.DatabaseObject(Fixture.Shortsku, "ProductActions.GetSkuGreaterThanTwoHundredDollars;");

            ProductDetail.AddSingleProductToCart(sku);
            Assert.True(Cart.IsCurrentPage, "Current page is not cart page");

            // Act: Proceed to Shipping Page
            Cart.CheckOut();
            Assert.True(Shipping.IsCurrentPage, "Current page is not shipping page");

            // Act: User has captured the screenshot of the full page
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { Cart.IgnoreCartId() }, true);

            // Act: Tab on Address Box
            CustomerAddressInformation.SelectSavedAddressShippingInfo();

            // Act: User has captured the screenshot of the visible page
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            // Act: Click on Add New Address
            Shipping.OpenAddNewAddressModal();

            // Act: User has captured the screenshot of the visible page
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            // Act: Data Clean Up
            Browser.Navigate(Urls.ManageAccountPageUrl);
            ManageAccountWorkflow.DeleteAllSavedAddresses();
            ShoppingCartWorkflow.EmptyCart();
        }
    }
}