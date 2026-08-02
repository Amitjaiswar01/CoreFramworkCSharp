using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.UI;
using Applitools.Utils;
using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Exceptions;
using Automation.Framework.Utilities;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Databases.Actions;
using LampsPlus.AutomationFramework.Pages.Refactored.Modal;
using LampsPlus.AutomationFramework.Utilities;
using OperatingSystem = LampsPlus.AutomationFramework.Utilities.TestConfiguration.OperatingSystem;
using Page = Automation.Framework.Core.Page;

namespace LampsPlus.AutomationFramework.Pages.Refactored.ProductDetail
{
    public class ProductDetailDesktop : IProductDetailDesktop
    {
        //Class members
        private string _qtyNormalInputId = "QtyNormal";
        private string _qtyMultiProdId = "QtyMultiProd";
        private string _pdAddToCartId = "pdAddToCart";
        private string _lblPriceId = "lblPrice";
        private string _lblFreeShippingId = "lblFreeShipping";
        private string _lblStockInventoryId = "lblStockInventory";
        private string _addToCartMultiproductId = "AddToCart_Multiproduct";
        private string _divBreadCrumbId = "divBreadCrumb";
        private string _stockCheckXpath = "//div[@class='stockCheck']";
        private string _viewInYourRoomId = "showInRoomBtn";
        private string _turnToQuestionsAndAnswersSectionId = "turntoQuestionsAndAnswersSection";
        private string _sampleRoomClass = "sampleRoomBtn";
        private string _addActiveRoomCss = "button.addActiveRoom";
        private string _arIframeId = "modalIframe";
        private string _arSampleImageXpath = "(//span[@class='image '])[1]";
        private string _printModalFrameId = "modalIframe";
        private string _pdInStockId = "pdInStock";
        private string _printIconClass = "lpIcon-print";
        private string _printKioskStyleId = "printKioskStyleProductBtn";
        private string _printKioskRoomSceneId = "printKioskStyleButton";
        private string _productCompleteTheLookId = "productCompleteTheLook";
        private string _addToCartXpath = "//*[@id='pdAddToCart']";
        private string _ttQnASearchBarId = "searchQuery_questionsAndAnswers";
        private string _ttProductSearchResultsClass  = "ProductSearchUGCResults";
        private string _ttProductSearchResultsCardClass = "ProductSearchUGCResultsCard";
        private string _writeReviewBtnSelector  = ".productReviews__writeReviewBtn";
        private string _writeReviewModalSelector  = ".tt-o-modal__container";
        private string _mediaModalContentModalClass = "OverlayContent";
        private string _lpHeaderWrapperId = "lpHeader-wrapper";
        private string _availInventoryId = "availInventory";
        private string _pdAddToCartSystemOptionsId = "pdAddToCartSystemOptions";
        private string _pdProdTitleStickyClass = "pdProdTitleSticky";
        private string _pdNeedHelpLinkId = "pdNeedHelpLink";
        private string _needHelpModalCloseButtonClass = "Overlay__contentWrapper__closeButton";
        private string _appNeedHelpWithProductClass = "AppNeedHelpWithProduct";
        private string _lblStickyPriceId  = "lblStickyPrice";
        private string _h1ProductNameId = "h1ProductName";
        private string _needHelpSupportInfoChatClass = "needHelpSupportInfoChat";
        private string _thumbnailWrapperClass = "thumbnailWrapper";
        private string _productImageThumbnailId = "pdAddlImgs";
        private string _modalProductImageThumbnailXpath = "//*[@id=\"fsNav\"]//*[@data-slick-index=\"1\"]";
        private string _makeAnAppoinmentBtnClass = "makeAnAppoinmentBtn";
        private string _customerPhotosClass = "imageTab--customer";
        private string _pnlPleaseCallCustomerServiceId = "pnlPleaseCallCustomerService";
        private string _pdMoreYouMayLikeId = "pdMoreYouMayLike";
        private string _pdReplacementPartModalId = "pdReplacementPartModal";
        private string _overlayContentWrapperClass = "Overlay__contentWrapper";
        private string _specificationSectionClass = "specificationSection";
        private string _pdSummaryTeaserStarsLinkId = "pdSummaryTeaserStarsLink";
        private string _imageTabContentClass = "imageTabContent";
        private string _openBoxTagClass = "openBoxTag";
        private string _buyItNewContainerClass = "openBoxBuyItNewContainer";
        private string _openBoxLinkId = "openBoxPdpLink";
        private string _pdFanFeaturesId = "pdFanFeatures";
        private string _productDetailsSectionId = "productDetailsSection";
        private string _energyInfoModalId = "jsEnergyInfoModalData";
        private string _recentlyViewedViewAllBtnClass = "viewAllRecentlyViewedBtn";
        private string _browseXpath = "//*[@id='browse']";
        private string _showInRoomBtnXpath = "//*[@id='showInRoomBtn']";
        private string _sidebarTopButtonXpath = "//*[@class='sidebarTop__right']//button[1]";
        private string _fsZoomOptionsId = "fsZoomOptions";
        private string _wishListButtonTextClass = "wish-list-button-text";
        private string _giftCardFirstNameId = "giftCardFirstName";
        private string _giftCardLastNameId = "giftCardLastName";
        private string _giftCardMessageId = "giftCardMessage";
        private string _pnlProductPriceStrikeXpath = "//*[@id='pnlProductPrice']//strike";
        private string _priceAdditionalSaveId = "priceAdditionalSave";
        private string _endsDateClass = "endsDate";
        private string _priceTypeUpperCaseSelector = ".priceType .upperCase";
        private string _stickyPriceContainerSelector = ".stickyPriceContainer__priceTop .upperCase";
        private string _newProductPriceSelector = ".newProductPrice > strike";
        private string _stickyPriceContainerSaveXpath = "//*[@class='stickyPriceContainer']//li[2]";
        private string _stickyPriceContainerEndsXpath = "//*[@class='stickyPriceContainer']//li[3]";
        private string _stickyPriceContainerSaleXpath = "//*[@class='stickyPriceContainer']//*[contains(text(),'Sale')]";
        private string ItemPriceText => Browser.Locate.ElementBySelector(_lblPriceId.ToCssIdSelector()).Text;

