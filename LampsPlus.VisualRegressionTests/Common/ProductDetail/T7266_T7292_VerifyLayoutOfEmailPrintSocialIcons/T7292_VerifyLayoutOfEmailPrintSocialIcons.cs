using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail.T7266_T7292_VerifyLayoutOfEmailPrintSocialIcons
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7292_iPhone_VerifyLayoutOfEmailPrintSocialIcons : T7292_MobileBase
    {
        public T7292_iPhone_VerifyLayoutOfEmailPrintSocialIcons(ITestOutputHelper output, T7292_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth)]
        public void LayoutOfEmailPrintSocialIcons(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7292_AndroidPhone_VerifyLayoutOfEmailPrintSocialIcons : T7292_MobileBase
    {
        public T7292_AndroidPhone_VerifyLayoutOfEmailPrintSocialIcons(ITestOutputHelper output, T7292_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfEmailPrintSocialIcons(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7292_Emulator_VerifyLayoutOfEmailPrintSocialIcons : T7292_MobileBase
    {
        public T7292_Emulator_VerifyLayoutOfEmailPrintSocialIcons(ITestOutputHelper output, T7292_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfEmailPrintSocialIcons(string config) => Validate(Validate, config);
    }


    public class T7292_SharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7292_SharedSku_Fixture()
        {
            ShortSku = ProductActions.GetFreeShippingAndReturnShortSkus;
        }
    }

    /// <summary>
    /// Verify the layout of Email and Print icons, Social Media Icons, Free Shipping & Free Returns Callout, Check Stock / Check Availability link and Inventory Information on the PDP.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7360
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7292
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7360"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7292")]
    public abstract class T7292_MobileBase : VisualTestsBaseMobile, IClassFixture<T7292_SharedSku_Fixture>
    {
        protected readonly T7292_SharedSku_Fixture Fixture;

        protected T7292_MobileBase(ITestOutputHelper output, T7292_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            //Arrange: User has identified a SKU.
            InitializeVisualTest(config);
            var shortSku = Fixture.ShortSku;
            Assert.DatabaseObject(shortSku, "ProductActions.GetFreeShippingAndReturnShortSkus()");

            //Act: Load the PDP page.
            ProductDetail.NavigateToProductDetailByShortSku(shortSku);

            //Act: Capture a screenshot of the entire page.
            Browser.ScrollToBottomOfPage(Browser.PageUrl);
            Browser.ScrollToTopOfWindow();
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ProductDetail.IgnoreStockCheckWrapper(), ProductDetail.IgnoreCertonaDrawerName(), ProductDetail.IgnoreMoreYouMayLikeContainer() },true,true);
        }
    }
}
