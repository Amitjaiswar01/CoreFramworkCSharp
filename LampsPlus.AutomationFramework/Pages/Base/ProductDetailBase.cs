using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Utilities;
using OpenQA.Selenium;
using Page = Automation.Framework.Core.Page;

namespace LampsPlus.AutomationFramework.Pages.Base
{
    /// <summary>
    /// Base class for common behavior between desktop and mobile views.
    /// </summary>
    public abstract class ProductDetailBase : Page, IProductDetail
    {
        protected ProductDetailBase(IBrowser browser, IGlobalLocators globalLocators) : base(browser) { GlobalLocators = globalLocators; }

        #region Class Setup
        private string _turnTwoQuestionId;

        internal IGlobalLocators GlobalLocators { get; }

        #region Element Text
        public string BuildFullSystemSectionTitle => Browser.Locate.ElementByClassName(BuildFullSystemSectionTitleClass, Browser.Locate.ElementById(BuildFullSystemSectionId)).Text;
        public string BuildFullSystemTableFirstSku => Browser.Locate.ElementBySelector($"{HtmlTextWriterTag.Tr.ToNthChildSelector(3)} {HtmlTextWriterTag.Td.ToNthChildSelector(3)}", Browser.Locate.ElementById(BuildFullSystemOptionsId)).Text;
        public string BuildFullSystemTableTitle => Browser.Locate.ElementById(BuildFullSystemTableTitleId).Text;
        public string CustomerThumbnailImagePath => SelectedThumbnailWrapper.GetAttribute("data-imgpath");
        public string CustomerMainImagePath => ImageContainer.GetAttribute("data-imgpath");
        public string GetTitleSku => ProductSkuLabel.GetAttribute(ContentString);
        public string SaleItemPriceText => Browser.Locate.ElementByClassName("specialOrSale").Text;
        public string ItemPriceText => Browser.Locate.ElementById("lblPrice").Text;
        public string ProductComparePrice => Browser.Locate.ElementBySelector(ComparePriceClass.ToCssClassSelector()).Text;
        public string ModalThumbnailImagePath => SelectedThumbnailWrapper.GetAttribute("data-imgpath");
        public string ModalThumbnailImageSrc => ProductHeroThumbnail.GetAttribute("src");
        public string ModalCustomerPhotosThumbnailSrc => CustomerPhotosThumbnail.GetAttribute("src");
        public string ModalMainImageSrc => ProductHeroImage.GetAttribute("src");
        public string ModalCustomerPhotosSrc => ProductCustomerPhotos.GetAttribute("src");
        public string ModalDiffrentThumbnailImagePath => SelectedDifferentThumbnailWrapper.GetAttribute("data-imgpath");
        public string ModalMainImagePath => ImageContainer.GetAttribute("data-imgpath");
        public string ProductImageUrl => ProductImage.GetAttribute("src");
        public string ProductImagePath => ProductImage.GetAttribute("data-imgpath");
        public string ProductThumbnailImagePath => ThumbnailWrapper.GetAttribute("data-imgpath");
        public string ProductName => Browser.Locate.ElementById(H1ProductNameId).Text;
        public string ProductNameWithSku => $"{ProductName} ({ProductSkuNumber.Replace("Style # ", "")})";
        public string ProductSaleEndDateText => Browser.Locate.ElementByClassName(EndsDateClass).Text;
        public string ProductSalePrice => Browser.Locate.ElementById(PriceAdditionalSaveId).Text;
        public string ProductSkuNumber => Browser.Locate.ElementById(PdProdSkuId).Text;
        public string ProductImageThumbnailId => "pdAddlImgs";
        public string QuantityLeft => Browser.Locate.ElementById(AvailInventoryId).Text;
        public string QuickPrintLpModalPrice => Browser.Locate.ElementById(PdQuickPrintLpModalPriceId).Text;
        public string QuickPrintLpModalProductName => Browser.Locate.ElementBySelector($"{DivPopupContentId.ToCssIdSelector()} {HtmlTextWriterTag.Table.ToDirectChildSelector()} {HtmlTextWriterTag.Tbody.ToDirectChildSelector()} {HtmlTextWriterTag.Tr.ToDirectChildSelector().ToNthChildSelector(2)} {HtmlTextWriterTag.Td.ToDirectChildSelector()} {HtmlTextWriterTag.P.ToDirectChildSelector().ToNthChildSelector(1)} {HtmlTextWriterTag.Strong.ToDirectChildSelector()}").Text;
        public string RelatedItemSku => RelatedItemsContainer.GetAttribute("data-certonasku");
        public string RelatedItemUrl => RelatedItemsContainer.GetAttribute(HtmlTextWriterAttribute.Href.ToString());
        public string StruckThroughPrice => Browser.Locate.ElementBySelector("#pnlProductPrice > ul > li.regPrice").Text;
        public string ToOrderString => Browser.Locate.ElementByXpath("//*[@id=\"pnlPleaseCallCustomerService\"]/p/strong").Text;
        public string ForAvailabilityText => ForAvailabilityCallout.FindElement(By.TagName("div")).Text;
        public string ForAvailabilityPhone => Browser.Locate.ElementByTagName(HtmlTextWriterTag.A, ForAvailabilityCallout).Text;
        public string ForAvailability => ForAvailabilityCallout.Text;
        public string ForAvailabilityCallText => Browser.Locate.ElementByClassName(PnlPleaseCallCustomerServiceClass).Text;
        public IElement ToOrderCallCalloutDesktop => Browser.Locate.ElementById(PnlPleaseCallCustomerServiceId);
        public IElement ForAvailabilityCallout => Browser.Locate.ElementByClassName(PdPleaseCallClass);
        public IElement BuildFullSystemTitle => Browser.Locate.ElementByClassName(BuildFullSystemSectionTitleClass);

        public decimal ProductPrice => decimal.Parse(Price.Text.Replace("$", string.Empty).Replace("Price:", string.Empty).Replace("\r\n", "")); // Split price as some prices have callout next to it (e.g. "$99.99 Daily Sale")

        #endregion

        public string Chandeliers { get; } = "chandeliers";
        public string HousingOptionsString { get; } = "Housing Options";
        public string InStockCaps { get; } = "IN STOCK";
        public string InStockNonCaps { get; } = "In Stock";
        public string MoreOptionsString { get; } = "MORE OPTIONS";
        public string OverlayContentWrapperCloseButtonClass { get; } = "Overlay__contentWrapper__closeButton";
        public string PdMymlSectionId { get; } = "pdMoreYouMayLike";
        public string PdMymlSectionItemId { get; } = "moreYouMayLikeContainer";
        public string PdRecentlyViewedSectionItemId { get; } = "recentlyViewedContainer";
        public string OtherOptionsString { get; } = "Other Options";
        public string ProsSpecialPriceLabel { get; } = "PROS SPECIAL PRICE";
        public string ShipsIn { get; } = "Ships in";
        public string SkuStatusLabel { get; } = "SKU Status:";
        public string ToOrderCallout { get; } = "TO ORDER, CALL 800-782-1967";
        public string TurnTwoSampleQuestionText { get; } = "LPQA lpqa";
        public string EndVerbiageSfpAndPlaClass { get; } = "saleEnd";
        public string ProductPriceId { get; } = "lblPrice";
        public string ProductAttributeString { get; } = "PRODUCT ATTRIBUTES";
        public string ProductSpecificationString { get; } = "TECHNICAL SPECIFICATIONS";
        public string StoreRadioButtonSelector { get; } = "input#RbAllEmployees[checked='checked']";
        public virtual string ProductReviewUrlFragment { get; } = "#readreviews";
        #endregion

