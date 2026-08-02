using System.Web.UI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using Automation.Framework;
using Automation.Framework.Core;
using Automation.Framework.Utilities;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Databases.Actions;
using LampsPlus.AutomationFramework.Pages.Refactored.Modal;
using OperatingSystem = LampsPlus.AutomationFramework.Utilities.TestConfiguration.OperatingSystem;

namespace LampsPlus.AutomationFramework.Pages.Refactored.ProductDetail
{
    public class ProductDetailMobile : ProductDetailDesktop, IProductDetailMobile
    {
        //Class Members
        private string _limitedQtyFieldClass = "qtyLimitedField";
        private string _quantityDrawerXpath = "//div[@aria-hidden='false']//parent::div[@id='pdQtyLimitedDrawer']";
        private string _maxQuantityXpath = "//*[@id='pdQtyLimitedDrawer']//li[last()]//label";
        private string _jsCertonaTitleClass  = ".jsCertonaTitle.sectionTitle.alt2.fl";
        private string _shipsInMessageClass = "shipsInMessage";
        private string _priceLabelId = "lblPrice";
        private string _productCompleteTheLookCollapsibleButtonId = "productCompleteTheLookCollapsibleButton";
        private string _productCompleteTheLookSkeletonId = "#productCompleteTheLookSkeleton";
        private string _divBreadCrumbId = "divBreadCrumb";
        private string _writeReviewBtnXpath = "productReviews__writeReviewBtn";
        private string _questionAnswerArrowId = "productQuestionsAndAnswersCollapsibleButton";
        private string _productReviewRatingStarCountClass = "ProductReviewRatingStars__summaryCount";
        private string _productReviewsSectionXpath = "//*[@id='productReviewsCollapsible']/button";
        private string _haveAQuestionCallContainerClass = "haveAQuestion__call-container";
        private string _haveAQuestionSectionId = "haveAQuestionSection";
        private string _chatXpath = "//*[text()='Chat']";
        private string _widgetFloatingHeaderClass = "widget-floating__header";
        private string _confirmationDialogButtonYesClass = "confirmation-dialog__button--yes";
        private string _widgetFloatingButtonCloseSelector = "button.widget-floating__button--close";
        private string _moreYouLikeBorderClass = "moreYouLikeBorder";
        private string _mobileCallXpath = "(//XCUIElementTypeStaticText[@name='Call 1 (888) 739-0201'])[2]";
        private string _buildFullSystemContainerId = "buildFullSystemContainer";
        private string _pdpStickyHeaderId = "pdpStickyHeader";
        private string _pdAddToCartBuildFullId = "pdbuildFullSystemAddToCart";
        private string _buildFullSystemDrawerXpath = "//*[@id='pdBuildFullSystemCollapsibleButton']";
        private string _qtyNormalInputId = "QtyNormal";
        private string _pdRelatedItemsClass = "pdRelatedItems";
        private string _lblStickyPriceClass = "pdpStickyHeader__price--regular";
        private string _lblStickyImageClass = "pdpStickyHeader__image-wrapper ";
        private string _showProductHelpLinkId = "showProductHelpLink";
        private string _needHelpModalCloseButtonClass = "lpMobileOverlayClose";
        private string _showProductHelpModalId = "showProductHelpModal";
        private string _thumbnailImageCarouselClass = "js-pd-carousel-image";
        private string _thumbnailImageCarouselId = "pdImageCarousel";
        private string _btnPdpZoomClass = "btnPdpZoom";
        private string _customerPhotosXpath = "//li[text()='Customer Photos']";
        private string _productStoreInfoId = "productStoreInfo";
        private string _chatBtnClass = "chatBtn";
        private string _pnlPleaseCallCustomerServiceClass = "pnlPleaseCallCustomerService";
        private string _overlayContentWrapperClass = "Overlay__contentWrapper";
        private string _productDescId = "pnlProductDescription";
        private string _productDescSelector = "//*[@id='pnlProductDescriptionyCollapsibleButton']";
        private string _addReplacementPartsReplacementPartsModalClass = "add-replacementParts__replacementPartsModal";
        private string _replacementPartLinkId = "replacementPartsBtn";
        private string _productSpecificationTablesId = "pnlProductSpecificationCollapsibleButton";
        private string _productAttributesClass = "productAttributes";
        private string _searchQueryReviewsId = "#searchQuery_reviews";
        private string _inHomeConsultationAppointmentClass = "in-home-consultation_appointment";
        private string _moreYouMayLikeContainerId = "pdMoreYouMayLike";
        private string _buildFullSystemImgXpath = "//*[@class='buildFullSystem__image']/img";
        private string _customerPhotoTabSelector = "main > div:nth-child(4)";
        private string _pdpStickyHeaderPriceTypeClass = "pdpStickyHeader__priceType";

