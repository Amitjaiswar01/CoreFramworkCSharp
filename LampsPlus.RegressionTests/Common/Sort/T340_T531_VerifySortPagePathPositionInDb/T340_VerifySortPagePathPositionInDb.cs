using System.Linq;
using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Sort.T340_T531_VerifySortPagePathPositionInDb
{
    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T340_Windows_VerifySortPagePathPositionInDb : T340_DesktopBase
    {
        public T340_Windows_VerifySortPagePathPositionInDb(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI_ElasticSearch)]
        public void SortPagePathPositionInDb(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T340_Mac_VerifySortPagePathPositionInDb : T340_DesktopBase
    {
        public T340_Mac_VerifySortPagePathPositionInDb(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void SortPagePathPositionInDb(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T340_iPad_VerifySortPagePathPositionInDb : T340_DesktopBase
    {
        public T340_iPad_VerifySortPagePathPositionInDb(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void SortPagePathPositionInDb(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T340_TabletEmulator_VerifySortPagePathPositionInDb : T340_DesktopBase
    {
        public T340_TabletEmulator_VerifySortPagePathPositionInDb(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_ESI)]
        public void SortPagePathPositionInDb(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the sort page path and position is recorded in the DB for items placed in cart.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10082
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T340
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10082"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T340")]
    [Trait(LpTraits.Keys.Category, LpTraits.Categories.CRUD)]
    public abstract class T340_DesktopBase : TestsBaseDesktop
    {
        protected T340_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            var sortPath = "/products/bathroom-lighting/";
            var position = 2;

            //Arrange : User is on Homepage
            InitializeFunctionalTest(config);

            //Act : Navigate to a sort page appended with ?test=junk
            Browser.Navigate(Urls.BathroomLightingUrl + "?test=junk");
            Assert.True(Sort.IsCurrentPage, "Current page is not Sort page");

            //Act : Navigate product to Cart from Sort page and notedown its position
            Sort.NavigateToPdpFromSortByProductPosition(position);
            ProductDetail.AddToCart();
              
            //Act : Notedown CartId and get the values from database
            var cartId = Cart.GetCartId();
            var productDataDb = SortActions.GetSortPathPositionCartItems(cartId);

            //Assert : Verify SortPath and SortPosition columns in DB match values for the SKU added to the cart
            Assert.Equals(position, productDataDb.First().SortPosition, "SortPosition does not match for Cart Item");
            Assert.Equals(sortPath, productDataDb.First().SortPath, "Invalid Sort Path for Cart Item");
        }
    }
}