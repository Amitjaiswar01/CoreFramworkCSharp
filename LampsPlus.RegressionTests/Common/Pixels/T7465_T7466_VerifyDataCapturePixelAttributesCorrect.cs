using Xunit;
using Xunit.Abstractions;
using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using static LampsPlus.AutomationFramework.Utilities.DataCaptureUtility;

namespace LampsPlus.RegressionTests.Common.Pixels
{
    [Collection(LpTraits.RegressionFeatureTags.Pixel)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Pixels)]
    public class T7465_Windows_VerifyDataCapturePixelAttributesCorrect : T7465_DesktopBase
    {
        public T7465_Windows_VerifyDataCapturePixelAttributesCorrect(ITestOutputHelper output) : base(output, TestConfiguration.Windows_Chrome_SNIS_UNSI) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T7465. Rework - ACD-10713")]
        public void VerifyDataCapturePayload() => Validate();
    }


    [Collection(LpTraits.RegressionFeatureTags.Pixel)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Pixels)]
    public class T7465_Mac_VerifyDataCapturePixelAttributesCorrect : T7465_DesktopBase
    {
        public T7465_Mac_VerifyDataCapturePixelAttributesCorrect(ITestOutputHelper output) : base(output, TestConfiguration.Mac_Safari_SNIS_UNSI) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [Theory(Skip = "Rework - ACD-10759")]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyDataCapturePayload(string config) => Validate();
    }


    [Collection(LpTraits.RegressionFeatureTags.Pixel)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Pixels)]
    public class T7465_iPad_VerifyDataCapturePixelAttributesCorrect : T7465_DesktopBase
    {
        public T7465_iPad_VerifyDataCapturePixelAttributesCorrect(ITestOutputHelper output) : base(output, TestConfiguration.iPad_Safari_SNIS_UNSI) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyDataCapturePayload(string config) => Validate();
    }


    [Collection(LpTraits.RegressionFeatureTags.Pixel)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Pixels)]
    public class T7465_TabletEmulator_VerifyDataCapturePixelAttributesCorrect : T7465_DesktopBase
    {
        public T7465_TabletEmulator_VerifyDataCapturePixelAttributesCorrect(ITestOutputHelper output) : base(output, TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyDataCapturePayload(string config) => Validate();
    }


    public class T7466_iPhone_VerifyDataCapturePixelAttributesCorrect : T7466_MobileBase
    {
        public T7466_iPhone_VerifyDataCapturePixelAttributesCorrect(ITestOutputHelper output) : base(output, TestConfiguration.iPhone_Safari_SNIS_UNSI) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableFact(Skip = "This test should be executed on Mobile Emulator.")]
        public void VerifyDataCapturePayload() => Validate();
    }


    public class T7466_Emulator_VerifyDataCapturePixelAttributesCorrect : T7466_MobileBase
    {
        public T7466_Emulator_VerifyDataCapturePixelAttributesCorrect(ITestOutputHelper output) : base(output, TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T7466. Rework - ACD-10713")]
        public void VerifyDataCapturePayload() => Validate();
    }


    /// <summary>
    /// Verify that the correct data attributes are displayed for Sort page, PDP, Add to Cart and Order Confirmation Page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8268
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7465
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8268"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7465"), Trait(LpTraits.Keys.Category, LpTraits.RegressionFeatureTags.DataCapture)]
    public abstract class T7465_DesktopBase : T7465_T7466_Base
    {
        protected T7465_DesktopBase(ITestOutputHelper output, string config) : base(output, config) { }

        public override void VerifyPageLevelData(List<JObject> parsedEventCalls)
        {
            DataCaptureUtility.VerifyPageLevelPayloadIsForDesktop(parsedEventCalls);
        }

        public override int GetNumberOfItemsOnSortRow()
        {
             return GetVisualProductsCountDesktop();
        }
    }


    /// <summary>
    /// Verify that the correct data attributes are displayed for Sort page, PDP, Add to Cart and Order Confirmation Page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8268
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7466
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8268"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7466"), Trait(LpTraits.Keys.Category, LpTraits.RegressionFeatureTags.DataCapture)]
    public abstract class T7466_MobileBase : T7465_T7466_Base
    {
        protected T7466_MobileBase(ITestOutputHelper output, string config) : base(output, config) { }

        public override void VerifyPageLevelData(List<JObject> parsedEventCalls)
        {
            DataCaptureUtility.VerifyPageLevelPayloadIsForMobile(parsedEventCalls);
        }

        public override int GetNumberOfItemsOnSortRow()
        {
            return 4;
        }
    }

    [Collection(LpTraits.RegressionFeatureTags.DataCapture)]
    public abstract class T7465_T7466_Base : NetworkLoggingTestsBase
    {
        protected T7465_T7466_Base(ITestOutputHelper output, string config) : base(output, config) { }

        protected void Validate()
        {
            var sortAbTestInfo = SortActions.GetSortWithActiveAbTest();
            Skip.If(sortAbTestInfo.Count == 0, "There is no Sort Page with an Active A/B test");

            Browser.Navigate("https://" + sortAbTestInfo[0]["Url"]);

            Browser.Wait.IsVisibleElement(By.ClassName(Sort.UnveilClass));

            Browser.ScrollIntoView(Sort.DisplayedProductAtIndex(0), true);

            NetworkLoggingUtility.ClearNetworkLog();

            Browser.RefreshPage();

            var testCompositionId = sortAbTestInfo[0]["TestCompositionId"];
            var parsedEventCalls = DataCaptureUtility.GetCurrentDataCaptureNetworkData();

            VerifyPageLevelData(parsedEventCalls);

            // Verify amount of items in data-capture call is the same amount as visible
            var skuCount = DataCaptureUtility.GetCountOfSkuEventsBySection(parsedEventCalls, PayloadValues.PageSections.SortResults);
            var numOnRow = GetNumberOfItemsOnSortRow();

            Assert.Equals(numOnRow, skuCount, $"Data capture events for \"{PayloadValues.PageSections.SortResults.ToString()}\" not equal to {numOnRow}.");

            var firstFourVisibleSkus = Sort.ProductContainersList.Take(numOnRow).Select(elem => elem.GetAttribute("data-sku")).ToList();

            // Verifies that the data-capture sku events for the first four visible skus contain the following values for
            // each respective key
            DataCaptureUtility.VerifySkuEventPayload(parsedEventCalls, PayloadValues.PageSections.SortResults, firstFourVisibleSkus, new Dictionary<string, object>
            {
                { PayloadKeys.Event, PayloadValues.Event.SkuView.ToString() },
                { PayloadKeys.EventId, Convert.ToInt32(PayloadValues.Event.SkuView) },
                { PayloadKeys.HasAddToCart, "0" },
                { PayloadKeys.SectionId, Convert.ToInt32(PayloadValues.PageSections.SortResults) },
                { PayloadKeys.TestCompositionId, testCompositionId }
            });

            var pdpSku = Sort.ChooseFirstNormalProduct();

            Browser.Wait.IsVisibleElement(By.Id(GlobalLocators.PdAddToCartId));

            // We need to get a fresh copy of the network data after interacting with the site
            // Waits for the specific sku with the pagesection value to appear in the network log
            parsedEventCalls = DataCaptureUtility.WaitAndGetDataCaptureNetworkData(PayloadValues.PageSections.PDPMainProduct, pdpSku);

            VerifyPageLevelData(parsedEventCalls);

            DataCaptureUtility.VerifySkuEventPayload(parsedEventCalls, PayloadValues.PageSections.PDPMainProduct, new List<string> { pdpSku }, new Dictionary<string, object>
            {
                { PayloadKeys.Event, PayloadValues.Event.SkuView.ToString() },
                { PayloadKeys.EventId, Convert.ToInt32(PayloadValues.Event.SkuView) },
                { PayloadKeys.HasAddToCart, "1" },
                { PayloadKeys.SectionId, Convert.ToInt32(PayloadValues.PageSections.PDPMainProduct) },
                { PayloadKeys.TestCompositionId, testCompositionId }
            });

            AddOneToCart();

            parsedEventCalls = DataCaptureUtility.GetCurrentDataCaptureNetworkData();

            VerifyPageLevelData(parsedEventCalls);

            DataCaptureUtility.VerifySkuEventPayload(parsedEventCalls, PayloadValues.PageSections.AddToCartPdp, new List<string> { pdpSku }, new Dictionary<string, object>
            {
                { PayloadKeys.Event, PayloadValues.Event.AddToCart.ToString() },
                { PayloadKeys.EventId, Convert.ToInt32(PayloadValues.Event.AddToCart) },
                { PayloadKeys.HasAddToCart, null },
                { PayloadKeys.SectionId, Convert.ToInt32(PayloadValues.PageSections.AddToCartPdp) },
                { PayloadKeys.Quantity, 1 },
                { PayloadKeys.TestCompositionId, testCompositionId }
            });

            NetworkLoggingUtility.ClearNetworkLog();

            var nonSortSku = ProductActions.GetSkuGreaterThanTwoHundredDollars;

            PlaceInternationalOrder(nonSortSku);

            parsedEventCalls = DataCaptureUtility.WaitAndGetDataCaptureNetworkData(null, pdpSku, nonSortSku);

            VerifyPageLevelData(parsedEventCalls);

            // First check common values between the sort and non-sort sku in the Order Confirmation data-capture event
            DataCaptureUtility.VerifySkuEventPayload(parsedEventCalls, null, new List<string> { pdpSku, nonSortSku }, new Dictionary<string, object>
            {
                { PayloadKeys.Event, PayloadValues.Event.OrderConfirmation.ToString() },
                { PayloadKeys.EventId, Convert.ToInt32(PayloadValues.Event.OrderConfirmation) },
                { PayloadKeys.HasAddToCart, null },
                { PayloadKeys.SectionId, null },
            });

            DataCaptureUtility.VerifySkuEventPayload(parsedEventCalls, null, new List<string> { pdpSku }, new Dictionary<string, object>
            {
                { PayloadKeys.Quantity, 1 },
                { PayloadKeys.TestCompositionId, testCompositionId }
            });

            DataCaptureUtility.VerifySkuEventPayload(parsedEventCalls, null, new List<string> { nonSortSku }, new Dictionary<string, object>
            {
                { PayloadKeys.Quantity, 1 },
                { PayloadKeys.TestCompositionId, "0" }
            });
        }

        public abstract void VerifyPageLevelData(List<JObject> parsedEventCalls);

        public abstract int GetNumberOfItemsOnSortRow();

        private void AddOneToCart()
        {
            ProductDetail.QuantityField.SendKeys(Keys.Backspace);
            ProductDetail.QuantityField.SendKeys("1");
            GlobalLocators.AddToCartButton.Click();
            Browser.Wait.IsVisibleElement(By.ClassName(CartOverview.CheckOutNowClass));
        }

        private void PlaceInternationalOrder(string sku)
        {
            Browser.NavigateToPdp(sku);

            Browser.Wait.IsVisibleElement(By.Id(GlobalLocators.PdAddToCartId));

            GlobalLocators.AddToCartButton.Click();

            Browser.Wait.IsVisibleElement(By.ClassName(CartOverview.CheckOutNowClass));

            CartOverview.CheckOutNowButton.Click();

            Browser.Wait.IsVisibleElement(By.Id(Shipping.ProceedPaymentId));

            WaitForGlobalSpinnerToClose();

            ShoppingCartWorkflow.EnterDefaultShippingAddress();
            ShoppingCartWorkflow.ProceedToPayment();

            Browser.Wait.IsVisibleElement(By.ClassName(Payment.SameAsShippingControlClass));
            Browser.Wait.ForClickableElement(Payment.SameAsShippingCheckBoxGeneric).Click();

            CustomerAddressInformation.EnterIntBillingAddress(new IntAddress());

            Payment.PlaceInternationalOrder();

            Browser.Wait.IsVisibleElement(By.ClassName(OrderConfirmation.CreateAccountButtonClass));
        }
    }
}