        protected string ProductSkuId => "pdProdSku";
        protected string PdAddToCartStickyId  => "pdAddToCartSticky";
        protected string ContentString => "content";
        protected string PdAddToPortfolioNormalId => "pdAddToPortfolioNormal";
        protected string PdProdSkuId => "pdProdSku";
        protected string WriteReviewModalCloseCssSelector  => ".tt-o-modal__close";
        protected string ProductReviewCardClass => "ProductReviewCard__mediaItem";
        protected string ProductName => Browser.Locate.ElementById(_h1ProductNameId).Text;
        protected string StickyWrapperId  => "stickyWrapper";
        protected string PaypalLaterWidgetId => "paypalLaterWidget";
        protected string AllowtransparencySelector = "[allowtransparency='true']";
        protected string TagMediumClass = "tag--medium";
        protected string MessageLogoContainerSelector = ".message__logo-container";
        protected string MessageDisclaimerClass = "message__disclaimer";
        protected string EnergyGuideIconId => "jsEnergyInfoLogo";
        protected virtual string SystemOptionsQtyClass => "systemOptionsQty";
        protected virtual string BuildFullSystemId => "build-full-system";
        protected virtual string SkuOnPdp => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Span, HtmlTextWriterAttribute.Id, PdProdSkuId).GetAttribute(ContentString);
        protected virtual string TurnToQuestionAndAnswerSection => "turntoQuestionsAndAnswersSection";
        protected virtual string TurnToReviewsSection => "turntoReviewsSection";
        protected virtual string DivProductDetailTop => "divProductDetail-top";
        protected virtual string ProductReviewsSectionId => "productReviewsContainer";
        protected virtual string LoadMoreReviewsBtnXpath => "//*[@id='productReviewsContainer']//*[text()='Load More Reviews']";
        protected virtual string PdAddToPortfolioSystemOptionsId => "pdAddToPortfolioSystemOptions";
        protected virtual string PdViewFullTrackSystemId  => "pdViewFullTrackSystem";
        public string BuyItNewLinkText => "Buy It New";
        public string SavedWishListAfterText => "SAVED";
        public string SavedWishListBeforeText => "SAVE";

        private IElement GiftCardMessage => Browser.Locate.ElementById(_giftCardMessageId);
        private IElement GiftCardFirstName => Browser.Locate.ElementById(_giftCardFirstNameId);
        private IElement GiftCardLastName => Browser.Locate.ElementById(_giftCardLastNameId);
        private IElement WisListButtonText => Browser.Locate.ElementByClassName(_wishListButtonTextClass);
        private IElement RecentlyViewedViewAll => Browser.Locate.ElementByClassName(_recentlyViewedViewAllBtnClass);
        private IElement OpenBoxLink => Browser.Locate.ElementById(_openBoxLinkId);
        private IElement ButItNewLinkContainer => Browser.Locate.ElementByLinkText(BuyItNewLinkText);
        private IElement OpenBoxTag => Browser.Locate.ElementByClassName(_openBoxTagClass);
        private IElement StickyTitle => Browser.Locate.ElementByClassName(_pdProdTitleStickyClass);
        private IElement ReviewStarsLink => Browser.Locate.ElementById(_pdSummaryTeaserStarsLinkId);
        private IElement Price => Browser.Locate.ElementById(_lblPriceId);
        private IElement BreadCrumbElement => Browser.Locate.ElementById(_divBreadCrumbId);
        private IElement CompleteTheLookSection => Browser.Locate.ElementById(_productCompleteTheLookId);
        private IElement FirstSampleRoom => Browser.Locate.ElementByXpath(_arSampleImageXpath);
        private IElement SampleRoomBtn => Browser.Locate.ElementByClassName(_sampleRoomClass);
        private IElement LblStockInventory => Browser.Locate.ElementBySelector(_lblStockInventoryId.ToCssIdSelector());
        private IElement ProductQtyCallOut => Browser.Locate.ElementById(_availInventoryId);
        private IElement ModalProductImageThumbnail => Browser.Locate.ElementByXpath(_modalProductImageThumbnailXpath);
        private IElement PreviousCarrot => Browser.Locate.ElementByXpath("//*[@id=\"prevImageModal\"]");
        private IElement FanFeatures => Browser.Locate.ElementById(_pdFanFeaturesId);
        private IElement ProductDetailSection => Browser.Locate.ElementById(_productDetailsSectionId);
        private IElement StrikeThroughPrice => Browser.Locate.ElementByXpath(_pnlProductPriceStrikeXpath);
        private IElement StickyStrikeThroughPrice => Browser.Locate.ElementBySelector(_newProductPriceSelector);
        private IElement SaveAmount => Browser.Locate.ElementById(_priceAdditionalSaveId);
        private IElement StickySaveAmount => Browser.Locate.ElementByXpath(_stickyPriceContainerSaveXpath);
        private ReadOnlyCollection<IElement> ListOfBreadCrumbLink() => BreadCrumbElement.FindElements(By.TagName("a"));

