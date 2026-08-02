using System.Collections.Generic;
using System.Collections.ObjectModel;
using Automation.Framework;
using Automation.Framework.Utilities;

namespace LampsPlus.AutomationFramework.Pages
{
    /// <summary>
    /// Base class for common behavior between desktop and mobile views.
    /// </summary>
    public interface IProductDetail
    {
        #region Class Setup
        string AvailabilityPhoneNumberString { get; }
        string AvailabilityString { get; }
        string AvailabilityTextString { get; }
        string Chandeliers { get; }
        string GiftCardDenominationXpath { get; }
        string HousingOptionsString { get; }
        string InStockCaps { get; }
        string InStockNonCaps { get; }
        string PdCheckStoreAvailabilityId { get; }
        string PdRelItmsContainerId { get; }
        string PriceWithoutText { get; }
        string ProductReviewUrlFragment { get; }
        string ProsSpecialPriceLabel { get; }
        string PdPleaseCallClass { get; }
        string ShipsIn { get; }
        string SkuStatusLabel { get; }
        string SkuOnPdpXpath { get; }
        string StockCheckXpath { get; }
        string ToOrderCallout { get; }
        string ToOrderCalloutString { get; }
        string ToOrderString { get; }
        string ForAvailabilityText { get; }
        string ForAvailabilityPhone { get; }
        string ForAvailabilityCallout { get; }
        string ForAvailabilityCall { get; }
        string ForAvailability { get; }
        string ForAvailabilityCallText { get; }
        string TurnTwoQuestionTextInputLabelTextAttribute { get; }
        string TurnTwoSampleQuestionText { get; }
        string BreadCrumbXpath { get; }
        string RecentlyViewContainerId { get; }
        string ShowInRoomBtnId { get; }
        string PdpArIframeXpath { get; }
        string ArViewerBtnClass { get; }
        string GetStartedBtnClass { get; }
        string ProductPriceId { get; }
        string ProductSpecificationString { get; }
        string ProductAttributeString { get; }
        string StoreRadioButtonSelector { get; }
        #endregion

