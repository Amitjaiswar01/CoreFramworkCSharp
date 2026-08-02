using System;
using System.Configuration;
using Automation.Framework.Utilities;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework.Pages.Refactored.Cart;
using LampsPlus.AutomationFramework.Pages.Refactored.ContactUs;
using LampsPlus.AutomationFramework.Pages.Refactored.CreateAccount;
using LampsPlus.AutomationFramework.Pages.Refactored.CsrBlock;
using LampsPlus.AutomationFramework.Pages.Refactored.CustomerAddressInformation;
using LampsPlus.AutomationFramework.Pages.Refactored.OrderSummaryBlock;
using LampsPlus.AutomationFramework.Pages.Refactored.Email;
using LampsPlus.AutomationFramework.Pages.Refactored.HeaderFooter;
using LampsPlus.AutomationFramework.Pages.Refactored.Home;
using LampsPlus.AutomationFramework.Pages.Refactored.ManageAccount;
using LampsPlus.AutomationFramework.Pages.Refactored.MobileDrawer;
using LampsPlus.AutomationFramework.Pages.Refactored.Modal;
using LampsPlus.AutomationFramework.Pages.Refactored.OrderConfirmation;
using LampsPlus.AutomationFramework.Pages.Refactored.Payment;
using LampsPlus.AutomationFramework.Pages.Refactored.OrderDetails;
using LampsPlus.AutomationFramework.Pages.Refactored.OrderHistory;
using LampsPlus.AutomationFramework.Pages.Refactored.ProductDetail;
using LampsPlus.AutomationFramework.Pages.Refactored.ProductDetailDimmers;
using LampsPlus.AutomationFramework.Pages.Refactored.ProductDetailMcp;
using LampsPlus.AutomationFramework.Pages.Refactored.RoomViewer;
using LampsPlus.AutomationFramework.Pages.Refactored.Search;
using LampsPlus.AutomationFramework.Pages.Refactored.Shipping;
using LampsPlus.AutomationFramework.Pages.Refactored.SignIn;
using LampsPlus.AutomationFramework.Pages.Refactored.Sort;
using LampsPlus.AutomationFramework.Pages.Refactored.SortFullPageCertona;
using LampsPlus.AutomationFramework.Pages.Refactored.SortPla;
using LampsPlus.AutomationFramework.Pages.Refactored.WishList;
using LampsPlus.AutomationFramework.Services;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Environment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.AutomationFramework.Workflow.Refactored.CertonaWorkflow;
using LampsPlus.AutomationFramework.Workflow.Refactored.GlobalSetupWorkflow;
using LampsPlus.AutomationFramework.Workflow.Refactored.GlobalTeardownWorkflow;
using LampsPlus.AutomationFramework.Workflow.Refactored.GoogleAnalyticsWorkflow;
using LampsPlus.AutomationFramework.Workflow.Refactored.ManageAccountWorkflow;
using LampsPlus.AutomationFramework.Workflow.Refactored.RoomViewerWorkflow;
using LampsPlus.AutomationFramework.Workflow.Refactored.SearchWorkflow;
using LampsPlus.AutomationFramework.Workflow.Refactored.ShoppingCartWorkflow;
using LampsPlus.AutomationFramework.Workflow.Refactored.SignInWorkflow;
using LampsPlus.AutomationFramework.Workflow.Refactored.WishListWorkflow;
using OperatingSystem = LampsPlus.AutomationFramework.Utilities.TestConfiguration.OperatingSystem;

namespace LampsPlus.AutomationFramework
{
    public class TestsBaseMobile : TestsBaseRefactored.TestsBase
    {
        //Cart POM
        private ICartMobile _cart;
        public ICartMobile Cart => _cart ?? (_cart = new CartMobile(Browser, Modal, Drawer, Assert, ProductActions));

        //Create Account POM
        private ICreateAccountMobile _createAccount;
        public ICreateAccountMobile CreateAccount => _createAccount ?? (_createAccount = new CreateAccountMobile(Browser));

