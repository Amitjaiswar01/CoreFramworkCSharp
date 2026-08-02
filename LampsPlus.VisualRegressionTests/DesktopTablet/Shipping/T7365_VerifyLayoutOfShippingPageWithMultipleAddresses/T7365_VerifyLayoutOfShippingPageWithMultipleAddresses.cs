using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Pages.Refactored.Address;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.DesktopTablet.Shipping.T7365_VerifyLayoutOfShippingPageWithMultipleAddresses
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7365_Windows_VerifyLayoutOfShippingPageWithMultipleAddresses : T7365_DesktopBase
    {
        public T7365_Windows_VerifyLayoutOfShippingPageWithMultipleAddresses(ITestOutputHelper output, T7365_DesktopFixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void VerifyLayoutOfShippingPageWithMultipleAddresses(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7365_Mac_VerifyLayoutOfShippingPageWithMultipleAddresses : T7365_DesktopBase
    {
        public T7365_Mac_VerifyLayoutOfShippingPageWithMultipleAddresses(ITestOutputHelper output, T7365_DesktopFixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void VerifyLayoutOfShippingPageWithMultipleAddresses(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7365_iPad_VerifyLayoutOfShippingPageWithMultipleAddresses : T7365_DesktopBase
    {
        public T7365_iPad_VerifyLayoutOfShippingPageWithMultipleAddresses(ITestOutputHelper output, T7365_DesktopFixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void VerifyLayoutOfShippingPageWithMultipleAddresses(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7365_TabletEmulator_VerifyLayoutOfShippingPageWithMultipleAddresses : T7365_DesktopBase
    {
        public T7365_TabletEmulator_VerifyLayoutOfShippingPageWithMultipleAddresses(ITestOutputHelper output, T7365_DesktopFixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void VerifyLayoutOfShippingPageWithMultipleAddresses(string config) => Validate(Validate, config);
    }


    public class T7365_DesktopFixture : FixtureBase
    {
        public string FirstShortSku { get; }
        public string SecondShortSku { get; }

        public T7365_DesktopFixture()
        {
            // Make sure we have 2 different items to be able to do multiple shipping addresses
            do
            {
                FirstShortSku = ProductActions.GetAnySkuWithProductDetailPage;
                SecondShortSku = ProductActions.GetAnySkuWithProductDetailPage;
            }
            while (FirstShortSku == SecondShortSku);
        }
    }

    /// <summary>
    /// Verify the layout of the Shipping Page with Multiple Addresses.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7508
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7365
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7508"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7365")]
    public abstract class T7365_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7365_DesktopFixture>
    {
        protected readonly T7365_DesktopFixture Fixture;

        protected T7365_DesktopBase(ITestOutputHelper output, T7365_DesktopFixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            //Arrange: User has identified 2 SKUs and added them to the cart 
            InitializeVisualTest(config);
            var firstSku = Fixture.FirstShortSku;
            var secondSku = Fixture.SecondShortSku;
            Assert.DatabaseObject(firstSku, "ProductActions.GetAnySkuWithProductDetailPage()");
            Assert.DatabaseObject(secondSku, "ProductActions.GetAnySkuWithProductDetailPage()");

            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel { Sku = firstSku });
            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel { Sku = secondSku });

            //Act: From the Cart page, proceed to the Shipping Page.
            CsrBlock.SetSaleSourceValue();//Phone selected by default
            Cart.CheckOut();
            Assert.True(Shipping.IsCurrentPage, "Current page is not a Shipping page");

            //Act: On the Shipping page, click on the Ship to multiple addresses link.
            Shipping.ShipToMultipleAddresses();

            //Act: Fill the Shipping Information form as Item 1 Address.
            var firstShippingAddress = new Address("Item1");
            ShoppingCartWorkflow.CreateNewSavedAddressFromModal(firstShippingAddress, newAddressButtonIndex:0);
            Assert.True(Modal.IsModalNotVisible(), "Modal is displayed");

            //Act: Fill the Shipping Information form with a different first name, last name and address than Item 1 and click SAVE.
            var secondShippingAddress = new Address("Item2");
            ShoppingCartWorkflow.CreateNewSavedAddressFromModal(secondShippingAddress, newAddressButtonIndex: 1);
            Assert.True(Modal.IsModalNotVisible(), "Modal is displayed");

            //Act:  Capture a screenshot of the entire page.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture);
        }
    }
}