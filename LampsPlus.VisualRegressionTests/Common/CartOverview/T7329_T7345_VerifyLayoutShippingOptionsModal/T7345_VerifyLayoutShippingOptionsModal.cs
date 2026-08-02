using System.Collections.Generic;
using xRetry;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;


namespace LampsPlus.VisualRegressionTests.Common.CartOverview.T7329_T7345_VerifyLayoutShippingOptionsModal
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7345_iPhone_VerifyLayoutShippingOptionsModal : T7345_MobileBase
    {
        public T7345_iPhone_VerifyLayoutShippingOptionsModal(ITestOutputHelper output, T7345_ShareSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth)]
        public void LayoutShippingOptModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7345_AndroidPhone_VerifyLayoutShippingOptionsModal : T7345_MobileBase
    {
        public T7345_AndroidPhone_VerifyLayoutShippingOptionsModal(ITestOutputHelper output, T7345_ShareSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutShippingOptModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7345_Emulator_VerifyLayoutShippingOptionsModal : T7345_MobileBase
    {
        public T7345_Emulator_VerifyLayoutShippingOptionsModal(ITestOutputHelper output, T7345_ShareSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutShippingOptModal(string config) => Validate(Validate, config);
    }


    public class T7345_ShareSkus_Fixture : FixtureBase
    {
        public string CanadaShippableShortSku { get; }

        public T7345_ShareSkus_Fixture()
        {
            CanadaShippableShortSku = ProductActions.GetAnySkuWithProductDetailPage;
        }
    }

    /// <summary>
    /// Verify the layout of the Shipping Options modal and Cart Overview page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7501
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7345
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7501"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7345")]
    public abstract class T7345_MobileBase : VisualTestsBaseMobile, IClassFixture<T7345_ShareSkus_Fixture>

    {
        protected readonly T7345_ShareSkus_Fixture Fixture;

        protected T7345_MobileBase(ITestOutputHelper output, T7345_ShareSkus_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            /*Arrangement
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
            Capture a screenshot of entire page.
            */
            Assert.True(Cart.IsCurrentPage, "Current page is not Cart page");
            Cart.OpenShippingOptions();
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, Cart.IgnoreShippingOptionModal());

            /*Act
            Select Canada from country drop-down, enter Canada Zipcode and apply. 
            Capture a screenshot of the entire page.
            */
            CustomerAddressInformation.SelectCountry(CountryCodeList.CA);
            Cart.ApplyZipCode(canadaZipcode);
            ScreenCapturer.CaptureWholeOverlayModal(Browser.PageUrl, Cart.IgnoreShippingOptionModal(), false, true, new List<IElement> { Shipping.IgnoreMobileShippingOptionsModal() });

            /*Act
            Navigate to the Cart Overview page.
            Capture a screenshot of the visible page.
            */
            Cart.ShippingUpdate();
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}
