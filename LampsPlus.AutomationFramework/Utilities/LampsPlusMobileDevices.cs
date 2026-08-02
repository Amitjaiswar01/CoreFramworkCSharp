using Automation.Framework;

namespace LampsPlus.AutomationFramework.Utilities
{
    /// <summary>
    /// Supported Lamps Plus mobile device configurations.
    /// </summary>
    public class LampsPlusMobileDevices
    {
        private const string Chrome = "chrome";
        private const string Safari = "safari";
        private const string XcuiTest = "XCUITest";

        /// <summary>
        /// Real automation iPad Pro.
        /// </summary>
        public static MobileDevice iPadPro = new MobileDevice("ad1e6f50442933564cc6c53d8aae249515876c05", "iPad Pro", Safari)
        {
            AutomationLibrary = XcuiTest, PlatformVersion = "15.0"
        };

        /// <summary>
        /// Real automation iPhone.
        /// </summary>
        public static MobileDevice iPhone = new MobileDevice("00008020-0003654E018A002E", "iPhone", Safari)
        {
            AutomationLibrary = XcuiTest,
            PlatformVersion = "15.0"
        };

        /// <summary>
        /// Real automation Android Phone (Motorola X).
        /// </summary>
        public static MobileDevice MotoX = new MobileDevice("ZY225DXTB7", "Moto X", Chrome)
        {
            PlatformVersion = "8.1.0"
        };
    }
}
