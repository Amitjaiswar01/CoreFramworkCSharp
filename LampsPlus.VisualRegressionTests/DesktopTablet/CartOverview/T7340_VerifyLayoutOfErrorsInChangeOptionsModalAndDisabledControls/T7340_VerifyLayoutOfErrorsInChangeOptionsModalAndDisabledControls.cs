using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.DesktopTablet.CartOverview.T7340_VerifyLayoutOfErrorsInChangeOptionsModalAndDisabledControls
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7340_Windows_VerifyLayoutOfErrorsInChangeOptionsModalAndDisabledControls : T7340_DesktopBase
    {
        public T7340_Windows_VerifyLayoutOfErrorsInChangeOptionsModalAndDisabledControls(ITestOutputHelper output, T7340_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_ESI)]
        public void LayoutOfErrorsInChangeOptionsModalAndDisabledControls(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7340_Mac_VerifyLayoutOfErrorsInChangeOptionsModalAndDisabledControls : T7340_DesktopBase
    {
        public T7340_Mac_VerifyLayoutOfErrorsInChangeOptionsModalAndDisabledControls(ITestOutputHelper output, T7340_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SIS_ESI)]
        public void LayoutOfErrorsInChangeOptionsModalAndDisabledControls(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7340_iPad_VerifyLayoutOfErrorsInChangeOptionsModalAndDisabledControls : T7340_DesktopBase
    {
        public T7340_iPad_VerifyLayoutOfErrorsInChangeOptionsModalAndDisabledControls(ITestOutputHelper output, T7340_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SIS_ESI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SIS_ESI)]
        public void LayoutOfErrorsInChangeOptionsModalAndDisabledControls(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7340_TabletEmulator_VerifyLayoutOfErrorsInChangeOptionsModalAndDisabledControls : T7340_DesktopBase
    {
        public T7340_TabletEmulator_VerifyLayoutOfErrorsInChangeOptionsModalAndDisabledControls(ITestOutputHelper output, T7340_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SIS_ESI)]
        public void LayoutOfErrorsInChangeOptionsModalAndDisabledControls(string config) => Validate(Validate, config);
    }


    public class T7340_SharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7340_SharedSku_Fixture()
        {
            ShortSku = ProductActions.GetSkuBetweenTenAndTwentyDollars;
        }
    }


    /// <summary>
    /// Verify the layout of the errors in the Change Options modal and the disabled controls of the Price Adjustment modal.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9788
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7340
    /// </summary>
    [Collection(LpTraits.UserRole.EmployeeKiosk)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9788"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7340")]

    public abstract class T7340_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7340_SharedSku_Fixture>
    {
        protected readonly T7340_SharedSku_Fixture Fixture;

        protected T7340_DesktopBase(ITestOutputHelper output, T7340_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            /*Arrange:
            User has added SKU on cart page
            User is on cart page
            */
            InitializeVisualTest(config);
            Assert.DatabaseObject(Fixture.ShortSku, "ProductActions.GetSkuBetweenTenAndTwentyDollars()");
            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel(Fixture.ShortSku));
            Assert.True(Cart.IsCurrentPage,"User is not on cart page.");

            //Act: Click on Shipping options link
            Cart.OpenShippingOptions();

            //Act: Enter zip code and click on search button
            Cart.ApplyZipCode(ZipCodeList.MarshallIslands);

            //Act: Capture screenshot of the modal
            ScreenCapturer.CaptureElementArea(Browser.PageUrl,Modal.GetLpModalContent());

            //Act: Click on Update button
            Cart.ShippingUpdate();

            //Act: Capture screenshot of the entire page with cart ID ignored
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl,Cart.IgnoreCartIdAndCartTitle(),true,true, maxRightOffset:10);
            
            //Act: Click on POS checkbox for SKU
            Cart.CheckPosBox();

            //Act: Capture screenshot of the entire page with cart ID ignored
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl,Cart.IgnoreCartIdAndCartTitle(),true,true, maxRightOffset: 10);
            
            //Act: Click on edit link below product price
            Cart.OpenDiscountTooltip();

            //Act: Capture screenshot of the modal
            ScreenCapturer.CaptureElementArea(Browser.PageUrl,Modal.GetDiscountToolTipModal());
        }
    }
}