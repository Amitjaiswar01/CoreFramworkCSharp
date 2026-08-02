using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail.T7821_T7822_VerifyLayoutOfSpecificationsTable
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7821_Windows_VerifyLayoutOfSpecificationsTables : T7821_DesktopBase
    {
        public T7821_Windows_VerifyLayoutOfSpecificationsTables(ITestOutputHelper output, T7821_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyLayoutOfSpecificationsTables(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7821_Mac_VerifyLayoutOfSpecificationsTables : T7821_DesktopBase
    {
        public T7821_Mac_VerifyLayoutOfSpecificationsTables(ITestOutputHelper output, T7821_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfSpecificationsTables(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7821_iPad_VerifyLayoutOfSpecificationsTables : T7821_DesktopBase
    {
        public T7821_iPad_VerifyLayoutOfSpecificationsTables(ITestOutputHelper output, T7821_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfSpecificationsTables(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7821_TabletEmulator_VerifyLayoutOfSpecificationsTables : T7821_DesktopBase
    {
        public T7821_TabletEmulator_VerifyLayoutOfSpecificationsTables(ITestOutputHelper output, T7821_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyLayoutOfSpecificationsTables(string config) => Validate(Validate, config);
    }


    public class T7821_SharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7821_SharedSku_Fixture()
        {
            ShortSku = ProductActions.GetSkuThatHasSpecificationsTables;
        }
    }


    /// <summary>
    /// Verify the Layout of the Specifications Tables Displayed on PDP
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9846
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7821
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9846"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7821")]
    public abstract class T7821_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7821_SharedSku_Fixture>
    {
        protected readonly T7821_SharedSku_Fixture Fixture;

        protected T7821_DesktopBase(ITestOutputHelper output, T7821_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            //Arrange: User has identified a SKU that has a Specification table.
            InitializeVisualTest(config);
            var sku = Fixture.ShortSku;
            Assert.DatabaseObject(sku, "ProductActions.GetSkuThatHasSpecificationsTables()");

            /*Act:
             Navigate to the PDP https://www.lampsplus.com/products/<SKU>.
             Once the PDP loads, scroll down the page to the Specifications section.
             */
            ProductDetail.NavigateToProductDetailByShortSku(sku);
            ProductDetail.ScrollToProductSpecificationTable();

            //Act: Capture a screenshot of the whole page while ignoring the MYML container.
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ProductDetail.IgnoreMoreYouMayLikeSection() }, true, true);
        }
    }
}