        protected IElement StickyAddToCart => Browser.Locate.ElementById(PdAddToCartStickyId);
        protected IElement AddToWishListButton => Browser.Locate.ElementById(PdAddToPortfolioNormalId);
        protected IElement ImageTabContent => Browser.Locate.ElementByClassName(_imageTabContentClass);
        protected IElement QuestionsAndAnswersSection => Browser.Locate.ElementById(_turnToQuestionsAndAnswersSectionId);
        protected IElement ProductReviewCard(int index) => Browser.Locate.ElementsByClassName(ProductReviewCardClass)[index];
        protected IElement ShipInLink => Browser.Locate.ElementById(_pdInStockId);
        protected IElement TtProductSearchResultCards(int index) => Browser.Locate.ElementsByClassName(_ttProductSearchResultsCardClass)[index];
        protected IElement TtQnASearchBar => Browser.Locate.ElementById(_ttQnASearchBarId);
        protected IElement AddToCartButton => Browser.Locate.ElementBySelector($"{_pdAddToCartId.ToCssIdSelector()}, {_addToCartMultiproductId.ToCssIdSelector()}");
        protected IElement FreeShippingCallout => Browser.Locate.ElementById(_lblFreeShippingId);
        protected IElement ClickOnPrintIconButton => Browser.Locate.ElementByClassName(_printIconClass);
        protected IElement ClickOnPrintKioskStyleIconButton => Browser.Locate.ElementById(_printKioskStyleId);
        protected IElement PrintModalFrame => Browser.Locate.ElementById(_printModalFrameId);
        protected IElement ClickOnPrintKioskRoomSceneButton => Browser.Locate.ElementById(_printKioskRoomSceneId);
        protected IElement ViewInYourRoomBtn => Browser.Locate.ElementById(_viewInYourRoomId);
        protected IElement PaypalLogo => Browser.Locate.ElementById(PaypalLaterWidgetId);
        protected IElement QuantityField => Browser.Locate.ElementBySelector($"#{_qtyNormalInputId}, #{_qtyMultiProdId}");
        protected IElement PdMymlSection => Browser.Locate.ElementBySelector(_pdMoreYouMayLikeId.ToCssIdSelector());
        protected IElement EnergyGuideIcon => Browser.Locate.ElementById(EnergyGuideIconId); 
        protected IElement TurnToReviewModal => Browser.Locate.ElementBySelector(_writeReviewModalSelector);
        protected IElement EnergyInfoModal => Browser.Locate.ElementById(_energyInfoModalId);

        protected virtual IElement MediaModalContentModal => Browser.Locate.ElementByClassName(_mediaModalContentModalClass);
        protected virtual IElement StickyPrice => Browser.Locate.ElementById(_lblStickyPriceId);
        protected virtual IElement NeedHelpLink => Browser.Locate.ElementById(_pdNeedHelpLinkId);
        protected virtual IElement NeedHelpModalCloseBtn => Browser.Locate.ElementByClassName(_needHelpModalCloseButtonClass);
        protected virtual IElement StockCheckWrapper => Browser.Locate.ElementByXpath(_stockCheckXpath);
        protected virtual IElement BuildFullSystemAddToWishListButton => Browser.Locate.ElementById(PdAddToPortfolioSystemOptionsId);
        protected virtual IElement BuildFullSystemButton => Browser.Locate.ElementById(PdViewFullTrackSystemId);
        protected virtual IElement LoadMoreReviews => Browser.Locate.ElementByXpath(LoadMoreReviewsBtnXpath);
        protected virtual IElement TurnToQuestionsAndAnswersSection => Browser.Locate.ElementById(TurnToQuestionAndAnswerSection);
        protected virtual IElement TurnToReviewSection => Browser.Locate.ElementById(TurnToReviewsSection);
        protected virtual IElement TopContentProductDetail => Browser.Locate.ElementById(DivProductDetailTop);
        protected virtual IElement ProductReviewsSection => Browser.Locate.ElementById(ProductReviewsSectionId);
        protected virtual IElement BuildFullSystemAddToCartButton => Browser.Locate.ElementById(_pdAddToCartSystemOptionsId);
        protected virtual IElement StickyWrapper => Browser.Locate.ElementById(StickyWrapperId);
        protected virtual IElement BuildFullSystemContainer => Browser.Locate.ElementById(BuildFullSystemId);
        protected virtual IElement CustomerPhotos => Browser.Locate.ElementByClassName(_customerPhotosClass);
        protected virtual IElement ReplacementPartLink => Browser.Locate.ElementById(_pdReplacementPartModalId);
        protected virtual IElement MakeAppointmentBtn => Browser.Locate.ElementByClassName(_makeAnAppoinmentBtnClass);
        protected virtual IElement MoreImages(int index) => Browser.Locate.ElementBySelector($"{_productImageThumbnailId.ToCssIdSelector()} > {HtmlTextWriterTag.Div.ToNthChildSelector(index)}");
        protected virtual IElement ProductSpecificationsTable => Browser.Locate.ElementByClassName(_specificationSectionClass);
        protected virtual ReadOnlyCollection<IElement> BuildFullSystemShortSkuLinks => Browser.Locate.ElementsBySelector($"{HtmlTextWriterTag.Td.ToNthChildSelector(3)} {HtmlTextWriterTag.A}", BuildFullSystemContainer);
        protected virtual ReadOnlyCollection<IElement> BuildFullSystemQtyElements => Browser.Locate.ElementsByClassName(SystemOptionsQtyClass, BuildFullSystemContainer);
        protected virtual ReadOnlyCollection<IElement> ThumbnailImages => Browser.Locate.ElementsByClassName(_thumbnailWrapperClass, Browser.Locate.ElementById(_productImageThumbnailId));

