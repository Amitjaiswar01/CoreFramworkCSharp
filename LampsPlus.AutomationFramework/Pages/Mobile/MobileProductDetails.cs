using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.UI;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using Automation.Framework;
using Automation.Framework.Core;
using Automation.Framework.Exceptions;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Base;


namespace LampsPlus.AutomationFramework.Pages.Mobile
{
    /// <summary>
    /// Example page: https://www.lampsplus.com/products/possini-euro-design-vicina-chrome-led-torchiere-floor-lamp__4g433.html.
    /// </summary>
    public class MobileProductDetail : ProductDetailBase
    {
        public MobileProductDetail(IBrowser browser, IGlobalLocators globalLocators) : base(browser, globalLocators) { }

        #region CSS Selector Strings
        private string CityTitleClass { get; } = "cityTitle";
        private string MaxQuantityXpath { get; } = "//*[@id='pdQtyLimitedDrawer']//li[last()]//label";
        private string ProductSpectionSectionClass { get; } = "pnlProductSpecification__content";
        private string ProductGoodToKnowSectionClass { get; } = "pnlProductDescription__content";
        private string PdStoreAppointmentBrnId { get; } = "pdStoreAppointmentBtn";
        private string ProductSpecificationTablesId { get; } = "pnlProductSpecificationCollapsibleButton";
        private string TextStoreButtonClass { get; } = "textStoreBtn";
        private string QuestionAnswerArrowId { get; } = "productQuestionsAndAnswersCollapsibleButton";

