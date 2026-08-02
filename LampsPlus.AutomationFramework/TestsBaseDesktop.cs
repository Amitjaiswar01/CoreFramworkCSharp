using System;
using System.Configuration;
using Xunit.Abstractions;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Refactored.Cart;
using LampsPlus.AutomationFramework.Pages.Refactored.ContactUs;
using LampsPlus.AutomationFramework.Pages.Refactored.CreateAccount;
using LampsPlus.AutomationFramework.Pages.Refactored.Email;
using LampsPlus.AutomationFramework.Pages.Refactored.HeaderFooter;
using LampsPlus.AutomationFramework.Pages.Refactored.Home;
using LampsPlus.AutomationFramework.Pages.Refactored.ManageAccount;
using LampsPlus.AutomationFramework.Pages.Refactored.CsrBlock;
using LampsPlus.AutomationFramework.Pages.Refactored.CustomerAddressInformation;
using LampsPlus.AutomationFramework.Pages.Refactored.EmployeeOrderLookup;
using LampsPlus.AutomationFramework.Pages.Refactored.Modal;
using LampsPlus.AutomationFramework.Pages.Refactored.OrderDetails;
using LampsPlus.AutomationFramework.Pages.Refactored.OrderHistory;
using LampsPlus.AutomationFramework.Pages.Refactored.OrderConfirmation;
using LampsPlus.AutomationFramework.Pages.Refactored.OrderSummaryBlock;
using LampsPlus.AutomationFramework.Pages.Refactored.Payment;
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
using LampsPlus.AutomationFramework.Workflow.Refactored.HeaderFooterWorkflow;
using LampsPlus.AutomationFramework.Workflow.Refactored.ManageAccountWorkflow;
using LampsPlus.AutomationFramework.Workflow.Refactored.RoomViewerWorkflow;
using LampsPlus.AutomationFramework.Workflow.Refactored.ShoppingCartWorkflow;
using LampsPlus.AutomationFramework.Workflow.Refactored.SignInWorkflow;
using LampsPlus.AutomationFramework.Workflow.Refactored.WishListWorkflow;
using OperatingSystem = LampsPlus.AutomationFramework.Utilities.TestConfiguration.OperatingSystem;

namespace LampsPlus.AutomationFramework
{
    public class TestsBaseDesktop : TestsBaseRefactored.TestsBase
    {
        //Cart POM
        private ICartDesktop _cart;
        public ICartDesktop Cart => _cart ?? (_cart = new CartDesktop(Browser, Modal, Assert, ProductActions));

        //Create Account POM
        private ICreateAccountDesktop _createAccount;
        public ICreateAccountDesktop CreateAccount => _createAccount ?? (_createAccount = new CreateAccountDesktop(Browser));

        //Shopping Cart Workflow
        private IShoppingCartWorkflowDesktop _shoppingCartWorkflow;
        public IShoppingCartWorkflowDesktop ShoppingCartWorkflow => _shoppingCartWorkflow ?? (_shoppingCartWorkflow = new ShoppingCartWorkflowDesktop(Browser, Home, Assert, ProductActions, OrderSummaryBlock, ProductDetail, Modal, Shipping, Sort, Cart, OperatingSystem, CustomerAddressInformation, Payment, Address, CsrBlock, UserRole, HeaderFooter));

        //HeaderFooter  Workflow
        private IHeaderFooterWorkflowDesktop _headerFooterWorkflow;
        public IHeaderFooterWorkflowDesktop HeaderFooterWorkflow => _headerFooterWorkflow ?? (_headerFooterWorkflow = new HeaderFooterWorkflowDesktop(Browser, Search, HeaderFooter, Sort, Assert));

        //CSR Block POM
        private ICsrBlockDesktop _csrBlock;
        public ICsrBlockDesktop CsrBlock => _csrBlock ?? (_csrBlock = new CsrBlockDesktop(Browser));

        //Customer Address Information POM
        private ICustomerAddressInformationDesktop _customerAddressInformation;
        public ICustomerAddressInformationDesktop CustomerAddressInformation => _customerAddressInformation ?? (_customerAddressInformation = new CustomerAddressInformationDesktop(Browser, Log, Settings, Address));
        
