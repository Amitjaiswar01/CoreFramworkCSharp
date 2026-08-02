using System.Collections.Generic;
using OpenQA.Selenium;
using xRetry;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail

{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7274_Windows_VerifyLayoutOfShipsFreeWithOrdersOver49Callout : T7274_DesktopBase
    {
        public T7274_Windows_VerifyLayoutOfShipsFreeWithOrdersOver49Callout(ITestOutputHelper output, T7274_T7299_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfShipsFreeWithOrdersOver49Callout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7274_Mac_VerifyLayoutOfShipsFreeWithOrdersOver49Callout : T7274_DesktopBase
    {
        public T7274_Mac_VerifyLayoutOfShipsFreeWithOrdersOver49Callout(ITestOutputHelper output, T7274_T7299_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfShipsFreeWithOrdersOver49Callout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7274_iPad_VerifyLayoutOfShipsFreeWithOrdersOver49Callout : T7274_DesktopBase
    {
        public T7274_iPad_VerifyLayoutOfShipsFreeWithOrdersOver49Callout(ITestOutputHelper output, T7274_T7299_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfShipsFreeWithOrdersOver49Callout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7274_TabletEmulator_VerifyLayoutOfShipsFreeWithOrdersOver49Callout : T7274_DesktopBase
    {
        public T7274_TabletEmulator_VerifyLayoutOfShipsFreeWithOrdersOver49Callout(ITestOutputHelper output, T7274_T7299_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory] 
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfShipsFreeWithOrdersOver49Callout(string config) => Validate(Validate, config);
    }



    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7299_iPhone_VerifyLayoutOfShipsFreeWithOrdersOver49Callout : T7299_MobileBase
    {
        public T7299_iPhone_VerifyLayoutOfShipsFreeWithOrdersOver49Callout(ITestOutputHelper output, T7274_T7299_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth)]
        public void LayoutOfShipsFreeWithOrdersOver49Callout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7299_AndroidPhone_VerifyLayoutOfShipsFreeWithOrdersOver49Callout : T7299_MobileBase
    {
        public T7299_AndroidPhone_VerifyLayoutOfShipsFreeWithOrdersOver49Callout(ITestOutputHelper output, T7274_T7299_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfShipsFreeWithOrdersOver49Callout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7299_Emulator_VerifyLayoutOfShipsFreeWithOrdersOver49Callout : T7299_MobileBase
    {
        public T7299_Emulator_VerifyLayoutOfShipsFreeWithOrdersOver49Callout(ITestOutputHelper output, T7274_T7299_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)] 
        public void LayoutOfShipsFreeWithOrdersOver49Callout(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Ships Free with Orders Over $49 Callout.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7369
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7274
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7369"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7274")]
    public abstract class T7274_DesktopBase : T7274_T7299_Base
    {
        protected T7274_DesktopBase(ITestOutputHelper output, T7274_T7299_SharedSku_Fixture fixture) : base(output, fixture) { }

        protected override void TakeScreenshot()
        {
            ScreenCapturer.CaptureScreenRegionWithIgnoredLayouts(Browser.PageUrl, ProductDetail.TopContentProductDetail, new List<IElement> { ProductDetail.StockCheckWrapper });
        }
    }


    /// <summary>
    /// Verify the layout of the Ships Free with Orders Over $49 Callout.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7369
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7299
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7369"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7299")]
    public abstract class T7299_MobileBase : T7274_T7299_Base
    {
        protected T7299_MobileBase(ITestOutputHelper output, T7274_T7299_SharedSku_Fixture fixture) : base(output, fixture) { }

        protected override void TakeScreenshot()
        {
            Browser.Wait.IsVisibleElement(By.Id(GlobalLocators.PdAddToCartId));

            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ProductDetail.StockCheckWrapper, ProductDetail.StockCheckElement, ProductDetail.MoreYouMayLikeContainer }, true, true);
        }
    }


    public class T7274_T7299_SharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7274_T7299_SharedSku_Fixture()
        {
            ShortSku =  ProductActions.GetShipsFreeOnOrdersOver49CallOutShortSku;
        }
    }


    public abstract class T7274_T7299_Base : VisualTestsBase, IClassFixture<T7274_T7299_SharedSku_Fixture>
    {
        protected readonly T7274_T7299_SharedSku_Fixture Fixture;

        protected T7274_T7299_Base(ITestOutputHelper output, T7274_T7299_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            InitializeVisualTest(config);

            var sku = Fixture.ShortSku;
            Assert.DatabaseObject(sku, "ProductActions.GetShipsFreeOnOrdersOver49CallOutShortSku()");

            ProductDetail.NavigateToProductDetailByShortSku(sku);
            Browser.Wait.ForDomReady();

            TakeScreenshot();
        }
        
        protected abstract void TakeScreenshot();
    }
}
