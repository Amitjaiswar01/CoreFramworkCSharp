using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.IntegrationTests.Pages.GlobalLocators
{
    public class GlobalLocatorDesktopTest : GlobalLocatorTests
    {
        public GlobalLocatorDesktopTest(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Integration.PageObjectModel, "GlobalLocators")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LocateGlobalLocatorsElementsTest(string config) => Locate(config);

        protected override void PromoCodeElementValidation()
        {
            VerifyElementNotImplemented(() => GlobalLocators.LpMobileOverlayElement);
        }

        protected override void PdpElementVerification()
        {
            VerifyElementDisplayed(() => GlobalLocators.AllPageContent);
            VerifyElementDisplayed(() => GlobalLocators.CalloutButton);

            VerifyElementNotImplemented(() => GlobalLocators.PdpDrawerElement);

            VerifyElementsNotImplemented(() => GlobalLocators.PdpDrawerElements);
            VerifyElementNotImplemented(() => GlobalLocators.LpMobileOverlayVideoElement);
            VerifyElementNotImplemented(() => GlobalLocators.LpModalCloseVideoElement);
            VerifyElementNotImplemented(() => GlobalLocators.LpDropdownPanel);
            
            Browser.Wait.UntilElementDoesntExist(GlobalLocators.LpModalId.ToCssIdSelector());

            GlobalLocators.AddToCartButton.Click();
        }

        protected override void CartElementVerification()
        {
            VerifyElementNotImplemented(() => GlobalLocators.LpMobileDrawerElement);
            Browser.Wait.ForPage(Urls.CartOverviewPageUrl);
            CartOverview.EmailButton.Click();
            Browser.Wait.ForDisplayedElement(GlobalLocators.IframeModal, 30);
            VerifyElementDisplayed(() => GlobalLocators.IframeModal);
        }

        protected override void DrawerValidation()
        {
            VerifyElementNotImplemented(() => GlobalLocators.DisplayedMobileDrawerMenu);
            VerifyElementNotImplemented(() => GlobalLocators.MobileDrawerMenuInnerContainer);
            VerifyElementNotImplemented(() => GlobalLocators.CloseDrawerButton);
        }

        protected override void ShippingPageDropdownValidation()
        {
            VerifyElementNotImplemented(() => GlobalLocators.CountryDropdown);
            VerifyElementNotImplemented(() => GlobalLocators.StateDropdown);
        }
    }


    public class GlobalLocatorMobileTest : GlobalLocatorTests
    {
        public GlobalLocatorMobileTest(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Integration.PageObjectModel, "GlobalLocators")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LocateGlobalLocatorsElementsTest(string config) => Locate(config);

        protected override void PromoCodeElementValidation()
        {
            VerifyElementDisplayed(() => GlobalLocators.LpMobileOverlayElement);
        }

        protected override void PdpElementVerification()
        {
            HeaderFooter.ContactUsPhoneIcon.Click();
            Browser.Wait.ForDisplayedElement(GlobalLocators.LpDropdownPanel);
            
            VerifyElementNotImplemented(() => GlobalLocators.CalloutButton);
            VerifyElementNotImplemented(() => GlobalLocators.AllPageContent);

            VerifyElementDisplayed(() => GlobalLocators.LpDropdownPanel);
            VerifyElementDisplayed(() => GlobalLocators.PdpDrawerElement);
            VerifyElementDisplayed(() => GlobalLocators.PdpDrawerElements);

            ProductDetail.NavigateToProductDetailByShortSku(ProductActions.GetAnySkuWithProductDetailPage);

            Browser.Wait.ForDomReady();

            ProductDetail.ProductDescriptionAccordion.Click();
            Browser.Wait.ForElementToStopAnimating(ProductDetail.ProductDescriptionAccordion);

            ProductDetail.RelatedVideo.Click();

            Browser.Wait.ForDisplayedElement(GlobalLocators.LpMobileOverlayVideoElement);
           
            VerifyElementDisplayed(() => GlobalLocators.LpMobileOverlayVideoElement);
            VerifyElementDisplayed(() => GlobalLocators.LpModalCloseVideoElement);

            GlobalLocators.LpModalCloseVideoElement.Click();

            Browser.Wait.UntilElementUnloads(GlobalLocators.LpMobileOverlayVideoElement);

            ProductDetail.StickyAddToCart.Click();
        }

        protected override void CartElementVerification()
        {
            VerifyElementDisplayed(() => GlobalLocators.LpMobileDrawerElement);
            VerifyElementNotImplemented(() => GlobalLocators.IframeModal);
        }

        protected override void DrawerValidation()
        {
            Browser.Navigate(Urls.AllChandeliersSortPageUrl);
            Sort.ToggleSortFilterMenuButton.Click();
            CommonWorkflow.WaitForDrawerToStopAnimating();

            VerifyElementDisplayed(() => GlobalLocators.DisplayedMobileDrawerMenu);
            VerifyElementDisplayed(() => GlobalLocators.MobileDrawerMenuInnerContainer);
            VerifyElementDisplayed(() => GlobalLocators.CloseDrawerButton);
        }

        protected override void ShippingPageDropdownValidation()
        {
            CartOverview.CheckOutNowButton.Click();

            CustomerAddressInformation.ShowCountryLink.Click();

            VerifyElementDisplayed(() => GlobalLocators.CountryDropdown);
            VerifyElementDisplayed(() => GlobalLocators.StateDropdown);
        }
    }


    /// <summary>
    /// Tests to ensure all IElements and Lists of IElements can be found for Global Locators.
    /// </summary>
    [Trait(LpTraits.Integration.PageObjectModel, "GlobalLocators")]
    public abstract class GlobalLocatorTests : PageObjectTestsBase
    {
        protected GlobalLocatorTests(ITestOutputHelper output) : base(output) { }

        public void Locate(string config)
        {
            InitializeFramework(config, Urls.HomePageUrl);

            BuildElementsList(GlobalLocators);

            Browser.Navigate($"{Urls.HomePagePromoCodeUrl}{AutomationFramework.Utilities.Payment.PromoCodeList.SilicusTest.Name}");

            VerifyElementDisplayed(() => GlobalLocators.Iframe);

            PromoCodeElementValidation();

            GetPlaForHomeLocatorTest();

            VerifyElementDisplayed(() => GlobalLocators.PlaAddToCartElement);

            Browser.Navigate(Urls.SignInPageUrl);

            Browser.Wait.ForClickableElement(SignIn.SignInButton);

            SignIn.SignInButton.Click(); //Verifies 'ErrorMessageElement' variable on SignIn.cs

            VerifyElementDisplayed(() => GlobalLocators.ErrorMessageElement);

            Browser.Navigate(Urls.ProductDetailPageUrl);

            VerifyElementDisplayed(() => GlobalLocators.AddToCartButton);

            PdpElementVerification();
            
            Browser.Wait.ForPage(Urls.CartOverviewPageUrl);

            CartElementVerification();

            ShippingPageDropdownValidation();

            Browser.Navigate(Urls.HomePageUrl);

            DrawerValidation();

            ProductDetail.NavigateToProductDetailByShortSku(ProductActions.GetSkuWithViewInRoomOnPdp);
            ProductDetail.ClickViewInYourRoomJs();

            Browser.Wait.ForDomReady();

            Browser.Wait.ForElement(GlobalLocators.LpModalCloseElement, 30);
            VerifyElementDisplayed(() => GlobalLocators.LpModalCloseElement);
            GlobalLocators.LpModalCloseElement.Click();
        }

        protected abstract void PromoCodeElementValidation();

        protected abstract void PdpElementVerification();

        protected abstract void CartElementVerification();

        protected abstract void DrawerValidation();

        protected abstract void ShippingPageDropdownValidation();
    }
}