        #region CSS Selector Strings                
        string AddToWishListButtonXpath { get; }
        string AdjacentButtonClass { get; }
        string AppCheckStoreAvailabilityClass { get; }
        string AssetActionsClass { get; }
        string BcChatContainerId { get; }
        string BtnPdpZoomClass { get; }
        string BuildFullSystemContainerId { get; }
        string BuildFullSystemId { get; }
        string BuyItNewLinkClass { get; }
        string CallStoreButtonClass { get; }
        string CheckStoreModalFirstNameId { get; }
        string DivProductDetailTop { get; }
        string TurnToQuestionAndAnswerSection { get; }
        string EnergyInfoModalId { get; }
        string EnergyGuideIconId { get; }
        string FsImageContainerId { get; }
        string JsCertonaTitleClass { get; }
        string JsOtherOptionLinkClass { get; }
        string LblPriceId { get; }
        string LblFreeReturnsBottomId { get; }
        string LblStickyPriceId { get; }
        string LimitedQtyFieldClass { get; }
        string LimitedQuantitySelectionId { get; }
        string LpContainerId { get; }
        string LpCollapsibleCollapsedHidden { get; }
        string ProductReviewsSectionId { get; }
        string LpMobileAccordionClass { get; }
        string MoreYouLikeBorderClass { get; }
        string LpModalContentId { get; }
        string MainImagePathXpath { get; }
        string ModalProductImageThumbnailXpath { get; }
        string NotifyMeMessageContainerSuccessClass { get; }
        string OpenBoxAvailableLinkId { get; }
        string OverlayContentWrapperCloseButtonClass { get; }
        string PdAddToCartBuildFullId { get; }
        string PdAddToCartStickyId { get; }
        string PdpAddToWishlistClass { get; }
        string PdAddToPortfolioNormalId { get; }
        string PdAddToPortfolioSystemOptionsId { get; }
        string PdHeroSpotId { get; }
        string pdImgContainerId { get; }
        string PdProdImgClass { get; }
        string PdProdImgStickyId { get; }
        string PdProdSkuId { get; }
        string PdProdTitleStickyClass { get; }
        string PdFanFeatures { get; }
        string PdRelatedItmsId { get; }
        string PdRelatedItmsXpath { get; }
        string PdReviewsId { get; }
        string PdRelVideosId { get; }
        string PdViewFullTrackSystemId { get; }
        string PdFirstRelatedItmXpath { get; }
        string PdpStickyHeaderId { get; }
        string PdpStickyHeaderImageWrapperClass { get; }
        string PdHeroImageId { get; }
        string PnlProductDescriptionId { get; }
        string PopularColorsId { get; }
        string PrintKioskStyleProductBtnId { get; }
        string PrintKioskStyleButtonId { get; }
        string ProductAttributesClass { get; }
        string ProductSkuId { get; }
        string ProductNameAndNumber { get; }
        string ProductDescId { get; }
        string ProductDescSelector { get; }
        string ProductDetailsSectionId { get; }
        string ProductImageThumbnailId { get; }
        string ProductReviewsCollapsibleSelector { get; }
        string ProductReviewRatingStarCountClass { get; }
        string ProductTechnicalSpecificationsClass { get; }
        string GoodToKnowClass { get; }
        string QandAId { get; }
        string QtyLimitedName { get; }
        string QtyNormalId { get; }
        string QuantityDrawerXpath { get; }
        string RelatedItemsContainerXpath { get; }
        string RelatedItemSectionXpath { get; }
        string ReviewsSectionXpath { get; }
        string ReviewsElementSelector { get; }
        string SearchZipInputId { get; }
        string SearchResultsListClass { get; }
        string ShipsInMessageClass { get; }
        string SlickActiveClass { get; }
        string StickyWrapperId { get; }
        string SlickListClass { get; }
        string StoreAvailabilityLocatorContentClass { get; }
        string StoreAvailabilityClass { get; }
        string StoreAvailabilityQuestionsClass { get; }
        string SystemOptionsQtyClass { get; }
        string ThumbnailImageCarouselClass { get; }
        string ThumbnailImageCarouselId { get; }
        string TextAttributeValue { get; }
        string TitleAttributeValue { get; }
        string ToOrderCalloutClass { get; }
        string ToOrderCallCalloutOnOrdersOver49Class { get; }
        string ProsSpecialPriceCallOutClass { get; }
        string Tt4QProductImgClass { get; }
        string TtLeftHeaderClass { get; }
        string TtrespMobileDispInlineClass { get; }
        string TtOverallRatingStarsId { get; }
        string TtWriteReviewBtnPortraitId { get; }
        string WishListIndicatorString { get; }
        string SelectStoreClass { get; }
        string StoreAssociateId { get; }
        string StoreAssociateModalClass { get; }
        string CheckStoreChooseAnotherStateOrZipClass { get; }
        string ProsRetailPriceClass { get; }
        string ProsTradePriceId { get; }
        string ProsSavingId { get; }
        string YourTradePriceClass { get; }
        string RecentlyViewedLoadedClass { get; }
        string RecentlyViewedXPath { get; }
        string OrigPriceClass { get; }
        string StickyCallOutClass { get; }
        string StickySaveXpath { get; }
        string StickyPriceClass { get; }
        string EndVerbiageSfpAndPlaClass { get; }
        string MobileStruckPriceSfpXpath { get; }
        string StickyContainerSfpClass { get; }
        string ProductReviewCardClass {get;}
        string MediaModalContentModalClass { get; }
        string LoadMoreReviewsBtnClass { get; }
        string ReplacementPartModalClass { get; }
        string StickySaleClass { get; }
        string GoodtoKnowIconClass { get; }
        string ShopAllColorText { get; }
        string EmailRecipientListClass { get; }
        string DesignChatClass { get; }
        string NeedHelpChatClass { get; }
        string CloseNeedHelpClass { get; }
        #endregion

