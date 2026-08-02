using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using LampsPlus.Automation.Framework;

using LampsPlus.Automation.Tests.Constants;
using LampsPlus.Automation.Tests.Databases.Actions;

using OpenQA.Selenium;

namespace LampsPlus.Automation.Tests.Utilities
{
    /// <summary>
    /// Certona helper methods
    /// </summary>
    public class CertonaUtilities
    {
        private List<string> _allCoordinatingSkuIds;
        private string _certonaWidgetContainerClass = ".suggestedProductsContainer";
        private string _dataCertonaSku = "data-certonasku";
        private string _dataScheme = "data-scheme";
        private string _dataSku = "data-sku";
        private string _dataQaSkuSource = "data-qa-sku-source";
        private bool _isUsingExternalReferrer;
        private string[] _pageSchemesCache;
        private string _relatedItemsId = "#related-items";
        private XmlDocument _xmlResponseCache;

        private string[] PageSchemes
        {
            get
            {
                TestBase.Verify.NotNull(_pageSchemesCache, "Schemes not set. Need to run 'await CertonaUtilities.StoreCertonaData(...)' after visiting multiple pages.");
                return _pageSchemesCache;
            }
            set => _pageSchemesCache = value;
        }

        private XmlDocument XmlResponse
        {
            get
            {
                TestBase.Verify.NotNull(_xmlResponseCache, "XML Response empty. Need to run 'await CertonaUtilities.StoreCertonaData(...)' after visiting multiple pages.");

                return _xmlResponseCache;
            }
            set => _xmlResponseCache = value;
        }

        private string CurrentPageSku => SkuList.Last();
        private bool OnItemPage => (bool)Browser.ExecuteJs("return Boolean(window.resx.RecommendedItemsRequest.Criteria.ShortSkuList.length)");
        private IWebElement PdpLinkOnGoogle => Browser.Locate.Element("a[href^='https://www.lampsplus.com/products/']");
        private IWebElement PlaFrameElement => Browser.Locate.Element($"{Locators.Sort.SfpQuickLookId} {Page.IframeTagString}");
        private string SkuContext => $"&context={string.Join(";", SkuList)}";
        private IEnumerable<string> SkuList =>
            ((IEnumerable)Browser.ExecuteJs("return window.resx.RecommendedItemsRequest.Criteria.ShortSkuList")).Cast<object>().ToList().Select(s => (string)s).ToList();
        private List<string> RelatedItemSkus =>
            Browser.Locate.Elements(Locators.ProductDetail.PdRelItmsProdClass)
                .Select(x => x.GetAttribute(TestBase.SignInWorkflow.IsLoggedInAsCustomerService ? _dataSku : _dataQaSkuSource)).ToList();

        internal TestsBase TestBase;
        internal IBrowser Browser => TestBase.Browser;

        /// <summary>
        /// Certona helper methods
        /// </summary>
        public CertonaUtilities(TestsBase testsBase) { TestBase = testsBase; }

        /// <summary>
        /// Visits multiple random pdps to populate the Certona Recently viewed widget.
        /// </summary>
        /// <param name="numberOfPages">Number of pages to visit. (Default: 4)</param>
        public void VisitMultiplePages(int numberOfPages = 4)
        {
            var randomSkus = ProductActions.GetListableInStockShortSku(numberOfPages);
            TestBase.ConditionalVerify.True(randomSkus.Any(), "ProductActions.GetListableInStockShortSku()");

            foreach (var sku in randomSkus)
            {
                Browser.NavigateToPdp(sku);
            }
        }

        /// <summary>
        /// Verifies if schemes are included in the response from Certona.
        /// </summary>
        public void VerifySchemesExistInResponse()
        {
            foreach (var scheme in PageSchemes)
            {
                TestBase.SoftVerify.StringContains(XmlResponse.InnerXml, scheme, $"{scheme} doesn't appear in Certona result.");
            }
        }