        public override string LoadMoreReviewsBtnClass { get; } = "//*[@id='productReviewsApp']//*[text()='Load More Reviews']";
        public override string AdjacentButtonClass { get; } = "adjacentButton";
        public override string AvailabilityPhoneNumberString { get; } = "888-739-0201";
        public override string AvailabilityString { get; } = "For Availability:";
        public override string AvailabilityTextString { get; } = "For Availability: 888-739-0201";
        public override string BtnPdpZoomClass { get; } = "btnPdpZoom";
        public override string BuildFullSystemContainerId { get; } = "buildFullSystemContainer";
        public override string BuildFullSystemId { get; } = "pdBuildFullSystem";
        public override string CallStoreButtonClass { get; } = "callStoreBtn";
        public override string GiftCardDenominationXpath { get; } = "//*[@id=\"giftCardAmountSelector\"]//span";
        public override string JsCertonaTitleClass { get; } = "jsCertonaTitle";
        public override string JsOtherOptionLinkClass { get; } = "js-other-option-link";
        public override string LimitedQtyFieldClass { get; } = "qtyLimitedField";
        public override string LimitedQuantitySelectionId { get; } = "pdQtyLimitedDrawer";
        public override string LpContainerId { get; } = "applicationNode";
        public override string LpCollapsibleCollapsedHidden { get; } = "lpCollapsible--collapsed_hidden";
        public override string LpMobileAccordionClass { get; } = "lpMobileAccordion";
        public override string MainImagePathXpath { get; } = "//*[@id='pdHeroSpot']/div/div/div[3]/img";
        public override string PdAddToCartBuildFullId { get; } = "pdbuildFullSystemAddToCart";
        public override string PdAddToCartStickyId { get; } = "pdAddToCartSticky";
        public override string PdAddToPortfolioSystemOptionsId { get; } = "pdbuildFullSystemAddToWishList";
        public override string PdpAddToWishlistClass { get; } = "pdpAddToWishlist";
        public override string PdHeroSpotId { get; } = "pdHeroSpot";
        public override string PdRelatedItmsId { get; } = "pdRelatedItms";
        public override string PdRelatedItmsXpath { get; } = "//*[@id='pdRelatedItms']/div[1]";
        public override string PdFirstRelatedItmXpath { get; } = "//*[@id='pdRelItmsContainer']/div[1]/a/img";
        public override string PdReviewsId { get; } = "pdReviews";
        public override string PdpStickyHeaderId { get; } = "pdpStickyHeader";
        public override string PdpStickyHeaderImageWrapperClass { get; } = "pdpStickyHeader__image-wrapper";
        public override string PdViewFullTrackSystemId { get; } = "pdViewFullTrackSystemBtn";
        public override string PnlProductDescriptionId { get; } = "pnlProductDescription";
        public override string PopularColorsId { get; } = "popularColors";
        public override string ProductDescId { get; } = "pnlProductDescription";
        public override string ProductDescSelector { get; } = "//*[@id='pnlProductDescriptionyCollapsibleButton']";
        public override string ProductNameAndNumber { get; } = "productNameAndNumber";
        public override string ProductReviewUrlFragment { get; } = "?readreviews=true";
        public override string ProductReviewsCollapsibleSelector { get; } = "#productReviewsCollapsible.lpCollapsible--collapsed_hidden";
        public override string ProductReviewRatingStarCountClass { get; } = "ProductReviewRatingStars__summaryCount";
        public override string QtyLimitedName { get; } = "qtyLimited";
        public override string QuantityDrawerXpath { get; } = "//div[@aria-hidden='false']//parent::div[@id='pdQtyLimitedDrawer']";
        public override string ShipsInMessageClass { get; } = "shipsInMessage";
        public override string StoreAvailabilityClass { get; } = "storeAvailability";
        public override string StoreAvailabilityQuestionsClass { get; } = "storeAvailabilityQuestions";
        public override string SystemOptionsQtyClass { get; } = "buildFullSystem__qty";
        public override string ThumbnailImageCarouselClass { get; } = "js-pd-carousel-image";
        public override string ThumbnailImageCarouselId { get; } = "pdImageCarousel";//added
        public override string TextAttributeValue { get; } = "text";
        public override string TitleAttributeValue { get; } = "title";
        public override string Tt4QProductImgClass { get; } = "TT4QProductImg";
        public override string TtLeftHeaderClass { get; } = "TTleftHeader";
        public override string TtrespMobileDispInlineClass { get; } = "TTrespMobileDispInline";
        public override string TtOverallRatingStarsId { get; } = "TToverallRatingStars";
        public override string TtWriteReviewBtnPortraitId { get; } = "TTwriteReviewBtn-portrait";
        public override string MoreYouLikeBorderClass { get; } = "moreYouLikeBorder";
        public override string MaxAvailableQuantity => Browser.Locate.ElementsByAttributeEquals(HtmlTextWriterAttribute.Name, QtyLimitedName).Last().GetAttribute(HtmlTextWriterAttribute.Value.ToString());
        public override string SkuOnPdp => Browser.Locate.ElementBySelector(ProductSkuId.ToCssIdSelector()).GetAttribute(ContentString);
        public override string ToOrderCalloutString => Browser.Locate.ElementBySelector($"{PdPleaseCallClass.ToCssClassSelector()} {HtmlTextWriterTag.A}").Text;
        #endregion
        public override string StickyCallOutClass { get; } = "pdpStickyHeader__priceWrapper";
        public override string AddToWishListButtonXpath { get; } = "//*[@id='pdAddToPortfolioNormal']";
        public override string AssetActionsClass { get; } = "assetActions";
        public override string ToOrderCalloutClass { get; } = "pnlPleaseCallCustomerService";
        public override string ToOrderCallCalloutOnOrdersOver49Class { get; } = "pdPleaseCall";
        public override string RelatedItemsContainerXpath { get; } = "//*[@id='pdRelItmsContainer']/div[1]/a";
        public override string RelatedItemSectionXpath { get; } = "//*[@id='pdRelatedItms']/div[1]";
        public override string ReviewsSectionXpath { get; } = "//*[@id='productReviewsCollapsible']/button";
        public override string SearchResultsListClass { get; } = "searchResultsList";
        public override string ProsSpecialPriceCallOutClass { get; } = "tradePriceLabel";
        public override string StoreAvailabilityLocatorContentClass { get; } = "storeAvailabilityLocatorContent";
        public override string ReviewsElementSelector { get; } = "#pdReviews > div.lpmcHeader.lpmcToggleCollapsible";
        public override string ReplacementPartLinkId { get; } = "replacementPartsBtn";
        public override string ReplacementPartSkuXpath { get; } = "//*[@class='heading']/span";
        public override string WishListIndicatorString { get; } = "wish-list-button-text";
        public override string DivProductDetailTop { get; } = "divProductDetail";
        public override string StoreAssociateId { get; } = "pdStoreAppointmentBtn";
        public override string StickyPriceClass { get; } = "pdpStickyHeader__price";
        public override string StickySaleClass { get; } = "pdpStickyHeader__priceType";
        public override string ProductReviewsSectionId { get; } = "productReviewsCollapsibleButton";
        public override string ProductReviewSweepstakeClass { get; } = "productReviews__sweepstakesMobile";
        public override string ProductAttributesClass { get; } = "productAttributes";
        public override string ProductTechnicalSpecificationsClass { get; } = "technicalSpecifications";
        public override string LblStickyPriceClass { get; } = "pdpStickyHeader__price--regular";
        public override string MobileStruckPriceSfpXpath { get; } = "//strike[1]";
        public override string ReplacementPartModalClass { get; } = "add-replacementParts__replacementPartsModal";
        public override string TurnToQuestionAndAnswerSection { get; } = "jsProductQuestionsAndAnswersContainer";
        public override string WriteReviewBtnXpath { get; } = "productReviews__writeReviewBtn";
        public override string GoodtoKnowIconClass { get; } = "eachItem";
        public override string ShowInRoomBtnId { get; } = "showInRoomBtn";
        public override string PdpArIframeXpath { get; } = "//iframe[@aria-hidden='true']";
        public override string ArViewerBtnClass { get; } = "arKit__btn";
        public override string GetStartedBtnClass { get; } = "arKit__getStarted";
        public override string ProductHeroThumbnailClass { get; } = "ProductHeroThumbnail--active";
        public override string CustomerPhotosThumbnailClass { get; } = "CustomerThumbnail--active";
        public override string ProductHeroImageClass { get; } = "react-transform-element";
        public override string ProductCustomerPhotosClass { get; } = "CustomerHeroImage";
        public override string FsImageContainerId => throw new NotImplementedException();
        public override string PdFanFeatures => throw new NotImplementedException();
        public override string PdHeroImageId => throw new NotImplementedException();
        public override string LblStickyPriceId => throw new NotImplementedException();
        public override string LpModalContentId => throw new NotImplementedException();
        public override string ModalProductImageThumbnailXpath => throw new NotImplementedException();
        public override string PdProdImgStickyId => throw new NotImplementedException();
        public override string PdProdTitleStickyClass => throw new NotImplementedException();
        public override string ProductDetailsSectionId => throw new NotImplementedException();
        public override string PdProdImgClass => throw new NotImplementedException();
        public override string QandAId => throw new NotImplementedException();
        public override string QtyNormalId => throw new NotImplementedException();
        public override string SlickActiveClass => throw new NotImplementedException();
        public override string StickyWrapperId => throw new NotImplementedException();
        public override string SlickListClass => throw new NotImplementedException();
        public override string SelectStoreClass => throw new NotImplementedException();
        public override string StoreAssociateModalClass => throw new NotImplementedException();
        public override string MediaModalContentModalClass => throw new NotImplementedException();
        public override string AppCheckStoreAvailabilityClass => throw new NotImplementedException();
        public override string pdImgContainerId => throw new NotImplementedException();
        public override string SkuOnPdpXpath => throw new NotImplementedException();
        public override string StockCheckXpath => throw new NotImplementedException();
        public override string StickySaveXpath => throw new NotImplementedException();
        public override string EndVerbiageOnSfpStickyXpath => throw new NotImplementedException();
        public override string StickyContainerSfpClass => throw new NotImplementedException();
        public override string ViewInYourRoomSampleImageXpath => throw new NotImplementedException();
        public override string ViewInYourRoomSelectPhotoXpath => throw new NotImplementedException();
        public override string ViewInYourRoomXpath => throw new NotImplementedException();
        public override string WriteReviewBtnSelector => throw new NotImplementedException();
        public override string WriteReviewModalXpath => throw new NotImplementedException();
        public override string WriteReviewModalSelector => throw new NotImplementedException();
        public override string BreadCrumbXpath => throw new NotImplementedException();
        public override string ShopAllColorText => throw new NotImplementedException();
        public override string EmailRecipientListClass => throw new NotImplementedException();
        public override string DesignChatClass => throw new NotImplementedException();
        public override string ProductHelpId => throw new NotImplementedException();
        public override string NeedHelpChatClass => throw new NotImplementedException();
        public override string CloseNeedHelpClass => throw new NotImplementedException();
        #region Page Elements