        #region Page Elements
        IElement GetAllColorPlusElement { get; }
        IElement StickySaleCallout { get; }
        IElement BuildFullSystemTitle { get; }
        IElement RecentlyViewedLoaded { get; }
        IElement RecentlyViewed { get; }
        IElement RecentlyViewedContainer { get; }
        IElement YourTradePrice { get; }
        IElement ProsRetailPrice { get; }
        IElement ProsTradePrice { get; }
        IElement ProsSaving { get; }
        IElement StoreAssociateModal { get; }
        IElement MediaModalContentModal { get; }
        IElement StickyHeaderPrice { get; }
        IElement StoreAssociate { get; }
        IElement SelectStore { get; }
        IElement CallStoreButton { get; }
        IElement CityTitle { get; }
        IElement TextStoreButton { get; }
        IElement OpenBoxAvailableLink { get; }
        IElement ActiveMainProductImage { get; }
        IElement AddToCartLabelElement { get; }
        IElement AskStoreAssociate { get; }
        IElement BottomPortionOfPdp { get; }
        IElement BuildFullSystemButton { get; }
        IElement BuildFullSystemProductContainer { get; }
        IElement CarouselImage(int index);
        IElement CertonaDrawerName { get; }
        IElement CheckAvailabilityModal { get; }
        IElement CheckAvailabilityStoreList { get; }
        IElement CheckAvailabilityModalCloseButton { get; }
        IElement CheckStoreFirstName { get; }
        IElement CheckStoreCallButton { get; }
        IElement CheckStoreChooseAnotherStateOrZip { get; }
        IElement CheckStorePhone {get;}
        IElement CheckStoreQuestion { get; }
        IElement CheckStoreReserveItemButton { get; }
        IElement CheckStoreReserveItemButtonBottom { get; }
        IElement CheckStoreSearchButton { get; }
        IElement CheckStoreSearchArrow { get; }
        IElement CsInfo { get; }
        IElement CustomerPhotos { get; }
        IElement EmailLink { get; }
        IElement EmailModalContent { get; }
        IElement EmailRecipientTextbox { get; }
        IElement EmailRecipientList { get; }
        IElement FanFeatures { get; }
        IElement FirstNameTextbox { get; }
        IElement GiftCardDenomination(int index);
        IElement GiftCardFirstName { get; }
        IElement GiftCardLastName { get; }
        IElement GiftCardMessage { get; }
        IElement HeaderChatLink { get; }
        IElement LastNameTextbox { get; }
        IElement MainProductImage { get; }
        IElement MobileAddToCartButtonContainer { get; }
        IElement MobileGiftCardDenomination { get; }
        IElement MobileMaxQuantity { get; }
        IElement MoreYouMayLikeContainer { get; }
        IElement PdImageColumn { get; }
        IElement ProductHeroThumbnail { get; }
        IElement CustomerPhotosThumbnail { get; }
        IElement ProductHeroImage { get; }
        IElement ProductCustomerPhotos { get; }
        IElement MoreImages(int index);
        IElement FromEmailTextbox { get; }
        IElement ZipcodeTextbox { get; }
        IElement StockCheck { get; }
        IElement StockCheckElement { get; }
        IElement StockCheckWrapper { get; }
        IElement GoodToKnow { get; }
        IElement StoreAvailabilityLocatorContent { get; }
        IElement ProductQuestionAnswerArrow { get; }
        IElement SearchResultsList { get; }
        IElement ProsSpecialPriceCallout { get;  }
        IElement SendEmailButton { get; }
        IElement SelectedThumbnailWrapper { get; }
        IElement AddToWishListButton { get; }
        IElement ComparePriceCallout { get; }
        IElement BoldChatButtonContainer { get; }
        IElement VirtualAssistantContainer { get; }
        IElement VirtualAssistantCloseButton { get; }
        IElement VirtualAssistantCloseConfirmationButton { get; }
        IElement BoldChatStartBtn { get; }
        IElement BoldChatFrame { get; }
        IElement BrandLogo { get; }
        IElement BrandLogoLink { get; }
        IElement BreadCrumbElement { get; }
        IElement BuildFullSystemAddToWishListButton { get; }
        IElement BuildFullSystemAddToCartButton { get; }
        IElement BuildFullSystemContainer { get; }
        IElement BuildFullSystemOptions { get; }
        IElement BuyItNewLink { get; }
        IElement ChatButtonLink { get; }
        IElement DesignChatLink { get; }
        IElement ProductHelp { get; }
        IElement NeedHelpChat { get; }
        IElement CloseNeedHelp { get; }
        IElement CheckStoreAvailabilityModal { get; }
        IElement EnergyGuideIcon { get; }
        IElement EnergyInfoModal { get; }
        IElement FreeShippingAndReturnElement { get; }
        IElement FreeShippingToStatesWithStoresLabel { get; }
        IElement FreeShippingCallout { get; }
        IElement FooterChatLink { get; }
        IElement GetYourPhotoFrame { get; }
        IElement HousingOptions { get; }
        IElement HousingOptionsSectionHeader { get; }
        IElement ItemPrice { get; }
        IElement ImageContainer { get; }
        IElement LblStockInventory { get; }
        IElement LongSkuElement { get; }
        IElement ManufacturerLink { get; }
        IElement MainImagePath(int index);
        IElement MarginModalLink { get; }
        IElement MobileAccordionContainer { get; }
        IElement ModalProductImageThumbnail { get; }
        IElement PdHeroSpot { get; }
        IElement PdMymlSection { get; }
        IElement PdMymlSectionItem { get; }
        IElement PdRecentlyViewedSectionItem { get; }
        IElement PdReviewsElement { get; }
        IElement PopularColorsDropdown { get; }
        IElement ProductDetailPageContainer { get; }
        IElement ProductDetailSpecificationSection { get; }
        IElement ProductGoodToKnowSection { get; }
        IElement ProductReviewsSection { get; }
        IElement ProductReviewSweepstake { get; }
        IElement ProductSpecificationsTables { get; }
        IElement OrigPrice { get; }
        IElement PdChat { get; }
        IElement PdpAddToWishlist { get; }
        IElement pdImgContainer { get; }
        IElement PdSocialIconElement { get; }
        IElement PdSocialPrintIconElement { get; }
        IElement PdProdInfoColElement { get; }
        IElement PdProdSpecificationsTables { get; }
        IElement VideoWindow { get; }
        IElement Price { get; }
        IElement PriceAdditionalSave { get; }
        IElement ProductImage { get; }
        IElement ProductImageThumbnail { get; }
        IElement PriceType { get; }
        IElement PrintKioskStyleButtonElement { get; }
        IElement PrintKioskStyleProductBtnElement { get; }
        IElement ProductAttributes { get; }
        IElement ProductDescDropDown { get; }
        IElement ProductDetailSection { get; }
        IElement RelatedItemDropdown { get; }
        IElement ProductQtyCallOut { get; }
        IElement ProductSkuLabel { get; }
        IElement ProductReviewModal { get; }
        IElement ProductInStockTextLink { get; }
        IElement ProductSlider { get; }
        IElement QuantityField { get; }
        IElement QuestionsAndAnswersChatLink { get; }
        IElement QuestionsAndAnswersChatContainer { get; }
        IElement QuestionsAndAnswersCommentsSection { get; }
        IElement QuickPrintInput { get; }
        IElement QuickPrintLink { get; }
        IElement RelatedItemsSection { get; }
        IElement RelatedItemSection { get; }
        IElement ReviewsSection { get; }
        IElement ReplacementPartLink { get; }
        IElement SamplePhotosTab { get; }
        IElement SearchZipTextBox { get; }
        IElement ShipsFreeWithOrdersOver49CallOut { get; }
        IElement SampleRoomBtn { get; }
        IElement StoreInventoryElement { get; }
        IElement TradePriceLabel { get; }
        IElement SocialLinksContainer { get; }
        IElement StickyWrapper { get; }
        IElement StickyTitle { get; }
        IElement StickyImageWrapper { get; }
        IElement StickyImage { get; }
        IElement StickyAddToCart { get; }
        IElement StickyPrice { get; }
        IElement StoreAvailabilityQuestions { get; }
        IElement SingleQuestionAndAnswerElementResult { get; }
        IElement ThumbnailWrapper { get; }
        IElement ThumbnailCarouselImage { get; }
        IElement ToOrderCallCalloutDesktop { get; }
        IElement ToOrderCalloutOnPdp { get; }
        IElement ToOrderCallCalloutOnOrdersOver49Phone { get; }
        IElement ToOrderCallCalloutOnOrdersOver49PhoneLink { get; }
        IElement ToOrderCallCalloutOnOrdersOver49ForAvailabilityText { get; }
        IElement TopContentProductDetail { get; }
        IElement YourSavingsCallout { get; }
		IElement TurnTwoAskAQuestionCloseButton { get; }
		IElement TurnTwoAskAQuestionTextArea { get; }
        IElement TurnTwoBrowseQaWrapper { get; }
        IElement TtQnASearchBar { get; }
        IElement TtProductSearchResults { get; }
        IElement LoadMoreReviews { get; }
        IElement TtProductSearchResultCards(int index);
        IElement ReviewImage(int index);
        IElement TurnToReviewAddNewPhotoButton { get; }
		IElement TurnToReviewAttachPhoto { get; }
		IElement TurnToReviewMediaSubmitButton { get; }
        IElement TurnToWriteReviewButton { get; }
        IElement TurnToQuestionAndAnswerContainer { get; }
        IElement TurnToQuestionsAndAnswersSection { get; }
        IElement TurnToReviewSection { get; }
        IElement TurnToReviewFileInput { get; }
        IElement TurnToReviewFileMediaListSelected { get; }        
        IElement TurnToReviewProductImage { get; }
		IElement TurnToReviewProductName { get; }
		IElement TurnToReviewRating { get; }        
        IElement TurnToReviewScreen { get; }
        IElement TurnToReviewModal { get; }
        IElement TurnToReviewShareMediaScreen { get; }
        IElement TurnToSubmitAnswerButton { get; }
        IElement TurnToReviewText { get; }
		IElement TurnToReviewTitle { get; }
		IElement TurnToReviewWindow { get; }
		IElement TurnToDynamicAddAnswerButton { get; }
		IElement TurnToDynamicAddAnswerTextArea { get; }
		IElement TurnToDynamicAddQuestionsCancelButton { get; }
		IElement RelatedItemAnchor { get; }
		IElement ManufacturerLinkAnchor { get; }
		IElement PdRelItmsContainer { get; }
		IElement ProductCallOut { get; }
		IElement RelatedItemsContainer { get; }
		IElement StoreAvailability { get; }
	    IElement LimitedQtyField { get; }
	    IElement LimitedQuantitySelection { get; }
        IElement ProductDescriptionAccordion { get; }
        IElement RelatedVideo { get; }
        IElement PdRelVideosContainer { get; }
        IElement RecentlyViewedViewAllButton { get; }
        IElement WishListIndicatorIcon { get; }
        IElement ZipInputFieldCheckStore { get; }
        IElement ZoomIcon { get; } 
        IElement StickySaveCallout { get; }
        IElement EndsDate(int index);
        IElement ProductReviewCard(int index);
        IElement StickyPriceCallout { get; }
        IElement StickyCallOut { get; }
        IElement EndVerbiagePlaAndSfp { get; }
        IElement EndVerbiageOnSfp { get; }
        IElement MobileStruckPriceSfp { get; }
        IElement StickyContainerSfp { get; }
        IElement ReplacementProductPartModal { get; }
        IElement WriteReviewBtn { get; }
        IElement SoldOutLabel { get; }
        IElement ShowInRoomBtn { get; }
        IElement PdpArIframe { get; }
        IElement ArViewerBtn(int index);
        IElement GetStartedBtn { get; }
        IElement InStockElement { get; }

