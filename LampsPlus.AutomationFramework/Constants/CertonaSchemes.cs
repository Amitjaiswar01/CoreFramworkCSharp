namespace LampsPlus.AutomationFramework.Constants
{
    /// <summary>
    /// Constant strings of possible Certona Scheme names
    /// </summary>
    public class CertonaSchemes
    {
        public const string Cart = "cart_rr";
        public const string CategoryLanding = "categorylanding_rr";
        public const string GlobalFooterCategoryLanding = "GF_categorylanding_rr";
        public const string GlobalFooterDefault = "GF_default_rr";
        public const string GlobalFooterFull = "GF_full_rr";
        public const string GlobalFooterHomepage = "GF_homepage_rr";
        public const string GlobalFooterNoSearch = "GF_nosearch_rr";
        public const string GlobalFooterProduct = "GF_product_rr";
        public const string GlobalFooterSortPage = "GF_sortpage_rr";
        public const string Home = "home_rr";
        public const string MoreLikeThis = "glo_MLT_sort_rr";
        public const string NoSearch = "nosearch_rr";
        public const string NoSearchSku = "nosearchsku_rr";
        public const string OrderConfirmation = "orderconfirmation_rr";
        public const string OrderStatus = "orderstatus_rr";
        public const string Product = "product_rr";
        public const string Related = "related_rr";
        public const string Similar = "similar_rr";
        public const string SimilarFullPage = "similar2_rr";
        public const string SortPage = "sortpage_rr";
        public const string Wishlist = "wishlist_rr";
        public const string LastCategory = "lastCategory_rr";
        public const string TrendingCategory = "categorylanding02_rr";//TODO Added new scheme

        /// <summary>
        /// Array of schemes that require a context parameter in the Certona request URL
        /// </summary>
        public static readonly string[] SchemesThatNeedUrlContextParameter = { GlobalFooterProduct, GlobalFooterSortPage, MoreLikeThis, NoSearchSku, Similar, SimilarFullPage, SortPage, Product, Related, OrderStatus, OrderConfirmation, Wishlist, Cart };

        /// <summary>
        /// Array of schemes that require a extra criteria in the Certona request
        /// </summary>
        public static readonly string[] SchemesThatNeedUrlSortCriteriaParameters = { CategoryLanding, GlobalFooterCategoryLanding, GlobalFooterSortPage, SortPage, TrendingCategory };//TODO Added scheme
    }
}
