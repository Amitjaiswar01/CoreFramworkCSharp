using System;
using System.Collections.Generic;
using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.ProductDetail
{
    public interface IProductDetailDesktop : IPageObjectModel
    {
        bool IsAddToCartButtonVisible { get; }
        bool IsChatIconEnabled();
        bool IsNeedHelpModalVisible { get; }
        bool IsNeedHelpModalChatVisible { get; }
        bool IsQuantityBoxDisplayed();
        bool IsCallCustomerServiceBlockVisible { get; }
        bool IsEndsVerbiageVisible { get; }
        bool IsEndsVerbiageVisibleOnStickyHeader { get; }
        bool IsSaleVerbiageVisible { get; }
        bool IsSaleVerbiageVisibleOnStickyHeader { get; }
        bool IsOpenBoxVerbiageVisibleOnStickyHeader { get; }
        decimal GetDbProductPriceBySku(string sku);
        float GetProductPrice();
        string GetDbProductNameBySku(string sku);
        string GetProductPriceOnPdp();
        string GetProductSku();
        string GetShortSkuPrice();
        string GetProductQuantity();
        string GetProductCallOutQuantity();
        string GetBreadcrumbText();
        string GetProductInventory();
        string GetPayPalCalloutPDP();
        string CalculatePayPalInterestRate(decimal productPrice);
        string GetProductName();
        string GetOpenBoxCallout();
        string GetBuyItNewText();
        string GetOpenBoxAvailableLinkText();
        string GetSavedButtonCallout();
        string GetStrikeThroughPriceOnPdp();
        string GetStrikeThroughPriceOnStickyHeader();
        string GetSaveAmountOnPdp();
        string GetSaveAmountOnStickyHeader();
        string BuyItNewLinkText { get; }
        string SavedWishListAfterText { get; }
        string SavedWishListBeforeText { get; }
        decimal GetProductPriceOnStickyHeader();
        int GetNumberOfThumbnailImages();
        void SelectThumbnailImage(int indexOfThumbnail);
        void AddGiftCardDetails(string inputText);
        void OpenThumbnailModal();
        void SwitchToNewProduct();
        void SwitchToTheOpenBoxProduct();
        void SelectDifferentThumbnailInsideModal();
        void OpenCustomerPhotosTab();
        void AddProductMaxQuantity();
        void AddToCartIpad();
        void ClickOnViewInYourRoom();
        void SelectSampleArRoom();
        void AddMultipleProductsToRoom();
        void SwitchToIframe();
        void AddToWishList();
        void NavigateToProductDetailByShortSku(string shortSku);
        void ClickOnLastBreadcrumb();
        void ClickOnPrintIcon();
        void ClickOnPrintKioskStyleIcon();
        void AddToCart();
        void OpenShipInModal();
        void ClickOnPrintKioskStyleWithRoomScene();
        void NavigateToArPage();
        void OpenSampleRoomModal();
        void NavigateToMultiplePdps(int count);
        void AddSingleProductToCart(string sku);
        void ChangeProductQuantity(string quantity);
        void NavigateToEachProductDetailPage(IList<String> shortSkusList);
        void AddProductToCart(IList<string> skusList);
        void FocusCompleteLookSection();
        void NavigateToPlaPageByShortSku(string shortSku);
        void QnASearchByText(string text);
        void OpenProductHelpAndStoreAvailabilityModal();
        void CloseNeedHelpModal();
        void GetFirstResultFromAskQuestionSection();
        void OpenReviewsModal();
        void CloseReviewModal();
        void OpenReviewPhotoModal();
        void StickyNavAddToCart();
        void OpenBulbAndReplacementPartsModal();
        void ScrollToProductSpecificationTable();
        void NavigateToOpenBoxProductDetailByShortSku(string shortSku);
        void MoveToReviewsSection();
        void FocusOnFanFeaturesSection();
        void OpenEnergyGuide();
        void NavigateToRecentlyViewedPage();
        void OpenMakeAnAppointmentModal();
        void GetRelationshipWidgetSection();
        bool IsPaypalLaterWidgetDisplayed();
        IElement GetPayPalLogo();
        IElement GetWishListButton();
        IElement GetTurnToReviewSection();
        List<string> GetFreeShippingProductsSkus(List<string> listOfLinks);
        List<IElement> GetStickyNavContents();
        Dictionary<string, int> AddAllBuildFullSystemSkusToCart();
        Dictionary<string, int> AddAllBuildFullSystemSkusToWishList(int qtyCtr);
    }
}