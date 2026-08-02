using System.Collections.Generic;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;

namespace LampsPlus.VisualRegressionTests.DesktopTablet.OrderConfirmation
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7396_Windows_VerifyLayoutOfOrderConfirmationLineItemDiscount : T7396_DesktopBase
    {
        public T7396_Windows_VerifyLayoutOfOrderConfirmationLineItemDiscount(ITestOutputHelper output, T7396_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_ESI)]
        public void LayoutOfOrderConfirmationLineItemDiscount(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7396_Mac_VerifyLayoutOfOrderConfirmationLineItemDiscount : T7396_DesktopBase
    {
        public T7396_Mac_VerifyLayoutOfOrderConfirmationLineItemDiscount(ITestOutputHelper output, T7396_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SIS_ESI)]
        public void LayoutOfOrderConfirmationLineItemDiscount(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7396_iPad_VerifyLayoutOfOrderConfirmationLineItemDiscount : T7396_DesktopBase
    {
        public T7396_iPad_VerifyLayoutOfOrderConfirmationLineItemDiscount(ITestOutputHelper output, T7396_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SIS_ESI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SIS_ESI)]
        public void LayoutOfOrderConfirmationLineItemDiscount(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7396_TabletEmulator_VerifyLayoutOfOrderConfirmationLineItemDiscount : T7396_DesktopBase
    {
        public T7396_TabletEmulator_VerifyLayoutOfOrderConfirmationLineItemDiscount(ITestOutputHelper output, T7396_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SIS_ESI)]
        public void LayoutOfOrderConfirmationLineItemDiscount(string config) => Validate(Validate, config);
    }


    public class T7396_SharedProductSku_Fixture : FixtureBase
    {
        public string SkuBetweenTenAndTwentyDollars { get; }

        public T7396_SharedProductSku_Fixture()
        {
            SkuBetweenTenAndTwentyDollars = ProductActions.GetSkuBetweenTenAndTwentyDollars;
        }
    }

    
    /// <summary>
    /// Verify the layout of the Order Confirmation page when a line item discount is given.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7528
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7396
    /// </summary>
    //[Collection(LpTraits.UserRole.EmployeeKiosk)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7528"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7396")]
    public abstract class T7396_DesktopBase : VisualTestsBase, IClassFixture<T7396_SharedProductSku_Fixture>
    {
        protected readonly T7396_SharedProductSku_Fixture Fixture;

        protected T7396_DesktopBase(ITestOutputHelper output, T7396_SharedProductSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }
        
        protected void Validate(string config)
        {
            InitializeVisualTest(config, useEmployeeManagerAccount: true);
            Home.EnterStoreInSession("1");

            var productBetweenTenAndTwenty = Fixture.SkuBetweenTenAndTwentyDollars;
            Assert.DatabaseObject(productBetweenTenAndTwenty, "ProductActions.GetSkuBetweenTenAndTwentyDollars");
            ShoppingCartWorkflow.AddItemToCartBySku(new ProductModel { Sku = productBetweenTenAndTwenty });

            Browser.Wait.ForDisplayedElement(CartOverview.CheckOutNowButton);
            Browser.Wait.ForDisplayedElement(CsrBlock.CsrPanelElement);

            CsrBlock.SelectSaleSource(Sources.CartSources.Phone);
            CartOverview.UncheckAllPosCheckboxes();
            ShoppingCartWorkflow.ApplyCartItemDiscount(0, 10);
            Browser.Wait.UntilElementUnloads(CartOverview.ApplyDiscountModal);

            CartOverview.CheckOutNowButton.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(Shipping.ProceedPaymentId.ToCssIdSelector()));

            SubmittingOrdersWorkflow.EmployeePlacesOrderForCurrentCartWithPurchaseOrderPaymentMethod();

            ScreenCapturer.CaptureScreenRegionWithIgnoredLayouts(Browser.PageUrl,OrderConfirmation.OrderSummaryContainer, new List<IElement> { OrderConfirmation.OrderIdHeading, OrderConfirmation.EmailUTagElement }, true, true);
        }
    }
}
