namespace LampsPlus.AutomationFramework.Constants
{
    /// <summary>
    /// String constants for XUnit categories (Traits) for grouping tests.
    /// </summary>
    public static class LpTraits
    {
        /// <summary>
        /// Tests that are executed exclusively against db_test database.
        /// </summary>
        public class PPE
        {
            public const string DbTest = "DbTest";
        }


        /// <summary>
        /// Executable suites.
        /// </summary>
        public class Suite
        {
            public const string Desktop = "Desktop";
            public const string Mobile = "Mobile";
            public const string Regression = "Regression";
        }

        
        /// <summary>
        /// Key value.
        /// </summary>
        public class Keys : Suite
        {
            public const string Category = "Category";
            public const string Database = "Database";
            public const string Feature = "Feature";

            /// <summary>
            /// Use help for helper utilities like killing drivers.
            /// </summary>
            public const string Help = "Help";
        }


        /// <summary>
        /// Used to identify tests that deal with creation, reading, updating, or deleting.
        /// </summary>
        public class Categories
        {
	        public const string CRUD = "CRUD";
		}


        /// <summary>
        /// Tags added for every automated test case.
        /// </summary>
        public class RequiredTestCaseTags
        {
            public const string TaskId = "Task ID";
            // ReSharper disable once InconsistentNaming
            public const string TId = "Test ID";
            public const string Os = "Operating System";
            public const string UserRole = "User Role";
        }


        /// <summary>
        /// Feature being tested.
        /// </summary>
        public class RegressionFeatureTags
        {
            public const string AddingToCartAndWishlist = "Shopping Cart & Wishlist";
            public const string AugmentedReality = "Augmented Reality";
            public const string CartOverview = "Cart Overview";
            public const string ChangeEmailPreferences = "Change Email Preferences";
            public const string ContactUs = "Contact Us";
            public const string CreateAccount = "Create Account";
            public const string DataCapture = "Data Capture";
            public const string GlobalLocators = "Global Locators";
            public const string HeaderFooter = "Header and Footer";
            public const string Homepage = "Homepage";
            public const string ManageAccount = "Manage Account";
            public const string OrderHistory = "Order History";
            public const string OrderSummaryBlock = "Order Summary Block";
            public const string Payment = "Payment";
            public const string Pixel = "Pixel";
            public const string ProductDetail = "Product Detail";
            public const string Search = "Search";
            public const string Shipping = "Shipping";
            public const string SignIn = "Sign In";
            public const string Sort = "Sort";
            public const string Stores = "Stores";
            public const string OrderConfirmation = "OrderConfirmation";
        }

       
        /// <summary>
        /// Integration specific tests.
        /// </summary>
        public class Integration
        {
            public const string PageObjectModel = "Page Object Model";
        }

        /// <summary>
        /// Unit specific tests.
        /// </summary>
        public class Unit
        {
            public const string MobileSafari = "ClearSafariSession";
            public const string MobileIpadSafari = "ClearIpadSafariSession";
            public const string SwitchLpInstance = "SwitchLpInstance";
            public const string SwitchLpInstanceIphoneVisual = "SwitchLpInstanceIphoneVisual";
            public const string SwitchLpInstanceIpad = "SwitchLpInstanceIpad";
            public const string SwitchLpInstanceIpadVisual = "SwitchLpInstanceIpadVisual";
        }


        /// <summary>
        /// Experimental trait to group failures runs/fixes at batch file by Exception.
        /// </summary>
        public class Experimental
        {
            public const string Debugging = "Debugging"; //Debugging trait 
        }


        /// <summary>
        /// Trait to modify user accounts data via database queries.
        /// </summary>
        public class AccountActions
        {
            public const string EnableDisabledTestAccountsWindows = "EnableDisabledTestAccountsWindows"; // trait to enable disabled users in database
            public const string EnableDisabledTestAccountsIos = "EnableDisabledTestAccountsIos"; // trait to enable disabled users in database
        }

        /// <summary>
        /// Operating system definitions.
        /// </summary>
        public class OperatingSystem
        {
            public const string AndroidEightPhone = "Android 8 Phone";
            public const string ChromeMobileEmulation = "Chrome Mobile Emulation";
            public const string ChromeTabletEmulation = "Chrome Tablet Emulation";
            public const string iOsPhoneLatestSupportedVersion = "iOS 14 Phone";
            public const string iOsTabletLatestSupportedVersion = "iOS 14 Tablet";
            public const string MacMojave = "Mac Mojave";
            public const string WindowsTen = "Windows 10";
        }


