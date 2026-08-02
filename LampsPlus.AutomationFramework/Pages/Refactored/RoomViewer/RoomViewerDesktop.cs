using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Pages.Refactored.Modal;
using LampsPlus.AutomationFramework.Databases.Entities;
using LampsPlus.AutomationFramework.Utilities;

namespace LampsPlus.AutomationFramework.Pages.Refactored.RoomViewer
{
    public class RoomViewerDesktop : IRoomViewerDesktop
    {
        //Class members
        private string _productItemXpath = "//div[@class='productItem__image']//img";
        private string _controlClass = "control";
        private string _hideSkuClass = "hideShowBtn--hide";
        private string _deselectClass = "lpIcon-delete01";
        private string _removeSkuClass = "lpIcon-delete03";
        private string _undoSkuClass = "undo";
        private string _duplicateSkuClass = "lpIcon-duplicate";
        private string _productListEmptyClass = "productListEmpty";
        private string _backToProductClass = "backToProduct";
        private string _emailBtnClass = "lpIcon-email";
        private string _firstNameId = "FirstName";
        private string _lastNameId = "LastName";
        private string _printRoomBtnClass = "lpIcon-print";
        private string _fromEmailId = "FromEmail";
        private string _shareModalId = "shareModal";
        private string _formZipCodeFieldId = "ZipCode";
        private string _toEmailFieldId = "ToEmail";
        private string _sendEmailBtnId = "sendEmailBtn";
        private string _sendCopyCheckboxId = "fieldCheckbox";
        private string _emailNotificationClass = "emailNotification";
        private string _shareRoomBtnClass = "lpIcon-share01";
        private string _printModalId = "printModal";
        private string _modalIframeId = "modalIframe";
        private string _arEmailModalCloseBtnClass = "arEmailClose";
        private string _roomNameUpdateBtnClass = "contentEditableBtnSave";
        private string _changeRoomPhotoClass = "changeRoomPhoto";
        private string _imageClass = "image";
        private string _browseId = "browse";
        private string _productListCountClass = "productListCount__left";
        private string _addingToListClass = "addToList";
        private string _addAllToCartClass = "addAllCart";
        private string _addToCartClass = "addSelectedCart";
        private string _productItemClass = "productItem__link";
        private string _saveIconId = "savedPortfolio-totalSaved--icon";
        private string _saveRoomLinkId = "savedPortfolio-yourRooms--label";
        private string _duplicateSceneClass = "duplicateScene";
        private string _createRoomNameXpath = "//button[text()='Create Room']";
        private string _arCanvasSelector = "#arCanvas > svg > image";
        private string _openRoomClass = "openScene";
        private string _activeRoomClass = "unveil--done";
        private string _scenesContainerClass = "scenesContainer";
        private string _arPageTitleClass = "arPageTitle";
        private string _savedRoomContainerSelector = ".scenesContainer h1";
        private string _savedRoomBreadcrumbClass = "breadCrumb";
        private string _deleteRoomLinkClass = "deleteScene";
        private string _deleteSavedRoomButtonClass = "confirmDeleteScene";
        private string _continueShoppingButtonClass = "continueShopping";
        private string _sidebarTopLeftThumbnailXpath = "//div[@class='sidebarTop']//*[@type='button']//*[name()='img']";
        private string _hideShowBtnClass = "hideShowBtn--showBtn";
        private string _itemTotalClass = "productPrice";
        private string _productNameClass = "productBottom__title";
        private string _hideButtonClass = "productHideShow";
        private string _showButtonXpath = "//*[@id='productCarousel']/div/div/div[1]/div[1]/button/span";
        private string _deselectSkuXpath = "//button[contains(@class,'deselect')]";
        private string _shareModalXpath = "//*[@id=\"shareModal\"]//li[1]";
        private string _emailButtonXpath = "//button[contains(@class, 'email')]";
        private string ProductName(int index) => ProductNameLabel(index).Text;
        private string ProductTotalPrice(int index) => ProductTotalCostLabel(index).Text.Replace("$", "");

        protected virtual string _addSelectedCartClass => "addSelectedCart";

        private int UniqueProductsCount => Browser.Locate.ElementsByClassName(_productNameClass).Count;