        #region CSS Selector Strings
        private string WidgetFloatingButtonCloseClass { get; } = "widget-floating__button--close";
        private string BuildFullSystemSectionId { get; } = "build-full-system";
        public string BuildFullSystemSectionTitleClass { get; } = "pdSectionTitle";
        private string BuildFullSystemTableTitleId { get; } = "pdFullSystemOptionsTitle";
        private string OverlayContentWrapperClass { get; } = "Overlay__contentWrapper";
        private string CompareCalloutClass { get; } = "comparePrice";
        private string ComparePriceClass { get; } = "comparePrice";
        private string CourseTitleClass { get; } = "productCarouselTitle";
        private string DivBreadCrumbId { get; } = "divBreadCrumb";
        private string DivPopupContentId { get; } = "divPopupContent";
        private string H1ProductNameId { get; } = "h1ProductName";
        private string LblFreeShippingId { get; } = "lblFreeShipping";
        private string MarginLinkId { get; } = "marginModalLink";
        private string ModalShortSkuClass { get; } = "shortSku";
        private string PdpStickyHeaderPriceClass { get; } = "pdpStickyHeader__price";
        private string PdManufacturerLinkClass { get; } = "pdManufacturerLink";
        private string PdRelItmsProdClass { get; } = "pdRelItmsProd";
        private string PdQuickPrintLpModalPriceId { get; } = "lblPrice";
        private string PlayerId { get; } = "player";
        private string PnlBrandId { get; } = "pnlBrand";
        private string PnlPleaseCallCustomerServiceClass { get; } = "pnlPleaseCallCustomerService";
        private string PnlPleaseCallCustomerServiceId { get; } = "pnlPleaseCallCustomerService";
        private string QtyNormalInputId { get; } = "QtyNormal";
        private string QtyMultiProdId { get; } = "QtyMultiProd";
        private string QuestionAskStoreAssociateLinkId { get; } = "pdAskStoreAssociate";
        private string ReadReviewsId { get; } = "readReviews";
        private string ScrollableHeaderClass { get; } = "pdScrollableHeader";
        private string ShipsFreeWithOrdersOverFortyNineCallOutId { get; } = "lblFreeShippingOver50";
        private string StoreTitleClass { get; } = "storeTitle";
        private string StoreNameClass { get; } = "storeName";
        private string StruckThroughClass { get; } = "regPrice";
        private string TradePriceLabelClass { get; } = "tradePriceLabel";
        private string TradeSavingsId { get; } = "lblTradeSavings";
        private string Tt2AnswerBtnId { get; } = "TT2answerBtn";
        private string Tt3QuestWrpClass { get; } = "TT3questWrp";
        private string TurnTwoAskQuestionCloseButtonId { get; } = "TT4closeQuestionBox";
        private string ViewAllRecentlyViewedButtonClass { get; } = "viewAllRecentlyViewedBtn";
        public string AvailInventoryId { get; } = "availInventory";
        private string BcChatContainerFrame { get; } = "#bc-frame > iframe";
        private string ConfirmationDialogClass { get; } = "confirmation-dialog";
        public string BcChatContainerId { get; } = "bc-chat-container";
        public string ConfirmationDialogButtonClass { get; } = "confirmation-dialog__button--yes";
        public string BoldChatButtonContainerClass { get; } = "boldChatButtonContainer";
        public string BoldChatWrapperClass { get; } = "boldChatWrapper";
        public string BoldChatWrapperWithIconClass { get; } = "boldChatWrapperWithIcon";
        public string BuildFullSystemOptionsId { get; } = "pdFullSystemOptions";
        public string BuyItNewLinkClass { get; } = "openBoxBuyItNewContainer";
        public string ButtonClass { get; } = "Button";
        public string GoodToKnowClass { get; } = "goodToKnowSection";
        public string ButtonSecondaryClass { get; } = "Button--secondary";
        public string CheckStoreChooseAnotherStateOrZipClass { get; } = "chooseAnotherStateOrZip";
        public string CheckStoreChooseAnotherStateOrZipXpath { get; } = "//button[contains(@class,'chooseAnotherStateOrZip')]";
        public string CheckStoreModalFirstNameId { get; } = "firstName";
        public string CheckStoreModalPhoneId { get; } = "phoneNumber";
        public string CheckStoreModalQuestionId { get; } = "question";
        public string CheckStoreModalReserveItemButtonClass { get; } = "selectStore";
        public string CheckAvailabilityStoreListClass { get; } = "storeList";
        public string CheckStoreAvailabilitySearchButtonClass { get; } = "checkStoreAvailabilitySearchButton";
        public string CheckStoreCallButtonXpath { get; } = "//*[@id='bdContent']/div[2]/div[2]/ul/li[1]/div/a";
        public string CsInfoClass { get; } = "csInfo";
        public string CustomerPhotosClass { get; } = "imageTab--customer";
        public string CustomerPhotosXpath { get; } = "//li[text()='Customer Photos']";
        public string EnergyGuideIconId { get; } = "jsEnergyInfoLogo";
        public string EnergyInfoModalId { get; } = "jsEnergyInfoModalData";
        public string GiftCardDenominationClass { get; } = "giftCardAmountItem";
        public string GiftCardFirstNameId { get; } = "giftCardFirstName";
        public string GiftCardLastNameId { get; } = "giftCardLastName";
        public string GiftCardMessageId { get; } = "giftCardMessage";
        public string HousingOptionsId { get; } = "housing-options";
        public string ImageClass { get; } = "image";
        public string InlineEmailsClass { get; } = "inlineEmails";
        public string LblFreeReturnsBottomId { get; } = "lblFreeReturnsBottom";
        public string LblFreeShippingToStatesWithStoresId { get; } = "lblFreeShippingToStatesWithStores";
        public string LblPriceId { get; } = "lblPrice";
        public string LblStockInventoryId { get; } = "lblStockInventory";
        public string LongSkuClass { get; } = "longSku";
        public string NotifyMeMessageContainerSuccessClass { get; } = "notifyme__message-container--success";
        public string OpenBoxAvailableLinkId { get; } = "openBoxPdpLink";
        public string MarginModalLinkId { get; } = "marginModalLink";
        public string PdAddToCartSystemOptionsId { get; } = "pdAddToCartSystemOptions";
        public string PdAddToPortfolioNormalId { get; } = "pdAddToPortfolioNormal";
        public string PdChatId { get; } = "pdChat";
        public string PdCheckStoreAvailabilityId { get; } = "pdCheckStoreAvailability";
        public string PdEmailItemId { get; } = "pdEmailItem";
        public string PdEmailItemXpath { get; } = "//button[@id='pdEmailItem']";
        public string PdFirstNameId { get; } = "FirstName";
        public string PdFromEmailId { get; } = "FromEmail";
        public string PdHousingOptionsProdContainerClass { get; } = "pdHousingOptionsProdContainer";
        public string PdRelItmsContainerId { get; } = "pdRelItmsContainer";
        public string PdInStockId { get; } = "pdInStock";
        public string PdLastNameId { get; } = "LastName";
        public string PdPanesId { get; } = "pdPanes";
        public string PdPostalCodeId { get; } = "PostalCode";
        public string PdProdInfoColId { get; } = "pdProdInfoCol";
        public string PdProdSkuId { get; } = "pdProdSku";
        public string PdQandAId { get; } = "pdQandA";
        public string PdQuickPrintClass { get; } = "pdQuickPrint";
        public string PdQuickPrintId { get; } = "pdQuickPrint";
        public string PdRelVideosId { get; } = "pdRelatedVideos";
        public string PdReviewModalId { get; } = "TTtraUserStateMain";
        public string PdScrollableOtherOptionsId { get; } = "pdScrollableOtherOptions";
        public string PdSectionTitleClass { get; } = "pdSectionTitle";
        public string PdSocialIconClass { get; } = "pdSocial__icon";
        public string PdSocialPrintIconId { get; } = "pdPrint";
        public string PdSocialPrintIconXpath { get; } = "//button[@id='pdPrint']";
        public string PdPleaseCallClass { get; } = "pdPleaseCall";
        public string PriceAdditionalSaveId { get; } = "priceAdditionalSave";
        public string PriceTypeClass { get; } = "priceType";
        public string PrintKioskStyleButtonId { get; } = "printKioskStyleButton";
        public string PrintKioskStyleButtonXpath { get; } = "//*[@id='printKioskStyleButton']";
        public string PrintKioskStyleProductBtnId { get; } = "printKioskStyleProductBtn";
        public string PrintKioskStyleProductBtnXpath { get; } = "//*[@id='printKioskStyleProductBtn']";
        public string QtyLeftClass { get; } = "qtyLeft";
        public string QuestionAndAnswerCommentColumns { get; } = "TT3cText";
        public string RelatedItemSectionId { get; } = "pnlRelatedItems";
        public string ReviewsSectionId { get; } = "productReviewsCollapsibleButton";
        public string RelatedItemsId { get; } = "related-items";
        public string SamplePhotosClass { get; } = "samplePhotos";
        public string SamplePhotosTabClass { get; } = "samplePhotosTab";
        public string SampleRoomBtnClass { get; } = "sampleRoomBtn";
        public string SearchZipInputId { get; } = "txtZipCode";
        public string SendEmailButtonClass { get; } = "sendEmailBtn";
        public string SelectedThumbnailWrapperClass { get; } = "selectedThumbnail";
        public string ShowInRoomId { get; } = "showInRoom";
        public string StockCheckClass { get; } = "stockCheck";
        public string StockCheckTitleClass { get; } = "stockCheckTitle";
        public string StoreInventoryClass { get; } = "storeInventory";
        public string ThumbnailWrapperClass { get; } = "thumbnailWrapper";
        public string ActiveCarouselClass { get; } = "react-multi-carousel-item--active"; 
        public string ThumbnailsCarouselClass { get; } = "react-multi-carousel-item";
        public string Tt2InlineAnswerId { get; } = "TT2inlineAnswer";
        public string Tt2QuestionTextId { get; } = "TT2questionText";
        public string TtQnASearchBarId { get; } = "searchQuery_questionsAndAnswers";
        public string TtProductSearchResultsClass { get; } = "ProductSearchUGCResults";
        public string ReviewImageClass { get; } = "ProductReviewCard__mediaItem";
        public string Tt4AddAnswerClass { get; } = "TT4addAnswer";
        public string Tt2CancelBtnId { get; } = "TT2cancelBtn";
        public string TtBrowseQaWrapperId { get; } = "TTbrowseQAWrapper";
        public string TtImgFileAttributeValue { get; } = "TTimgFile";
        public string TtMediaListContPhotoId { get; } = "TTmediaListCont-photo";
        public string TtMediaSubmitBtnId { get; } = "TTmediaSubmitBtn";
        public string TtQuestionIdAttributeName { get; } = "ttqid";
        public string TtRevCatItemImgId { get; } = "TTrevCatItemImg";
        public string TtRevRatingId { get; } = "TTrevRating";
        public string TtReviewTextId { get; } = "TTreviewText";
        public string TtReviewSectionId { get; } = "turntoReviewsSection";
        public string TtReviewTitleId { get; } = "TTreviewTitle";
        public string TtSelectedClass { get; } = "TTselected";
        public string TtShareMediaScreenId { get; } = "TTshareMediaScreen";
        public string TtTraWindowCloseId { get; } = "TTtraWindowClose";
        public string TtVcAddNewPhotoClass { get; } = "TTvc-add-new-photo";
        public string TtVcBarMediaLgPhotoClass { get; } = "TTvc-bar-media-lg-photo";
        public string TtWriteRevGreetId { get; } = "TTwriteRevGreet";
        public string TtWriteReviewBtnId { get; } = "TTwriteReviewBtn";
        public string TtWriteReviewScreenId { get; } = "TTwriteReviewScreen";
        public string TtmediaSmallDescClass { get; } = "TTmediaSmallDesc";
        public string TurnToModalWindowXpath { get; } = "//*[@id=\"TTtraWindow\"][contains(@style,\"display: block;\")]";
        public string ProductSkuId { get; } = "pdProdSku";
        public string ZipInputFieldCheckStoreClass { get; } = "adjacentButton";
        public string TurnTwoQuestionTextInputLabelTextAttribute { get; } = "ttinputlabeltext";
        public string SocialLinksClass { get; } = "socialLinks";
        public string PriceWithoutText => Price.Text.Replace("Sale", string.Empty).Replace("Daily Sale", string.Empty).Replace("Clearance", string.Empty).Trim();
        public string ProductSkuNumberWithoutPrefix => ProductSkuNumber.Replace("- Style # ", "");
        public string ProductSkuNumberWithoutPrefixOnCheckStore => ModalShortSku.Replace("Style # ", "");
        public string ProsRetailPriceClass { get; } = "regPrice";
        public string ProsTradePriceId { get; } = "lblPrice";
        public string ProsSavingId { get; } = "lblTradeSavings";
        public string YourTradePriceClass { get; } = "tradePriceLabel";
        public string RecentlyViewedLoadedClass { get; } = "recentlyViewedWrapper";
        public string RecentlyViewedXPath { get; } = "//*[@id='lblStandardFooter']//a[@href='/recently-viewed/']";
        public string EndsDateClass { get; } = "endsDate";
        public string ProductReviewCardClass { get; } = "ProductReviewCard__mediaItem";
        public string SaleVerbiageClass { get; } = "sale";
        public string OrigPriceClass { get; } = "origPrice";
        public string saleVerbiageCallClass { get; } = "sale";
        public string ReviewBtnSelector { get; } = ".productReviews__writeReviewBtnWrapper";
        public string QuestionsAndAnswersSectionTitleClass { get; } = "pdSectionTitle";
        public string WriteReviewModalCloseCssSelector { get; } = ".tt-o-modal__close";
        public string RecentlyViewContainerId { get; } = "recentlyViewedContainer";
        public string TwentyFiveDollarGiftCardString { get; } = "$25";
        public string WidgetFloatingWrapperClass { get; } = "widget-floating__wrapper";

