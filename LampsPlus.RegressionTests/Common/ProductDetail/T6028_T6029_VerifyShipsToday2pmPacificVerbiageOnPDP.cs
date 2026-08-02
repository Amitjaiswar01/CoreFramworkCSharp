using System;
using System.Linq;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.DesktopTablet.ProductDetail;
using OpenQA.Selenium;
using xRetry;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.Common.ProductDetail
{
    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T6028_Windows_VerifyShipsToday2PmVerbiageOnPdp : T6028_DesktopBase
	{
        public T6028_Windows_VerifyShipsToday2PmVerbiageOnPdp(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
		public void VerifyShipsToday2PmVerbiageOnPdp(string config) => Validate(config);
	}


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T6028_Mac_VerifyShipsToday2PmVerbiageOnPdp : T6028_DesktopBase
    {
        public T6028_Mac_VerifyShipsToday2PmVerbiageOnPdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyShipsToday2PmVerbiageOnPdp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T6028_iPad_VerifyShipsToday2PmVerbiageOnPdp : T6028_DesktopBase
    {
        public T6028_iPad_VerifyShipsToday2PmVerbiageOnPdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyShipsToday2PmVerbiageOnPdp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T6028_TabletEmulator_VerifyShipsToday2PmVerbiageOnPdp : T6028_DesktopBase
    {
        public T6028_TabletEmulator_VerifyShipsToday2PmVerbiageOnPdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyShipsToday2PmVerbiageOnPdp(string config) => Validate(config);
    }

    //[Collection(LpTraits.BatchGroup.Mobile.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.ProductDetail)]
	public class T6029_iPhone_VerifyShipsToday2PmVerbiageOnPdp : T6029_MobileBase
	{
		public T6029_iPhone_VerifyShipsToday2PmVerbiageOnPdp(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifyShipsToday2PmVerbiageOnPdp(string config) => Validate(config);
	}


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
	public class T6029_Emulator_VerifyShipsToday2PmVerbiageOnPdp : T6029_MobileBase
	{
        public T6029_Emulator_VerifyShipsToday2PmVerbiageOnPdp(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyShipsToday2PmVerbiageOnPdp(string config) => Validate(config);
	}


    /// <summary>
	/// Verify that the 'Ships Today! (orders by 2pm Pacific)' verbiage on the PDP is correct.
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5548
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T6028
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5548"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T6028")]
	public abstract class T6028_DesktopBase : T6028_T6029_Base
	{
		protected T6028_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void CheckShipsTodayVerify()
        {
            var stockCheckText = TextActions.RegexNoTabsAndNewLines(ProductDetail.StockCheckWrapper.Text.ToLower()).Split('|')[0].Trim();
            ConditionalOrderCheck(stockCheckText);
        }

        protected override void CheckShipsTodayText()
        {
            CheckShipsTodayVerify();
        }

        protected override void CheckShipsTodayPdpText()
        {
            CheckShipsTodayVerify();
        }
    }


    /// <summary>
	/// Verify that the 'Ships Today! (orders by 2pm Pacific)' verbiage on the PDP is correct.
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5352
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T6029
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5352"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T6029")]
	public abstract class T6029_MobileBase : T6028_T6029_Base
	{
		protected T6029_MobileBase(ITestOutputHelper output) : base(output) { }

        protected override void CheckShipsTodayText()
        {
            var shipsTodayText = SortPla.ShipsTodayQLElement.Text.ToLower();
            shipsTodayText = shipsTodayText.Replace(Environment.NewLine, " ");
            ConditionalOrderCheck(shipsTodayText);
        }

        protected override void CheckShipsTodayPdpText()
        {
            var shipsTodayText = SortPla.ShipsTodayPdpElement.Text.ToLower();
            shipsTodayText = shipsTodayText.Replace(Environment.NewLine, " ");
            ConditionalOrderCheck(shipsTodayText);
        }

        protected override void Validate(string config)
        {
            InitializeFramework(config);
            var bathroomLightingSku = ProductActions.GetSingleSkuBathroomLighting;

            Assert.DatabaseObject(bathroomLightingSku, "ProductActions.GetSingleSkuBathroomLighting");

            Browser.NavigateToPdp(bathroomLightingSku);
            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

            CheckShipsTodayPdpText();
        }
    }


	public abstract class T6028_T6029_Base : ProductDetailTestsBase
    {
        protected T6028_T6029_Base(ITestOutputHelper output) : base(output) { }

        protected virtual void Validate(string config)
        {
            InitializeFramework(config);
            var bathroomLightingSku = ProductActions.GetSingleSkuBathroomLighting;

            Assert.DatabaseObject(bathroomLightingSku, "ProductActions.GetSingleSkuBathroomLighting");

            Browser.NavigateToPdp(bathroomLightingSku);
            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

            CheckShipsTodayPdpText();

            string randomSortPageUrl = Urls.SortPageUrls.OrderBy(u => Guid.NewGuid()).First();
            Browser.Navigate($"{randomSortPageUrl}?sfp={bathroomLightingSku}" );
            Browser.Wait.ForDomReady();
            Browser.SwitchFocusToIframe(SortPla.PlaFrameElement);
            CheckShipsTodayText();

            Browser.Navigate($"https://www.lampsplus.com/sfp/{bathroomLightingSku}");
            Browser.Wait.ForDomReady();
            CheckShipsTodayText();
        }

        protected abstract void CheckShipsTodayText();
        protected abstract void CheckShipsTodayPdpText();

        protected void ConditionalOrderCheck(string checkText)
        {
            //Define current time and time difference
            var start = new TimeSpan(14, 00, 00);
            var end = new TimeSpan(24, 00, 00);
            var nowOrig = DateTime.Now.TimeOfDay;

            nowOrig = nowOrig.Seconds > 45 ? new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute + 1, 00) : new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 00);

            Log.Message($"Time corrected now is: {nowOrig.ToString()}");
            var timeDifference = start - nowOrig;
            var minutesMinusOne = timeDifference.Minutes - 1;
            Log.Message($"Time difference is: {timeDifference.ToString()}");

            //Conditional verification for orders based on current time
            if (DateTimeHelper.IsTimeInBetween(start, end, nowOrig))
            {
                Assert.Equals("ships today! (orders by 2 pm pacific)", checkText, $"{RecurringDataIssue}Shipping callout is incorrect for product.");
            }
            else
            {
                if (timeDifference.Hours == 0 && timeDifference.Minutes <= 59)
                {
                    Assert.True($"ships today if ordered in the next " + $"{timeDifference.Minutes} min." == checkText || $"ships today if ordered in the next " + $"{minutesMinusOne} min." == checkText, $"{RecurringDataIssue}Shipping callout is incorrect for product.");
                }
                else
                {
                    Assert.True($"ships today if ordered in the next " + $"{timeDifference.Hours} hr. {timeDifference.Minutes} min." == checkText || $"ships today if ordered in the next " + $"{timeDifference.Hours} hr. {minutesMinusOne} min." == checkText, $"{RecurringDataIssue}Shipping callout is incorrect for product.");
                }
            }
        }
    }
}
