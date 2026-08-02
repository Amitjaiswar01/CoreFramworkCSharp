using System.Collections.Generic;
using Automation.Framework;
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
    public class T7277_Windows_VerifyLayoutOfEmailModalAndThankYouMessage : T7277_DesktopBase
    {
        public T7277_Windows_VerifyLayoutOfEmailModalAndThankYouMessage(ITestOutputHelper output, T7277_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfEmailModalAndThankYouMessage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7277_Mac_VerifyLayoutOfEmailModalAndThankYouMessage : T7277_DesktopBase
    {
        public T7277_Mac_VerifyLayoutOfEmailModalAndThankYouMessage(ITestOutputHelper output, T7277_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfEmailModalAndThankYouMessage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7277_iPad_VerifyLayoutOfEmailModalAndThankYouMessage : T7277_DesktopBase
    {
        public T7277_iPad_VerifyLayoutOfEmailModalAndThankYouMessage(ITestOutputHelper output, T7277_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfEmailModalAndThankYouMessage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7277_TabletEmulator_VerifyLayoutOfEmailModalAndThankYouMessage : T7277_DesktopBase
    {
        public T7277_TabletEmulator_VerifyLayoutOfEmailModalAndThankYouMessage(ITestOutputHelper output, T7277_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfEmailModalAndThankYouMessage(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Email modal and success message in the modal.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7370
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7277
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7370"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7277")]
    public abstract class T7277_DesktopBase : T7277_Base
    {
        protected T7277_DesktopBase(ITestOutputHelper output, T7277_SharedProductSku_Fixture fixture) : base(output, fixture) { }
    }


      public class T7277_SharedProductSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7277_SharedProductSku_Fixture()
        {
            ShortSku = ProductActions.GetAnySkuWithProductDetailPage;
        }
    }


    public abstract class T7277_Base : VisualTestsBase, IClassFixture<T7277_SharedProductSku_Fixture>
    {
        protected readonly T7277_SharedProductSku_Fixture Fixture;

        protected T7277_Base(ITestOutputHelper output, T7277_SharedProductSku_Fixture fixture) : base(output, fixture)
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

            Browser.ScrollIntoView(GlobalLocators.AddToCartButton);

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

            var recipientEmail = $"{CurrentDateTime}@mailinator.com";
            var email = recipientEmail.Replace(" ", string.Empty).Replace(":", string.Empty);
            
            ProductDetail.EmailRecipientTextbox.SendKeys(email);
            ProductDetail.FirstNameTextbox.SendKeys("InternalUser");
            ProductDetail.LastNameTextbox.SendKeys("LPTEST");
            ProductDetail.FromEmailTextbox.SendKeys("autocsrregular@lampsplus.com");

            if (Browser.Device != null)
            {
                if (Browser.Device.IsPad)
                {
                    var xElementCoordinate = 0;
                    var yElementCoordinate = 0;
                    Browser.GetElementCoordinates(ProductDetail.SendEmailButton, ref xElementCoordinate, ref yElementCoordinate, 100);
                    Browser.ClickWithTapByCoordinates(xElementCoordinate, yElementCoordinate);
                }
            }
            else
            {
                ProductDetail.SendEmailButton.Click();
            }

            Browser.Wait.IsVisibleElement(By.CssSelector(ProductDetail.EmailRecipientListClass.ToCssClassSelector()));

            ScreenCapturer.CaptureElementAreaWithIgnoredLayouts(Browser.PageUrl, ProductDetail.EmailModalContent, new List<IElement> { ProductDetail.EmailRecipientList });
        }
    }
}