        private IElement AddingToWishList => Browser.Locate.ElementByClassName(_addingToListClass);
        private IElement AddingToCart => Browser.Locate.ElementByClassName(_addToCartClass);
        private IElement AddingAllToCart => Browser.Locate.ElementByClassName(_addAllToCartClass);
        private IElement SaveIcon => Browser.Locate.ElementById(_saveIconId);
        private IElement SaveRoomLink => Browser.Locate.ElementById(_saveRoomLinkId);
        private IElement ActiveRoomOpen => Browser.Locate.ElementByClassName(_activeRoomClass);
        private IElement RoomNameUpdateBtn => Browser.Locate.ElementByClassName(_roomNameUpdateBtnClass);
        private IElement ProductListCount => Browser.Locate.ElementByXpath("//div[@class='productListCount__left']");
        private IElement ProductNameLabel(int index) => Browser.Locate.ElementsByClassName(_productNameClass)[index];
        private IElement HideButton(int index) => Browser.Locate.ElementsByClassName(_hideButtonClass)[index];
        private IElement ShowButton => Browser.Locate.ElementByXpath(_showButtonXpath);
        private IElement ProductTotalCostLabel(int index) => Browser.Locate.ElementsByClassName(_itemTotalClass)[index];
        private IElement ChangeRoomPhoto => Browser.Locate.ElementByClassName(_changeRoomPhotoClass);
        private IElement ChooseRandomSample(int index) => Browser.Locate.ElementsBySelector(_imageClass.ToCssClassSelector())[index];
        private IElement ShowSku => Browser.Locate.ElementByClassName(_hideShowBtnClass);
        private IElement AddToList => Browser.Locate.ElementByClassName(_addingToListClass);
        private IElement ProductListEmpty => Browser.Locate.ElementByXpath("//*[@id='arFooter']/div/div[2]");
        private IElement BackToProduct => Browser.Locate.ElementByClassName(_backToProductClass);
        private IElement DuplicateRoom => Browser.Locate.ElementByClassName(_duplicateSceneClass);
        private IElement SavedRoom(int index) => Browser.Locate.ElementsByClassName(_activeRoomClass)[index];
        private IElement DeleteSavedRoomLink(int index) => Browser.Locate.ElementsByClassName(_deleteRoomLinkClass)[index];
        private IElement SavedRoomContainer => Browser.Locate.ElementBySelector(_savedRoomContainerSelector);
        private IElement CreateRoom => Browser.Locate.ElementByXpath(_createRoomNameXpath);
        private IElement ArPageTitle => Browser.Locate.ElementByClassName(_arPageTitleClass);
        private IElement OpenSavedRoom => Browser.Locate.ElementByClassName(_openRoomClass);
        private IElement EmailButton => Browser.Locate.ElementByClassName(_emailBtnClass);
        private IElement ArEmailModalCloseBtn => Browser.Locate.ElementByClassName(_arEmailModalCloseBtnClass);
        private IElement ShareRoomBtn => Browser.Locate.ElementByClassName(_shareRoomBtnClass);
        private IElement PrintRoomBtn => Browser.Locate.ElementByClassName(_printRoomBtnClass);
        private IElement FormFirstNameField => Browser.Locate.ElementById(_firstNameId);
        private IElement FormLastNameField => Browser.Locate.ElementById(_lastNameId);
        private IElement FormEmailFromField => Browser.Locate.ElementById(_fromEmailId);
        private IElement ToEmailFromField => Browser.Locate.ElementById(_toEmailFieldId);
        private IElement FormZipCodeField => Browser.Locate.ElementById(_formZipCodeFieldId);
        private IElement SendEmailButton => Browser.Locate.ElementById(_sendEmailBtnId);
        private IElement DeleteSavedRoomButton => Browser.Locate.ElementByClassName(_deleteSavedRoomButtonClass);
        private ReadOnlyCollection<IElement> SendCopy => Browser.Locate.ElementsByClassName(_sendCopyCheckboxId);
        private ReadOnlyCollection<IElement> DeleteSavedRoomLinks => Browser.Locate.ElementsBySelector(_deleteRoomLinkClass.ToCssClassSelector());
        private IElement ThumbnailImage => Browser.Locate.ElementByXpath(_sidebarTopLeftThumbnailXpath);
        private IElement ProductsShowIcon(int index) => Browser.Locate.ElementsByClassName(_hideShowBtnClass)[index];