        //Shopping Cart Workflow
        private IShoppingCartWorkflowMobile _shoppingCartWorkflow;
        public IShoppingCartWorkflowMobile ShoppingCartWorkflow => _shoppingCartWorkflow ?? (_shoppingCartWorkflow = new ShoppingCartWorkflowMobile(Browser, Assert, ProductDetail, Cart, 
            CustomerAddressInformation, Shipping, Payment, OrderSummaryBlock, Address, CsrBlock, Sort, OperatingSystem, ProductActions));

        //CSR Block POM
        private ICsrBlockMobile _csrBlock;
        public ICsrBlockMobile CsrBlock => _csrBlock ?? (_csrBlock = new CsrBlockMobile(Browser));

        //Customer Address Information POM
        private ICustomerAddressInformationMobile _customerAddressInformation;
        public ICustomerAddressInformationMobile CustomerAddressInformation => _customerAddressInformation ?? (_customerAddressInformation = new CustomerAddressInformationMobile(Browser, Log, Settings, Address));

        //SortFullPageCertona POM
        private ISortFullPageCertonaMobile _sortFullPageCertona;
        public ISortFullPageCertonaMobile SortFullPageCertona => _sortFullPageCertona ?? (_sortFullPageCertona = new SortFullPageCertonaMobile(Browser));

        //Email POM
        private IEmailMobile _email;
        public IEmailMobile Email => _email ?? (_email = new EmailMobile(Browser, Assert));

        //GlobalSetup Workflow
        private IGlobalSetupWorkflowMobile _globalSetupWorkflowPom;
        public IGlobalSetupWorkflowMobile GlobalSetupWorkflow => _globalSetupWorkflowPom ?? (_globalSetupWorkflowPom = new GlobalSetupWorkflowMobile(Browser, HeaderFooter, SignInWorkflow,
            OperatingSystem, CookieUtility, UserRole, TestSetup, Log, Home, Settings, NetworkLoggingUtility, CustomerAddressInformation, ShoppingCartWorkflow));

        //HeaderFooter POM
        private IHeaderFooterMobile _headerFooter;
        public IHeaderFooterMobile HeaderFooter => _headerFooter ?? (_headerFooter = new HeaderFooterMobile(Browser, Assert, Modal));

        //Home POM
        private IHomeMobile _homePage;
        public IHomeMobile Home => _homePage ?? (_homePage = new HomeMobile(Browser));

        //Manage Account POM
        private IManageAccountMobile _manageAccount;
        public IManageAccountMobile ManageAccount => _manageAccount ?? (_manageAccount = new ManageAccountMobile(Browser, AccountActions, Assert, Modal, Address));

        //Manage Account Workflow
        private IManageAccountWorkflowMobile _manageAccountWorkflow;
        public IManageAccountWorkflowMobile ManageAccountWorkflow => _manageAccountWorkflow ?? (_manageAccountWorkflow = new ManageAccountWorkflowMobile(Browser, Assert, HeaderFooter, SignIn, ManageAccount, Shipping, Address));
        
        //Order Summary Block POM
        private IOrderSummaryBlockMobile _orderSummaryBlock;
        public IOrderSummaryBlockMobile OrderSummaryBlock => _orderSummaryBlock ?? (_orderSummaryBlock = new OrderSummaryBlockMobile(Browser, OperatingSystem));

        //Payment POM
        private IPaymentMobile _payment;
        public IPaymentMobile Payment => _payment ?? (_payment = new PaymentMobile(Browser, Assert, Modal));

        //Order History POM
        private IOrderHistoryMobile _orderHistory;
        public IOrderHistoryMobile OrderHistory => _orderHistory ?? (_orderHistory = new OrderHistoryMobile(Browser));

        //Order Details POM
        private IOrderDetailsMobile _orderDetails;
        public IOrderDetailsMobile OrderDetails => _orderDetails ?? (_orderDetails = new OrderDetailsMobile(Browser));

        //Order Confirmation POM
        private IOrderConfirmationMobile _orderConfirmation;
        public IOrderConfirmationMobile OrderConfirmation => _orderConfirmation ?? (_orderConfirmation = new OrderConfirmationMobile(Browser, OperatingSystem));

        //ProductDetail POM
        private IProductDetailMobile _productDetail;
        public IProductDetailMobile ProductDetail => _productDetail ?? (_productDetail = new ProductDetailMobile(Browser, ProductActions, Assert, OperatingSystem, Modal));

