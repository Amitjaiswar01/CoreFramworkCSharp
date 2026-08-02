using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Automation.Framework.Core;
using Automation.Framework;
using Automation.Framework.Core.ScreenCapturer;
using Automation.Framework.Exceptions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Pages.Refactored.Cart.Visual;
using LampsPlus.AutomationFramework.Pages.Refactored.CustomerAddressInformation.Visual;
using LampsPlus.AutomationFramework.Pages.Refactored.Email.Visual;
using LampsPlus.AutomationFramework.Pages.Refactored.HeaderFooter.Visual;
using LampsPlus.AutomationFramework.Pages.Refactored.Home.Visual;
using LampsPlus.AutomationFramework.Pages.Refactored.ManageAccount.Visual;
using LampsPlus.AutomationFramework.Pages.Refactored.ProductDetail.Visual;
using LampsPlus.AutomationFramework.Pages.Refactored.Shipping.Visual;
using LampsPlus.AutomationFramework.Pages.Refactored.OrderConfirmation.Visual;
using LampsPlus.AutomationFramework.Pages.Refactored.OrderDetails.Visual;
using LampsPlus.AutomationFramework.Pages.Refactored.OrderHistory.Visual;
using LampsPlus.AutomationFramework.Pages.Refactored.Search.Visual;
using LampsPlus.AutomationFramework.Pages.Refactored.SignIn.Visual;
using LampsPlus.AutomationFramework.Pages.Refactored.Sort.Visual;
using LampsPlus.AutomationFramework.Pages.Refactored.SortFullPageCertona.Visual;
using LampsPlus.AutomationFramework.Pages.Refactored.SortPla.Visual;
using LampsPlus.AutomationFramework.Pages.Refactored.WishList.Visual;
using LampsPlus.AutomationFramework.Services;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Environment;

namespace LampsPlus.VisualRegressionTests.BaseRefactored
{
    public class VisualTestsBaseDesktop : TestsBaseDesktop
    {
        private readonly FixtureBase _fixtureBase;

        //Visual Customer Address Information POM
        private ICustomerAddressInformationDesktopVisual _customerAddressInformation;
        public new ICustomerAddressInformationDesktopVisual CustomerAddressInformation => _customerAddressInformation ?? (_customerAddressInformation = new CustomerAddressInformationDesktopVisual(Browser, Log, Settings, Address));

        //Visual Cart POM
        private ICartDesktopVisual _cart;
        public new ICartDesktopVisual Cart => _cart ?? (_cart = new CartDesktopVisual(Browser, Modal, Assert, ProductActions));

        //Visual Search POM
        private ISearchDesktopVisual _search;
        public new ISearchDesktopVisual Search => _search ?? (_search = new SearchDesktopVisual(Browser, Assert, ProductActions));

        //Visual Sort POM
        private ISortDesktopVisual _sort;
        public new ISortDesktopVisual Sort => _sort ?? (_sort = new SortDesktopVisual(Browser, Log));

        //Visual SortPla POM
        private ISortPlaDesktopVisual _sortPlaPage;
        public new ISortPlaDesktopVisual SortPla => _sortPlaPage ?? (_sortPlaPage = new SortPlaDesktopVisual(Browser));

        //Visual Sort Full Page Certona POM
        private ISortFullPageCertonaDesktopVisual _sortFullPageCertona;
        public new ISortFullPageCertonaDesktopVisual SortFullPageCertona => _sortFullPageCertona ?? (_sortFullPageCertona = new SortFullPageCertonaDesktopVisual(Browser));

        //Visual OrderHistory POM
        private IOrderHistoryDesktopVisual _orderHistoryPage;
        public new IOrderHistoryDesktopVisual Orderhistory => _orderHistoryPage ?? (_orderHistoryPage = new OrderHistoryDesktopVisual(Browser));

        //Visual Shipping POM
        private IShippingDesktopVisual _shipping;
        public new IShippingDesktopVisual Shipping => _shipping ?? (_shipping = new ShippingDesktopVisual(Browser, Modal));

        //Visual Sign In POM
        private ISignInDesktopVisual _signInPage;
        public new ISignInDesktopVisual SignIn => _signInPage ?? (_signInPage = new SignInDesktopVisual(Browser, Settings, Assert, Modal));

        //Visual ProductDetail POM
        private IProductDetailDesktopVisual _productDetail;
        public new IProductDetailDesktopVisual ProductDetail => _productDetail ?? (_productDetail = new ProductDetailDesktopVisual(Browser, ProductActions, Assert, OperatingSystem, Modal));

        //Visual Order Confirmation POM
        private IOrderConfirmationDesktopVisual _orderConfirmation;
        public new IOrderConfirmationDesktopVisual OrderConfirmation => _orderConfirmation ?? (_orderConfirmation = new OrderConfirmationDesktopVisual(Browser, OperatingSystem));

        //Visual Wish List POM
        private IWishListDesktopVisual _wishList;
        public new IWishListDesktopVisual WishList => _wishList ?? (_wishList = new WishListDesktopVisual(Browser, Modal, OperatingSystem));

        //Visual Home POM
        private IHomeDesktopVisual _home;
        public new IHomeDesktopVisual Home => _home ?? (_home = new HomeDesktopVisual(Browser));

        //Visual Order Details POM
        private IOrderDetailsDesktopVisual _orderDetails;
        public new IOrderDetailsDesktopVisual OrderDetails => _orderDetails ?? (_orderDetails = new OrderDetailsDesktopVisual(Browser));

        //Visual Email POM
        private IEmailDesktopVisual _email;
        public new IEmailDesktopVisual Email => _email ?? (_email = new EmailDesktopVisual(Browser, Assert));

