using Automation.Framework.Enums;


namespace Automation.Framework.Utilities
{
    /// <summary>
    /// Configure environment specific settings for the test environment.
    /// Examples are the database, Selenium Grid Information, or running tests locally.
    /// </summary>
    public class SessionSettings
    {
        /// <summary>
        /// Browser to use.
        /// </summary>
        public WebBrowser Browser { get; set; }

        /// <summary>
        /// IP Address of the Selenium Grid Hub to connect to.
        /// </summary>
        public string HubIpAddress { get; set; }

        /// <summary>
        /// /Port of the Selenium Grid Hub to connect to.
        /// </summary>
        public string HubPort { get; set; }

        /// <summary>
        /// Run tests local or on the Selenium Grid.
        /// true = local run, false = remote run.
        /// </summary>
        public bool IsLocalEnvironment { get; set; }

		/// <summary>
		/// Is the test a mobile test.
		/// </summary>
		public bool IsMobileView { get; set; }

        /// <summary>
        /// Is the test a tablet test.
        /// </summary>
        public bool IsTabletView { get; set; }

        /// <summary>
        /// Is the test a chrome emulation tablet  test.
        /// </summary>
        public bool IsTabletEmulationView { get; set; }

        /// <summary>
        /// Is the test config runs as a baseline
        /// </summary>
        public bool IsBaseLine { get; set; }

        /// <summary>
        /// If the Appium driver is used a MobileDevice configuration is required.
        /// </summary>
        public MobileDevice MobileDevice { get; set; }

        public string ProxyAddress { get; set; }

        public bool IsVisualTest { get; set; }

        public string BaselineInstance { get; set; }

        public string TargetInstance { get; set; }

        public string SettingsTestName { get; set; }

    }
}
