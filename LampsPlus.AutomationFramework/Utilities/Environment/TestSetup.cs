using Automation.Framework;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Enums;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.AutomationFramework.Workflow.Base;
using OperatingSystem = LampsPlus.AutomationFramework.Utilities.TestConfiguration.OperatingSystem;

namespace LampsPlus.AutomationFramework.Utilities.Environment
{
    /// <summary>
    /// Helper to configure test setup and teardown.
    /// </summary>
    public class TestSetup
    {
        /// <summary>
        /// Contains details about test setup.
        /// </summary>
        public Config TestConfiguration { get; }


        /// <summary>
        /// Contains details about test setup.
        /// </summary>
        public TestConfigurationModel TestConfigurationModelConfig { get; }

        /// <summary>
        /// Contains information about the login account used by the test.
        /// </summary>
        public Account AccountConfig { get; set; }

        /// <summary>
        /// Contains Shopping Cart specific setup and teardown configuration options.
        /// </summary>
        public ShoppingCart ShoppingCartConfig { get; }

        /// <summary>
        /// Contains WishList specific setup and teardown configuration options.
        /// </summary>
        public WishList WishListConfig { get; }

        /// <summary>
        /// Initial Url to navigate.
        /// Note: This will happen after sign in if a configuration that needs to sign in is used.
        /// </summary>
        public string InitialUrl { get; }

        /// <summary>
        /// Is the current test a Network Logging test?
        /// </summary>
        public bool IsNetworkLoggingTest { get; set; }

        public bool TearDownAccountUnderTest { get; }

        public string TestTagName => GetTestTagName();

        public int DesiredViewPortWidth { get; set; }

        public bool EmployeeManagerAccountStatus { get; set; }//TODO To pass value from TestSetup constructor to AccountSetup() method


        /// <summary>
        /// Build a TestSetup object with the given configuration and optional initial URL.
        /// </summary>
        /// <param name="config"></param>
        /// <param name="initialUrl"></param>
        /// <param name="useEmployeeManagerAccount"></param>
        /// <param name="accountUnderTest"></param>
        /// <param name="tearDownAccountUnderTest"></param>
        public TestSetup(string config, string initialUrl = "", bool useEmployeeManagerAccount = false, LampsPlusAccount accountUnderTest = null, bool tearDownAccountUnderTest = true)
        {
            TestConfiguration = new Config(config);
            TestConfigurationModelConfig = new TestConfigurationModel(config);

            AccountConfig = new Account(accountUnderTest);

            EmployeeManagerAccountStatus = useEmployeeManagerAccount;

            ShoppingCartConfig = new ShoppingCart();
            WishListConfig = new WishList();

            DesiredViewPortWidth = TestConfigurationModelConfig.DesiredViewPortWidth;

            InitialUrl = initialUrl;

            TearDownAccountUnderTest = tearDownAccountUnderTest;
        }

        /// <summary>
        /// Build a TestSetup object with the Account configuration.
        /// </summary>
        public void AccountSetup()
        {
            AccountConfig.AccountUnderTest = SignInWorkflowBase.GetDefaultLoginTypeByUserRole(TestConfiguration.UserRole, EmployeeManagerAccountStatus);//TODO
        }

        private string GetTestTagName()
        {
            var getTestTagName = $"{TestConfiguration.OperatingSystem}_{TestConfiguration.Browser}_{TestConfiguration.UserRole}";

            return getTestTagName;
        }

        /// <summary>
        /// Test configuration defined in the requirements.
        /// Device, Browser, OS, and UserRole.
        /// </summary>
        public class Config
        {
            /// <summary>
            /// String representing the configuration for a given test.
            /// </summary>
            public string ConfigString { get; }

            /// <summary>
            /// Type of device used in the test.
            /// </summary>
            public OperatingSystem OperatingSystem { get; }

            /// <summary>
            /// Browser used in the test.
            /// </summary>
            public WebBrowser Browser { get; }

            /// <summary>
            /// User role used in the test.
            /// </summary>
            public UserRole UserRole { get; }


            public EnvironmentUnderTest EnvironmentUnderTest { get; }

            /// <summary>
            /// Will the configuration use a mobile view?
            /// </summary>
            public bool IsMobileView { get; }

            /// <summary>
            /// Will the configuration use a tablet view?
            /// </summary>
            public bool IsTabletView { get; }

            public bool IsUsingEasyAsk { get; }

            public bool IsSearchRelatedTest { get; }

            /// <summary>
            /// Will the configuration use a tablet emulation view?
            /// </summary>
            public bool IsTabletEmulationView { get; }

            /// <summary>
            /// Is the configuration running as the baseline?
            /// </summary>
            public bool IsBaseLine { get; }

            /// <summary>
            /// Mobile device to use based on the Operating System configuration.
            /// </summary>
            public MobileDevice Device { get; }

            /// <summary>
            /// Should the Appium driver be used?
            /// </summary>
            public bool UseAppiumDriver => OperatingSystem == OperatingSystem.iPad ||
                                           OperatingSystem == OperatingSystem.iPhone ||
                                           OperatingSystem == OperatingSystem.Android;

