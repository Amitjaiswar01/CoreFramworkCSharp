using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using Xunit.Abstractions;
using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Core;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Databases.Actions;
using LampsPlus.AutomationFramework.Pages;
using LampsPlus.AutomationFramework.Pages.Desktop;
using LampsPlus.AutomationFramework.Pages.Mobile;
using LampsPlus.AutomationFramework.Services;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Certona;
using LampsPlus.AutomationFramework.Utilities.Environment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.AutomationFramework.Workflow;
using LampsPlus.AutomationFramework.Workflow.Desktop;
using LampsPlus.AutomationFramework.Workflow.Mobile;
using Assert = Automation.Framework.Verifies.Assert;
using OperatingSystem = LampsPlus.AutomationFramework.Utilities.TestConfiguration.OperatingSystem;

//#if DebugLocal || ReleaseLocal
//[assembly: CollectionBehavior(MaxParallelThreads = 8)]
//#endif

namespace LampsPlus.AutomationFramework
{
    /// <summary>
    /// Base class for all XUnit tests.
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public class TestsBase : IDisposable
    {
        #region Private fields and properties

        /// <summary>
        /// A string that is persistent in the source throughout the entire site.
        /// Used for verifying server error pages.
        /// </summary>
        private string _persistentStringInSource = "lamps plus";
        private string _testTagName => TestSetup.TestTagName;
        private int _desiredViewPortWidth => TestSetup.DesiredViewPortWidth;

        /// <summary>
        /// Default seconds to wait for Selenium elements in production.
        /// </summary>
        public int ImplicitWaitTime = 70;

        /// <summary>
        /// Request API private class members
        /// </summary>
        private readonly string[] _localIpAddresses = ConfigurationManager.AppSettings["LocalIpAddresses"].Split(',');
        private RequestApi _requestApi;

        /// <summary>
        /// Member reference of the public Test property.
        /// </summary>
        private ITest _test;
        private CertonaUtilities _certonaUtilities;
        private CookieUtility _cookieUtility;
        private ICreateAccountWorkflow _createAccountWorkflow;
        private ISignInWorkflow _signInWorkflow;
        private IShoppingCartWorkflow _shoppingCartWorkflow;
        private ISortWorkflow _sortWorkflow;
        private IManageAccountWorkflow _manageAccountWorkflow;
        private ISubmittingOrdersWorkflow _submittingOrdersWorkflow;
        private IGlobalSetupWorkflow _globalSetupWorkflow;
        private IGlobalTeardownWorkflow _globalTeardownWorkflow;
        private ICommonWorkflow _commonWorkflow;
        private ICheckoutWorkflow _checkoutWorkflow;
        private ICreateAccount _createAccountPage;
        private CsrBlock _csrBlockPage;
        private IContactUs _contactUsPage;
        private ICustomerAddressInformation _customerAddressInformationPage;
        private IHeaderFooter _headerFooterPage;
        private IHome _homePage;
        private IGlobalLocators _globalLocators;
        private ILightingCollection _lightingCollectionPage;
        private IManageAccount _manageAccountPage;
        private IOrderConfirmation _orderConfirmationPage;
        private IOrderHistory _orderHistoryPage;
        private IOrderDetails _orderDetailsPage;
        private IOrderSummaryBlock _orderSummaryBlockPage;
        private IPayment _paymentPage;
        private IPayPal _payPalPage;
        private IProductDetail _productDetailPage;
        private IProductDetailColorPlus _productDetailColorPlusPage;
        private IProductDetailDimmers _productDetailDimmersPage;
        private IProductDetailFinishFamily _productDetailFinishFamilyPage;
        private IProductDetailMcp _productDetailMcpPage;
        private IProductDetailMultiProduct _productDetailMultiProductPage;
        private IProductDetailTiffanyColorPlus _productDetailTiffanyColorPlusPage;
        private IProductDetailTrackLighting _productDetailTrackLightingPage;
        private ISearch _searchPage;
        private ISignIn _signInPage;
        private IShipping _shippingPage;
        private ICartOverview _cartOverviewPage;
        private ISort _sortPage;
        private SortBucket _sortBucketPage;
        private ISortFullPageCertona _sortFullPageCertonaPage;
        private ISortPla _sortPlaPage;
        private IStores _storesPage;
        private SearchPageUrls _searchUrls;
        private IWishList _wishListPage;
        private IEmail _emailPage;
        private EmployeeOrderLookup _employeeOrderLookup;
        private EmployeeTools _employeeTools;
        private IMagicMerchandizer _magicMerchandizer;
        private IRoomViewer _roomViewerPage;
        #endregion

        #region Protected class fields and properties

        /// <summary>
        /// Name of the current method under test.
        /// </summary>
        protected string TestName => $"{GetType().FullName}.{TestCase?.TestCase.TestMethod.Method.Name}";
        protected bool IsVisualTest;
        protected bool IsLpInstanceSwitchForMobileTest { get; set; }

        #endregion

        #region Public fields and properties

        /// <summary>
        /// Fix version of target website
        /// </summary>
        public string TargetFixVersion { get; set; }
        /// <summary>
        /// Fix version of baseline website
        /// </summary>
        public string BaselineFixVersion { get; set; }
        /// <summary>
        /// Get the current time formatted yyyyMMddHHmmssffff.
        /// </summary>
        public string CurrentDateTime => Log.FormatDateTime(DateTime.Now);

        public string RecurringDataIssue => "Recurring Data Issue: ";

        #endregion

        #region Public class flags
        /// <summary>
        /// Flag to determine if the test configuration has been successfully ran.
        /// </summary>
        public bool IsTestConfigurationSet { get; private set; }

        /// <summary>
        /// Flag to determine if environment DbClust.
        /// </summary>
        public static bool IsDbClust { get; private set; }

        /// <summary>
        /// Should the driver be closed after the test has completed?
        /// </summary>
        public bool DisposeOfBrowserAfterTest { get; private set; }

        public bool EmptyCart { get; private set; }
        #endregion

        #region Public class instances (objects)

        /// <summary>
        /// Public Test properties (objects)
        /// </summary>
        public CsrBlock CsrBlock => _csrBlockPage ?? (_csrBlockPage = new CsrBlock(Browser));
        public IContactUs ContactUs => _contactUsPage ?? (_contactUsPage = Settings.IsMobileView ? (IContactUs)new MobileContactUs(Browser) : new ContactUs(Browser));
        public ICustomerAddressInformation CustomerAddressInformation => _customerAddressInformationPage ?? (_customerAddressInformationPage = Settings.IsMobileView ? (ICustomerAddressInformation)new MobileCustomerAddressInformation(Browser, OrderSummaryBlock, Shipping, GlobalLocators, this) : new CustomerAddressInformation(Browser, OrderSummaryBlock, Shipping, GlobalLocators, this));
        public ISignIn SignIn => _signInPage ?? (_signInPage = Settings.IsMobileView ? (ISignIn)new MobileSignIn(Browser) : new SignIn(Browser));
        public IProductDetail ProductDetail => _productDetailPage ?? (_productDetailPage = Settings.IsMobileView ? (IProductDetail)new MobileProductDetail(Browser, GlobalLocators) : new ProductDetail(Browser, GlobalLocators));
        public IProductDetailColorPlus ProductDetailColorPlus => _productDetailColorPlusPage ?? (_productDetailColorPlusPage = Settings.IsMobileView ? (IProductDetailColorPlus)new MobileProductDetailColorPlus(Browser, GlobalLocators, ProductDetail) : new ProductDetailColorPlus(Browser, GlobalLocators, ProductDetail));
        public IProductDetailDimmers ProductDetailDimmers => _productDetailDimmersPage ?? (_productDetailDimmersPage = Settings.IsMobileView ? (IProductDetailDimmers)new MobileProductDetailDimmers(Browser) : new ProductDetailDimmers(Browser));
        public IProductDetailFinishFamily ProductDetailFinishFamily => _productDetailFinishFamilyPage ?? (_productDetailFinishFamilyPage = Settings.IsMobileView ? (IProductDetailFinishFamily)new MobileProductDetailFinishFamily(Browser, GlobalLocators) : new ProductDetailFinishFamily(Browser, GlobalLocators));
        public IProductDetailMcp ProductDetailMcp => _productDetailMcpPage ?? (_productDetailMcpPage = Settings.IsMobileView ? (IProductDetailMcp)new MobileProductDetailMcp(Browser) : new ProductDetailMcp(Browser));
        public IProductDetailMultiProduct ProductDetailMultiProduct => _productDetailMultiProductPage ?? (_productDetailMultiProductPage = Settings.IsMobileView ? (IProductDetailMultiProduct)new MobileProductDetailMultiProduct(Browser, GlobalLocators, ProductDetail) : new ProductDetailMultiProduct(Browser, GlobalLocators, ProductDetail));
        public IProductDetailTiffanyColorPlus ProductDetailTiffanyColorPlus => _productDetailTiffanyColorPlusPage ?? (_productDetailTiffanyColorPlusPage = Settings.IsMobileView ? (IProductDetailTiffanyColorPlus)new MobileProductDetailTiffanyColorPlus(Browser, GlobalLocators, ProductDetail) : new ProductDetailTiffanyColorPlus(Browser, GlobalLocators, ProductDetail));
        public IProductDetailTrackLighting ProductDetailTrackLighting => _productDetailTrackLightingPage ?? (_productDetailTrackLightingPage = Settings.IsMobileView ? (IProductDetailTrackLighting)new MobileProductDetailTrackLighting(Browser, GlobalLocators) : new ProductDetailTrackLighting(Browser, GlobalLocators));
        public ICartOverview CartOverview => _cartOverviewPage ?? (_cartOverviewPage = Settings.IsMobileView ? (ICartOverview)new MobileCartOverview(Browser, ShoppingCartActions, ProductActions, GlobalLocators, Shipping) : new CartOverview(Browser, ShoppingCartActions, ProductActions, GlobalLocators, Shipping));
        public IHeaderFooter HeaderFooter => _headerFooterPage ?? (_headerFooterPage = Settings.IsMobileView ? (IHeaderFooter)new MobileHeaderFooter(Browser) : new HeaderFooter(Browser));
        public IHome Home => _homePage ?? (_homePage = Settings.IsMobileView ? (IHome)new MobileHome(Browser, GlobalLocators) : new Home(Browser, GlobalLocators));
        public IGlobalLocators GlobalLocators => _globalLocators ?? (_globalLocators = Settings.IsMobileView ? (IGlobalLocators)new MobileGlobalLocators(Browser) : new GlobalLocators(Browser));
        public ILightingCollection LightingCollection => _lightingCollectionPage ?? (_lightingCollectionPage = new LightingCollection(Browser));
        public IOrderConfirmation OrderConfirmation => _orderConfirmationPage ?? (_orderConfirmationPage = Settings.IsMobileView ? (IOrderConfirmation)new MobileOrderConfirmation(Browser, this) : new OrderConfirmation(Browser, this));
        public IOrderHistory OrderHistory => _orderHistoryPage ?? (_orderHistoryPage = Settings.IsMobileView ? (IOrderHistory)new MobileOrderHistory(Browser, this) : new OrderHistory(Browser, this));
        public IOrderDetails OrderDetails => _orderDetailsPage ?? (_orderDetailsPage = Settings.IsMobileView ? (IOrderDetails)new MobileOrderDetails(Browser) : new OrderDetails(Browser));
        public IOrderSummaryBlock OrderSummaryBlock => _orderSummaryBlockPage ?? (_orderSummaryBlockPage = Settings.IsMobileView ? (IOrderSummaryBlock)new MobileOrderSummaryBlock(Browser, this) : new OrderSummaryBlock(Browser, this));
        public IPayment Payment => _paymentPage ?? (_paymentPage = Settings.IsMobileView ? (IPayment)new MobilePayment(Browser, CustomerAddressInformation, GlobalLocators, this) : new Payment(Browser, CustomerAddressInformation, GlobalLocators, this));
        public IPayPal PayPal => _payPalPage ?? (_payPalPage = Settings.IsMobileView ? (IPayPal)new MobilePayPal(Browser) : new PayPal(Browser));
        public IManageAccount ManageAccount => _manageAccountPage ?? (_manageAccountPage = Settings.IsMobileView ? (IManageAccount)new MobileManageAccount(Browser, GlobalLocators) : new ManageAccount(Browser, GlobalLocators));
        public IRoomViewer RoomViewer => _roomViewerPage ?? (_roomViewerPage = Settings.IsMobileView ? (IRoomViewer)new MobileRoomViewer(Browser) : new RoomViewer(Browser));
        public ISearch Search => _searchPage ?? (_searchPage = Settings.IsMobileView ? (ISearch)new MobileSearch(Browser, this) : new Search(Browser, this));
        public IShipping Shipping => _shippingPage ?? (_shippingPage = Settings.IsMobileView ? (IShipping)new MobileShipping(Browser) : new Shipping(Browser));
        public ISort Sort => _sortPage ?? (_sortPage = Settings.IsMobileView ? (ISort)new MobileSort(Browser, GlobalLocators, this) : new Sort(Browser, GlobalLocators, this));
        public SortBucket SortBucket => _sortBucketPage ?? (_sortBucketPage = new SortBucket(Browser, GlobalLocators));
        public ISortFullPageCertona SortFullPageCertona => _sortFullPageCertonaPage ?? (_sortFullPageCertonaPage = Settings.IsMobileView ? (ISortFullPageCertona)new MobileSortFullPageCertona(Browser, this) : new SortFullPageCertona(Browser, this));
        public ISortPla SortPla => _sortPlaPage ?? (_sortPlaPage = Settings.IsMobileView ? (ISortPla)new MobileSortPla(Browser) : new SortPla(Browser));
        public IStores Stores => _storesPage ?? (_storesPage = Settings.IsMobileView ? (IStores)new MobileStores(Browser, GlobalLocators) : new Stores(Browser, GlobalLocators));
        public IWishList WishList => _wishListPage ?? (_wishListPage = Settings.IsMobileView ? (IWishList)new MobileWishList(Browser, GlobalLocators) : new WishList(Browser, GlobalLocators));
        public ICreateAccount CreateAccount => _createAccountPage ?? (_createAccountPage = Settings.IsMobileView ? (ICreateAccount)new MobileCreateAccount(Browser, GlobalLocators) : new CreateAccount(Browser, GlobalLocators));
        public IEmail Email => _emailPage ?? (_emailPage = Settings.IsMobileView ? (IEmail)new MobileEmail(Browser) : new Email(Browser));
        public EmployeeOrderLookup EmployeeOrderLookup => _employeeOrderLookup ?? (_employeeOrderLookup = new EmployeeOrderLookup(Browser));
        public EmployeeTools EmployeeTools => _employeeTools ?? (_employeeTools = new EmployeeTools(Browser));
        public SearchPageUrls SearchPageUrls => _searchUrls ?? (_searchUrls = new SearchPageUrls(Browser));
        public IMagicMerchandizer MagicMerchandizer => _magicMerchandizer ?? (_magicMerchandizer = (IMagicMerchandizer)new MagicMerchandizer(Browser));
        public CertonaUtilities CertonaUtilities => _certonaUtilities ?? (_certonaUtilities = new CertonaUtilities(this));
        public CookieUtility CookieUtility => _cookieUtility ?? (_cookieUtility = new CookieUtility(Browser, Assert));
        public RequestApi RequestApi => _requestApi ?? (_requestApi = new RequestApi(_localIpAddresses));
        public SessionSettings Settings { get; private set; }
        public IManageAccountWorkflow ManageAccountWorkflow => _manageAccountWorkflow ?? (_manageAccountWorkflow = (Settings.IsMobileView ? (IManageAccountWorkflow)new MobileManageAccountWorkflow(this) : new ManageAccountWorkflow(this)));
        public ISignInWorkflow SignInWorkflow => (_signInWorkflow ?? (_signInWorkflow = (Settings.IsMobileView ? (ISignInWorkflow)new MobileSignInWorkflow(this) : new SignInWorkflow(this))));
        public IShoppingCartWorkflow ShoppingCartWorkflow => _shoppingCartWorkflow ?? (_shoppingCartWorkflow = (Settings.IsMobileView ? (IShoppingCartWorkflow)new MobileShoppingCartWorkflow(CartOverview,this) : new ShoppingCartWorkflow(CartOverview,this)));
        public ICreateAccountWorkflow CreateAccountWorkflow => _createAccountWorkflow ?? (_createAccountWorkflow = (Settings.IsMobileView ? (ICreateAccountWorkflow)new MobileCreateAccountWorkflow(this) : new CreateAccountWorkflow(this)));
        public ISortWorkflow SortWorkflow => _sortWorkflow ?? (_sortWorkflow = (Settings.IsMobileView ? (ISortWorkflow)new MobileSortWorkflow(this) : new SortWorkflow(this)));
        public ISubmittingOrdersWorkflow SubmittingOrdersWorkflow => _submittingOrdersWorkflow ?? (_submittingOrdersWorkflow = Settings.IsMobileView ? (ISubmittingOrdersWorkflow)new MobileSubmittingOrdersWorkflow(this) : new SubmittingOrdersWorkflow(this));
        public IGlobalSetupWorkflow GlobalSetupWorkflow => _globalSetupWorkflow ?? (_globalSetupWorkflow = Settings.IsMobileView ? (IGlobalSetupWorkflow)new MobileGlobalSetupWorkflow(this) : new GlobalSetupWorkflow(this));
        public IGlobalTeardownWorkflow GlobalTeardownWorkflow => _globalTeardownWorkflow ?? (_globalTeardownWorkflow = Settings.IsMobileView ? (IGlobalTeardownWorkflow)new MobileGlobalTeardownWorkflow(this) : new GlobalTeardownWorkflow(this));
        public ICommonWorkflow CommonWorkflow => _commonWorkflow ?? (_commonWorkflow = Settings.IsMobileView ? (ICommonWorkflow)new MobileCommonWorkflow(this) : new CommonWorkflow(this));
        public ICheckoutWorkflow CheckoutWorkflow => _checkoutWorkflow ?? (_checkoutWorkflow = Settings.IsMobileView ? (ICheckoutWorkflow)new MobileCheckoutWorkflow(this) : new CheckoutWorkflow(this));

        /// <summary>
        /// Test ITestOutputHelper instance.
        /// </summary>
        public ITestOutputHelper OutputHelper { get; }

        /// <summary>
        /// Log class to provide common logging format.
        /// </summary>
        public Log Log { get; }

        /// <summary>
        /// Provides access to Selenium.
        /// </summary>
        public IBrowser Browser { get; set; }

        /// <summary>
        /// Provides access to ScreenCapturer.
        /// </summary>
        public IScreenCapturer ScreenCapturer { get; set; }

        /// <summary>
        /// Type of device used in the test.
        /// </summary>
        public OperatingSystem OperatingSystem => TestSetup.TestConfiguration.OperatingSystem;

        /// <summary>
        /// Browser used in the test.
        /// </summary>
        public WebBrowser WebBrowser => TestSetup.TestConfiguration.Browser;

        /// <summary>
        /// User role used in the test.
        /// </summary>
        public UserRole UserRole => TestSetup.TestConfiguration.UserRole;

        public IAssert Assert { get; private set; }

        /// <summary>
        /// Provides access to library of account actions that can be used to retrieve data from the database.
        /// </summary>
        public AccountActions AccountActions { get; private set; }

        /// <summary>
        /// Provides access to library of product actions that can be used to retrieve data from the database.
        /// </summary>
        public ProductActions ProductActions { get; private set; }

        /// <summary>
        /// Provides access to library of order actions that can be used to retrieve data from the database.
        /// </summary>
        public OrderActions OrderActions { get; private set; }

        /// <summary>
        /// Provides access to library of sort actions that can be used to retrieve data from the database.
        /// </summary>
        public SortActions SortActions { get; private set; }

        /// <summary>
        /// Provides access to library of shopping cart actions that can be used to retrieve data from the database.
        /// </summary>
        public ShoppingCartActions ShoppingCartActions { get; private set; }

        /// <summary>
        /// Provides advanced test setup and teardown capabilities. 
        /// </summary>
        public TestSetup TestSetup { get; set; }

        public EnvironmentResolver EnvironmentResolver { get; private set; }
        public DevEnvInformation DevEnvInformation { get; private set; }
        public NetworkLoggingUtility NetworkLoggingUtility { get; private set; }
        public DataCaptureUtility DataCaptureUtility { get; private set; }

        /// <summary>
        /// Get the ITest object for the current test.
        /// </summary>
        public ITest TestCase
        {
            get
            {
                if (_test == null)
                {
                    _test = (ITest)OutputHelper.GetType().GetField("test", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(OutputHelper);
                }

                return _test;
            }
        }

        #endregion

        #region Constructor

        public TestsBase(ITestOutputHelper output, bool enableRealTimeLogging = false)
        {
            OutputHelper = output;

            Log = new Log(OutputHelper, TestName, enableRealTimeLogging);
        }
        #endregion
        
        #region Private methods

        /// <summary>
        /// Move focus to the requested element and take a screenshot.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnReadyToMoveToEvent(object sender, AssertBase.WebElementEventArgs e)
        {
            Browser.MouseOverOnElement(new Element(e.Element.InternalElement, Log, string.Empty, LocatorStrategy.Js));
            Browser.TakeScreenshot(string.Empty, true);
        }

        private void InitializeSessionSettings(bool IsVisualTest)
        {
            Settings = new SessionSettings
            {
                IsBaseLine = TestSetup.TestConfiguration.IsBaseLine,

                IsMobileView = TestSetup.TestConfiguration.IsMobileView,

                IsTabletView = TestSetup.TestConfiguration.IsTabletView,

                IsTabletEmulationView = TestSetup.TestConfiguration.IsTabletEmulationView, //Verifies if Chrome tablet emulation test

                IsLocalEnvironment = EnvironmentResolver.IsLocalEnvironment,
                Browser = WebBrowser,
                ProxyAddress = EnvironmentResolver.ProxyIpAddress,

                HubIpAddress = EnvironmentResolver.HubIpAddress,
                HubPort = EnvironmentResolver.HubPort,

                IsVisualTest = IsVisualTest,

                TargetInstance = EnvironmentResolver.TargetEnvironment,

                BaselineInstance = EnvironmentResolver.BaselineEnvironment,

                SettingsTestName = TestName
            };

            var _applicationMobileGridSettings = ConfigurationManager.GetSection("GridGroup/MobileGrid") as NameValueCollection;
            var MobileHubHost = _applicationMobileGridSettings.GetValues("SeleniumMobileHubHost").FirstOrDefault();

            if (OperatingSystem == OperatingSystem.iPhone)
            {
                var MobileHubAlternativeHost = _applicationMobileGridSettings.GetValues("SeleniumMobileHubHostAlternative").FirstOrDefault();
                var IsMobileProductionRegression = _applicationMobileGridSettings.GetValues("IsProductionRegression").FirstOrDefault();

                if (IsMobileProductionRegression.Equals("true"))
                {
                    Settings.HubIpAddress = IsVisualTest ? MobileHubAlternativeHost : MobileHubHost;
                }
            }

            if (OperatingSystem == OperatingSystem.iPad)
            {
                var tabletHubPort = _applicationMobileGridSettings.GetValues("ProxyPortTablet").FirstOrDefault();

                if (IsVisualTest)
                {
                    tabletHubPort = _applicationMobileGridSettings.GetValues("ProxyVisualPortTablet").FirstOrDefault();
                }

                Settings.HubIpAddress = MobileHubHost;
                Settings.HubPort = tabletHubPort;
            }

            if (OperatingSystem == OperatingSystem.Android)
            {
                Settings.MobileDevice = LampsPlusMobileDevices.MotoX;
            }
            if (OperatingSystem == OperatingSystem.iPad)
            {
                Settings.MobileDevice = LampsPlusMobileDevices.iPadPro;
            }
            else if (OperatingSystem == OperatingSystem.iPhone)
            {
                Settings.MobileDevice = LampsPlusMobileDevices.iPhone;
            }
        }

        private void InitializeActions(DatabaseConnectionStringsManager connectionStringsManager)
        {
            AccountActions = new AccountActions(connectionStringsManager.CartEasyConnectionString);
            ProductActions = new ProductActions(connectionStringsManager.CartEasyConnectionString,
                                                connectionStringsManager.ProductsConnectionString,
                                                connectionStringsManager.ProdutMicroServicesConnectionString);
            OrderActions = new OrderActions(connectionStringsManager.CartEasyConnectionString,
                                            connectionStringsManager.AssetsConnectionString,
                                            connectionStringsManager.DomExportOrderConnectionString,
                                            connectionStringsManager.UserProfileConnectionString);
            SortActions = new SortActions(connectionStringsManager.AssetsConnectionString, connectionStringsManager.ProductsConnectionString, connectionStringsManager.CartEasyConnectionString);
            ShoppingCartActions = new ShoppingCartActions(connectionStringsManager.CartEasyConnectionString,
                                                          connectionStringsManager.AssetsConnectionString);
        }
        private void InitializeBrowser(bool visualTest)
        {
            if (OperatingSystem == OperatingSystem.Android)
            {
                Browser = new MobileBrowser(TestSetup.TestConfiguration.Device, Log, TestName, _testTagName, _desiredViewPortWidth, Settings, ImplicitWaitTime, _persistentStringInSource, visualTest, DisposeOfBrowserAfterTest);
            }

            else if (OperatingSystem == OperatingSystem.iPhone)
            {
                Browser = new IphoneBrowser(TestSetup.TestConfiguration.Device, Log, TestName, _testTagName, _desiredViewPortWidth, Settings, ImplicitWaitTime, _persistentStringInSource, visualTest, DisposeOfBrowserAfterTest);
            }

            else if (OperatingSystem == OperatingSystem.iPad)
            {
                Browser = new IpadBrowser(TestSetup.TestConfiguration.Device, Log, TestName, _testTagName, _desiredViewPortWidth, Settings, ImplicitWaitTime, _persistentStringInSource, visualTest, DisposeOfBrowserAfterTest);
            }

            else if (WebBrowser == WebBrowser.ChromeMobileView)
            {
                Browser = new MobileBrowser(WebBrowser, Log, TestName, _testTagName, _desiredViewPortWidth, Settings, ImplicitWaitTime, _persistentStringInSource, visualTest, DisposeOfBrowserAfterTest);
            }

            else if (OperatingSystem == OperatingSystem.Windows || OperatingSystem == OperatingSystem.Mac) // Desktop configuration.
            {
                Browser = new Browser(WebBrowser, Log, TestName, _testTagName, _desiredViewPortWidth, Settings, ImplicitWaitTime, _persistentStringInSource, visualTest, DisposeOfBrowserAfterTest);
            }

            else
            {
                Browser = new Browser(WebBrowser, Log, TestName, _testTagName, _desiredViewPortWidth, Settings, ImplicitWaitTime, _persistentStringInSource, visualTest, DisposeOfBrowserAfterTest);
            }
        }

        private void InitializeAssert()
        {
            Assert = new Assert(Browser);

            // Initialize events to get notified when Browser behavior is needed by a Verify statement.
            Assert.ReadyToMoveToEventHandler += OnReadyToMoveToEvent;
        }
        #endregion

        #region Protected methods
        /// <summary>
        /// Log all xUnit traits.
        /// </summary>
        protected void LogTraits()
        {
            Log.Header("Traits");

            foreach (var trait in TestCase.TestCase.Traits)
            {
                foreach (var val in trait.Value)
                {
                    Log.Message($"{trait.Key} : {val}", false);
                }
            }

            Log.Footer();
        }

        #endregion

        #region Public methods (contains entry point method InitializeFramework() and Dispose() method)

        /// <summary>
        /// Initialize a test framework based on the given configuration and optional initial URL to navigate to.
        /// </summary>
        /// <param name="config">Environment configuration used by the test.</param>
        /// <param name="url">Optional parameter: Initial URL to navigate to after framework initialization.</param>
        /// <param name="disposeBrowserAfterTest">Optional parameter: Dispose of the browser and driver after the test has completed when true.</param>
        /// <param name="skipGlobalSetup">Optional parameter: Skip the global setup Lamps Plus setup when true.</param>
        /// <param name="skipHomePageNav">Optional parameter: Skip the navigation to LP home page when true.</param>
        /// <param name="emptyCart">Optional parameter: Empties LP shopping cart when true.</param>
        /// <param name="visualTestAccount">Optional parameter: Visual test when true.</param>
        /// <param name="setup">Optional parameter: TestSetup setup when not null.</param>
        /// <param name="isInstanceSwitchMobile">Optional parameter: Is Bamboo pre-condition step (LP testing instance switch).</param>
        public void InitializeFramework(string config, string url = "", bool disposeBrowserAfterTest = true, bool skipGlobalSetup = false, bool skipHomePageNav = false, bool emptyCart = false, bool visualTestAccount = false, TestSetup setup = null , bool isInstanceSwitchMobile = false)
        {
            TestSetup = setup ?? new TestSetup(config, url);

            DisposeOfBrowserAfterTest = disposeBrowserAfterTest;

            IsVisualTest = visualTestAccount;

            EmptyCart = emptyCart;

            IsLpInstanceSwitchForMobileTest = isInstanceSwitchMobile;

            Log.Header("Begin Framework Initialization");

            EnvironmentResolver = new EnvironmentResolver(TestSetup.TestConfiguration.EnvironmentUnderTest, TestSetup.IsNetworkLoggingTest, TestSetup.TestConfiguration.OperatingSystem, Log);

            InitializeSessionSettings(visualTestAccount);

            if (IsLpInstanceSwitchForMobileTest)
            {
                SwitchMobileGridEnvironmentalTestingInstance(visualTestAccount);

                return;   //Exit method if LP instance switch mobile test.
            }

            InitializeBrowser(visualTestAccount);
            InitializeAssert();
            NetworkLoggingUtility = new NetworkLoggingUtility(Browser,Assert, Settings, OperatingSystem, RequestApi, EnvironmentResolver, Log);

            DevEnvInformation = new DevEnvInformation(TestSetup.TestConfiguration.EnvironmentUnderTest, new DenvPageParser(Browser, Settings));
            Browser.SiteVersion = DevEnvInformation.FixVersion;
            Browser.IsProdInstance = DevEnvInformation.IsProductionInstance;
            DevEnvInformation.LogInformation(Log);

            DataCaptureUtility = new DataCaptureUtility(Browser,Assert, NetworkLoggingUtility);

            Log.Message($"DatabaseConnectionString:{DevEnvInformation.DatabaseString}");
            InitializeActions(new DatabaseConnectionStringsManager(DevEnvInformation.DatabaseString));

            //TODO Check if DbClust 
            IsDbClust = DevEnvInformation.DatabaseString.Equals("clust");
            Log.Message($"Is dbClust: {IsDbClust}");

            IsTestConfigurationSet = true; // Set flag to true to indicate the InitializeFramework has completed.
            Log.Message("Framework Initialization Complete");

            ClearNetworkLogIfLoggingTest();

            if (Browser.Device != null && (Browser.Device.IsIphone || Browser.Device.IsPad))
            {
                Browser.ClearBrowserSession(Urls.DevEnvPageUrl);
            }

            Log.Header("Begin Test Case");

            if (visualTestAccount) //Exit method if visualTestAccount
            {
                return;
            }

            TestSetup.AccountSetup(); //Regression Account setup based on 'IsDbClust': DBclust or DBtest

            if (!skipGlobalSetup)
            {
                GlobalSetupWorkflow.Setup(skipHomePageNav);
            }
        }

        private void SwitchMobileGridEnvironmentalTestingInstance(bool isVisualAccount)
        {
            var testTraitValue = TestCase?.TestCase.Traits.Values.SelectMany(list => list).Distinct().ToList().First();
            switch (testTraitValue)
            {
                case LpTraits.Unit.SwitchLpInstance:
                    EnvironmentResolver.SwitchLpInstanceIphoneFunctional();
                    break;
                case LpTraits.Unit.SwitchLpInstanceIphoneVisual:
                    EnvironmentResolver.SwitchLpInstanceIphoneVisual();
                    break;
                case LpTraits.Unit.SwitchLpInstanceIpadVisual:
                    EnvironmentResolver.SwitchLpInstanceIpadVisual();
                    break;
                default:
                    EnvironmentResolver.SwitchLpInstanceMobile(NetworkLoggingUtility, isVisualAccount, IsLpInstanceSwitchForMobileTest);
                    break;
            }
        }

        /// <summary>
        /// Clears network log in Browser Mob Proxy if current test is a logging test.
        /// </summary>
        public void ClearNetworkLogIfLoggingTest()
        {
            if (!TestSetup.IsNetworkLoggingTest) return;

            Log.Message("Network HAR Log Cleared.");

            NetworkLoggingUtility.ClearNetworkLog();
        }

        /// <summary>
        /// Log the page source (DOM) for the current page.
        /// </summary>
        public void LogPageSource() { Log.LogPageSource(Browser.PageSource); }

        /// <summary>
        /// Wait for the global spinner to close.
        /// </summary>
        public void WaitForGlobalSpinnerToClose() { Browser.Wait.ForElement(Browser.Locate.ElementBySelector(HtmlTextWriterTag.Body.ToTagNotClassSelector(GlobalLocators.LoadingClass))); }

        /// <summary>
        /// Close a modal window.
        /// </summary>
        public void CloseLpModal()
        {
            Browser.Wait.ForDisplayedElement(Browser.Locate.ElementBySelector(GlobalLocators.LpModalCloseId.ToCssIdSelector()));
            Browser.Locate.ElementBySelector(GlobalLocators.LpModalCloseId.ToCssIdSelector()).Click();
            Browser.Wait.UntilElementDoesntExist(GlobalLocators.LpModalId.ToCssIdSelector());
        }

        /// <summary>
        /// Teardown method to close the WebDriver and cleanup unused resources.
        /// </summary>
        public virtual void Dispose()
        {
            DisposeMethod();
        }

        /// <summary>
        /// Teardown core method.
        /// </summary>
        public void DisposeMethod(bool skipBrowserAndCapturerDispose = false)
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
                                                       && !string.IsNullOrWhiteSpace(TestSetup.AccountConfig.AccountUnderTest.UserName))
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

                if (Browser.Device != null && (Browser.Device.IsIphone || Browser.Device.IsPad) &&
                    UserRole != UserRole.SIS_UNSI && UserRole != UserRole.SNIS_UNSI 
                    && !cloudRun)//log out with non-anonymous user roles for iOS tests only.
                {
                    SignInWorkflow.EnsureUserSignedOut();
                }

                ClearNetworkLogIfLoggingTest();

                if (!skipBrowserAndCapturerDispose)
                {
                    Browser?.Dispose();
                    //ScreenCapturer instance Disposal
                    ScreenCapturer?.ApplitoolsDispose();
                }

                Assert?.Dispose();

                Log.Message("Test Dispose Complete", false);
                Log.Footer("Teardown Complete");

                Log.ElementValidity.ExportLogToFile();
            }
        }

        /// <summary>
        /// Is an element present in the DOM and take a screenshot.
        /// </summary>
        /// <param name="cssSelector">CSS Selector to locate an element by.</param>
        /// <param name="isCheckImmediate">When true do not wait for the element to be located.</param>
        public bool IsElementPresent(string cssSelector, bool isCheckImmediate = false)
        {
            var isElementPresent = false;

            if (!isCheckImmediate)
            {
                if (Browser.Locate.ElementBySelector(cssSelector) != null)
                {
                    Browser.MouseOverOnElement(Browser.Locate.ElementBySelector(cssSelector));
                    Browser.TakeScreenshot();

                    isElementPresent = true;
                }
            }
            else { isElementPresent = Browser.Locate.ElementImmediately(cssSelector).IsInitialized; }

            return isElementPresent;
        }

        public static IElement GetElementByElementText(IElement parentElement, string control, string text)
        {
            return parentElement.FindElement(By.XPath("//" + control + "[.='" + text + "']"));
        }

        public static IEnumerable<object[]> RepeatFunctionalTest(string config) => Enumerable.Range(1, 10).Select(x => new object[] { config }).ToList();
        #endregion
    }
}