        public abstract string MoreYouLikeBorderClass { get; }
        public abstract string SkuOnPdpXpath { get; }
        public abstract string BreadCrumbXpath { get; }
        public abstract string ShopAllColorText { get; }
        public abstract string ViewInYourRoomSelectPhotoXpath { get; }
        public abstract string ViewInYourRoomSampleImageXpath { get; }
        public abstract string ViewInYourRoomXpath { get; }
        public abstract string WriteReviewBtnSelector { get; }
        public abstract string WriteReviewBtnXpath { get; }
        public abstract string WriteReviewModalXpath { get; }
        public abstract string WriteReviewModalSelector { get; }
        public abstract string AssetActionsClass { get; }
        public abstract string AvailabilityPhoneNumberString { get; }
        public abstract string AvailabilityString { get; }
        public abstract string AvailabilityTextString { get; }
        public abstract string ToOrderCalloutString { get; }
        public abstract string WishListIndicatorString { get; }
        public abstract string AddToWishListButtonXpath { get; }
        public abstract string AdjacentButtonClass { get; }
        public abstract string AppCheckStoreAvailabilityClass { get; }
        public abstract string BtnPdpZoomClass { get; }
        public abstract string BuildFullSystemContainerId { get; }
        public abstract string BuildFullSystemId { get; }
        public abstract string CallStoreButtonClass { get; }
        public abstract string DivProductDetailTop { get; }
        public abstract string TurnToQuestionAndAnswerSection { get; }
        public abstract string FsImageContainerId { get; }
        public abstract string GiftCardDenominationXpath { get; }
        public abstract string GoodtoKnowIconClass { get; }
        public abstract string JsCertonaTitleClass { get; }
        public abstract string JsOtherOptionLinkClass { get; }
        public abstract string LblStickyPriceId { get; }
        public abstract string LblStickyPriceClass { get; }
        public abstract string LimitedQtyFieldClass { get; }
        public abstract string LimitedQuantitySelectionId { get; }
        public abstract string LpCollapsibleCollapsedHidden { get; }
        public abstract string LpContainerId { get; }
        public abstract string LpMobileAccordionClass { get; }
        public abstract string LpModalContentId { get; }
        public abstract string MainImagePathXpath { get; }
        public abstract string ModalProductImageThumbnailXpath { get; }
        public abstract string PdpAddToWishlistClass { get; }
        public abstract string PdAddToCartBuildFullId { get; }
        public abstract string PdAddToCartStickyId { get; }
        public abstract string PdAddToPortfolioSystemOptionsId { get; }
        public abstract string PdFanFeatures { get; }
        public abstract string PdProdImgStickyId { get; }
        public abstract string PdProdTitleStickyClass { get; }
        public abstract string PdProdImgClass { get; }
        public abstract string PdHeroSpotId { get; }
        public abstract string PdHeroImageId { get; }
        public abstract string pdImgContainerId { get; }
        public abstract string PdRelatedItmsId { get; }
        public abstract string PdRelatedItmsXpath { get; }
        public abstract string PdReviewsId { get; }
        public abstract string PdFirstRelatedItmXpath { get; }
        public abstract string PdpStickyHeaderId { get; }
        public abstract string PdpStickyHeaderImageWrapperClass { get; }
        public abstract string PdViewFullTrackSystemId { get; }
        public abstract string PnlProductDescriptionId { get; }
        public abstract string PopularColorsId { get; }
        public abstract string ProductDescId { get; }
        public abstract string ProductDescSelector { get; }
        public abstract string ProductDetailsSectionId { get; }
        public abstract string ProductReviewsCollapsibleSelector { get; }
        public abstract string ProductReviewRatingStarCountClass { get; }
        public abstract string ReplacementPartLinkId { get; }
        public abstract string ReplacementPartSkuXpath { get; }
        public abstract string ProductNameAndNumber { get; }
        public abstract string QandAId { get; }
        public abstract string QtyLimitedName { get; }
        public abstract string QtyNormalId { get; }
        public abstract string QuantityDrawerXpath { get; }
        public abstract string RelatedItemsContainerXpath { get; }
        public abstract string RelatedItemSectionXpath { get; }
        public abstract string ReviewsSectionXpath { get; }
        public abstract string ProductAttributesClass { get; }
        public abstract string ProductReviewsSectionId { get; }
        public abstract string ProductReviewSweepstakeClass { get; }
        public abstract string ProductTechnicalSpecificationsClass { get; }
        public abstract string ReviewsElementSelector { get; }
        public abstract string SearchResultsListClass { get; }
        public abstract string ShipsInMessageClass { get; }
        public abstract string SlickActiveClass { get; }
        public abstract string SlickListClass { get; }
        public abstract string StickyWrapperId { get; }
        public abstract string StoreAvailabilityClass { get; }
        public abstract string StoreAvailabilityLocatorContentClass { get; }
        public abstract string StoreAvailabilityQuestionsClass { get; }
        public abstract string SystemOptionsQtyClass { get; }
        public abstract string ThumbnailImageCarouselClass { get; }
        public abstract string ThumbnailImageCarouselId { get; }
        public abstract string TextAttributeValue { get; }
        public abstract string TitleAttributeValue { get; }
        public abstract string ToOrderCalloutClass { get; }
        public abstract string ToOrderCallCalloutOnOrdersOver49Class { get; }
        public abstract string ProsSpecialPriceCallOutClass { get; }
        public abstract string Tt4QProductImgClass { get; }
        public abstract string TtLeftHeaderClass { get; }
        public abstract string TtrespMobileDispInlineClass { get; }
        public abstract string TtOverallRatingStarsId { get; }
        public abstract string TtWriteReviewBtnPortraitId { get; }
        public abstract string StockCheckXpath { get; }
        public abstract string SelectStoreClass { get; }
        public abstract string StoreAssociateId { get; }
        public abstract string StoreAssociateModalClass { get; }
        public abstract string MediaModalContentModalClass { get; }
        public abstract string StickySaveXpath { get; }
        public abstract string StickyPriceClass { get; }
        public abstract string StickyCallOutClass { get; }
        public abstract string EndVerbiageOnSfpStickyXpath { get; }
        public abstract string MobileStruckPriceSfpXpath { get; }
        public abstract string StickyContainerSfpClass { get; }
        public abstract string LoadMoreReviewsBtnClass { get; }
        public abstract string ReplacementPartModalClass { get; }
        public abstract string StickySaleClass { get; }
        public abstract string ShowInRoomBtnId { get; }
        public abstract string PdpArIframeXpath { get; }
        public abstract string ArViewerBtnClass { get; }
        public abstract string GetStartedBtnClass { get; }
        public abstract  string ProductHeroThumbnailClass { get; }
        public abstract string CustomerPhotosThumbnailClass { get; }
        public abstract string ProductHeroImageClass { get; }
        public abstract string ProductCustomerPhotosClass { get; }
        public abstract string EmailRecipientListClass { get; }
        public abstract string DesignChatClass { get;}
        public abstract string ProductHelpId { get; }
        public abstract string NeedHelpChatClass { get; }
        public abstract string CloseNeedHelpClass { get; }
        public abstract IElement ProductHeroThumbnail { get; }
        public abstract IElement CustomerPhotosThumbnail { get; }
        public abstract IElement ProductHeroImage { get; }
        public abstract IElement ProductCustomerPhotos { get; }
        public abstract IElement DesignChatLink { get; }
        public abstract IElement ProductHelp { get; }
        public abstract IElement NeedHelpChat { get; }
        public abstract IElement CloseNeedHelp { get; }
        #endregion