        /// <summary>
        /// Verifies page widget titles match their respective boxtitles in the Certona response.
        /// </summary>
        /// <param name="schemes">Which schemes' respective titles to verify. If no schemes are passed, all are verified.</param>
        public void VerifyTitlesMatchResponse(params string[] schemes)
        {
            // Check all schemes' respective titles if none are supplied
            if (schemes.Length == 0) { schemes = PageSchemes; }

            foreach (var scheme in schemes)
            {
                // <boxtitle> isn't nested inside the scheme element in the xml, so we get the following sibling boxtitle after the scheme element
                var currentBoxTitle =
                    XmlResponse.SelectSingleNode(
                        $"resonance/items[@scheme='{scheme}']/following-sibling::boxtitle");
                // ReSharper disable once PossibleNullReferenceException
                var expectedWidgetTitle = currentBoxTitle.InnerText.ToLower();

                // If boxtitle in xml is empty, it means certona didn't return any results for the specific sku and scheme, so iteration should be skipped
                // ReSharper disable once InvertIf
                if (expectedWidgetTitle != string.Empty)
                {
                    var widgetTitle = Browser.Wait.ForElement(Browser.Locate
                        .Element($"[{_dataScheme}='{scheme}'] h2, [{_dataScheme}='{scheme}'] .jsCertonaTitle"), 5).Text.ToLower();

                    // Widget titles are equal to their respective <boxtitle> in the certona response
                    TestBase.SoftVerify.Equals(expectedWidgetTitle, widgetTitle,
                        $"Certona widget title \"{widgetTitle}\" on page is not equal to {expectedWidgetTitle}.");
                }
            }
        }

        /// <summary>
        /// Verifies all Certona skus on the page appear in the response, or are coordinating items.
        /// </summary>
        public void VerifySkusMatchResponse()
        {
            var allPageSkuIds = Browser.Locate.Elements($"[{_dataScheme}][{_dataCertonaSku}]").Select(x => x.GetAttribute(_dataCertonaSku));
            var allSkuDataFromXml =
                XmlResponse.SelectNodes("resonance/items/item/id");
            // ReSharper disable once AssignNullToNotNullAttribute
            var allSkuIds = allSkuDataFromXml.Cast<XmlNode>()
                .Select(node => node.InnerText)
                .ToList();

            if (OnItemPage)
            {
                if (_allCoordinatingSkuIds == null) { _allCoordinatingSkuIds = ProductActions.GetInStockCoordinatingItems(CurrentPageSku); }
                allSkuIds.AddRange(_allCoordinatingSkuIds);
            }  

            // Checks if skus on page appear either in coordinating items or in certona's returned results
            foreach (var sku in allPageSkuIds)
            {
                TestBase.SoftVerify.True(
                    allSkuIds.Contains(sku),
                    $"Sku {sku} on page not found in Certona returned data, and is not a coordinating item.");
            }
        }

        /// <summary>
        /// Verifies our Certona requests are made with the proper Min, Max, and Requested quantities, and also that it's properly reflected on the page.
        /// </summary>
        public void VerifySkuAmountsMatchDefinedQuantities()
        {
            foreach (var scheme in PageSchemes)
            {
                var schemeQuantities = GetSchemeQuantities(scheme);
                var shemeSkuElements = Browser.Locate.Elements($"[{_dataScheme}='{scheme}'][{_dataCertonaSku}]");
                var widgetCriteriaJs = $"return window.resx.RecommendedItemsRequest.WidgetCriteria.filter(function(current){{return current.Scheme === '{scheme}'}})[0]";
                var minResult = Convert.ToInt32(Browser.ExecuteJs($"{widgetCriteriaJs}.MinimumResult"));
                var maxResult = Convert.ToInt32(Browser.ExecuteJs($"{widgetCriteriaJs}.MaximumResult"));
                var requestResults = Convert.ToInt32(Browser.ExecuteJs($"{widgetCriteriaJs}.NumberOfRequestResult"));
                var certonaMinimumResult = schemeQuantities.Minimum;
                var certonaMaximumResult = schemeQuantities.Maximum;
                var certonaNumberOfRequestResult = schemeQuantities.Requested;

                TestBase.SoftVerify.Equals(certonaMinimumResult, minResult, $"{minResult} does not equal {certonaMinimumResult}.");
                TestBase.SoftVerify.Equals(certonaMaximumResult, maxResult, $"{maxResult} does not equal {certonaMaximumResult}.");
                TestBase.SoftVerify.Equals(certonaNumberOfRequestResult, requestResults, $"{requestResults} does not equal {certonaNumberOfRequestResult}.");
                TestBase.SoftVerify.True(shemeSkuElements.Count >= minResult && shemeSkuElements.Count <= maxResult, $"The quantity of Certona items in the {scheme} widget ({shemeSkuElements.Count}) is out of the bounds of {minResult} and {maxResult}.");
            }
        }

