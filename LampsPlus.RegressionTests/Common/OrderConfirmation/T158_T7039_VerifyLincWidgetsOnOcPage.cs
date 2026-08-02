using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.DesktopTablet.OrderConfirmation;
using OpenQA.Selenium;
using xRetry;
using Xunit;
using Xunit.Abstractions;
using Skip = Xunit.Skip;

namespace LampsPlus.RegressionTests.Common.OrderConfirmation
{
    //[Collection(LpTraits.BatchGroup.Common.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    public class T158_Windows_VerifyLincWidgetsOnOcPage : T158_DesktopBase
    {
        public T158_Windows_VerifyLincWidgetsOnOcPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
		public void VerifyLincWidgetsOnOcPage(string config) => Validate(config);      
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    public class T158_Mac_VerifyLincWidgetsOnOcPage : T158_DesktopBase
    {
        public T158_Mac_VerifyLincWidgetsOnOcPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyLincWidgetsOnOcPage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    public class T158_iPad_VerifyLincWidgetsOnOcPage : T158_DesktopBase
    {
        public T158_iPad_VerifyLincWidgetsOnOcPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyLincWidgetsOnOcPage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    public class T158_TabletEmulator_VerifyLincWidgetsOnOcPage : T158_DesktopBase
    {
        public T158_TabletEmulator_VerifyLincWidgetsOnOcPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyLincWidgetsOnOcPage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.OrderConfirmation)]
    public class T7039_iPhone_VerifyLincWidgetsOnOcPage : T7039_MobileBase
    {
        public T7039_iPhone_VerifyLincWidgetsOnOcPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
		public void VerifyLincWidgetsOnOcPage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderConfirmation)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    public class T7039_Emulator_VerifyLincWidgetsOnOcPage : T7039_MobileBase
    {
        public T7039_Emulator_VerifyLincWidgetsOnOcPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyLincWidgetsOnOcPage(string config) => Validate(config);
    }


    /// <summary>
    /// Verify Linc widgets is visible on the OC page.
    /// JIRA Task ID: https://lampstrack.lampsplus.com:8443/browse/ACD-6525
    /// Test Case ID: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T158
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-6525"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T158")]
    public abstract class T158_DesktopBase : T158_T7039_Base
    {
        protected T158_DesktopBase(ITestOutputHelper output) : base(output) { }
    }


    /// <summary>
    /// Verify Linc widgets is visible on the OC page.
    /// Jira Task ID: https://lampstrack.lampsplus.com:8443/browse/ACD-5423
    /// Test Case ID: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7039
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5423"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7039")]
    public abstract class T7039_MobileBase : T158_T7039_Base
    {
        protected T7039_MobileBase(ITestOutputHelper output) : base(output) { }
    }


    public abstract class T158_T7039_Base : OrderConfirmationTestsBase
    {
        protected T158_T7039_Base(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            InitializeFramework(config, Urls.HomePageUrl);

            Skip.IfNot(DevEnvInformation.DatabaseSymbol == "P", "This test can only be executed against DBCLUST.");

            var getLincCompatibleProduct = ProductActions.GetLincCompatibleProduct;
            Assert.DatabaseObject(getLincCompatibleProduct, "ProductActions.GetLincCompatibleProduct()");

            Browser.Wait.ForDomReady();
            ShoppingCartWorkflow.AddItemToCartBySku(new ProductModel { Sku = getLincCompatibleProduct });

            SignInWorkflow.SignIn(LampsPlusAccounts.CustomerLoginAccount);

            Browser.Wait.IsVisibleElement(By.CssSelector(Home.CartCountId.ToCssIdSelector()));

            ManageAccountWorkflow.DeleteAllSavedAddresses();

            Browser.Navigate(Urls.CartOverviewPageUrl);

            Browser.Wait.IsVisibleElement(By.ClassName(CartOverview.CheckOutNowClass));

            CartOverview.CheckOutNowButton.Click();

            CustomerAddressInformation.CheckShippingFormIsLoaded();

            ShoppingCartWorkflow.EnterDefaultShippingAddress(UserRole.SNIS_NPCSI);
            ShoppingCartWorkflow.ProceedToPayment();

            Browser.Wait.ForDomReady();

            CustomerAddressInformation.EnterIntBillingAddress(new IntAddress());

            Payment.PlaceInternationalOrder();

            Browser.Wait.ForDomReady();

            Browser.Wait.IsVisibleElement(By.CssSelector(OrderConfirmation.LincOptinWidgetClass.ToCssClassSelector()));

            Assert.Displayed(OrderConfirmation.LincOptInWidget, "Linc Widget is Not Displayed.");
        }
    }
}
