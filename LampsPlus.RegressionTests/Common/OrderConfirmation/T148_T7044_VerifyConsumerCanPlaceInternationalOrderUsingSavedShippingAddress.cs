using Xunit;
using Xunit.Abstractions;
using OpenQA.Selenium;
using xRetry;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.DesktopTablet.OrderConfirmation;

namespace LampsPlus.RegressionTests.Common.OrderConfirmation
{
    //[Collection(LpTraits.BatchGroup.Common.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    public class T148_Windows_VerifyPlaceIntOrderUsingSvdAddr : T148_DesktopBase
    {
        public T148_Windows_VerifyPlaceIntOrderUsingSvdAddr(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void PlaceIntOrderUsingSvdAddr(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    public class T148_Mac_VerifyPlaceIntOrderUsingSvdAddr : T148_DesktopBase
    {
        public T148_Mac_VerifyPlaceIntOrderUsingSvdAddr(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI)]
        public void PlaceIntOrderUsingSvdAddr(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    public class T148_iPad_VerifyPlaceIntOrderUsingSvdAddr : T148_DesktopBase
    {
        public T148_iPad_VerifyPlaceIntOrderUsingSvdAddr(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI)]
        public void PlaceIntOrderUsingSvdAddr(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    public class T148_TabletEmulator_VerifyPlaceIntOrderUsingSvdAddr : T148_DesktopBase
    {
        public T148_TabletEmulator_VerifyPlaceIntOrderUsingSvdAddr(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI)]
        public void PlaceIntOrderUsingSvdAddr(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.OrderConfirmation)]
    public class T7044_iPhone_VerifyPlaceIntOrderUsingSvdAddr : T7044_MobileBase
    {
        public T7044_iPhone_VerifyPlaceIntOrderUsingSvdAddr(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
        public void PlaceIntOrderUsingSvdAddr(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    public class T7044_Emulator_VerifyPlaceIntOrderUsingSvdAddr : T7044_MobileBase
    {
        public T7044_Emulator_VerifyPlaceIntOrderUsingSvdAddr(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void PlaceIntOrderUsingSvdAddr(string config) => Validate(config);
    }


    /// <summary>
    /// Verify consumer can place international order using saved shipping address.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-6527
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T148
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-6527"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T148")]
    public abstract class T148_DesktopBase : T148_T7044_Base
    {
        protected T148_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected override void OpenAddShippingAddressModal()
        {
            Browser.Wait.ForDomReady();
            ManageAccount.OpenAddShippingAddressModal();

            Browser.Wait.IsVisibleElement(By.Id(GlobalLocators.LpModalId));
        }

        protected override void TestInitialization(string config, string url)
        {
            InitializeFramework(config, url);
        }
    }


    /// <summary>
    /// Verify consumer can place international order using saved shipping address.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5507
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7044
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5507"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7044")]
    public abstract class T7044_MobileBase : T148_T7044_Base
    {
        protected T7044_MobileBase(ITestOutputHelper output) : base(output) { }

        protected override void OpenAddShippingAddressModal()
        {
            Browser.Wait.ForClickableElement(ManageAccount.BtnAddShippingAddress,30);
            Browser.ClickByJs(ManageAccount.BtnAddShippingAddress);
        }

        protected override void TestInitialization(string config, string url)
        {
            InitializeFramework(config, url);
            if (OperatingSystem == OperatingSystem.iPhone)
            {
                Browser.DisposeBrowserAfterTest = false;
            }
        }
    }


    public abstract class T148_T7044_Base : OrderConfirmationTestsBase
    {
        protected T148_T7044_Base(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            TestInitialization(config, Urls.ManageShippingAddressPageUrl);

            OpenAddShippingAddressModal();

            var intAddress = new IntAddress("LP-148") { State = "N/A" };
            ManageAccountWorkflow.AddNewShippingAddressToModal(intAddress);

            if (ManageAccount.BtnSaveShippingAddress.Displayed)
            {
                ManageAccount.BtnSaveShippingAddress.Click();
            }

            var productGreaterThanTwoHundredDollars = ProductActions.GetSkuGreaterThanTwoHundredDollars;
            Assert.DatabaseObject(productGreaterThanTwoHundredDollars, "ProductActions.GetSkuGreaterThanTwoHundredDollars()");

            ShoppingCartWorkflow.AddItemToCartBySku(new ProductModel { Sku = productGreaterThanTwoHundredDollars });

            CartOverview.RemovePromoCode(); //In Case the account has an added promo code.

            CartOverview.CheckOutNowButton.Click();

            Browser.Wait.ForDomReady();

            CustomerAddressInformation.ProceedToPayment();

            Browser.Wait.IsVisibleElement(By.CssSelector(Payment.PlaceYourIntlOrderButtonId.ToCssIdSelector()));

            Payment.PlaceInternationalOrder();

            Browser.Wait.IsVisibleElement(By.ClassName(OrderConfirmation.OrderConfirmationHeadingClass));
        }

        protected abstract void OpenAddShippingAddressModal();
        protected abstract void TestInitialization(string config, string url);
    }
}
