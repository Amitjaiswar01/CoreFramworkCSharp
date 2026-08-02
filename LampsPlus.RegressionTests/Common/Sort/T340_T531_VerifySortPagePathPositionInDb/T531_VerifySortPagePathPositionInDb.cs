using System.Linq;
using xRetry;
using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Sort.T340_T531_VerifySortPagePathPositionInDb
{
    //[Collection(LpTraits.BatchGroup.Mobile.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Sort)]
    public class T531_iPhone_VerifySortPagePathPositionInDb : T531_MobileBase
    {
        public T531_iPhone_VerifySortPagePathPositionInDb(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_ElasticSearch)]
        public void SortPagePathPositionInDb(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.Sort)]
    public class T531_Android_VerifySortPagePathPositionInDb : T531_MobileBase
    {
        public T531_Android_VerifySortPagePathPositionInDb(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void SortPagePathPositionInDb(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T531_Emulator_VerifySortPagePathPositionInDb : T531_MobileBase
    {
        public T531_Emulator_VerifySortPagePathPositionInDb(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_ElasticSearch)]
        public void SortPagePathPositionInDb(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the sort page path and position is recorded in the DB for items placed in cart.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10082
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T531
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10082"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T531")]
    public abstract class T531_MobileBase : TestsBaseMobile
    {
        protected T531_MobileBase(ITestOutputHelper output) : base(output) { }

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