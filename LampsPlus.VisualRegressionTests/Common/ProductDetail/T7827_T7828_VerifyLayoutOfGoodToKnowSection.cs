using System.Collections.Generic;
using Automation.Framework;
using OpenQA.Selenium;
using xRetry;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7827_Windows_VerifyLayoutOfGoodToKnowSection : T7827_DesktopBase
    {
        public T7827_Windows_VerifyLayoutOfGoodToKnowSection(ITestOutputHelper output, T7827_T7828_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyLayoutOfGoodToKnowSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7827_Mac_VerifyLayoutOfGoodToKnowSection : T7827_DesktopBase
    {
        public T7827_Mac_VerifyLayoutOfGoodToKnowSection(ITestOutputHelper output, T7827_T7828_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfGoodToKnowSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7827_iPad_VerifyLayoutOfGoodToKnowSection : T7827_DesktopBase
    {
        public T7827_iPad_VerifyLayoutOfGoodToKnowSection(ITestOutputHelper output, T7827_T7828_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfGoodToKnowSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7827_TabletEmulator_VerifyLayoutOfGoodToKnowSection : T7827_DesktopBase
    {
        public T7827_TabletEmulator_VerifyLayoutOfGoodToKnowSection(ITestOutputHelper output, T7827_T7828_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyLayoutOfGoodToKnowSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7828_iPhone_VerifyLayoutOfGoodToKnowSection : T7828_MobileBase
    {
        public T7828_iPhone_VerifyLayoutOfGoodToKnowSection(ITestOutputHelper output, T7827_T7828_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfGoodToKnowSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7828_AndroidPhone_VerifyLayoutOfGoodToKnowSection : T7828_MobileBase
    {
        public T7828_AndroidPhone_VerifyLayoutOfGoodToKnowSection(ITestOutputHelper output, T7827_T7828_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void VerifyLayoutOfGoodToKnowSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7828_Emulator_VerifyLayoutOfGoodToKnowSection : T7828_MobileBase
    {
        public T7828_Emulator_VerifyLayoutOfGoodToKnowSection(ITestOutputHelper output, T7827_T7828_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyLayoutOfGoodToKnowSection(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout for an item with Good To Know Section.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9492
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7827
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9492"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7827")]

    public abstract class T7827_DesktopBase : T7827_T7828_Base
    {
        protected T7827_DesktopBase(ITestOutputHelper output, T7827_T7828_SharedSku_Fixture fixture) : base(output, fixture) { }

        public override void VerifyGoodToKnowSection()
        {
            Browser.ScrollIntoView(ProductDetail.GoodToKnow);
            Browser.Wait.IsVisibleElement(By.ClassName(ProductDetail.GoodToKnowClass));
            Assert.Displayed(ProductDetail.GoodToKnow, "The Good To Know section is not displayed");
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement>{ProductDetail.MoreYouMayLikeContainer});
        }
    }


    /// <summary>
    /// Verify the layout for an item with the Good To Know Section.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9492
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7828
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9492"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7828")]
    public abstract class T7828_MobileBase : T7827_T7828_Base
    {
        protected T7828_MobileBase(ITestOutputHelper output, T7827_T7828_SharedSku_Fixture fixture) : base(output, fixture) { }

        public override void VerifyGoodToKnowSection()
        {
            Browser.ScrollIntoView(ProductDetail.ProductDescDropDown);
            Browser.ExecuteJs("window.scrollBy(0,-100)");
            ProductDetail.ProductDescDropDown.Click();
            Browser.Wait.ForDisplayedElement(ProductDetail.GoodToKnow);
            Assert.Displayed(ProductDetail.GoodToKnow, "The Good To Know section is not displayed");
            ProductDetail.ForceHideStickyHeader();
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, ProductDetail.ProductGoodToKnowSection);
        }
    }


    public class T7827_T7828_SharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7827_T7828_SharedSku_Fixture()
        {
            ShortSku = ProductActions.GetSkuThatHasGoodToKnowIcons;
        }
    }


    public abstract class T7827_T7828_Base : VisualTestsBase, IClassFixture<T7827_T7828_SharedSku_Fixture>
    {
        protected readonly T7827_T7828_SharedSku_Fixture Fixture;

        protected T7827_T7828_Base(ITestOutputHelper output, T7827_T7828_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            InitializeVisualTest(config);
            var sku = Fixture.ShortSku;
            Assert.DatabaseObject(sku, "ProductActions.GetSkuThatHasGoodToKnowIcons()");
            ProductDetail.NavigateToProductDetailByShortSku(sku);
            Browser.Wait.IsVisibleElement(By.XPath(GlobalLocators.PdAddToCartXpath));

            VerifyGoodToKnowSection();
        }

        public abstract void VerifyGoodToKnowSection();
    }
}
    
