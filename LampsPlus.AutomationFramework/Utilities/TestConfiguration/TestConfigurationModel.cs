using System;
using System.ComponentModel;
using System.Linq;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Enums;

namespace LampsPlus.AutomationFramework.Utilities.TestConfiguration
{
    /// <summary>
    /// Information needed to configure a test.
    /// </summary>
    public class TestConfigurationModel
    {
        private readonly ViewPortWidthType _viewPortWidthType;

        /// <summary>
        /// Supported Lamps Plus user account types.
        /// <see cref="TestConfiguration"/>
        /// </summary>
        public UserRole UserRole { get; set; }

        /// <summary>
        /// Supported Lamps Plus Browser configuration.
        /// </summary>
        public WebBrowser Browser { get; set; }

       

        /// <summary>
        /// Supported Lamps Plus Operating Systems configuration.
        /// </summary>
        public OperatingSystem OperatingSystem { get; set; }

        public EnvironmentUnderTest EnvironmentUnderTest { get; }

        public int DesiredViewPortWidth { get; set; }

        public string TestConfiguration { get; set; }


        /// <summary>
        /// Update object properties based on test configuration string.
        /// </summary>
        /// <param name="testConfiguration"><see cref="TestConfiguration"/></param>
        public TestConfigurationModel(string testConfiguration)
        {
            TestConfiguration = testConfiguration;

            var split = testConfiguration.Split('_');
            EnvironmentUnderTest = EnvironmentUnderTest.Target;

            _viewPortWidthType = ViewPortWidthType.Primary;

            if (split[split.Length - 1] == "Baseline" && (split[split.Length - 2] == "SecondaryViewPortWidth"))
            {            
                EnvironmentUnderTest = EnvironmentUnderTest.Baseline;
                testConfiguration = testConfiguration.Replace($"_{split[split.Length - 1]}", string.Empty).Replace($"_{split[split.Length - 2]}", string.Empty);
                _viewPortWidthType = ViewPortWidthType.SecondaryViewPortWidth;
            }
            else if (split[split.Length - 1] == "Baseline")
            {
                EnvironmentUnderTest = EnvironmentUnderTest.Baseline;
                testConfiguration = testConfiguration.Replace($"_{split[split.Length - 1]}", string.Empty);
            }
            else if (split[split.Length - 1] == "SecondaryViewPortWidth")
            {
                testConfiguration = testConfiguration.Replace($"_{split[split.Length - 1]}", string.Empty);
                _viewPortWidthType = ViewPortWidthType.SecondaryViewPortWidth;
            }
            else if (split[split.Length - 2] == "SecondaryViewPortWidth")
            {
                testConfiguration = testConfiguration.Replace($"_{split[split.Length - 2]}", string.Empty);
                _viewPortWidthType = ViewPortWidthType.SecondaryViewPortWidth;
            }
            else if (split[split.Length - 1] == "EasyAsk" || split[split.Length - 1] == "ElasticSearch")
            {
                if (testConfiguration.Contains("Baseline"))
                {
                    EnvironmentUnderTest = EnvironmentUnderTest.Baseline;
                    testConfiguration = testConfiguration.Replace($"_{split[split.Length - 2]}", string.Empty).Replace($"_{split[split.Length - 2]}", string.Empty);
                }
                else
                {
                    testConfiguration = testConfiguration.Replace($"_{split[split.Length - 1]}", string.Empty);
                }
            }

            else if (split[split.Length - 1] == "EasyAsk" || split[split.Length - 1] == "ElasticSearch")
            {
                if (testConfiguration.Contains("Baseline"))
                {
                    EnvironmentUnderTest = EnvironmentUnderTest.Baseline;
                    testConfiguration = testConfiguration.Replace($"_{split[split.Length - 2]}", string.Empty).Replace($"_{split[split.Length - 2]}", string.Empty);
                }
                else
                {
                    testConfiguration = testConfiguration.Replace($"_{split[split.Length - 1]}", string.Empty);
                }
            }

            OperatingSystem = (OperatingSystem)Enum.Parse(typeof(OperatingSystem), split[0]);
            Browser = (WebBrowser)Enum.Parse(typeof(WebBrowser), split[1]);

            if (split.Contains("EasyAsk") || split.Contains("ElasticSearch"))
            {
                UserRole = (UserRole)Enum.Parse(typeof(UserRole), $"{testConfiguration.Split('_')[2]}_{testConfiguration.Split('_')[3]}");
            }
            else
            {
                UserRole = (UserRole)Enum.Parse(typeof(UserRole), testConfiguration.Replace($"{split[0]}_", string.Empty).Replace($"{split[1]}_", string.Empty));
            }

            DesiredViewPortWidth = GetDesiredViewPortWidth();
        }

