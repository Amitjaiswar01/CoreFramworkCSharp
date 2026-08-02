using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Automation.Framework;
using Automation.Framework.Core;
using Automation.Framework.Core.ScreenCapturer;
using Automation.Framework.Exceptions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Pages.Refactored.Cart.Visual;
using LampsPlus.AutomationFramework.Pages.Refactored.CustomerAddressInformation.Visual;
using LampsPlus.AutomationFramework.Pages.Refactored.Email.Visual;
using LampsPlus.AutomationFramework.Pages.Refactored.Home.Visual;
using LampsPlus.AutomationFramework.Pages.Refactored.ManageAccount.Visual;
using LampsPlus.AutomationFramework.Pages.Refactored.OrderConfirmation.Visual;
using LampsPlus.AutomationFramework.Pages.Refactored.OrderDetails.Visual;
using LampsPlus.AutomationFramework.Pages.Refactored.ProductDetail.Visual;
using LampsPlus.AutomationFramework.Pages.Refactored.Search.Visual;
using LampsPlus.AutomationFramework.Pages.Refactored.Shipping.Visual;
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
    public class VisualTestsBaseMobile : TestsBaseMobile
    {
        private readonly FixtureBase _fixtureBase;

        //Visual Customer Address Information POM
        private ICustomerAddressInformationMobileVisual _customerAddressInformation;
        public new ICustomerAddressInformationMobileVisual CustomerAddressInformation => _customerAddressInformation ?? (_customerAddressInformation = new CustomerAddressInformationMobileVisual(Browser, Log, Settings, Address));

        //Visual Cart POM
        private ICartMobileVisual _cart;
        public new ICartMobileVisual Cart => _cart ?? (_cart = new CartMobileVisual(Browser, Modal, Drawer, Assert, ProductActions));

        //Visual OrderConfirmation POM
        private IOrderConfirmationMobileVisual _orderConfirmation;
        public new IOrderConfirmationMobileVisual OrderConfirmation => _orderConfirmation ?? (_orderConfirmation = new OrderConfirmationMobileVisual(Browser, OperatingSystem));

        //Visual Search POM
        private ISearchMobileVisual _search;
        public new ISearchMobileVisual Search => _search ?? (_search = new SearchMobileVisual(Browser, Assert, ProductActions));

        //Visual Sort POM
        private ISortMobileVisual _sort;
        public new ISortMobileVisual Sort => _sort ?? (_sort = new SortMobileVisual(Browser, Log, Drawer));

        //Visual SortPla POM
        private ISortPlaMobileVisual _sortPlaPage;
        public new ISortPlaMobileVisual SortPla => _sortPlaPage ?? (_sortPlaPage = new SortPlaMobileVisual(Browser));

        //Visual Sort Full Page Certona POM
        private ISortFullPageCertonaMobileVisual _sortFullPageCertona;
        public new ISortFullPageCertonaMobileVisual SortFullPageCertona => _sortFullPageCertona ?? (_sortFullPageCertona = new SortFullPageCertonaMobileVisual(Browser));

        //Visual Shipping POM
        private IShippingMobileVisual _shipping;
        public new IShippingMobileVisual Shipping => _shipping ?? (_shipping = new ShippingMobileVisual(Browser, Modal));

        //Visual Sign In POM
        private ISignInMobileVisual _signInPage;
        public new ISignInMobileVisual SignIn => _signInPage ?? (_signInPage = new SignInMobileVisual(Browser, Settings, Assert, Modal));

        //Visual ProductDetail POM
        private IProductDetailMobileVisual _productDetail;
        public new IProductDetailMobileVisual ProductDetail => _productDetail ?? (_productDetail = new ProductDetailMobileVisual(Browser, ProductActions, Assert, OperatingSystem, Modal));

        //Visual WishList POM
        private IWishListMobileVisual _wishList;
        public new IWishListMobileVisual WishList=> _wishList ?? (_wishList = new WishListMobileVisual(Browser, Modal, Drawer, OperatingSystem));

        //Visual Home POM
        private IHomeMobileVisual _home;
        public new IHomeMobileVisual Home => _home ?? (_home = new HomeMobileVisual(Browser));

        //Visual Order Details POM
        private IOrderDetailsMobileVisual _orderDetails;
        public new IOrderDetailsMobileVisual OrderDetails => _orderDetails ?? (_orderDetails = new OrderDetailsMobileVisual(Browser));

        //Visual Email POM
        private IEmailMobileVisual _email;
        public new IEmailMobileVisual Email => _email ?? (_email = new EmailMobileVisual(Browser, Assert));

        //Visual Manage Account POM
        private IManageAccountMobileVisual _manageAccount;
        public new IManageAccountMobileVisual ManageAccount => _manageAccount ?? (_manageAccount = new ManageAccountMobileVisual(Browser, AccountActions, Assert, Modal, Address));

        public VisualTestsBaseMobile(ITestOutputHelper output, FixtureBase fixtureBase) : base(output)
        {
            _fixtureBase = fixtureBase;
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
            InitializeVisualTestBase(config, initialUrl, disposeBrowser, fixture: _fixtureBase, skipGlobalSetup: skipGlobalSetup);
            
            if (!skipGlobalSetup)
            {
                GlobalSetupWorkflow.Setup(skipHomePageNav);
            }
        }

        public static IEnumerable<object[]> RepeatVisualTest(string baselineConfig, string targetConfig)
        {
            return VisualTestsBaseDesktop.RepeatVisualTest(baselineConfig, targetConfig);
        }

        private void VisualAccountSetup(string config, bool useEmployeeManagerAccount, TestSetup setup, FixtureBase fixtureBase)
        {
            setup.AccountConfig.AccountUnderTest = fixtureBase.GetAccountUnderTest(config, useEmployeeManagerAccount);
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