        /// <summary>
        /// Verifies Certona calls are disabled on the page and widgets don't appear for employees.
        /// </summary>
        public void VerifyCertonaIsDisabledForEmployee()
        {
            if (!TestBase.SignInWorkflow.IsLoggedInAsCustomerService)
            {
                var currentUrlOrSku = _isUsingExternalReferrer ? CurrentPageSku : Browser.PageUrl;
                TestBase.SignInWorkflow.LoginFromSignInPage(LoginType.CustomerServiceRegularLoginAccount);

                if (_isUsingExternalReferrer) { OpenPdpWithExternalReferrer(currentUrlOrSku); }
                else { Browser.Navigate(currentUrlOrSku); }
            }

            // Valid check because when this bool is false, there is never a scheme name in the .RecommendedItemsRequest.WidgetCriteria Array
            var isCertonaDisabled = (bool)Browser.ExecuteJs("return window.resx.RecommendedItemsRequest.DisableCertona");

            TestBase.SoftVerify.True(isCertonaDisabled, "Certona calls are not disabled while employee is signed in.");

            VerifyCertonaElementsAreNotDisplayedOnPage();

            if (Browser.Locate.DoesElementExistImmediatly(PlaFrameElement))
            {
                Browser.SwitchFocusToIframe(PlaFrameElement);
                VerifyCertonaElementsAreNotDisplayedOnPage();
                Browser.SwitchToDefaultContent();
            }
        }

        /// <summary>
        /// Opens PDP page through google search results.
        /// </summary>
        /// <param name="sku">Desired sku pdp to open.</param>
        public void OpenPdpWithExternalReferrer(string sku)
        {
            Browser.Navigate($"https://www.google.com/search?q=%22Style+%23+{sku}%22+site%3Alampsplus.com");
            PdpLinkOnGoogle.Click();
            Browser.Wait.ForDomReady();
            _isUsingExternalReferrer = true;
        }

        /// <summary>
        /// Stores XML Response of Certona API call for use in other Certona methods.
        /// </summary>
        /// <param name="schemes">Store array of schemes for test use. These schemes are also used to build and call the url for the Certona api.</param>
        /// <returns></returns>
        public async Task StoreCertonaData(params string[] schemes)
        {
            PageSchemes = schemes;
            XmlResponse = await RequestApi.GetXml(BuildApiUrl(schemes));
        }

        /// <summary>
        /// Verifies passed sku appears last in the specified schemes widget.
        /// </summary>
        /// <param name="sku">The sku that will be used to check if it appears last in the specified schemes widget.</param>
        public void VerifySkuAppearsLastInRelatedItems(string sku)
        {
            var lastSkuInWidget = RelatedItemSkus.Last();
            TestBase.Verify.True(lastSkuInWidget == sku, $"On {CurrentPageSku} PDP, the last sku {lastSkuInWidget} in {CertonaSchemes.Related} widget is not equal to last coordinating item sku {sku}.");
        }

        /// <summary>
        /// Verifies unique Certona skus (that aren't also coordinating items from db) don't appear in the related items widget.
        /// </summary>
        /// <param name="coordinatingItemsFromDb">String list of Coordinating items from Database.</param>
        public void VerifyUniqueCertonaSkusDontAppearInRelatedItems(List<string> coordinatingItemsFromDb)
        {
            var dedupedCertonaSkus = GetDedupedCertonaSkus(coordinatingItemsFromDb);
            var uniqueCertonaSkusInWidget = RelatedItemSkus.Intersect(dedupedCertonaSkus).ToList();

            // Verify unique certona skus don't appear in the widget
            TestBase.Verify.True(!uniqueCertonaSkusInWidget.Any(), $"Certona Sku appears in widget. Skus: {string.Join(", ", uniqueCertonaSkusInWidget)}");
        }

