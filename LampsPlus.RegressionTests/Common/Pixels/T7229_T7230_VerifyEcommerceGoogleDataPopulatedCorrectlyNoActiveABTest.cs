using System.Collections.Generic;
using System.Web;
using System;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;
using xRetry;
using OpenQA.Selenium;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Pixels
{
    [Collection(LpTraits.RegressionFeatureTags.Pixel)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Pixels)]
    public class A_T7229_Windows_VerifyEcommerceGoogleDataPopulatedWithNoActiveABTest : T7229_DesktopBase
    {
        public A_T7229_Windows_VerifyEcommerceGoogleDataPopulatedWithNoActiveABTest(ITestOutputHelper output) : base(output, TestConfiguration.Windows_Chrome_SNIS_UNSI) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T7229. Rework - ACD-10721")]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void EcommerceGoogleDataPopulated(string config) => Validate(config);
    }


    [Collection(LpTraits.RegressionFeatureTags.Pixel)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Pixels)]
    public class A_T7229_Mac_VerifyEcommerceGoogleDataPopulatedWithNoActiveABTest : T7229_DesktopBase
    {
        public A_T7229_Mac_VerifyEcommerceGoogleDataPopulatedWithNoActiveABTest(ITestOutputHelper output) : base(output, TestConfiguration.Mac_Safari_SNIS_UNSI) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T7229. Rework - ACD-10721")]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void EcommerceGoogleDataPopulated(string config) => Validate(config);
    }


    [Collection(LpTraits.RegressionFeatureTags.Pixel)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Pixels)]
    public class A_T7229_iPad_VerifyEcommerceGoogleDataPopulatedWithNoActiveABTest : T7229_DesktopBase
    {
        public A_T7229_iPad_VerifyEcommerceGoogleDataPopulatedWithNoActiveABTest(ITestOutputHelper output) : base(output, TestConfiguration.iPad_Safari_SNIS_UNSI) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T7229. Rework - ACD-10721")]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void EcommerceGoogleDataPopulated(string config) => Validate(config);
    }


    [Collection(LpTraits.RegressionFeatureTags.Pixel)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Pixels)]
    public class A_T7229_TabletEmulator_VerifyEcommerceGoogleDataPopulatedWithNoActiveABTest : T7229_DesktopBase
    {
        public A_T7229_TabletEmulator_VerifyEcommerceGoogleDataPopulatedWithNoActiveABTest(ITestOutputHelper output) : base(output, TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T7229. Rework - ACD-10721")]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void EcommerceGoogleDataPopulated(string config) => Validate(config);
    }


    public class T7230_iPhone_VerifyEcommerceGoogleDataPopulatedWithNoActiveABTest : T7230_MobileBase
    {
        public T7230_iPhone_VerifyEcommerceGoogleDataPopulatedWithNoActiveABTest(ITestOutputHelper output) : base(output, TestConfiguration.iPhone_Safari_SNIS_UNSI) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T7230. Rework - ACD-10514")]
        public void EcommerceGoogleDataPopulated(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Pixels)]
    public class T7230_Emulator_VerifyEcommerceGoogleDataPopulatedWithNoActiveABTest : T7230_MobileBase
    {
        public T7230_Emulator_VerifyEcommerceGoogleDataPopulatedWithNoActiveABTest(ITestOutputHelper output) : base(output, TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T7230. Rework - ACD-10514")]
        public void EcommerceGoogleDataPopulated(string config) => Validate(config);
    }

    /// <summary>
    /// Verify that the Ecommerce Google Data is populated correctly (No Active A/B Test).
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7156
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7229
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7156"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7229"), Trait(LpTraits.Keys.Category, LpTraits.RegressionFeatureTags.Pixel)]
    public abstract class T7229_DesktopBase : T7229_T7230_Base
    {
        protected T7229_DesktopBase(ITestOutputHelper output, string config) : base(output, config) { }

        protected override void AddQuantityValue(string qty)
        {
            if (ProductDetail.PdProdInfoColElement.FindElements(By.Id("pdAddToCart")).Count > 0)
            {
                ProductDetail.QuantityField.SendKeys(Keys.Backspace);
                ProductDetail.QuantityField.SendKeys(qty);
            }
            else
            {
                Browser.Locate.ElementById("QtyMultiProd").SendKeys(Keys.Backspace);
                Browser.Locate.ElementById("QtyMultiProd").SendKeys(qty);
            }
        }

        protected override int GetVisualProdsCount()
        {
            return GetVisualProductsCountDesktop();
        }

        protected override void CloseMobileSortMenu()
        {
        }
    }

    /// <summary>
    /// Verify that the Ecommerce Google Data is populated correctly (No Active A/B Test).
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-6748
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7230
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-6748"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7230"), Trait(LpTraits.Keys.Category, LpTraits.RegressionFeatureTags.Pixel)]
    public abstract class T7230_MobileBase : T7229_T7230_Base
    {
        protected T7230_MobileBase(ITestOutputHelper output, string config) : base(output, config) { }

        protected override void AddQuantityValue(string qty)
        {
            if (ProductDetail.MobileAddToCartButtonContainer.FindElements(By.Id("pdAddToCart")).Count > 0)
            {
                ProductDetail.QuantityField.SendKeys(Keys.Backspace);
                ProductDetail.QuantityField.SendKeys(qty);
            }
            else
            {
                Browser.Locate.ElementById("QtyMultiProd").SendKeys(Keys.Backspace);
                Browser.Locate.ElementById("QtyMultiProd").SendKeys(qty);
            }
        }

        protected override int GetVisualProdsCount()
        {
            return GetVisualProductsCountMobile();
        }

        protected override void CloseMobileSortMenu()
        {
            if (Sort.ToggleSortFilterMenuButton.IsInitialized && Sort.ToggleSortFilterMenuButton.GetAttribute("aria-expanded") == "true")
            {
                Sort.ToggleSortFilterMenuCloseButton.Click();
            }
        }
    }

    public abstract class T7229_T7230_Base : NetworkLoggingTestsBase
    {
        protected T7229_T7230_Base(ITestOutputHelper output, string config) : base(output, config) { }

        protected void Validate(string config)
        {
            var sortAbTestInfo = SortActions.GetSortWithNoActiveAbTest();

            //using the first test returned by the query
            var sortPath = "https://" + sortAbTestInfo[0]["Url"];

            //add two products to cart using different filter options for each.
            ValidateAbTestGaData(sortAbTestInfo, sortPath, 1);

            //submit order on two products added to cart above.
            CartOverview.CheckOutNowButton.Click();

            // Shipping Page Workflow
            Browser.Wait.IsVisibleElement(By.Id(Shipping.ProceedPaymentId));

            WaitForGlobalSpinnerToClose();

            CustomerAddressInformation.EnterShippingAddress(new IntAddress(), isIntAddress:true);

            // Payment Page Workflow
            ShoppingCartWorkflow.ProceedToPayment();
            Payment.PlaceInternationalOrder();

            Browser.Wait.ForDomReady();

            // Order Confirmation Page
            Browser.Wait.IsVisibleElement(By.ClassName(OrderConfirmation.OrderConfirmationHeaderContainerClass));

            var oCuTagValues = GetAndFormatUtagData();

            var expectedOcProdQueryStrings = new Dictionary<string, string>();

            expectedOcProdQueryStrings.Add("pr1cd16", $"{oCuTagValues["TestId"][0]}");
            expectedOcProdQueryStrings.Add("pr1cd17", $"{oCuTagValues["MmId"][0]}");
            expectedOcProdQueryStrings.Add("pr1cd18", $"{oCuTagValues["FormulaId"][0]}");
            expectedOcProdQueryStrings.Add("pr1cd19", $"{oCuTagValues["PinId"][0]}");
            expectedOcProdQueryStrings.Add("pr1cd20", $"{oCuTagValues["TestStartDate"][0]}");
            expectedOcProdQueryStrings.Add("pr1cd25", $"{sortAbTestInfo[0]["TestCompositionId"]}");
            expectedOcProdQueryStrings.Add("pr1cd35", $"{sortAbTestInfo[0]["FilterId"]}");

            Assert.True(NetworkLoggingUtility.RequestHasQueryParams("dt=order%20processing", expectedOcProdQueryStrings), "Order Confirmation is not sending expected product information.");
        }
        private void ValidateAbTestGaData(List<Dictionary<string, string>> sortAbTestInfo, string sortPath, int reps)
        {
            for (var i = 0; i < reps; i++)
            {
                //Sort page product impressions
                NetworkLoggingUtility.ClearNetworkLog();

                Browser.Navigate(sortPath);
                Browser.Wait.ForDomReady();

                NetworkLoggingUtility.ClearNetworkLog();

                Sort.GetRandomAppliedFilterOption(ExcludeFilters());

                Browser.Wait.ForDomReady();

                //set up complete, compare utag data to database and GA data in network logs to utag data. 
                CompareGaData(sortPath, sortAbTestInfo, true);
            }
        }

        private void CompareGaData(string sortPath, List<Dictionary<string, string>> sortAbTestInfo, bool verboseLogs)
        {

            var uTagValues = GetAndFormatUtagData();

            var uTagFilterId = uTagValues["FilterId"][0];

            var currentTestInfo = sortAbTestInfo.ToList();

            var currentTestInfoFilterId = sortAbTestInfo[0]["FilterId"];

            Assert.Equals(currentTestInfoFilterId, uTagFilterId, $"Utag FilterId '{uTagFilterId}' does not match Filter Id '{currentTestInfoFilterId}' found in database");

            //Step 2
            //if we did find a matching entry, compare the rest of the values
            if (currentTestInfo.Count > 0)
            {
                var expectedUtagValues = new Dictionary<string, string>
                {
                    {"TestId", "0"},
                    {"MmId", "0"},
                    {"FormulaId", "0"},
                    {"PinId", "0"},
                    {"TestStartDate", ""},
                    {"TestCompositionId", currentTestInfo[0]["TestCompositionId"]},
                    {"FilterId", currentTestInfo[0]["FilterId"]}
                };
                //verify utag data
                var uTagDataIsCorrect = expectedUtagValues.All(uTag => uTagValues.Any(info => info.Key == uTag.Key && info.Value[0] == uTag.Value));
                Assert.True(uTagDataIsCorrect, "Utag data does not match expected values");
            }

            Uri rel = new Uri(Browser.PageUrl);

            var relPath = rel.AbsolutePath;

            var expectedGeneralQueryStrings = new Dictionary<string, string>
            {
                {"ec", "ecommerce"},
                {"ea", "product impression"},
                {"cd26", "Sort"},
                {"il1nm", relPath}
            };

            var prodsPerRow = GetVisualProdsCount();

            var expectedProdsQueryStrings = new Dictionary<string, string>();

            for (var i = 1; i <= prodsPerRow; i++)
            {
                var skuValue = Sort.DisplayedProductAtIndex(i - 1).GetAttribute(GlobalLocators.DataSkuAttribute);
                var priceValue = Sort.DisplayedProductAtIndex(i - 1).GetAttribute("data-price");
                var nameValue = Sort.GetProductNameBySku(skuValue);

                expectedProdsQueryStrings.Add($"il1pi{i}id", skuValue);
                expectedProdsQueryStrings.Add($"il1pi{i}nm", nameValue);
                expectedProdsQueryStrings.Add($"il1pi{i}ca", uTagValues["ProductCategory"][0]);
                expectedProdsQueryStrings.Add($"il1pi{i}pr", priceValue);
                expectedProdsQueryStrings.Add($"il1pi{i}ps", $"{i}");
                expectedProdsQueryStrings.Add($"il1pi{i}cd16", $"{uTagValues["TestId"][0]}");
                expectedProdsQueryStrings.Add($"il1pi{i}cd17", $"{uTagValues["MmId"][0]}");
                expectedProdsQueryStrings.Add($"il1pi{i}cd18", $"{uTagValues["FormulaId"][0]}");
                expectedProdsQueryStrings.Add($"il1pi{i}cd19", $"{uTagValues["PinId"][0]}");
                expectedProdsQueryStrings.Add($"il1pi{i}cd20", $"{uTagValues["TestStartDate"][0]}");
                expectedProdsQueryStrings.Add($"il1pi{i}cd25", $"{sortAbTestInfo[0]["TestCompositionId"]}");
                expectedProdsQueryStrings.Add($"il1pi{i}cd35", $"{sortAbTestInfo[0]["FilterId"]}");
            }

            if (verboseLogs)
            {
                //print out expectedProdsQueryStrings
                Log.Message("\nExpectedProdsQueryStrings:");
                foreach (var kvp in expectedProdsQueryStrings) Log.Message($"{kvp.Key}={kvp.Value}");
            }

            //Step 4
            Assert.True(NetworkLoggingUtility.GetRequestQueryParamValueLogging("ea=product%20impression", expectedProdsQueryStrings, false, verboseLogs), "Product Impression is not sending expected product information.");
            //Step 3
            var requestProds = NetworkLoggingUtility.GetNumberOfProductsInRequest("ea=product%20impression", "il1pi");
            Log.Message($"Number of requestProds: {requestProds}");
            Assert.True(requestProds == prodsPerRow, "Number of Products in request do not match the number of products per row");
            //Step 1
            Assert.True(NetworkLoggingUtility.RequestHasQueryParams("ea=product%20impression", expectedGeneralQueryStrings), "Product Impression does not contain general query parameters.");

            //Product detail page view and product click
            NetworkLoggingUtility.ClearNetworkLog();

            //sometimes mobile filter menu is left open, make sure it's closed.
            CloseMobileSortMenu();

            Browser.Wait.ForClickableElement(Sort.FirstDisplayedProductLink);

            //select product
            var pos = Sort.ClickProductNearOrOverTwoHundredDollars();

            Browser.Wait.IsVisibleElement(By.Id(GlobalLocators.PdAddToCartId), 30);

            var prodSku = ProductDetail.SkuOnPdp;
            var prodPrice = ProductDetail.ProductPrice;
            var prodName = ProductDetail.ProductName;

            //product click - uses uTag data from sort view. 
            var expectedGeneralPdpQueryStrings = new Dictionary<string, string>
            {
                {"ec", "ecommerce"},
                {"ea", "product click"},
                {"cd26", "Sort"},
                {"pal", relPath}
            };

            var expectedProductQueryStrings = new Dictionary<string, string>
            {
                {"pr1id", prodSku},
                {"pr1nm", prodName },
                {"pr1ca", uTagValues["ProductCategory"][0]},
                {"pr1pr", prodPrice.ToString()},
                {"pr1ps", pos.ToString()},
                {"pr1cd16", $"{uTagValues["TestId"][0]}" },
                {"pr1cd17", $"{uTagValues["MmId"][0]}"},
                {"pr1cd18", $"{uTagValues["FormulaId"][0]}"},
                {"pr1cd19", $"{uTagValues["PinId"][0]}"},
                {"pr1cd20", $"{uTagValues["TestStartDate"][0]}"},
                {"pr1cd25", $"{sortAbTestInfo[0]["TestCompositionId"]}"},
                {"pr1cd35", $"{sortAbTestInfo[0]["FilterId"]}"}
            };

            if (verboseLogs)
            {
                //print out expectedProductQueryStrings
                Log.Message("\nExpectedProductQueryStrings:");
                foreach (var kvp in expectedProductQueryStrings) Log.Message($"{kvp.Key}={kvp.Value}");
            }

            var numProdsInProductClick = NetworkLoggingUtility.GetNumberOfProductsInRequest("ea=product%20click", "pr");

            //Step 5
            Assert.True(NetworkLoggingUtility.RequestHasQueryParams("ea=product%20click", expectedGeneralPdpQueryStrings), "Product Click does not contain general query parameters.");
            //Step 6
            Assert.True(NetworkLoggingUtility.GetRequestQueryParamValueLogging("ea=product%20click", expectedProductQueryStrings, false, verboseLogs), "Product Click does not contain product parameters.");

            Assert.True(numProdsInProductClick == 1, "Number of Products in product click request is greater than 1");

            //PDP page view
            //get new Utag Data from PDP view
            var PDPuTagValues = GetAndFormatUtagData();

            var expectedPageviewGenericStrings = new Dictionary<string, string>
            {
                {"t", "pageview"},
                {"cd26", "Sort"},
                {"pal", relPath}
            };

            //this one encodes the product name when others do not. Need special encode function to handle TM and other special characters.
            var expectedPageviewProductQueryStrings = new Dictionary<string, string>
            {
                {"pr1id", prodSku},
                {"pr1nm", HtmlEncode(prodName)},
                {"pr1ca", PDPuTagValues["ProductCategory"][0]},
                {"pr1pr", prodPrice.ToString()},
                {"pr1ps", pos.ToString()},
                {"pr1cd16", $"{PDPuTagValues["TestId"][0]}" },
                {"pr1cd17", $"{PDPuTagValues["MmId"][0]}"},
                {"pr1cd18", $"{PDPuTagValues["FormulaId"][0]}"},
                {"pr1cd19", $"{PDPuTagValues["PinId"][0]}"},
                {"pr1cd20", $"{PDPuTagValues["TestStartDate"][0]}"},
                {"pr1cd25", $"{sortAbTestInfo[0]["TestCompositionId"]}"},
                {"pr1cd35", $"{sortAbTestInfo[0]["FilterId"]}"}
            };

            var numProdsInPageView = NetworkLoggingUtility.GetNumberOfProductsInRequest("t=pageview", "pr");

            //Step 7
            Assert.True(NetworkLoggingUtility.RequestHasQueryParams("t=pageview", expectedPageviewGenericStrings), "Pageview does not contain general parameters.");
            //Step 8
            Assert.True(NetworkLoggingUtility.RequestHasQueryParams("t=pageview", expectedPageviewProductQueryStrings), "Pageview does not contain product parameters.");

            Assert.True(numProdsInPageView == 1, "Number of Products in pageview request is greater than 1");
            Assert.True(GlobalLocators.AddToCartButton.IsInitialized, "Product does not have an Add To Cart button");

            //Step 9: add to cart
            AddQuantityValue("3");

            GlobalLocators.AddToCartButton.Click();

            Browser.Wait.IsVisibleElement(By.ClassName(CartOverview.CheckOutNowClass), 60);

            var expectedGeneralAddToCartQueryStrings = new Dictionary<string, string>
            {
                {"ec", "ecommerce"},
                {"ea", "add to cart"},
                {"cd26", "Sort"},
                {"pal", relPath}
            };

            //same product query strings as the pageview except it also has the quantity parameter.
            var expectedAtcProductQueryStrings = new Dictionary<string, string> { { "pr1qt", "3" } };

            foreach (var prodQueryString in expectedPageviewProductQueryStrings)
            {
                expectedAtcProductQueryStrings.Add(prodQueryString.Key, prodQueryString.Value);
            }

            Assert.True(NetworkLoggingUtility.RequestHasQueryParams("ea=add%20to%20cart", expectedGeneralAddToCartQueryStrings), "Add to Cart does not contain general query parameters.");
        }

        private Dictionary<string, string[]> GetAndFormatUtagData()
        {
            var uTagData = UtagData.ParseUtagData(Browser.PageSource);
            var json = JsonConvert.SerializeObject(uTagData);
            var uTagValues = (JObject)JsonConvert.DeserializeObject(json);
            var formatteduTagValues = new Dictionary<string, string[]>();
            foreach (var uTag in uTagValues)
            {
                //utag data comes with extra characters such as [] and \" and \r\n at the beginning and end
                var formattedValueArr = uTag.Value.ToString().Split(',');

                for (var i = 0; i < formattedValueArr.Count(); i++)
                {
                    //trim the brackets then the \r\n then the extra quotes
                    var formattedValue = formattedValueArr[i].Trim('[').Trim(']').Trim().Trim('"');
                    formattedValueArr[i] = formattedValue;
                }

                formatteduTagValues.Add(uTag.Key, formattedValueArr);
            }
            return formatteduTagValues;
        }

        private List<string> ExcludeFilters()
        {
            //we want to exclude these filters per test case
            string[] notTheseFilters =
            {
                "Type",
                "Usage",
                "Style",
                "Sale",
                "Finish",
                "Category",
                "Manufacturer",
                "Color"
            };

            return notTheseFilters.ToList();
        }

        private string HtmlEncode(string text)
        {
            char[] chars = HttpUtility.HtmlEncode(text).ToCharArray();
            StringBuilder result = new StringBuilder(text.Length + (int)(text.Length * 0.1));

            foreach (char c in chars)
            {
                int value = Convert.ToInt32(c);
                if (value > 127)
                    result.AppendFormat("&#{0};", value);
                else
                    result.Append(c);
            }

            return result.ToString();
        }

        protected abstract void AddQuantityValue(string qty);

        protected abstract int GetVisualProdsCount();

        protected abstract void CloseMobileSortMenu();
    }
}