        ReadOnlyCollection<IElement> BuildFullSystemQtyElements { get; }
		ReadOnlyCollection<IElement> BuildFullSystemShortSkuLinks { get; }
		ReadOnlyCollection<IElement> HousingOptionsSectionDivContainers { get; }
		ReadOnlyCollection<IElement> ListOfFullSystemProductNames { get; }
		ReadOnlyCollection<IElement> ListOfFullSystemSkus { get; }
        ReadOnlyCollection<IElement> MoreThumbnailImage { get; }
        ReadOnlyCollection<IElement> GoodToKnowIcon { get; }
        ReadOnlyCollection<IElement> ProductSliders { get; }
		ReadOnlyCollection<IElement> RelatedItems { get; }
		ReadOnlyCollection<IElement> SamplePhotos { get; }
        ReadOnlyCollection<IElement> ThumbnailImageCarousel { get; }
        ReadOnlyCollection<IElement> ReplacementPartSku { get; }
        #endregion

        /// <summary>
        /// Does the given page have http://www.lampsplus.com/products in the URL?
        /// </summary>
        bool IsProductDetailPage { get; }

		/// <summary>
		/// Is the Bold Chat close icon immediately visible?
		/// </summary>
		bool IsVirtualAssistantCloseIconVisible { get; }

