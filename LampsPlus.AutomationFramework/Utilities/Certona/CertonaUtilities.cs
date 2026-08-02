using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Xml;
using System.Xml.Linq;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Pages.Desktop;
using LampsPlus.AutomationFramework.Workflow.Desktop;
using OpenQA.Selenium;

namespace LampsPlus.AutomationFramework.Utilities.Certona
{
    /// <summary>
    /// Certona helper methods
    /// </summary>
    public class CertonaUtilities
    {
        private List<string> _allCoordinatingSkuIds;
		private const string _suggestedProductsContainerClass = "suggestedProductsContainer";
		private const string _dataCertonaSku = "data-certonasku";
        private const string _dataScheme = "data-scheme";
        private const string _dataSku = "data-sku";
        private const string _dataQaSkuSource = "data-qa-sku-source";
        private bool _isUsingExternalReferrer;
        private string[] _pageSchemesCache;
        private const string _relatedItemsId = "related-items";
		private const string _jsCertonaTitleClass = "jsCertonaTitle";
		private XmlDocument _xmlResponseCache;
        private IElement _h1TitleBelowLpContainer;

        private string[] PageSchemes
        {
            get
            {
                Framework.Assert.NotNull(_pageSchemesCache, "Schemes not set. Need to run 'CertonaUtilities.StoreCertonaData(...)' after visiting multiple pages.");
                return _pageSchemesCache;
            }
            set => _pageSchemesCache = value;
        }

        private XmlDocument XmlResponse
        {
            get
            {
                Framework.Assert.NotNull(_xmlResponseCache, "XML Response empty. Need to run 'CertonaUtilities.StoreCertonaData(...)' after visiting multiple pages.");

                return _xmlResponseCache;
            }
            set => _xmlResponseCache = value;
        }

        private string CurrentPageSku => SkuList.Last();
        private bool OnItemPage => (bool)Browser.ExecuteJs("return Boolean(window.resx.RecommendedItemsRequest.Criteria.ShortSkuList.length)");
        private IElement PdpLinkOnGoogle => Browser.Locate.ElementByTagNameAndAttributeStartsWith(HtmlTextWriterTag.A, HtmlTextWriterAttribute.Href, Urls.LampsPlusProductsUrl);
        public IElement PlaFrameElement => Browser.Locate.ElementById(Framework.SortPla.SfpQuickLookId);
        private string SkuContext => $"&context={string.Join(";", SkuList)}";
        private IEnumerable<string> SkuList => ((IEnumerable)Browser.ExecuteJs("return window.resx.RecommendedItemsRequest.Criteria.ShortSkuList")).Cast<object>().ToList().Select(s => (string)s).ToList();
        private List<string> RelatedItemSkus => Framework.ProductDetail.RelatedItems.Select(x => x.GetAttribute(Framework.SignInWorkflow.IsLoggedInAsCustomerService || Framework.SignInWorkflow.IsLoggedInAsKiosk || Framework.Settings.IsMobileView ? _dataSku : _dataQaSkuSource)).ToList();

        internal TestsBase Framework; // Circular reference?
        internal IBrowser Browser => Framework.Browser;

        /// <summary>
        /// Certona helper methods
        /// </summary>
        public CertonaUtilities(TestsBase testsBase)
        {
            Framework = testsBase;            
        }

        /// <summary>
        /// Visits multiple random pdps to populate the Certona Recently viewed widget.
        /// </summary>
        /// <param name="numberOfPages">Number of pages to visit. (Default: 4)</param>
        public void VisitMultiplePages(int numberOfPages = 4)
        {
            var randomSkus = Framework.ProductActions.GetListableInStockShortSku(numberOfPages);
            Framework.Assert.True(randomSkus.Any(), "ProductActions.GetListableInStockShortSku(numberOfPages)");

            foreach (var sku in randomSkus)
            {
                Framework.ProductDetail.NavigateToProductDetailByShortSku(sku);
                Browser.Wait.ForDomReady();
            }
        }