        protected override string PdAddToPortfolioSystemOptionsId => "pdbuildFullSystemAddToWishList";
        protected override string PdViewFullTrackSystemId => "pdViewFullTrackSystemBtn";
        protected override string LoadMoreReviewsBtnXpath => "//*[@id='productReviewsApp']//*[text()='Load More Reviews']";
        protected override string TurnToQuestionAndAnswerSection => "jsProductQuestionsAndAnswersContainer";
        protected override string TurnToReviewsSection => "jsTurnToReviewsContainer";
        protected override string DivProductDetailTop  => "divProductDetail";
        protected override string ProductReviewsSectionId  => "productReviewsCollapsibleButton";
        protected override string SystemOptionsQtyClass  => "buildFullSystem__qty";
        protected override string SkuOnPdp => Browser.Locate.ElementBySelector(ProductSkuId.ToCssIdSelector()).GetAttribute(ContentString);

        private IElement BuildFullSystemDrawer => Browser.Locate.ElementByXpath(_buildFullSystemDrawerXpath);
        private IElement BuildFullSystemProductContainer => Browser.Locate.ElementById(_buildFullSystemContainerId);
        private IElement ProductQuestionAnswerArrow => Browser.Locate.ElementById(_questionAnswerArrowId);
        private IElement WriteReviewBtn => Browser.Locate.ElementByClassName(_writeReviewBtnXpath);
        private IElement LimitedQtyField => Browser.Locate.ElementBySelector(_limitedQtyFieldClass.ToCssClassSelector());
        private IElement MobileMaxQuantity => Browser.Locate.ElementByXpath(_maxQuantityXpath);
        private IElement Price => Browser.Locate.ElementById(_priceLabelId);
        private IElement BreadCrumbTrail => Browser.Locate.ElementById(_divBreadCrumbId);
        private IElement CompleteTheLook => Browser.Locate.ElementById(_productCompleteTheLookCollapsibleButtonId);
        private IElement ChatButton(int index) => Browser.Locate.ElementsByXpath(_chatXpath)[index];
        private IElement HaveAQuestionContent => Browser.Locate.ElementById(_haveAQuestionSectionId);
        private IElement AssistantCloseIcon => Browser.Locate.ElementBySelector(_widgetFloatingButtonCloseSelector);
        private IElement ConfirmationCloseButton => Browser.Locate.ElementBySelector(_confirmationDialogButtonYesClass.ToCssClassSelector());
        private IElement MoreYouLikeHeader => Browser.Locate.ElementByClassName(_moreYouLikeBorderClass);
        private IElement StickyImage => Browser.Locate.ElementByClassName(_lblStickyImageClass);
        private IElement ZoomIcon => Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.Button, _btnPdpZoomClass);
        private IElement ProductDescDropDown => Browser.Locate.ElementByXpath(_productDescSelector);
        private IElement ProductAttributes => Browser.Locate.ElementByClassName(_productAttributesClass);
        private IElement SearchReviewField => Browser.Locate.ElementBySelector(_searchQueryReviewsId);
        private ReadOnlyCollection<IElement> BuildFullSystemImage => Browser.Locate.ElementsByXpath(_buildFullSystemImgXpath);