        //ProductDetailMcpDesktop POM
        private IProductDetailDimmersMobile _productDetailDimmers;
        public IProductDetailDimmersMobile ProductDetailDimmers => _productDetailDimmers ?? (_productDetailDimmers = new ProductDetailDimmersMobile(Browser));

        //ProductDetailMcpMobile POM
        private IProductDetailMcpMobile _productDetailMcp;
        public IProductDetailMcpMobile ProductDetailMcp => _productDetailMcp ?? (_productDetailMcp = new ProductDetailMcpMobile(Browser));

        //RoomViewer POM
        private IRoomViewerMobile _roomViewer;
        public IRoomViewerMobile RoomViewer => _roomViewer ?? (_roomViewer = new RoomViewerMobile(Browser, Modal, Assert, Settings));

        //SignIn POM
        private ISignInMobile _signInPage;
        public ISignInMobile SignIn => _signInPage ?? (_signInPage = new SignInMobile(Browser, Settings, Assert, Modal));

        //SignIn Workflow
        private ISignInWorkflowMobile _signInWorkflow;
        public ISignInWorkflowMobile SignInWorkflow => _signInWorkflow ?? (_signInWorkflow = new SignInWorkflowMobile(Browser, Log,  SignIn, Home));

        //Sort POM
        private ISortMobile _sort;
        public ISortMobile Sort => _sort ?? (_sort = new SortMobile(Browser, Log, Drawer));

        //SortPla POM
        private ISortPlaMobile _sortPlaPage;
        public ISortPlaMobile SortPla => _sortPlaPage ?? (_sortPlaPage = new SortPlaMobile(Browser));

        //Shipping POM
        private IShippingMobile _shipping;
        public IShippingMobile Shipping => _shipping ?? (_shipping = new ShippingMobile(Browser, Modal));

        //Mobile drawer POM
        private IMobileDrawer _drawer;
        public IMobileDrawer Drawer => _drawer ?? (_drawer = new MobileDrawer(Browser));

        //Modal POM
        private IModalMobile _modal;
        public IModalMobile Modal => _modal ?? (_modal = new ModalMobile(Browser));

        //GlobalTeardownWorkflow 
        private IGlobalTeardownWorkflowMobile _globalTeardownWorkflow;
        public IGlobalTeardownWorkflowMobile GlobalTeardownWorkflow => _globalTeardownWorkflow ?? (_globalTeardownWorkflow = new GlobalTeardownWorkflowMobile(Browser, TestSetup, Log, Home));

        //WishList POM
        private IWishListMobile _wishList;
        public IWishListMobile WishList => _wishList ?? (_wishList = new WishListMobile(Browser, Modal, Drawer, OperatingSystem));

        //WishList Workflow
        private IWishListWorkflowMobile _wishListWorkflow;
        public IWishListWorkflowMobile WishListWorkflow => _wishListWorkflow ?? (_wishListWorkflow = new WishListWorkflowMobile(Browser, WishList, Sort, ProductDetail, Assert));

        //Google Analytics Workflow
        private IGoogleAnalyticsWorkflowMobile _googleAnalyticsWorkflow;
        public IGoogleAnalyticsWorkflowMobile GoogleAnalyticsWorkflow => _googleAnalyticsWorkflow ?? (_googleAnalyticsWorkflow = new GoogleAnalyticsWorkflowMobile(Browser, Log, Sort, NetworkLoggingUtility, Assert, ProductDetail, Cart));

        //Search POM
        private ISearchMobile _search;
        public ISearchMobile Search => _search ?? (_search = new SearchMobile(Browser, Assert, ProductActions));

        //Contact Us POM
        private IContactUsMobile _contactUsPage;
        public IContactUsMobile ContactUs => _contactUsPage ?? (_contactUsPage = new ContactUsMobile(Browser));

        //CertonaWorkflow
        private ICertonaWorkflowMobile _certonaWorkflow;
        public ICertonaWorkflowMobile CertonaWorkflow => _certonaWorkflow ?? (_certonaWorkflow = new CertonaWorkflowMobile(Browser, ProductDetail, ProductActions, Assert));

