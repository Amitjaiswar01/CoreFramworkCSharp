using System.Collections.Generic;
using xRetry;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail.T7291_T7314_VerifyLayoutOfEnergyGuideForCeilingFans
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7314_iPhone_VerifyLayoutOfEnergyGuideForCeilingFans : T7314_MobileBase
    {
        public T7314_iPhone_VerifyLayoutOfEnergyGuideForCeilingFans(ITestOutputHelper output, T7314_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth)]
        public void LayoutOfEnergyGuideForCeilingFans(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7314_Android_VerifyLayoutOfEnergyGuideForCeilingFans : T7314_MobileBase
    {
        public T7314_Android_VerifyLayoutOfEnergyGuideForCeilingFans(ITestOutputHelper output, T7314_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfEnergyGuideForCeilingFans(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7314_Emulator_VerifyLayoutOfEnergyGuideForCeilingFans : T7314_MobileBase
    {
        public T7314_Emulator_VerifyLayoutOfEnergyGuideForCeilingFans(ITestOutputHelper output, T7314_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfEnergyGuideForCeilingFans(string config) => Validate(Validate, config);
    }


    public class T7314_SharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7314_SharedSku_Fixture()
        {
            ShortSku = ProductActions.GetFanWithEnergyGuideIconShortSku;
        }
    }


    /// <summary>
    /// Verify the layout of the Energy Guide for Ceiling Fans.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9847
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7314
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9847"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7314")]
    public abstract class T7314_MobileBase : VisualTestsBaseMobile, IClassFixture<T7314_SharedSku_Fixture>
    {
        protected readonly T7314_SharedSku_Fixture Fixture;

        protected T7314_MobileBase(ITestOutputHelper output, T7314_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            //Arrange: User has identified a ceiling fan that has an energy guide.
            InitializeVisualTest(config);
            var sku = Fixture.ShortSku;
            Assert.DatabaseObject(sku, "ProductActions.GetFanWithEnergyGuideIconShortSku().ShortSku");

            //Act: Take the SKU from the query in the pre-conditions and enter it into the 'Search' field on the Lamps Plus site and execute the search.
            ProductDetail.NavigateToProductDetailByShortSku(sku);

            //Act: Expand the Description section.
            ProductDetail.OpenProductDetailsDrawer();

            //Act: Capture a screenshot of the entire page.
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ProductDetail.IgnoreStockCheckWrapper(), ProductDetail.IgnoreCertonaDrawerName() }, true, true);

            //Act: Tap on the 'Energy Guide' icon.
            ProductDetail.OpenEnergyGuide();

            //Act: Capture a screenshot of the visible screen.
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, ProductDetail.GetEnergyInfoModal());
        }
    }
}