        //Visual Manage Account POM
        private IManageAccountDesktopVisual _manageAccount;
        public new IManageAccountDesktopVisual ManageAccount => _manageAccount ?? (_manageAccount = new ManageAccountDesktopVisual(Browser, AccountActions, Assert, Modal, Address));

        //Visual Header Footer POM
        private IHeaderFooterDesktopVisual _headerFooter;
        public new IHeaderFooterDesktopVisual HeaderFooter => _headerFooter ?? (_headerFooter = new HeaderFooterDesktopVisual(Browser, Assert, Modal));

        public VisualTestsBaseDesktop(ITestOutputHelper output, FixtureBase fixtureBase) : base(output)
        {
            _fixtureBase = fixtureBase;
        }

        public static IEnumerable<object[]> RepeatVisualTest(string baselineConfig, string targetConfig) => Enumerable.Range(1, 3).Select(x => new List<object[]> { new object[] { baselineConfig }, new object[] { targetConfig } }).SelectMany(i => i).ToArray();

        private void VisualAccountSetup(string config, bool useEmployeeManagerAccount, TestSetup setup, FixtureBase fixtureBase)
        {
            setup.AccountConfig.AccountUnderTest = fixtureBase.GetAccountUnderTest(config, useEmployeeManagerAccount);
        }

        public void InitializeVisualTestBase(string config, string initialUrl = "", bool disposeBrowser = true, bool skipGlobalSetup = false,
            bool useEmployeeManagerAccount = false, AccountConfiguration accountConfiguration = null, bool skipHomePageNav = false,
            bool emptyCart = false, bool isVisualInstanceSwitchTest = false, FixtureBase fixture = null)
        {

            LampsPlusAccount accountUnderTest = null;
            TestSetup = new TestSetup(config, initialUrl, useEmployeeManagerAccount, accountUnderTest, false);

            //If Baseline test fails but not Skipped, the Target test will be failed. If Baseline Skipped, Target will be Skipped as well with the same Skip message.
            if (!TestSetup.TestConfiguration.IsBaseLine)
            {
                Log.Message($"Is Baseline test passed:  {fixture.IsBaselinePassed}");

                if (fixture.IsBaselineSkipped)
                {
                    Skip.If(fixture.IsBaselineSkipped, fixture.SkipMessage);
                }
                else if (!fixture.IsBaselinePassed && !fixture.IsBaselineSkipped)
                {
                    throw new FrameworkVisualTestsException("Baseline visual test failed and comparison test shouldn't be executed");
                }
            }

            if (accountConfiguration != null)
            {
                TestSetup.AccountConfig.KeepMeLoggedIn = accountConfiguration.KeepMeLoggedIn;
                TestSetup.AccountConfig.StoreInSessionStoreNumber = accountConfiguration.StoreInSessionStoreNumber;
                TestSetup.AccountConfig.ClearStoreInSessionOnSetup = accountConfiguration.ClearStoreInSessionOnSetup;
                TestSetup.AccountConfig.ClearStoreInSessionOnTearDown = accountConfiguration.ClearStoreInSessionOnTearDown;
                TestSetup.AccountConfig.ClearSavedPaymentOptionsOnSetup = accountConfiguration.ClearSavedPaymentOptionsOnSetup;
                TestSetup.AccountConfig.ClearSavedShippingAddressOnSetup = accountConfiguration.ClearSavedShippingAddressOnSetup;
            }

            //Get fix versions of baseline and target for visual Batch name
            BaselineFixVersion = fixture.BaselineFixVersion;
            TargetFixVersion = fixture.TargetFixVersion;

            InitializeFramework(config, disposeBrowserAfterTest: disposeBrowser, visualTestAccount: true, setup: TestSetup, isInstanceSwitchMobile: isVisualInstanceSwitchTest);

            //Initialize Screen capturer
            ScreenCapturer = TestSetup.TestConfiguration.UseAppiumDriver ? (IScreenCapturer) new ApplitoolsScreenCapturerAppium((Browser)Browser, Log, BaselineFixVersion, TargetFixVersion, Settings) :
                new ApplitoolsScreenCapturer((Browser)Browser, Log, BaselineFixVersion, TargetFixVersion, Settings);

            VisualAccountSetup(config, useEmployeeManagerAccount, TestSetup, fixture);

            if (!TestSetup.TestConfiguration.IsBaseLine)
            {
                UserAccountManagerService.ClearUserAssets(TestSetup.AccountConfig.AccountUnderTest.UserName);
            }
        }

        public void InitializeVisualTest(string config, string initialUrl = "", bool disposeBrowser = true, bool skipGlobalSetup = false, bool useEmployeeManagerAccount = false, AccountConfiguration accountConfiguration = null, bool skipHomePageNav = false, bool emptyCart = false, bool isVisualInstanceSwitchTest = false)
        { 
           InitializeVisualTestBase(config, "", disposeBrowser, fixture: _fixtureBase,  skipGlobalSetup: skipGlobalSetup);
           
           if (!skipGlobalSetup)
           {
               GlobalSetupWorkflow.Setup(skipHomePageNav);
           }
        }

        private void IsBaselineTestPassed(bool status)
        {
            if (TestSetup.TestConfiguration.IsBaseLine)
            {
                _fixtureBase.IsBaselinePassed = status;
            }
        }

        protected void Validate(Action<string> validate, string config)
        {
            validate(config);
            IsBaselineTestPassed(true);
        }

        //Visual test Dispose
        public override void Dispose()
        {
            if (TestSetup.TestConfiguration.IsBaseLine)
            {
                Log.Message($"FixtureBase.IsBaselinePassed: {_fixtureBase.IsBaselinePassed}");
            }

            DisposeMethod();
        }
    }
}
