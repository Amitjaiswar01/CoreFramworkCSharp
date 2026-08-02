using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.IntegrationTests.Pages.Home
{
    public class HomeLocatorDesktopTest : HomeLocatorTests
    {
        public HomeLocatorDesktopTest(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Integration.PageObjectModel, "Home")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LocateHomeElementsTest(string config) => Locate(config);

        protected override void CustomerServiceSignInElementValidation()
        {
            SignInWorkflow.SignIn(LampsPlusAccounts.CustomerServiceManagerLoginAccount);

            VerifyElementDisplayed(() => Home.StoreNumberField);
            Browser.Wait.ForDisplayedElement(Home.CartCountElement);
            VerifyElementDisplayed(() => Home.CartCountElement);
            Browser.Wait.ForDisplayedElement(Home.StoreWidget);
            VerifyElementDisplayed(() => Home.StoreWidget);
            VerifyElementDisplayed(() => Home.StoreHeader);
            VerifyElementDisplayed(() => Home.ChangeStoreLink);
            VerifyElementDisplayed(() => Home.StoreDetailsLink);

            SignInWorkflow.SignOut();
        }

        protected override void VerifyPlaElements()
        {
            VerifyElementDisplayed(() => Home.PlaReviewStars);
            VerifyElementDisplayed(() => Home.PlaReviews);
            VerifyElementDisplayed(() => Home.PlaQandR);
        }

        protected override void VerifyHomeElements()
        {
            VerifyElementDisplayed(() => Home.StoreMap);
            VerifyElementDisplayed(() => Home.StoreInfo);
        }
    }

    public class HomeLocatorMobileTest : HomeLocatorTests
    {
        public HomeLocatorMobileTest(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Integration.PageObjectModel, "Home")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LocateHomeElementsTest(string config) => Locate(config);

        protected override void CustomerServiceSignInElementValidation()
        {
            ShoppingCartWorkflow.AddSingleItemToCart();
            Browser.Navigate(Urls.HomePageUrl);

            VerifyElementNotImplemented(() => Home.StoreNumberField);
            VerifyElementDisplayed(() => Home.CartCountElement);
            VerifyElementNotImplemented(() => Home.StoreWidget);
            VerifyElementNotImplemented(() => Home.StoreHeader);
            VerifyElementNotImplemented(() => Home.ChangeStoreLink);
            VerifyElementNotImplemented(() => Home.StoreDetailsLink);
        }

        protected override void VerifyPlaElements()
        {
            VerifyElementNotImplemented(() => Home.PlaReviewStars);
            VerifyElementNotImplemented(() => Home.PlaReviews);
            VerifyElementNotImplemented(() => Home.PlaQandR);
        }

        protected override void VerifyHomeElements()
        {
            VerifyElementNotImplemented(() => Home.StoreMap);
            VerifyElementNotImplemented(() => Home.StoreInfo);
        }
    }
    

    /// <summary>
    /// Tests to ensure all IElements and Lists of IElements can be found on the Home page.
    /// </summary>
    public abstract class HomeLocatorTests : PageObjectTestsBase
    {
        protected HomeLocatorTests(ITestOutputHelper output) : base(output) { }

        public void Locate(string config)
        {
            InitializeFramework(config, Urls.HomePageUrl);
            BuildElementsList(Home);

            VerifyElementDisplayed(() => Home.BodyElement);
            VerifyElementDisplayed(() => Home.HomeSliderElement);
            VerifyElementDisplayed(() => Home.HomepageSplashBanner);
            VerifyElementDisplayed(() => Home.CartHeaderButton);

            VerifyHomeElements();

            CustomerServiceSignInElementValidation();

            GetPlaForHomeLocatorTest();

            VerifyElementDisplayed(() => Home.PlaFrameElement);
            VerifyElementDisplayed(() => Home.PlaViewDetailsLinkElement);
            VerifyElementDisplayed(() => Home.PlaAddToCartElement);

            VerifyPlaElements();

            SignInWorkflow.SignIn(LampsPlusAccounts.HospitalityLoginAccount);
            VerifyElementDisplayed(() => Home.HospitalitySplashBanner);
            VerifyElementExists(() => Home.HospitalitySplashBannerLink);
        }

        protected abstract void CustomerServiceSignInElementValidation();

        protected abstract void VerifyPlaElements();

        protected abstract void VerifyHomeElements();
    }
}