        #region Page Elements
        public IElement RecentlyViewedContainer => Browser.Locate.ElementById(RecentlyViewContainerId);
        public IElement EndVerbiageOnSfp => Browser.Locate.ElementByXpath(EndVerbiageOnSfpStickyXpath);
        public IElement RecentlyViewed => Browser.Locate.ElementByXpath(RecentlyViewedXPath);
        public IElement RecentlyViewedLoaded => Browser.Locate.ElementByClassName(RecentlyViewedLoadedClass);
        public IElement YourTradePrice => Browser.Locate.ElementByClassName(YourTradePriceClass);
        public IElement ProsRetailPrice => Browser.Locate.ElementByClassName(ProsRetailPriceClass);
        public IElement ProsTradePrice => Browser.Locate.ElementById(ProsTradePriceId);
        public IElement ProsSaving => Browser.Locate.ElementById(ProsSavingId);
        public IElement StoreAssociateModal => Browser.Locate.ElementByClassName(StoreAssociateModalClass);
        public IElement MediaModalContentModal => Browser.Locate.ElementByClassName(MediaModalContentModalClass);
        public IElement StickyHeaderPrice => Browser.Locate.ElementBySelector(PdpStickyHeaderPriceClass.ToCssClassSelector());
        public IElement StoreAssociate => Browser.Locate.ElementById(StoreAssociateId);
        public IElement SelectStore => Browser.Locate.ElementByClassName(SelectStoreClass);
        public IElement StockCheck => Browser.Locate.ElementByClassName(StockCheckClass);
        public IElement OpenBoxAvailableLink => Browser.Locate.ElementById(OpenBoxAvailableLinkId);
        public IElement AddToCartLabelElement => Browser.Locate.ElementByLinkText("Add to Cart");
        public virtual IElement AddToWishListButton => Browser.Locate.ElementById(PdAddToPortfolioNormalId);
        public IElement VirtualAssistantCloseButton => Browser.Locate.ElementByClassName(WidgetFloatingButtonCloseClass);
        public IElement VirtualAssistantCloseConfirmationButton => Browser.Locate.ElementByClassName(ConfirmationDialogButtonClass);
        public IElement VirtualAssistantContainer => Browser.Locate.ElementByClassName(WidgetFloatingWrapperClass);
        public IElement BoldChatStartBtn => Browser.Locate.ElementBySelector("[value='Start Chat']");
        public IElement BoldChatFrame => Browser.Locate.ElementBySelector(BcChatContainerFrame);
        public IElement GoodToKnow => Browser.Locate.ElementByClassName(GoodToKnowClass);
        public IElement BrandLogo => Browser.Locate.ElementById(PnlBrandId);
        public IElement BrandLogoLink => Browser.Locate.ElementByTagName(HtmlTextWriterTag.A, BrandLogo, true);
        public IElement BreadCrumbElement => Browser.Locate.ElementById(DivBreadCrumbId);
        public IElement BuyItNewLink => Browser.Locate.ElementByTagName(HtmlTextWriterTag.A, Browser.Locate.ElementByClassName(BuyItNewLinkClass));
        public IElement CheckAvailabilityModalCloseButton => Browser.Locate.ElementByClassName(OverlayContentWrapperCloseButtonClass);
        public IElement CheckStoreAvailabilityModal => Browser.Locate.ElementByClassName(OverlayContentWrapperClass);
        public IElement ComparePriceCallout => Browser.Locate.ElementBySelector(ComparePriceClass.ToCssClassSelector());
        public IElement EnergyGuideIcon => Browser.Locate.ElementById(EnergyGuideIconId);
        public IElement EnergyInfoModal => Browser.Locate.ElementById(EnergyInfoModalId);
        public IElement FreeShippingAndReturnElement => Browser.Locate.ElementBySelector(LblFreeReturnsBottomId.ToCssIdSelector());
        public IElement FreeShippingCallout => Browser.Locate.ElementById(LblFreeShippingId);
        public IElement GiftCardDenomination(int index) => Browser.Locate.ElementsByClassName(GiftCardDenominationClass)[index];
        public IElement GiftCardFirstName => Browser.Locate.ElementByXpath("//*[@id='giftCardFirstName']");
        public IElement GiftCardLastName => Browser.Locate.ElementByXpath("//*[@id='giftCardLastName']");
        public IElement GiftCardMessage => Browser.Locate.ElementByXpath("//*[@id='giftCardMessage']");
        public IElement EndVerbiagePlaAndSfp => Browser.Locate.ElementByClassName(EndVerbiageSfpAndPlaClass);
        public IElement ItemPrice => Browser.Locate.ElementById(ProductPriceId);
        public IElement ManufacturerLink => Browser.Locate.ElementByClassName(PdManufacturerLinkClass);
        public IElement ManufacturerLinkAnchor => Browser.Locate.ElementByTagName(HtmlTextWriterTag.A, ManufacturerLink);
        public string ModalShortSku => Browser.Locate.ElementByClassName(ModalShortSkuClass).Text;
        public IElement MoreYouMayLikeContainer => Browser.Locate.ElementBySelector(PdMymlSectionItemId.ToCssIdSelector());
        public IElement OrigPrice => Browser.Locate.ElementByClassName(OrigPriceClass);
        public IElement PdMymlSection => Browser.Locate.ElementBySelector(PdMymlSectionId.ToCssIdSelector());
        public IElement PdMymlSectionItem => Browser.Locate.ElementByTagName(HtmlTextWriterTag.A, Browser.Locate.ElementById(PdMymlSectionItemId));
        public IElement PdRecentlyViewedSectionItem => Browser.Locate.ElementByTagName(HtmlTextWriterTag.A, Browser.Locate.ElementById(PdRecentlyViewedSectionItemId));
        public IElement PdRelItmsContainer => Browser.Locate.ElementById(PdRelItmsContainerId);
        public IElement PdRelVideosContainer => Browser.Locate.ElementById(PdRelVideosId);
        public IElement Price => Browser.Locate.ElementById(LblPriceId);
        public IElement PriceAdditionalSave => Browser.Locate.ElementById(PriceAdditionalSaveId);
        public IElement ProductQtyCallOut => Browser.Locate.ElementById(AvailInventoryId);
        public IElement ProductSkuLabel => Browser.Locate.ElementById(PdProdSkuId);
        public IElement QuantityField => Browser.Locate.ElementBySelector($"#{QtyNormalInputId}, #{QtyMultiProdId}");
        public IElement RecentlyViewedViewAllButton => Browser.Locate.ElementByClassName(ViewAllRecentlyViewedButtonClass);
        public IElement RelatedItemAnchor => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.A, "data-scheme", "related_rr", RelatedItems[0]);
        public virtual IElement RelatedItemsContainer => Browser.Locate.ElementByTagName(HtmlTextWriterTag.A, PdRelItmsContainer);
        public IElement RelatedVideo => Browser.Locate.ElementByTagName(HtmlTextWriterTag.Img, PdRelVideosContainer);
        public IElement ReplacementPartLink => Browser.Locate.ElementById(ReplacementPartLinkId);
        public IElement ShipsFreeWithOrdersOver49CallOut => Browser.Locate.ElementById(ShipsFreeWithOrdersOverFortyNineCallOutId);
        public IElement SingleQuestionAndAnswerElementResult => Browser.Locate.ElementImmediately(Tt3QuestWrpClass.ToCssClassSelector());
        public IElement ReplacementProductPartModal => Browser.Locate.ElementByClassName(ReplacementPartModalClass);
        public IElement TtProductSearchResultCards(int index) => Browser.Locate.ElementsByClassName(TtProductSearchResultsClass)[index];
        public IElement ReviewImage(int index) => Browser.Locate.ElementsByClassName(ReviewImageClass)[index];
        public string StoreTitle => Browser.Locate.ElementByClassName(StoreTitleClass).Text;
        public string StoreName => Browser.Locate.ElementByClassName(StoreNameClass).Text;
        public string GiftCardValue => Browser.Locate.ElementByClassName(ProductNameAndNumber).Text;


