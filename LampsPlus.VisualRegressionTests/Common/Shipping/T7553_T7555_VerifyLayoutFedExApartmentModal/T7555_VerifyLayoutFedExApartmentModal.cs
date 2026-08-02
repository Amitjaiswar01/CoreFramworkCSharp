using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Pages.Refactored.Address;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.Shipping.T7553_T7555_VerifyLayoutFedExApartmentModal
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7555_iPhone_VerifyLayoutFedExApartmentModal : T7555_MobileBase
    {
        public T7555_iPhone_VerifyLayoutFedExApartmentModal(ITestOutputHelper output, T7555_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void LayoutFedExApartmentModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7555_AndroidPhone_VerifyLayoutFedExApartmentModal : T7555_MobileBase
    {
        public T7555_AndroidPhone_VerifyLayoutFedExApartmentModal(ITestOutputHelper output, T7555_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutFedExApartmentModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7555_Emulator_VerifyLayoutFedExApartmentModal : T7555_MobileBase
    {
        public T7555_Emulator_VerifyLayoutFedExApartmentModal(ITestOutputHelper output, T7555_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutFedExApartmentModal(string config) => Validate(Validate, config);
    }


    public class T7555_SharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7555_SharedSku_Fixture()
        {
            ShortSku = ProductActions.GetAnySkuWithProductDetailPage;
        }
    }


    /// <summary>
    /// Verify the layout of the FedEx Apartment Verification modal.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8631
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7555
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8631"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7555")]
    public abstract class T7555_MobileBase : VisualTestsBaseMobile, IClassFixture<T7555_SharedSku_Fixture>
    {
        protected readonly T7555_SharedSku_Fixture Fixture;

        protected T7555_MobileBase(ITestOutputHelper output, T7555_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            //Arrange: User has identified a SKU and added it to the cart.
            InitializeVisualTest(config);
            var sku = Fixture.ShortSku;
            Assert.DatabaseObject(sku, "ProductActions.GetAnySkuWithProductDetailPage()");
            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel(sku));
            Cart.CheckOut();
            Assert.True(Shipping.IsCurrentPage, "Current page is not a Shipping page");

            //Arrange: custom address
            var addressCustom = new Address
            {
                FirstName = "Test",
                LastName = "Test",
                AddressLine1 = "607 East Providencia Ave",
                City = "Burbank",
                ZipCode = "91501"
            };

            //Act: Enter Shipping page fields and FedEx modal is triggered
            ShoppingCartWorkflow.ShowFedExValidationModal(enterApartment: false, address: addressCustom);

            //Act: Capture a screenshot of the FedEx Validation modal element.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}