        public override IElement StickySaleCallout => Browser.Locate.ElementByClassName(StickySaleClass);
        public override IElement StickyPriceCallout => Browser.Locate.ElementByClassName(StickyPriceClass);
        public override IElement ProductReviewsSection => Browser.Locate.ElementById(ProductReviewsSectionId);
        public override IElement ProductReviewSweepstake => Browser.Locate.ElementById(ProductReviewSweepstakeClass);
        public override IElement AddToWishListButton => Browser.Locate.ElementByXpath(AddToWishListButtonXpath);
        public override IElement AskStoreAssociate => Browser.Locate.ElementById(PdStoreAppointmentBrnId);
        public override IElement RelatedItemsContainer => Browser.Locate.ElementByXpath(RelatedItemsContainerXpath);
        public override IElement BoldChatButtonContainer => Browser.Locate.ElementByClassName(BoldChatButtonContainerClass, SocialLinksContainer);
        public override IElement BuildFullSystemProductContainer => Browser.Locate.ElementById(BuildFullSystemContainerId);
        public override IElement CarouselImage(int index) => Browser.Locate.ElementBySelector($"{ThumbnailImageCarouselId.ToCssIdSelector()} > {HtmlTextWriterTag.Div.ToNthChildSelector(index)} > {HtmlTextWriterTag.Div}");
        public override IElement CertonaDrawerName => Browser.Locate.ElementByClassName(JsCertonaTitleClass);
        public override IElement CheckStoreCallButton => Browser.Locate.ElementByXpath(CheckStoreCallButtonXpath);
        public override IElement CheckStoreChooseAnotherStateOrZip => Browser.Locate.ElementByXpath(CheckStoreChooseAnotherStateOrZipXpath);
        public override IElement CheckStoreSearchArrow => Browser.Locate.ElementByClassName(CheckStoreAvailabilitySearchButtonClass);
        public override IElement GetYourPhotoFrame => Browser.Locate.ElementBySelector($"{ShowInRoomId.ToCssIdSelector()} {HtmlTextWriterTag.Div.ToDirectChildSelector()} {HtmlTextWriterTag.Iframe.ToDirectChildSelector()}");
        public override IElement LimitedQtyField => Browser.Locate.ElementBySelector(LimitedQtyFieldClass.ToCssClassSelector());
        public override IElement LimitedQuantitySelection => Browser.Locate.ElementBySelector(LimitedQuantitySelectionId.ToCssIdSelector());
        public override IElement MainImagePath(int index) => Browser.Locate.ElementBySelector($"{PdHeroSpotId.ToCssIdSelector()} {HtmlTextWriterTag.Div} > {HtmlTextWriterTag.Div.ToNthChildSelector(index)}");
        public override IElement MobileAddToCartButtonContainer => Browser.Locate.ElementByClassName(AssetActionsClass);
        public override IElement MobileAccordionContainer => Browser.Locate.ElementByClassName(LpMobileAccordionClass);
        public override IElement MobileGiftCardDenomination => Browser.Locate.ElementByXpath(GiftCardDenominationXpath);
        public override IElement MobileMaxQuantity => Browser.Locate.ElementByXpath(MaxQuantityXpath);
        public override IElement PdHeroSpot => Browser.Locate.ElementById(PdHeroSpotId);
        public override IElement PdpAddToWishlist => Browser.Locate.ElementByClassName(PdpAddToWishlistClass);
        public override IElement PopularColorsDropdown => Browser.Locate.ElementById(PopularColorsId);
        public override IElement PdProdSpecificationsTables => Browser.Locate.ElementById(ProductSpecificationTablesId);
        public override IElement PdReviewsElement => Browser.Locate.ElementBySelector(ReviewsElementSelector);
        public override IElement ProductCallOut => Browser.Locate.ElementsByTagName(HtmlTextWriterTag.Span, ItemPrice, true).LastOrDefault();
        public override IElement ProductDescDropDown => Browser.Locate.ElementByXpath(ProductDescSelector);
        public override IElement ProductDescriptionAccordion => Browser.Locate.ElementByXpath("//*[@id=\"pnlProductDescription\"]/div[1]");
        public override IElement ProductDetailPageContainer => Browser.Locate.ElementById(LpContainerId);
        public override IElement ProductImage => Browser.Locate.ElementByTagName(HtmlTextWriterTag.Img, Browser.Locate.ElementByClassName(ActiveClass, PdHeroSpot));
        public override IElement ProsSpecialPriceCallout => Browser.Locate.ElementByClassName(ProsSpecialPriceCallOutClass);
        public override IElement ProductDetailSpecificationSection => Browser.Locate.ElementByClassName(ProductSpectionSectionClass);
        public override IElement ProductGoodToKnowSection => Browser.Locate.ElementByClassName(ProductGoodToKnowSectionClass);
        public override IElement RelatedItemDropdown => Browser.Locate.ElementByXpath(PdRelatedItmsXpath);
        public override IElement ReviewsSection => Browser.Locate.ElementByXpath(ReviewsSectionXpath);
        public override IElement SearchResultsList => Browser.Locate.ElementByClassName(SearchResultsListClass);
        public override IElement SearchZipTextBox => Browser.Locate.ElementByClassName(AdjacentButtonClass);
        public override IElement SocialLinksContainer => Browser.Locate.ElementByClassName(SocialLinksClass);
        public override IElement StickyAddToCart => Browser.Locate.ElementById(PdAddToCartStickyId);
        public override IElement ProductReviewCard(int index) => Browser.Locate.ElementsByClassName(ProductReviewCardClass)[index];


