using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.HeaderFooter.T7232_T7321_VerifyLayoutOfHeaderMenus
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7321_iPhone_VerifyLayoutOfHeaderMenusOnHomepage : T7321_MobileBase
    {
        public T7321_iPhone_VerifyLayoutOfHeaderMenusOnHomepage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth)]
        public void LayoutOfHeaderMenusOnHomepage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    //[Collection(LpTraits.UserRole.Customer)]
    public class T7321_iPhone_Customer_VerifyLayoutOfHeaderMenusOnHomepage : T7321_MobileBase
    {
        public T7321_iPhone_Customer_VerifyLayoutOfHeaderMenusOnHomepage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI_SecondaryViewPortWidth)]
        public void LayoutOfHeaderMenusOnHomepage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7321_Android_VerifyLayoutOfHeaderMenusOnHomepage : T7321_MobileBase
    {
        public T7321_Android_VerifyLayoutOfHeaderMenusOnHomepage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfHeaderMenusOnHomepage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7321_Android_Customer_VerifyLayoutOfHeaderMenusOnHomepage : T7321_MobileBase
    {
        public T7321_Android_Customer_VerifyLayoutOfHeaderMenusOnHomepage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_NPCSI)]
        public void LayoutOfHeaderMenusOnHomepage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7321_Emulator_VerifyLayoutOfHeaderMenusOnHomepage : T7321_MobileBase
    {
        public T7321_Emulator_VerifyLayoutOfHeaderMenusOnHomepage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfHeaderMenusOnHomepage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7321_Emulator_Customer_VerifyLayoutOfHeaderMenusOnHomepage : T7321_MobileBase
    {
        public T7321_Emulator_Customer_VerifyLayoutOfHeaderMenusOnHomepage(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void LayoutOfHeaderMenusOnHomepage(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Header menus on the Homepage appear correctly when they are open.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9800
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7321
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9800"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7321")]
    public abstract class T7321_MobileBase : VisualTestsBaseMobile, IClassFixture<FixtureBase>
    {
        protected T7321_MobileBase(ITestOutputHelper output, FixtureBase fixture) : base(output, fixture) { }

        protected void Validate(string config)
        {
            //Arrange: User on the Lamps Plus homepage.
            InitializeVisualTest(config);
            Browser.Navigate(Urls.HomePageUrl);
            Assert.True(Home.IsCurrentPage, "User is not on the Home page.");

            //Act: Tap on the Hamburger menu.
            HeaderFooter.ToggleHamburgerMenu();

            //Act: Capture a screenshot of the menu overlay.
            ScreenCapturer.CaptureScrollableOverlay(Browser.PageUrl, HeaderFooter.GetHamburgerMenu(), true);
        }
    }

}
