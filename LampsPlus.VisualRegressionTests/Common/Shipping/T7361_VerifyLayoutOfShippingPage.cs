using System.Collections.Generic;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.VisualRegressionTests.Common.Shipping
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7361_Windows_VerifyLayoutOfShipPage : T7361_DesktopBase
    {
        public T7361_Windows_VerifyLayoutOfShipPage(ITestOutputHelper output, SharedItem_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void VerifyLayoutOfShipPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7361_Mac_VerifyLayoutOfShipPage : T7361_DesktopBase
    {
        public T7361_Mac_VerifyLayoutOfShipPage(ITestOutputHelper output, SharedItem_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI)]
        public void VerifyLayoutOfShipPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7361_iPad_VerifyLayoutOfShipPage : T7361_DesktopBase
    {
        public T7361_iPad_VerifyLayoutOfShipPage(ITestOutputHelper output, SharedItem_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI)]
        public void VerifyLayoutOfShipPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7361_TabletEmulator_VerifyLayoutOfShipPage : T7361_DesktopBase
    {
        public T7361_TabletEmulator_VerifyLayoutOfShipPage(ITestOutputHelper output, SharedItem_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI)]
        public void VerifyLayoutOfShipPage(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Shipping Page, Ship to A Different Address button, Select a Shipping Address modal, Shipping Information modal and Order Confirmation page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7507
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7361
    /// </summary>
    //[Collection(LpTraits.UserRole.Customer)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7507"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7361")]
    public abstract class T7361_DesktopBase : T7361_Base
    {
        protected T7361_DesktopBase(ITestOutputHelper output, SharedItem_Fixture fixture) : base(output, fixture) { }
    }


    public class SharedItem_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public SharedItem_Fixture()
        {
            ShortSku = ProductActions.GetSkuGreaterThanTwoHundredDollars;
        }
    }


    public abstract class T7361_Base : VisualTestsBase, IClassFixture<SharedItem_Fixture>
    {
        protected readonly SharedItem_Fixture Fixture;

        protected T7361_Base(ITestOutputHelper output, SharedItem_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        } 
        
        protected virtual void Validate(string config)
        {
            InitializeVisualTest(config);
            ManageAccountWorkflow.DeleteAllSavedAddresses();

            //Precondition: User has two saved addresses.
            Browser.Navigate(Urls.ManageAccountPageUrl);

            var sku = Fixture.ShortSku;
            var shippingAddress1 = new Address{};
            var shippingAddress2 = new Address { AddressLine1 = "9201 Winnetka Ave"};

            Assert.DatabaseObject(sku, "ProductActions.GetSkuGreaterThanTwoHundredDollars()");

            //Create saved addresses
            Browser.Wait.ForClickableElement(ManageAccount.ManageShippingAddressesLinkForElement, 3).Click();
            Browser.Wait.ForClickableElement(ManageAccount.BtnAddShippingAddress);
            ManageAccount.BtnAddShippingAddress.Click();
            ManageAccountWorkflow.AddNewShippingAddressToModal(shippingAddress1);
            Browser.Wait.ForClickableElement(ManageAccount.BtnSaveShippingAddress).Click();

            Browser.Wait.UntilElementUnloads(ManageAccount.ModalWindow);

            Browser.Wait.ForDomReady();
            ManageAccount.BtnAddShippingAddress.Click();
            ManageAccountWorkflow.AddNewShippingAddressToModal(shippingAddress2);
            Browser.Wait.ForClickableElement(ManageAccount.BtnSaveShippingAddress).Click();

            //Verify second non-default address was added
            Browser.Wait.ForCondition(() => Browser.Locate.ElementsByXpath(ManageAccount.SavedAddressXpath).Count > 1);

            //Precondition: Add an item to the cart.
            ProductDetail.NavigateToProductDetailByShortSku(sku);
            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));
            GlobalLocators.AddToCartButton.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(CartOverview.CheckOutNowClass.ToCssClassSelector()));

            //Step 1: Proceed to the Shipping page and capture a screenshot of the entire page.
            Browser.Wait.ForClickableElement(CartOverview.CheckOutNowButton).Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(Shipping.ProceedPaymentId.ToCssIdSelector()));

            //Capture screenshot of Shipping page.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture);

            Browser.Wait.IsVisibleElement(By.CssSelector(Shipping.ShipToDifferentAddrClass.ToCssClassSelector()));

            //Step 2: Click on the 'Ship to a different address' button and capture a screenshot of the modal element.
            CustomerAddressInformation.ShipToDifferentAddressButton.Click();
            Browser.SwitchFocusToIframe(GlobalLocators.Iframe);
            Browser.Wait.IsVisibleElement(By.CssSelector(Shipping.AddNewAddrClass.ToCssClassSelector()));

            //Capture screenshot of Select a Shipping Address page.
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { Shipping.SelectShippingAddressOption }, true, true);

            //Step 3: Click on the 'Add new address' button and capture a screenshot of the modal element.
            CustomerAddressInformation.AddNewAddressButton.Click();
            Browser.Wait.ForDomReady();

            //Capture screenshot of New Shipping modal / form.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            GlobalLocators.LpModalCloseElement.Click();
            Browser.RefreshPage();

            ManageAccountWorkflow.DeleteAllSavedAddresses();
        }
    }
}
