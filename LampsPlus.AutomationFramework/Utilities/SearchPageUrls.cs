using System.Collections.Generic;
using Automation.Framework;
using Automation.Framework.Core;

namespace LampsPlus.AutomationFramework.Utilities
{
    /// <summary>
    /// Navigate to Search pages.
    /// </summary>
    public class SearchPageUrls : Page
    {
        /// <inheritdoc />
        public SearchPageUrls(IBrowser browser) : base(browser) { }

        public static string BdProdSortId => "bdProdSort";
        public static string FloorLampsUrl => "https://www.lampsplus.com/products/s_floor-lamps/?s=1";
        public static string BathroomVanityLightsUrl => "https://www.lampsplus.com/products/s_bathroom-vanity-lights/?s=1";
        public static string TableLampsUrl => "https://www.lampsplus.com/products/s_table-lamps/?s=1";
        public static string LampShadesUrl => "https://www.lampsplus.com/products/s_lamp-shades/?s=1";
        public static string WallSconcesUrl => "https://www.lampsplus.com/products/s_wall-sconces/?s=1";
        public static string FlushMountCeilingLightUrl => "https://www.lampsplus.com/products/s_flush-mount-ceiling-light/?s=1";
        public static string KitChenPendantLightsUrl => "https://www.lampsplus.com/products/s_kitchen-pendant-lights/?s=1";
        public static string CeilingFansWithLightsUrl => "https://www.lampsplus.com/products/s_ceiling-fans-with-lights/?s=1";
        public static string CrystalChandeliersUrl => "https://www.lampsplus.com/products/s_crystal-chandeliers/?s=1";
        public static string PossiniUrl => "https://www.lampsplus.com/products/s_possini/?s=1";

        public IElement SortBodyElement => Browser.Locate.ElementById(BdProdSortId);

        /// <summary>
        /// Store the Urls in the list.
        /// </summary>
        public static List<string> GetListOfSearchPageUrls
        {
            get
            {
                var listOfSearchPageUrls = new List<string>
                {
                    FloorLampsUrl,
                    BathroomVanityLightsUrl,
                    TableLampsUrl,
                    LampShadesUrl,
                    WallSconcesUrl,
                    FlushMountCeilingLightUrl,
                    KitChenPendantLightsUrl,
                    CeilingFansWithLightsUrl,
                    CrystalChandeliersUrl,
                    PossiniUrl
                };

                return listOfSearchPageUrls;
            }
        }

        /// <summary>
        /// Navigate to each of the search page.
        /// </summary>
        public void NavigateSearchPages()
        {
            var countNumberOfUrl = GetListOfSearchPageUrls.Count;

            for (var numberOfUrl = 0; numberOfUrl < countNumberOfUrl; numberOfUrl++)
            {
                Browser.Navigate(GetListOfSearchPageUrls[numberOfUrl]);

                Browser.Wait.ForElement(SortBodyElement, 10);
            }
        }
       
    }
}