        protected List<Utilities.ProductModel> ProductsInRoomViewerList;
        protected IElement HideSku => Browser.Locate.ElementByClassName(_hideSkuClass);
        protected IElement DeselectSku => Browser.Locate.ElementByXpath(_deselectSkuXpath);
        protected IElement AddToCartButton => Browser.Locate.ElementByClassName(_addSelectedCartClass);
        protected IElement Control(int index) => Browser.Locate.ElementsBySelector(_controlClass.ToCssClassSelector())[index];
        protected IElement ArCanvasElement(int index) => Browser.Locate.ElementsBySelector(_arCanvasSelector)[index];
        protected IElement RemoveSku => Browser.Locate.ElementByClassName(_removeSkuClass);
        protected IElement UndoSku => Browser.Locate.ElementByClassName(_undoSkuClass);
        protected IElement DuplicateSku => Browser.Locate.ElementByXpath("//button[contains(@aria-label,'Duplicate')]");

        //Instances
        protected IBrowser Browser;
        protected IModalDesktop Modal;
        protected IAssert Assert;
        protected SessionSettings Settings;

        public RoomViewerDesktop(IBrowser browser, IModalDesktop modal, IAssert assert, SessionSettings settings)
        {
            ProductsInRoomViewerList = new List<Utilities.ProductModel>();
            Browser = browser;
            Modal = modal;
            Assert = assert;
            Settings = settings;
        }

        //Interface implementation
        public string PageTitle { get; }
        public string PageUrl { get; }
        public bool IsCurrentPage => Browser.Wait.IsVisibleElement(By.XPath("//button[contains(@class,'addSelectedCart')]"));
        public bool IsAddToCartDisabled => Browser.Wait.ForCondition(() => AddToCartButton.GetAttribute("aria-disabled") == "true");
        public bool IsSaveDisabled => Browser.Wait.ForCondition(() => AddToList.GetAttribute("aria-disabled") == "true");
        public bool IsSkuDisplayed => Browser.Wait.ForCondition(() => ArCanvasElement(1).GetAttribute("style").Contains("display: none"));
        public bool IsHideDisabled => Browser.Wait.ForCondition(() => Control(0).GetAttribute("aria-disabled") == "true");
        public bool IsDeselectDisabled => Browser.Wait.ForCondition(() => Control(1).GetAttribute("aria-disabled") == "true");
        public bool IsDuplicateDisabled => Browser.Wait.ForCondition(() => Control(2).GetAttribute("aria-disabled") == "true");
        public bool IsRemoveDisabled => Browser.Wait.ForCondition(() => Control(3).GetAttribute("aria-disabled") == "true");
        public bool IsBringFwdDisabled => Browser.Wait.ForCondition(() => Control(4).GetAttribute("aria-disabled") == "true");
        public bool IsMoveBackDisabled => Browser.Wait.ForCondition(() => Control(5).GetAttribute("aria-disabled") == "true");
        public bool IsFlipHorizontallyDisabled => Browser.Wait.ForCondition(() => Control(6).GetAttribute("aria-disabled") == "true");
        public bool RoomContainsNoProduct => Browser.Wait.ForCondition(() => ProductListEmpty.Text.Contains("Your room contains no products"));
        public bool RoomContains1Product => Browser.Wait.ForCondition(() => ProductListCount.Text.Contains("1 Product In This Room"));
        public bool RoomContains2Product => Browser.Wait.ForCondition(() => ProductListCount.Text.Contains("2 Products In This Room"));
        public bool BackToProductEnabled => Browser.Wait.ForCondition(() => BackToProduct.GetAttribute("aria-disabled") == "false");
        public bool BackToProductDisabled => Browser.Wait.ForCondition(() => BackToProduct.GetAttribute("aria-disabled") == "true");
        public bool IsEmailNotificationDisplayed => Browser.Wait.IsVisibleElement(By.ClassName(_emailNotificationClass));
        public bool IsShareRoomModalDisplayed => Browser.Wait.IsVisibleElement(By.Id(_shareModalId), 30);
        public bool IsPrintModalDisplayed => Browser.Wait.IsVisibleElement(By.Id(_printModalId), 30);
        public bool IsChooseSampleRoomVisible => Browser.Locate.DoesElementExistImmediately(Modal.LpModalIframeId.ToCssIdSelector());