        /// <summary>
        /// User role definitions.
        /// </summary>
        public class UserRole
        {
            public const string Anonymous = "SNIS-UNSI";
            public const string Customer = "SNIS-NPCSI";
            public const string CustomerKiosk = "SIS-UNSI";
            public const string Employee = "SNIS-ESI";
            public const string EmployeeCompanyInCart = "SNIS-ESI-CIC";
            public const string EmployeeKiosk = "SIS-ESI";
            public const string EmployeeManager = "EmployeeManager";
            public const string Hospitality = "SNIS-HCSI";
            public const string HospitalityKiosk = "SIS-HCSI";
            public const string Professional = "SNIS-PCSI";
        }


        public class BatchGroup
        {
            public class Common
            {
                public const string AddingToCartAndWishList = "Common-AddingToCartAndWishList";
                public const string AugmentedReality = "Common-AugmentedReality";
                public const string CartOverview = "Common-CartOverview";
                public const string Certona = "Common-Certona";
                public const string ChangeEmailPreferences = "Common-ChangeEmailPreferences";
                public const string ContactUs = "Common-ContactUs";
                public const string CreateAccount = "Common-CreateAccount";
                public const string HeaderFooter = "Common-HeaderFooter";
                public const string Homepage = "Common-Homepage";
                public const string ManageAccount = "Common-ManageAccount";
                public const string OrderConfirmation = "Common-OrderConfirmation";
                public const string OrderHistory = "Common-OrderHistory";
                public const string Payment = "Common-Payment";
                public const string Pixels = "Common-Pixels";
                public const string ProductDetail = "Common-ProductDetail";
                public const string Search = "Common-Search";
                public const string Shipping = "Common-Shipping";
                public const string Sort = "Common-Sort";
                public const string Stores = "Common-Stores";
                public const string OtherPages = "Common-OtherPages";
            }

            public class Desktop
            {
                public const string AddingToCartAndWishList = "Desktop-AddingToCartAndWishList";
                public const string AugmentedReality = "Common-AugmentedReality";
                public const string CartOverview = "Desktop-CartOverview";
                public const string Certona = "Desktop-Certona";
                public const string ChangeEmailPreferences = "Desktop-ChangeEmailPreferences";
                public const string CreateAccount = "Desktop-CreateAccount";
                public const string HeaderFooter = "Desktop-HeaderFooter";
                public const string Homepage = "Desktop-Homepage";
                public const string ManageAccount = "Desktop-ManageAccount";
                public const string OrderConfirmation = "Desktop-OrderConfirmation";
                public const string OrderHistory = "Desktop-OrderHistory";
                public const string OrderSummary = "Desktop-OrderSummary";
                public const string OtherPages = "Desktop-OtherPages";
                public const string Payment = "Desktop-Payment";
                public const string ProductDetail = "Desktop-ProductDetail";
                public const string Search = "Desktop-Search";
                public const string SecureSignin = "Desktop-SecureSignin";
                public const string Shipping = "Desktop-Shipping";
                public const string Sort = "Desktop-Sort";
                public const string Stores = "Desktop-Stores";
            }

            public class Mobile
            {
                public const string AddingToCartAndWishList = "Mobile-AddingToCartAndWishList";
                public const string AugmentedReality = "Mobile-AugmentedReality";
                public const string CartOverview = "Mobile-CartOverview";
                public const string Certona = "Mobile-Certona";
                public const string ChangeEmailPreferences = "Mobile-ChangeEmailPreferences";
                public const string ContactUs = "Mobile-ContactUs";
                public const string CreateAccount = "Mobile-CreateAccount";
                public const string HeaderFooter = "Mobile-HeaderFooter";
                public const string Homepage = "Mobile-Homepage";
                public const string ManageAccount = "Mobile-ManageAccount";
                public const string OrderConfirmation = "Mobile-OrderConfirmation";
                public const string OrderHistory = "Mobile-OrderHistory";
                public const string Payment = "Mobile-Payment";
                public const string Poc = "Mobile-ProofOfConcept";
                public const string ProductDetail = "Mobile-ProductDetail";
                public const string Search = "Mobile-Search";
                public const string Shipping = "Mobile-Shipping";
                public const string Sort = "Mobile-Sort";
                public const string Stores = "Mobile-Stores";
            }
        }

        public class RunEnvironment
        {
            public const string ProductionOnly = "Run-ProductionOnly";
            public const string TestDatabaseOnly = "Run-TestDatabaseOnly";
        }
    }
}