        //Email POM
        private IEmailDesktop _email;
        public IEmailDesktop Email => _email ?? (_email = new EmailDesktop(Browser, Assert));

        //GlobalSetup Workflow
        private IGlobalSetupWorkflowDesktop _globalSetupWorkflowPom;
        public IGlobalSetupWorkflowDesktop GlobalSetupWorkflow => _globalSetupWorkflowPom ?? (_globalSetupWorkflowPom = new GlobalSetupWorkflowDesktop(Browser, HeaderFooter, SignInWorkflow,
            OperatingSystem, CookieUtility, UserRole, TestSetup, Log, Home, Settings, NetworkLoggingUtility, CustomerAddressInformation, ShoppingCartWorkflow));

        //HeaderFooter POM
        private IHeaderFooterDesktop _headerFooter;
        public IHeaderFooterDesktop HeaderFooter => _headerFooter ?? (_headerFooter = new HeaderFooterDesktop(Browser, Assert, Modal));

        //Home POM
        private IHomeDesktop _homePage;
        public IHomeDesktop Home => _homePage ?? (_homePage = new HomeDesktop(Browser));

        //Manage Account POM
        private IManageAccountWorkflowDesktop _manageAccountWorkflow;
        public IManageAccountWorkflowDesktop ManageAccountWorkflow => _manageAccountWorkflow ?? (_manageAccountWorkflow = new ManageAccountWorkflowDesktop(Browser, Assert, HeaderFooter, SignIn, ManageAccount, Shipping, Address));

        //Manage Account Workflow
        private IManageAccountDesktop _manageAccount;
        public IManageAccountDesktop ManageAccount => _manageAccount ?? (_manageAccount = new ManageAccountDesktop(Browser, AccountActions, Assert, Modal, Address));

        //Order Summary Block POM
        private IOrderSummaryBlockDesktop _orderSummaryBlock;
        public IOrderSummaryBlockDesktop OrderSummaryBlock => _orderSummaryBlock ?? (_orderSummaryBlock = new OrderSummaryBlockDesktop(Browser, OperatingSystem));

        //Order History POM
        private IOrderHistoryDesktop _orderHistory;
        public IOrderHistoryDesktop OrderHistory => _orderHistory ?? (_orderHistory = new OrderHistoryDesktop(Browser));

        //Order Details POM
        private IOrderDetailsDesktop _orderDetails;
        public IOrderDetailsDesktop OrderDetails => _orderDetails ?? (_orderDetails = new OrderDetailsDesktop(Browser));

        //Order Confirmation POM
        private IOrderConfirmationDesktop _orderConfirmation;
        public IOrderConfirmationDesktop OrderConfirmation => _orderConfirmation ?? (_orderConfirmation = new OrderConfirmationDesktop(Browser, OperatingSystem));

        //Payment POM
        private IPaymentDesktop _payment;
        public IPaymentDesktop Payment => _payment ?? (_payment = new PaymentDesktop(Browser, Assert, Modal));

        //ProductDetail POM
        private IProductDetailDesktop _productDetail;
        public IProductDetailDesktop ProductDetail => _productDetail ?? (_productDetail = new ProductDetailDesktop(Browser, ProductActions, Assert, OperatingSystem, Modal));

        //ProductDetailMcpDesktop POM
        private IProductDetailMcpDesktop _productDetailMcp;
        public IProductDetailMcpDesktop ProductDetailMcp => _productDetailMcp ?? (_productDetailMcp = new ProductDetailMcpDesktop(Browser));

        //ProductDetailMcpDesktop POM
        private IProductDetailDimmersDesktop _productDetailDimmers;
        public IProductDetailDimmersDesktop ProductDetailDimmers => _productDetailDimmers ?? (_productDetailDimmers = new ProductDetailDimmersDesktop(Browser));

        //RoomViewer POM
        private IRoomViewerDesktop _roomViewer;
        public IRoomViewerDesktop RoomViewer => _roomViewer ?? (_roomViewer = new RoomViewerDesktop(Browser, Modal, Assert, Settings));

        //Shipping POM
        private IShippingDesktop _shipping;
        public IShippingDesktop Shipping => _shipping ?? (_shipping = new ShippingDesktop(Browser, Modal));

