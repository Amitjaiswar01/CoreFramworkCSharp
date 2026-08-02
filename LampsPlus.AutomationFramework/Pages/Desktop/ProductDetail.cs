using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.UI;
using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Exceptions;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Pages.Desktop
{
    /// <summary>
    /// Example page: https://www.lampsplus.com/products/possini-euro-design-vicina-chrome-led-torchiere-floor-lamp__4g433.html.
    /// </summary>
    public class ProductDetail : ProductDetailBase
    {
        public ProductDetail(IBrowser browser, IGlobalLocators globalLocators) : base(browser, globalLocators) { }

        #region Class Setup

        #region Element Text
        public override string SkuOnPdp => Browser.Locate.ElementByXpath(SkuOnPdpXpath).GetAttribute(ContentString);
        public override string MaxAvailableQuantity => QuantityField.FindElements(By.TagName(HtmlTextWriterTag.Option.ToString())).Last().Text;
        public override string ToOrderCalloutString => Browser.Locate.ElementByXpath("//div[@class='pdPleaseCall']").Text;
        #endregion

        #region CSS Selectors
        private string PdAskStoreAssociateId { get; } = "pdAskStoreAssociate";
        private string ProductSpecificationTablesClass{ get; } = "specificationSection";
        public override string SkuOnPdpXpath { get; } = "//*[@id='pdProdSku']";
        public override string BreadCrumbXpath { get; } = "//*[@id='divBreadCrumb']";
        public override string AppCheckStoreAvailabilityClass { get; } = "AppCheckStoreAvailability";
        public override string BuildFullSystemId { get; } = "build-full-system";
        public override string DivProductDetailTop { get; } = "divProductDetail-top";
        public override string TurnToQuestionAndAnswerSection { get; } = "turntoQuestionsAndAnswersSection";
        public override string FsImageContainerId { get; } = "fsImageContainer";
        public override string LblStickyPriceId { get; } = "lblStickyPrice";
        public override string LpContainerId { get; } = "lpContainer";
        public override string ProductReviewsSectionId { get; } = "productReviewsContainer";
        public override string LpModalContentId { get; } = "lpModalContent";
        public override string ModalProductImageThumbnailXpath { get; } = "//*[@id='fsNav']/div/div/div[1]/div[1]";
        public override string PdViewFullTrackSystemId { get; } = "pdViewFullTrackSystem";
        public override string SlickActiveClass { get; } = "slick-active";
        public override string StickyWrapperId { get; } = "stickyWrapper";
        public override string ShipsInMessageClass { get; } = "stockCheck";
        public override string SlickListClass { get; } = "slick-list";
        public override string StockCheckXpath { get; } = "//div[@class='stockCheck']";
        public override string SystemOptionsQtyClass { get; } = "systemOptionsQty";
        public override string PdReviewsId { get; } = "read-reviews";
        public override string PdAddToCartStickyId { get; } = "pdAddToCartSticky";
        public override string PdAddToPortfolioSystemOptionsId { get; } = "pdAddToPortfolioSystemOptions";
        public override string PdFanFeatures { get; } = "pdFanFeatures";
        public override string PdHeroImageId { get; } = "pdImgCol";
        public override string pdImgContainerId { get; } = "pdImgContainer";
        public override string PdProdImgStickyId { get; } = "pdProdImgSticky";
        public override string PdProdTitleStickyClass { get; } = "pdProdTitleSticky";
        public override string PdProdImgClass { get; } = "pdProdImg";
        public override string ProductDetailsSectionId { get; } = "productDetailsSection";
        public override string ProductAttributesClass { get; } = "productAttrs";
        public override string ProductTechnicalSpecificationsClass { get; } = "technicalSpecs";
        public override string QandAId { get; } = "qAndA";
        public override string QtyNormalId { get; } = "QtyNormal";
        public override string WishListIndicatorString { get; } = "savedPortfolio-totalSaved--totalSavedCount";
        public override string SelectStoreClass { get; } = "jsSelectStoreBtn";
        public override string StoreAssociateId { get; } = "pdAskStoreAssociate";
        public override string StoreAssociateModalClass { get; } = "AppCheckStoreAvailability";
        public override string MediaModalContentModalClass { get; } = "OverlayContent";
        public override string ReplacementPartLinkId { get; } = "pdReplacementPartModal";
        public override string ReplacementPartSkuXpath { get; } = "//td[normalize-space()][2] ";
        public override string StickySaveXpath { get; } = "//div[@id='stickyWrapper-sticky-wrapper']//li[2]"; 
        public override string StickyCallOutClass { get; } = "stickyPriceContainer__priceTop";
        public override string StickySaleClass { get; } = "stickyPriceContainer__priceType";
        public override string EndVerbiageOnSfpStickyXpath { get; } = "//li[@class='endsDate']";
        public override string StickyContainerSfpClass { get; } = "stickyPriceContainer";
        public override string ViewInYourRoomSampleImageXpath { get; } = "(//span[@class='image '])[1]";
        public override string ViewInYourRoomSelectPhotoXpath { get; } = "//*[@id='browse']";
        public override string ViewInYourRoomXpath { get; } = "//*[@id='showInRoomBtn']";
        public override string WriteReviewBtnSelector { get; } = ".productReviews__writeReviewBtn";
        public override string WriteReviewModalXpath { get; } = "//*[@id='tt-review-form-title']";
        public override string WriteReviewModalSelector { get; } = ".tt-o-modal__body";
        public override string LoadMoreReviewsBtnClass { get; } = "//*[@id='productReviewsContainer']//*[text()='Load More Reviews']";
        public override string ReplacementPartModalClass { get; } = "OverlayContent";
        public override string GoodtoKnowIconClass { get; } = "nameDiv";
        public override string ShopAllColorText { get; } = "Shop all Color Plus";
        public override string EmailRecipientListClass { get; } = "recipientEmailList";
        public override string DesignChatClass { get; } = "designChatContainer";
        public override string ProductHelpId { get; } = "pdNeedHelpLink";
        public override string NeedHelpChatClass { get; } = "needHelpSupportInfoChat";
        public override string CloseNeedHelpClass { get; } = "Overlay__contentWrapper__closeButton";
        #endregion

        public override string MoreYouLikeBorderClass => throw new NotImplementedException();
        public override string AssetActionsClass => throw new NotImplementedException();
        public override string MobileStruckPriceSfpXpath => throw new NotImplementedException();
        public override string AdjacentButtonClass => throw new NotImplementedException();
        public override string AvailabilityPhoneNumberString => throw new NotImplementedException();
        public override string AvailabilityString => throw new NotImplementedException();
        public override string AvailabilityTextString => throw new NotImplementedException();
        public override string BtnPdpZoomClass => throw new NotImplementedException();
        public override string BuildFullSystemContainerId => throw new NotImplementedException();
        public override string CallStoreButtonClass => throw new NotImplementedException();
        public override string GiftCardDenominationXpath => throw new NotImplementedException();
        public override string JsCertonaTitleClass => throw new NotImplementedException();
        public override string JsOtherOptionLinkClass => throw new NotImplementedException();
        public override string LimitedQtyFieldClass => throw new NotImplementedException();
        public override string LimitedQuantitySelectionId => throw new NotImplementedException();
        public override string LpCollapsibleCollapsedHidden => throw new NotImplementedException();
        public override string LpMobileAccordionClass => throw new NotImplementedException();
        public override string MainImagePathXpath => throw new NotImplementedException();
        public override string PdAddToCartBuildFullId => throw new NotImplementedException();
        public override string PdRelatedItmsId => throw new NotImplementedException();
        public override string PdRelatedItmsXpath => throw new NotImplementedException();
        public override string PdFirstRelatedItmXpath => throw new NotImplementedException();
        public override string PdpStickyHeaderId => throw new NotImplementedException();
        public override string PdpStickyHeaderImageWrapperClass => throw new NotImplementedException();
        public override string PopularColorsId => throw new NotImplementedException();
        public override string ProductDescId => throw new NotImplementedException();
        public override string ProductDescSelector => throw new NotImplementedException();
        public override string ProductReviewsCollapsibleSelector => throw new NotImplementedException();
        public override string ProductReviewRatingStarCountClass => throw new NotImplementedException();
        public override string QtyLimitedName => throw new NotImplementedException();
        public override string QuantityDrawerXpath => throw new NotImplementedException();
        public override string StoreAvailabilityClass => throw new NotImplementedException();
        public override string StoreAvailabilityQuestionsClass => throw new NotImplementedException();
        public override string ThumbnailImageCarouselClass => throw new NotImplementedException();
        public override string ThumbnailImageCarouselId => throw new NotImplementedException();
        public override string TextAttributeValue => throw new NotImplementedException();
        public override string TitleAttributeValue => throw new NotImplementedException();
        public override string Tt4QProductImgClass => throw new NotImplementedException();
        public override string TtLeftHeaderClass => throw new NotImplementedException();
        public override string TtrespMobileDispInlineClass => throw new NotImplementedException();
        public override string TtOverallRatingStarsId => throw new NotImplementedException();
        public override string TtWriteReviewBtnPortraitId => throw new NotImplementedException();
        public override string PdHeroSpotId => throw new NotImplementedException();
        public override string PnlProductDescriptionId => throw new NotImplementedException();
        public override string ProductNameAndNumber => throw new NotImplementedException();
        public override string PdpAddToWishlistClass => throw new NotImplementedException();
        public override string ToOrderCalloutClass => throw new NotImplementedException();
        public override string ToOrderCallCalloutOnOrdersOver49Class => throw new NotImplementedException();
        public override string ProsSpecialPriceCallOutClass => throw new NotImplementedException();
        public override string RelatedItemsContainerXpath => throw new NotImplementedException();
        public override string RelatedItemSectionXpath => throw new NotImplementedException();
        public override string ReviewsSectionXpath => throw new NotImplementedException();
        public override string SearchResultsListClass => throw new NotImplementedException();
        public override string StoreAvailabilityLocatorContentClass => throw new NotImplementedException();
        public override string AddToWishListButtonXpath => throw new NotImplementedException();
        public override string ReviewsElementSelector => throw new NotImplementedException();
        public override string StickyPriceClass => throw new NotImplementedException();
        public override string LblStickyPriceClass => throw new NotImplementedException();
        public override string WriteReviewBtnXpath => throw new NotImplementedException();
        public override string ProductReviewSweepstakeClass => throw new NotImplementedException();
        public override string ShowInRoomBtnId => throw new NotImplementedException();
        public override string PdpArIframeXpath => throw new NotImplementedException();
        public override string ArViewerBtnClass => throw new NotImplementedException();
        public override string GetStartedBtnClass => throw new NotImplementedException();
        public override string ProductHeroThumbnailClass => throw new NotImplementedException();
        public override string CustomerPhotosThumbnailClass => throw new NotImplementedException();
        public override string ProductHeroImageClass => throw new NotImplementedException();
        public override string ProductCustomerPhotosClass => throw new NotImplementedException();
        #endregion

        #region Page Elements
        public override IElement GetAllColorPlusElement => Browser.Locate.ElementByLinkText(ShopAllColorText);
        public override IElement StickySaleCallout => Browser.Locate.ElementByClassName(StickySaleClass);
        public override IElement StickySaveCallout => Browser.Locate.ElementByXpath(StickySaveXpath);
        public override IElement StickyContainerSfp => Browser.Locate.ElementByClassName(StickyContainerSfpClass);
        public override IElement ActiveMainProductImage => Browser.Locate.ElementByClassName(SlickActiveClass);
        public override IElement AskStoreAssociate => Browser.Locate.ElementById(PdAskStoreAssociateId);
        public override IElement BoldChatButtonContainer => Browser.Locate.ElementByClassName(BoldChatButtonContainerClass);
        public override IElement BottomPortionOfPdp => Browser.Locate.ElementById(PdPanesId);
        public override IElement BuildFullSystemAddToCartButton => Browser.Locate.ElementById(PdAddToCartSystemOptionsId);
        public override IElement BuildFullSystemAddToWishListButton => Browser.Locate.ElementById(PdAddToPortfolioSystemOptionsId);
        public override IElement BuildFullSystemButton => Browser.Locate.ElementById(PdViewFullTrackSystemId);
        public override IElement BuildFullSystemContainer => Browser.Locate.ElementById(BuildFullSystemId);
        public override IElement BuildFullSystemOptions => Browser.Locate.ElementById(BuildFullSystemOptionsId);
        public override IElement ChatButtonLink => Browser.Locate.ElementByTagName(HtmlTextWriterTag.A, PdChat);
        public override IElement DesignChatLink => Browser.Locate.ElementByClassName(DesignChatClass);
        public override IElement ProductHelp => Browser.Locate.ElementById(ProductHelpId);
        public override IElement NeedHelpChat => Browser.Locate.ElementByClassName(NeedHelpChatClass);
        public override IElement CloseNeedHelp => Browser.Locate.ElementByClassName(CloseNeedHelpClass);
        public override IElement CheckAvailabilityModal => Browser.Locate.ElementByClassName(AppCheckStoreAvailabilityClass);
        public override IElement CheckAvailabilityStoreList => Browser.Locate.ElementByClassName(CheckAvailabilityStoreListClass);
        public override IElement CheckStoreFirstName => Browser.Locate.ElementById(CheckStoreModalFirstNameId);
        public override IElement CheckStorePhone => Browser.Locate.ElementById(CheckStoreModalPhoneId);
        public override IElement CheckStoreQuestion => Browser.Locate.ElementById(CheckStoreModalQuestionId);
        public override IElement CheckStoreReserveItemButton => Browser.Locate.ElementByClassName(CheckStoreModalReserveItemButtonClass);
        public override IElement CheckStoreReserveItemButtonBottom => Browser.Locate.ElementByClassName(ButtonClass);
        public override IElement CheckStoreSearchButton => Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.Button, ButtonSecondaryClass);
        public override IElement CsInfo => Browser.Locate.ElementByClassName(CsInfoClass);
        public override IElement CustomerPhotos => Browser.Locate.ElementByClassName(CustomerPhotosClass);
        public override IElement EmailLink => Browser.Locate.ElementByXpath(PdEmailItemXpath);
        public override IElement EmailModalContent => Browser.Locate.ElementById(LpModalContentId);
        public override IElement EmailRecipientTextbox => Browser.Locate.ElementByClassName(InlineEmailsClass);
        public override IElement EmailRecipientList => Browser.Locate.ElementByClassName(EmailRecipientListClass);
        public override IElement FanFeatures => Browser.Locate.ElementById(PdFanFeatures);
        public override IElement FirstNameTextbox => Browser.Locate.ElementById(PdFirstNameId);
        public override IElement FooterChatLink => Browser.Locate.ElementByClassName(BoldChatWrapperClass).FindElement(By.TagName(HtmlTextWriterTag.A.ToString()));
        public override IElement FreeShippingToStatesWithStoresLabel => Browser.Locate.ElementById(LblFreeShippingToStatesWithStoresId);
        public override IElement FromEmailTextbox => Browser.Locate.ElementById(PdFromEmailId);
        public override IElement HeaderChatLink => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Span, "data-ga-label", "Global-Header");
        public override IElement HousingOptions => Browser.Locate.ElementById(HousingOptionsId);
        public override IElement HousingOptionsSectionHeader => Browser.Locate.ElementByClassName(PdSectionTitleClass, HousingOptions);
        public override IElement ImageContainer => Browser.Locate.ElementByXpath("//*[@id='fsImageContainer']/img");
        public override IElement LastNameTextbox => Browser.Locate.ElementById(PdLastNameId);
        public override IElement LblStockInventory => Browser.Locate.ElementBySelector(LblStockInventoryId.ToCssIdSelector());
        public override IElement LimitedQtyField => Browser.Locate.ElementById(QtyNormalId);
        public override IElement LongSkuElement => Browser.Locate.ElementBySelector(LongSkuClass.ToCssClassSelector());
        public override IElement MainProductImage => Browser.Locate.ElementByClassName(PdProdImgClass, ActiveMainProductImage, true);
        public override IElement MarginModalLink => Browser.Locate.ElementById(MarginModalLinkId);
        public override IElement ModalProductImageThumbnail => Browser.Locate.ElementByXpath(ModalProductImageThumbnailXpath);
        public override IElement MoreImages(int index) => Browser.Locate.ElementBySelector($"{ProductImageThumbnailId.ToCssIdSelector()} > {HtmlTextWriterTag.Div.ToNthChildSelector(index)}");
        public override IElement PdChat => Browser.Locate.ElementById(PdChatId);
        public override IElement PdImageColumn => Browser.Locate.ElementById(PdHeroImageId);
        public override IElement PdProdInfoColElement => Browser.Locate.ElementById(PdProdInfoColId);
        public override IElement PdProdSpecificationsTables => Browser.Locate.ElementByClassName(ProductSpecificationTablesClass);
        public override IElement PdReviewsElement => Browser.Locate.ElementById(PdReviewsId);
        public override IElement PdSocialIconElement => Browser.Locate.ElementByClassName(PdSocialIconClass);
        public override IElement PdSocialPrintIconElement => Browser.Locate.ElementByXpath(PdSocialPrintIconXpath);
        public override IElement PriceType => Browser.Locate.ElementByClassName(PriceTypeClass);
        public override IElement PrintKioskStyleButtonElement => Browser.Locate.ElementByXpath(PrintKioskStyleButtonXpath);
        public override IElement PrintKioskStyleProductBtnElement => Browser.Locate.ElementByXpath(PrintKioskStyleProductBtnXpath);
        public override IElement ProductAttributes => Browser.Locate.ElementByClassName(ProductAttributesClass);
        public override IElement ProductCallOut => Browser.Locate.ElementByClassName(PriceTypeClass);
        public override IElement ProductDetailPageContainer => Browser.Locate.ElementBySelector(LpContainerId.ToCssIdSelector());
        public override IElement ProductReviewsSection => Browser.Locate.ElementById(ProductReviewsSectionId);
        public override IElement ProductDetailSection => Browser.Locate.ElementById(ProductDetailsSectionId);
        public override IElement ProductSpecificationsTables => Browser.Locate.ElementByClassName(ProductTechnicalSpecificationsClass);
        public override IElement ProductImage => Browser.Locate.ElementByClassName(PdProdImgClass, Browser.Locate.ElementByClassName(ActiveClass));
        public override IElement pdImgContainer => Browser.Locate.ElementById(pdImgContainerId);
        public override IElement ProductImageThumbnail => Browser.Locate.ElementById(ProductImageThumbnailId);
        public override IElement ThumbnailWrapper => Browser.Locate.ElementByTagName(HtmlTextWriterTag.Img, Browser.Locate.ElementByClassNames(ThumbnailWrapperClass, ActiveClass), true);
        public override IElement ProductInStockTextLink => Browser.Locate.ElementById(PdInStockId);
        public override IElement ProductSlider => Browser.Locate.ElementById(PdScrollableOtherOptionsId);
        public override IElement ProsSpecialPriceCallout => Browser.Locate.ElementByXpath("//*[@id='pnlProductPrice']/div/strong");
        public override IElement QuestionsAndAnswersChatContainer => Browser.Locate.ElementByClassName(BoldChatWrapperWithIconClass);
        public override IElement QuestionsAndAnswersChatLink => Browser.Locate.ElementByLinkText("Chat", QuestionsAndAnswersChatContainer);
        public override IElement QuickPrintInput => Browser.Locate.ElementByTagName(HtmlTextWriterTag.Input, Browser.Locate.ElementByClassName(PdQuickPrintClass));
        public override IElement QuickPrintLink => Browser.Locate.ElementById(PdQuickPrintId);
        public override IElement ReviewsSection => Browser.Locate.ElementById(ReviewsSectionId);
        public override IElement RelatedItemsSection => Browser.Locate.ElementById(RelatedItemsId);
        public override IElement SamplePhotosTab => Browser.Locate.ElementByClassName(SamplePhotosTabClass);
        public override IElement SampleRoomBtn => Browser.Locate.ElementByClassName(SampleRoomBtnClass);
        public override IElement SendEmailButton => Browser.Locate.ElementByClassName(SendEmailButtonClass);
        public override IElement SelectedThumbnailWrapper => Browser.Locate.ElementByXpath("//div[contains(@class,'selectedThumbnail')]/img");
        public override IElement SelectedDifferentThumbnailWrapper => Browser.Locate.ElementByTagName(HtmlTextWriterTag.Img, Browser.Locate.ElementByClassNames(ThumbnailWrapperClass, SelectedThumbnailWrapperClass), true);
        public override IElement StickyAddToCart => Browser.Locate.ElementById(PdAddToCartStickyId);
        public override IElement StickyImage => Browser.Locate.ElementById(PdProdImgStickyId);
        public override IElement StickyPrice => Browser.Locate.ElementById(LblStickyPriceId);
        public override IElement StickyTitle => Browser.Locate.ElementByClassName(PdProdTitleStickyClass);
        public override IElement StickyWrapper => Browser.Locate.ElementById(StickyWrapperId);
        public override IElement StockCheckElement => Browser.Locate.ElementByClassName(ShipsInMessageClass);
        public override IElement InStockElement => Browser.Locate.ElementByXpath("//*[@class='stockCheck']/span[1]/span");
        public override IElement StockCheckTitleElement => Browser.Locate.ElementByClassName(StockCheckTitleClass);
        public override IElement StockCheckWrapper => Browser.Locate.ElementByXpath(StockCheckXpath);
        public override IElement StoreAvailability => Browser.Locate.ElementById(PdCheckStoreAvailabilityId);
        public override IElement StoreInventoryElement => Browser.Locate.ElementByClassName(StoreInventoryClass);
        public override IElement TopContentProductDetail => Browser.Locate.ElementById(DivProductDetailTop);
        public override IElement TurnToDynamicAddAnswerButton => Browser.Locate.ElementByClassName(Tt4AddAnswerClass, Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Div, TtQuestionIdAttributeName, TurnTwoQuestionId));
        public override IElement TurnToDynamicAddAnswerTextArea => Browser.Locate.ElementById($"{Tt2InlineAnswerId}-{TurnTwoQuestionId}");
        public override IElement TurnToDynamicAddQuestionsCancelButton => Browser.Locate.ElementById($"{Tt2CancelBtnId}-{TurnTwoQuestionId}");
        public override IElement TurnToQuestionAndAnswerContainer => Browser.Locate.ElementBySelector(QandAId.ToCssIdSelector());
        public override IElement TurnToQuestionsAndAnswersSection => Browser.Locate.ElementById(TurnToQuestionAndAnswerSection);
        public override IElement TurnToReviewAddNewPhotoButton => Browser.Locate.ElementByClassName(TtVcAddNewPhotoClass);
        public override IElement TurnToReviewAttachPhoto => Browser.Locate.ElementByClassName(TtVcBarMediaLgPhotoClass);
        public override IElement TurnToReviewFileInput => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Name, TtImgFileAttributeValue);
        public override IElement TurnToReviewFileMediaListSelected => Browser.Locate.ElementBySelector($"{TtMediaListContPhotoId.ToCssIdSelector()} {TtSelectedClass.ToCssClassSelector()}");
        public override IElement TurnToReviewMediaSubmitButton => Browser.Locate.ElementById(TtMediaSubmitBtnId);
        public override IElement TurnToReviewProductImage => Browser.Locate.ElementById(TtRevCatItemImgId);
        public override IElement TurnToReviewProductName => Browser.Locate.ElementBySelector($"{TtWriteRevGreetId.ToCssIdSelector()} {TtmediaSmallDescClass.ToCssClassSelector()}");
        public override IElement TurnToReviewRating => Browser.Locate.ElementById(TtRevRatingId);
        public override IElement TurnToReviewScreen => Browser.Locate.ElementById(TtWriteReviewScreenId);
        public override IElement TurnToReviewModal => Browser.Locate.ElementBySelector(WriteReviewModalSelector);
        public override IElement TurnToReviewShareMediaScreen => Browser.Locate.ElementById(TtShareMediaScreenId);
        public override IElement TurnToReviewText => Browser.Locate.ElementById(TtReviewTextId);
        public override IElement TurnToReviewTitle => Browser.Locate.ElementById(TtReviewTitleId);
        public override IElement TurnToReviewWindow => Browser.Locate.ElementById(TtTraWindowCloseId);
        public override IElement TurnToWriteReviewButton => Browser.Locate.ElementById(TtWriteReviewBtnId);
        public override IElement TurnTwoBrowseQaWrapper => Browser.Locate.ElementById(TtBrowseQaWrapperId);
        public override IElement WishListIndicatorIcon => Browser.Locate.ElementById(WishListIndicatorString);
        public override IElement ZipcodeTextbox => Browser.Locate.ElementById(PdPostalCodeId);
        public override IElement ProductReviewModal => Browser.Locate.ElementById(PdReviewModalId);
        public override IElement QuestionsAndAnswersCommentsSection => Browser.Locate.ElementByClassName(QuestionAndAnswerCommentColumns);
        public override IElement EndsDate(int index) => Browser.Locate.ElementsByClassName(EndsDateClass)[index];
        public override IElement ProductReviewCard(int index) => Browser.Locate.ElementsByClassName(ProductReviewCardClass)[index];
        public override IElement LoadMoreReviews => Browser.Locate.ElementByXpath(LoadMoreReviewsBtnClass);
        public override IElement SoldOutLabel => Browser.Locate.ElementByTagName(HtmlTextWriterTag.Strong, PriceType);
        public override IElement SearchZipTextBox => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Input, HtmlTextWriterAttribute.Id, SearchZipInputId);
        public override IElement MobileStruckPriceSfp => throw new NotImplementedException();
        public override IElement BuildFullSystemProductContainer => throw new NotImplementedException();
        public override IElement CallStoreButton => throw new NotImplementedException();
        public override IElement CertonaDrawerName => throw new NotImplementedException();
        public override IElement CheckStoreSearchArrow => throw new NotImplementedException();
        public override IElement CheckStoreCallButton => throw new NotImplementedException();
        public override IElement CheckStoreChooseAnotherStateOrZip => throw new NotImplementedException();
        public override IElement CityTitle => throw new NotImplementedException();
        public override IElement GetYourPhotoFrame => throw new NotImplementedException();
        public override IElement LimitedQuantitySelection => throw new NotImplementedException();
        public override IElement MainImagePath(int index) => throw new NotImplementedException();
        public override IElement MobileAddToCartButtonContainer => throw new NotImplementedException();
        public override IElement MobileAccordionContainer => throw new NotImplementedException();
        public override IElement MobileGiftCardDenomination => throw new NotImplementedException();
        public override IElement MobileMaxQuantity => throw new NotImplementedException();
        public override IElement PdpAddToWishlist => throw new NotImplementedException();
        public override IElement PdHeroSpot => throw new NotImplementedException();
        public override IElement PopularColorsDropdown => throw new NotImplementedException();
        public override IElement ProductDescDropDown => throw new NotImplementedException();
        public override IElement ProductDescriptionAccordion => throw new NotImplementedException();
        public override IElement ProductDetailSpecificationSection => throw new NotImplementedException();
        public override IElement ProductGoodToKnowSection => throw new NotImplementedException();
        public override IElement ProductHeroThumbnail => throw new NotImplementedException();
        public override IElement CustomerPhotosThumbnail => throw new NotImplementedException();
        public override IElement ProductHeroImage => throw new NotImplementedException();
        public override IElement ProductCustomerPhotos => throw new NotImplementedException();
        public override IElement RelatedItemDropdown => throw new NotImplementedException();
        public override IElement SearchResultsList => throw new NotImplementedException();
        public override IElement SocialLinksContainer => throw new NotImplementedException();
        public override IElement StickyImageWrapper => throw new NotImplementedException();
        public override IElement StoreAvailabilityQuestions => throw new NotImplementedException();
        public override IElement StoreAvailabilityLocatorContent => throw new NotImplementedException(); 
        public override IElement ProductQuestionAnswerArrow => throw new NotImplementedException();
        public override IElement TextStoreButton => throw new NotImplementedException();
        public override IElement ThumbnailCarouselImage => throw new NotImplementedException();
        public override IElement ToOrderCalloutOnPdp => throw new NotImplementedException();
        public override IElement ZoomIcon => throw new NotImplementedException();
        public override IElement CarouselImage(int index) => throw new NotImplementedException();
        public override IElement ZipInputFieldCheckStore => throw new NotImplementedException();
        public override IElement StickyPriceCallout => throw new NotImplementedException();
        public override IElement ProductReviewSweepstake => throw new NotImplementedException();
        public override IElement WriteReviewBtn => throw new NotImplementedException();
        public override IElement ShowInRoomBtn => throw new NotImplementedException();
        public override IElement PdpArIframe => throw new NotImplementedException();
        public override IElement ArViewerBtn(int index) => throw new NotImplementedException();
        public override IElement GetStartedBtn => throw new NotImplementedException();
        public override ReadOnlyCollection<IElement> BuildFullSystemQtyElements => Browser.Locate.ElementsByClassName(SystemOptionsQtyClass, BuildFullSystemContainer);
        public override ReadOnlyCollection<IElement> BuildFullSystemShortSkuLinks => Browser.Locate.ElementsBySelector($"{HtmlTextWriterTag.Td.ToNthChildSelector(3)} {HtmlTextWriterTag.A}", BuildFullSystemContainer);
        public override ReadOnlyCollection<IElement> HousingOptionsSectionDivContainers => Browser.Locate.ElementsByClassName(PdHousingOptionsProdContainerClass, HousingOptions);
        public override ReadOnlyCollection<IElement> ListOfFullSystemProductNames => ListOfFullSystemData(2);
        public override ReadOnlyCollection<IElement> ListOfFullSystemSkus => ListOfFullSystemData(3);
        public override ReadOnlyCollection<IElement> MoreThumbnailImage => Browser.Locate.ElementsByClassName(ThumbnailWrapperClass, Browser.Locate.ElementById(ProductImageThumbnailId));
        public override ReadOnlyCollection<IElement> ProductSliders => Browser.Locate.ElementsByClassName(PdScrollableOtherOptionsId);
        public override ReadOnlyCollection<IElement> SamplePhotos => Browser.Locate.ElementsByClassName(ImageClass, Browser.Locate.ElementByClassName(SamplePhotosClass));
        public override ReadOnlyCollection<IElement> ThumbnailImageCarousel => throw new NotImplementedException();
        public override ReadOnlyCollection<IElement> GoodToKnowIcon => Browser.Locate.ElementsByClassName(GoodtoKnowIconClass);

        public override (string textMessage, string phoneNumber) GetTxtMessageAndPhoneNumber() => throw new NotImplementedException();
        
        public override bool IsQuantityLeftShows => PdProdInfoColElement.FindElements(By.ClassName(QtyLeftClass)).Any();
        public override bool IsReplacementPartLinkVisible => Browser.Locate.ElementById(ReplacementPartLinkId).IsInitialized;
        #endregion

        private bool IsCheckStoreAvailabilityModalVisible(int timeToWait)
        {
            return Browser.Wait.IsVisibleElement(By.ClassName(AppCheckStoreAvailabilityClass), timeToWait);
        }

        public override void AddMaxQuantityToCart()
		{
			Browser.Wait.ForCondition(() => Browser.Locate.ElementsByTagName(HtmlTextWriterTag.Option, QuantityField).Count > 0);

			Browser.Locate.ElementsByTagName(HtmlTextWriterTag.Option, QuantityField).Last().Click();
			Browser.Wait.ForElement(GlobalLocators.AddToCartButton).Click();
		}

        public override Dictionary<string, int> AddAllBuildFullSystemSkusToCart()
        {
            var buildFullSystemQtyFields = BuildFullSystemQtyElements;
            var buildFullSystemShortSkuLinks = BuildFullSystemShortSkuLinks;
            var qtyCtr = 1;
            var addedProducts = new Dictionary<string, int>();

            for (var i = 0; i < buildFullSystemQtyFields.Count; i++)
            {
                var qtyField = buildFullSystemQtyFields[i];
                var shortSku = buildFullSystemShortSkuLinks[i].Text;

                qtyField.Clear();
                qtyField.SendKeys(qtyCtr.ToString());
                addedProducts.Add(shortSku, qtyCtr);

                // cycle through qty 1 - 5 for variation
                // ReSharper disable once RedundantAssignment
                qtyCtr = qtyCtr == 5 ? 1 : qtyCtr++;
            }

            Browser.ExecuteJs("arguments[0].style.display = 'none';", StickyWrapper.InternalElement);

            BuildFullSystemAddToCartButton.Click();

            return addedProducts;
        }

        public override void ClickTurnToWriteReview(bool isiPhoneTest = false)
        {
            ClickTurnToWriteReviewButtonJs();
            Browser.Wait.IsVisibleElement(By.XPath(TurnToModalWindowXpath));
        }

        public override void CompleteTurnToWriteReview()
        {
            // Upload Photo process
            TurnToReviewAttachPhoto.Click();
            Browser.Wait.ForElement(TurnToReviewShareMediaScreen);
            TurnToReviewFileInput.SendKeys(FileUpload.TurnToReviewPhotoUploadPath);
            Browser.Wait.ForElement(TurnToReviewFileMediaListSelected);

            // TurnTo adds opacity:0.2 while upload is in progress then changes it to 1 when upload is done
            Browser.MouseOverOnElement(TurnToReviewAddNewPhotoButton);

            // Click Submit button twice because clicking it for the first time changes the UI to allow user to review the upload
            // and then it keeps the same exact button which needs to be clicked again to finish the upload process
            TurnToReviewMediaSubmitButton.Click();
            TurnToReviewMediaSubmitButton.Click();

            // Close the review
            TurnToReviewWindow.Click();
        }

        public override void FocusOnTurnToQAndA()
        {
            Browser.Wait.ForElement(TurnTwoBrowseQaWrapper);
        }

        public override void ProductCheckStoreAvailabilityLink()
        {
            Browser.Wait.IsVisibleElement(By.Id(PdCheckStoreAvailabilityId));
            Browser.ClickOnButtonMultipleTimes(StoreAvailability, 5, IsCheckStoreAvailabilityModalVisible);
            Browser.Wait.ForDomReady();
        }
        
        public override void TypeIntoQAndATextarea(string sampleText)
        {
            ForceHideStickyHeader();
            Browser.MouseOverOnElement(TurnTwoAskAQuestionTextArea);
            TurnTwoAskAQuestionTextArea.Clear();
            TurnTwoAskAQuestionTextArea.Click();
            TurnTwoAskAQuestionTextArea.SendKeys(sampleText);
        }

        public override void AddAnswerToQuestion(string sampleText)
        {
            Browser.MouseOverOnElement(TurnToDynamicAddAnswerButton);
            TurnToDynamicAddAnswerButton.Click();
            Browser.Wait.ForElement(TurnToDynamicAddAnswerTextArea).SendKeys(sampleText);
        }

        public override bool TimeVerifyCheck(string availabilityCallout1, string availabilityCallout2)
        {
            var start = new TimeSpan(07, 00, 00);
            var end1 = new TimeSpan(16, 30, 00);
            var timeNow = DateTime.Now.TimeOfDay;
            var end2 = new TimeSpan(18, 00, 00);
            var thisDay = DateTime.Today.DayOfWeek.ToString();
            bool condition = false;

            if (timeNow.Seconds > 45)
            {
                timeNow = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute + 1, 00);
            }
            else
            {
                timeNow = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 00);
            }

            if ((thisDay == "Saturday") || (thisDay == "Sunday"))
            {
                if (DateTimeHelper.IsTimeInBetween(start, end1, timeNow))
                {
                    if (string.Equals(ToOrderCallout, availabilityCallout1, StringComparison.OrdinalIgnoreCase))
                    {
                        condition = true;
                    }
                }
                else
                {
                    if (string.Equals(ToOrderCallout, availabilityCallout2, StringComparison.OrdinalIgnoreCase))
                    {
                        condition = true;
                    }                  
                }
            }
            else
            {
                if (DateTimeHelper.IsTimeInBetween(start, end2, timeNow))
                {
                    if (string.Equals(ToOrderCallout, availabilityCallout1, StringComparison.OrdinalIgnoreCase))
                    {
                        condition = true;
                    }
                }
                else
                {
                    if (string.Equals(ToOrderCallout, availabilityCallout2, StringComparison.OrdinalIgnoreCase))
                    {
                        condition = true;
                    }
                }
            }
            return condition;
        }

        public override bool TimeVerifyCheckMobile(string textChatExpected, string textChatActual, string phoneExpected, string phoneActual, string availabilityTextExpected, string availabilityTextActual)
        {
            throw new NotImplementedException();
        }
        public override void ClickPhotoModal(int productReviewCounter, int pixelsScroll, int reviewClassNotFound, int endCondition)
        {
            do
            {
                Browser.ScrollToByPixelsVertical(pixelsScroll.ToString());
                if (Browser.Locate.ElementImmediately(WriteReviewBtnSelector).IsInitialized)
                {
                    Browser.Wait.ForDisplayedElement(ProductReviewCard(0));

                    ProductReviewCard(0).Click();
                    break;
                }

                Log.Message($"Scrolling to element, scroll# {productReviewCounter}");
                pixelsScroll += pixelsScroll;

                if (productReviewCounter == reviewClassNotFound) throw new FrameworkWaitException($"Element {ProductReviewCardClass} is not found");

            } while (productReviewCounter < endCondition);
        }
    }
}
