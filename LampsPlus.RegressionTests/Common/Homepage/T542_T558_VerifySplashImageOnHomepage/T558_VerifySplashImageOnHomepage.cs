using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using xRetry;

namespace LampsPlus.RegressionTests.Common.Homepage.T542_T558_VerifySplashImageOnHomepage
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Homepage)]
    public class T558_iPhone_VerifySplashImageOnHomepage : T558_MobileBase
    {
        public T558_iPhone_VerifySplashImageOnHomepage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void SplashImageOnHomepage(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Homepage)]
    public class T558_AndroidPhone_VerifySplashImageOnHomepage : T558_MobileBase
    {
        public T558_AndroidPhone_VerifySplashImageOnHomepage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void SplashImageOnHomepage(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Homepage)]
    public class T558_Emulator_VerifySplashImageOnHomepage : T558_MobileBase
    {
        public T558_Emulator_VerifySplashImageOnHomepage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void SplashImageOnHomepage(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Homepage)]
    public class T558_iPhone_VerifySplashImageOnHomepagePcsi : T558_MobileBase
    {
        public T558_iPhone_VerifySplashImageOnHomepagePcsi(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_PCSI)]
        public void SplashImageOnHomepage(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Homepage)]
    public class T558_AndroidPhone_VerifySplashImageOnHomepagePcsi : T558_MobileBase
    {
        public T558_AndroidPhone_VerifySplashImageOnHomepagePcsi(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_PCSI)]
        public void SplashImageOnHomepage(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Homepage)]
    public class T558_Emulator_VerifySplashImageOnHomepagePcsi : T558_MobileBase
    {
        public T558_Emulator_VerifySplashImageOnHomepagePcsi(ITestOutputHelper output) : base(output)
        {
        }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_PCSI)]
        public void SplashImageOnHomepage(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the Splash image on the Homepage.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7726
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T558
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7726"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T558")]
    public abstract class T558_MobileBase : TestsBaseMobile
    {
        protected T558_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: User is on Homepage
            InitializeFunctionalTest(config, Urls.HomePageUrl);

            //Act: Click on Splash Image
            Home.NavigateToSalePageViaSplashBanner();
            Assert.True(Sort.IsCurrentPage,"Current page is not sort page");

            //Assert: Verify if User is redirected to Sale Page
            Assert.Equals(Urls.OnSaleUrl, Browser.PageUrl, $"The link for Splash image was expecting {Urls.OnSaleUrl} but found {Browser.PageUrl}.");
        }
    }
}