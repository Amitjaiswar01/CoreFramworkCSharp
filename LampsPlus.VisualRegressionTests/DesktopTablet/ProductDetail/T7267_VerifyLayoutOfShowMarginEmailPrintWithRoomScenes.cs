using Automation.Framework.Core;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;

namespace LampsPlus.VisualRegressionTests.DesktopTablet.ProductDetail
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7267_Windows_VerifyLayoutOfShowMarginEmailPrintWithRoomScenes : T7267_DesktopBase
    {
        public T7267_Windows_VerifyLayoutOfShowMarginEmailPrintWithRoomScenes(ITestOutputHelper output, SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfShowMarginEmailPrintWithRoomScenes(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7267_Mac_VerifyLayoutOfShowMarginEmailPrintWithRoomScenes : T7267_DesktopBase
    {
        public T7267_Mac_VerifyLayoutOfShowMarginEmailPrintWithRoomScenes(ITestOutputHelper output, SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfShowMarginEmailPrintWithRoomScenes(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7267_iPad_VerifyLayoutOfShowMarginEmailPrintWithRoomScenes : T7267_DesktopBase
    {
        public T7267_iPad_VerifyLayoutOfShowMarginEmailPrintWithRoomScenes(ITestOutputHelper output, SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfShowMarginEmailPrintWithRoomScenes(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7267_TabletEmulator_VerifyLayoutOfShowMarginEmailPrintWithRoomScenes : T7267_DesktopBase
    {
        public T7267_TabletEmulator_VerifyLayoutOfShowMarginEmailPrintWithRoomScenes(ITestOutputHelper output, SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfShowMarginEmailPrintWithRoomScenes(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Show Margin link, Email Icon, Print Icon, Print Kiosk Style Product on Print modal and Print Kiosk Style with Room Scenes on Print modal.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7361
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7267
    /// </summary>
    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7361"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7267")]
    public abstract class T7267_DesktopBase : T7267_Base
    {
        protected T7267_DesktopBase(ITestOutputHelper output, SharedSku_Fixture fixture) : base(output, fixture) { }
    }


    public class SharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public SharedSku_Fixture()
        {
            ShortSku = ProductActions.GetAnySkuWithProductDetailPage;
        }
    }


    public abstract class T7267_Base : VisualTestsBase, IClassFixture<SharedSku_Fixture>
    {
        protected readonly SharedSku_Fixture Fixture;

        protected T7267_Base(ITestOutputHelper output, SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            InitializeVisualTest(config);

            var sku = Fixture.ShortSku;

            Assert.DatabaseObject(sku, "ProductActions.GetAnySkuWithProductDetailPage()");

            ProductDetail.NavigateToProductDetailByShortSku(sku);

            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

            // Email icon
            if (Browser.Device != null)
            {
                if (Browser.Device.IsPad)
                {
                    ((IpadBrowser)Browser).SwitchToNativeContext((AppiumDriver<AppiumWebElement>)Browser.Driver); //Switch to iOS Native context
                    Browser.Locate.ElementByXpath("//XCUIElementTypeOther[@name='email']").Click();
                    ((IpadBrowser)Browser).SwitchToWebViewContext((AppiumDriver<AppiumWebElement>)Browser.Driver); //Switch back to iOS WebView context
                }
            }
            else
            {
                ProductDetail.EmailLink.Click();
            }

            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.LpModalId.ToCssIdSelector()));
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, ProductDetail.EmailModalContent);
        }

    }
}