        protected IElement CertonaDrawerName => Browser.Locate.ElementBySelector(_jsCertonaTitleClass);
        protected IElement CompleteTheLookSection => Browser.Locate.ElementBySelector(_productCompleteTheLookSkeletonId);
        protected IElement CustomerPhotoTab => Browser.Locate.ElementBySelector(_customerPhotoTabSelector);
        protected IElement MoreYouMayLikeContainer => Browser.Locate.ElementById(_moreYouMayLikeContainerId);
        protected override IElement NeedHelpLink => Browser.Locate.ElementById(_showProductHelpLinkId);
        protected override IElement NeedHelpModalCloseBtn => Browser.Locate.ElementByClassName(_needHelpModalCloseButtonClass);
        protected override IElement StickyPrice => Browser.Locate.ElementByClassName(_lblStickyPriceClass);
        protected override IElement StockCheckWrapper => Browser.Locate.ElementByTagNameAndClassName(HtmlTextWriterTag.Div, _shipsInMessageClass);
        protected override IElement LoadMoreReviews => Browser.Locate.ElementByXpath(LoadMoreReviewsBtnXpath);
        protected override IElement TopContentProductDetail => Browser.Locate.ElementById(DivProductDetailTop);
        protected override IElement ProductReviewsSection => Browser.Locate.ElementByXpath(_productReviewsSectionXpath);
        protected override IElement TurnToQuestionsAndAnswersSection => Browser.Locate.ElementByClassName(TurnToQuestionAndAnswerSection);
        protected override IElement TurnToReviewSection => Browser.Locate.ElementByClassName(TurnToReviewsSection);
        protected override IElement BuildFullSystemAddToCartButton => Browser.Locate.ElementById(_pdAddToCartBuildFullId);
        protected override IElement StickyWrapper => Browser.Locate.ElementById(_pdpStickyHeaderId);
        protected override IElement BuildFullSystemAddToWishListButton => Browser.Locate.ElementById(PdAddToPortfolioSystemOptionsId);
        protected override IElement BuildFullSystemButton => Browser.Locate.ElementById(PdViewFullTrackSystemId);
        protected override IElement CustomerPhotos => Browser.Locate.ElementByXpath(_customerPhotosXpath);
        protected override IElement MediaModalContentModal => Browser.Locate.ElementByClassName(_overlayContentWrapperClass);
        protected override IElement MoreImages(int index) => Browser.Locate.ElementBySelector($"{_thumbnailImageCarouselId.ToCssIdSelector()} > {HtmlTextWriterTag.Div.ToNthChildSelector(index)} > {HtmlTextWriterTag.Div}");
        protected override IElement ReplacementPartLink => Browser.Locate.ElementById(_replacementPartLinkId);
        protected override IElement ProductSpecificationsTable => Browser.Locate.ElementById(_productSpecificationTablesId);
        protected override IElement MakeAppointmentBtn => Browser.Locate.ElementByClassName(_inHomeConsultationAppointmentClass);
        protected override ReadOnlyCollection<IElement> BuildFullSystemShortSkuLinks => Browser.Locate.ElementsBySelector($"{HtmlTextWriterTag.Ul} {HtmlTextWriterTag.Li}", BuildFullSystemProductContainer);
        protected override ReadOnlyCollection<IElement> BuildFullSystemQtyElements => Browser.Locate.ElementsByClassName(SystemOptionsQtyClass);
        protected override ReadOnlyCollection<IElement> ThumbnailImages => Browser.Locate.ElementsByClassName(_thumbnailImageCarouselClass);

        public ProductDetailMobile(IBrowser browser, ProductActions productActions, IAssert assert, OperatingSystem operatingSystem, IModalDesktop modal) : base(browser, productActions, assert, operatingSystem, modal) { }

