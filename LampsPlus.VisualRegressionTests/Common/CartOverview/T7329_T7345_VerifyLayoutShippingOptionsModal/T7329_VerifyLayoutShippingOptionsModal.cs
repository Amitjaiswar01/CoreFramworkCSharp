using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;


namespace LampsPlus.VisualRegressionTests.Common.CartOverview.T7329_T7345_VerifyLayoutShippingOptionsModal
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7329_Windows_VerifyLayoutShippingOptionsModal : T7329_DesktopBase
    {
        public T7329_Windows_VerifyLayoutShippingOptionsModal(ITestOutputHelper output, T7239_ShareSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutShippingOptModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7329_Mac_VerifyLayoutShippingOptionsModal : T7329_DesktopBase
    {
        public T7329_Mac_VerifyLayoutShippingOptionsModal(ITestOutputHelper output, T7239_ShareSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutShippingOptModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7329_iPad_VerifyLayoutShippingOptionsModal : T7329_DesktopBase
    {
        public T7329_iPad_VerifyLayoutShippingOptionsModal(ITestOutputHelper output, T7239_ShareSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutShippingOptModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7329_TabletEmulator_VerifyLayoutShippingOptionsModal : T7329_DesktopBase
    {
        public T7329_TabletEmulator_VerifyLayoutShippingOptionsModal(ITestOutputHelper output, T7239_ShareSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutShippingOptModal(string config) => Validate(Validate, config);
    }


    public class T7239_ShareSkus_Fixture : FixtureBase
    {
        public string CanadaShippableShortSku { get; }

        public T7239_ShareSkus_Fixture()
        {
            CanadaShippableShortSku = ProductActions.GetAnySkuWithProductDetailPage;
        }
    }

    /// <summary>
    /// Verify the layout of the Shipping Options modal and Cart Overview page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7501
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7329
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7501"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7329")]
    public abstract class T7329_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7239_ShareSkus_Fixture>
   
    {
        protected readonly T7239_ShareSkus_Fixture Fixture;

        protected T7329_DesktopBase(ITestOutputHelper output, T7239_ShareSkus_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            /*Arrange
            User has added the CanadaShippable SKU item to the cart.
            */
            InitializeVisualTest(config);
            var canadaZipcode = "a1a1a1";
            Assert.DatabaseObject(Fixture.CanadaShippableShortSku, "ProductionActions.GetCanadaShippableSku()");

            /*Act
            Click on the 'Change Options' link on the Cart Overview page.     
            */
            ProductDetail.NavigateToProductDetailByShortSku(Fixture.CanadaShippableShortSku);
            Assert.True(ProductDetail.IsCurrentPage, "User is not on PDP.");
            ProductDetail.AddToCart();

            /*Act
            Navigate to the Cart Overview page.
            Capture a screenshot of the Shipping Options overlay.
            */
            Assert.True(Cart.IsCurrentPage, "Current page is not Cart page");
            Cart.OpenShippingOptions();
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, Cart.IgnoreShippingOptionModal());

            /*Act
            Select Canada from country drop-down, enter Canada Zipcode and apply. 
            Capture a screenshot of the Shipping Overlay.
            */
            CustomerAddressInformation.SelectCountry(CountryCodeList.CA);
            Cart.ApplyZipCode(canadaZipcode);
            ScreenCapturer.CaptureElementAreaWithIgnoredLayouts(Browser.PageUrl, Modal.GetLpModal(), new List<IElement> { Cart.IgnoreModalTimeCheck(0) });

            /*Act
            Navigate to the Cart Overview page.
            Capture a screenshot of the entire page.
            */
            Cart.ShippingUpdate();
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, Cart.IgnoreCartIdAndMoreYouMayLike(), true, false, Cart.GetMoreYouMayLike(), maxDownOffset:10, maxRightOffset:10); //
        }
    }
}