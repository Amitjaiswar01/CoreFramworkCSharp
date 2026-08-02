using System.Linq;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;
using xRetry;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.DesktopTablet.ProductDetail;

namespace LampsPlus.RegressionTests.Common.ProductDetail
{
    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7825_Windows_VerifyGoodToKnowSectionAndLabelOnPDP : T7825_DesktopBase
    {
        public T7825_Windows_VerifyGoodToKnowSectionAndLabelOnPDP(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyGoodToKnowIconsAndLabel(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7825_Mac_VerifyGoodToKnowSectionAndLabelOnPDP : T7825_DesktopBase
    {
        public T7825_Mac_VerifyGoodToKnowSectionAndLabelOnPDP(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyGoodToKnowIconsAndLabel(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7825_iPad_VerifyGoodToKnowSectionAndLabelOnPDP : T7825_DesktopBase
    {
        public T7825_iPad_VerifyGoodToKnowSectionAndLabelOnPDP(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyGoodToKnowIconsAndLabel(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7825_TabletEmulator_VerifyGoodToKnowSectionAndLabelOnPDP : T7825_DesktopBase
    {
        public T7825_TabletEmulator_VerifyGoodToKnowSectionAndLabelOnPDP(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyGoodToKnowIconsAndLabel(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.ProductDetail)]
    public class T7826_iPhone_VerifyGoodToKnowSectionAndLabelOnPDP : T7826_MobileBase
    {
        public T7826_iPhone_VerifyGoodToKnowSectionAndLabelOnPDP(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifyGoodToKnowIconsAndLabel(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.ProductDetail)]
    public class T7826_AndroidPhone_VerifyGoodToKnowSectionAndLabelOnPDP : T7826_MobileBase
    {
        public T7826_AndroidPhone_VerifyGoodToKnowSectionAndLabelOnPDP(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void VerifyGoodToKnowIconsAndLabel(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7826_Emulator_VerifyGoodToKnowSectionAndLabelOnPDP : T7826_MobileBase
    {
        public T7826_Emulator_VerifyGoodToKnowSectionAndLabelOnPDP(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyGoodToKnowIconsAndLabel(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that Good to Know icon displayed on PDP.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9491
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7825
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9491"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7825")]
    public abstract class T7825_DesktopBase : T7825_T7826_Base
    {
        protected T7825_DesktopBase(ITestOutputHelper output) : base(output) { }

        public override void VerifyGoodToKnowSection(string expectedIconList, int goodToKnowIconsCount)
        {
            Browser.Wait.IsVisibleElement(By.ClassName(ProductDetail.GoodToKnowClass));

            foreach (var result in ProductDetail.GoodToKnowIcon)
            {
                var icons = TextActions.RegexNoTabsAndNewLines(result.Text.Trim());

                Assert.StringContains(expectedIconList, icons, "Good To know icons are not visible");
            }
        }
    }
    

    /// <summary>
    /// Verify that Good to Know icon displayed on PDP.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9491
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7826
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9491"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7826")]
    public abstract class T7826_MobileBase : T7825_T7826_Base
    {
        protected T7826_MobileBase(ITestOutputHelper output) : base(output) { }

        public override void VerifyGoodToKnowSection(string expectedIconList, int goodToKnowIconsCount)
        {
            Browser.ScrollToElement(GlobalLocators.AddToCartButton);

            Browser.Wait.ForClickableElement(ProductDetail.ProductDescDropDown).Click();

            Browser.Wait.IsVisibleElement(By.ClassName(ProductDetail.GoodToKnowClass));

            Assert.Displayed(ProductDetail.GoodToKnow, "The Good To Know Section Is Not Displayed");

            if (goodToKnowIconsCount == 0) return;
            foreach (var result in ProductDetail.GoodToKnowIcon)
            {
                var icons = result.Text;

                Assert.StringContains(expectedIconList, icons, "Good To know icons are not visible");
            }
        }
    }


    public abstract class T7825_T7826_Base : ProductDetailTestsBase
    {
        protected T7825_T7826_Base(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            InitializeFramework(config, Urls.HomePageUrl);

            var shortSku = ProductActions.GetSkuThatHasGoodToKnowIcons;

            Assert.DatabaseObject(shortSku, "ProductActions.GetSkuThatHasGoodToKnowIcons()");

            ProductDetail.NavigateToProductDetailByShortSku(shortSku);

            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

            int goodToKnowIconsCount = ProductDetail.GoodToKnowIcon.Count();

            var expectedIconList = (" Motion Sensor , Solar , Dark Sky ,  Dusk to Dawn , LED ");

            VerifyGoodToKnowSection(expectedIconList, goodToKnowIconsCount);
        }
        public abstract void VerifyGoodToKnowSection(string expectedIconList, int goodToKnowIconsCount);
    }
}