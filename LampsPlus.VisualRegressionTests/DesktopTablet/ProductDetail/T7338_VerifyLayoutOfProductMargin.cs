using System.Collections.Generic;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;

namespace LampsPlus.VisualRegressionTests.DesktopTablet.ProductDetail
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7338_Windows_VerifyLayoutOfProductMargin : T7338_DesktopBase
    {
        public T7338_Windows_VerifyLayoutOfProductMargin(ITestOutputHelper output, T7338_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory] 
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI_Baseline)] 
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void LayoutOfProductMargin(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7338_Mac_VerifyLayoutOfProductMargin : T7338_DesktopBase
    {
        public T7338_Mac_VerifyLayoutOfProductMargin(ITestOutputHelper output, T7338_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void LayoutOfProductMargin(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7338_iPad_VerifyLayoutOfProductMargin : T7338_DesktopBase
    {
        public T7338_iPad_VerifyLayoutOfProductMargin(ITestOutputHelper output, T7338_SharedSku_Fixture fixture) : base(output, fixture) { }


        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void LayoutOfProductMargin(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7338_TabletEmulator_VerifyLayoutOfProductMargin : T7338_DesktopBase
    {
        public T7338_TabletEmulator_VerifyLayoutOfProductMargin(ITestOutputHelper output, T7338_SharedSku_Fixture fixture) : base(output, fixture) { }
        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void LayoutOfProductMargin(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Product Margin modal.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7451
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7338
    /// </summary>
    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7390"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7338")]
    public abstract class T7338_DesktopBase : T7338_Base
    {
        protected T7338_DesktopBase(ITestOutputHelper output, T7338_SharedSku_Fixture fixture) : base(output, fixture) { }
    }


    public class T7338_SharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7338_SharedSku_Fixture()
        {
            ShortSku = ProductActions.GetRandomComboKitSku;
        }
    }

    
    public abstract class T7338_Base : VisualTestsBase, IClassFixture<T7338_SharedSku_Fixture>
    {
        protected readonly T7338_SharedSku_Fixture Fixture;

        protected T7338_Base(ITestOutputHelper output, T7338_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            InitializeVisualTest(config);
            var sku = Fixture.ShortSku;
            Assert.DatabaseObject(sku, "ProductActions.GetRandomComboKitSku()");

            ProductDetail.NavigateToProductDetailByShortSku(sku);
            ShoppingCartWorkflow.AddItemToCartBySku(new ProductModel { Sku = sku });
            Browser.Wait.ForDomReady();
            //Capture 1
            Browser.ScrollToTopOfWindow();
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { CartOverview.CartTitle, CartOverview.CartIdContainer }, true, true);

            Browser.ScrollIntoView(CartOverview.CartEditPriceElement);
            Browser.ExecuteJs("window.scrollBy(0,-800)");
            CartOverview.CartEditPriceElement.Click();
            Browser.Wait.ForDisplayedElement(CartOverview.ShowUpTooltip);
            Browser.Wait.ForElementToStopAnimating(CartOverview.ShowUpTooltip);
            //Capture 2
            ScreenCapturer.CaptureElementArea($"{Browser.PageUrl}-EditPrice", CartOverview.ShowUpTooltip);

            var cachedElem = CartOverview.ProductTotalCostLabel(0);
            CartOverview.ApplyDiscount("5","Sale price", OperatingSystem);
            Browser.Wait.UntilElementUnloads(cachedElem);
            //Capture 3
            Browser.ScrollToTopOfWindow();
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { CartOverview.CartTitle, CartOverview.CartIdContainer }, true, true);

            var secondCachedElem = CartOverview.ProductTotalCostLabel(0);
            Browser.ScrollToElement(CartOverview.CartEditPriceElement);
            CartOverview.CartEditPriceElement.Click();
            CartOverview.DiscountTooltipRemoveButton.Click();
            Browser.Wait.UntilElementUnloads(secondCachedElem);

            //Capture 4
            Browser.ScrollToTopOfWindow();
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { CartOverview.CartTitle, CartOverview.CartIdContainer }, true, true);

            CartOverview.CartEditPriceElement.Click();

            CartOverview.ApplyDiscount("100", "Sale price", OperatingSystem);
            Browser.Wait.IsVisibleElement(By.Id(GlobalLocators.LpModalId));
            //Capture 5
            ScreenCapturer.CaptureElementArea($"{Browser.PageUrl}-Authorization", GlobalLocators.Iframe);
           
            CartOverview.AuthorizationModalUsernameInput.SendKeys(LampsPlusAccounts.CustomerServiceRegularLoginAccount.UserName);
            CartOverview.AuthorizationModalPasswordInput.SendKeys(LampsPlusAccounts.CustomerServiceRegularLoginAccount.Password);
            CartOverview.ModalSubmitButton.Click();
            Browser.Wait.ForCondition(() => CartOverview.AuthorizationModalErrorText.Text.Trim() != string.Empty,30);

            Browser.Wait.ForDisplayedElement(GlobalLocators.Iframe);
            Browser.ScrollToTopOfWindow(); 
            Browser.SwitchFocusToIframe(GlobalLocators.Iframe);
            //Capture 6
            ScreenCapturer.CaptureScreenRegionWithIgnoredLayouts($"{Browser.PageUrl}-Authorization", GlobalLocators.Iframe, new List<IElement> { CartOverview.AuthorizationModalUsernameInput });
        }
    }
}
