using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using Automation.Framework.Core;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.VisualRegressionTests.Mobile.ProductDetail
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7852_Emulator_VerifyLayoutOf2DAnd3DRoom : T7852_MobileBase
    {
        public T7852_Emulator_VerifyLayoutOf2DAnd3DRoom(ITestOutputHelper output, SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T7852. Rework - CI-3595")]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void AugmentedReality2DAnd3DView(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7852_iPhone_VerifyLayoutOf2DAnd3DRoom : T7852_MobileBase
    {
        public T7852_iPhone_VerifyLayoutOf2DAnd3DRoom(ITestOutputHelper output, SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T7852. Rework - CI-3595")]
        //[RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void AugmentedReality2DAnd3DView(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7852_AndroidPhone_VerifyLayoutOf2DAnd3DRoom : T7852_MobileBase
    {
        public T7852_AndroidPhone_VerifyLayoutOf2DAnd3DRoom(ITestOutputHelper output, SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void AugmentedReality2DAnd3DView(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the 2D and 3D Room Viewer
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9706
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7852
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9706"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7852")]
    public abstract class T7852_MobileBase : T7852_Base
    {
        protected T7852_MobileBase(ITestOutputHelper output, SharedSku_Fixture fixture) : base(output, fixture) { }
    }


    public class SharedSku_Fixture : FixtureBase
    {
        public string shortSku { get; }

        public SharedSku_Fixture()
        {
            shortSku = ProductActions.GetAugmentedReality2DAnd3DSku;
        }
    }


    public abstract class T7852_Base : VisualTestsBase, IClassFixture<SharedSku_Fixture>
    {
        protected readonly SharedSku_Fixture Fixture;
        protected T7852_Base(ITestOutputHelper output, SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            InitializeVisualTest(config);

            var sku = Fixture.shortSku;

            Browser.NavigateToPdp(sku);
            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

            /* Act: Navigate to PDP of the item from the query in the preconditions
            Tap the 'View In Your Room' link on the PDP.        
            */
            Browser.Wait.ForDomReady();
            Browser.Wait.ForDisplayedElement(ProductDetail.ShowInRoomBtn);
            Browser.ClickByJs(ProductDetail.ShowInRoomBtn);

            //Act: Tap the '3D AR Viewer' button.
            Browser.Wait.IsVisibleElement(By.XPath(ProductDetail.PdpArIframeXpath));
            Browser.Wait.ForDomReady();
            Browser.SwitchToDefaultContent();
            Browser.SwitchFocusToIframe(ProductDetail.PdpArIframe);
            ProductDetail.ArViewerBtn(0).Click();
            Browser.Wait.IsVisibleElement(By.ClassName(ProductDetail.GetStartedBtnClass));

            //Act: Capture a screenshot of the visible area.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            //Act: For iPhone only, tap the 'Get Started' button.
            Browser.Wait.ForClickableElement(ProductDetail.GetStartedBtn);
            Browser.ClickWithTapByElementCoordinates(ProductDetail.GetStartedBtn);
            Browser.Wait.ForDomReady();

            /* Act: Wait for the 3D iOS AR View Screen to load 
            Capture a screenshot of the visible area.
            */
            if (Browser.Device != null)
            {
                if (Browser.Device.IsIphone)
                {
                    ((IphoneBrowser)Browser).SwitchToNativeContext((AppiumDriver<AppiumWebElement>)Browser.Driver); //Switch to iOS Native context
                    Browser.Wait.IsVisibleElement(By.XPath("//XCUIElementTypeWebView[@name='WebView']"));
                    ScreenCapturer.CaptureScreen("Ar_View", ScreenshotType.VisualAreaCapture);
                    ((IphoneBrowser)Browser).SwitchToWebViewContext((AppiumDriver<AppiumWebElement>)Browser.Driver); //Switch back to iOS WebView context
                }
            }
        }
    }
}
