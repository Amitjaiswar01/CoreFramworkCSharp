using Automation.Framework;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Automation.Framework.Verifies;

namespace LampsPlus.AutomationFramework.Utilities
{
    /// <summary>
    /// Data Capture helper methods
    /// </summary>
    public class DataCaptureUtility
    {
        //TestsBase instances
        private readonly IBrowser _browser;
        private readonly IAssert _assert;
        private readonly NetworkLoggingUtility _networkLoggingUtility;


        private int _maximumSecondsToWait = 5;
        private string _dataCapUrl = "data-capture";
        private static object _lock = new object();
        private static bool _isInitialized;

        public DataCaptureUtility(IBrowser browser, IAssert assert, NetworkLoggingUtility networkLoggingUtility)
        {
            _browser = browser;
            _assert = assert;
            _networkLoggingUtility = networkLoggingUtility;

            Initialize();
        }

        private void Initialize()
        {
            if (_isInitialized) return;

            lock (_lock)
            {
                if (_isInitialized) return;

                _isInitialized = true;
            }
        }

        /// <summary>
        /// Get a fresh list of all the data-capture call post data.
        /// </summary>
        /// <returns>A list of all the data-capture post data.</returns>
        public List<JObject> GetCurrentDataCaptureNetworkData()
        {
            return _networkLoggingUtility.GetPostRequestsByUrl(_dataCapUrl)
                .Select(request => JObject.Parse(((JValue)request["request"]["postData"]["text"]).Value.ToString())).ToList();
        }

        /// <summary>
        /// Waits for certain sku(s) with pageSection to appear in network log, then gets a fresh list of all the data-capture call post data.
        /// </summary>
        /// <param name="pageSection">Page section value of sku.</param>
        /// <param name="pageSkus">The sku(s) to wait for.</param>
        /// <returns>A list of all the data-capture post data.</returns>
        public List<JObject> WaitAndGetDataCaptureNetworkData(PayloadValues.PageSections? pageSection, params string[] pageSkus)
        {
            List<JObject> entries = new List<JObject>();

            SpinWait.SpinUntil(() =>
            {
                entries = GetCurrentDataCaptureNetworkData();

                var skuEventsForSection = entries
                                            .SelectMany(evnt => evnt["events"])
                                            .Where(skuEvent => pageSection != null ?
                                                    skuEvent["section"].ToString() == pageSection.ToString() :
                                                    skuEvent["section"].Type == JTokenType.Null)
                                            .ToList();

                // All skus in pageSkus array are found in skuEventsForSection
                return pageSkus.All(pageSku => skuEventsForSection.Any(evnt => evnt[PayloadKeys.Sku].ToString() == pageSku));
            }, TimeSpan.FromSeconds(_maximumSecondsToWait));

            return entries;
        }

        /// <summary>
        /// Verifies all the page level payload data for every data-capture call.
        /// </summary>
        /// <param name="parsedEvents">The network data.</param>
        /// <param name="pairs">Values of properties to check in page level data.</param>
        public void VerifyPageLevelPayload(List<JObject> parsedEvents, Dictionary<string, string> pairs)
        {
            parsedEvents.ForEach(evnt =>
                pairs.ToList().ForEach(pair =>
                    _assert.Equals(pair.Value, evnt[pair.Key].ToString(), "Expected page level data not found in event(s).")));
        }

        /// <summary>
        /// Verify the post data payload values for each sku passed in.
        /// </summary>
        /// <param name="parsedEvents">The network data.</param>
        /// <param name="pageSection">The page section of the skus passed in.</param>
        /// <param name="pageSkus">The skus to check the events of.</param>
        /// <param name="pairs">Values of properties to check in the events of each page sku.</param>
        public void VerifySkuEventPayload(List<JObject> parsedEvents, PayloadValues.PageSections? pageSection, List<string> pageSkus, Dictionary<string, object> pairs)
        {
            // Gets all sku event calls and flattens to one array, and filters to only the sku's in the data capture section
            // Page section is implicitly checked in these linq constraints
            // Section will always be either null, or a string
            var skuEventsForSection = parsedEvents
                .SelectMany(evnt => evnt["events"])
                .Where(skuEvent => pageSection != null ?
                        skuEvent["section"].ToString() == pageSection.ToString() :
                        skuEvent["section"].Type == JTokenType.Null
                      ).ToList();

            pageSkus.ForEach(sku =>
            {
                var skuEvent = skuEventsForSection.Find(evnt => evnt[PayloadKeys.Sku].ToString() == sku);
                _assert.NotNull(skuEvent, $"No event found for page sku '{sku}'");

                if (skuEvent == null)
                {
                    return;
                }

                pairs.ToList().ForEach(pair =>
                {
                    var val = pair.Value;
                    var skuEventValue = skuEvent[pair.Key];

                    if (val is int)
                    {
                        _assert.True(skuEventValue.Type == JTokenType.Integer && Convert.ToInt32(val) == Convert.ToInt32(skuEventValue),
                        $"Sku '{sku}' event key '{pair.Key}' has incorrect value of '{skuEventValue}' instead of '{val}'");
                    }
                    else if (val is string)
                    {
                        _assert.True(skuEventValue.Type == JTokenType.String && val.ToString() == skuEventValue.ToString(),
                        $"Sku '{sku}' event key '{pair.Key}' has incorrect value of '{skuEventValue}' instead of '{val}'");
                    }
                    else if (val is null)
                    {
                        _assert.True(skuEventValue.Type == JTokenType.Null, $"Sku '{sku}' event key '{pair.Key}' has incorrect value of '{skuEventValue}' instead of 'null'");
                    }
                });
            });
        }

