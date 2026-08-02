using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using OpenQA.Selenium;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Databases.Entities;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.DesktopTablet.OrderHistory;
using xRetry;
using Skip = Xunit.Skip;

namespace LampsPlus.RegressionTests.Common.OrderHistory
{
    //[Collection(LpTraits.BatchGroup.Common.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderHistory)]
    public class T283_Windows_VerifyOrderStatusIsCorrect : T283_DesktopBase
    {
        public T283_Windows_VerifyOrderStatusIsCorrect(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyOrderStatusIsCorrect(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderHistory)]
    public class T283_Mac_VerifyOrderStatusIsCorrect : T283_DesktopBase
    {
        public T283_Mac_VerifyOrderStatusIsCorrect(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyOrderStatusIsCorrect(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderHistory)]
    public class T283_iPad_VerifyOrderStatusIsCorrect : T283_DesktopBase
    {
        public T283_iPad_VerifyOrderStatusIsCorrect(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyOrderStatusIsCorrect(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderHistory)]
    public class T283_TabletEmulator_VerifyOrderStatusIsCorrect : T283_DesktopBase
    {
        public T283_TabletEmulator_VerifyOrderStatusIsCorrect(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyOrderStatusIsCorrect(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.OrderHistory)]
    public class T488_iPhone_VerifyOrderStatusIsCorrect : T488_MobileBase
    {
        public T488_iPhone_VerifyOrderStatusIsCorrect(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifyOrderStatusIsCorrect(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderHistory)]
    public class T488_Emulator_VerifyOrderStatusIsCorrect : T488_MobileBase
    {
        public T488_Emulator_VerifyOrderStatusIsCorrect(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyOrderStatusIsCorrect(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the Order Status shows the correct status for an order.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5204
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T283
    /// </summary>
    //[Collection(LpTraits.BatchGroup.Common.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5204"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T283")]
    public abstract class T283_DesktopBase : T283_T488_Base
    {
        protected T283_DesktopBase(ITestOutputHelper output) : base(output) { }
    }


    /// <summary>
    /// Verify the Order Status shows the correct status for an order.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5175
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T488
    /// </summary>
    //[Collection(LpTraits.BatchGroup.Common.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5175"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T488")]
    public abstract class T488_MobileBase : T283_T488_Base
    {
        protected T488_MobileBase(ITestOutputHelper output) : base(output) { }
    }


    public abstract class T283_T488_Base : OrderHistoryTestsBase
    {
        /// <inherit />
        protected T283_T488_Base(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            InitializeFramework(config);

            Skip.IfNot(DevEnvInformation.DatabaseSymbol == "P", "This test can only be executed against DBCLUST.");

            var orderStatus = OrderActions.GetOrderForEachStatus();

            Assert.DatabaseObject(orderStatus, "OrderActions.GetOrderForEachStatus()");

            LoopThroughAndVerifyOrders(orderStatus);
        }

        private void LoopThroughAndVerifyOrders(IEnumerable<OrderModel> orderStatus)
        {
            Browser.Wait.ForDomReady();
            foreach (var order in orderStatus)
            {
                Browser.Navigate(Urls.OrderHistoryPageUrl);
                Browser.Wait.IsVisibleElement(By.CssSelector(OrderHistory.CheckStatusButtonId.ToCssIdSelector()));

                OrderHistory.OrderIdField.SendKeys(order.OrderId);
                OrderHistory.EmailField.SendKeys(order.EmailAddress);
                OrderHistory.CheckStatusBtn.Click();
                                          
                Browser.Wait.IsVisibleElement(By.CssSelector(OrderDetails.ProductColClass.ToCssClassSelector()));
                             
                VerifyLineItemStatusIsCorrect(order.OrderStatus, OrderDetails.GetProductStatus(OrderDetails.OrderDetailTableFirstRow[0]).Text.TrimStart(), order.OrderId);
            }
        }
    }
}