        public string GetSkuData()
        {
            Browser.Wait.IsVisibleElement(By.XPath(_productItemXpath));
            return Browser.Locate.ElementByXpath(_productItemXpath).GetAttribute("data-sku").ToLower();
        }

        public string GetRoomContainsNoProductText()
        {
            if (Settings.Browser == WebBrowser.Safari)
            {
                var script = "return document.querySelector('#arFooter > div > div.productListEmpty.productListContent').innerText";
                var text = (string)Browser.ExecuteJs(script);
                return text;
            }

            return ProductListEmpty.Text;

        }

        public string GetRoomContainsProductText()
        {
            if (Settings.Browser == WebBrowser.Safari)
            {
                var script = "return document.querySelector('#arFooter > div > div.productListCount > div.productListCount__left').innerText";
                var text = (string)Browser.ExecuteJs(script);
                return text;
            }

            Browser.SwitchToCurrentWindow();
            return ProductListCount.Text;
        }

        public void SelectHideButton()
        {
            HideSku.Click();
            Browser.Wait.ForDomReady();
            Assert.True(IsSaveDisabled, "Save button is not disabled.");
        }

        public void SelectShowButton()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_hideShowBtnClass.ToCssClassSelector()));
            Browser.ScrollIntoView(ShowSku);
            Browser.Wait.ForDomReady();
            ShowSku.Click();
            Browser.Wait.ForCondition(() => BackToProduct.GetAttribute("aria-disabled") == "false");
        }

        public void SelectDeselectButton()
        { 
            Browser.ScrollToTopOfWindow(); 
            Browser.RefreshPage();
            Browser.Wait.ForDomReady();
            Browser.SwitchToCurrentWindow();
            DeselectSku.Click();
            Browser.Wait.ForCondition(() => Control(1).GetAttribute("aria-disabled") == "true");
        }

        public void SelectRemoveButton()
        {
            Browser.SwitchToCurrentWindow();
            ArCanvasElement(1).Click();
            Browser.Wait.ForDomReady();

            RemoveSku.Click();
            Browser.Wait.ForDomReady();
        }

        public void SelectUndoButton()
        { 
            Browser.SwitchToCurrentWindow();
            Browser.Wait.ForCondition(() => UndoSku.GetAttribute("aria-disabled") == "false");
            UndoSku.Click();
            Browser.Wait.ForDomReady();
        }

        public void SelectDuplicateButton()
        {
            Browser.Wait.ForDomReady();
            Browser.SwitchToCurrentWindow();
            ArCanvasElement(1).Click();
            Browser.Wait.ForDomReady();
            Browser.SwitchToCurrentWindow();
            DuplicateSku.Click();
            Browser.Wait.ForDomReady();
            Browser.RefreshPage();
        }

        public void OpenAndFocusEmailModal()
        {
            Browser.Wait.IsVisibleElement(By.XPath(_emailButtonXpath));
            EmailButton.Click();
            Browser.SwitchFocusToIframe(Modal.GetLpModal());
            Browser.Wait.ForDomReady();
        }

        public void OpenShareRoomModal()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_shareRoomBtnClass));
            ShareRoomBtn.Click();
            Browser.Wait.ForDomReady();
            Browser.SwitchFocusToIframe(Modal.GetLpModal());
            Browser.Wait.IsVisibleElement(By.XPath(_shareModalXpath));
        }

        public void OpenPrintRoomModal()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_printRoomBtnClass));
            PrintRoomBtn.Click();
            Browser.SwitchFocusToIframe(Modal.GetLpModal());
        }

        public void InputEmailRecipientsInForm(string[] recipientEmails) { ToEmailFromField.SendKeys(string.Join(", ", recipientEmails)); }

        public void RoomViewerEmail(params string[] recipientEmails)
        {
            Browser.SwitchFocusToIframe(Browser.Locate.ElementBySelector(_modalIframeId.ToCssIdSelector()));
            Browser.Wait.IsVisibleElement(By.Id(_firstNameId));
            FormFirstNameField.SendKeys("LPFirst");
            FormLastNameField.SendKeys("LPLast");
            FormEmailFromField.SendKeys("fedcsrmanager@lampsplus.com");
            FormZipCodeField.SendKeys("91311");
            InputEmailRecipientsInForm(recipientEmails);
            SendCopy[0].Click();
            SendCopy[1].Click();
            SendCopy[2].Click();
            SendEmailButton.Click();
        }

        public void ChangeRoomName(string RoomName)
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_arPageTitleClass));
            ArPageTitle.Click();
            ArPageTitle.SendKeys(RoomName);
            Browser.Wait.IsVisibleElement(By.ClassName(_roomNameUpdateBtnClass));
            RoomNameUpdateBtn.Click();
            Browser.Wait.IsVisibleElement(By.ClassName(_arPageTitleClass));
        }

        public string GetRoomName()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_arPageTitleClass));
            return ArPageTitle.Text.Replace("edit", String.Empty).TrimEnd();
        }

        public virtual void ChooseSampleImageFromChangeRoomImageSection()
        {
            Modal.SwitchFocusToModal();
            
            Browser.Wait.IsVisibleElement(By.Id(_browseId));
            Browser.Wait.ForDisplayedElement(ChooseRandomSample(2)).Click();

            Browser.Wait.ForCondition(()=>IsChooseSampleRoomVisible == false);
            Browser.RefreshPage();
        }

        public void SelectChangeRoomBtn()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_changeRoomPhotoClass));
            ChangeRoomPhoto.Click();
        }

        public string GetArCanvasHref(int index)
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_arCanvasSelector));
            return ArCanvasElement(index).GetAttribute("href");
        }

        public string GetSavedRoomModalTitle()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_scenesContainerClass));
            return SavedRoomContainer.Text;
        }

        public string GetProductListCount()
        {
            Browser.Wait.ForDisplayedElement(ProductListCount);
            return ProductListCount.Text.Replace("Products In This Room", string.Empty).Trim();
        }

        public List<Utilities.ProductModel> GetListOfAllProductsOnRoomViewer()
        {
            ProductsInRoomViewerList.Clear();

            for (var index = 0; index < UniqueProductsCount; index++) { ProductsInRoomViewerList.Add(new Utilities.ProductModel(ProductName(index), ProductTotalPrice(index))); }

            return ProductsInRoomViewerList;
        }

        public void ARPageLoad()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_productItemClass),30);
        }

        public void AddingFirstProductToWishList()
        {
            Browser.Wait.ForDomReady();
            HideButton(0).Click();
            ShowButton.Click();
            Browser.Wait.IsVisibleElement(By.ClassName(_addingToListClass));
            AddingToWishList.Click();
            Browser.Wait.ForDomReady(30);
        }

        public void AddToCart()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_addToCartClass));
            AddingToCart.Click();
        }

        public void AddAllToCart()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_addAllToCartClass),10);
            AddingAllToCart.Click();
        }

        public void AddingSecondProductToWishList()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_addingToListClass));
            AddingToWishList.Click();
            Browser.Wait.ForDomReady(30);
        }

        public void OpenDuplicateRoom()
        {
            DuplicateRoom.Click();
            Browser.Wait.IsVisibleElement(By.XPath(_createRoomNameXpath));
            CreateRoom.Click();
        }

        public void SelectDuplicateRoomOption()
        {
            DuplicateRoom.Click();
            Browser.Wait.IsVisibleElement(By.XPath(_createRoomNameXpath));
        }

        public void CreateDuplicateRoom()
        {
            Browser.Wait.IsVisibleElement(By.XPath(_createRoomNameXpath));
            CreateRoom.Click();
        }

        public void NavigateToSavedRooms()
        {
            Browser.Navigate(Urls.SavedRoomPageUrl);
            Browser.Wait.IsVisibleElement(By.ClassName(_savedRoomBreadcrumbClass));
        }

        public void OpenSavedRoomModal()
        {
            OpenSavedRoom.Click();
            Browser.Wait.WaitForIframeAndSwitchToIt(Modal.LpModalIframeId);
        }

        public void OpenNonActiveRoom()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_activeRoomClass));
            SavedRoom(1).Click();
        }

        public bool IsNewUnknownRoom(string roomNo)
        {
            Browser.Wait.ForCondition(() => ArPageTitle.GetAttribute("data-placeholder") == "Unnamed Room_"+ roomNo);
            return true;
        }

        public void DeleteSavedRooms()
        {
            Browser.Wait.ForDomReady();
            NavigateToSavedRooms();
            if (Browser.Locate.DoesElementExistImmediately(_deleteRoomLinkClass.ToCssClassSelector()))
            {
                var savedRoomCount = DeleteSavedRoomLinks.Count;

                for (var i = 0; i < savedRoomCount; i++)
                {
                    if (i != 0) { Browser.RefreshPage(); } //Refresh page for all Items except the very first item.

                    Browser.Wait.IsVisibleElement(By.CssSelector(_savedRoomBreadcrumbClass.ToCssClassSelector()));
                    Browser.Wait.ForDisplayedElement(DeleteSavedRoomLink(0));

                    DeleteSavedRoomLink(0).Click();

                    Browser.Wait.IsVisibleElement(By.Id(Modal.LpModalId));

                    DeleteSavedRoomButton.Click();
                }
            }

            Browser.Wait.IsVisibleElement(By.ClassName(_continueShoppingButtonClass), 60);
        }

        public virtual void OpenActiveRoom()
        {
            SaveIcon.Click();
            Browser.Wait.IsVisibleElement(By.Id(_saveRoomLinkId), 5);
            SaveRoomLink.Click();
            Browser.Wait.ForPage(Urls.RoomsPageUrl);
            Browser.Wait.IsVisibleElement(By.ClassName(_activeRoomClass));
            ActiveRoomOpen.Click();
            Browser.Wait.IsVisibleElement(By.CssSelector(_arPageTitleClass.ToCssClassSelector()));
        }

        public List<ArProductModel> dataBaseList(Databases.Entities.ProductModel shortSkus)
        {
            List<ArProductModel> act = new List<ArProductModel>();
            foreach (var arProductModel in shortSkus.ArProducts)
            {
                var dataBaseProductList = arProductModel;

                act.Add(dataBaseProductList);
            }
            return act;
        }

        public string GetArCanvasHref()
        {
            return ArCanvasElement(0).GetAttribute("Href");
        }

        public string GetArProductHref()
        {
            return ArCanvasElement(1).GetAttribute("Href");
        }

        public string GetProductNameByShortSkuFromDb(string productName)
        {
            char name = '"';

            string correctProductName = productName.Replace("&quot;", name.ToString());

            return correctProductName;
        }

        public string GetThumbnailImageHref()
        {
            return ThumbnailImage.GetAttribute("src");
        } 
        
        public string GetTitleOfArPage()
        {
            return TextActions.NormalizeWhitespace(ProductListCount.Text);
        }

        public string GetFirstProductHref(int index)
        {
            //Ensure to hide both the products
            Browser.Wait.IsVisibleElement(By.ClassName(_hideButtonClass));
            Browser.ScrollIntoView(HideButton((0)), true);
            Browser.Wait.ForDomReady();
            HideButton(0).Click();
            Browser.Navigate(Urls.AugmentedRealityUrl);
            Browser.Wait.ForDomReady();
            Browser.ScrollIntoView(HideButton((1)));
            Browser.Wait.ForDomReady();
            HideButton(1).Click();

            //Click on show to get focused on 1st product
            ProductsShowIcon(index).Click();
            Browser.RefreshPage();
            Browser.Wait.ForDomReady();
            return GetThumbnailImageHref();
        }

        public string GetSecondProductHref(int index)
        {
            Browser.Navigate(Urls.AugmentedRealityUrl);
            Browser.Wait.ForDomReady();
            Browser.ScrollIntoView(HideButton((1)));
            Browser.Wait.ForDomReady();
            HideButton(0).Click();

            //Click on show to get focused on 2nd product
            ProductsShowIcon(index).Click();
            Browser.Wait.ForDomReady();
            return GetThumbnailImageHref();
        }

        public void WaitForSavedRoomsToDisplay()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_activeRoomClass));
        }
    }
}