        protected void ClickPhotoModal(int productReviewCounter, int pixelsScroll, int reviewClassNotFound, int endCondition)
        {
            do
            {
                Browser.ScrollToByPixelsVertical(pixelsScroll.ToString());
                if (Browser.Locate.ElementImmediately(_writeReviewBtnSelector).IsInitialized)
                {
                    Browser.Wait.ForDisplayedElement(ProductReviewCard(0));

                    ProductReviewCard(0).Click();
                    break;
                }

                //Log.Message($"Scrolling to element, scroll# {productReviewCounter}");
                pixelsScroll += pixelsScroll;

                if (productReviewCounter == reviewClassNotFound) throw new FrameworkWaitException($"Element {ProductReviewCardClass} is not found");

            } while (productReviewCounter < endCondition);
        }

        //Instances
        protected IBrowser Browser;
        protected IAssert Assert;
        protected ProductActions ProductActions;
        protected OperatingSystem OperatingSystem;
        protected IModalDesktop Modal;

        public ProductDetailDesktop(IBrowser browser, ProductActions productActions, IAssert assert, OperatingSystem operatingSystem, IModalDesktop modal)
        {
            Browser = browser;
            Assert = assert;
            ProductActions = productActions;
            OperatingSystem = operatingSystem;
            Modal = modal;
        }

        //Interface implementation
        public string PageTitle { get; }
        public string PageUrl { get; }
        public bool IsCurrentPage => Browser.Wait.IsVisibleElement(By.CssSelector(ProductSkuId.ToCssIdSelector()));
        public bool IsAddToCartButtonVisible => Browser.Locate.DoesElementExistImmediately(($"{_pdAddToCartId.ToCssIdSelector()}, {_addToCartMultiproductId.ToCssIdSelector()}"));
        public virtual bool IsCallCustomerServiceBlockVisible => Browser.Locate.DoesElementExistImmediately(_pnlPleaseCallCustomerServiceId.ToCssIdSelector());
        public virtual bool IsNeedHelpModalVisible => Browser.Wait.IsVisibleElement(By.ClassName(_mediaModalContentModalClass));
        public virtual bool IsOpenBoxVerbiageVisibleOnStickyHeader => Browser.Wait.IsVisibleElement(By.CssSelector(_stickyPriceContainerSelector));
        public virtual bool IsNeedHelpModalChatVisible => Browser.Locate.ElementsByClassName(_needHelpSupportInfoChatClass).Count > 0;
        public bool IsEndsVerbiageVisible => Browser.Locate.ElementsByClassName(_endsDateClass).Count > 0;
        public bool IsEndsVerbiageVisibleOnStickyHeader => Browser.Locate.ElementsByXpath(_stickyPriceContainerEndsXpath).Count > 0;
        public bool IsSaleVerbiageVisible => Browser.Locate.ElementsBySelector(_priceTypeUpperCaseSelector).Count > 0;
        public bool IsSaleVerbiageVisibleOnStickyHeader => Browser.Locate.ElementsByXpath(_stickyPriceContainerSaleXpath).Count > 0;

        public virtual Dictionary<string, int> AddAllBuildFullSystemSkusToCart()
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

        public virtual Dictionary<string, int> AddAllBuildFullSystemSkusToWishList(int qtyCtr)
        {
            BuildFullSystemButton.Click();

            var buildFullSystemQtyFields = BuildFullSystemQtyElements;
            var buildFullSystemShortSkuLinks = BuildFullSystemShortSkuLinks;
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

            Browser.ScrollIntoView(BuildFullSystemAddToWishListButton);
            BuildFullSystemAddToWishListButton.Click();

            return addedProducts;
        }

        public void StickyNavAddToCart()
        {
            Browser.Wait.ForDomReady();
            StickyAddToCart.Click();
        }

        public virtual List<IElement> GetStickyNavContents()
        {
            var contentsItems = new List<IElement>
            {
                StickyAddToCart,
                StickyTitle,
                StickyPrice
            };

            return contentsItems;
        }

        public void QnASearchByText(string text)
        {
            Browser.Wait.ForDisplayedElement(TtQnASearchBar);
            TtQnASearchBar.SendKeys(text);
            Browser.Wait.IsVisibleElement(By.ClassName(_ttProductSearchResultsClass));
        }