        public IElement TurnToSubmitAnswerButton => Browser.Locate.ElementById($"{Tt2AnswerBtnId}-{TurnTwoQuestionId}");
        public IElement TradePriceLabel => Browser.Locate.ElementByClassName(TradePriceLabelClass);
        public IElement YourSavingsCallout => Browser.Locate.ElementById(TradeSavingsId);
        public IElement TurnTwoAskAQuestionCloseButton => Browser.Locate.ElementById(TurnTwoAskQuestionCloseButtonId);
        public IElement TurnTwoAskAQuestionTextArea => Browser.Locate.ElementById(Tt2QuestionTextId);
        public IElement TtQnASearchBar => Browser.Locate.ElementById(TtQnASearchBarId);
        public IElement TtProductSearchResults => Browser.Locate.ElementByClassName(TtProductSearchResultsClass);
        public IElement TurnToReviewSection => Browser.Locate.ElementById(TtReviewSectionId);
        public IElement VideoWindow => Browser.Locate.ElementById(PlayerId); 
        public IElement StickyCallOut => Browser.Locate.ElementByClassName(StickyCallOutClass);
        public IElement RelatedItemSection => Browser.Locate.ElementById(RelatedItemSectionId);

        public abstract IElement InStockElement { get; }
        public abstract IElement BuildFullSystemAddToCartButton { get; }
        public abstract IElement GetAllColorPlusElement { get; }
        public abstract IElement StickyContainerSfp { get; }
        public abstract IElement StickySaleCallout { get; }
        public abstract IElement ActiveMainProductImage { get; }
        public abstract IElement AskStoreAssociate { get; }
        public abstract IElement BoldChatButtonContainer { get; }
        public abstract IElement BottomPortionOfPdp { get; }
        public abstract IElement BuildFullSystemAddToWishListButton { get; }
        public abstract IElement BuildFullSystemButton { get; }
        public abstract IElement BuildFullSystemContainer { get; }
        public abstract IElement BuildFullSystemProductContainer { get; }
        public abstract IElement BuildFullSystemOptions { get; }
        public abstract IElement CallStoreButton { get; }
        public abstract IElement CityTitle { get; }
        public abstract IElement CarouselImage(int index);
        public abstract IElement CertonaDrawerName { get; }
        public abstract IElement ChatButtonLink { get; }
        public abstract IElement CheckAvailabilityModal { get; }
        public abstract IElement CheckAvailabilityStoreList { get; }
        public abstract IElement CheckStoreChooseAnotherStateOrZip { get; }
        public abstract IElement CheckStoreFirstName { get; }
        public abstract IElement CheckStoreCallButton { get; }
        public abstract IElement CheckStorePhone {get;}
        public abstract IElement CheckStoreQuestion { get; }
        public abstract IElement CheckStoreReserveItemButton { get; }
        public abstract IElement CheckStoreReserveItemButtonBottom { get; }
        public abstract IElement CheckStoreSearchButton { get; }
        public abstract IElement CheckStoreSearchArrow { get; }
        public abstract IElement CustomerPhotos { get; }
        public abstract IElement CsInfo { get; }
        public abstract IElement EmailLink { get; }
        public abstract IElement EmailModalContent { get; }
        public abstract IElement EmailRecipientTextbox { get; }
        public abstract IElement EmailRecipientList { get; }
        public abstract IElement FanFeatures { get; }
        public abstract IElement FirstNameTextbox { get; }
        public abstract IElement FooterChatLink { get; }
        public abstract IElement FreeShippingToStatesWithStoresLabel { get; }
        public abstract IElement FromEmailTextbox { get; }
        public abstract IElement GetYourPhotoFrame { get; }
        public abstract IElement HeaderChatLink { get; }
        public abstract IElement HousingOptions { get; }
        public abstract IElement HousingOptionsSectionHeader { get; }
        public abstract IElement ImageContainer { get; }
        public abstract IElement LastNameTextbox { get; }
        public abstract IElement LblStockInventory { get; }
        public abstract IElement LimitedQtyField { get; }
        public abstract IElement LimitedQuantitySelection { get; }
        public abstract IElement LongSkuElement { get; }
        public abstract IElement MainImagePath (int index);
        public abstract IElement MainProductImage { get;}
        public abstract IElement MarginModalLink { get; }
        public abstract IElement MobileAddToCartButtonContainer { get; }
        public abstract IElement MobileAccordionContainer { get; }
        public abstract IElement MobileGiftCardDenomination { get; }
        public abstract IElement MobileMaxQuantity { get; }
        public abstract IElement ModalProductImageThumbnail { get; }
        public abstract IElement MoreImages(int index);
        public abstract IElement PdImageColumn { get; }
        public abstract IElement PdChat { get; }
        public abstract IElement PdProdInfoColElement { get; } 
        public abstract IElement PdProdSpecificationsTables { get; }
        public abstract IElement PdReviewsElement { get; }
        public abstract IElement PdSocialIconElement { get; }
        public abstract IElement PdSocialPrintIconElement { get; }
        public abstract IElement PdpAddToWishlist { get; }
        public abstract IElement PopularColorsDropdown { get; }
        public abstract IElement PriceType { get; }
        public abstract IElement PrintKioskStyleButtonElement { get; }
        public abstract IElement PrintKioskStyleProductBtnElement { get; }
        public abstract IElement ProductAttributes { get; }
        public abstract IElement ProductCallOut { get; }
        public abstract IElement ProductDetailPageContainer { get; }
        public abstract IElement ProductDescDropDown { get; }
        public abstract IElement ProductDescriptionAccordion { get; }
        public abstract IElement ProductDetailSection { get; }
        public abstract IElement ProductDetailSpecificationSection { get; }
        public abstract IElement ProductGoodToKnowSection { get; }
        public abstract IElement ProductReviewsSection { get; }
        public abstract IElement ProductReviewSweepstake { get; }
        public abstract IElement ProductSpecificationsTables { get; }
        public abstract IElement PdHeroSpot { get; }
        public abstract IElement pdImgContainer { get; }
        public abstract IElement ProductImage { get; }
        public abstract IElement StockCheckTitleElement { get; }
        public abstract IElement ProductImageThumbnail { get; }
        public abstract IElement ProductInStockTextLink { get; }
        public abstract IElement ProductSlider { get; }
        public abstract IElement ProsSpecialPriceCallout { get; }
        public abstract IElement QuestionsAndAnswersChatContainer { get; }
        public abstract IElement QuestionsAndAnswersChatLink { get; }
        public abstract IElement QuickPrintInput { get; }
        public abstract IElement QuickPrintLink { get; }
        public abstract IElement RelatedItemDropdown { get; }
        public abstract IElement ReviewsSection { get; }
        public abstract IElement RelatedItemsSection { get; }
        public abstract IElement SamplePhotosTab { get; }
        public abstract IElement SampleRoomBtn { get; }
        public abstract IElement SearchResultsList { get; }
        public abstract IElement SendEmailButton { get; }
        public abstract IElement SelectedThumbnailWrapper { get; }
        public abstract IElement SelectedDifferentThumbnailWrapper { get; }
        public abstract IElement SearchZipTextBox { get; }
        public abstract IElement StickyAddToCart { get; }
        public abstract IElement StickyImage { get; }
        public abstract IElement StickyImageWrapper { get; }
        public abstract IElement StickyPrice { get; }
        public abstract IElement StickyTitle { get; }
        public abstract IElement StickyWrapper { get; }
        public abstract IElement StockCheckElement { get; }
        public abstract IElement StockCheckWrapper { get; }
        public abstract IElement StoreAvailability { get; }
        public abstract IElement StoreAvailabilityQuestions { get; }
        public abstract IElement StoreAvailabilityLocatorContent { get; }
        public abstract IElement ProductQuestionAnswerArrow { get; }
        public abstract IElement StoreInventoryElement { get; }
        public abstract IElement SocialLinksContainer { get; }
        public abstract IElement TextStoreButton { get; }
        public abstract IElement ThumbnailCarouselImage { get; }
        public abstract IElement ThumbnailWrapper { get; }
        public abstract IElement TopContentProductDetail { get; }
        public abstract IElement TurnToDynamicAddAnswerButton { get; }
        public abstract IElement TurnToDynamicAddQuestionsCancelButton { get; }
        public abstract IElement TurnToDynamicAddAnswerTextArea { get; }
        public abstract IElement TurnToQuestionAndAnswerContainer { get; }
        public abstract IElement TurnToQuestionsAndAnswersSection { get; }
        public abstract IElement TurnToReviewAddNewPhotoButton { get; }
        public abstract IElement TurnToReviewAttachPhoto { get; }
        public abstract IElement TurnToReviewFileInput { get; }
        public abstract IElement TurnToReviewFileMediaListSelected { get; }
        public abstract IElement TurnToReviewMediaSubmitButton { get; }
        public abstract IElement TurnToReviewProductImage { get; }
        public abstract IElement TurnToReviewProductName { get; }
        public abstract IElement TurnToReviewRating { get; }
        public abstract IElement TurnToReviewScreen { get; }
        public abstract IElement TurnToReviewModal { get; }
        public abstract IElement ProductReviewModal { get; }
        public abstract IElement TurnToReviewShareMediaScreen { get; }
        public abstract IElement QuestionsAndAnswersCommentsSection { get; }
        public abstract IElement ToOrderCalloutOnPdp { get; }
        public abstract IElement TurnToReviewText { get; }
        public abstract IElement TurnToReviewTitle { get; }
        public abstract IElement TurnToReviewWindow { get; }
        public abstract IElement TurnToWriteReviewButton { get; }
        public abstract IElement TurnTwoBrowseQaWrapper { get; }
        public abstract IElement WishListIndicatorIcon { get; }
        public abstract IElement ZipcodeTextbox { get; }
        public abstract IElement ZipInputFieldCheckStore { get; }
        public abstract IElement ZoomIcon { get; }
        public abstract IElement StickySaveCallout { get; }
        public abstract IElement EndsDate(int index);
        public abstract IElement ProductReviewCard(int index);
        public abstract IElement StickyPriceCallout { get; }
        public abstract IElement MobileStruckPriceSfp { get; }
        public abstract IElement WriteReviewBtn { get; }
        public abstract IElement LoadMoreReviews { get; }
        public abstract IElement SoldOutLabel { get; }
        public abstract IElement ShowInRoomBtn { get; }
        public abstract IElement PdpArIframe { get; }
        public abstract IElement ArViewerBtn (int index);
        public abstract IElement GetStartedBtn { get; }
        public ReadOnlyCollection<IElement> ListOfBreadCrumbLink() => BreadCrumbElement.FindElements(By.TagName("a"));
        public ReadOnlyCollection<IElement> ListOfFullSystemData(int nthIndex) => Browser.Locate.ElementsBySelector($"{HtmlTextWriterTag.Td.ToNthChildSelector(nthIndex)}", BuildFullSystemOptions);
        public ReadOnlyCollection<IElement> RelatedItems => Browser.Locate.ElementsByTagNameAndAttributeName(HtmlTextWriterTag.A, "data-certonasku", PdRelItmsContainer);
        public ReadOnlyCollection<IElement> ReplacementPartSku => Browser.Locate.ElementsByXpath(ReplacementPartSkuXpath);
        public abstract ReadOnlyCollection<IElement> BuildFullSystemQtyElements { get; }
        public abstract ReadOnlyCollection<IElement> BuildFullSystemShortSkuLinks { get; }
        public abstract ReadOnlyCollection<IElement> HousingOptionsSectionDivContainers { get; }
        public abstract ReadOnlyCollection<IElement> ListOfFullSystemProductNames { get; }
        public abstract ReadOnlyCollection<IElement> ListOfFullSystemSkus { get; }
        public abstract ReadOnlyCollection<IElement> GoodToKnowIcon { get; }
        public abstract ReadOnlyCollection<IElement> MoreThumbnailImage { get; }
        public abstract ReadOnlyCollection<IElement> ProductSliders { get; }
        public abstract ReadOnlyCollection<IElement> SamplePhotos { get; }
        public abstract ReadOnlyCollection<IElement> ThumbnailImageCarousel { get; }

