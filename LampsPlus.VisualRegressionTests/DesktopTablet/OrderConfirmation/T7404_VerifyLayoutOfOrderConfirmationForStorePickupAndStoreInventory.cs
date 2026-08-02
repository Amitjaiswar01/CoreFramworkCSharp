using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.VisualRegressionTests.DesktopTablet.OrderConfirmation
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7404_Windows_VerifyLayoutOfOrderConfirmationForStorePickupAndInventory : T7404_DesktopBase
    {
        public T7404_Windows_VerifyLayoutOfOrderConfirmationForStorePickupAndInventory(ITestOutputHelper output, T7404_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void LayoutOfOrderConfirmationForStorePickupAndInventory(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7404_Mac_VerifyLayoutOfOrderConfirmationForStorePickupAndInventory : T7404_DesktopBase
    {
        public T7404_Mac_VerifyLayoutOfOrderConfirmationForStorePickupAndInventory(ITestOutputHelper output, T7404_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void LayoutOfOrderConfirmationForStorePickupAndInventory(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7404_iPad_VerifyLayoutOfOrderConfirmationForStorePickupAndInventory : T7404_DesktopBase
    {
        public T7404_iPad_VerifyLayoutOfOrderConfirmationForStorePickupAndInventory(ITestOutputHelper output, T7404_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void LayoutOfOrderConfirmationForStorePickupAndInventory(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7404_TabletEmulator_VerifyLayoutOfOrderConfirmationForStorePickupAndInventory : T7404_DesktopBase
    {
        public T7404_TabletEmulator_VerifyLayoutOfOrderConfirmationForStorePickupAndInventory(ITestOutputHelper output, T7404_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void LayoutOfOrderConfirmationForStorePickupAndInventory(string config) => Validate(Validate, config);
    }


    public class T7404_SharedProductSku_Fixture : FixtureBase
    {
        public string SkuBetweenTenAndTwentyDollars { get; }
        public string SkuBopusEligible { get; }
        public string SkuLessThanTenDollars { get; }
        public T7404_SharedProductSku_Fixture()
        {
            SkuBetweenTenAndTwentyDollars = ProductActions.GetSkuBetweenTenAndTwentyDollars;
            SkuBopusEligible = ProductActions.GetBopusEligibleSku;
            SkuLessThanTenDollars = ProductActions.GetLessThanTenDollarItem;
        }
    }


    /// <summary>
    /// Verify the layout of the Order Confirmation page when placing an order with a Ship, Store Pickup and Store Inventory item.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8059
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7404
    /// </summary>
    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8059"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7404")]
    public abstract class T7404_DesktopBase : VisualTestsBase, IClassFixture<T7404_SharedProductSku_Fixture>
    {
        protected readonly T7404_SharedProductSku_Fixture Fixture;

        protected T7404_DesktopBase(ITestOutputHelper output, T7404_SharedProductSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            InitializeVisualTest(config);

            var shortSku1 = Fixture.SkuBetweenTenAndTwentyDollars;
            Assert.DatabaseObject(shortSku1, "ProductActions.GetSkuBetweenTenAndTwentyDollars()");
            ShoppingCartWorkflow.AddItemToCartBySku(new ProductModel { Sku = shortSku1 });

            var shortSku2 = Fixture.SkuBopusEligible;
            Assert.DatabaseObject(shortSku2, "ProductActions.GetBopusEligibleSku()");
            ShoppingCartWorkflow.AddItemToCartBySku(new ProductModel { Sku = shortSku2 });

            var shortSku3 = Fixture.SkuLessThanTenDollars;
            Assert.DatabaseObject(shortSku3, "ProductActions.GetLessThanTenDollarItem()");
            ShoppingCartWorkflow.AddItemToCartBySku(new ProductModel { Sku = shortSku3 });

            //cart
            //Item 2 Store Pickup
            Browser.Wait.IsVisibleElement(By.ClassName(CartOverview.CheckOutNowClass));
            CartOverview.RemovePromoCode();
            Browser.Wait.ForClickableElement(CartOverview.ChangeShippingOptionsLinkByIndex(1), 30).Click();
            Browser.Wait.ForElement(CartOverview.ShippingOptionModal);
            Browser.Wait.ForClickableElement(CartOverview.StorePickupElement).Click();
            Browser.Wait.ForDisplayedElement(CartOverview.StorePickupZipField);

            CartOverview.StorePickupZipField.SendKeys("91311");
            CartOverview.StorePickupSearchButton.Click();
            Browser.Wait.ForElementToStopAnimating(CartOverview.StorePickupSearchButton);
            Browser.Wait.ForDomReady();

            Browser.ScrollToTopOfWindow(); // TODO: This should not be required, but as of now the modal gets cut off during capture and thus, scrolling to top of window.
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, GlobalLocators.Iframe);

            Browser.Wait.ForDisplayedElement(CartOverview.StorePickupList(0)).Click();
            Browser.Wait.ForClickableElement(CartOverview.StorePickupUpdateButton).Click();
            Browser.Wait.UntilElementUnloads(GlobalLocators.Iframe);

            //item 3 Store Inventory
            Browser.ScrollIntoView(CartOverview.ChangeShippingOptionsLinkByIndex(2));
            Browser.ExecuteJs("window.scrollBy(0,-800)");
            CartOverview.ChangeShippingOptionsLinkByIndex(2).Click();
            Browser.Wait.ForElement(CartOverview.ShippingOptionModal);
            Browser.Wait.ForClickableElement(CartOverview.StoreInventoryTab).Click();
            Browser.Wait.ForDisplayedElement(CartOverview.StoreInventoryZipField);

            CartOverview.StoreInventoryZipField.SendKeys("91311");
            CartOverview.StoreInventorySearchButton.Click();
            Browser.Wait.ForElementToStopAnimating(CartOverview.StoreInventorySearchButton);
            Browser.Wait.ForDomReady();

            Browser.ScrollToTopOfWindow(); // TODO: This should not be required, but as of now the modal gets cut off during capture and thus, scrolling to top of window.
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, GlobalLocators.Iframe);

            Browser.Wait.ForDisplayedElement(CartOverview.StorePickupList(5)).Click();
            Browser.Wait.ForClickableElement(CartOverview.StoreInventoryUpdateButton).Click();
            Browser.Wait.UntilElementUnloads(GlobalLocators.Iframe);

            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { CartOverview.CartTitle, CartOverview.CartIdContainer }, true, true, CartOverview.CartIdContainer, 10);

            CsrBlock.SelectSaleSource(Sources.CartSources.SalesPhone);
            Browser.Wait.ForDomReady();

            var checkOutNowButton = CartOverview.CheckOutNowButton;
            checkOutNowButton.Click();

            // Shipping
            Browser.Wait.ForDomReady();
            CustomerAddressInformation.EnterShippingAddress(new Address());
            ShoppingCartWorkflow.ProceedToPayment();

            //Wire Transfer payment option
            Browser.Wait.IsVisibleElement(By.CssSelector(Payment.JsPaymentTypeOptionListClass.ToCssClassSelector()));
            Payment.WireTransferRadio.Click();
            Payment.PlaceOrderButton.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(OrderConfirmation.OrderConfirmationHeadingClass.ToCssClassSelector()));

            ScreenCapturer.CaptureScreenRegionWithIgnoredLayouts(Browser.PageUrl, OrderConfirmation.OrderSummaryContainer, new List<IElement> { OrderConfirmation.OrderIdHeading, OrderConfirmation.EmailUTagElement });
        }
    }
}
