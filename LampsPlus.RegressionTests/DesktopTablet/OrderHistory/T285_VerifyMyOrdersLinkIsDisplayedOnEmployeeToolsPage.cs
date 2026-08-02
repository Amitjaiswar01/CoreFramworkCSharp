using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.DesktopTablet.OrderHistory
{
    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.OrderHistory)]
    public class T285_Windows_VerifyMyOrdersLink : T285_DesktopBase
    {
        public T285_Windows_VerifyMyOrdersLink(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void MyOrdersLink(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.OrderHistory)]
    public class T285_Mac_VerifyMyOrdersLink : T285_DesktopBase
    {
        public T285_Mac_VerifyMyOrdersLink(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void MyOrdersLink(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.OrderHistory)]
    public class T285_iPad_VerifyMyOrdersLink : T285_DesktopBase
    {
        public T285_iPad_VerifyMyOrdersLink(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void MyOrdersLink(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.OrderHistory)]
    public class T285_TabletEmulator_VerifyMyOrdersLink : T285_DesktopBase
    {
        public T285_TabletEmulator_VerifyMyOrdersLink(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void MyOrdersLink(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the 'My Orders' link is accessible through the Employee Tools page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5345
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T285  
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5345"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T285")]
    public abstract class T285_DesktopBase : OrderHistoryTestsBase
    {
        protected T285_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            InitializeFramework(config, Urls.SecureEmployeeToolsPageUrl);

            HeaderFooter.UserNameLink.Click();

            ManageAccount.MyOrdersLink.Click();

            Assert.Displayed(ManageAccount.MyPastOrderSection, "My Past Order Section not displayed for Employee");
        }
    }
}