        public IElement GetScrollableHeaderByName(string name)
        {
            var scrollBarHeaders = Browser.Locate.ElementsByClassName(ScrollableHeaderClass);

            foreach (var item in scrollBarHeaders)
            {
                if (item.Text.Trim().ToUpper().Equals(name.ToUpper()))
                {
                    return item;
                }
            }

            return null;
        }

        public IElement GetCourseTitleByName(string name)
        {
            var courseTitles = Browser.Locate.ElementsByClassName(CourseTitleClass);

            foreach (var item in courseTitles)
            {
                if (item.Text.Trim().ToUpper().Equals(name.ToUpper()))
                {
                    return item;
                }
            }

            return null;
        }
        #endregion

        public decimal TradePrice
        {
            get
            {
                var tradePriceText = ItemPrice.Text;
                if (string.IsNullOrWhiteSpace(tradePriceText)) return 0;
                decimal.TryParse(tradePriceText.Replace("$", ""), out var tradePrice);

                return tradePrice;
            }
        }

        public decimal YourSavingsPrice
        {
            get
            {
                var tradeSavingText = YourSavingsCallout.Text;
                if (string.IsNullOrWhiteSpace(tradeSavingText)) return 0;
                var tradeSavingPrice = tradeSavingText.Replace("Your Savings $", "");
                decimal.TryParse(tradeSavingPrice, out var tradePrice);

                return tradePrice;
            }
        }
        