            /// <summary>
            /// Initialize the Test Configuration object. This sets the Device, Browser, UserRole, and OS for the test.
            /// </summary>
            /// <param name="config">Environment configuration used by the test.</param>
            public Config(string config)
            {
                ConfigString = config;
                var configuration = new TestConfigurationModel(ConfigString);

                OperatingSystem = configuration.OperatingSystem;
                Browser = configuration.Browser;
                UserRole = configuration.UserRole; // TODO: Hook up user role specific behavior based on this.
                EnvironmentUnderTest = configuration.EnvironmentUnderTest;

                IsBaseLine = configuration.EnvironmentUnderTest == EnvironmentUnderTest.Baseline;

                IsMobileView = Browser == WebBrowser.ChromeMobileView || OperatingSystem == OperatingSystem.Android || OperatingSystem == OperatingSystem.iPhone;

                IsTabletView = OperatingSystem == OperatingSystem.iPad;

                IsTabletEmulationView = Browser == WebBrowser.ChromeTabletView;

                IsUsingEasyAsk = config.Contains("EasyAsk");

                IsSearchRelatedTest = configuration.TestConfiguration.Contains("EasyAsk") || configuration.TestConfiguration.Contains("ElasticSearch");

                if (OperatingSystem == OperatingSystem.iPad)
                {
                    Device = LampsPlusMobileDevices.iPadPro;
                }
                else if (OperatingSystem == OperatingSystem.iPhone)
                {
                    Device = LampsPlusMobileDevices.iPhone;
                }
                else if (OperatingSystem == OperatingSystem.Android)
                {
                    Device = LampsPlusMobileDevices.MotoX;
                }
            }
        }

        /// <summary>
        /// Contains Saved Shipping Address configuration properties for test setup and teardown.
        /// </summary>
        public class SavedShippingAddress : ContainerBase
        {
            /// <summary>
            /// By default remove the saved shipping address on setup and teardown.
            /// </summary>
            public SavedShippingAddress()
            {
                EmptyOnSetup = true;
                EmptyOnTearDown = true;
            }
        }

        /// <summary>
        /// Contains Saved Payment Option configuration properties for test setup and teardown.
        /// </summary>
        public class SavedPaymentOptions : ContainerBase
        {
            /// <summary>
            /// By default remove the saved payment options on setup and teardown.
            /// </summary>
            public SavedPaymentOptions()
            {
                EmptyOnSetup = true;
                EmptyOnTearDown = true;
            }
        }

        /// <summary>
        /// Contains Shopping Cart configuration properties for test setup and teardown.
        /// </summary>
        public class ShoppingCart : ContainerBase
        {
            /// <summary>
            /// By default empty the Shopping Cart on setup and teardown.
            /// </summary>
            public ShoppingCart()
            {
                EmptyOnSetup = true;
                EmptyOnTearDown = true;
            }
        }


        /// <summary>
        /// Contains WishList configuration properties for test setup and teardown.
        /// </summary>
        public class WishList : ContainerBase
        {
            /// <summary>
            /// By default empty the WishList on setup and teardown.
            /// </summary>
            public WishList()
            {
                EmptyOnSetup = true;
                EmptyOnTearDown = true;
            }
        }


        /// <summary>
        /// Common properties for "containers" aka Shopping Cart and WishList.
        /// </summary>
        public class ContainerBase
        {
            /// <summary>
            /// Empty the cart at the beginning of the test when true.
            /// </summary>
            public bool EmptyOnSetup { get; set; }

            /// <summary>
            /// Empty the cart at the end of the test when true.
            /// </summary>
            public bool EmptyOnTearDown { get; set; }
        }


        /// <summary>
        /// Information about the account used in the test.
        /// </summary>
        public class Account
        {
            private LampsPlusAccount _accountUnderTest;

            private string _storeInSessionStoreNumber;

            public LampsPlusAccount AccountUnderTest//TODO
            {
                get => _accountUnderTest;
                set => _accountUnderTest = value;
            }

            public string UserName => _accountUnderTest.UserName;
            public string Password => _accountUnderTest.Password;
            public string FirstName => _accountUnderTest.FirstName;
            public string LastName => _accountUnderTest.LastName;

            public string Discount => _accountUnderTest.Discount;

            /// <summary>
            /// Use an Employee Manager account when true.
            /// </summary>
            public bool UseEmployeeManagerAccount { get; set; }

            /// <summary>
            /// Keep the account logged in when true.
            /// </summary>
            public bool KeepMeLoggedIn { get; set; }

            /// <summary>
            /// Enter the store number to put the site in store in session mode for that store. Ex "12" for store 12.
            /// </summary>
            public string StoreInSessionStoreNumber
            {
                get => _storeInSessionStoreNumber;
                set
                {
                    _storeInSessionStoreNumber = value;
                    ClearStoreInSessionOnTearDown = true;
                }
            }

            /// <summary>
            /// Clear the store in session modifier on test startup.
            /// </summary>
            public bool ClearStoreInSessionOnSetup { get; set; }

            /// <summary>
            /// Clear the store in session modifier on test teardown.
            /// </summary>
            public bool ClearStoreInSessionOnTearDown { get; set; }

            /// <summary>
            /// Clear the saved payment options at the start of the test.
            /// </summary>
            public bool ClearSavedPaymentOptionsOnSetup { get; set; }

            /// <summary>
            /// Clear the saved shipping addresses at the start of the test.
            /// </summary>
            public bool ClearSavedShippingAddressOnSetup { get; set; }

            /// <summary>
            /// Contains information about the saved shipping addresses used by the test.
            /// </summary>
            public SavedShippingAddress SavedShippingAddressConfig { get; }

            /// <summary>
            /// Contains information about the saved payment options used by the test.
            /// </summary>
            public SavedPaymentOptions SavedPaymentOptionsConfig { get; }

            /// <summary>
            /// Populate Account properties from the provided LoginAccount.
            /// </summary>
            /// <param name="accountUnderTest">Account details.</param>
            public Account(LampsPlusAccount accountUnderTest)
            {
                _accountUnderTest = accountUnderTest;

                SavedPaymentOptionsConfig = new SavedPaymentOptions();
                SavedShippingAddressConfig = new SavedShippingAddress();
            }
        }
    }
}