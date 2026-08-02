using Automation.Framework.Utilities;

namespace Automation.Framework
{
    /// <summary>
    /// Described the required capabilities for Appium to connect to a mobile device.
    /// </summary>
    public class MobileDevice
    {
        /// <summary>
        /// Unique identifier for the device.
        /// </summary>
        public string DeviceUuid { get; set; }

        /// <summary>
        /// Name of the device to show in Appium logs.
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// Name of the browser to use with Appium.
        /// </summary>
        public string BrowserName { get; set; }

        /// <summary>
        /// May not be required, XCUITest is an example.
        /// </summary>
        public string AutomationLibrary { get; set; }

        /// <summary>
        /// The Operating System version of the device under test.
        /// </summary>
        public string PlatformVersion { get; set; }

        public bool IsIphone => DeviceName.CaseInsensitiveContains("iPhone");
        public bool IsPad => DeviceName.CaseInsensitiveContains("iPad");
        public bool IsAndroid => DeviceName.CaseInsensitiveContains("Android");
        public bool IsIosVersion(string versionNumber) => PlatformVersion.CaseInsensitiveContains(versionNumber);

        /// <summary>
        /// Described the required capabilities for Appium to connect to a mobile device.
        /// AutomationLibrary can be optionally configured for example "XCUITest" for Apple devices.
        /// </summary>
        /// <param name="deviceUuid">Unique device identifier to find the requested device.</param>
        /// <param name="deviceName">Name used to identify a device.</param>
        /// <param name="browserName">Name of the browser requested for the mobile device.</param>
        public MobileDevice(string deviceUuid, string deviceName, string browserName)
        {
            DeviceUuid = deviceUuid;
            DeviceName = deviceName;
            BrowserName = browserName;
        }
    }
}