        //SignIn POM
        private ISignInDesktop _signInPage;
        public ISignInDesktop SignIn => _signInPage ?? (_signInPage = new SignInDesktop(Browser, Settings, Assert, Modal));

        //SortFullPageCertona POM
        private ISortFullPageCertonaDesktop _sortFullPageCertona;
        public ISortFullPageCertonaDesktop SortFullPageCertona => _sortFullPageCertona ?? (_sortFullPageCertona = new SortFullPageCertonaDesktop(Browser));

        //SignIn Workflow
        private ISignInWorkflowDesktop _signInWorkflow;
        public ISignInWorkflowDesktop SignInWorkflow => _signInWorkflow ?? (_signInWorkflow = new SignInWorkflowDesktop(Browser, Log, SignIn, Home));

        //Sort POM
        private ISortDesktop _sortPage;
        public ISortDesktop Sort => _sortPage ?? (_sortPage = new SortDesktop(Browser, Log));

        //SortPla POM
        private ISortPlaDesktop _sortPlaPage;
        public ISortPlaDesktop SortPla => _sortPlaPage ?? (_sortPlaPage = new SortPlaDesktop(Browser));

        //Modal POM
        private IModalDesktop _modal;
        public IModalDesktop Modal => _modal ?? (_modal = new ModalDesktop(Browser));

        //GlobalTeardownWorkflow 
        private IGlobalTeardownWorkflowDesktop _globalTeardownWorkflow;
        public IGlobalTeardownWorkflowDesktop GlobalTeardownWorkflow => _globalTeardownWorkflow ?? (_globalTeardownWorkflow = new GlobalTeardownWorkflowDesktop(Browser, TestSetup, Log, Home));

        //WishList POM
        private IWishListDesktop _wishList;
        public IWishListDesktop WishList => _wishList ?? (_wishList = new WishListDesktop(Browser, Modal, OperatingSystem));

        //WishListWorkflow
        private IWishListWorkflowDesktop _wishListWorkflow;
        public IWishListWorkflowDesktop WishListWorkflow => _wishListWorkflow ?? (_wishListWorkflow = new WishListWorkflowDesktop(Browser, WishList, Sort, ProductDetail, Assert, HeaderFooter));

        //Room Viewer Workflow
        private IRoomViewerWorkflowDesktop _roomViewerWorkflow;
        public IRoomViewerWorkflowDesktop RoomViewerWorkflow => _roomViewerWorkflow ?? (_roomViewerWorkflow = new RoomViewerWorkflowDesktop(Browser, ProductDetail, Assert, RoomViewer));

        //Google Analytics Workflow
        private IGoogleAnalyticsWorkflowDesktop _googleAnalyticsWorkflow;
        public IGoogleAnalyticsWorkflowDesktop GoogleAnalyticsWorkflow => _googleAnalyticsWorkflow ?? (_googleAnalyticsWorkflow = new GoogleAnalyticsWorkflowDesktop(Browser, Log, Sort, NetworkLoggingUtility, Assert, ProductDetail, Cart));

        //Employee Order lookup POM
        private IEmployeeOrderLookupDesktop _orderLookup;
        public new IEmployeeOrderLookupDesktop OrderLookup => _orderLookup ?? (_orderLookup = new EmployeeOrderLookupDesktop(Browser, ProductActions, Assert));

        //Search POM
        private ISearchDesktop _searchPage;
        public ISearchDesktop Search => _searchPage ?? (_searchPage = new SearchDesktop(Browser, Assert, ProductActions));

        //Contact Us POM
        private IContactUsDesktop _contactUsPage;
        public IContactUsDesktop ContactUs => _contactUsPage ?? (_contactUsPage = new ContactUsDesktop(Browser));

        //CertonaWorkflow
        private ICertonaWorkflowDesktop _certonaWorkflow;
        public ICertonaWorkflowDesktop CertonaWorkflow => _certonaWorkflow ?? (_certonaWorkflow = new CertonaWorkflowDesktop(Browser, ProductDetail, ProductActions, Assert));

        public TestsBaseDesktop(ITestOutputHelper output, bool enableRealTimeLogging = false) : base(output, enableRealTimeLogging) { }

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
                    //Workflow.Desktop.SignInWorkflow.EnsureUserSignedOut();
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