using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Environment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.WarmUpTests.ESIWarmUpTest
{
    public class T7476_WarmUpElementsAndPagesRelatedToEmployeeSignedIn : T7476_DesktopBase
    {
        public T7476_WarmUpElementsAndPagesRelatedToEmployeeSignedIn(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void WarmUpTestForEsi(string config) => Validate(config);
    }


    /// <summary>
    /// Warm up elements and pages related to the Customer Signed In.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8401
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7476
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8401"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7476")]
    public abstract class T7476_DesktopBase : TestsBase
    {
        protected T7476_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            var setup = new TestSetup(config, Urls.EmployeeToolsPageUrl);
            InitializeFramework(config, setup: setup);

            Browser.Navigate(Urls.OrderHistoryPageUrl);

            Browser.Navigate(Urls.EmployeeOrderLookupPageUrl);

            EmployeeOrderLookup.FirstOrder.Click();

            ProductDetail.NavigateToProductDetailByShortSku(ProductActions.GetProductWithSkuStatus().ShortSku);
            Browser.Wait.ForClickableElement(GlobalLocators.AddToCartButton);

            GlobalLocators.AddToCartButton.Click();
            Browser.Wait.ForPage(Urls.CartOverviewPageUrl);

            CsrBlock.AddProfessionalAccountLink.Click();
            CloseLpModal();

            CartOverview.CartEditPriceElement.Click();
            CartOverview.TextPercentDiscountField.SendKeys("100");
            CartOverview.SelDiscountReasonField.Click();
            new SelectElement(CartOverview.SelDiscountReasonField.InternalElement).SelectByIndex(1);
            CartOverview.ApplyDiscountButton.Click();

            Browser.Wait.IsVisibleElement(By.Id(GlobalLocators.LpModalId));

            CloseLpModal();
         
            SignInWorkflow.SignOut();
            SignInWorkflow.SignIn(LampsPlusAccounts.CustomerServiceManagerLoginAccount);

            ProductDetail.NavigateToProductDetailByShortSku(ProductActions.GetProductWithSkuStatus().ShortSku);

            ProductDetail.MarginModalLink.Click();
        }
    }
}