        public string SkuStatusValue()
        {
            var skuStatusValue = string.Empty;
            string[] skuStatusValues = null;
            if (CsInfo.Displayed && CsInfo.Text.Contains(SkuStatusLabel))
            {
                skuStatusValue = CsInfo.Text.Split(new[] { SkuStatusLabel }, StringSplitOptions.None).Last();
                skuStatusValues = skuStatusValue.Split();
            }
            return skuStatusValues[1];
        }

        public string TurnTwoQuestionId
        {
            get
            {
                if (string.IsNullOrEmpty(_turnTwoQuestionId))
                {
                    _turnTwoQuestionId = Browser.Locate
                        .ElementByAttributeName(TtQuestionIdAttributeName, TurnTwoBrowseQaWrapper)
                        .GetAttribute(TtQuestionIdAttributeName);
                }

                return _turnTwoQuestionId;
            }
        }

        public string GetRandomSku(List<string> shortSku)
        {
            Random random = new Random();
            var index = random.Next(shortSku.Count);
            var randomSku = shortSku[index];

            return randomSku;
        }

        public abstract (string textMessage, string phoneNumber) GetTxtMessageAndPhoneNumber();

        public abstract bool IsQuantityLeftShows { get; }
        public abstract bool IsReplacementPartLinkVisible { get; }

        public abstract string MaxAvailableQuantity { get; }
        public abstract string SkuOnPdp { get; }

        public List<string> GetListOfFullSystemProductNames
        {
            get
            {
                var fullSystemOptionProductNames = new List<string>();

                foreach (var item in ListOfFullSystemProductNames)
                {
                    fullSystemOptionProductNames.Add(item.Text);
                }

                return fullSystemOptionProductNames;
            }
        }

        public List<int> GetListOfFullSystemQuantities
        {
            get
            {
                var fullSystemOptionQuantities = new List<int>();

                foreach (var item in BuildFullSystemQtyElements)
                {
                    int.TryParse(item.GetAttribute(HtmlTextWriterAttribute.Value.ToString()), out var quantity);

                    fullSystemOptionQuantities.Add(quantity);
                }

                return fullSystemOptionQuantities;
            }
        }

        public List<string> GetListOfFullSystemSkus
        {
            get
            {
                var fullSystemOptionSkus = new List<string>();

                foreach (var item in ListOfFullSystemSkus)
                {
                    fullSystemOptionSkus.Add(item.Text);
                }

                return fullSystemOptionSkus;
            }
        }

        public List<string> GetSkusFromHousingOptionsSection
        {
            get
            {
                var housingOptionShortSkus = new List<string>();

                foreach (var item in HousingOptionsSectionDivContainers) { housingOptionShortSkus.Add(item.GetAttribute("data-shortsku")); }

                return housingOptionShortSkus;
            }
        }

        public Dictionary<string, int> AddAllBuildFullSystemSkusToWishList()
        {
            var buildFullSystemQtyFields = BuildFullSystemQtyElements;
            var buildFullSystemShortSkuLinks = BuildFullSystemShortSkuLinks;
            var qtyCtr = 1;
            var addedProducts = new Dictionary<string, int>();

            for (var i = 0; i < buildFullSystemQtyFields.Count; i++)
            {
                var qtyField = buildFullSystemQtyFields[i];
                var shortSku = buildFullSystemShortSkuLinks[i].Text;
                Browser.ScrollIntoView(qtyField, true);
                qtyField.Clear();
                qtyField.SendKeys(qtyCtr.ToString());
                addedProducts.Add(shortSku, qtyCtr);

                // cycle through qty 1 - 5 for variation
                qtyCtr = qtyCtr == 5 ? 1 : qtyCtr + 1;
            }

            //In IE, it mistakenly clicks the Add to Cart button in the Sticky container instead of the Add to Wishlist button
            //this is because, the Add to Wishlist button is coincidentally covered by the Sticky container as IE driver scrolls the page too much.
            //so the workaround is to hide the Sticky container before clicking the Add to Wishlist button.
            Browser.ExecuteJs("arguments[0].style.display = 'none';", StickyWrapper.InternalElement);

            BuildFullSystemAddToWishListButton.Click();

            return addedProducts;
        }