        //Room Viewer Workflow
        private IRoomViewerWorkflowMobile _roomViewerWorkflow;
        public IRoomViewerWorkflowMobile RoomViewerWorkflow => _roomViewerWorkflow ?? (_roomViewerWorkflow = new RoomViewerWorkflowMobile(Browser, ProductDetail, Assert, RoomViewer));

        //Search Workflow 
        private ISearchWorkflowMobile _searchWorkflow;
        public ISearchWorkflowMobile SearchWorkflow => _searchWorkflow ?? (_searchWorkflow = new SearchWorkflowMobile(Browser, Search, Sort, ProductDetail));

        public TestsBaseMobile(ITestOutputHelper output, bool enableRealTimeLogging = false) : base(output, enableRealTimeLogging) { }

        protected void InitializeFunctionalTest(string config, string url = "", bool disposeBrowserAfterTest = true, bool skipGlobalSetup = false, bool skipHomePageNav = false, bool emptyCart = false, bool visualTestAccount = false, TestSetup setup = null, bool isInstanceSwitchTest = false)
        {
            InitializeFramework(config, url, disposeBrowserAfterTest, skipGlobalSetup, skipHomePageNav, emptyCart, visualTestAccount, setup, isInstanceSwitchTest);
            
            if (!skipGlobalSetup)
            {
                GlobalSetupWorkflow.Setup(skipHomePageNav);
            }
        }

        public override void Dispose()
        {
            DisposeMethod();
        }

        /// <summary>
        /// Teardown core method.
        /// </summary>
        public void DisposeMethod()
        {
            if (IsLpInstanceSwitchForMobileTest) return; //Exit method if LP instance switch mobile test.

            try
            {
                if (Browser.IsTestFailed && OperatingSystem == OperatingSystem.Windows && !Settings.IsMobileView && !Settings.IsTabletEmulationView)
                {
                    Browser?.TakeScreenshot(chromeDriverEntirePageScreenshot: true);
                }
                else
                {
                    Browser?.TakeScreenshot();
                }

                Log.Footer("Test Case Complete");

                if (IsTestConfigurationSet)
                {
                    GlobalTeardownWorkflow.TearDown();
                }

                if (TestSetup.TearDownAccountUnderTest && TestSetup.AccountConfig.AccountUnderTest != null
                                                       && !string.IsNullOrWhiteSpace(TestSetup.AccountConfig
                                                           .AccountUnderTest.UserName))
                    UserAccountManagerService.ReleaseUser(TestSetup.AccountConfig.AccountUnderTest.UserName);
            }
            catch (Exception e)
            {
                Log.BlockMessage($"EXCEPTION: {e}");
            }
            finally
            {
                LogTraits();
                Log.Header("Begin Test Dispose");

                var cloudRun = ConfigurationManager.AppSettings["MobileGridCloud"].CaseInsensitiveContains("true");

                if ((EmptyCart || Browser.Device != null && Browser.Device.IsIphone && Browser.DisposeBrowserAfterTest 
                               || Browser.Device != null && Browser.Device.IsPad && Browser.DisposeBrowserAfterTest)
                    && !cloudRun)
                {
                    Browser.Navigate(Urls.CartOverviewPageUrl);
                    ShoppingCartWorkflow.EmptyCart(); //Required, so that product doesn't appear in Resubmit utility.
                    Browser?.TakeScreenshot("", false, false);
                }

                if (Browser.Device != null && (Browser.Device.IsIphone || Browser.Device.IsPad) 
                    && UserRole != UserRole.SIS_UNSI && UserRole != UserRole.SNIS_UNSI
                    && !cloudRun)//log out with non-anonymous user roles for iOS tests only.
                {
                    HeaderFooter.SignOut();
                }

                ClearNetworkLogIfLoggingTest();

                Browser?.Dispose();

                //ScreenCapturer instance Disposal
                ScreenCapturer?.ApplitoolsDispose();

                Assert?.Dispose();

                Log.Message("Test Dispose Complete", false);
                Log.Footer("Teardown Complete");

                Log.ElementValidity.ExportLogToFile();
            }
        }
    }
}
