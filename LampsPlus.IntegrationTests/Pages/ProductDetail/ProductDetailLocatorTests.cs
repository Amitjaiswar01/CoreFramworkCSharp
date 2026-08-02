using Automation.Framework;

using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.IntegrationTests.Pages.ProductDetail
{
    public class ProductDetailLocatorDesktopTest : ProductDetailLocatorTests
    {
        public ProductDetailLocatorDesktopTest(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Integration.PageObjectModel, "ProductDetail")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LocateProductDetailElementsTest(string config) => Locate(config);

        protected override void CheckProfessionalPdp()
        {
            // Professional - PCSI
            SignInWorkflow.SignInFromHeader(SignInWorkflow.GetUserAccount(UserRolesTypes.Professional));

            ProductDetail.NavigateToProductDetailByShortSku(ProductActions.GetTradePriceInfo().ShortSku);

            VerifyElementDisplayed(() => ProductDetail.TradePriceLabel);
            VerifyElementDisplayed(() => ProductDetail.TradeSavingsPrice);

            SignInWorkflow.SignOut();
        }

        protected override void CheckStickyArea()
        {
            VerifyElementDisplayed(() => ProductDetail.StickyTitle);
            VerifyElementDisplayed(() => ProductDetail.StickyPrice);
            VerifyElementNotImplemented(() => ProductDetail.StickyImageWrapper);
        }

        protected override void CheckCommonPdpElements()
        {
            VerifyElementDisplayed(() => ProductDetail.TopContentProductDetail);
            VerifyElementDisplayed(() => ProductDetail.ProductDetailPageContainer);
            VerifyElementDisplayed(() => ProductDetail.StockCheckElement);
            VerifyElementDisplayed(() => ProductDetail.StockCheckWrapper);
            VerifyElementDisplayed(() => ProductDetail.PdSocialIconElement);
            VerifyElementDisplayed(() => ProductDetail.PdProdInfoColElement);
            VerifyElementDisplayed(() => ProductDetail.ProductSlider);
            VerifyElementDisplayed(() => ProductDetail.BrandLogo);
            VerifyElementDisplayed(() => ProductDetail.BrandLogoLink);
            VerifyElementDisplayed(() => ProductDetail.ManufacturerLink);
            VerifyElementDisplayed(() => ProductDetail.ManufacturerLinkAnchor);
            VerifyElementDisplayed(() => ProductDetail.ProductSliders);
            VerifyElementDisplayed(() => ProductDetail.PdSocialPrintIconElement);
            VerifyElementNotImplemented(() => ProductDetail.ProductDescriptionAccordion);
            VerifyElementNotImplemented(() => ProductDetail.PdpAddToWishlist);
        }

        protected override void TestEmailAFriendWebElements()
        {
            VerifyElementDisplayed(() => ProductDetail.EmailLink);

            Browser.Wait.ForClickableElement(ProductDetail.EmailLink).Click();

            VerifyElementDisplayed(() => ProductDetail.EmailModalContent);
            VerifyElementDisplayed(() => ProductDetail.EmailRecipientTextbox);
            VerifyElementDisplayed(() => ProductDetail.FirstNameTextbox);
            VerifyElementDisplayed(() => ProductDetail.LastNameTextbox);
            VerifyElementDisplayed(() => ProductDetail.FromEmailTextbox);
            VerifyElementDisplayed(() => ProductDetail.ZipcodeTextbox);
            VerifyElementDisplayed(() => ProductDetail.SendEmailButton);

            GlobalLocators.LpModalCloseElement.Click();
        }

        protected override void TestViewInYourRoomsWebElements()
        {
            VerifyElementNotImplemented(() => ProductDetail.GetYourPhotoFrame);
            ProductDetail.SwitchToModalIframe();
            Thread.Sleep(10000); // Popup takes lot of time to load, none of the other techniques working except Thread.Sleep
            VerifyElementDisplayed(() => ProductDetail.SampleRoomBtn);
            ProductDetail.SampleRoomBtn.Click();
            Browser.Wait.UntilElementUnloads(ProductDetail.SampleRoomBtn);
            Thread.Sleep(5000); // Popup takes lot of time to unload, none of the other techniques working except Thread.Sleep
            VerifyElementDisplayed(() => ProductDetail.SamplePhotos);
            VerifyElementDisplayed(() => ProductDetail.SamplePhotosTab);
        }

        protected override void TestTurnToWebElements()
        {
            VerifyElementDisplayed(() => ProductDetail.TurnToQuestionAndAnswerContainer);
            VerifyElementDisplayed(() => ProductDetail.TurnToWriteReviewButton);
            VerifyElementDisplayed(() => ProductDetail.TurnTwoBrowseQaWrapper);
            VerifyElementDisplayed(() => ProductDetail.TurnTwoDynamicAddAnswerButton);
            VerifyElementDisplayed(() => ProductDetail.QuestionsAndAnswersCommentsSection);
            Browser.Wait.ForClickableElement(ProductDetail.TurnTwoAskAQuestionTextArea).Click();

            VerifyElementDisplayed(() => ProductDetail.TurnTwoAskAQuestionCloseButton);
            ProductDetail.TurnTwoAskAQuestionCloseButton.Click();

            Browser.Wait.ForElement(ProductDetail.TurnTwoDynamicAddAnswerButton);
            Browser.MouseOverOnElement(ProductDetail.TurnTwoDynamicAddAnswerButton);
            Browser.Wait.ForClickableElement(ProductDetail.TurnTwoDynamicAddAnswerButton).Click();

            VerifyElementDisplayed(() => ProductDetail.TurnTwoDynamicAddAnswerTextArea);
            VerifyElementDisplayed(() => ProductDetail.TurnTwoDynamicAddQuestionsCancelButton);
            VerifyElementDisplayed(() => ProductDetail.TurnTwoAskAQuestionTextArea);
            VerifyElementDisplayed(() => ProductDetail.PdReviewsElement);
            VerifyElementDisplayed(() => ProductDetail.SingleQuestionAndAnswerElementResult);
            VerifyElementNotDisplayed(() => ProductDetail.ProductReviewModal);
        }

        protected override void TestTurnToReviewWebElements()
        {
            ProductDetail.ClickTurnToWriteReview();
            VerifyElementDisplayed(() => ProductDetail.TurnToReviewScreen);
            VerifyElementDisplayed(() => ProductDetail.TurnToReviewProductName);
            VerifyElementDisplayed(() => ProductDetail.TurnToReviewRating);
            VerifyElementDisplayed(() => ProductDetail.TurnToReviewTitle);
            VerifyElementDisplayed(() => ProductDetail.TurnToReviewText);
            VerifyElementDisplayed(() => ProductDetail.TurnToReviewAttachPhoto);
            VerifyElementDisplayed(() => ProductDetail.TurnToReviewWindow);
            Thread.Sleep(1000);
            VerifyElementDisplayed(() => ProductDetail.TurnToReviewProductImage);

            Browser.Wait.ForClickableElement(ProductDetail.TurnToReviewAttachPhoto).Click();

            VerifyElementDisplayed(() => ProductDetail.TurnToReviewShareMediaScreen);
            ProductDetail.TurnToReviewFileInput.SendKeys(FileUpload.TurnToReviewPhotoUploadPath);
            VerifyElementDisplayed(() => ProductDetail.TurnToReviewFileMediaListSelected);
            VerifyElementNotDisplayed(() => ProductDetail.TurnToReviewFileInput);
            VerifyElementDisplayed(() => ProductDetail.TurnToReviewAddNewPhotoButton);
            VerifyElementDisplayed(() => ProductDetail.TurnToReviewMediaSubmitButton);
        }

        protected override void CheckProductInStock()
        {
            VerifyElementDisplayed(() => ProductDetail.ProductInStockTextLink);
        }

        protected override void CheckLimitedQuantitySection()
        {
            VerifyElementDisplayed(() => ProductDetail.LimitedQtyField);
            VerifyElementNotImplemented(() => ProductDetail.LimitedQuantitySelection);
        }

        protected override void VerifyRelatedItemSection()
        {
            VerifyElementDisplayed(() => ProductDetail.RelatedItemAnchor);
            VerifyElementDisplayed(() => ProductDetail.RelatedItemsContainer);
            VerifyElementDisplayed(() => ProductDetail.RelatedItemsSection);
            VerifyElementDisplayed(() => ProductDetail.PdRelVideosContainer);
            VerifyElementDisplayed(() => ProductDetail.RelatedVideo);
            VerifyElementDisplayed(() => ProductDetail.PdRelItmsContainer);
            VerifyElementDisplayed(() => ProductDetail.RelatedItems);
            VerifyElementNotImplemented(() => ProductDetail.RelatedItemDropdown);
            VerifyElementNotImplemented(() => ProductDetail.RelatedItemSection);
        }

        protected override void VerifyProsSpecialPriceCallout()
        {
            VerifyElementDisplayed(() => ProductDetail.ProsSpecialPriceCallout);
        }

        protected override void VerifyEnergyGuideElements()
        {
            VerifyElementNotImplemented(() => ProductDetail.ProductDescDropDown);
            VerifyElementDisplayed(() => ProductDetail.EnergyGuideIcon);
            VerifyElementNotDisplayed(() => ProductDetail.EnergyInfoModal);
        }

        protected override void VerifyBuildFullSystemElements()
        {
            VerifyElementDisplayed(() => ProductDetail.BuildFullSystemQtyElements);
            VerifyElementDisplayed(() => ProductDetail.BuildFullSystemShortSkuLinks);
        }

        protected override void VerifyHousingOptionElements()
        {
            VerifyElementDisplayed(() => ProductDetail.HousingOptions);
            VerifyElementDisplayed(() => ProductDetail.HousingOptionsSectionHeader);
            VerifyElementDisplayed(() => ProductDetail.HousingOptionsSectionDivContainers);
        }

        protected override void TestChatWebElements()
        {
            Browser.Wait.ForDomReady();
            Browser.Wait.ForClickableElement(ProductDetail.PdChat);

            VerifyElementDisplayed(() => ProductDetail.ChatButtonLink);
            VerifyElementDisplayed(() => ProductDetail.PdChat);
            VerifyElementDisplayed(() => ProductDetail.QuestionsAndAnswersChatContainer);
            VerifyElementDisplayed(() => ProductDetail.QuestionsAndAnswersChatLink);
            VerifyElementDisplayed(() => ProductDetail.FooterChatLink);
            VerifyElementNotImplemented(() => ProductDetail.SocialLinksContainer);
        }

        protected override void StoreInSessionMode()
        {
            CookieUtility.EnterStoreInSessionMode();
            // Free shipping within state
            ProductDetail.NavigateToProductDetailByShortSku(ProductActions.GetShipsFreeWithinStateShortSku);

            VerifyElementDisplayed(() => ProductDetail.FreeShippingToStatesWithStoresLabel);

            ProductDetail.NavigateToProductDetailByShortSku(ProductActions.GetSkuWithStoreAssociateLink());
            VerifyElementDisplayed(() => ProductDetail.AskStoreAssociate);

            // sku with warehouse inventory
            ProductDetail.NavigateToProductDetailByShortSku(ProductActions.GetProductWithWarehouseInventory().ShortSku);

            VerifyElementDisplayed(() => ProductDetail.LblStockInventory);
            VerifyElementDisplayed(() => ProductDetail.StoreInventoryElement);

            ProductDetail.NavigateToProductDetailByShortSku(ProductActions.GetProductWithWarehouseInventory().ShortSku);

            VerifyElementDisplayed(() => ProductDetail.QuickPrintInput);

            CookieUtility.ExitStoreInSessionMode();
        }

        protected override void VerifyStoreAvailabilityElements()
        {
            VerifyElementNotImplemented(() => ProductDetail.StoreAvailabilityQuestions);
        }

        protected override void CheckCsrWebElements()
        {
            SignInWorkflow.SignIn(SignInWorkflow.GetUserAccount(UserRolesTypes.CustomerServiceManager));
            ProductDetail.NavigateToProductDetailByShortSku(ProductActions.GetProductWithSkuStatus().ShortSku);

            VerifyElementDisplayed(() => ProductDetail.CsInfo);
            VerifyElementDisplayed(() => ProductDetail.LongSkuElement);

            VerifyElementDisplayed(() => ProductDetail.MarginModalLink);

            // Store In Session Employee sign in 
            ProductDetail.NavigateToProductDetailByShortSku(ProductActions.GetProductWithSkuStatus().ShortSku);
            Home.EnterStoreInSession("12");

            VerifyElementDisplayed(() => ProductDetail.QuickPrintLink);

            ProductDetail.PdSocialPrintIconElement.Click();

            Browser.Wait.ForClickableElement(GlobalLocators.LpModalCloseElement);

            Browser.SwitchFocusToIframe(GlobalLocators.IframeModal);
            Thread.Sleep(2000); //Other Browser.Wait methods not working here.

            VerifyElementDisplayed(() => ProductDetail.PrintKioskStyleButtonElement);
            VerifyElementDisplayed(() => ProductDetail.PrintKioskStyleProductBtnElement);

            Browser.Navigate(Urls.HomePageUrl);

            SignInWorkflow.SignOut();
        }

        protected override void VerifyDistinctTrackLightingElements()
        {
            VerifyElementDisplayed(() => ProductDetail.BuildFullSystemAddToWishListButton);
            VerifyElementDisplayed(() => ProductDetail.BuildFullSystemAddToCartButton);
            VerifyElementDisplayed(() => ProductDetail.BuildFullSystemContainer);
            VerifyElementDisplayed(() => ProductDetail.BuildFullSystemOptions);
            VerifyElementDisplayed(() => ProductDetail.ListOfFullSystemProductNames);
            VerifyElementDisplayed(() => ProductDetail.ListOfFullSystemSkus);
        }
    }


    public class ProductDetailLocatorMobileTest : ProductDetailLocatorTests
    {
        public ProductDetailLocatorMobileTest(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Integration.PageObjectModel, "ProductDetail")]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LocateProductDetailElementsTest(string config) => Locate(config);

        protected override void CheckProfessionalPdp()
        {
            // Professional - PCSI
            SignInWorkflow.SignIn(SignInWorkflow.GetUserAccount(UserRolesTypes.Professional), true);

            ProductDetail.NavigateToProductDetailByShortSku(ProductActions.GetTradePriceInfo().ShortSku);

            VerifyElementDisplayed(() => ProductDetail.TradePriceLabel);
            VerifyElementDisplayed(() => ProductDetail.TradeSavingsPrice);

            HeaderFooter.HamburgerMenu.Click();
            Thread.Sleep(1000);
            Browser.Wait.ForClickableElement(HeaderFooter.SignOutLink).Click();
        }

        protected override void CheckStickyArea()
        {
            VerifyElementDisplayed(() => ProductDetail.StickyImageWrapper);

            VerifyElementNotImplemented(() => ProductDetail.StickyTitle);
            VerifyElementNotImplemented(() => ProductDetail.StickyPrice);
        }

        protected override void CheckCommonPdpElements()
        {
            VerifyElementDisplayed(() => ProductDetail.PdpAddToWishlist);
            VerifyElementDisplayed(() => ProductDetail.StockCheckWrapper);
            VerifyElementDisplayed(() => ProductDetail.StockCheckElement);
            VerifyElementDisplayed(() => ProductDetail.ProductDescriptionAccordion);
            VerifyElementNotDisplayed(() => ProductDetail.BrandLogo);
            VerifyElementNotDisplayed(() => ProductDetail.BrandLogoLink);
            VerifyElementNotImplemented(() => ProductDetail.ProductDetailPageContainer);
            VerifyElementNotImplemented(() => ProductDetail.PdSocialIconElement);
            VerifyElementNotImplemented(() => ProductDetail.PdProdInfoColElement);
            VerifyElementNotImplemented(() => ProductDetail.ProductSlider);
            VerifyElementsNotImplemented(() => ProductDetail.ProductSliders);
            VerifyElementNotImplemented(() => ProductDetail.PdSocialPrintIconElement);
            VerifyElementNotImplemented(() => ProductDetail.TopContentProductDetail);

            Thread.Sleep(2000);
            ProductDetail.ProductDescriptionAccordion.Click();
            Browser.Wait.ForElementToStopAnimating(ProductDetail.ProductDescriptionAccordion);

            VerifyElementDisplayed(() => ProductDetail.ManufacturerLink);
            VerifyElementDisplayed(() => ProductDetail.ManufacturerLinkAnchor);
        }

        protected override void TestEmailAFriendWebElements()
        {
            VerifyElementNotImplemented(() => ProductDetail.EmailLink);
            VerifyElementNotImplemented(() => ProductDetail.EmailModalContent);
            VerifyElementNotImplemented(() => ProductDetail.EmailRecipientTextbox);
            VerifyElementNotImplemented(() => ProductDetail.FirstNameTextbox);
            VerifyElementNotImplemented(() => ProductDetail.LastNameTextbox);
            VerifyElementNotImplemented(() => ProductDetail.FromEmailTextbox);
            VerifyElementNotImplemented(() => ProductDetail.ZipcodeTextbox);
            VerifyElementNotImplemented(() => ProductDetail.SendEmailButton);
        }

        protected override void TestViewInYourRoomsWebElements()
        {
            Browser.Wait.ForDisplayedElement(ProductDetail.GetYourPhotoFrame);

            VerifyElementDisplayed(() => ProductDetail.GetYourPhotoFrame);
            VerifyElementNotImplemented(() => ProductDetail.SampleRoomBtn);
            VerifyElementsNotImplemented(() => ProductDetail.SamplePhotos);
            VerifyElementNotImplemented(() => ProductDetail.SamplePhotosTab);
        }

        protected override void TestTurnToWebElements()
        {
            VerifyElementNotDisplayed(() => ProductDetail.TurnToWriteReviewButton);
            VerifyElementDisplayed(() => ProductDetail.TurnTwoBrowseQaWrapper);

            ProductDetail.TurnTwoBrowseQaWrapper.Click();

            VerifyElementDisplayed(() => ProductDetail.TurnTwoDynamicAddAnswerButton);
            VerifyElementDisplayed(() => ProductDetail.TurnTwoAskAQuestionTextArea);
            VerifyElementDisplayed(() => ProductDetail.PdReviewsElement);
            VerifyElementDisplayed(() => ProductDetail.SingleQuestionAndAnswerElementResult);

            VerifyElementNotDisplayed(() => ProductDetail.TurnTwoDynamicAddAnswerTextArea);
            VerifyElementNotDisplayed(() => ProductDetail.TurnTwoDynamicAddQuestionsCancelButton);
            VerifyElementNotDisplayed(() => ProductDetail.TurnTwoAskAQuestionCloseButton);

            VerifyElementNotImplemented(() => ProductDetail.ProductReviewModal);
            VerifyElementNotImplemented(() => ProductDetail.QuestionsAndAnswersCommentsSection);
        }

        protected override void TestTurnToReviewWebElements()
        {
            VerifyElementNotImplemented(() => ProductDetail.TurnToReviewScreen);

            ProductDetail.ClickTurnToWriteReview();

            VerifyElementDisplayed(() => ProductDetail.TurnToReviewProductName);
            VerifyElementDisplayed(() => ProductDetail.TurnToReviewProductImage);
            VerifyElementDisplayed(() => ProductDetail.TurnToReviewRating);
            VerifyElementDisplayed(() => ProductDetail.TurnToReviewTitle);
            VerifyElementDisplayed(() => ProductDetail.TurnToReviewText);
            VerifyElementNotImplemented(() => ProductDetail.TurnToQuestionAndAnswerContainer);
            VerifyElementNotImplemented(() => ProductDetail.TurnToReviewShareMediaScreen);
            VerifyElementNotImplemented(() => ProductDetail.TurnToReviewFileMediaListSelected);
            VerifyElementNotImplemented(() => ProductDetail.TurnToReviewFileInput);
            VerifyElementNotImplemented(() => ProductDetail.TurnToReviewAddNewPhotoButton);
            VerifyElementNotImplemented(() => ProductDetail.TurnToReviewMediaSubmitButton);
            VerifyElementNotImplemented(() => ProductDetail.TurnToReviewAttachPhoto);
            VerifyElementDisplayed(() => ProductDetail.TurnToReviewWindow);
            ProductDetail.TurnToReviewWindow.Click();
            Browser.SwitchToTabByIndex(0);
        }

        protected override void CheckProductInStock()
        {
            VerifyElementNotImplemented(() => ProductDetail.ProductInStockTextLink);
        }

        protected override void CheckLimitedQuantitySection()
        {
            VerifyElementDisplayed(() => ProductDetail.LimitedQtyField);
            ProductDetail.LimitedQtyField.Click();
            VerifyElementDisplayed(() => ProductDetail.LimitedQuantitySelection);
        }

        protected override void VerifyRelatedItemSection()
        {
            VerifyElementDisplayed(() => ProductDetail.RelatedItemDropdown);

            ProductDetail.RelatedItemDropdown.Click();

            VerifyElementDisplayed(() => ProductDetail.RelatedItemSection);
            VerifyElementDisplayed(() => ProductDetail.PdRelItmsContainer);
            VerifyElementDisplayed(() => ProductDetail.RelatedItems);

            ProductDetail.RelatedItemSection.Click();
            
            VerifyElementDisplayed(() => ProductDetail.RelatedItemsContainer);
            VerifyElementDisplayed(() => ProductDetail.RelatedItemAnchor);

            ProductDetail.ProductDescriptionAccordion.Click();
            Browser.Wait.ForElementToStopAnimating(ProductDetail.ProductDescriptionAccordion);

            VerifyElementDisplayed(() => ProductDetail.PdRelVideosContainer);
            VerifyElementDisplayed(() => ProductDetail.RelatedVideo);

            VerifyElementNotImplemented(() => ProductDetail.RelatedItemsSection);
        }

        protected override void VerifyProsSpecialPriceCallout()
        {
            VerifyElementDisplayed(() => ProductDetail.ProsSpecialPriceCallout);
        }

        protected override void VerifyEnergyGuideElements()
        {
            VerifyElementDisplayed(() => ProductDetail.ProductDescDropDown);
            VerifyElementNotDisplayed(() => ProductDetail.EnergyGuideIcon);
            VerifyElementNotDisplayed(() => ProductDetail.EnergyInfoModal);
        }

        protected override void VerifyBuildFullSystemElements()
        {
            VerifyElementsNotImplemented(() => ProductDetail.BuildFullSystemQtyElements);
            VerifyElementsNotImplemented(() => ProductDetail.BuildFullSystemShortSkuLinks);
        }

        protected override void VerifyHousingOptionElements()
        {
            VerifyElementNotImplemented(() => ProductDetail.HousingOptions);
            VerifyElementNotImplemented(() => ProductDetail.HousingOptionsSectionHeader);
            VerifyElementsNotImplemented(() => ProductDetail.HousingOptionsSectionDivContainers);
        }

        protected override void TestChatWebElements()
        {
            VerifyElementNotImplemented(() => ProductDetail.ChatButtonLink);
            VerifyElementNotImplemented(() => ProductDetail.PdChat);
            VerifyElementNotImplemented(() => ProductDetail.QuestionsAndAnswersChatContainer);
            VerifyElementNotImplemented(() => ProductDetail.QuestionsAndAnswersChatLink);
            VerifyElementNotImplemented(() => ProductDetail.FooterChatLink);
            VerifyElementDisplayed(() => ProductDetail.SocialLinksContainer);
        }

        protected override void StoreInSessionMode()
        {
            VerifyElementNotImplemented(() => ProductDetail.FreeShippingToStatesWithStoresLabel);
            VerifyElementNotImplemented(() => ProductDetail.LblStockInventory);
            VerifyElementNotImplemented(() => ProductDetail.StoreInventoryElement);
            VerifyElementNotImplemented(() => ProductDetail.QuickPrintInput);
            VerifyElementNotImplemented(() => ProductDetail.AskStoreAssociate);
        }

        protected override void VerifyStoreAvailabilityElements()
        {
            VerifyElementDisplayed(() => ProductDetail.StoreAvailabilityQuestions);
        }

        protected override void CheckCsrWebElements()
        {
            VerifyElementNotImplemented(() => ProductDetail.CsInfo);
            VerifyElementNotImplemented(() => ProductDetail.LongSkuElement);
            VerifyElementNotImplemented(() => ProductDetail.MarginModalLink);
            VerifyElementNotImplemented(() => ProductDetail.QuickPrintLink);
            VerifyElementNotImplemented(() => ProductDetail.PrintKioskStyleButtonElement);
            VerifyElementNotImplemented(() => ProductDetail.PrintKioskStyleProductBtnElement);
        }

        protected override void VerifyDistinctTrackLightingElements()
        {
            VerifyElementNotImplemented(() => ProductDetail.BuildFullSystemAddToWishListButton);
            VerifyElementNotImplemented(() => ProductDetail.BuildFullSystemAddToCartButton);
            VerifyElementNotImplemented(() => ProductDetail.BuildFullSystemContainer);
            VerifyElementNotImplemented(() => ProductDetail.BuildFullSystemOptions);
            VerifyElementsNotImplemented(() => ProductDetail.ListOfFullSystemProductNames);
            VerifyElementsNotImplemented(() => ProductDetail.ListOfFullSystemSkus);
        }
    }


    /// <summary>
    /// Tests to ensure all IElements and Lists of IElements can be found on the given page object.
    /// </summary>
    [Trait(LpTraits.Integration.PageObjectModel, "ProductDetail")]
    public abstract class ProductDetailLocatorTests : PageObjectTestsBase
    {
        protected ProductDetailLocatorTests(ITestOutputHelper output) : base(output) { }

        public void Locate(string config)
        {
            InitializeFramework(config);

            BuildElementsList(ProductDetail);

            CheckProfessionalPdp();

            ProductDetail.NavigateToProductDetailByShortSku(ProductActions.GetItemNotOnSale);

            Browser.ScrollToBottomOfWindow();
            Browser.Wait.ForElementToStopAnimating(ProductDetail.StickyWrapper);

            VerifyElementDisplayed(() => ProductDetail.StickyWrapper);
            VerifyElementDisplayed(() => ProductDetail.StickyImage);
            VerifyElementDisplayed(() => ProductDetail.StickyAddToCart);
            VerifyElementDisplayed(() => ProductDetail.AddToCartLabelElement);

            // Testing common web elements of PDP
            CheckStickyArea();

            ProductDetail.ForceHideStickyHeader();

            VerifyElementDisplayed(() => ProductDetail.AddToWishListButton);

            VerifyElementDisplayed(() => ProductDetail.Price);
            VerifyElementDisplayed(() => ProductDetail.ProductSkuLabel);
            VerifyElementDisplayed(() => ProductDetail.QuantityField);
            VerifyElementDisplayed(() => ProductDetail.ItemPrice);
            VerifyElementDisplayed(() => ProductDetail.BreadCrumbElement);
            VerifyElementDisplayed(() => ProductDetail.ProductImage);

            ProductDetail.AddToWishListButton.Click();
            Browser.Wait.ForDomReady();

            CheckCommonPdpElements();

            TestEmailAFriendWebElements();

            ProductDetail.NavigateToProductDetailByShortSku(ProductActions.GetSkuWithViewInRoomOnPdp);

            Browser.Wait.ForDomReady();
            VerifyElementDisplayed(() => ProductDetail.ShowInRoomBtn);

            ProductDetail.ClickViewInYourRoomJs();

            TestViewInYourRoomsWebElements();

            SortWorkflow.VisitMostPopularLampProductThatHasQuestionsAndAnswers();
            Browser.Wait.ForDomReady();

            TestTurnToWebElements();

            ProductDetail.NavigateToProductDetailByShortSku(ProductActions.GetSkuThatQualifiesForReviews);
            Browser.Wait.ForDomReady();

            TestTurnToReviewWebElements();

            // Single SKU
            ProductDetail.NavigateToProductDetailByShortSku(ProductActions.GetSingleSku().ShortSku);

            CheckProductInStock();

            // SKU with quantity call out
            ProductDetail.NavigateToProductDetailByShortSku(ProductActions.GetProductWithLimitedInventory().Sku);

            VerifyElementDisplayed(() => ProductDetail.ProductQtyCallOut);

            CheckLimitedQuantitySection();

            // SKU with 'ShipsFreeOnOrdersOver49' call out for single product
            ProductDetail.NavigateToProductDetailByShortSku(ProductActions.GetShipsFreeOnOrdersOver49CallOutShortSku);

            VerifyElementDisplayed(() => ProductDetail.ShipsFreeWithOrdersOver49CallOut);

            // SKU with related items
            ProductDetail.NavigateToProductDetailByShortSku(ProductActions.GetAnySkuWithRelatedVideos);
            Browser.Wait.ForDomReady();
            
            VerifyRelatedItemSection();

            // SKU on clearance
            ProductDetail.NavigateToProductDetailByShortSku(ProductActions.GetShortSkuOnClearance);

            VerifyElementDisplayed(() => ProductDetail.ProductCallOut);

            // SKU with check store availability link
            ProductDetail.NavigateToProductDetailByShortSku(ProductActions.GetItemsThatHaveCheckStoreAvailabilityLinkOnProductDetailPage());

            VerifyElementDisplayed(() => ProductDetail.StoreAvailability);

            VerifyStoreAvailabilityElements();

            // SKU with member special price discount call out
            ProductDetail.NavigateToProductDetailByShortSku(ProductActions.GetProMemberSpecialPriceDiscountCallOutShortSku);

            VerifyProsSpecialPriceCallout();

            // Fan sku
            ProductDetail.NavigateToProductDetailByShortSku(ProductActions.GetFanWithEnergyGuideIconShortSku);

            VerifyEnergyGuideElements();

            // Build full system SKU
            ProductDetail.NavigateToProductDetailByShortSku(ProductActions.GetProductWithBuildFullSystemSkus().Keys.First());

            VerifyBuildFullSystemElements();

            // SKU with housing options
            ProductDetail.NavigateToProductDetailByShortSku(ProductActions.GetSkuThatHasHousingOptions);

            VerifyHousingOptionElements();

            // SKU on sale with compare price
            ProductDetail.NavigateToProductDetailByShortSku(ProductActions.GetLpProductOnSaleWithComparePrice().ShortSku);

            VerifyElementDisplayed(() => ProductDetail.ComparePriceCallout);

            // SKU saves under $1
            ProductDetail.NavigateToProductDetailByShortSku(ProductActions.GetSkuSavePriceUnderOne.ShortSku);

            VerifyElementDisplayed(() => ProductDetail.PriceType);

            // SKU saves $1 and over
            ProductDetail.NavigateToProductDetailByShortSku(ProductActions.GetSkuSavePriceOneAndOver.ShortSku);

            Browser.Wait.ForDisplayedElement(ProductDetail.OrigPrice);
            VerifyElementDisplayed(() => ProductDetail.OrigPrice);
            VerifyElementDisplayed(() => ProductDetail.PriceAdditionalSave);

            // Free shipping and Return
            ProductDetail.NavigateToProductDetailByShortSku(ProductActions.GetFreeShippingAndReturnShortSkus); 
            Browser.Wait.ForDomReady();
            
            VerifyElementDisplayed(() => ProductDetail.FreeShippingAndReturnElement);

            // Free Shipping only
            ProductDetail.NavigateToProductDetailByShortSku(ProductActions.GetFreeShippingProduct().ShortSku);

            VerifyElementDisplayed(() => ProductDetail.FreeShippingCallout);

            ProductDetail.NavigateToProductDetailByShortSku(ProductActions.GetTrackLightingProductCase1().PrimarySku);

            VerifyDistinctTrackLightingElements();


            Browser.Navigate(Urls.ProductDetailPageUrl);

            TestChatWebElements();

            VerifyElementDisplayed(() => ProductDetail.BoldChatButtonContainer);

            ProductDetail.BoldChatButtonContainer.Click();

            VerifyElementDisplayed(() => ProductDetail.BoldChatContainer);
            VerifyElementDisplayed(() => ProductDetail.BoldChatCloseButton);

            Thread.Sleep(2000);
            ProductDetail.BoldChatCloseButton.Click();

            Thread.Sleep(2000);
            VerifyElementDisplayed(() => ProductDetail.BoldChatCloseIcon);

            Browser.Wait.ForClickableElement(ProductDetail.BoldChatCloseIcon).Click();

            // Store In Session Mode
            StoreInSessionMode();

            // CSR Only
            CheckCsrWebElements();
        }

        protected abstract void CheckProfessionalPdp();

        protected abstract void CheckStickyArea();
        protected abstract void CheckCommonPdpElements();

        protected abstract void TestEmailAFriendWebElements();

        protected abstract void TestViewInYourRoomsWebElements();

        protected abstract void TestTurnToWebElements();

        protected abstract void TestTurnToReviewWebElements();

        protected abstract void CheckProductInStock();

        protected abstract void CheckLimitedQuantitySection();

        protected abstract void VerifyRelatedItemSection();

        protected abstract void VerifyProsSpecialPriceCallout();

        protected abstract void VerifyEnergyGuideElements();

        protected abstract void VerifyBuildFullSystemElements();

        protected abstract void VerifyHousingOptionElements();

        protected abstract void TestChatWebElements();

        protected abstract void StoreInSessionMode();

        protected abstract void VerifyStoreAvailabilityElements();

        protected abstract void CheckCsrWebElements();

        protected abstract void VerifyDistinctTrackLightingElements();

        protected void VerifyElementNotDisplayed(Expression<Func<IElement>> element)
        {
            var methodInfo = element.Body.ToString().Split('.');
            var propName = methodInfo[methodInfo.Length - 1];
            var compiledElement = element.Compile().Invoke();

            if (!ElementList.Contains(propName)) { SoftVerify.False(true, $"{propName} found but was not expected in the list of IElement properties for the given page object."); }
            else
            {
                SoftVerify.False(compiledElement.IsInitialized && compiledElement.Displayed, $"The unexpected element \"{propName}\" is displayed");
                ElementList.Remove(propName);
            }
        }
    }
}
