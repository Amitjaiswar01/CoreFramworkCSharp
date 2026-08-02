using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Homepage.T542_T558_VerifySplashImageOnHomepage
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Homepage)]
    public class T542_Windows_VerifySplashImageOnHomepage : T542_DesktopBase
    {
        public T542_Windows_VerifySplashImageOnHomepage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void SplashImageOnHomepage(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Homepage)]
    public class T542_Windows_Pro_VerifySplashImageOnHomepage : T542_DesktopBase
    {
        public T542_Windows_Pro_VerifySplashImageOnHomepage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_PCSI)]
        public void SplashImageOnHomepage(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Homepage)]
    public class T542_Windows_Hosp_VerifySplashImageOnHomepage : T542_DesktopBase
    {
        public T542_Windows_Hosp_VerifySplashImageOnHomepage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_HCSI)]

        public void SplashImageOnHomepage(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Homepage)]
    public class T542_Windows_Sis_VerifySplashImageOnHomepage : T542_DesktopBase
    {
        public T542_Windows_Sis_VerifySplashImageOnHomepage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_ESI)]

        public void SplashImageOnHomepage(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Homepage)]
    public class T542_Mac_VerifySplashImageOnHomepage : T542_DesktopBase
    {
        public T542_Mac_VerifySplashImageOnHomepage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void SplashImageOnHomepage(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Homepage)]
    public class T542_iPad_VerifySplashImageOnHomepage : T542_DesktopBase
    {
        public T542_iPad_VerifySplashImageOnHomepage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void SplashImageOnHomepage(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Homepage)]
    public class T542_TabletEmulator_VerifySplashImageOnHomepage : T542_DesktopBase
    {
        public T542_TabletEmulator_VerifySplashImageOnHomepage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void SplashImageOnHomepage(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the Splash image on the Homepage.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7726
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T542
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7726"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T542")]
    public abstract class T542_DesktopBase : TestsBaseDesktop
    {
        protected T542_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: Navigate to Homepage : https://www.lampsplus.com/
            InitializeFunctionalTest(config, Urls.HomePageUrl);
            Browser.Wait.ForDomReady();

            /* Act: Click on Splash Image and
            Assert: Verify if Non-Hospitality User is redirected to Sale Page 
            and for Hospitality, User is redirected to Hospitality Grade Products Sort Page.
            */
            if (config.Contains("HCSI"))
            {
                Home.NavigateToHospitalityProductsPageViaSplashBanner();
                Assert.Equals(Urls.HospitalityProducts, Browser.PageUrl, $"The link for Splash image was expecting {Urls.HospitalityProducts} but found {Browser.PageUrl}.");
            }
            else
            {
                Home.NavigateToSalePageViaSplashBanner();
                Assert.Equals(Urls.LpOnSaleUrl, Browser.PageUrl, $"The link for Splash image was expecting {Urls.LpOnSaleUrl} but found {Browser.PageUrl}.");
            }
        }
    }
}