        /// <summary>
        /// Verifies Certona Skus in related items widget are placed after all the coordinating items from the database.
        /// </summary>
        /// <param name="coordinatingItemsFromDb">String list of Coordinating items from Database.</param>
        public void VerifyUniqueCertonaSkusAppearAfterLastCoordinatingItemInRelatedItems(List<string> coordinatingItemsFromDb)
        {
            var dedupedCertonaSkus = GetDedupedCertonaSkus(coordinatingItemsFromDb);
            var widgetSkus = RelatedItemSkus;
            var lastCoordinatingItemIndex = widgetSkus.IndexOf(coordinatingItemsFromDb.Last());

            // If not only coordinating items in the widget
            if (widgetSkus.Count != coordinatingItemsFromDb.Count)
            {
                // Trim the coordinating skus from the begining of the widgetSkus list
                widgetSkus.RemoveRange(0, lastCoordinatingItemIndex + 1);

                TestBase.Verify.True(widgetSkus.Count <= 4, "There are more than 4 certona items displayed in the widget");

                // All of the trimmed widget skus should be found in the deduped certona results
                TestBase.Verify.True(dedupedCertonaSkus.Intersect(widgetSkus).Count() == widgetSkus.Count, "There should only be Certona items listed after the last coordinating item in the widget.");
            }
            else
            {
                TestBase.Verify.True(widgetSkus.Last() == coordinatingItemsFromDb.Last(), @"Since no Certona items populated the page, expected the last sku in the widget 
                                                                                          to be equal to the last sku of the database coordinating items.");
            }     
        }

        /// <summary>
        /// Returns the SchemeQuantities object for a specific scheme.
        /// </summary>
        /// <param name="scheme">Certona Scheme from CertonaSchemes class.</param>
        /// <returns>SchemeQuantities object</returns>
        private SchemeQuantities GetSchemeQuantities(string scheme)
        {
            SchemeQuantities schemeQuantities = null;
            var pageUrl = Browser.PageUrl;

            switch (scheme)
            {
                case CertonaSchemes.Cart:
                case CertonaSchemes.CategoryLanding:
                    schemeQuantities = new SchemeQuantities { Minimum = 3, Maximum = 4, Requested = 9 };
                    break;

                case CertonaSchemes.GlobalFooterDefault:
                case CertonaSchemes.GlobalFooterHomepage:
                case CertonaSchemes.GlobalFooterNoSearch:
                case CertonaSchemes.GlobalFooterProduct:
                case CertonaSchemes.GlobalFooterSortpage:
                case CertonaSchemes.GlobalFooterCategoryLanding:
                    schemeQuantities = new SchemeQuantities { Minimum = 1, Maximum = 30, Requested = 30 };
                    break;

                case CertonaSchemes.Home:
                    if (pageUrl == Urls.HomePageUrl)
                    {
                        schemeQuantities = new SchemeQuantities { Minimum = 3, Maximum = 5, Requested = 9 };
                    }
                    else if (pageUrl == Urls.InstallationPageUrl)
                    {
                        schemeQuantities = new SchemeQuantities { Minimum = 3, Maximum = 4, Requested = 9 };
                    }
                    break;

                case CertonaSchemes.MoreLikeThis:
                    schemeQuantities = new SchemeQuantities { Minimum = 1, Maximum = 11, Requested = 15 };
                    break;

                case CertonaSchemes.NoSearch:
                case CertonaSchemes.NoSearchSku:
                    schemeQuantities = new SchemeQuantities { Minimum = 3, Maximum = 9, Requested = 12 };
                    break;

                case CertonaSchemes.Product:
                    schemeQuantities = new SchemeQuantities { Minimum = 2, Maximum = 5, Requested = 9 };
                    break;

                case CertonaSchemes.Related:
                    schemeQuantities = new SchemeQuantities { Minimum = 0, Maximum = 10, Requested = 12 };
                    break;

                case CertonaSchemes.Similar:
                    schemeQuantities = new SchemeQuantities { Minimum = 3, Maximum = 8, Requested = 15 };
                    break;

                case CertonaSchemes.SimilarFullpage:
                    if (pageUrl.Contains("sfp3"))
                    {
                        schemeQuantities = new SchemeQuantities { Minimum = 3, Maximum = 9, Requested = 15 };
                    }
                    else if (pageUrl.Contains("sfp"))
                    {
                        schemeQuantities = new SchemeQuantities { Minimum = 1, Maximum = 90, Requested = 90 };
                    }
                    break;

                case CertonaSchemes.Sortpage:
                    schemeQuantities = new SchemeQuantities { Minimum = 3, Maximum = 4, Requested = 12 };
                    break;

                case CertonaSchemes.Wishlist:
                    schemeQuantities = new SchemeQuantities { Minimum = 3, Maximum = 8, Requested = 9 };
                    break;
            }

            return schemeQuantities;
        }

        private CertonaIds GetCertonaIds()
        {
            const string recommendedItemsRequestJs = "return window.resx.RecommendedItemsRequest";

            return new CertonaIds
            {
                TrackingId = Browser.ExecuteJs($"{recommendedItemsRequestJs}.TrackingId").ToString(),
                SessionId = Browser.ExecuteJs($"{recommendedItemsRequestJs}.SessionId").ToString()
            };
        }

        private bool ShouldIncludeSkuContext() { return CertonaSchemes.SchemesThatNeedUrlContextParameter.Intersect(PageSchemes).Any(); }
        private bool ShouldIncludeSortCriteria() { return CertonaSchemes.SchemesThatNeedUrlSortCriteriaParameters.Intersect(PageSchemes).Any(); }

        private string GetSortCriteria()
        {
            const string criteriaJsCode = "return resx.RecommendedItemsRequest.Criteria";
            var urlCriteria = new StringBuilder();

            var category = (string)Browser.ExecuteJs($"{criteriaJsCode}.Category");
            var finish = (string)Browser.ExecuteJs($"{criteriaJsCode}.Finish");
            var color = (string)Browser.ExecuteJs($"{criteriaJsCode}.Color");
            var style = (string)Browser.ExecuteJs($"{criteriaJsCode}.Style");
            var type = (string)Browser.ExecuteJs($"{criteriaJsCode}.Type");
            var criteria = new Dictionary<string, string> { { "category", category }, { "finish", finish }, { "color", color }, { "style", style }, { "type", type } };

			foreach (var entry in criteria)
			{
				if (entry.Value != string.Empty) { urlCriteria.Append($"&{entry.Key}={entry.Value}"); }
			}

			return Uri.EscapeUriString(urlCriteria.ToString());
        }

        private string BuildApiUrl(string[] schemes)
        {
            var certonaIds = GetCertonaIds();
            var joinedSchemes = string.Join(";", schemes);
            var joinedSchemeQuantities = string.Join(";", schemes.Select(scheme => GetSchemeQuantities(scheme).Requested.ToString()).ToList());
            var url = new StringBuilder();

            url.Append("http://www.res-x.com/ws/r2/resonance.aspx?appid=lampsplus01");
            url.Append($"&trackingid={certonaIds.TrackingId}&sessionid={certonaIds.SessionId}");
            url.Append($"&scheme={joinedSchemes}");
            url.Append($"&number={joinedSchemeQuantities}");
            if (ShouldIncludeSkuContext()) { url.Append(SkuContext); }
            if (ShouldIncludeSortCriteria()) { url.Append(GetSortCriteria()); }

            return url.ToString();
        }

        private void VerifyCertonaElementsAreNotDisplayedOnPage()
        {
            var nonRelatedItemsCertonaWidgets = $"{_certonaWidgetContainerClass}:not({_relatedItemsId})";

            foreach (var titleElement in Browser.Locate.Elements($"{nonRelatedItemsCertonaWidgets} h2, {nonRelatedItemsCertonaWidgets} .jsCertonaTitle"))
            {
                // Checking title because some widget containers appear on page even when signed in as employee
                TestBase.SoftVerify.True(titleElement.Text == string.Empty, $"Scheme {titleElement.GetAttribute(_dataScheme)} widget appears on page while employee is signed in.");
            }

            // Checking for Certona skus
            foreach (var element in Browser.Locate.Elements($"[{_dataCertonaSku}]"))
            {
                TestBase.SoftVerify.NotDisplayed(element, $"Sku {element.GetAttribute(_dataCertonaSku)} appears in widget while employee is signed in.");
            }
        }

        /// <summary>
        /// Dedupe Certona schemes with coordinating Items from DB
        /// </summary>
        /// <param name="coordinatingItemsFromDb">List of coordinating items from db</param>
        /// <returns></returns>
        private IEnumerable<string> GetDedupedCertonaSkus(ICollection<string> coordinatingItemsFromDb)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            return XmlResponse.SelectNodes($"resonance/items[@scheme='{CertonaSchemes.Related}']/item/id").Cast<XmlNode>()
                .Select(node => node.InnerText)
                .Where(x => !coordinatingItemsFromDb.Contains(x));
        }


        /// <summary>
        /// Scheme Min, Max, and Requested quantity values.
        /// </summary>
        private class SchemeQuantities
        {
            /// <summary>
            /// Minimum number of Certona items to show on page.
            /// </summary>
            public int Minimum { get; set; }

            /// <summary>
            /// Maximum number of Certona items to show on page.
            /// </summary>
            public int Maximum { get; set; }

            /// <summary>
            /// Number of Certona recommended items requested from api.
            /// </summary>
            public int Requested { get; set; }
        }


        private class CertonaIds
        {
            public string TrackingId { get; set; }
            public string SessionId { get; set; }
        }
    }
}