        public override IElement StickyImage => Browser.Locate.ElementByTagName(HtmlTextWriterTag.Img, StickyImageWrapper, true);
        public override IElement StickyImageWrapper => Browser.Locate.ElementByClassName(PdpStickyHeaderImageWrapperClass);
        public override IElement StickyWrapper => Browser.Locate.ElementById(PdpStickyHeaderId);
        public override IElement StockCheckTitleElement => throw new NotImplementedException();
        public override IElement StockCheckElement => Browser.Locate.ElementBySelector(ShipsInMessageClass.ToCssClassSelector());
        public override IElement StockCheckWrapper => Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.Div, ShipsInMessageClass);
        public override IElement StoreAvailability => Browser.Locate.ElementByClassName(StoreAvailabilityClass);
        public override IElement StoreAvailabilityQuestions => Browser.Locate.ElementByClassName(StoreAvailabilityQuestionsClass);
        public override IElement StoreAvailabilityLocatorContent => Browser.Locate.ElementByClassName(StoreAvailabilityLocatorContentClass);
        public override IElement ProductQuestionAnswerArrow => Browser.Locate.ElementById(QuestionAnswerArrowId);
        public override IElement TextStoreButton => Browser.Locate.ElementByClassName(TextStoreButtonClass);
        public override IElement CallStoreButton => Browser.Locate.ElementByClassName(CallStoreButtonClass);
        public override IElement CityTitle => Browser.Locate.ElementByClassName(CityTitleClass);
        public override IElement TopContentProductDetail => Browser.Locate.ElementById(DivProductDetailTop);
        public override IElement ThumbnailCarouselImage => Browser.Locate.ElementById(ThumbnailImageCarouselId);
        public override IElement TurnToReviewProductImage => Browser.Locate.ElementByClassName(Tt4QProductImgClass);
        public override IElement TurnToDynamicAddAnswerButton => Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.Span, TtrespMobileDispInlineClass);
        public override IElement TurnToDynamicAddQuestionsCancelButton => Browser.Locate.ElementByTagNameAndAttributeStartsWith(HtmlTextWriterTag.A, HtmlTextWriterAttribute.Id, Tt2CancelBtnId);
        public override IElement TurnToDynamicAddAnswerTextArea => Browser.Locate.ElementByTagNameAndAttributeStartsWith(HtmlTextWriterTag.Textarea, HtmlTextWriterAttribute.Id, Tt2InlineAnswerId);
        public override IElement TurnToReviewProductName => Browser.Locate.ElementByClassName(TtmediaSmallDescClass);
        public override IElement TurnToReviewRating => Browser.Locate.ElementBySelector($"{TtOverallRatingStarsId.ToCssIdSelector()} {HtmlTextWriterTag.Li.ToNthChildSelector(5)}");
        public override IElement TurnToReviewText => Browser.Locate.ElementByName(TextAttributeValue);
        public override IElement TurnToReviewTitle => Browser.Locate.ElementByName(TitleAttributeValue);
        public override IElement TurnToReviewWindow => Browser.Locate.ElementByClassName(TtLeftHeaderClass).FindElement(By.TagName("a"));
        public override IElement TurnToWriteReviewButton => Browser.Locate.ElementBySelector(TtWriteReviewBtnPortraitId.ToCssIdSelector());
        public override IElement TurnTwoBrowseQaWrapper => Browser.Locate.ElementBySelector($"{PdQandAId.ToCssIdSelector()} > {HtmlTextWriterTag.Div}");
        public override IElement WishListIndicatorIcon => Browser.Locate.ElementByClassName(WishListIndicatorString);
        public override IElement ZipInputFieldCheckStore => Browser.Locate.ElementByClassName(ZipInputFieldCheckStoreClass);
        public override IElement ZoomIcon => Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.Button, BtnPdpZoomClass);
        public override IElement ActiveMainProductImage => throw new NotImplementedException();
        public override IElement BottomPortionOfPdp => throw new NotImplementedException();
        public override IElement InStockElement => throw new NotImplementedException();
        public override IElement BuildFullSystemAddToCartButton => Browser.Locate.ElementById(PdAddToCartBuildFullId);
        public override IElement BuildFullSystemButton => Browser.Locate.ElementById(PdViewFullTrackSystemId);
        public override IElement BuildFullSystemAddToWishListButton => Browser.Locate.ElementById(PdAddToPortfolioSystemOptionsId);
        public override IElement BuildFullSystemContainer => Browser.Locate.ElementById(BuildFullSystemId);
        public override IElement PriceType => Browser.Locate.ElementById(LblPriceId);
        public override IElement ProductAttributes => Browser.Locate.ElementByClassName(ProductAttributesClass);
        public override IElement ProductSpecificationsTables => Browser.Locate.ElementByClassName(ProductTechnicalSpecificationsClass);
        public override IElement StickyPrice => Browser.Locate.ElementByClassName(LblStickyPriceClass);
        public override IElement MobileStruckPriceSfp => Browser.Locate.ElementByXpath(MobileStruckPriceSfpXpath);
        public override IElement WriteReviewBtn => Browser.Locate.ElementByClassName(WriteReviewBtnXpath);
        public override IElement TurnToQuestionsAndAnswersSection => Browser.Locate.ElementByClassName(TurnToQuestionAndAnswerSection);
        public override IElement LoadMoreReviews => Browser.Locate.ElementByXpath(LoadMoreReviewsBtnClass);
        public override IElement SoldOutLabel => Browser.Locate.ElementByXpath("//div[contains(@class,'soldOutLabel')]");
        public override IElement ShowInRoomBtn => Browser.Locate.ElementById(ShowInRoomBtnId);
        public override IElement PdpArIframe => Browser.Locate.ElementByXpath(PdpArIframeXpath);
        public override IElement ArViewerBtn(int index) => Browser.Locate.ElementsBySelector(".arKit__btn")[index];
        public override IElement GetStartedBtn => Browser.Locate.ElementByClassName(GetStartedBtnClass);
        public override IElement ProductHeroThumbnail => Browser.Locate.ElementByTagName(HtmlTextWriterTag.Img, Browser.Locate.ElementByClassName(ProductHeroThumbnailClass));
        public override IElement CustomerPhotosThumbnail => Browser.Locate.ElementByTagName(HtmlTextWriterTag.Img, Browser.Locate.ElementByClassName(CustomerPhotosThumbnailClass));
        public override IElement ProductHeroImage => Browser.Locate.ElementByTagName(HtmlTextWriterTag.Img, Browser.Locate.ElementByClassName(ProductHeroImageClass));
        public override IElement ProductCustomerPhotos => Browser.Locate.ElementByTagName(HtmlTextWriterTag.Img, Browser.Locate.ElementByClassName(ProductCustomerPhotosClass));
        public override IElement MoreImages(int index) => Browser.Locate.ElementsByClassName(ThumbnailsCarouselClass)[index];
        public override IElement CustomerPhotos => Browser.Locate.ElementByXpath(CustomerPhotosXpath);
        public override IElement StickyContainerSfp => throw new NotImplementedException();
        public override IElement GetAllColorPlusElement => throw new NotImplementedException();
        public override IElement BuildFullSystemOptions => throw new NotImplementedException();
        public override IElement ChatButtonLink => throw new NotImplementedException();
        public override IElement CheckAvailabilityModal => throw new NotImplementedException();
        public override IElement CheckAvailabilityStoreList => throw new NotImplementedException();
        public override IElement CheckStoreFirstName => throw new NotImplementedException();
        public override IElement CheckStorePhone => throw new NotImplementedException();
        public override IElement CheckStoreQuestion => throw new NotImplementedException();
        public override IElement CheckStoreReserveItemButton => throw new NotImplementedException();
        public override IElement CheckStoreReserveItemButtonBottom => throw new NotImplementedException();
        public override IElement CheckStoreSearchButton => throw new NotImplementedException();
        public override IElement CsInfo => throw new NotImplementedException();
        public override IElement EmailLink => throw new NotImplementedException();
        public override IElement EmailModalContent => throw new NotImplementedException();
        public override IElement EmailRecipientTextbox => throw new NotImplementedException();
        public override IElement EmailRecipientList => throw new NotImplementedException();
        public override IElement FanFeatures => throw new NotImplementedException();
        public override IElement FirstNameTextbox => throw new NotImplementedException();
        public override IElement FooterChatLink => throw new NotImplementedException();
        public override IElement FreeShippingToStatesWithStoresLabel => throw new NotImplementedException();
        public override IElement FromEmailTextbox => throw new NotImplementedException();
        public override IElement HeaderChatLink => throw new NotImplementedException();
        public override IElement HousingOptions => throw new NotImplementedException();
        public override IElement HousingOptionsSectionHeader => throw new NotImplementedException();
        public override IElement ImageContainer => throw new NotImplementedException();
        public override IElement LastNameTextbox => throw new NotImplementedException();
        public override IElement LblStockInventory => throw new NotImplementedException();
        public override IElement LongSkuElement => throw new NotImplementedException();
        public override IElement MainProductImage => throw new NotImplementedException();
        public override IElement MarginModalLink => throw new NotImplementedException(); 
        public override IElement ModalProductImageThumbnail => throw new NotImplementedException();
        public override IElement PdChat => throw new NotImplementedException();
        public override IElement PdImageColumn => throw new NotImplementedException();
        public override IElement PdProdInfoColElement => throw new NotImplementedException();
        public override IElement PdSocialIconElement => throw new NotImplementedException();
        public override IElement PdSocialPrintIconElement => throw new NotImplementedException();
        public override IElement PrintKioskStyleButtonElement => throw new NotImplementedException();
        public override IElement PrintKioskStyleProductBtnElement => throw new NotImplementedException();
        public override IElement ProductDetailSection => throw new NotImplementedException();
        public override IElement pdImgContainer => throw new NotImplementedException();
        public override IElement ProductInStockTextLink => throw new NotImplementedException();
        public override IElement ProductImageThumbnail => throw new NotImplementedException();
        public override IElement ProductSlider => throw new NotImplementedException();
        public override IElement QuestionsAndAnswersChatContainer => throw new NotImplementedException();
        public override IElement QuestionsAndAnswersChatLink => throw new NotImplementedException();
        public override IElement QuickPrintInput => throw new NotImplementedException();
        public override IElement QuickPrintLink => throw new NotImplementedException();
        public override IElement RelatedItemsSection => throw new NotImplementedException();
        public override IElement SamplePhotosTab => throw new NotImplementedException();
        public override IElement SampleRoomBtn => throw new NotImplementedException();
        public override IElement SendEmailButton => throw new NotImplementedException();
        public override IElement StickyTitle => throw new NotImplementedException();
        public override IElement StoreInventoryElement => throw new NotImplementedException();
        public override IElement SelectedThumbnailWrapper => throw new NotImplementedException();
        public override IElement SelectedDifferentThumbnailWrapper => throw new NotImplementedException();
        public override IElement ThumbnailWrapper => throw new NotImplementedException();
        public override IElement ToOrderCalloutOnPdp => throw new NotImplementedException();
        public override IElement TurnToQuestionAndAnswerContainer => throw new NotImplementedException();
        public override IElement TurnToReviewAddNewPhotoButton => throw new NotImplementedException();
        public override IElement TurnToReviewAttachPhoto => throw new NotImplementedException();
        public override IElement TurnToReviewFileInput => throw new NotImplementedException();
        public override IElement TurnToReviewFileMediaListSelected => throw new NotImplementedException();
        public override IElement TurnToReviewMediaSubmitButton => throw new NotImplementedException();
        public override IElement TurnToReviewScreen => throw new NotImplementedException();
        public override IElement TurnToReviewModal => throw new NotImplementedException();
        public override IElement TurnToReviewShareMediaScreen => throw new NotImplementedException();
        public override IElement ZipcodeTextbox => throw new NotImplementedException();
        public override IElement ProductReviewModal => throw new NotImplementedException();
        public override IElement QuestionsAndAnswersCommentsSection => throw new NotImplementedException();
        public override IElement StickySaveCallout => throw new NotImplementedException();
        public override IElement DesignChatLink => throw new NotImplementedException();
        public override IElement ProductHelp => throw new NotImplementedException();
        public override IElement NeedHelpChat => throw new NotImplementedException();
        public override IElement CloseNeedHelp => throw new NotImplementedException();
        public override IElement EndsDate(int index) => throw new NoSuchElementException();
        public override bool IsReplacementPartLinkVisible => throw new NotImplementedException();

        public override ReadOnlyCollection<IElement> BuildFullSystemQtyElements => Browser.Locate.ElementsByClassName(SystemOptionsQtyClass, BuildFullSystemContainer);

        public override ReadOnlyCollection<IElement> BuildFullSystemShortSkuLinks => Browser.Locate.ElementsBySelector($"{HtmlTextWriterTag.Ul} {HtmlTextWriterTag.Li}", BuildFullSystemProductContainer);
        public override ReadOnlyCollection<IElement> MoreThumbnailImage => Browser.Locate.ElementsByClassName(ActiveCarouselClass, Browser.Locate.ElementByClassName(ThumbnailsCarouselClass));
        public override ReadOnlyCollection<IElement> HousingOptionsSectionDivContainers => throw new NotImplementedException();
        public override ReadOnlyCollection<IElement> ListOfFullSystemProductNames => throw new NotImplementedException();
        public override ReadOnlyCollection<IElement> ListOfFullSystemSkus => throw new NotImplementedException();
        public override ReadOnlyCollection<IElement> ProductSliders => throw new NotImplementedException();
        public override ReadOnlyCollection<IElement> SamplePhotos => throw new NotImplementedException();
        public override ReadOnlyCollection<IElement> GoodToKnowIcon => Browser.Locate.ElementsByClassName(GoodtoKnowIconClass);
        public override ReadOnlyCollection<IElement> ThumbnailImageCarousel => Browser.Locate.ElementsByClassName(ThumbnailImageCarouselClass);
        #endregion

        public override (string textMessage, string phoneNumber) GetTxtMessageAndPhoneNumber()
        {
            var defaultWaitTime = 70;
            var actualMessage = String.Empty;
            var actualPhoneNumber = String.Empty;

            if (Browser.Device != null)
            {
                if (Browser.Device.IsIphone)
                {

                    try
                    {
                        ((IphoneBrowser)Browser).SwitchToNativeContext((AppiumDriver<AppiumWebElement>)Browser.Driver);
                        Browser.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(defaultWaitTime / 10); //reduced implicit wait

                        var messagesModalLocator = "//XCUIElementTypeButton[@name='OK']";

                        //Accept Messages app settings
                        Browser.Wait.IsVisibleElement(By.XPath(messagesModalLocator), -(defaultWaitTime - defaultWaitTime / 10)); //reduced explicit wait 
                        Browser.Driver.FindElement(By.XPath(messagesModalLocator)).Click();
                    }
                    catch
                    {
                        Log.Message("Messages Modal alert is not shown");
                    }
                    finally
                    {
                        //get actual data on iOS Messages modal 
                        actualMessage = Browser.Locate.ElementByXpath("//XCUIElementTypeTextField[@name='messageBodyField']", nativeContext: true)
                            .Text.Trim().Replace("  ", " ");

                        actualPhoneNumber = Browser.Locate.ElementByXpath("//XCUIElementTypeTextField[@name='To:']", nativeContext: true).Text;
                        actualPhoneNumber = actualPhoneNumber.TrimEnd().Substring(actualPhoneNumber.Length - 11); //to get phone number only

                        Log.Message($"Actual iOS SMS message is: {actualMessage}");
                        Log.Message($"Actual iOS store phone number is: {actualPhoneNumber}");

                        Browser.CloseApp("com.apple.MobileSMS");

                    }
                }
            }

            return (actualMessage, actualPhoneNumber);
        }

        public override bool IsQuantityLeftShows => Browser.Locate.ElementById(AvailInventoryId).Displayed;

        public override void AddMaxQuantityToCart()
		{
			Browser.Wait.IsVisibleElement(By.CssSelector(LimitedQtyFieldClass.ToCssClassSelector()));
            Browser.Wait.ForDomReady();

            LimitedQtyField.Click();

            Browser.Wait.ForDomReady();
            Browser.SwitchToCurrentWindow();

            Browser.Wait.IsVisibleElement(By.XPath(QuantityDrawerXpath));
            Browser.ExecuteJs("arguments[0].click()", MobileMaxQuantity.InternalElement);

		    Browser.Wait.ForElementToStopAnimating(MobileMaxQuantity);

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
                var shortSku = buildFullSystemShortSkuLinks[i].GetAttribute("data-shortsku");

                Browser.ScrollIntoView(qtyField,true);
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
            Browser.Wait.IsVisibleElement(By.CssSelector(PdReviewsId.ToCssIdSelector()));
            Browser.ScrollToElement(PdReviewsElement);
            PdReviewsElement.Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(TtWriteReviewBtnPortraitId.ToCssIdSelector()));
            Browser.ScrollToElement(TurnToWriteReviewButton);
            if (isiPhoneTest)
            {
                var xElementCoordinate = 0;
                var yElementCoordinate = 0;
                Browser.GetElementCoordinates(TurnToWriteReviewButton, ref xElementCoordinate, ref yElementCoordinate, 110);
                Browser.ClickWithTapByCoordinates(xElementCoordinate, yElementCoordinate);
                Browser.Wait.ForDomReady();
            }
            else
                TurnToWriteReviewButton.Click();

            Browser.SwitchToTabByIndex(1, true);
        }


        public override void CompleteTurnToWriteReview()
        {
            // Close the review
            TurnToReviewWindow.Click();
            Browser.SwitchToTabByIndex(0);
        }

        public override void FocusOnTurnToQAndA()
        {
            Browser.Wait.ForElement(TurnTwoBrowseQaWrapper);
            Browser.ScrollIntoView(GlobalLocators.PdpDrawerElement);
            Browser.Wait.IsVisibleElement(By.Id(PdQandAId));
            TurnTwoBrowseQaWrapper.Click();
            Browser.Wait.IsVisibleElement(By.Id(Tt2QuestionTextId));
            Browser.ScrollToElement(TurnTwoAskAQuestionTextArea);
        }
        
        public override void ProductCheckStoreAvailabilityLink()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(StoreAvailabilityClass));
            StoreAvailability.Click();
            Browser.Wait.ForDomReady();
        }

        public override void TypeIntoQAndATextarea(string sampleText)
        {
            Browser.ScrollIntoView(GlobalLocators.PdpDrawerElement);
            TurnTwoAskAQuestionTextArea.Clear();
            TurnTwoAskAQuestionTextArea.Click();
            TurnTwoAskAQuestionTextArea.SendKeys(sampleText);
        }

        public override void AddAnswerToQuestion(string sampleText)
        {
            Browser.ScrollIntoView(GlobalLocators.PdpDrawerElement);
            TurnToDynamicAddAnswerButton.Click();
            Browser.Wait.ForClickableElement(TurnToDynamicAddQuestionsCancelButton);
            TurnToDynamicAddAnswerTextArea.SendKeys(sampleText);
        }

        public override bool TimeVerifyCheck(string availabilityCallout1, string availabilityCallout2)
        {
            throw new NotImplementedException();
        }

        public override bool TimeVerifyCheckMobile(string textChatExpected, string textChatActual, string phoneExpected, string phoneActual, string availabilityTextExpected, string availabilityTextActual)
        {
            var start = new TimeSpan(07, 00, 00);
            var end1 = new TimeSpan(20, 00, 00);
            var timeNow = DateTime.Now.TimeOfDay;
            var start2 = new TimeSpan(04, 00, 00);
            var end2 = new TimeSpan(20, 00, 00);
            var thisDay = DateTime.Today.DayOfWeek.ToString();
            var condition = false;

            timeNow = timeNow.Seconds > 45 ? new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute + 1, 00) : new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 00);

            if ((thisDay == "Saturday") || (thisDay == "Sunday"))
            {
                if (DateTimeHelper.IsTimeInBetween(start, end1, timeNow))
                {
                    if (string.Equals(textChatExpected, textChatActual, StringComparison.OrdinalIgnoreCase) && string.Equals(phoneExpected, phoneActual, StringComparison.OrdinalIgnoreCase))
                    {
                        condition = true;
                    }
                }
                else
                {
                    if (string.Equals(availabilityTextExpected, availabilityTextActual, StringComparison.OrdinalIgnoreCase))
                    {
                        condition = true;
                    }
                }
            }
            else
            {
                if (DateTimeHelper.IsTimeInBetween(start2, end2, timeNow))
                {
                    if (string.Equals(textChatExpected, textChatActual, StringComparison.OrdinalIgnoreCase) && string.Equals(phoneExpected, phoneActual, StringComparison.OrdinalIgnoreCase))
                    {
                        condition = true;
                    }
                }
                else
                {
                    if (string.Equals(availabilityTextExpected, availabilityTextActual, StringComparison.OrdinalIgnoreCase))
                    {
                        condition = true;
                    }
                }
            }

            return condition;
        }

        public override void ClickPhotoModal(int productReviewCounter, int pixelsScroll, int reviewClassNotFound, int endCondition)
        {
            do
            {
                Browser.ScrollToTopOfWindow();
                Browser.ScrollToByPixelsVertical(pixelsScroll.ToString());

                if (Browser.Wait.ForDisplayedElement(WriteReviewBtn).IsInitialized)
                {
                    Browser.Wait.ForClickableElement(ProductReviewCard(0));

                    Browser.ScrollIntoView(ProductReviewsSection);

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
