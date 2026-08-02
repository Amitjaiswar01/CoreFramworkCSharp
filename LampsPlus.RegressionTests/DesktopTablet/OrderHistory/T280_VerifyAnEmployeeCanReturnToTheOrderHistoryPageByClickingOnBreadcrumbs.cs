using Xunit;
using Xunit.Abstractions;
using OpenQA.Selenium;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.Environment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;


namespace LampsPlus.RegressionTests.DesktopTablet.OrderHistory
{
    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.OrderHistory)]
    public class T280_Windows_VerifyEmpReturnToOrderHistory : T280_DesktopBase
    {
        public T280_Windows_VerifyEmpReturnToOrderHistory(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void EmpReturnToOrderHistory(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.OrderHistory)]
    public class T280_Mac_VerifyEmpReturnToOrderHistory : T280_DesktopBase
    {
        public T280_Mac_VerifyEmpReturnToOrderHistory(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void EmpReturnToOrderHistory(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.OrderHistory)]
    public class T280_iPad_VerifyEmpReturnToOrderHistory : T280_DesktopBase
    {
        public T280_iPad_VerifyEmpReturnToOrderHistory(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void EmpReturnToOrderHistory(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.OrderHistory)]
    public class T280_TabletEmulator_VerifyEmpReturnToOrderHistory : T280_DesktopBase
    {
        public T280_TabletEmulator_VerifyEmpReturnToOrderHistory(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void EmpReturnToOrderHistory(string config) => Validate(config);
    }


    ///<Summary>
    ///Verify the user can return to the 'Order History' page by clicking on the breadcrumb
    ///JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5401
    ///Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T280
    ///</Summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5401"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T280")]
    public abstract class T280_DesktopBase : OrderHistoryTestsBase
    {
        protected T280_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            var setup = new TestSetup(config);
            InitializeFramework(config, setup: setup);

            Browser.Wait.IsVisibleElement(By.CssSelector(HeaderFooter.UserNameId.ToCssIdSelector()));
            HeaderFooter.UserNameLink.Click();

            Browser.Wait.IsVisibleElement(By.LinkText(ManageAccount.MyOrdersString));

            ManageAccount.MyOrdersLink.Click();
            Browser.Wait.ForDomReady();
            Browser.Wait.ForClickableElement(ManageAccount.RadioButton);
            ManageAccount.RadioButton.Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(ProductDetail.StoreRadioButtonSelector));

            var orderHistoryUrl = Browser.PageUrl;

            Browser.ClickByJs(ManageAccount.OrderId);
            Browser.Wait.ForClickableElement(ManageAccount.OrderHistoryBreadcrumb);
            ManageAccount.OrderHistoryBreadcrumb.Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(EmployeeOrderLookup.LbSearchId.ToCssIdSelector()));

            Assert.Equals(orderHistoryUrl, Browser.PageUrl, "Page url not same");
        }
    }
}
