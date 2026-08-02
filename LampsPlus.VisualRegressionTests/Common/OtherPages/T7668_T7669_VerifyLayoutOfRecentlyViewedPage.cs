using System.Collections.Generic;
using OpenQA.Selenium;
using xRetry;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.VisualRegressionTests.Common.OtherPages
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7668_Windows_VerifyLayoutOfRecentlyViewedPage : T7668_DesktopBase
    {
        public T7668_Windows_VerifyLayoutOfRecentlyViewedPage(ITestOutputHelper output, T7668_T7669_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfRecentlyViewedPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7668_Mac_VerifyLayoutOfRecentlyViewedPage : T7668_DesktopBase
    {
        public T7668_Mac_VerifyLayoutOfRecentlyViewedPage(ITestOutputHelper output, T7668_T7669_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfRecentlyViewedPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7668_iPad_VerifyLayoutOfRecentlyViewedPage : T7668_DesktopBase
    {
        public T7668_iPad_VerifyLayoutOfRecentlyViewedPage(ITestOutputHelper output, T7668_T7669_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfRecentlyViewedPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7668_TabletEmulator_VerifyLayoutOfRecentlyViewedPage : T7668_DesktopBase
    {
        public T7668_TabletEmulator_VerifyLayoutOfRecentlyViewedPage(ITestOutputHelper output, T7668_T7669_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfRecentlyViewedPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7669_iPhone_VerifyLayoutOfRecentlyViewedPage : T7669_MobileBase
    {
        public T7669_iPhone_VerifyLayoutOfRecentlyViewedPage(ITestOutputHelper output, T7668_T7669_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void LayoutOfRecentlyViewedPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7669_Emulator_VerifyLayoutOfRecentlyViewedPage : T7669_MobileBase
    {
        public T7669_Emulator_VerifyLayoutOfRecentlyViewedPage(ITestOutputHelper output, T7668_T7669_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfRecentlyViewedPage(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Recently Viewed page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8879
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7668
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8879"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7668")]
    public abstract class T7668_DesktopBase : T7668_T7669_Base
    {
        protected T7668_DesktopBase(ITestOutputHelper output, T7668_T7669_SharedSku_Fixture fixture) : base(output, fixture) { }
    }


    /// <summary>
    /// Verify the layout of the Recently Viewed page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8879
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7669
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8879"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7669")]
    public abstract class T7669_MobileBase : T7668_T7669_Base
    {
        protected T7669_MobileBase(ITestOutputHelper output, T7668_T7669_SharedSku_Fixture fixture) : base(output, fixture) { }
    }


    public class T7668_T7669_SharedSku_Fixture : FixtureBase
    {
        public List<string> ShortSkus { get; }

        public T7668_T7669_SharedSku_Fixture()
        {
            ShortSkus = ProductActions.GetListableInStockShortSku(4);
        }
    }

    public abstract class T7668_T7669_Base : VisualTestsBase, IClassFixture<T7668_T7669_SharedSku_Fixture>
    {
        protected readonly T7668_T7669_SharedSku_Fixture Fixture;

        protected T7668_T7669_Base(ITestOutputHelper output, T7668_T7669_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }
        
        protected void Validate(string config)
        {
            InitializeVisualTest(config);

            foreach (var sku in Fixture.ShortSkus)
            {
                Browser.NavigateToPdp(sku);
                Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()),5);
                Browser.ScrollToBottomOfPage(ProductDetail.PdAddToCartStickyId);
            }

            Browser.Wait.IsVisibleElement(By.ClassName(ProductDetail.RecentlyViewedLoadedClass));
            Browser.Wait.ForDisplayedElement(ProductDetail.RecentlyViewedViewAllButton);
            Browser.ScrollIntoView(ProductDetail.AddToWishListButton);

            ProductDetail.RecentlyViewedViewAllButton.Click();
            
            Browser.Wait.IsVisibleElement(By.CssSelector(Sort.SortItemAddToCartButtonClass.ToCssClassSelector()));

            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { Sort.RecentlyViewed }, true ,true, Sort.RecentlyViewed, 10,10);
        }
    }
}
