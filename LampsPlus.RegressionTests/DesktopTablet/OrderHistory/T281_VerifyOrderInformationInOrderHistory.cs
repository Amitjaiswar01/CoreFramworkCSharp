using System.Linq;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.Environment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.DesktopTablet.OrderHistory
{
    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.OrderHistory)]
    public class T281_Windows_VerifyOrderInformationInOrderHistory : T281_DesktopBase
    {
        public T281_Windows_VerifyOrderInformationInOrderHistory(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void OrderInformationInOrderHistory(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.OrderHistory)]
    public class T281_Mac_VerifyOrderInformationInOrderHistory : T281_DesktopBase
    {
        public T281_Mac_VerifyOrderInformationInOrderHistory(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void OrderInformationInOrderHistory(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.OrderHistory)]
    public class T281_iPad_VerifyOrderInformationInOrderHistory : T281_DesktopBase
    {
        public T281_iPad_VerifyOrderInformationInOrderHistory(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void OrderInformationInOrderHistory(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.OrderHistory)]
    public class T281_TabletEmulator_VerifyOrderInformationInOrderHistory : T281_DesktopBase
    {
        public T281_TabletEmulator_VerifyOrderInformationInOrderHistory(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void OrderInformationInOrderHistory(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the information for an Order in the Order History.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5419
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T281
    /// </summary>
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5419"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T281")]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    public abstract class T281_DesktopBase : OrderHistoryTestsBase
    {
        protected T281_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            var setup = new TestSetup(config, Urls.OrderHistoryPageUrl);
            InitializeFramework(config, setup: setup);
            var dbOrderDetails = OrderActions.GetOrderDetailsForOrderHistory();
            var firstOrder = dbOrderDetails?.First();

            Assert.DatabaseObject(dbOrderDetails, "GetOrderDetailsForOrderHistory()");

            // ReSharper disable once PossibleNullReferenceException
            Log.Message($"ID: {firstOrder.OrderId}. Email: {firstOrder.EmailAddress}");

            SearchForOrder(firstOrder);
            
            VerifyOrderIdOnPageIsCorrect(firstOrder);
            VerifyOrderDateOnPageIsCorrect(firstOrder);
            VerifyLineItemsOnPageAreCorrect(dbOrderDetails);
            VerifyBillingInfoOnPageIsCorrect(firstOrder);
            VerifyShippingInfoOnPageIsCorrect(firstOrder);
            VerifySalesAssociateNumberOnPageIsCorrect(firstOrder);
            VerifySummaryTotalsOnPageAreCorrect(firstOrder);
        }
    }
}