		/// <summary>
		/// Is the chat button link immediately visible?
		/// </summary>
		bool IsChatButtonLinkVisible { get; }

		/// <summary>
		/// Is the compare callout element immediately visible?
		/// </summary>
		bool IsCompareCalloutElementVisible { get; }

		/// <summary>
		/// Is the margin link immediately visible?
		/// </summary>
		bool IsMarginLinkVisible { get; }

        /// <summary>
        /// Is the shop All link element immediately visible?
        /// </summary>
        bool IsShopAllLinkVisible { get; }

        /// <summary>
        /// Is the replacement part modal immediately visible?
        /// </summary>
        bool IsReplacementPartModalVisible { get; }

        /// <summary>
        /// Is the quick print link element immediately visible?
        /// </summary>
        /// 
        bool IsQuestionAskStoreAssociateLink { get; }

        /// <summary>
        /// Is Question?AskStoreAssociate link element immediately visible?
        /// </summary> 
         
        bool IsCheckCompareCallOut { get; }
        /// <summary>
        /// Is Compare Call out link element  visible?
        /// </summary> 
        
        bool IsCheckEndDateCallOut { get; }
        /// <summary>
        /// Is End link element link visible?
        /// </summary> 
        
        bool IsQuickPrintLinkVisible { get; }

