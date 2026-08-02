using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Automation.Framework;
using Automation.Framework.Utilities;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Pages.Refactored.Cart;
using LampsPlus.AutomationFramework.Pages.Refactored.ProductDetail;
using LampsPlus.AutomationFramework.Pages.Refactored.Sort;
using LampsPlus.AutomationFramework.Utilities;

namespace LampsPlus.AutomationFramework.Workflow.Refactored.GoogleAnalyticsWorkflow
{
    public class GoogleAnalyticsWorkflowDesktop : IGoogleAnalyticsWorkflowDesktop
    {
        //Class members
        private void CompareGaData(string sortPath, List<Dictionary<string, string>> sortAbTestInfo, bool verboseLogs)
        {

            var uTagValues = GetAndFormatUtagData();

            var uTagFilterId = uTagValues["FilterId"][0];

            var currentTestInfo = sortAbTestInfo.ToList();

            var currentTestInfoFilterId = sortAbTestInfo[0]["FilterId"];

            _assert.Equals(currentTestInfoFilterId, uTagFilterId, $"Utag FilterId '{uTagFilterId}' does not match Filter Id '{currentTestInfoFilterId}' found in database");

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
                _assert.True(uTagDataIsCorrect, "Utag data does not match expected values");
            }

            Uri rel = new Uri(_browser.PageUrl);

            var relPath = rel.AbsolutePath;

            var expectedGeneralQueryStrings = new Dictionary<string, string>
            {
                {"ec", "ecommerce"},
                {"ea", "product impression"},
                {"cd26", "Sort"},
                {"il1nm", relPath}
            };

            var prodsPerRow = _sort.GetVisibleProductsCount();

            var expectedProdsQueryStrings = new Dictionary<string, string>();

            for (var i = 1; i <= prodsPerRow; i++)
            {
                var skuValue = _sort.GetDisplayedProductAttribute(i - 1, "data-sku");
                var priceValue = _sort.GetDisplayedProductAttribute(i - 1, "data-price");
                var nameValue = _sort.GetProductNameBySku(skuValue);

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
                _log.Message("\nExpectedProdsQueryStrings:");
                foreach (var kvp in expectedProdsQueryStrings) _log.Message($"{kvp.Key}={kvp.Value}");
            }

            //Step 4
            _assert.True(_networkLoggingUtility.GetRequestQueryParamValueLogging("ea=product%20impression", expectedProdsQueryStrings, false, verboseLogs), "Product Impression is not sending expected product information.");
            //Step 3
            var requestProds = _networkLoggingUtility.GetNumberOfProductsInRequest("ea=product%20impression", "il1pi");
            _log.Message($"Number of requestProds: {requestProds}");
            _assert.True(requestProds == prodsPerRow, "Number of Products in request do not match the number of products per row");
            //Step 1
            _assert.True(_networkLoggingUtility.RequestHasQueryParams("ea=product%20impression", expectedGeneralQueryStrings), "Product Impression does not contain general query parameters.");

            _networkLoggingUtility.ClearNetworkLog();

            //select product
            var pos = _sort.GetProductNearOrOverTwoHundredDollarsPosition();
            _assert.True(_productDetail.IsCurrentPage,"Current page is not PDP page");

            var prodSku = _productDetail.GetProductSku();
            var prodPrice = _productDetail.GetProductPrice();
            var prodName = _productDetail.GetProductName();

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
                _log.Message("\nExpectedProductQueryStrings:");
                foreach (var kvp in expectedProductQueryStrings) _log.Message($"{kvp.Key}={kvp.Value}");
            }

            var numProdsInProductClick = _networkLoggingUtility.GetNumberOfProductsInRequest("ea=product%20click", "pr");

            //Step 5
            _assert.True(_networkLoggingUtility.RequestHasQueryParams("ea=product%20click", expectedGeneralPdpQueryStrings), "Product Click does not contain general query parameters.");
            //Step 6
            _assert.True(_networkLoggingUtility.GetRequestQueryParamValueLogging("ea=product%20click", expectedProductQueryStrings, false, verboseLogs), "Product Click does not contain product parameters.");

            _assert.True(numProdsInProductClick == 1, "Number of Products in product click request is greater than 1");

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

            var numProdsInPageView = _networkLoggingUtility.GetNumberOfProductsInRequest("t=pageview", "pr");

            //Step 7
            _assert.True(_networkLoggingUtility.RequestHasQueryParams("t=pageview", expectedPageviewGenericStrings), "Pageview does not contain general parameters.");
            //Step 8
            _assert.True(_networkLoggingUtility.RequestHasQueryParams("t=pageview", expectedPageviewProductQueryStrings), "Pageview does not contain product parameters.");

            _assert.True(numProdsInPageView == 1, "Number of Products in pageview request is greater than 1");

            //Step 9: add to cart
            _productDetail.ChangeProductQuantity("3");
            _productDetail.AddToCart();
            _assert.True(_cart.IsCurrentPage,"Current page is not a Cart page");

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

            _assert.True(_networkLoggingUtility.RequestHasQueryParams("ea=add%20to%20cart", expectedGeneralAddToCartQueryStrings), "Add to Cart does not contain general query parameters.");
        }


        private List<string> ExcludeFilters()
        {
            //we want to exclude these filters per test case
            string[] notTheseFilters =
            {
                "Type",
                "Usage",
                "Sale",
                "Finish",
                "Category",
                "Specials"
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

        public GoogleAnalyticsWorkflowDesktop(IBrowser browser, Log log, ISortDesktop sort, NetworkLoggingUtility networkLoggingUtility, IAssert assert, IProductDetailDesktop productDetail, ICartDesktop cart)
        {
            _browser = browser;
            _log = log;
            _sort = sort;
            _networkLoggingUtility = networkLoggingUtility;
            _assert = assert;
            _productDetail = productDetail;
            _cart = cart;
        }

        //Desktop POM and Workflow instances
        private readonly ISortDesktop _sort;
        private readonly IProductDetailDesktop _productDetail;
        private readonly ICartDesktop _cart;

        //TestsBase instances
        private readonly IBrowser _browser;
        private readonly IAssert _assert;
        private readonly Log _log;
        private readonly NetworkLoggingUtility _networkLoggingUtility;

        //Interface implementation
        public Dictionary<string, string[]> GetAndFormatUtagData()
        {
            var uTagData = UtagData.ParseUtagData(_browser.PageSource);
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

        public void ValidateAbTestGaData(List<Dictionary<string, string>> sortAbTestInfo, string sortPath, int reps)
        {
            for (var i = 0; i < reps; i++)
            {
                //Sort page product impressions
                _networkLoggingUtility.ClearNetworkLog();

                _browser.Navigate(sortPath);
                _browser.Wait.ForDomReady();
                _networkLoggingUtility.ClearNetworkLog();

                _sort.ApplyFilters(1, filtersToExclude: ExcludeFilters());
                 
                _browser.Wait.ForDomReady();

                //set up complete, compare utag data to database and GA data in network logs to utag data. 
                CompareGaData(sortPath, sortAbTestInfo, true);
            }
        }
    }
}