        public virtual void OpenProductHelpAndStoreAvailabilityModal()
        {
            Browser.Wait.IsVisibleElement(By.Id(_pdNeedHelpLinkId));
            NeedHelpLink.Click();
            Browser.Wait.IsVisibleElement(By.ClassName(_appNeedHelpWithProductClass));
        }

        public virtual void CloseNeedHelpModal()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_needHelpModalCloseButtonClass));
            NeedHelpModalCloseBtn.Click();
        }

        public virtual void OpenReviewsModal()
        {
            Browser.Wait.ForElement(Browser.Locate.ElementBySelector(_writeReviewBtnSelector)).Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(".tt-c-review-form__submit"));
        }

        public virtual IElement GetTurnToReviewSection()
        {
            Browser.ScrollIntoView(TurnToReviewSection);
            Browser.Wait.ForDomReady();
            Browser.Wait.ForElement(Browser.Locate.ElementBySelector(_writeReviewBtnSelector));
            return TurnToReviewSection;
        }

        public void GetFirstResultFromAskQuestionSection()
        {
            Browser.Wait.ForClickableElement(TtProductSearchResultCards(0)).Click();
        }

        public void CloseReviewModal()
        {
            Browser.Locate.ElementBySelector(WriteReviewModalCloseCssSelector).Click();
        }

        public virtual void OpenReviewPhotoModal()
        {
            Browser.ScrollIntoView(TurnToReviewSection);
            Browser.Wait.ForDomReady();
            Browser.Wait.ForDisplayedElement(LoadMoreReviews);

            Browser.ScrollToElement(LoadMoreReviews);
            Browser.Wait.ForClickableElement(LoadMoreReviews).Click();

            Browser.ScrollIntoView(TurnToReviewSection);
            Browser.Wait.ForDomReady();

            ClickPhotoModal(productReviewCounter: 0, pixelsScroll: 250, reviewClassNotFound: 9, endCondition: 10);

            Browser.Wait.ForDomReady();
            Browser.Wait.IsVisibleElement(By.ClassName(_mediaModalContentModalClass));
        }

        public string GetDbProductNameBySku(string sku)
        {
            return Page.DecodeHtmlString(ProductActions.GetShortSkuNameAndPrice(sku).Name);
        }

        public string GetProductName()
        {
            return ProductName;
        }

        public string GetSavedButtonCallout()
        {
            return WisListButtonText.Text;
        }

        public string GetProductPriceOnPdp()
        {
            Browser.Wait.IsVisibleElement(By.Id(_pdAddToCartId),30);
            return TextActions.GetOnlyPriceFromString(ItemPriceText);
        }

        public string GetOpenBoxCallout()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_openBoxTagClass));
            return OpenBoxTag.Text;
        }

        public decimal GetDbProductPriceBySku(string sku)
        {
            return decimal.Parse(ProductActions.GetShortSkuNameAndPrice(sku).Price.Trim().Trim('$'));
        }

        public float GetProductPrice()
        {
            var textReturned = Price.Text;
            return float.Parse(TextActions.GetPriceTextOnly(textReturned));
        }

        public void AddToCart()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_pdAddToCartId.ToCssIdSelector()));
            Browser.ScrollIntoView(AddToCartButton);
            Browser.Wait.ForDomReady();
            Browser.ClickByJs(AddToCartButton);
            Browser.Wait.ForDomReady();
        }

        public void ClickOnPrintIcon()
        {
            Browser.Wait.ForElement(ClickOnPrintIconButton).Click();
            Modal.IsModalVisible();
        }

        public void ClickOnPrintKioskStyleIcon()
        {
            Browser.SwitchFocusToIframe(PrintModalFrame);
            Browser.Wait.IsVisibleElement(By.CssSelector(_printKioskStyleId.ToCssIdSelector()));
            ClickOnPrintKioskStyleIconButton.Click();
            Browser.SwitchToCurrentWindow();
        }

        public virtual void ClickOnViewInYourRoom()
        {
            Browser.Wait.ForDisplayedElement(ViewInYourRoomBtn);
            ViewInYourRoomBtn.Click();
            Browser.SwitchFocusToIframe(Browser.Locate.ElementBySelector(_arIframeId.ToCssIdSelector()));
            Browser.Wait.IsVisibleElement(By.CssSelector(_sampleRoomClass.ToCssClassSelector()), 50);
        }

        public void AddMultipleProductsToRoom()
        {
            Browser.Wait.ForDisplayedElement(ViewInYourRoomBtn);
            ViewInYourRoomBtn.Click();
            Browser.SwitchFocusToIframe(Browser.Locate.ElementBySelector(_arIframeId.ToCssIdSelector()));
            Browser.Wait.IsVisibleElement(By.CssSelector(_addActiveRoomCss), 50);
            Browser.Locate.ElementBySelector(_addActiveRoomCss).Click();
            Browser.SwitchToDefaultContent();
            Browser.Wait.IsVisibleElement(By.CssSelector(_lpHeaderWrapperId.ToCssIdSelector()));
        }

        public void SwitchToIframe()
        {
            Browser.Wait.ForDomReady();
            Browser.SwitchToDefaultContent();
            Browser.SwitchFocusToIframe(Browser.Locate.ElementBySelector(_arIframeId.ToCssIdSelector()));
            Browser.Wait.ForDomReady();
        }

        public void AddToCartIpad()
        {
            Browser.ScrollIntoView(AddToCartButton);
            Browser.ScrollToByPixelsVertical("-70");
            var xElementCoordinate = 0;
            var yElementCoordinate = 0;
            Browser.GetElementCoordinates(AddToCartButton, ref xElementCoordinate, ref yElementCoordinate, 105);
            Browser.ClickWithTapByCoordinates(xElementCoordinate, yElementCoordinate);
        }

        public void ClickOnPrintKioskStyleWithRoomScene()
        {
            Browser.SwitchFocusToIframe(PrintModalFrame);
            Browser.Wait.IsVisibleElement(By.CssSelector(_printKioskRoomSceneId.ToCssIdSelector()));
            ClickOnPrintKioskRoomSceneButton.Click();

            Browser.SwitchToCurrentWindow();
        }

        public IElement GetPayPalLogo()
        {
            try
            {
                Browser.Wait.IsVisibleElement(By.Id(PaypalLaterWidgetId), 30);
            }
            catch
            {
                Browser.RefreshPage();
                Browser.Wait.IsVisibleElement(By.Id(PaypalLaterWidgetId), 30);
            }

            return PaypalLogo;
        }

        public virtual string GetPayPalCalloutPDP()
        {
            string finalString;

            Browser.SwitchFocusToIframe(Browser.Locate.ElementBySelector(AllowtransparencySelector));

            var payPalVerbiage = Browser.Locate.ElementByClassName(TagMediumClass).Text;
            var getWithFromVerbiage = Browser.ExecuteJs("return window.getComputedStyle(document.querySelector(arguments[0]), ':before').getPropertyValue('content');", MessageLogoContainerSelector).ToString().Remove(5).Substring(1);
            var getDotFromVerbiage = Browser.ExecuteJs("return window.getComputedStyle(document.querySelector(arguments[0]), ':after').getPropertyValue('content');", MessageLogoContainerSelector).ToString().Remove(2).Substring(1);
            var payPalTxt = Browser.Locate.ElementBySelector(MessageLogoContainerSelector).GetAttribute("alt");
            var learnMoreText = Browser.Locate.ElementByClassName(MessageDisclaimerClass).Text;

            if (learnMoreText.Contains("\r\n"))
            {
                var learnMoreTxt = learnMoreText.Substring(0, learnMoreText.IndexOf("\r\n"));
                finalString = payPalVerbiage + " " + getWithFromVerbiage + " " + payPalTxt + getDotFromVerbiage + " " + learnMoreTxt;
            }
            else
            {
                finalString = payPalVerbiage + " " + getWithFromVerbiage + " " + payPalTxt + getDotFromVerbiage + " " + learnMoreText;
            }

            Browser.SwitchToDefaultContent();

            return finalString;
        }

        public virtual string GetProductSku()
        {
            return Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Span, HtmlTextWriterAttribute.Id, ProductSkuId).GetAttribute(ContentString);
        }

        public void AddToWishList()
        {
            Browser.Wait.IsVisibleElement(By.Id(PdAddToPortfolioNormalId));
            Browser.Wait.ForElementToStopAnimating(AddToWishListButton);
            Browser.Wait.ForClickableElement(AddToWishListButton);
            Browser.ClickByJs(AddToWishListButton);
            Browser.Wait.WaitForAjaxComplete();
        }

        public virtual void AddProductMaxQuantity()
        {
            Browser.Wait.ForCondition(() => Browser.Locate.ElementsByTagName(HtmlTextWriterTag.Option, QuantityField).Count > 0);

            Browser.Locate.ElementsByTagName(HtmlTextWriterTag.Option, QuantityField).Last().Click();
        }

        public void NavigateToProductDetailByShortSku(string shortSku)
        {
            var url = $"{Urls.HomePageUrl}{Urls.ProductsUrlDirectory}/{shortSku}";

            Browser.Navigate(url);
            Assert.True(IsCurrentPage, "Current page is not Pdp page");
        }

        public string GetShortSkuPrice()
        {
            return ItemPriceText.TrimStart('$').Substring(0, ItemPriceText.IndexOf(".", StringComparison.Ordinal) + 3);
        }

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
            }
        }

        public virtual List<string> GetFreeShippingProductsSkus(List<string> listOfLinks)
        {
            var listOfSkus = new List<string>();

            foreach (var link in listOfLinks)
            {
                Browser.Navigate(link);

                Browser.Wait.IsVisibleElement(By.CssSelector(PdAddToPortfolioNormalId.ToCssIdSelector()));

                Assert.Displayed(FreeShippingCallout, "The free shipping callout was not displayed on the page.");

                listOfSkus.Add(SkuOnPdp);
            }

            return listOfSkus;
        }

        public void NavigateToArPage()
        {
            Browser.Wait.ForDisplayedElement(Browser.Locate.ElementByXpath(_showInRoomBtnXpath)).Click();
            Browser.Wait.ForDomReady();
            Browser.SwitchFocusToIframe(Modal.GetIframeModal());

            //Select "Use sample room"
            Browser.Wait.IsVisibleElement(By.CssSelector(_sampleRoomClass.ToCssClassSelector()), 15);
            Browser.Locate.ElementBySelector(_sampleRoomClass.ToCssClassSelector()).Click();
            Browser.Wait.ForDomReady();
            Browser.Wait.IsVisibleElement(By.XPath(_browseXpath));

            if (OperatingSystem == OperatingSystem.Mac)
            {
                Browser.ClickByCoordinatesJs(_browseXpath, 0, -100);
            }
            else
            {
                Browser.Wait.ForClickableElement(Browser.Locate.ElementByXpath(_arSampleImageXpath)).Click();
            }

            Browser.Wait.ForDomReady();
            Browser.SwitchToDefaultContent();
            Browser.Wait.IsVisibleElement(By.XPath(_sidebarTopButtonXpath));
            Browser.Wait.IsVisibleElement(By.CssSelector(_lpHeaderWrapperId.ToCssIdSelector()));
        }

        public void OpenSampleRoomModal()
        {
            Browser.SwitchFocusToIframe(Browser.Locate.ElementBySelector(_arIframeId.ToCssIdSelector()));
            Browser.Wait.IsVisibleElement(By.CssSelector(_sampleRoomClass.ToCssClassSelector()));
            SampleRoomBtn.Click();
            Browser.SwitchToDefaultContent();
            Browser.SwitchFocusToIframe(Browser.Locate.ElementBySelector(_arIframeId.ToCssIdSelector()));
            Browser.Wait.IsVisibleElement(By.XPath(_arSampleImageXpath), 30);
        }

        public void SelectSampleArRoom()
        {
            Browser.Wait.IsVisibleElement(By.XPath(_arSampleImageXpath));
            FirstSampleRoom.Click();
        }

        public void NavigateToEachProductDetailPage(IList<string> shortSkusList )
        {
            foreach (var sku in shortSkusList)
            {
                NavigateToProductDetailByShortSku(sku);
                Browser.Wait.IsVisibleElement(By.Id(_pdAddToCartId));
                Browser.Wait.ForDisplayedElement(AddToCartButton);
                Browser.ScrollIntoView(Browser.Locate.ElementByXpath(_addToCartXpath), alignToBottom: true);
                Browser.ScrollToBottomOfPage(Browser.PageUrl);
                Browser.Wait.ForDomReady();
            }
        }

        public void AddProductToCart(IList<string> skusList)
        {
            var sku = skusList[0];
            NavigateToProductDetailByShortSku(sku);
            Browser.Wait.IsVisibleElement(By.Id(_pdAddToCartId));
            Browser.ScrollIntoView(Browser.Locate.ElementByXpath(_addToCartXpath), alignToBottom: true);
            Browser.Wait.ForClickableElement(Browser.Locate.ElementByXpath(_addToCartXpath));
            Browser.Wait.ForDomReady();
            AddToCartButton.Click();
        }

        public void NavigateToMultiplePdps(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var sku = ProductActions.GetAnySkuWithProductDetailPage;
                Assert.DatabaseObject(sku, "ProductActions.GetAnySkuWithProductDetailPage()");

                NavigateToProductDetailByShortSku(sku);
                Assert.True(IsCurrentPage, "User is Not on Product detail Page");
            }
        }

        public void AddSingleProductToCart(string sku)
        {
            NavigateToProductDetailByShortSku(sku);
            Browser.Wait.IsVisibleElement(By.Id(_pdAddToCartId));
            Browser.ScrollIntoView(Browser.Locate.ElementByXpath(_addToCartXpath), alignToBottom: true);
            Browser.Wait.ForClickableElement(Browser.Locate.ElementByXpath(_addToCartXpath)).Click();
        }

        public virtual void ChangeProductQuantity(string quantity)
        {
            Browser.Wait.IsVisibleElement(By.Id(_qtyNormalInputId));
            QuantityField.SendKeys(Keys.Backspace);
            Browser.Wait.ForDomReady();
            QuantityField.SendKeys(quantity);
        }

        public string GetProductQuantity()
        {
            Browser.Wait.IsVisibleElement(By.Id(_qtyNormalInputId));
            return QuantityField.GetAttribute("value");
        }

        public string GetProductCallOutQuantity()
        {
            return ProductQtyCallOut.Text;
        }

        public string GetBuyItNewText()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_buyItNewContainerClass));
            return ButItNewLinkContainer.Text;
        }

        public string GetOpenBoxAvailableLinkText()
        {
            Browser.Wait.IsVisibleElement(By.Id(_openBoxLinkId));
            return OpenBoxLink.Text;
        }

        public void SwitchToNewProduct()
        {
            ButItNewLinkContainer.Click();
            Browser.Wait.IsVisibleElement(By.Id(_openBoxLinkId));
        }

        public void SwitchToTheOpenBoxProduct()
        {
            OpenBoxLink.Click();
            Browser.Wait.IsVisibleElement(By.ClassName(_buyItNewContainerClass));
        }

        public bool IsPaypalLaterWidgetDisplayed()
        {
            return Browser.Wait.IsVisibleElement(By.Id(PaypalLaterWidgetId));
        }

        public virtual string GetBreadcrumbText()
        {
            var returnedString = String.Concat(BreadCrumbElement.Text.Trim().Where(c => !Char.IsWhiteSpace(c)));
            return returnedString.Substring(0, 38);
        }

        public string GetProductInventory()
        {
            Browser.Wait.ForDisplayedElement(LblStockInventory);
            return LblStockInventory.Text;
        }

        public virtual void FocusCompleteLookSection()
        {
            Browser.ScrollIntoView(CompleteTheLookSection);
        }

        public IElement GetWishListButton()
        {
            return AddToWishListButton;
        }

        public void OpenShipInModal()
        {
            Browser.Wait.IsVisibleElement(By.Id(_pdInStockId));
            ShipInLink.Click(); 
        }

        public void AddGiftCardDetails(string inputText)
        {
            Browser.Wait.IsVisibleElement(By.Id(_giftCardFirstNameId));
            GiftCardFirstName.Click();
            GiftCardFirstName.SendKeys(inputText);
            GiftCardLastName.Click();
            GiftCardLastName.SendKeys(inputText);
            GiftCardMessage.Click();
            GiftCardMessage.SendKeys(inputText);
        }

        public bool IsChatIconEnabled()
        {
            var start = new TimeSpan(04, 00, 00);
            var start1 = new TimeSpan(07, 00, 00);
            var end = new TimeSpan(20, 00, 00);
            var nowOrig = DateTime.Now.TimeOfDay;

            foreach (var str in Enum.GetNames(typeof(DayOfWeek)))
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

        public string CalculatePayPalInterestRate(decimal productPrice)
        {
            return Math.Round((productPrice / 4), 2).ToString();
        }
        
        public void NavigateToPlaPageByShortSku(string shortSku)
        {
            var url = $"{Urls.ProductFullPageBaseUrl}{shortSku}";

            Browser.Navigate(url);
            Browser.Wait.IsVisibleElement(By.CssSelector(($"{_pdAddToCartId.ToCssIdSelector()}, {_addToCartMultiproductId.ToCssIdSelector()}")));
        }

        public int GetNumberOfThumbnailImages()
        {
            return ThumbnailImages.Count;
        }

        public virtual void SelectThumbnailImage(int indexOfThumbnail)
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_productImageThumbnailId.ToCssIdSelector()));
            var selectedThumb = MoreImages(indexOfThumbnail);
            Browser.MouseOverOnElement(selectedThumb);
        }

        public void OpenThumbnailModal()
        {
            MoreImages(2).Click();
            Modal.IsModalVisible();
            Browser.Wait.IsInvisibleElement(By.XPath("//*[@id='fsContent']/span"));
        }

        public void SelectDifferentThumbnailInsideModal()
        {
            Browser.Wait.ForDomReady();
            ModalProductImageThumbnail.Click();
        }

        public virtual void OpenCustomerPhotosTab()
        {
            CustomerPhotos.Click();
            Browser.Wait.ForDomReady();
            Browser.Wait.ForDisplayedElement(PreviousCarrot);
            Browser.Wait.ForDisplayedElement(Browser.Locate.ElementById(_fsZoomOptionsId));
        }

        public virtual void OpenBulbAndReplacementPartsModal()
        {
            Browser.Wait.ForClickableElement(ReplacementPartLink).Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(_overlayContentWrapperClass.ToCssClassSelector()));
        }

        public void ScrollToProductSpecificationTable()
        {
            Browser.ScrollIntoView(ProductSpecificationsTable);
            Browser.Wait.ForDomReady();
        }

        public void NavigateToOpenBoxProductDetailByShortSku(string shortSku)
        {
            Browser.Navigate($"{Urls.OpenBoxProductPageUrl}{shortSku}");
        }

        public void MoveToReviewsSection()
        {
            ReviewStarsLink.Click();
            Browser.Wait.ForDomReady();
        }

        public void FocusOnFanFeaturesSection()
        {
            Browser.ScrollIntoView(FanFeatures);
            Browser.ExecuteJs("window.scrollBy(0,-75)"); //Move the Fan Features header out from under the floating menu.
        }

        public virtual void OpenEnergyGuide()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(EnergyGuideIconId.ToCssIdSelector()));
            Browser.ScrollIntoView(ProductDetailSection);
            EnergyGuideIcon.Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(EnergyGuideIconId.ToCssIdSelector()));
            Browser.ScrollToTopOfWindow();
        }

        public void NavigateToRecentlyViewedPage()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_recentlyViewedViewAllBtnClass));
            RecentlyViewedViewAll.Click();
            Browser.Wait.ForDomReady();
        }

        public bool IsQuantityBoxDisplayed()
        {
            return Browser.Wait.IsVisibleElement(By.Id(_qtyNormalInputId));
        }

        public virtual void OpenMakeAnAppointmentModal()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_makeAnAppoinmentBtnClass));
            MakeAppointmentBtn.Click();
        }

        public string GetStrikeThroughPriceOnPdp()
        {
            return StrikeThroughPrice.Text;
        }

        public string GetStrikeThroughPriceOnStickyHeader()
        {
            return StickyStrikeThroughPrice.Text;
        }

        public string GetSaveAmountOnPdp()
        {
            return SaveAmount.Text;
        }

        public string GetSaveAmountOnStickyHeader()
        {
            return StickySaveAmount.Text;
        }

        public decimal GetProductPriceOnStickyHeader()
        {
            return decimal.Parse(TextActions.GetPriceTextOnly(StickyPrice.Text));
        }

        public void GetRelationshipWidgetSection()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_pdAddToCartId.ToCssIdSelector()));
            Browser.ScrollIntoView(AddToCartButton);
        }
    }
}