		/// <summary>
		/// Is the quick print input element immediately visible?
		/// </summary>
		bool IsQuickPrintInputVisible { get; }

		/// <summary>
		/// Is the single question and answer element immediately visible?
		/// </summary>
		bool IsSingleQuestionAndAnswerElementVisible { get; }

        /// <summary>
        /// Get the TurnTwo question id.
        /// </summary>

        bool IsCheckStoreAvailabilityLinkVisible { get; }

        /// <summary>
        /// Is the Check store availability link is immediately visible?
        /// </summary>

        bool IsEndDateVerbiageVisible { get; }
        bool IsLoggedInAsKiosk { get; }
        bool IsPriceVerbiageVisible { get; }
        bool IsSaleVerbiageVisible { get; }
        bool IsSavePriceAndVerbiageVisible { get; }
        bool IsStrikeThroughVisible { get; }
        bool IsReplacementPartLinkVisible { get; }
        string EndVerbiageOnSfpStickyXpath { get; }
        string TurnTwoQuestionId { get; }
        string BuildFullSystemSectionTitle { get; }
		string BuildFullSystemTableTitle { get; }
		string BuildFullSystemTableFirstSku { get; }
        string CustomerThumbnailImagePath { get; }
        string CustomerMainImagePath { get; }
        string ItemPriceText { get; }
        string SaleItemPriceText { get; }
        string QuantityLeft { get; }
		string MaxAvailableQuantity { get; }
        string ModalMainImagePath { get; }
        string MoreOptionsString { get; }
        string ModalThumbnailImagePath { get; }
        string ModalThumbnailImageSrc { get; }
        string ModalCustomerPhotosThumbnailSrc { get; }
        string ModalMainImageSrc { get; }
        string ModalCustomerPhotosSrc { get; }
        string OtherOptionsString { get; }
        string ProductComparePrice { get; }
        string ProductName { get; }
		string ProductSalePrice { get; }
        string ProductSkuNumberWithoutPrefix { get; }
        string GetTitleSku { get; }
		string RelatedItemUrl { get; }
		string RelatedItemSku { get; }
        string ReplacementPartLinkId { get; }
        string ReplacementPartSkuXpath { get; }
		string SkuOnPdp { get; }
        string StruckThroughPrice { get; }
        string ModalDiffrentThumbnailImagePath { get; }
        string StoreTitle { get; }
        string StoreName { get; }
        string ProductSkuNumber { get; }
		string ProductNameWithSku { get; }
        string ProductSaleEndDateText { get; }
		string QuickPrintLpModalPrice { get; }
	    string QuickPrintLpModalProductName { get; }
        decimal ProductPrice { get; }
        string ProductImagePath { get; }
        string ProductImageUrl { get; }
        string ProductThumbnailImagePath { get; }
        string ProductSkuNumberWithoutPrefixOnCheckStore { get; }
        string PdMymlSectionId { get; }
        string GiftCardFirstNameId { get; }
        string SaleVerbiageClass { get; }
        string SampleRoomBtnClass { get; }
        string ViewInYourRoomXpath { get; }
        string WriteReviewBtnSelector { get; }
        string WriteReviewBtnXpath { get; }
        string WriteReviewModalXpath { get; }
        string WriteReviewModalSelector { get; }
        string WriteReviewModalCloseCssSelector { get; }
        string ViewInYourRoomSampleImageXpath { get; }
        string ViewInYourRoomSelectPhotoXpath { get; }
        string ReviewBtnSelector { get; }
        string CheckStoreChooseAnotherStateOrZipXpath { get; }
        string GiftCardValue { get; }
        string TwentyFiveDollarGiftCardString { get; }

