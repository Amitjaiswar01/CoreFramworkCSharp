using System.Collections.Generic;
using xRetry;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Pages.Refactored.Address;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.Shipping.T7376_T7380_VerifyTheLayoutOfShippingMethodChangeErrorMsg
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7380_iPhone_VerifyTheLayoutOfShippingMethodChangeErrorMsg : T7380_MobileBase
    {
        public T7380_iPhone_VerifyTheLayoutOfShippingMethodChangeErrorMsg(ITestOutputHelper output, T7380_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth)]
        public void LayoutOfShippingMethodChangeErrorMsg(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7380_Android_VerifyTheLayoutOfShippingMethodChangeErrorMsg : T7380_MobileBase
    {
        public T7380_Android_VerifyTheLayoutOfShippingMethodChangeErrorMsg(ITestOutputHelper output, T7380_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfShippingMethodChangeErrorMsg(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7380_Emulator_VerifyTheLayoutOfShippingMethodChangeErrorMsg : T7380_MobileBase
    {
        public T7380_Emulator_VerifyTheLayoutOfShippingMethodChangeErrorMsg(ITestOutputHelper output, T7380_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfShippingMethodChangeErrorMsg(string config) => Validate(Validate, config);
    }


    public class T7380_SharedSku_Fixture : FixtureBase
    {
        public string AnySkuWithProductDetailPage { get; }

        public T7380_SharedSku_Fixture()
        {
            AnySkuWithProductDetailPage = ProductActions.GetAnySkuWithProductDetailPage;
        }
    }


    /// <summary>
    /// Verify the layout of the shipping method change Error Message.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7511
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7380
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7511"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7380")]
    public abstract class T7380_MobileBase : VisualTestsBaseMobile, IClassFixture<T7380_SharedSku_Fixture>
    {
        protected readonly T7380_SharedSku_Fixture Fixture;

        protected T7380_MobileBase(ITestOutputHelper output, T7380_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            //Arrange: User has identified a SKU and added it to the cart.
            InitializeVisualTest(config);
            var sku = Fixture.AnySkuWithProductDetailPage;
            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel(sku));

            //Act: On the Cart page, click on the Change Options link.
            Cart.OpenShippingOptions();

            /*Act:
            Enter 91311 in the zip code field.
            Click the APPLY button.
            Click the CLOSE button.
            Proceed to the Shipping Page.
            */
            Cart.ApplyZipCode(ZipCodeList.Chatsworth);
            Cart.ShippingUpdate();

            /*Act:
            Proceed to the Shipping Page.
            On the Shipping Page, enter an Alaska address - make sure to enter 'lptest' in the Apt/Other field.
            */
            Cart.CheckOut();
            Assert.True(Shipping.IsCurrentPage, "Current page is not a shipping page");
            var address = new Address { State = StateCodeListUnitedStates.AK };
            CustomerAddressInformation.EnterShippingAddress(address);

            //Act: Capture a screenshot of the entire page.
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { Cart.IgnoreCartId(), CustomerAddressInformation.GetEmailField() }, useStitchMode:true, useLazyLoad:true);
        }
    }
}