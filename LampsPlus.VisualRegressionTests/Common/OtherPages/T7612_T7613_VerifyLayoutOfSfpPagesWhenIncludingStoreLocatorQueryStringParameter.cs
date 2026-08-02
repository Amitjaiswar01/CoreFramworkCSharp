using System.Collections.Generic;
using OpenQA.Selenium;
using xRetry;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.VisualRegressionTests.Common.OtherPages
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7612_Windows_VerifyLayoutOfSfpPagesWhenIncludingStoreLocatorQueryStringParameter : T7612_DesktopBase
    {
        public T7612_Windows_VerifyLayoutOfSfpPagesWhenIncludingStoreLocatorQueryStringParameter(ITestOutputHelper output, T7612_T7613_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfSfpPageWithStoreLocatorStringParameter(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7612_Mac_VerifyLayoutOfSfpPagesWhenIncludingStoreLocatorQueryStringParameter : T7612_DesktopBase
    {
        public T7612_Mac_VerifyLayoutOfSfpPagesWhenIncludingStoreLocatorQueryStringParameter(ITestOutputHelper output, T7612_T7613_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfSfpPageWithStoreLocatorStringParameter(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7612_iPad_VerifyLayoutOfSfpPagesWhenIncludingStoreLocatorQueryStringParameter : T7612_DesktopBase
    {
        public T7612_iPad_VerifyLayoutOfSfpPagesWhenIncludingStoreLocatorQueryStringParameter(ITestOutputHelper output, T7612_T7613_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfSfpPageWithStoreLocatorStringParameter(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7612_TabletEmulator_VerifyLayoutOfSfpPagesWhenIncludingStoreLocatorQueryStringParameter : T7612_DesktopBase
    {
        public T7612_TabletEmulator_VerifyLayoutOfSfpPagesWhenIncludingStoreLocatorQueryStringParameter(ITestOutputHelper output, T7612_T7613_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfSfpPageWithStoreLocatorStringParameter(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7613_iPhone_VerifyLayoutOfSfpPagesWhenIncludingStoreLocatorQueryStringParameter : T7613_MobileBase
    {
        public T7613_iPhone_VerifyLayoutOfSfpPagesWhenIncludingStoreLocatorQueryStringParameter(ITestOutputHelper output, T7612_T7613_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void LayoutOfSfpPageWithStoreLocatorStringParameter(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7613_Emulator_VerifyLayoutOfSfpPagesWhenIncludingStoreLocatorQueryStringParameter : T7613_MobileBase
    {
        public T7613_Emulator_VerifyLayoutOfSfpPagesWhenIncludingStoreLocatorQueryStringParameter(ITestOutputHelper output, T7612_T7613_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfSfpPageWithStoreLocatorStringParameter(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the SFP pages when including a store locator query string parameter.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8817
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7612
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8817"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7612")]
    public abstract class T7612_DesktopBase : T7612_T7613_Base
    {
        protected T7612_DesktopBase(ITestOutputHelper output, T7612_T7613_SharedSkus_Fixture fixture) : base(output, fixture) { }
    }


    /// <summary>
    /// Verify the layout of the SFP pages when including a store locator query string parameter.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8817
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7613
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8817"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7613")]
    public abstract class T7613_MobileBase : T7612_T7613_Base
    {
        protected T7613_MobileBase(ITestOutputHelper output, T7612_T7613_SharedSkus_Fixture fixture) : base(output, fixture) { }

        protected override void Validate(string config)
        {
            InitializeVisualTest(config);

            var shortSku = Fixture.ShortSku;
            var location = Fixture.Location;
            Assert.DatabaseObject(shortSku, "ProductActions.GetAnySkuWithProductDetailPage");
            Assert.DatabaseObject(location, "ProductActions.GetStoreLocation().LocationNumber");

            var url = Urls.HomePageUrl;
            Browser.Navigate($"{url}sfp/{shortSku}/?cm_mmc=GOO-SH-_-NA-_-NA-_-{shortSku}&store={location}");

            Browser.Wait.IsVisibleElement(By.CssSelector(ProductDetail.PdHeroSpotId.ToCssIdSelector()));
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            Browser.Wait.IsVisibleElement(By.CssSelector(SortFullPageCertona.MobileStoreAddressAndHoursByClass.ToCssClassSelector()));
            Browser.Wait.ForClickableElement(SortFullPageCertona.StoreAddressAndHours);
            SortFullPageCertona.StoreAddressAndHours.Click();

            Browser.Wait.IsVisibleElement(By.CssSelector(Stores.StoreDetailsBtnClass.ToCssClassSelector()));

            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ProductDetail.CallStoreButton }, false, false, ProductDetail.CallStoreButton, 5);
        }
    }


    public class T7612_T7613_SharedSkus_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public string Location { get; }

        public T7612_T7613_SharedSkus_Fixture()
        {
            ShortSku = ProductActions.GetAnySkuWithProductDetailPage;

            Location = ProductActions.GetStoreLocation().LocationNumber;
        }
    }


    public abstract class T7612_T7613_Base : VisualTestsBase, IClassFixture<T7612_T7613_SharedSkus_Fixture>
    {
        protected readonly T7612_T7613_SharedSkus_Fixture Fixture;

        protected T7612_T7613_Base(ITestOutputHelper output, T7612_T7613_SharedSkus_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            InitializeVisualTest(config);

            var shortSku = Fixture.ShortSku;

            var location = Fixture.Location;
            Assert.DatabaseObject(location, "ProductActions.GetStoreLocation()");

            var url = Urls.HomePageUrl;
            Browser.Navigate($"{url}sfp/{shortSku}/?cm_mmc=GOO-SH-_-NA-_-NA-_-{shortSku}&store={location}");

            Browser.Wait.ForDomReady();
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ProductDetail.StockCheckWrapper });

            Browser.Wait.ForClickableElement(SortFullPageCertona.StoreAddressAndHours).Click();

            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}