        private int GetDesiredViewPortWidth()
        {
            if (OperatingSystem == OperatingSystem.Windows && Browser == WebBrowser.Chrome &&
                _viewPortWidthType == ViewPortWidthType.Primary)
            {
                return ViewPortWidth.DefaultViewPortWidthDesktop;
            }

            if (OperatingSystem == OperatingSystem.Windows && Browser == WebBrowser.Chrome &&
                _viewPortWidthType == ViewPortWidthType.SecondaryViewPortWidth)
            {
                return ViewPortWidth.SecondaryViewPortWidthDesktop;
            }

            if (OperatingSystem == OperatingSystem.Windows && Browser == WebBrowser.Chrome &&
                _viewPortWidthType != ViewPortWidthType.SecondaryViewPortWidth)
            {
                return ViewPortWidth.DefaultViewPortWidthDesktop;
            }

            if (OperatingSystem == OperatingSystem.Windows && Browser == WebBrowser.ChromeTabletView &&
                _viewPortWidthType == ViewPortWidthType.Primary)
            {
                return ViewPortWidth.DefaultViewPortWidthTablet;
            }

            if (OperatingSystem == OperatingSystem.Windows && Browser == WebBrowser.ChromeMobileView &&
                _viewPortWidthType == ViewPortWidthType.SecondaryViewPortWidth)
            {
                return ViewPortWidth.DefaultViewPortWidthChromeEmulator;
            }

            if (OperatingSystem == OperatingSystem.Windows && Browser == WebBrowser.ChromeMobileView &&
                _viewPortWidthType != ViewPortWidthType.SecondaryViewPortWidth)
            {
                return ViewPortWidth.DefaultViewPortWidthChromeEmulator;
            }

            if (OperatingSystem == OperatingSystem.iPhone && Browser == WebBrowser.Safari &&
                _viewPortWidthType == ViewPortWidthType.Primary)
            {
                return ViewPortWidth.DefaultViewPortWidthiPhone;
            }

            if (OperatingSystem == OperatingSystem.iPhone && Browser == WebBrowser.Safari &&
                _viewPortWidthType != ViewPortWidthType.Primary)
            {
                return ViewPortWidth.SecondaryViewPortWidthiPhone;
            }

            if (OperatingSystem == OperatingSystem.Android && Browser == WebBrowser.Chrome &&
                _viewPortWidthType == ViewPortWidthType.Primary)
            {
                return ViewPortWidth.DefaultViewPortWidthAndroid;
            }

            if (OperatingSystem == OperatingSystem.Mac && Browser == WebBrowser.Safari &&
                _viewPortWidthType == ViewPortWidthType.Primary)
            {
                return ViewPortWidth.DefaultViewPortWidthMac;
            }

            if (OperatingSystem == OperatingSystem.iPad && Browser == WebBrowser.Safari &&
                _viewPortWidthType == ViewPortWidthType.Primary)
            {
                return ViewPortWidth.DefaultViewPortWidthTablet;
            }

            throw new InvalidEnumArgumentException($"{nameof(ViewPortWidthType.NoPrimaryNorSecondaryViewPortWith)} cannot be 'Zero'");
        }      
    }

    /// <summary>
    /// Lamps Plus supported user roles.
    /// /// <see cref="TestConfiguration"/>
    /// </summary>
    public enum UserRole
    {
        SNIS_UNSI = 1,
        SNIS_NPCSI,
        SNIS_ESI_CIC,
        SNIS_ESI,
        SNIS_PCSI,
        SIS_UNSI,
        SIS_ESI,
        SIS_ESI_CIC,
        SNIS_HCSI,
        SIS_HCSI
    }


    /// <summary>
    /// Lamps Plus supported operating systems.
    /// </summary>
    public enum OperatingSystem
    {
        Windows = 1,
        Mac,
        iPad = 501,
        iPhone,
        Android,
        MobileSimulation
    }

    public enum ViewPortWidthType
    {
        NoPrimaryNorSecondaryViewPortWith = 0,
        Primary = 1,
        SecondaryViewPortWidth = 2,
    }
}