        //Interface implementation 
        public bool IsCustomerServiceNumberVisible() => Browser.Locate.ElementByXpath(_mobileCallXpath, nativeContext: true).IsInitialized;
        public bool IsChatIconVisible => Browser.Locate.ElementsByXpath(_chatXpath).Count > 0;
        public bool IsDrawerNameVisibleInViewport() => Browser.Locate.IsVisibleInViewport(CertonaDrawerName);
        public override bool IsNeedHelpModalVisible => Browser.Wait.IsVisibleElement(By.Id(_productStoreInfoId));
        public override bool IsNeedHelpModalChatVisible => Browser.Locate.ElementsByClassName(_chatBtnClass).Count > 0;
        public override bool IsCallCustomerServiceBlockVisible => Browser.Wait.IsVisibleElement(By.CssSelector(_pnlPleaseCallCustomerServiceClass.ToCssClassSelector()));
        public override bool IsOpenBoxVerbiageVisibleOnStickyHeader => Browser.Wait.IsVisibleElement(By.ClassName(_pdpStickyHeaderPriceTypeClass));

        public override Dictionary<string, int> AddAllBuildFullSystemSkusToWishList(int qtyCtr)
        {
            Browser.Wait.IsVisibleElement(By.XPath(_buildFullSystemDrawerXpath));
            BuildFullSystemDrawer.Click();

            var buildFullSystemQtyFields = BuildFullSystemQtyElements;
            var buildFullSystemShortSkuLinks = BuildFullSystemImage;
            var addedProducts = new Dictionary<string, int>();

            for (var i = 0; i < buildFullSystemQtyFields.Count; i++)
            {
                var qtyField = buildFullSystemQtyFields[i];
                var shortSku = buildFullSystemShortSkuLinks[i].GetAttribute("data-sku");
                Browser.ScrollIntoView(qtyField, true);
                qtyField.Clear();
                qtyField.SendKeys(qtyCtr.ToString());
                addedProducts.Add(shortSku, qtyCtr);

                // cycle through qty 1 - 5 for variation
                qtyCtr = qtyCtr == 5 ? 1 : qtyCtr + 1;
            }

            Browser.ExecuteJs("arguments[0].style.display = 'none';", StickyWrapper.InternalElement);

            BuildFullSystemAddToWishListButton.Click();

            return addedProducts;
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

                Browser.ScrollIntoView(qtyField, true);
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

        public void ToggleTurnToQuestionsAndAnswersSection()
        {
            ProductQuestionAnswerArrow.Click();
            Browser.Wait.ForDisplayedElement(TurnToQuestionsAndAnswersSection);
        }

        public override string GetProductSku()
        {
            return Browser.Locate.ElementBySelector(ProductSkuId.ToCssIdSelector()).GetAttribute(ContentString);
        }

        public override IElement GetTurnToReviewSection()
        {
            Browser.ScrollIntoView(TurnToReviewSection);
            Browser.ScrollToByPixelsVertical("-20");
            return TurnToReviewSection;
        }

        public override void OpenReviewsModal()
        {
            Browser.ScrollIntoView(ProductReviewsSection);
            WriteReviewBtn.Click();
        }

        public void ToggleProductReviewsSection()
        {
            ProductReviewsSection.Click();
        }

        public void DisplayProductHelpLink()
        {
            Browser.ScrollIntoView(Price);
            Browser.Wait.IsVisibleElement(By.Id(_showProductHelpLinkId));
        }

        public override void CloseNeedHelpModal()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_needHelpModalCloseButtonClass));
            NeedHelpModalCloseBtn.Click();
        }

        public override void OpenProductHelpAndStoreAvailabilityModal()
        {
            Browser.Wait.IsVisibleElement(By.Id(_showProductHelpLinkId));
            NeedHelpLink.Click();
            Browser.Wait.IsVisibleElement(By.Id(_showProductHelpModalId));
        }

        public override void OpenReviewPhotoModal()
        {
            Browser.Wait.ForDomReady();
            Browser.ScrollToTopOfWindow();
            Browser.Wait.ForDisplayedElement(AddToWishListButton);

            Browser.ScrollToByPixelsVertical("-300");
            Browser.ScrollToByPixelsVertical("-300");
            Browser.Wait.ForClickableElement(ProductReviewsSection);
            Browser.ScrollIntoView(ProductReviewsSection, true);
            ProductReviewsSection.Click();
            Browser.Wait.IsVisibleElement(By.ClassName(_productReviewRatingStarCountClass));

            Browser.Wait.IsVisibleElement(By.XPath(LoadMoreReviewsBtnXpath));
            Browser.Wait.ForClickableElement(LoadMoreReviews).Click();

            Browser.ScrollIntoView(ProductReviewCard(0), true);
            Browser.Wait.ForDisplayedElement(ProductReviewCard(0));

            ProductReviewCard(0).Click();
        }

        public string GetProductPriceText()
        {
            Browser.Wait.ForPage(Browser.PageUrl);

            return TextActions.GetPriceTextOnly(Price.Text);
        }

        public override void AddProductMaxQuantity()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_limitedQtyFieldClass.ToCssClassSelector()));
            Browser.Wait.ForDomReady();

            LimitedQtyField.Click();

            Browser.Wait.ForDomReady();
            Browser.SwitchToCurrentWindow();

            Browser.Wait.IsVisibleElement(By.XPath(_quantityDrawerXpath));
            Browser.ExecuteJs("arguments[0].click()", MobileMaxQuantity.InternalElement);

            Browser.Wait.ForElementToStopAnimating(MobileMaxQuantity);
        }

        public override List<string> GetFreeShippingProductsSkus(List<string> listOfLinks)
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

        public override string GetBreadcrumbText()
        {
            return BreadCrumbTrail.Text;
        }

        public override void FocusCompleteLookSection()
        {
            Browser.ScrollIntoView(CompleteTheLook, alignToBottom: true);
            Browser.Wait.ForClickableElement(CompleteTheLook);
            CompleteTheLook.Click();
        }

        public override void ChangeProductQuantity(string quantity)
        {
            Browser.Wait.IsVisibleElement(By.Id(_qtyNormalInputId));
            QuantityField.Click();
            Browser.ExecuteJs($"document.querySelector('#QtyNormal').value='{quantity}';");
        }

        public override void ClickOnViewInYourRoom()
        {
            Browser.Wait.ForDisplayedElement(ViewInYourRoomBtn);
            Browser.ClickByJs(ViewInYourRoomBtn);
        }

        public override List<IElement> GetStickyNavContents()
        {
            var contentsItems = new List<IElement>
            {
                StickyImage,
                StickyPrice,
                StickyAddToCart
            };

            return contentsItems;
        }

        public void OpenCallDialog()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_haveAQuestionCallContainerClass));
            Browser.Locate.ElementByClassName(_haveAQuestionCallContainerClass).Click();
        }

        public void OpenChat()
        {
            Browser.Wait.IsVisibleElement(By.XPath(_chatXpath));
            ChatButton(0).Click();
        }

        public void CloseChatAssistant()
        {
            AssistantCloseIcon.Click();
        }

        public IElement GetHaveAQuestionSection()
        {
            Browser.RefreshPage();
            Browser.ScrollIntoView(MoreYouLikeHeader, alignToBottom: true);
            return HaveAQuestionContent;
        }

        public void ConfirmClosingOfChatAssistant()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_confirmationDialogButtonYesClass.ToCssClassSelector()));
            ConfirmationCloseButton.Click();
        }

        public bool IsChatModalVisible()
        {
            return Browser.Wait.IsVisibleElement(By.ClassName(_widgetFloatingHeaderClass));
        }

        public bool AreRelatedItemsVisible()
        {
            return Browser.Wait.AreAllElementsVisible(By.CssSelector(_pdRelatedItemsClass.ToCssClassSelector()));
        }

        public override void SelectThumbnailImage(int indexOfThumbnail)
        {
            MoreImages(indexOfThumbnail).Click();
        }

        public override void OpenCustomerPhotosTab()
        {
            ZoomIcon.Click();
            Browser.Wait.ForDisplayedElement(CustomerPhotos);
            CustomerPhotos.Click();
        }

        public void OpenBuildFullSystemDrawer()
        {
            Browser.Wait.IsVisibleElement(By.XPath(_buildFullSystemDrawerXpath));
            BuildFullSystemDrawer.Click();
            Browser.ScrollToTopOfWindow();
        }

        public void OpenProductDetailsDrawer()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_productDescId.ToCssIdSelector()));
            Browser.Wait.ForClickableElement(ProductDescDropDown).Click();
            Browser.Wait.ForDomReady();
            Browser.ScrollToTopOfWindow();
        }

        public void FocusCustomerReviewsSection()
        {
            Browser.ScrollIntoView(SearchReviewField);
            Browser.ScrollToByPixelsVertical("-80");
        }

        public override void OpenBulbAndReplacementPartsModal()
        {
            Browser.Wait.ForClickableElement(ReplacementPartLink).Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(_addReplacementPartsReplacementPartsModalClass.ToCssClassSelector()));
        }

        public void OpenSpecificationTableDrawer()
        {
            Browser.ScrollToTopOfWindow();
            Browser.ScrollIntoView(ProductSpecificationsTable);
            Browser.ExecuteJs("window.scrollBy(0,-100)");
            ProductSpecificationsTable.Click();
            Browser.Wait.ForDisplayedElement(ProductAttributes);
        }

        public override string GetPayPalCalloutPDP()
        {
            var actualString = string.Empty;

            if (Browser.Device != null && Browser.Device.IsIphone)
            {
                ((IphoneBrowser)Browser).SwitchToNativeContext((AppiumDriver<AppiumWebElement>)Browser.Driver); //Switch to iOS Native context;

                actualString = Browser.Locate.ElementByXpath("//XCUIElementTypeButton[contains(@name, 'pay') or contains(@name, 'low')]").Text;
                actualString = actualString.StartsWith("Pay") ? actualString.Replace("Learn more", "with PayPal. Learn more") : actualString.Replace(" month .", "mo.").Replace("Learn more", "with PayPal. Learn more");
                
                ((IphoneBrowser)Browser).SwitchToWebViewContext((AppiumDriver<AppiumWebElement>)Browser.Driver); //Switch to iOS WebView context;
            }
            else
            {
                Browser.ScrollIntoView(Browser.Locate.ElementBySelector(AllowtransparencySelector), true);
                Browser.SwitchFocusToIframe(Browser.Locate.ElementBySelector(AllowtransparencySelector));

                var payPalVerbiage = Browser.Locate.ElementByClassName(TagMediumClass).Text;
                var getWithFromVerbiage = Browser.ExecuteJs("return window.getComputedStyle(document.querySelector(arguments[0]), ':before').getPropertyValue('content');",
                    MessageLogoContainerSelector).ToString().Remove(5).Substring(1);
                var getDotFromVerbiage = Browser.ExecuteJs("return window.getComputedStyle(document.querySelector(arguments[0]), ':after').getPropertyValue('content');",
                    MessageLogoContainerSelector).ToString().Remove(2).Substring(1);
                var payPalTxt = Browser.Locate.ElementBySelector(MessageLogoContainerSelector).GetAttribute("alt");
                var learnMoreText = Browser.Locate.ElementByClassName(MessageDisclaimerClass).Text;

                if (learnMoreText.Contains("\r\n"))
                {
                    var learnMoreTxt = learnMoreText.Substring(0, learnMoreText.IndexOf("\r\n"));
                    actualString = payPalVerbiage + " " + getWithFromVerbiage + " " + payPalTxt +
                                   getDotFromVerbiage + " " + learnMoreTxt;
                }
                else
                {
                    actualString = payPalVerbiage + " " + getWithFromVerbiage + " " + payPalTxt +
                                   getDotFromVerbiage + " " + learnMoreText;
                }

                Browser.SwitchToDefaultContent();
            }

            return actualString;
        }
        public IElement GetCertonaDrawerName()
        {
            return CertonaDrawerName;
        }

        public override void OpenEnergyGuide()
        {
            EnergyGuideIcon.Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(EnergyGuideIconId.ToCssIdSelector()));
            Browser.ScrollToTopOfWindow();
        }

        public override void OpenMakeAnAppointmentModal()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_inHomeConsultationAppointmentClass));
            MakeAppointmentBtn.Click();
        }
    }
}
