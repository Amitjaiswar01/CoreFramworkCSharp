using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Certona.T314_T507_VerifyCertonaSchemaForHomepage
{
    //[Collection(LpTraits.BatchGroup.Common.Certona)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Certona)]
    public class T314_Windows_VerifyCertonaSchemaForHomepage : T314_DesktopBase
    {
        public T314_Windows_VerifyCertonaSchemaForHomepage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void CertonaSchemaForHomepage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Certona)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Certona)]
    public class T314_Mac_VerifyCertonaSchemaForHomepage : T314_DesktopBase
    {
        public T314_Mac_VerifyCertonaSchemaForHomepage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void CertonaSchemaForHomepage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Certona)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Certona)]
    public class T314_iPad_VerifyCertonaSchemaForHomepage : T314_DesktopBase
    {
        public T314_iPad_VerifyCertonaSchemaForHomepage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void CertonaSchemaForHomepage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Certona)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Certona)]
    public class T314_TabletEmulator_VerifyCertonaSchemaForHomepage : T314_DesktopBase
    {
        public T314_TabletEmulator_VerifyCertonaSchemaForHomepage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void CertonaSchemaForHomepage(string config) => Validate(config);
    }


    /// <summary>
	/// Verify the correct schemes are being called to populate the Certona widgets on the home page.
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10237
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T314
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10237"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T314")] 
    public class T314_DesktopBase : TestsBaseDesktop
    {
        public T314_DesktopBase(ITestOutputHelper output) : base(output) { }

        public void Validate(string config)
        {
            // Arrange : User has visited the PDP for 4 items
            InitializeFunctionalTest(config);
            CertonaWorkflow.VisitMultiplePages(4);

            // Act : Add Item to Cart and Navigate to Home Page
            ProductDetail.AddToCart();
            Assert.True(Cart.IsCurrentPage, "This is Not a Cart Page");

            var skusInCart = Cart.GetListOfAllProductsOnCartPage();

            Browser.Navigate(Urls.HomePageUrl);
            Assert.True(Home.IsCurrentPage, "This is Not a Home Page");

            // Assert : Check Recently Viewed widget showing or not
            Assert.True(Home.IsRecentlyViewedWidgetVisible, "The 'Recently Viewed' widget is not displayed on Home Page.");
            Assert.True(Home.IsJustForYouWidgetVisible, "Just For You Widget is Not Displayed");
            Assert.True(Home.IsInYourCartWidgetVisible, "Cart Widget is Not Displayed");
            Assert.Equals(skusInCart[0].Sku, Home.GetCartWidgetSku(), "The Sku in Cart does not match with that on Cart Widget");
            Assert.False(string.IsNullOrWhiteSpace(Home.GetCertonaWidgetSku()), "No SKU displayed in Recently Viewed Section.");
            Assert.False(string.IsNullOrWhiteSpace(Home.GetJustForYouWidgetSku()), "No SKU displayed in Just for You widget");
        }
    }
}