        public bool HasTurnToReviews => Browser.Locate.DoesElementExistImmediately(ReadReviewsId.ToCssIdSelector());
        public bool IsVirtualAssistantCloseIconVisible => Browser.Locate.ElementImmediately(ConfirmationDialogButtonClass.ToCssClassSelector()).IsInitialized;
        public bool IsChatButtonLinkVisible => Browser.Locate.ElementImmediately(PdChatId.ToCssIdSelector()).IsInitialized;
        public bool IsCheckStoreAvailabilityLinkVisible => Browser.Locate.ElementImmediately(PdCheckStoreAvailabilityId.ToCssIdSelector()).IsInitialized;
        public bool IsCompareCalloutElementVisible => Browser.Locate.ElementImmediately(CompareCalloutClass.ToCssClassSelector()).IsInitialized;
        public bool IsEndDateVerbiageVisible => Browser.Locate.ElementByClassName(EndsDateClass).IsInitialized;
        public bool IsLpModalDisplayed => Browser.Locate.ElementById(GlobalLocators.LpModalId).IsInitialized;
        public bool IsLoggedInAsKiosk => (bool)Browser.ExecuteJs("return window.lp.globals.isKiosk");
        public bool IsMarginLinkVisible => Browser.Locate.ElementImmediately(MarginLinkId.ToCssIdSelector()).IsInitialized;
        public bool IsReplacementPartModalVisible => Browser.Locate.ElementByClassName(ReplacementPartModalClass).IsInitialized;
        public bool IsShopAllLinkVisible => Browser.Locate.ElementByTagName(HtmlTextWriterTag.A, ManufacturerLink).IsInitialized;
        public bool IsQuestionAskStoreAssociateLink => Browser.Locate.ElementImmediately(QuestionAskStoreAssociateLinkId.ToCssIdSelector()).IsInitialized;
        public bool IsProductDetailPage => Browser.PageUrl.Contains($"{Urls.HomePageUrl}{Urls.ProductsUrlDirectory}");
        public bool IsPriceVerbiageVisible => Browser.Locate.ElementByClassName(PriceTypeClass).IsInitialized;
        public bool IsQuickPrintInputVisible => Browser.Locate.ElementImmediately($"{PdQuickPrintClass.ToCssClassSelector()} {HtmlTextWriterTag.Input}").IsInitialized;
        public bool IsQuickPrintLinkVisible => Browser.Locate.ElementImmediately(PdQuickPrintId.ToCssIdSelector()).IsInitialized;
        public bool IsSingleQuestionAndAnswerElementVisible => Browser.Locate.ElementImmediately(Tt3QuestWrpClass.ToCssClassSelector()).IsInitialized;
        public bool IsSaleVerbiageVisible => Browser.Locate.ElementByClassName(SaleVerbiageClass).IsInitialized;
        public bool IsSavePriceAndVerbiageVisible => Browser.Locate.ElementByClassName(PriceAdditionalSaveId).IsInitialized;
        public bool IsStrikeThroughVisible => Browser.Locate.ElementByClassName(StruckThroughClass).IsInitialized;
        public bool IsCheckCompareCallOut => Browser.Locate.ElementBySelector(ComparePriceClass.ToCssClassSelector()).IsInitialized;
        public bool IsCheckEndDateCallOut => Browser.Locate.ElementByClassName(EndsDateClass).IsInitialized;

        public IElement ToOrderCallCalloutOnOrdersOver49PhoneLink => throw new NotImplementedException();

        public IElement ToOrderCallCalloutOnOrdersOver49Phone => throw new NotImplementedException();

        public IElement ToOrderCallCalloutOnOrdersOver49ForAvailabilityText => throw new NotImplementedException();

        string IProductDetail.ForAvailabilityCallout => throw new NotImplementedException();

        public string ForAvailabilityCall => throw new NotImplementedException();

        public void ClickBreadCrumbByIndex(int index) { ListOfBreadCrumbLink()[index].Click(); }

        public void ClickOnLastBreadcrumb()
        {
            var breadsCrumbs = ListOfBreadCrumbLink();

            if (breadsCrumbs.Count > 0)
            {
                breadsCrumbs.Last().Click();
            }
            else
            {
                Browser.Log.Message("Product page doesn't have bread crumbs");
                Browser.TakeScreenshot(InvestigateCaps);
            }
        }

        public void ClickTurnToWriteReviewButtonJs()
        {
            Browser.Wait.ForClickableElement(TurnToWriteReviewButton);
            Browser.ExecuteJs($"document.querySelector('#{TurnToWriteReviewButton.LocatorString}').click();");
        }

        public void CloseVirtualAssistant()
        {
            Browser.Wait.ForDisplayedElement(VirtualAssistantContainer);
            VirtualAssistantCloseButton.Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(ConfirmationDialogClass.ToCssClassSelector()));
            Browser.ExecuteJs($"$('{ConfirmationDialogButtonClass.ToCssClassSelector()}').click()");
            Browser.Wait.UntilElementUnloads(VirtualAssistantContainer);
        }

        public void ForceHideStickyHeader()
        {
            Browser.ExecuteJs("arguments[0].style.display = 'none'", StickyWrapper.InternalElement);
        }

        public string GetProductPrice()
        {
            return TextActions.RemoveTextBeforeAndIncludingCharacter(Price.Text, '$');
        }

        public void NavigateToProductDetailByShortSku(string shortSku)
        {
            var url = $"{Urls.HomePageUrl}{Urls.ProductsUrlDirectory}/{shortSku}";

            Browser.Navigate(url);
        }

        public void NavigateToOpenBoxProductDetailByShortSku(string shortSku)
        {
            Browser.Navigate($"{Urls.OpenBoxProductPageUrl}{shortSku}");
            Browser.Wait.ForDomReady();
        }

        public void PressCloseButtonOnQAndATextarea()
        {
            Browser.Wait.IsVisibleElement(By.Id(TurnTwoAskQuestionCloseButtonId));
            TurnTwoAskAQuestionCloseButton.Click();
            Browser.Wait.ForCondition(() => !TurnTwoAskAQuestionCloseButton.Displayed);
        }
        
        public bool IsWeekday()
        {
            var start = new TimeSpan(04, 00, 00);
            var start1 = new TimeSpan(07, 00, 00);
            var end = new TimeSpan(20, 00, 00);                 
            var nowOrig = DateTime.Now.TimeOfDay;
                
            foreach (string str in Enum.GetNames(typeof(DayOfWeek)))
            {

                if ((DateTime.Now.ToString("dddd") == DayOfWeek.Saturday.ToString()) || (DateTime.Now.ToString("dddd") == DayOfWeek.Sunday.ToString()) && (nowOrig >= start1) && (end > nowOrig))      // if time is between 7am - 8pm from Sat to Sun
                {
                    return true;
                }

                if (DateTime.Now.ToString("dddd") == str && (nowOrig >= start) && (end > nowOrig) && !((DateTime.Now.ToString("dddd") == DayOfWeek.Saturday.ToString()) || (DateTime.Now.ToString("dddd") == DayOfWeek.Sunday.ToString())))   // if time is between 4am -8pm from Mon to Fri
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsCorrectGiftCardAmountSelected(int timeToWait)
        {
            return Browser.Wait.ForCondition(() => GiftCardValue.Contains(TwentyFiveDollarGiftCardString));
        }

        public abstract void ClickPhotoModal(int productReviewCounter, int pixelsScroll, int reviewClassNotFound, int endCondition);

        public void SwitchToModalIframe() { Browser.SwitchFocusToIframe(Browser.Locate.ElementById(GlobalLocators.ModalIframeId)); }

        public void SwitchToTheEmailProductFrame() { Browser.SwitchFocusToIframe(GlobalLocators.IframeModal); }

        public void WaitForPdpToLoad() { Browser.Wait.ForCondition(() => IsProductDetailPage, 5); }

        public abstract Dictionary<string, int> AddAllBuildFullSystemSkusToCart();

        public abstract void AddMaxQuantityToCart();

        public abstract void AddAnswerToQuestion(string sampleText);

        public abstract void ClickTurnToWriteReview(bool isiPhoneTest = false);

        public abstract void CompleteTurnToWriteReview();

        public abstract void FocusOnTurnToQAndA();

        public abstract void ProductCheckStoreAvailabilityLink();

        public abstract void TypeIntoQAndATextarea(string sampleText);

        public abstract bool TimeVerifyCheck(string availabilityCallout1, string availabilityCallout2);

        public abstract bool TimeVerifyCheckMobile(string textChatExpected, string textChatActual, string phoneExpected, string phoneActual, string availabilityTextExpected, string availabilityTextActual);
    }
}