        (string textMessage, string phoneNumber) GetTxtMessageAndPhoneNumber();

        /// <summary>
        /// Get a list of full system skus.
        /// </summary>
        List<string> GetListOfFullSystemSkus { get; }

		/// <summary>
		/// Get a list of full system product names.
		/// </summary>
		List<string> GetListOfFullSystemProductNames { get; }

        /// <summary>
        /// Get list of full system quantities.
        /// </summary>
        List<int> GetListOfFullSystemQuantities { get; }

		/// <summary>
		/// Get SKUs for housing options selection.
		/// </summary>
		List<string> GetSkusFromHousingOptionsSection { get; }

        /// <summary>
        /// Does the product have no reviews yet?
        /// </summary>
        bool HasTurnToReviews { get; }

		/// <summary>
		/// Is the LP modal immediately displayed.
		/// </summary>
		bool IsLpModalDisplayed { get; }

		/// <summary>
		/// Is there any quantity left?
		/// </summary>
		bool IsQuantityLeftShows { get; }

        /// <summary>
        /// Get the trade savings price.
        /// </summary>
        decimal YourSavingsPrice { get; }

		/// <summary>
		/// Get the trade price.
		/// </summary>
		decimal TradePrice { get; }

		/// <summary>
		/// Log class to update log messages.
		/// </summary>
		Log Log { get; }

		/// <summary>
		/// Instance of a Browser to enable browser specific UI testing.
		/// </summary>
		IBrowser Browser { get; }

        ReadOnlyCollection<IElement> ListOfBreadCrumbLink();
		ReadOnlyCollection<IElement> ListOfFullSystemData(int nthIndex);

		/// <summary>
		/// Navigate to a pdp of the given SKU.
		/// </summary>
		void NavigateToProductDetailByShortSku(string shortSku);

        /// <summary>
        /// Navigate to Open Box PDP
        /// </summary>
        void NavigateToOpenBoxProductDetailByShortSku(string shortSku);


        /// <summary>
        /// Wait for the page to navigate to a "PDP" page.
        /// </summary>
        void WaitForPdpToLoad();

		/// <summary>
		/// Return scrollable header element by name
		/// </summary>
		/// <param name="name">Element name</param>
		/// <returns>Scrollable header element</returns>
		IElement GetScrollableHeaderByName(string name);