        /// <summary>
        /// Verifies if schemes are included in the response from Certona.
        /// </summary>
        public void VerifySchemesExistInResponse()
        {
            foreach (var scheme in PageSchemes)
            {
                Framework.Assert.StringContains(XmlResponse.InnerXml, scheme, $"{scheme} doesn't appear in Certona result.");
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
                var currentBoxTitle = XmlResponse.SelectSingleNode($"resonance/items[@scheme='{scheme}']/following-sibling::boxtitle");
                // ReSharper disable once PossibleNullReferenceException
                var expectedWidgetTitle = currentBoxTitle.InnerText.ToLower();

                // If boxtitle in xml is empty, it means certona didn't return any results for the specific sku and scheme, so iteration should be skipped
                // ReSharper disable once InvertIf
                if (expectedWidgetTitle != string.Empty)
                {
                    var widgetTitle = (Browser.PageUrl == Urls.RecentlyViewedUrl ?
                            _h1TitleBelowLpContainer ??
                            Browser.Wait.ForElement(Browser.Locate.ElementByTagName(HtmlTextWriterTag.H1)) :
                            WaitForTitleElement(scheme))
                        .Text.ToLower();

                    // Widget titles are equal to their respective <boxtitle> in the certona response
                    Framework.Assert.Equals(expectedWidgetTitle, widgetTitle, $"Certona widget title \"{widgetTitle}\" that appears on the page is not equal to the \"{expectedWidgetTitle}\" title returned from Certona API.");
                }
            }
        }

		private IElement WaitForTitleElement(string scheme)
		{
			var schemeElement = Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.A, _dataScheme, scheme);
            var closestContainer = Browser.Locate.AncestorElementBySelector(schemeElement, "div.certonaWidgetContainer, div[data-scheme]");
            var titleElement = (Browser.Locate.ElementsByTagName(HtmlTextWriterTag.H2, closestContainer).Concat(Browser.Locate.ElementsByClassName(_jsCertonaTitleClass, closestContainer))).First();
			return titleElement;
		}

        /// <summary>
        /// Verifies all Certona skus on the page appear in the response, or are coordinating items.
        /// </summary>
        public void VerifySkusMatchResponse()
        {
            var allPageSkuIds = Browser.Locate.ElementsByMultipleAttributeNames(_dataScheme, _dataCertonaSku).Select(x => x.GetAttribute(_dataCertonaSku)).ToList();
            var allSkuDataFromXml = XmlResponse.SelectNodes("resonance/items/item/id");

            // ReSharper disable once AssignNullToNotNullAttribute
            var allSkuIds = allSkuDataFromXml.Cast<XmlNode>().Select(node => node.InnerText).ToList();

            if (OnItemPage)
            {
                if (_allCoordinatingSkuIds == null) { _allCoordinatingSkuIds = Framework.ProductActions.GetInStockCoordinatingItems(CurrentPageSku); }
                allSkuIds.AddRange(_allCoordinatingSkuIds);
            }  

            // Checks if skus on page appear either in coordinating items or in certona's returned results
            foreach (var sku in allPageSkuIds)
            {
                Framework.Assert.True(allSkuIds.Contains(sku), $"Sku {sku} on page not found in Certona returned data, and is not a coordinating item.");
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
				var attributes = new[] { new KeyValuePair<string, string>(_dataScheme, scheme), new KeyValuePair<string, string>(_dataCertonaSku, string.Empty) };
                var shemeSkuElements = Browser.Locate.ElementsByMultipleAttributesEquals(attributes);
                var widgetCriteriaJs = $"return window.resx.RecommendedItemsRequest.WidgetCriteria.filter(function(current){{return current.Scheme === '{scheme}'}})[0]";
                var minResult = Convert.ToInt32(Browser.ExecuteJs($"{widgetCriteriaJs}.MinimumResult"));
                var maxResult = Convert.ToInt32(Browser.ExecuteJs($"{widgetCriteriaJs}.MaximumResult"));
                var requestResults = Convert.ToInt32(Browser.ExecuteJs($"{widgetCriteriaJs}.NumberOfRequestResult"));
                var certonaMinimumResult = schemeQuantities.Minimum;
                var certonaMaximumResult = schemeQuantities.Maximum;
                var certonaNumberOfRequestResult = schemeQuantities.Requested;

                Framework.Assert.Equals(certonaMinimumResult, minResult, $"Minimum Result {minResult} on page does not equal {certonaMinimumResult} in XML for {scheme}.");
                Framework.Assert.Equals(certonaMaximumResult, maxResult, $"Maximum Result {maxResult} on page does not equal {certonaMaximumResult} in XML for {scheme}.");
                Framework.Assert.Equals(certonaNumberOfRequestResult, requestResults, $"Quantity Requested {requestResults} on page does not equal {certonaNumberOfRequestResult} in XML for {scheme}.");
                // ignore certona min/max values if we are displaying coordinating items.
                if (scheme == CertonaSchemes.Related && _allCoordinatingSkuIds.Count < 11 || scheme != CertonaSchemes.Related)
                {
                    Framework.Assert.InRange(shemeSkuElements.Count, minResult, maxResult, $"The quantity of Certona items in the {scheme} widget ({shemeSkuElements.Count}) is out of the bounds of {minResult} and {maxResult}.");
                }
            }
        }

