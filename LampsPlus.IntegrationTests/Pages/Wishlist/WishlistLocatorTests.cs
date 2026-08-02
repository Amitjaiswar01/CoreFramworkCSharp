using System.Threading;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.IntegrationTests.Pages.Wishlist
{
    public class WishlistLocatorDesktopTest : WishlistLocatorTests
    {
        public WishlistLocatorDesktopTest(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Integration.PageObjectModel, "Wishlist")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LocateWishlistElementsTest(string config) => Locate(config);

        protected override void VerifyWishListElements()
        {
            // On Page Load
            VerifyElementDisplayed(() => WishList.WishListAddToCardBtns);
            VerifyElementDisplayed(() => WishList.FirstAddToCartBtn);
            VerifyElementDisplayed(() => WishList.ListOptionsView);
            VerifyElementDisplayed(() => WishList.WishListHeaderNameElement);
            VerifyElementDisplayed(() => WishList.WishListFreeShippingCalloutElement);
            VerifyElementDisplayed(() => WishList.WishListManageList);
            VerifyElementNotImplemented(() => WishList.MobileOptionsBtn);
            VerifyElementNotImplemented(() => WishList.MobileOptionsOpenElement);
            VerifyElementNotImplemented(() => WishList.MobileOptionsNewElement);
            VerifyElementNotImplemented(() => WishList.MobileOptionsRenameElement);
            VerifyElementNotImplemented(() => WishList.MobileOptionsDeleteElement);
            VerifyElementNotImplemented(() => WishList.MobileOptionsEmailLink);
            VerifyElementNotImplemented(() => WishList.MobileOptionsHelpElement);
            VerifyElementNotImplemented(() => WishList.WlSku);
            VerifyElementNotImplemented(() => WishList.WishlistMobileMenu);
            VerifyElementNotImplemented(() => WishList.WishlistMobileMenuCloseBtn);

            // View layouts - Desktop only
            VerifyElementDisplayed(() => WishList.GridViewButton);
            VerifyElementDisplayed(() => WishList.DetailsViewButton);
            VerifyElementDisplayed(() => WishList.CompareViewButton);

            WishList.DetailsViewButton.Click();
            WishList.CompareViewButton.Click();
            WishList.GridViewButton.Click();

            // Open
            WishList.ClickWishListOptions(WishListTypes.ManageList.Open);
            Browser.SwitchFocusToIframe(GlobalLocators.IframeModal);

            Browser.Wait.ForClickableElement(WishList.CancelOpenBtn);

            VerifyElementDisplayed(() => WishList.WishListOpenBtn);
            VerifyElementDisplayed(() => WishList.CancelOpenBtn);
            VerifyElementDisplayed(() => WishList.WishListItemsList);
            VerifyElementDisplayed(() => WishList.WishListItemNameElements);

            HandleClickForDesktopModalClose(WishList.CancelOpenBtn);

            // New
            WishList.ClickWishListOptions(WishListTypes.ManageList.New);
            Browser.SwitchFocusToIframe(GlobalLocators.IframeModal);
            Browser.Wait.ForDomReady(3000);

            Browser.Wait.ForClickableElement(WishList.CreateNewEmptyWishlistBtn, 5);

            VerifyElementDisplayed(() => WishList.CreateNewEmptyWishlistBtn);
            VerifyElementDisplayed(() => WishList.CancelNewBtn);
            VerifyElementNotImplemented(() => WishList.ConfirmNewWishlistBtn);
            HandleClickForDesktopModalClose(WishList.CancelNewBtn);

            // Rename
            WishList.ClickWishListOptions(WishListTypes.ManageList.Rename);
            Browser.SwitchFocusToIframe(GlobalLocators.IframeModal);

            Browser.Wait.ForClickableElement(WishList.CancelOpenBtn);

            VerifyElementDisplayed(() => WishList.WishListNameInputElement);
            VerifyElementDisplayed(() => WishList.CancelRenameBtn);
            VerifyElementDisplayed(() => WishList.SaveWishListRenameBtn);

            WishList.ClearWishListNameFieldText();
            WishList.WishListNameInputElement.SendKeys(WishListTypes.WishListNames.NewWishList);
            WishList.SaveWishListRenameBtn.Click();

            Browser.Wait.UntilElementUnloads(WishList.SaveWishListRenameBtn); // wait for animation

            // Delete
            WishList.ClickWishListOptions(WishListTypes.ManageList.Delete);
            VerifyElementDisplayed(() => WishList.CancelDeleteBtn);
            VerifyElementDisplayed(() => WishList.ConfirmDeleteWishlistBtn);
            HandleClickForDesktopModalClose(WishList.CancelDeleteBtn);

            // Print
            WishList.ClickWishListOptions(WishListTypes.ManageList.Print);
            Browser.SwitchFocusToIframe(GlobalLocators.IframeModal);

            Browser.Wait.ForClickableElement(WishList.PrintWithPricesBtn);

            VerifyElementDisplayed(() => WishList.PrintWithPricesBtn);
            VerifyElementDisplayed(() => WishList.PrintWithoutPricesBtn);
            VerifyElementDisplayed(() => WishList.CancelPrintBtn);
            HandleClickForDesktopModalClose(WishList.CancelPrintBtn);

            // Email
            WishList.ClickWishListOptions(WishListTypes.ManageList.Email);
            Browser.Wait.ForIframeDomReady(WishList.EmailWishlistIFrame);

            Browser.Wait.ForClickableElement(GlobalLocators.LpModalCloseElement);

            VerifyElementDisplayed(() => WishList.EmailWishlistIFrame);
            Browser.SwitchFocusToIframe(WishList.EmailWishlistIFrame);
            VerifyElementDisplayed(() => WishList.EmailWishlistForm);
            VerifyElementDisplayed(() => WishList.EmailWishlistFormFirstNameInput);
            VerifyElementDisplayed(() => WishList.EmailWishlistFormLastNameInput);
            VerifyElementDisplayed(() => WishList.EmailWishlistFormEmailInput);
            VerifyElementDisplayed(() => WishList.EmailWishlistFormZipcodeInput);
            VerifyElementDisplayed(() => WishList.EmailWishlistFormEmailOneInput);
            VerifyElementNotImplemented(() => WishList.EmailWishlistFormEmailTwoInput);
            VerifyElementNotImplemented(() => WishList.EmailWishlistFormEmailThreeInput);
            VerifyElementNotImplemented(() => WishList.EmailWishlistFormSendCopyCheckbox);
            VerifyElementDisplayed(() => WishList.EmailWishlistFormSpecialOffersCheckbox);
            VerifyElementDisplayed(() => WishList.EmailSendWishlistBtn);
            VerifyElementNotImplemented(() => WishList.EmailWishlistBackLink);
            Browser.SwitchToDefaultContent();
            HandleClickForDesktopModalClose(GlobalLocators.LpModalCloseElement);

            // Help
            WishList.ClickWishListOptions(WishListTypes.ManageList.Help);
            Browser.Wait.ForIframeDomReady(WishList.WishListHelpIFrame);

            Browser.Wait.ForClickableElement(GlobalLocators.LpModalCloseElement);

            VerifyElementDisplayed(() => WishList.WishListHelpIFrame);
            Browser.SwitchFocusToIframe(WishList.WishListHelpIFrame);
            VerifyElementDisplayed(() => WishList.WishListHelpContainerElement);
            VerifyElementNotImplemented(() => WishList.CancelHelpBtn);
            Browser.SwitchToDefaultContent();
            HandleClickForDesktopModalClose(GlobalLocators.LpModalCloseElement);
        }
    }

    public class WishlistLocatorMobileTest : WishlistLocatorTests
    {
        public WishlistLocatorMobileTest(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Integration.PageObjectModel, "Wishlist")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LocateWishlistElementsTest(string config) => Locate(config);

        protected override void VerifyWishListElements()
        {
            // On Page Load
            VerifyElementDisplayed(() => WishList.MobileOptionsBtn);
            VerifyElementDisplayed(() => WishList.WishListHeaderNameElement);
            VerifyElementDisplayed(() => WishList.WishListAddToCardBtns);
            VerifyElementDisplayed(() => WishList.FirstAddToCartBtn);
            VerifyElementDisplayed(() => WishList.WlSku);            
            VerifyElementNotImplemented(() => WishList.WishListFreeShippingCalloutElement);
            VerifyElementNotImplemented(() => WishList.ListOptionsView);
            VerifyElementNotImplemented(() => WishList.GridViewButton);
            VerifyElementNotImplemented(() => WishList.DetailsViewButton);
            VerifyElementNotImplemented(() => WishList.CompareViewButton);

            // Open options panel
            HandleClickForMobileElementClose(WishList.MobileOptionsBtn);

            VerifyElementDisplayed(() => WishList.MobileOptionsOpenElement);
            VerifyElementDisplayed(() => WishList.MobileOptionsNewElement);
            VerifyElementDisplayed(() => WishList.MobileOptionsRenameElement);
            VerifyElementDisplayed(() => WishList.MobileOptionsDeleteElement);
            VerifyElementDisplayed(() => WishList.MobileOptionsEmailLink);
            VerifyElementDisplayed(() => WishList.MobileOptionsHelpElement);

            // Email
            HandleClickForMobileElementClose(WishList.MobileOptionsEmailLink);

            VerifyElementDisplayed(() => WishList.EmailWishlistForm);
            VerifyElementDisplayed(() => WishList.EmailWishlistFormEmailOneInput);
            VerifyElementDisplayed(() => WishList.EmailWishlistFormEmailTwoInput);
            VerifyElementDisplayed(() => WishList.EmailWishlistFormEmailThreeInput);
            VerifyElementDisplayed(() => WishList.EmailWishlistFormFirstNameInput);
            VerifyElementDisplayed(() => WishList.EmailWishlistFormLastNameInput);
            VerifyElementDisplayed(() => WishList.EmailWishlistFormEmailInput);
            VerifyElementDisplayed(() => WishList.EmailWishlistFormZipcodeInput);
            VerifyElementDisplayed(() => WishList.EmailWishlistFormSendCopyCheckbox);
            VerifyElementDisplayed(() => WishList.EmailWishlistFormSpecialOffersCheckbox);
            VerifyElementDisplayed(() => WishList.EmailSendWishlistBtn);
            VerifyElementDisplayed(() => WishList.EmailWishlistBackLink);
            VerifyElementNotImplemented(() => WishList.EmailWishlistIFrame);
            WishList.EmailWishlistBackLink.Click();

            // Open options panel on page refresh
            HandleClickForMobileElementClose(WishList.MobileOptionsBtn);

            // Print - There is no Print option for mobile
            VerifyElementNotImplemented(() => WishList.PrintWithPricesBtn);
            VerifyElementNotImplemented(() => WishList.PrintWithoutPricesBtn);
            VerifyElementNotImplemented(() => WishList.CancelPrintBtn);

            // New
            HandleClickForMobileElementClose(WishList.MobileOptionsNewElement);
            VerifyElementDisplayed(() => WishList.ConfirmNewWishlistBtn);
            VerifyElementDisplayed(() => WishList.CancelNewBtn);
            VerifyElementNotImplemented(() => WishList.CreateNewEmptyWishlistBtn);
            WishList.CancelNewBtn.Click();

            // Delete
            HandleClickForMobileElementClose(WishList.MobileOptionsDeleteElement);
            VerifyElementDisplayed(() => WishList.ConfirmDeleteWishlistBtn);
            VerifyElementDisplayed(() => WishList.CancelDeleteBtn);
            HandleClickForMobileElementClose(WishList.CancelDeleteBtn);

            // Open
            HandleClickForMobileElementClose(WishList.MobileOptionsOpenElement);
            VerifyElementDisplayed(() => WishList.WishListItemsList);
            VerifyElementDisplayed(() => WishList.WishListItemNameElements);
            VerifyElementDisplayed(() => WishList.CancelOpenBtn);
            VerifyElementNotImplemented(() => WishList.WishListOpenBtn);
            VerifyElementsNotImplemented(() => WishList.WishListManageList);
            WishList.CancelOpenBtn.Click();
            //
            VerifyElementDisplayed(() => WishList.WishlistMobileMenu);
            VerifyElementDisplayed(() => WishList.WishlistMobileMenuCloseBtn);

            // Help
            HandleClickForMobileElementClose(WishList.MobileOptionsHelpElement);
            VerifyElementDisplayed(() => WishList.WishListHelpContainerElement);
            VerifyElementDisplayed(() => WishList.CancelHelpBtn);
            VerifyElementNotImplemented(() => WishList.WishListHelpIFrame);
            WishList.CancelHelpBtn.Click();

            // Rename
            HandleClickForMobileElementClose(WishList.MobileOptionsRenameElement);
            WishList.ClearRenameWishListNameFieldText();
            WishList.WishListNameInputElement.SendKeys(WishListTypes.WishListNames.NewWishList);
            VerifyElementDisplayed(() => WishList.WishListNameInputElement);
            VerifyElementDisplayed(() => WishList.CancelRenameBtn);
            VerifyElementDisplayed(() => WishList.SaveWishListRenameBtn);
            WishList.SaveWishListRenameBtn.Click();
        }
    }

    /// <summary>
    /// Tests to ensure all IElements and Lists of IElements can be found on the given page object.
    /// </summary>
    [Trait(LpTraits.Integration.PageObjectModel, "Wishlist")]
    public abstract class WishlistLocatorTests : PageObjectTestsBase
    {
        protected WishlistLocatorTests(ITestOutputHelper output) : base(output) { }
        
        public void Locate(string config)
        {
            InitializeFramework(config);
            BuildElementsList(WishList);

            WishListWorkflow.EmptyWishList();
            WishListWorkflow.AddSingleItemToWishList();

            Browser.Navigate(Urls.WishListPageUrl);

            VerifyElementDisplayed(() => WishList.AddAllToCartBtn);

            VerifyWishListElements();
        }

        protected abstract void VerifyWishListElements();
    }
}