        /// <summary>
        /// Return course title element by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IElement GetCourseTitleByName(string name);

        /// <summary>
        /// Go to the last breadcrumb on the page.
        /// </summary>
        void ClickOnLastBreadcrumb();

		/// <summary>
		/// Add max quantity to the cart.
		/// </summary>
		void AddMaxQuantityToCart();

		/// <summary>
		/// Close the Bold Chat modal.
		/// </summary>
		void CloseVirtualAssistant();

		/// <summary>
		/// Check store availability link for the given product.
		/// </summary>
		void ProductCheckStoreAvailabilityLink();

		/// <summary>
		/// Click the breadcrumb by the given index.
		/// </summary>
		/// <param name="index">Index of the breadcrumb to click.</param>
		void ClickBreadCrumbByIndex(int index);

		/// <summary>
		/// Wait for 1 second and switch to the email product frame.
		/// </summary>
		void SwitchToTheEmailProductFrame();

		/// <summary>
		/// Wait for 1 second and switch to modal frame
		/// </summary>
		void SwitchToModalIframe();
		
		/// <summary>
		/// Add all build full system SKUs to the WishList.
		/// </summary>
		/// <returns></returns>
		Dictionary<string, int> AddAllBuildFullSystemSkusToWishList();

		/// <summary>
		/// Add all build full system SKUs to the Cart.
		/// </summary>
		/// <returns></returns>
		Dictionary<string, int> AddAllBuildFullSystemSkusToCart();

		/// <summary>
		/// Type information into the question and answers text area.
		/// </summary>
		/// <param name="sampleText">Text to enter as a question.</param>
		void TypeIntoQAndATextarea(string sampleText);


        /// <summary>
        /// Click TurnTo Write A Review
        /// </summary>
        void ClickTurnToWriteReview(bool isiPhoneTest= false);

        /// <summary>
        /// Complete TurnTo Write A Review
        /// </summary>
        void CompleteTurnToWriteReview();


        /// <summary>
        /// Focus On TurnTo Question and Answer
        /// </summary>
        void FocusOnTurnToQAndA();

        /// <summary>
        /// Get the price of a SKU on the PDP.
        /// </summary>
        /// <returns></returns>
        string GetProductPrice();

        /// <summary>
        /// Press the close button on the question and answer text area.
        /// </summary>
        void PressCloseButtonOnQAndATextarea();

		/// <summary>
		/// Add answer to a question.
		/// </summary>
		/// <param name="sampleText">Answer to add to a question.</param>
		void AddAnswerToQuestion(string sampleText);

		/// <summary>
		/// Returns SKU Status displayed on the Product Detail Page
		/// </summary>
		/// <returns></returns>
		string SkuStatusValue();

        /// <summary>
        /// Returns SKU Customer Photo displayed on the Product Detail Page
        /// </summary>
        string GetRandomSku(List<string> shortSku);

        /// <summary>
        /// Navigate to the given URL.
        /// </summary>
        /// <param name="url">URL to navigate to. This must be begin with http:// or https://.</param>
        void Navigate(string url);

        /// <summary>
        /// Force hides sticky header
        /// </summary>
	    void ForceHideStickyHeader();

        /// <summary>
        /// Clicks TurnTo Write A Review button
        /// </summary>
        void ClickTurnToWriteReviewButtonJs();

        /// <summary>
        /// Verifies the Callouts for Availabilty according to Time
        /// </summary>
        bool TimeVerifyCheck(string availabilityCallout1, string availabilityCallout2);

        /// <summary>
        /// Verifies the Callouts for Availabilty according to Time
        /// </summary>
        bool TimeVerifyCheckMobile(string textChatExpected, string textChatActual, string phoneExpected, string phoneActual, string availabilityTextExpected, string availabilityTextActual);

        /// <summary>
        /// Verifies the Time Range for Ask A Store Associate
        /// </summary>
        bool IsWeekday();
        void ClickPhotoModal(int productReviewCounter, int pixelsScroll, int reviewClassNotFound, int endCondition);
        bool IsCorrectGiftCardAmountSelected(int timeToWait);
    }
}
