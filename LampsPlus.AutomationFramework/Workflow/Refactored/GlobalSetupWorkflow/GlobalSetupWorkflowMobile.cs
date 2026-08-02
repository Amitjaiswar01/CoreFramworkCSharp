using System.Configuration;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Refactored.CustomerAddressInformation;
using LampsPlus.AutomationFramework.Pages.Refactored.HeaderFooter;
using LampsPlus.AutomationFramework.Pages.Refactored.Home;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Environment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.AutomationFramework.Workflow.Refactored.ShoppingCartWorkflow;
using LampsPlus.AutomationFramework.Workflow.Refactored.SignInWorkflow;

namespace LampsPlus.AutomationFramework.Workflow.Refactored.GlobalSetupWorkflow
{
    public class GlobalSetupWorkflowMobile : IGlobalSetupWorkflowMobile
    {
        public GlobalSetupWorkflowMobile(IBrowser browser, IHeaderFooterMobile headerFooter, ISignInWorkflowMobile signInWorkflowMobile,
           OperatingSystem operatingSystem, CookieUtility cookieUtility, UserRole userRole, TestSetup testSetup, Log log, IHomeMobile home,
           SessionSettings settings, NetworkLoggingUtility networkLoggingUtility, ICustomerAddressInformationMobile customerAddressInformationMobile, IShoppingCartWorkflowMobile shoppingCartWorkflowMobile)
        {
            _browser = browser;
            _signInWorkflow = signInWorkflowMobile;
            _headerFooter = headerFooter;
            _operatingSystem = operatingSystem;
            _cookieUtility = cookieUtility;
            _userRole = userRole;
            _testSetup = testSetup;
            _log = log;
            _home = home;
            _settings = settings;
            _networkLoggingUtility = networkLoggingUtility;
            _customerAddressInformation = customerAddressInformationMobile;
            _shoppingCartWorkflow = shoppingCartWorkflowMobile;
        }

        //Mobile POM and Workflow instances
        private readonly ISignInWorkflowMobile _signInWorkflow;
        private readonly IHeaderFooterMobile _headerFooter;
        private readonly IHomeMobile _home;
        private readonly ICustomerAddressInformationMobile _customerAddressInformation;
        private readonly IShoppingCartWorkflowMobile _shoppingCartWorkflow;

        //TestsBase instances
        private readonly IBrowser _browser;
        private readonly OperatingSystem _operatingSystem;
        private readonly CookieUtility _cookieUtility;
        private readonly UserRole _userRole;
        private readonly TestSetup _testSetup;
        private readonly Log _log;
        private readonly SessionSettings _settings;
        private readonly NetworkLoggingUtility _networkLoggingUtility;

        //Class members
        private void SetStoreInSessionOnSetup()
        {
            // Need to be on LP site for the logic below
            if (_browser.PageUrl.ToLower().Contains("denv.aspx") || !_browser.PageUrl.ToLower().Contains("lampsplus"))
            {
                _browser.Navigate(Urls.HomePageUrl);
            }

            if (_userRole == UserRole.SIS_UNSI || _userRole == UserRole.SIS_ESI)
            {
                _cookieUtility.EnterStoreInSessionMode();
            }
            else if (!string.IsNullOrEmpty(_testSetup.AccountConfig.StoreInSessionStoreNumber))
            {
                _home.EnterStoreInSession(_testSetup.AccountConfig.StoreInSessionStoreNumber);
                _log.Message(
                    $"Enter store in session for store {_testSetup.AccountConfig.StoreInSessionStoreNumber}");
            }
            else if (_testSetup.AccountConfig.ClearStoreInSessionOnSetup)
            {
                _home.EnterStoreInSession("0");
                _log.Message("Store in session cleared");
            }
        }

        //Interface implementation
        public void Setup(bool skipHomePageNavigation = false)
        {
            var cloudRun = ConfigurationManager.AppSettings["MobileGridCloud"].CaseInsensitiveContains("true");

            if ((_operatingSystem == OperatingSystem.iPad || _operatingSystem == OperatingSystem.iPhone) && !cloudRun)//log out for iOS tests only.
            {
                _browser.Navigate(Urls.HomePageUrl);
                _home.WaitForHomePageToLoad();
                _headerFooter.SignOut();
            }

            if (_userRole != UserRole.SIS_UNSI && _userRole != UserRole.SNIS_UNSI)
            {
                _signInWorkflow.SignInAndClearSession(_testSetup.AccountConfig.AccountUnderTest.UserName, _testSetup.AccountConfig.AccountUnderTest.Password);
                _home.WaitForHomePageToLoad();
            }

            if (!_testSetup.IsNetworkLoggingTest)
            {
                _log.Message("Network HAR Log Cleared.");

                _networkLoggingUtility.ClearNetworkLog();
            }

            if (!string.IsNullOrWhiteSpace(_testSetup.InitialUrl))
            {
                _browser.Navigate(_testSetup.InitialUrl);
            }

            if (string.IsNullOrWhiteSpace(_testSetup.InitialUrl) && _settings.IsMobileView && _operatingSystem == OperatingSystem.Windows)
            {
                _browser.Navigate(Urls.HomePageUrl);
                _home.WaitForHomePageToLoad();
            }

            _cookieUtility.DisableCheckoutSurvey();

            if (_testSetup.TestConfiguration.IsSearchRelatedTest)
            {
                SetSearchProvider(_testSetup.TestConfiguration.IsUsingEasyAsk);
            }
        }

        protected void SetSearchProvider(bool isUsingEasyAsk)
        {
            _browser.DeleteCookie("SortAbTestSearchProvider_v5");
            if (isUsingEasyAsk)
            {
                _browser.AddCookie("SortAbTestSearchProvider_v5", "EasyAsk");
                _log.Message("Search Provider set to EasyAsk.");
            }
            else
            {
                _browser.AddCookie("SortAbTestSearchProvider_v5", "ElasticSearch");
                _log.Message("Search Provider set to ElasticSearch.");
            }
        }

        private void EmptyAssetsOnSetup()
        {
            if (_customerAddressInformation.IsLoggedInUser)
            {
                if (_testSetup.ShoppingCartConfig.EmptyOnSetup)
                {
                    _shoppingCartWorkflow.EmptyCart();
                    _log.Message("The cart is empty");
                }

                //if (TestsBase.TestSetup.WishListConfig.EmptyOnSetup)
                //{
                //    TestsBase.WishListWorkflow.DeleteCurrentUsersWishLists();
                //    TestsBase.Log.Message("All wishlists were deleted");
                //}
            }
        }
    }
}