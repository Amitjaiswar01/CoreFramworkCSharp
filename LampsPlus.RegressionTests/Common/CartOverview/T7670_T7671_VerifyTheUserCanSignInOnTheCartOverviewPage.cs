using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Enums;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.DesktopTablet.CartOverview;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.Common.CartOverview
{
    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T7670_Windows_VerifyTheUserCanSignInOnTheCartOverviewPage : T7670_DesktopBase
    {
        public T7670_Windows_VerifyTheUserCanSignInOnTheCartOverviewPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void SignInOnCartPage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T7670_Mac_VerifyTheUserCanSignInOnTheCartOverviewPage : T7670_DesktopBase
    {
        public T7670_Mac_VerifyTheUserCanSignInOnTheCartOverviewPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void SignInOnCartPage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T7670_iPad_VerifyTheUserCanSignInOnTheCartOverviewPage : T7670_DesktopBase
    {
        public T7670_iPad_VerifyTheUserCanSignInOnTheCartOverviewPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void SignInOnCartPage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T7670_TabletEmulator_VerifyTheUserCanSignInOnTheCartOverviewPage : T7670_DesktopBase
    {
        public T7670_TabletEmulator_VerifyTheUserCanSignInOnTheCartOverviewPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void SignInOnCartPage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.CartOverview)]
    public class T7671_iPhone_VerifyTheUserCanSignInOnTheCartOverviewPage : T7671_MobileBase
    {
        public T7671_iPhone_VerifyTheUserCanSignInOnTheCartOverviewPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void SignInOnCartPage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T7671_Emulator_VerifyTheUserCanSignInOnTheCartOverviewPage : T7671_MobileBase
    {
        public T7671_Emulator_VerifyTheUserCanSignInOnTheCartOverviewPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void SignInOnCartPage(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the user can sign in on the Cart Overview page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8880
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7670
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8880"), Trait(LpTraits.RequiredTestCaseTags.TId, "T7670")]
    public abstract class T7670_DesktopBase : T7670_T7671_Base
    {
        protected T7670_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected override void SignInOnCart()
        {
            SignInWorkflow.SignInFromHeader(LampsPlusAccounts.CustomerLoginAccount);
        }

        protected override void VerifyLinkAfterLogin()
        {
            Assert.Displayed(HeaderFooter.MyAccountLink, "My Account is not displayed");
        }
    }


    /// <summary>
    /// Verify the user can sign in on the Cart Overview page.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8880
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7671
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8880"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T392")]
    public abstract class T7671_MobileBase : T7670_T7671_Base
    {
        protected T7671_MobileBase(ITestOutputHelper output) : base(output) { }

        protected override void SignInOnCart()
        {
            SignInWorkflow.SignInHamburger(LampsPlusAccounts.CustomerLoginAccount);   
        }

        protected override void VerifyLinkAfterLogin()
        {
            Assert.Displayed(CartOverview.WelcomeBackMessage, "Welcome back message is not displayed");
        }
    }


    public abstract class T7670_T7671_Base : ShoppingCartTestsBase
    {
        protected T7670_T7671_Base(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            InitializeFramework(config);

            var shortSku = ProductActions.GetShortSkuWithShippingCharge(SubLocationCode.Lp);

            Assert.DatabaseObject(shortSku, "ProductActions.GetShortSkuWithShippingCharge(SubLocationCode.Lp)");

            ShoppingCartWorkflow.AddItemToCartBySku(new ProductModel { Sku = shortSku });

            var skuForUnsi = CartOverview.ProductSkuCart;

            SignInOnCart();

            Browser.Wait.IsVisibleElement(By.CssSelector(CartOverview.CheckOutNowClass.ToCssClassSelector()));

            var skuForCsi = CartOverview.ProductSkuCart;

            Assert.Equals(skuForUnsi, skuForCsi, "Sku in cart is not same after Signin");

            VerifyLinkAfterLogin();
        }

        protected abstract void SignInOnCart();

        protected abstract void VerifyLinkAfterLogin();
    }
}