        /// <summary>
        /// Returns IElement of given page section.
        /// </summary>
        /// <param name="pageSection">PageSection value.</param>
        /// <returns>IElement container of pagesection.</returns>
        public IElement GetContainerByPageSection(PayloadValues.PageSections pageSection)
        {
            return _browser.Locate.ElementBySelector($"[data-capture-section='{pageSection.ToString()}']");
        }

        /// <summary>
        /// Returns numbers of skus in the data-capture calls for a certain page section.
        /// </summary>
        /// <param name="parsedEvents">The network data.</param>
        /// <param name="pageSection">PageSection value.</param>
        /// <returns>Number of skus in data-capture call.</returns>
        public int GetCountOfSkuEventsBySection(List<JObject> parsedEvents, PayloadValues.PageSections pageSection)
        {
            return parsedEvents.SelectMany(evnt => evnt["events"]).Count(skuEvent => skuEvent["section"].ToString() == pageSection.ToString());
        }

        /// <summary>
        /// Verifies viewId and websiteMode Page Level data is correct for desktop.
        /// </summary>
        /// <param name="parsedEventCalls">The network data.</param>
        public void VerifyPageLevelPayloadIsForDesktop(List<JObject> parsedEventCalls)
        {
            VerifyPageLevelPayload(parsedEventCalls, new Dictionary<string, string>
            {
                { PayloadKeys.ViewId, PayloadValues.ViewId.Desktop.ToString("D") },
                { PayloadKeys.WebsiteMode, PayloadValues.WebsiteMode.Global.ToString() }
            });
        }

        /// <summary>
        /// Verifies viewId and websiteMode Page Level data is correct for mobile.
        /// </summary>
        /// <param name="parsedEventCalls">The network data.</param>
        public void VerifyPageLevelPayloadIsForMobile(List<JObject> parsedEventCalls)
        {
            VerifyPageLevelPayload(parsedEventCalls, new Dictionary<string, string>
            {
                { PayloadKeys.ViewId, PayloadValues.ViewId.Mobile.ToString("D") },
                { PayloadKeys.WebsiteMode, PayloadValues.WebsiteMode.Global.ToString() }
            });
        }


        public static class PayloadKeys
        {
            public static string PageTypeId = "pageTypeId";
            public static string ViewId = "viewId";
            public static string WebsiteMode = "websiteMode";
            public static string Event = "event";
            public static string EventId = "eventId";
            public static string HasAddToCart = "hasAddToCart";
            public static string SectionId = "sectionId";
            public static string Sku = "sku";
            public static string Quantity = "quantity";
            public static string TestCompositionId = "testCompositionId";
        }


        public static class PayloadValues
        {
            public enum Event
            {
                SkuView = 1,
                AddToCart,
                OrderConfirmation
            }

            public enum PageTypeId
            {
                None,
                Home,
                Landing,
                Sort,
                Product,
                Cart,
                Shipping,
                Checkout,
                Confirmation,
                AddToCart,
                ContactUs,
                WishList,
                Product_Quick_View,
                Slyce,
                All,
                Pla,
                Certona,
                MoreLikeThis,
                SearchResults
            }

            public enum ViewId
            {
                Desktop = 1,
                Mobile
            }

            public enum PageSections
            {
                AddAllToCart = 1,
                AddBulbs,
                AddToCartPdp,
                AddToWishList,
                BtnCheckout,
                CustomerAlsoViewed,
                SliderFamily,
                Featured,
                InYourCartHomeWidget,
                JustForYouHomeWidget,
                JustForYouFull,
                MoreLikeThisSortedResults,
                SliderMoreAccessories,
                SliderMoreOptions,
                MoreYouMayLikeFull,
                MoreYouMayLikeWidget,
                PLAHero,
                PLAResults,
                PDPMainProduct,
                PDPProductOption,
                RecentlyViewedFull,
                RecentlyViewedHomeWidget,
                RecentlyViewedFooterWidget,
                RelatedItems,
                SimilarDesigns,
                SortResults,
                FreeTextResults,
                TopSellingDesigns,
                WishListContents,
                ShopByRoomDetails,
                ShopByRoomSimilarItems,
                AvailableOptions,
                DimmerFacePlateOptions,
                Downrods,
                PopularAccessories,
                WishListGridView,
                WishListDetailView,
                WishListCompareView,
                ImageResults,
                CustomerAlsoViewedQuickView,
                OrderDetails,
                SuggestedProducts,
                LightingCollectionList,
                CartOverview,
                LightingCollectionDetail,
                AllBaseColors,
                StickyHeader,
                WishListAddAllToCart,
                BuildFullSystem,
                StickyHeaderDimmerFacePlate,
                AddToCartPdpDimmerFacePlate,
                AddToCartPdpFullTrackSystem,
                AddToCartPdpHousingOptions,
                AddToCartLightingCollection,
                StickyHeaderMultiProduct,
                AddToCartPdpMultiProduct,
                AddToCartPla,
                SliderBaseOptions,
                SliderShadeOptions
            }

            public enum WebsiteMode
            {
                Global
            }
        }
    }
}
