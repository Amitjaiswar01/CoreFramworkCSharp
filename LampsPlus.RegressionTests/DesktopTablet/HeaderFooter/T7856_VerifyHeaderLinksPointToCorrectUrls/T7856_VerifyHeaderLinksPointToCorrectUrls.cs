using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.HeaderFooter.T7856_VerifyHeaderLinksPointToCorrectUrls
{
    //[Collection(LpTraits.BatchGroup.Desktop.HeaderFooter)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.HeaderFooter)]
    public class T7856_Windows_VerifyHeaderLinksPointToCorrectUrls : T7856_DesktopBase
    {
        public T7856_Windows_VerifyHeaderLinksPointToCorrectUrls(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void HeaderLinksPointToCorrectUrls(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.HeaderFooter)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.HeaderFooter)]
    public class T7856_Mac_VerifyHeaderLinksPointToCorrectUrls : T7856_DesktopBase
    {
        public T7856_Mac_VerifyHeaderLinksPointToCorrectUrls(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void HeaderLinksPointToCorrectUrls(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.HeaderFooter)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.HeaderFooter)]
    public class T7856_iPad_VerifyHeaderLinksPointToCorrectUrls : T7856_DesktopBase
    {
        public T7856_iPad_VerifyHeaderLinksPointToCorrectUrls(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void HeaderLinksPointToCorrectUrls(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.HeaderFooter)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.HeaderFooter)]
    public class T7856_TabletEmulator_VerifyHeaderLinksPointToCorrectUrls : T7856_DesktopBase
    {
        public T7856_TabletEmulator_VerifyHeaderLinksPointToCorrectUrls(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void HeaderLinksPointToCorrectUrls(string config) => Validate(config);
    }


    /// <summary>
    /// Verify Collapsed Header Functionality on View in Room Page
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10132
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7856
    /// </summary>
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10132"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7856")]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    public abstract class T7856_DesktopBase : TestsBaseDesktop
    {
        protected T7856_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            /*Arrange - Navigate to any PDP*/
            InitializeFunctionalTest(config);
            var shortSku = ProductActions.GetSkuThatHasArOption;
            ProductDetail.NavigateToProductDetailByShortSku(shortSku);

            /*Act - Navigate to View In Your Page */
            ProductDetail.NavigateToArPage();

            // Assert 
            Assert.Equals(Urls.HomePageUrl, HeaderFooter.GetLpLogoLink(), "LP Logo link is not Correct");

            // Assert 
            Assert.Equals(Urls.AllChandeliersSortPageUrl, HeaderFooter.GetAllChandeliersLink(), "All Chandeliers link is not Correct");

            // Assert 
            Assert.Equals(Urls.ChandeliersDiningLivingRoomUrl, HeaderFooter.GetDiningLivingLink(), "Dining - Living Room link is not Correct");

            //Assert
            Assert.Equals(Urls.CeilingLightsFlushMountUrl, HeaderFooter.GetFlushmountLink(), "Flushmount link is not correct");

            // Assert
            Assert.Equals(Urls.TableLampsSortPageUrl, HeaderFooter.GetAllTableLampsLink(), "All Table Lamps link is not correct");

            // Assert 
            Assert.Equals(Urls.WallLampsPageUrl, HeaderFooter.GetWallLampsLink(), "Wall Lamps link link is not correct");

            // Assert
            Assert.Equals(Urls.WishListPageUrl, HeaderFooter.GetWishListLink(), "Wish List link is not correct");

            // Assert
            Assert.Equals(Urls.RoomsPageUrl, HeaderFooter.GetSavedRoomLink(), "Saved Rooms link is not correct");

            // Assert 
            Assert.Equals(Urls.CartOverviewPageUrl, HeaderFooter.GetCartIconLink(), "Cart link is not correct");
        }
    }
}