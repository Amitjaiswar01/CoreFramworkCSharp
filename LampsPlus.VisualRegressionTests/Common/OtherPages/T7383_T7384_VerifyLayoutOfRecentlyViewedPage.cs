using OpenQA.Selenium;
using xRetry;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.VisualRegressionTests.Common.OtherPages
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7383_Windows_VerifyLayoutOfRecentlyViewedPage : T7383_DesktopBase
    {
        public T7383_Windows_VerifyLayoutOfRecentlyViewedPage(ITestOutputHelper output, T7383_T7384_SharedItem_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfRecentlyViewedPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7383_Mac_VerifyLayoutOfRecentlyViewedPage : T7383_DesktopBase
    {
        public T7383_Mac_VerifyLayoutOfRecentlyViewedPage(ITestOutputHelper output, T7383_T7384_SharedItem_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfRecentlyViewedPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7383_iPad_VerifyLayoutOfRecentlyViewedPage : T7383_DesktopBase
    {
        public T7383_iPad_VerifyLayoutOfRecentlyViewedPage(ITestOutputHelper output, T7383_T7384_SharedItem_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfRecentlyViewedPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7383_TabletEmulator_VerifyLayoutOfRecentlyViewedPage : T7383_DesktopBase
    {
        public T7383_TabletEmulator_VerifyLayoutOfRecentlyViewedPage(ITestOutputHelper output, T7383_T7384_SharedItem_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfRecentlyViewedPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7384_iPhone_VerifyLayoutOfRecentlyViewedPage : T7384_MobileBase
    {
        public T7384_iPhone_VerifyLayoutOfRecentlyViewedPage(ITestOutputHelper output, T7383_T7384_SharedItem_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth)]
        public void LayoutOfRecentlyViewedPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7384_Android_VerifyLayoutOfRecentlyViewedPage : T7384_MobileBase
    {
        public T7384_Android_VerifyLayoutOfRecentlyViewedPage(ITestOutputHelper output, T7383_T7384_SharedItem_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfRecentlyViewedPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7384_Emulator_VerifyLayoutOfRecentlyViewedPage : T7384_MobileBase
    {
        public T7384_Emulator_VerifyLayoutOfRecentlyViewedPage(ITestOutputHelper output, T7383_T7384_SharedItem_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfRecentlyViewedPage(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Recently Viewed page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7517
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7383
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7517"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7383")]
    public abstract class T7383_DesktopBase : T7383_T7384_Base
    {
        protected T7383_DesktopBase(ITestOutputHelper output, T7383_T7384_SharedItem_Fixture fixture) : base(output, fixture) { }
    }


    /// <summary>
    /// Verify the layout of the Recently Viewed page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7517
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7384
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7517"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7384")]
    public abstract class T7384_MobileBase : T7383_T7384_Base
    {
        protected T7384_MobileBase(ITestOutputHelper output, T7383_T7384_SharedItem_Fixture fixture) : base(output, fixture) { }
    }


    public class T7383_T7384_SharedItem_Fixture : FixtureBase
    {
        public string FirstShortSku { get; }
        public string SecondShortSku { get; }

        public T7383_T7384_SharedItem_Fixture()
        {
            // Make sure we have visited 2 different items to be able to see them in recently viewed page.
            do
            {
                FirstShortSku = ProductActions.GetAnySkuWithProductDetailPage;
                SecondShortSku = ProductActions.GetAnySkuWithProductDetailPage;
            }
            while (FirstShortSku == SecondShortSku);
        }
    }

    public abstract class T7383_T7384_Base : VisualTestsBase, IClassFixture<T7383_T7384_SharedItem_Fixture>
    {
        protected readonly T7383_T7384_SharedItem_Fixture Fixture;

        protected T7383_T7384_Base(ITestOutputHelper output, T7383_T7384_SharedItem_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            InitializeVisualTest(config);

            var firstSku = Fixture.FirstShortSku;
            var secondSku = Fixture.SecondShortSku;

            Assert.DatabaseObject(firstSku, "ProductActions.GetAnySkuWithProductDetailPage()");
            Assert.DatabaseObject(secondSku, "ProductActions.GetAnySkuWithProductDetailPage()");

            ProductDetail.NavigateToProductDetailByShortSku(firstSku);
            Browser.Wait.IsVisibleElement(By.Id(GlobalLocators.PdAddToCartId));

            ProductDetail.NavigateToProductDetailByShortSku(secondSku);
            Browser.Wait.IsVisibleElement(By.Id(GlobalLocators.PdAddToCartId));

            Browser.Navigate(Urls.RecentlyViewedUrl);

            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture);
        }
    }
}
