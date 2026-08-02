using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Databases.Entities;
using LampsPlus.AutomationFramework.Utilities;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.DesktopTablet.ProductDetail
{
    /// <summary>
    /// Base class for Product Detail specific tests.
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Regression)]
    [Trait(LpTraits.Keys.Feature, LpTraits.RegressionFeatureTags.ProductDetail)]
    public class ProductDetailTestsBase : TestsBase
    {
        /// <summary>
        /// Test base for ProductDetail.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public ProductDetailTestsBase(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the Design Your Own Track Lighting System banner is present.
        /// </summary>
        /// <param name="primarySku"></param>
        public void VerifyDesignYourOwnTrackLightingSystemBanner(string primarySku)
        {
            Assert.Displayed(ProductDetailTrackLighting.DesignYourOwnTrackLightingSystemBanner, "Design your own track lighting system banner is not displayed.");
            Assert.TextLink(Urls.DesignYourOwnTrackLightingSystemPageUrl, ProductDetailTrackLighting.DesignYourOwnTrackLightingSystemBanner.GetAttribute(HtmlTextWriterAttribute.Href.ToString()), "Links do not match.");
            ProductDetailTrackLighting.DesignYourOwnTrackLightingSystemBanner.Click();
            Assert.Equals(Urls.DesignYourOwnTrackLightingSystemPageUrl, Browser.PageUrl, "Banner did not bring user to the correct design your own track lighting url.");
            ProductDetail.NavigateToProductDetailByShortSku(primarySku);
        }

        /// <summary>
        /// Verify the various elements of the Build Full System.
        /// </summary>
        /// <param name="primarySku"></param>
        /// <param name="buildFullSystemProducts"></param>
        /// <param name="tableTitle"></param>
        public void VerifyBuildFullSystem(string primarySku, List<BuildFullSystemProductModel> buildFullSystemProducts, string tableTitle)
        {
            var expectedFullSystemQuantity = 1;

            Assert.Displayed(ProductDetail.BuildFullSystemContainer, "Build full system container is not displayed.");
            Assert.Equals("BUILD FULL SYSTEM", ProductDetail.BuildFullSystemSectionTitle, "Build full system section title should be BUILD FULL SYSTEM.");
            Assert.Equals(tableTitle, ProductDetail.BuildFullSystemTableTitle, "Build full system table title does not match. ");

            var fullSystemSkus = ProductDetail.GetListOfFullSystemSkus;
            var fullSystemProductNames = ProductDetail.GetListOfFullSystemProductNames;
            var fullSystemQuantities = ProductDetail.GetListOfFullSystemQuantities;

            Assert.Equals(primarySku, fullSystemSkus[0], "Primary sku does not match.");
            Assert.Equals(buildFullSystemProducts.Count + 1, fullSystemSkus.Count, "The number of skus in the database does not match what is displayed on the web page.");
            Assert.Equals(expectedFullSystemQuantity, fullSystemQuantities[0], $"Expected a quantity of {expectedFullSystemQuantity} but found {fullSystemQuantities[0]}.");

            for (int i = 0; i < buildFullSystemProducts.Count; i++)
            {
                if (buildFullSystemProducts[i].DisplayOrder < 100)
                {
                    Assert.Equals(buildFullSystemProducts[i].BuildFullSystemSku, fullSystemSkus[i + 1], "Web page sku does not match the database sku.");
                    Assert.Equals(DecodeProductName(buildFullSystemProducts[i].ProductName), fullSystemProductNames[i + 1], "Web page Product name does not match the database name.");

                    if (buildFullSystemProducts[i].Quantity > 0)
                        Assert.Equals(buildFullSystemProducts[i].Quantity, fullSystemQuantities[i + 1], "Quantity on web page does not match the database quantity.");
                    continue;
                }

                var subProduct = buildFullSystemProducts.FirstOrDefault(sp => sp.BuildFullSystemSku.Equals(fullSystemSkus[i + 1]));

                Assert.NotNull(subProduct, "Product is Null");
                // ReSharper disable once PossibleNullReferenceException
                Assert.Equals(subProduct.BuildFullSystemSku, fullSystemSkus[i + 1], "Full system sku on the web page does not match database sku");
                Assert.Equals(DecodeProductName(subProduct.ProductName), fullSystemProductNames[i + 1], "Full system product name on the web page does not match the database name.");
            }
        }

        private static string DecodeProductName(string name)
        {
            return name.Replace("&quot;", "\"").Replace("&#39;", "'");
        }
    }
}
