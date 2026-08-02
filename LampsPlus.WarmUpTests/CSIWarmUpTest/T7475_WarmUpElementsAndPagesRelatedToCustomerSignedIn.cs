using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Environment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.WarmUpTests.CSIWarmUpTest
{
    public class T7475_WarmUpElementsAndPagesRelatedToCustomerSignedIn : T7475_DesktopBase
    {
        public T7475_WarmUpElementsAndPagesRelatedToCustomerSignedIn(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void WarmUpTestForCsi(string config) => Validate(config);
    }


    /// <summary>
    /// Warm up elements and pages related to the Customer Signed In.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8400
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7475
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8400"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7475")]
    public abstract class T7475_DesktopBase : TestsBase
    {
        protected T7475_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            var shippingAddress = new Address { AddressLine2 = "lptest", Phone = "1234567890" };

            var setup = new TestSetup(config, Urls.ManageAccountPageUrl);
            InitializeFramework(config, setup: setup);

            ManageAccount.ChangeEmailPreferencesLink.Click();
            CloseLpModal();

            ManageAccount.ChangePasswordLink.Click();

            Browser.Navigate(Urls.ManagePaymentOptionsPageUrl);
            ManageAccountWorkflow.AddNewDefaultPaymentMethod();

            Browser.Navigate(Urls.ManageShippingAddressPageUrl);
            ManageAccount.BtnAddShippingAddress.Click();
            ManageAccountWorkflow.AddShippingAddressFromModal(shippingAddress);

            Browser.Navigate(Urls.EmailSubscribeChangeEmailPreferencesUrl);

            var shortSku = ProductActions.GetAnySkuWithProductDetailPage;
            Assert.DatabaseObject(shortSku, "ProductActions.GetAnySkuWithProductDetailPage");
            ShoppingCartWorkflow.CheckoutWithSingleItem(shortSku);

            Browser.Wait.IsVisibleElement(By.Id(Shipping.ProceedPaymentId));

            CustomerAddressInformation.ShipToDifferentAddressButton.Click();

            Browser.Wait.IsVisibleElement(By.ClassName(Shipping.AddNewAddrClass));

            CustomerAddressInformation.AddNewAddressButton.Click();
            Browser.Wait.ForElementToStopAnimating(CustomerAddressInformation.ShippingInformationModal);
            CloseLpModal();

            ShoppingCartWorkflow.ProceedToPayment();
            Browser.Wait.IsVisibleElement(By.CssSelector(OrderDetails.DetailsClass.ToCssClassSelector()));
            Payment.DetailsLink.Click();
        }
    }
}