        /// <summary>
        /// Verifies Certona calls are disabled on the page and widgets don't appear for employees.
        /// </summary>
        public void VerifyCertonaIsDisabledForEmployee()
        {
            if (Framework.Settings.IsMobileView)
            {
                return;
            }
            if (!Framework.SignInWorkflow.IsLoggedInAsCustomerService)
            {
                var currentUrlOrSku = _isUsingExternalReferrer ? CurrentPageSku : Browser.PageUrl;

                if (Framework.SignInWorkflow.IsLoggedInUser)
                {
                    Framework.SignInWorkflow.SignOut();
                }

                Framework.SignInWorkflow.SignIn(LampsPlusAccounts.CustomerServiceRegularLoginAccount);

                Framework.Home.EnterStoreInSession("0");

                if (currentUrlOrSku == Urls.OrderConfirmationPageUrl && PageSchemes.Contains(CertonaSchemes.OrderConfirmation))
                {
                    SubmittingOrdersWorkflow ordersWorkflow = new SubmittingOrdersWorkflow(Framework);
                    ordersWorkflow.EmployeePlacesOrderForSearchedSkuWithPoPayment();
                    Browser.Wait.ForPage(Urls.OrderConfirmationPageUrl);
                }

                if (_isUsingExternalReferrer) { OpenPdpWithExternalReferrer(currentUrlOrSku); }
                else { Browser.Navigate(currentUrlOrSku); }
            }

            // Valid check because when this bool is false, there is never a scheme name in the .RecommendedItemsRequest.WidgetCriteria Array
            var isCertonaDisabled = (bool)Browser.ExecuteJs("return window.resx.RecommendedItemsRequest.DisableCertona");

            Framework.Assert.True(isCertonaDisabled, "Certona calls are not disabled while employee is signed in.");

            VerifyCertonaElementsAreNotDisplayedOnPage();

            if (Browser.Locate.DoesElementExistImmediately($"{Framework.SortPla.SfpQuickLookId.ToCssIdSelector()} {HtmlTextWriterTag.Iframe}"))
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
        public void StoreCertonaData(params string[] schemes)
        {
            PageSchemes = schemes;

            Framework.Log.BlockMessage($"Request Certona schemes: {string.Join(", ", schemes)}");
            XmlResponse = Framework.RequestApi.GetXml(BuildApiUrl(schemes));
        }

        /// <summary>
        /// Runs All Certona Tests and Verify Data in DOM matches Certona Data
        /// </summary>
        /// <param name="specialWidgetTitle">H1 widget title under the lpContainer class when in the recently viewed page.</param>
        public void RunAllCertonaTests(IElement specialWidgetTitle = null)
        {
            _h1TitleBelowLpContainer = specialWidgetTitle;
            VerifySchemesExistInResponse();
            VerifyTitlesMatchResponse();
            VerifySkusMatchResponse();
            VerifySkuAmountsMatchDefinedQuantities();
            VerifyCertonaIsDisabledForEmployee();
        }

        /// <summary>
        /// Verifies passed sku appears last in the specified schemes widget.
        /// </summary>
        /// <param name="sku">The sku that will be used to check if it appears last in the specified schemes widget.</param>
        public void VerifySkuAppearsLastInRelatedItems(string sku)
        {
            var lastSkuInWidget = RelatedItemSkus.Last();
            Framework.Assert.True(lastSkuInWidget == sku, $"On {CurrentPageSku} PDP, the last sku {lastSkuInWidget} in {CertonaSchemes.Related} widget is not equal to last coordinating item sku {sku}.");
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
            Framework.Assert.True(!uniqueCertonaSkusInWidget.Any(), $"Certona Sku appears in widget. Skus: {string.Join(", ", uniqueCertonaSkusInWidget)}");
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

                Framework.Assert.True(widgetSkus.Count <= 4, "There are more than 4 certona items displayed in the widget");

                // All of the trimmed widget skus should be found in the deduped certona results
                Framework.Assert.True(dedupedCertonaSkus.Intersect(widgetSkus).Count() == widgetSkus.Count, "There should only be Certona items listed after the last coordinating item in the widget.");
            }
            else
            {
                Framework.Assert.True(widgetSkus.Last() == coordinatingItemsFromDb.Last(), @"Since no Certona items populated the page, expected the last sku in the widget 
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
            var schemeXml = LoadSchemesXml(scheme);
            XElement item;

            if (schemeXml.Count() > 1 && scheme == CertonaSchemes.Home)
            {
                var pageUrl = Browser.PageUrl;

                if(pageUrl == Urls.HomePageUrl) { item = (from c in schemeXml.Descendants("PageType") where c.Value == "Homepage" select c.Parent).First(); }
                else { item = (from c in schemeXml.Descendants("PageType") where c.Value == "Installation" select c.Parent).First(); }
            }
            else
            {
                item = schemeXml.First();
            }

            var schemeQuantities = new SchemeQuantities
            {
                Minimum = Convert.ToInt32(item.Element("MinimumQuantity").Value),
                Maximum = Convert.ToInt32(item.Element("MaximumQuantity").Value),
                Requested = Convert.ToInt32(item.Element("QuantityRequested").Value)
            };

            return schemeQuantities;
        }

        private IEnumerable<XElement> LoadSchemesXml(string scheme)
        {
            try
            {
                var xmlPath = @"Utilities\Certona\RecommendedItemsConfiguration.xml";
                if (Framework.Settings.IsMobileView)
                {
                    xmlPath = @"Utilities\Certona\RecommendedItemsMobileConfiguration.xml";
                }
                var reccomendedItemsXml = XDocument.Load(xmlPath);
                // ReSharper disable once PossibleNullReferenceException
                return  from c in reccomendedItemsXml.Root.Descendants("Scheme") where c.Value == scheme select c.Parent;
            }
            catch (Exception e)
            {
                Framework.Log.Message(e.Message);

                throw;
            }
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
			var nonRelatedItemsCertonaWidgetsSelector = _suggestedProductsContainerClass.ToClassNotIdSelector(_relatedItemsId);
			var titleElements = Browser.Locate.ElementsBySelector($"{nonRelatedItemsCertonaWidgetsSelector} {HtmlTextWriterTag.H2}, {nonRelatedItemsCertonaWidgetsSelector} {_jsCertonaTitleClass.ToCssClassSelector()}");

			foreach (var titleElement in titleElements)
            {
                // Checking title because some widget containers appear on page even when signed in as employee
                Framework.Assert.True(titleElement.Text == string.Empty, $"Scheme {titleElement.GetAttribute(_dataScheme)} widget appears on page while employee is signed in.");
            }

            // Checking for Certona skus
            foreach (var element in Browser.Locate.ElementsByAttributeName(_dataCertonaSku))
            {
                Framework.Assert.NotDisplayed(element, $"Sku {element.GetAttribute(_dataCertonaSku)} appears in widget while employee is signed in.");
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
