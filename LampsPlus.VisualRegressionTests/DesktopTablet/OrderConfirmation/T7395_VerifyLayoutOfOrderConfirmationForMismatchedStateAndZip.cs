using System.Collections.Generic;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Environment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;

namespace LampsPlus.VisualRegressionTests.DesktopTablet.OrderConfirmation
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7395_Windows_VerifyLayoutOfOrderConfirmationForMismatchedStateAndZip : T7395_DesktopBase
    {
        public T7395_Windows_VerifyLayoutOfOrderConfirmationForMismatchedStateAndZip(ITestOutputHelper output, T7395_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_ESI)]
        public void LayoutOfOrderConfirmationForMismatchedStateAndZip(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7395_Mac_VerifyLayoutOfOrderConfirmationForMismatchedStateAndZip : T7395_DesktopBase
    {
        public T7395_Mac_VerifyLayoutOfOrderConfirmationForMismatchedStateAndZip(ITestOutputHelper output, T7395_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SIS_ESI)]
        public void LayoutOfOrderConfirmationForMismatchedStateAndZip(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7395_iPad_VerifyLayoutOfOrderConfirmationForMismatchedStateAndZip : T7395_DesktopBase
    {
        public T7395_iPad_VerifyLayoutOfOrderConfirmationForMismatchedStateAndZip(ITestOutputHelper output, T7395_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [Theory(Skip = "Rework - ACD-9344")]
        [InlineData(TestConfiguration.iPad_Safari_SIS_ESI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SIS_ESI)]
        public void LayoutOfOrderConfirmationForMismatchedStateAndZip(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7395_TabletEmulator_VerifyLayoutOfOrderConfirmationForMismatchedStateAndZip : T7395_DesktopBase
    {
        public T7395_TabletEmulator_VerifyLayoutOfOrderConfirmationForMismatchedStateAndZip(ITestOutputHelper output, T7395_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SIS_ESI)]
        public void LayoutOfOrderConfirmationForMismatchedStateAndZip(string config) => Validate(Validate, config);
    }


    public class T7395_SharedProductSku_Fixture : FixtureBase
    {
        public string SkuBetweenTenAndTwentyDollars { get; }

        public T7395_SharedProductSku_Fixture()
        {
            SkuBetweenTenAndTwentyDollars = ProductActions.GetSkuBetweenTenAndTwentyDollars;
        }
    }


    /// <summary>
    /// Verify the layout of the Order Confirmation page when the state and ZIP code do not match.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7527
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7395
    /// </summary>
    //[Collection(LpTraits.UserRole.EmployeeKiosk)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7527"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7395")]
    public abstract class T7395_DesktopBase : VisualTestsBase, IClassFixture<T7395_SharedProductSku_Fixture>
    {
        protected readonly T7395_SharedProductSku_Fixture Fixture;

        protected T7395_DesktopBase(ITestOutputHelper output, T7395_SharedProductSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            var setup = new AccountConfiguration { StoreInSessionStoreNumber = "12" };

            InitializeVisualTest(config, accountConfiguration:setup);

            var shortSku = Fixture.SkuBetweenTenAndTwentyDollars;
            Assert.DatabaseObject(shortSku, "ProductActions.GetSkuBetweenTenAndTwentyDollars()");

            ProductDetail.NavigateToProductDetailByShortSku(shortSku);
            Browser.Wait.ForDomReady();
            
            Browser.Wait.ForElement(GlobalLocators.AddToCartButton).Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(CartOverview.PosPurchaseOptionCheckboxClass.ToCssClassSelector()));
            Browser.Wait.ForDisplayedElement(CsrBlock.CsrPanelElement);
            CartOverview.RemovePromoCode();
            CsrBlock.SelectSaleSource(Sources.CartSources.Phone);
            OrderSummaryBlock.WaitForKioskPriceToUpdate();

            CartOverview.CheckOutNowButton.Click();

            Browser.Wait.ForDomReady();
            CustomerAddressInformation.EnterShippingAddress(new Address("Shipping")
            {
                AddressLine2 = "lptest",
                ZipCode = ZipCodeList.Chatsworth,
                State = StateCodeListUnitedStates.NH
            });

            OrderSummaryBlock.ProceedToPaymentButton.Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(Payment.PlaceYourOrderButtonId.ToCssIdSelector()));

            ShoppingCartWorkflow.EmployeePlaceOrderViaPo();
            Browser.Wait.IsVisibleElement(By.CssSelector(OrderConfirmation.OrderConfirmationHeadingClass.ToCssClassSelector()));

            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { OrderConfirmation.OrderIdHeading, OrderConfirmation.EmailUTagElement }, true,true, OrderConfirmation.EmailUTagElement, maxRightOffset:5);
        }
    }
}
