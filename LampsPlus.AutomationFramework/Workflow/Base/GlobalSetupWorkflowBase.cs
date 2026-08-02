using System.Configuration;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using OpenQA.Selenium;

namespace LampsPlus.AutomationFramework.Workflow.Base
{
    /// <summary>
    /// Common behavior used for global setup workflow.
    /// </summary>
    public abstract class GlobalSetupWorkflowBase : WorkflowBase, IGlobalSetupWorkflow
    {
        protected GlobalSetupWorkflowBase(TestsBase testsBase) : base(testsBase) { }

        /// <inheritdoc />
        public void Setup(bool skipHomePageNavigation = false)
        {
            Browser.Navigate(Urls.HomePageUrl);

            Browser.Wait.IsVisibleElement(!TestsBase.Settings.IsMobileView
                ? By.CssSelector(TestsBase.Home.HomepageSplashBannerClass.ToCssClassSelector())
                : By.CssSelector(TestsBase.Home.HpSplashImgClass.ToCssClassSelector()));

            var cloudRun = ConfigurationManager.AppSettings["MobileGridCloud"].CaseInsensitiveContains("true");

            if ((TestsBase.OperatingSystem == OperatingSystem.iPad || TestsBase.OperatingSystem == OperatingSystem.iPhone 
                                                                   || TestsBase.Settings.IsTabletEmulationView 
                                                                   || TestsBase.Settings.IsMobileView) 
                                                                   && !cloudRun)
            {
                TestsBase.SignInWorkflow.EnsureUserSignedOut();
            }

            if (TestsBase.UserRole != UserRole.SIS_UNSI && TestsBase.UserRole != UserRole.SNIS_UNSI)
            {
                TestsBase.SignInWorkflow.SignInWithUserRole(TestsBase.TestSetup);

                if (!TestsBase.Settings.IsMobileView)
                {
                    Browser.Wait.IsVisibleElement(TestsBase.UserRole != UserRole.SNIS_HCSI
                        ? By.CssSelector(TestsBase.Home.HomepageSplashBannerClass.ToCssClassSelector())
                        : By.CssSelector(TestsBase.Home.IsHospitalityClass.ToCssClassSelector()));
                }
                else
                {
                    Browser.Wait.IsVisibleElement(!TestsBase.Settings.IsMobileView
                        ? By.CssSelector(TestsBase.Home.HomepageSplashBannerClass.ToCssClassSelector())
                        : By.CssSelector(TestsBase.Home.HpSplashImgClass.ToCssClassSelector()));
                }
            }

            TestsBase.ClearNetworkLogIfLoggingTest();

            if (!string.IsNullOrWhiteSpace(TestsBase.TestSetup.InitialUrl))
            {
                Browser.Navigate(TestsBase.TestSetup.InitialUrl);
            }

            if (!TestsBase.Settings.IsMobileView)
            {
                SetStoreInSessionOnSetup();
            }

            TestsBase.CookieUtility.DisableCheckoutSurvey();

            if (TestsBase.TestSetup.TestConfiguration.IsSearchRelatedTest)
            {
                SetSearchProvider(TestsBase.TestSetup.TestConfiguration.IsUsingEasyAsk);
            }
        }

        protected void SetSearchProvider(bool isUsingEasyAsk)
        {
            Browser.DeleteCookie("SortAbTestSearchProvider_v5");
            if (isUsingEasyAsk)
            {
                Browser.AddCookie("SortAbTestSearchProvider_v5", "EasyAsk");
                TestsBase.Log.Message("Search Provider set to EasyAsk.");
            }
            else
            {
                Browser.AddCookie("SortAbTestSearchProvider_v5", "ElasticSearch");
                TestsBase.Log.Message("Search Provider set to ElasticSearch.");
            }
        }

        /// <summary>
        /// Set store in session.
        /// </summary>
        protected void SetStoreInSessionOnSetup()
        {
            // Need to be on LP site for the logic below
            if (Browser.PageUrl.ToLower().Contains("denv.aspx") || !Browser.PageUrl.ToLower().Contains("lampsplus"))
            {
                Browser.Navigate(Urls.HomePageUrl);
            }

            if (TestsBase.UserRole == UserRole.SIS_UNSI || TestsBase.UserRole == UserRole.SIS_ESI)
            {
                TestsBase.CookieUtility.EnterStoreInSessionMode();
            }
            else if (!string.IsNullOrEmpty(TestsBase.TestSetup.AccountConfig.StoreInSessionStoreNumber))
            {
                TestsBase.Home.EnterStoreInSession(TestsBase.TestSetup.AccountConfig.StoreInSessionStoreNumber);
                TestsBase.Log.Message(
                    $"Enter store in session for store {TestsBase.TestSetup.AccountConfig.StoreInSessionStoreNumber}");
            }
            else if (TestsBase.TestSetup.AccountConfig.ClearStoreInSessionOnSetup)
            {
                TestsBase.Home.EnterStoreInSession("0");
                TestsBase.Log.Message("Store in session cleared");
            }
        }

        private void EmptyAssetsOnSetup()
        {
            if (!TestsBase.SignInWorkflow.IsLoggedInUser) return;
            if (!TestsBase.TestSetup.ShoppingCartConfig.EmptyOnSetup) return;
            TestsBase.ShoppingCartWorkflow.EmptyCart();
            TestsBase.Log.Message("The cart is empty");

            if (!string.IsNullOrWhiteSpace(TestsBase.TestSetup.InitialUrl))
            {
                Browser.Navigate(TestsBase.TestSetup.InitialUrl);
            }

            //if (TestsBase.TestSetup.WishListConfig.EmptyOnSetup)
            //{
            //    TestsBase.WishListWorkflow.DeleteCurrentUsersWishLists();
            //    TestsBase.Log.Message("All wishlists were deleted");
            //}
        }

        private void EmptyAccountInfoOnSetup()
        {
	        if (TestsBase.SignInWorkflow.IsLoggedInUser && !TestsBase.SignInWorkflow.IsLoggedInAsCustomerService)
	        {
		        if (TestsBase.TestSetup.AccountConfig.SavedPaymentOptionsConfig.EmptyOnSetup)
		        {
			        TestsBase.ManageAccountWorkflow.DeleteAllSavedPaymentOptions();
			        TestsBase.Log.Message("Saved payments deleted");
		        }

		        if (TestsBase.TestSetup.AccountConfig.SavedShippingAddressConfig.EmptyOnSetup)
		        {
			        TestsBase.ManageAccountWorkflow.DeleteAllSavedAddresses();
			        TestsBase.Log.Message("Saved addresses deleted");
		        }
	        }
        }
    }
}
