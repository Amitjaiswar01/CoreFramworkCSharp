using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.HeaderFooter.T274_VerifySignedInUserDoesNotSeeEmailBox
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.HeaderFooter)]
    public class T274_Windows_VerifyEmailSubBoxNotInFooterTest : T274_DesktopBase
    {
        public T274_Windows_VerifyEmailSubBoxNotInFooterTest(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void EmailSubBoxNotVisibleInFooter(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.HeaderFooter)]
    public class T274_Mac_VerifyEmailSubBoxNotInFooterTest : T274_DesktopBase
    {
        public T274_Mac_VerifyEmailSubBoxNotInFooterTest(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI)]
        public void EmailSubBoxNotVisibleInFooter(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.HeaderFooter)]
    public class T274_iPad_VerifyEmailSubBoxNotInFooterTest : T274_DesktopBase
    {
        public T274_iPad_VerifyEmailSubBoxNotInFooterTest(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI)]
        public void EmailSubBoxNotVisibleInFooter(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.HeaderFooter)]
    public class T274_TabletEmulator_VerifyEmailSubBoxNotInFooterTest : T274_DesktopBase
    {
        public T274_TabletEmulator_VerifyEmailSubBoxNotInFooterTest(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI)]
        public void EmailSubBoxNotVisibleInFooter(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that if a user is signed in they will NOT have an Email subscribe text box in footer.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9946
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T274
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop), Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9946"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T274")]
    public abstract class T274_DesktopBase : TestsBaseDesktop
    {
        protected T274_DesktopBase(ITestOutputHelper output) : base(output) { }

        public void Validate(string config)
        {
            //Arrange: User has navigated to the home page.
            InitializeFunctionalTest(config);
            Browser.Navigate(Urls.HomePageUrl);
            Assert.True(Home.IsCurrentPage, "User is not on the hope page.");

            //Act and Assert that the 'Sign Up for Lamps Plus Coupons, Offers and Sale Alerts' text field and the subscribe button are NOT available.
            Assert.False(HeaderFooter.IsSignUpForCouponsOffersAndSaleAlertsLabelVisible(), "Email Subscribe field is visible.");
            Assert.False(HeaderFooter.IsSignUpForCouponsOffersAndSaleAlertsMessageVisible(), "Email Subscribe message is visible.");
            Assert.False(HeaderFooter.IsEmailSubscribeButtonVisible(), "Email Subscribe button is visible.");
        }
    }
}
