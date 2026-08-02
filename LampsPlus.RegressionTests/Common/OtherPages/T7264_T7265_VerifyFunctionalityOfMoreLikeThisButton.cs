using Automation.Framework.Utilities;
using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.Common.Sort;
using OpenQA.Selenium;
using xRetry;

namespace LampsPlus.RegressionTests.Common.OtherPages

{
    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7264_Windows_VerifyMoreLikeThisButton : T7264_DesktopBase
    {
        public T7264_Windows_VerifyMoreLikeThisButton(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyMoreLikeThisButton(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7264_Windows_Employee_VerifyMoreLikeThisButton : T7264_DesktopBase
    {
        public T7264_Windows_Employee_VerifyMoreLikeThisButton(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void VerifyMoreLikeThisButton(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7264_Windows_Kiosk_VerifyMoreLikeThisButton : T7264_DesktopBase
    {
        public T7264_Windows_Kiosk_VerifyMoreLikeThisButton(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_UNSI)]
        public void VerifyMoreLikeThisButton(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderHistory)]
    public class T7264_Mac_VerifyMoreLikeThisButton : T7264_DesktopBase
    {
        public T7264_Mac_VerifyMoreLikeThisButton(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyMoreLikeThisButton(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderHistory)]
    public class T7264_iPad_VerifyMoreLikeThisButton : T7264_DesktopBase
    {
        public T7264_iPad_VerifyMoreLikeThisButton(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyMoreLikeThisButton(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.OrderHistory)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderHistory)]
    public class T7264_TabletEmulator_VerifyMoreLikeThisButton : T7264_DesktopBase
    {
        public T7264_TabletEmulator_VerifyMoreLikeThisButton(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyMoreLikeThisButton(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Mobile.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Sort)]
    public class T7265_iPhone_VerifyMoreLikeThisButton : T7265_MobileBase
    {
        public T7265_iPhone_VerifyMoreLikeThisButton(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifyMoreLikeThisButton(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T7265_Emulator_VerifyMoreLikeThisButton : T7265_MobileBase
    {
        public T7265_Emulator_VerifyMoreLikeThisButton(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyMoreLikeThisButton(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the functionality of the 'More Like This' button.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7355
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7264
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7355"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7264")]
    public abstract class T7264_DesktopBase : T7264_T7265_Base
    {
        protected T7264_DesktopBase(ITestOutputHelper output) : base(output) { }
    }


    /// <summary>
    /// Verify the functionality of the 'More Like This' button.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7356
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7265
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7356"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7265")]
    public abstract class T7265_MobileBase : T7264_T7265_Base
    {
        protected T7265_MobileBase(ITestOutputHelper output) : base(output) { }

        protected override void Validate(string config)
        {
            InitializeFramework(config, Urls.CeilingFansUrl);
            Browser.Wait.IsVisibleElement(By.CssSelector(Sort.ToggleSortMenuClass.ToCssClassSelector()));

            //The user is navigated to the More Like This page URL for the selected SKU.
            var expectedUrl = $"{Urls.MoreLikeThisPageBaseUrl}{Sort.FirstProductSkuOnSort}/";
            Assert.Equals(expectedUrl, Sort.NavigationToMoreLikeThisPage(), "More like this sort page is not displayed.");

            Browser.Wait.IsVisibleElement(By.CssSelector(Sort.MoreLikeThisClass.ToCssClassSelector()));

            //The user is navigated to the PDP for the selected product.
            var expectedProduct = Sort.NavigationToPdpPageByProduct();
            Assert.Equals(expectedProduct, ProductDetail.SkuOnPdp, "User not on product details page.");
        }
    }


    public abstract class T7264_T7265_Base : SortTestsBase
    {
        protected T7264_T7265_Base(ITestOutputHelper output) : base(output) { }

        protected virtual void Validate(string config)
        {
            InitializeFramework(config, Urls.CeilingFansUrl);

            //The user is navigated to the More Like This page URL for the selected SKU.
            var expectedUrl = $"{Urls.MoreLikeThisPageBaseUrl}{Sort.FirstProductSku}{"/"}";
            Assert.Equals(expectedUrl, Sort.NavigationToMoreLikeThisPage(), "More like this sort page is not displayed.");


            //The user is navigated to the PDP for the selected product.
            var expectedProduct = Sort.NavigationToPdpPageByProduct();
            Assert.Equals(expectedProduct.Replace("\r\n", ""), ProductDetail.SkuOnPdp, "User not on product details page.");
        }
    }
}