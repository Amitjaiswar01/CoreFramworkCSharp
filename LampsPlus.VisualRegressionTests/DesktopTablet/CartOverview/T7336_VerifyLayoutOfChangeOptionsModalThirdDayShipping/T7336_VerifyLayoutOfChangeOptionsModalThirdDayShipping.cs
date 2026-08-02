using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using System.Collections.Generic;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.DesktopTablet.CartOverview.T7336_VerifyLayoutOfChangeOptionsModalThirdDayShipping
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7336_Windows_VerifyLayoutOfChangeOptionsModalThirdDayShipping : T7336_DesktopBase
    {
        public T7336_Windows_VerifyLayoutOfChangeOptionsModalThirdDayShipping(ITestOutputHelper output, T7336_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void VerifyLayoutOfChangeOptionsModalThirdDayShipping(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly) ]
    public class T7336_Mac_VerifyLayoutOfChangeOptionsModalThirdDayShipping : T7336_DesktopBase
    {
        public T7336_Mac_VerifyLayoutOfChangeOptionsModalThirdDayShipping(ITestOutputHelper output, T7336_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void VerifyLayoutOfChangeOptionsModalThirdDayShipping(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7336_iPad_VerifyLayoutOfChangeOptionsModalThirdDayShipping : T7336_DesktopBase
    {
        public T7336_iPad_VerifyLayoutOfChangeOptionsModalThirdDayShipping(ITestOutputHelper output, T7336_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void VerifyLayoutOfChangeOptionsModalThirdDayShipping(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7336_TabletEmulator_VerifyLayoutOfChangeOptionsModalThirdDayShipping : T7336_DesktopBase
    {
        public T7336_TabletEmulator_VerifyLayoutOfChangeOptionsModalThirdDayShipping(ITestOutputHelper output, T7336_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void VerifyLayoutOfChangeOptionsModalThirdDayShipping(string config) => Validate(Validate, config);
    }


    public class T7336_SharedProductSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }
       
        public T7336_SharedProductSku_Fixture()
        {
            ShortSku = ProductActions.GetShortSkuQualifiedFor3rdDayShipping();
        }
    }


    /// <summary>
    /// Verify Layout Of Change Options Modal Third Day Shipping
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9794
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7336
    /// </summary>
    [Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9794"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7336")]
    public abstract class T7336_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7336_SharedProductSku_Fixture>
    {
        protected readonly T7336_SharedProductSku_Fixture Fixture;

        protected T7336_DesktopBase(ITestOutputHelper output, T7336_SharedProductSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            /*Arrange
            Navigate Sku that qualifies 3rd Day Shipping
            Clear Cart
            */
            InitializeVisualTest(config);
            ShoppingCartWorkflow.EmptyCart();
            Assert.DatabaseObject(Fixture.ShortSku, "ProductionActions.GetShortSkuQualifiedFor3rdDayShipping");
            ProductDetail.AddSingleProductToCart(Fixture.ShortSku);

            /*Act
            Navigate to Cart Overview Page
            Click on Standard Shipping Link
            Apply Zip Code 91311
            */
            Assert.True(Cart.IsCurrentPage, "User is not on Cart Page");
            Cart.OpenShippingOptions();
            Cart.ApplyZipCode(ZipCodeList.Chatsworth);

            // Act: Capture Screenshot of Modal
            Modal.IsModalVisible();
            ScreenCapturer.CaptureScreenRegionWithIgnoredLayouts(Browser.PageUrl, Modal.GetLpModal(), new List<IElement> { Cart.IgnoreModalTimeCheck(0) , Cart.IgnoreModalTimeCheck(1), Cart.IgnoreModalTimeCheck(2) },true);
        }
    }
}