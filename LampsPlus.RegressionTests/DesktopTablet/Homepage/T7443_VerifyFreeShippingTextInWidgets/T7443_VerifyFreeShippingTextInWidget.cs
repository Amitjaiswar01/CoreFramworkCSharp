using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.DesktopTablet.Homepage.T7443_VerifyFreeShippingTextInWidgets
{
    //[Collection(LpTraits.BatchGroup.Desktop.Homepage)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Homepage)]
    public class T7443_Windows_VerifyFreeShippingTextInWidget : T7443_DesktopBase
    {
        public T7443_Windows_VerifyFreeShippingTextInWidget(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyFreeShippingTextInWidget(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.Homepage)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Homepage)]
    public class T7443_Mac_VerifyFreeShippingTextInWidget : T7443_DesktopBase
    {
        public T7443_Mac_VerifyFreeShippingTextInWidget(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyFreeShippingTextInWidget(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.Homepage)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Homepage)]
    public class T7443_iPad_VerifyFreeShippingTextInWidget : T7443_DesktopBase
    {
        public T7443_iPad_VerifyFreeShippingTextInWidget(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyFreeShippingTextInWidget(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.Homepage)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Homepage)]
    public class T7443_TabletEmulator_VerifyFreeShippingTextInWidget : T7443_DesktopBase
    {
        public T7443_TabletEmulator_VerifyFreeShippingTextInWidget(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyFreeShippingTextInWidget(string config) => Validate(config);
    }


    /// <summary>
    /// Verify Collapsed Header Functionality on View in Room Page
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9952
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7443
    /// </summary>
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9952"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7443")]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.HeaderFooter)]

    public abstract class T7443_DesktopBase : TestsBaseDesktop
    {
        protected T7443_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            /*Arrange - Navigate to any PDP*/
            InitializeFunctionalTest(config);
            var sku = ProductActions.GetAnySkuWithProductDetailPage;
            var pdpNavigationCount = 3;

            /*Act
            Navigate to Crystal Chandeliers Page and Navigate back to Home Page
            */
            Sort.Navigate(Sort.CrystalChandeliersUrl);
            Assert.True(Sort.IsCurrentPage, "User is not on the Crystal Chandeliers Page");
            Home.Navigate();
            Assert.True(Home.IsCurrentPage, "User is not on the Home Page");

            /*Assert*/
            Assert.False(Home.IsFreeShippingHeadingVisible, "The 'FREE Shipping...' text is displayed.");
            Assert.False(Home.IsInYourCartWidgetVisible, "The 'In Your Cart' widget is displayed.");
            Assert.False(Home.IsRecentlyViewedWidgetVisible, "The 'Recently Viewed' widget is displayed.");
            Assert.False(Home.IsJustForYouWidgetVisible, "The 'Just For You' widget is displayed.");

            /*Act
            Navigate to 3 Product Detail Pages and Navigate back to Home Page
            */
            ProductDetail.NavigateToMultiplePdps(pdpNavigationCount);
            Home.Navigate();
            Assert.True(Home.IsCurrentPage, "User is not on the Home Page");

            /*Assert*/
            Assert.False(Home.IsFreeShippingHeadingVisible, "The 'FREE Shipping...' text is displayed.");
            Assert.False(Home.IsInYourCartWidgetVisible, "The 'In Your Cart' widget is displayed.");
            Assert.True(Home.IsRecentlyViewedWidgetVisible, "The 'Recently Viewed' widget is not displayed.");
            Assert.True(Home.IsJustForYouWidgetVisible, "The 'Just For You' widget is not displayed.");

            /*Act
            Navigate to a PDP and Add the Product to Cart and Navigate back to Home Page
            */
            ProductDetail.AddSingleProductToCart(sku);  
            Assert.True(Cart.IsCurrentPage, "User is not on the Cart Page");
            Home.Navigate();
            Assert.True(Home.IsCurrentPage, "User is not on the Home Page");

            /*Assert*/
            Assert.False(Home.IsFreeShippingHeadingVisible, "The 'FREE Shipping...' text is displayed.");
            Assert.True(Home.IsInYourCartWidgetVisible, "The 'In Your Cart' widget is not displayed.");
            Assert.True(Home.IsRecentlyViewedWidgetVisible, "The 'Recently Viewed' widget is not displayed.");
            Assert.True(Home.IsJustForYouWidgetVisible, "The 'Just For You' widget is not displayed.");
        }
    }
}


