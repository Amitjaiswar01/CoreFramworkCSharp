using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Utilities;
using Automation.Framework.Verifies;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Pages.Refactored.Modal;

namespace LampsPlus.AutomationFramework.Pages.Refactored.RoomViewer
{
    public class RoomViewerMobile : RoomViewerDesktop, IRoomViewerMobile
    {
        //Class Members
        private string _pdpArIframeXpath = "//iframe[@aria-hidden='true']";
        private string _arKitBtnClass = ".arKit .arKit__btn";
        private string _darkroomButtonDefaultClass = "darkroom-button-default";
        private string _cancelErase = "cancelErase";
        private string _darkroomButtonDangerClass = "darkroom-button-danger";
        private string _uploadBtnId = "uploadBtn";
        private string _arFooterId = "arFooter";
        private string _arKitGetStartedClass = "arKit__getStarted";
        private string _arSubHeaderXpath = "//p[normalize-space()='View This Productin Your Room']";
        private string _fileTypeSelector = "[type='file']";
        private string _sampleRoomBtnClass = "sampleRoomBtn";
        private string _sampleRoomImageClass = "image";
        private string _sampleRoomFromRoomOptionsSelector = ".galleryWrapper .samplePhotos .image";
        private string _roomOptionsButtonClass = "arProductList__roomOptions";
        private string _duplicateRoomButtonClass = "duplicateRoom";
        private string _openSavedRoomButtonClass = "changeScene";
        private string _createRoomId = "createRoom";
        private string _arSampleImageXpath = "(//span[@class='image '])[1]";
        private string _arTopHeaderId = "toolSelections";
        private string _hideShowBtnClass = "hideShowBtn--show";
        private string _arCanvasImageSelector = "#arCanvas > svg > image:nth-child(4)";
        private string _arProductListHeaderXpath = "//*[@id=\"arFooter\"]//*[text() = 'Products in this room']";
        private string _savedRoomIframeXpath = "//iframe[@tabindex='-1']";
        private string _savedRoomImageClass = "image";
        private string _productInRoomLinkClass = "productItem__link";
        private string _startNewRoomBtnClass = "startNewRoom";
        private string _savedRoomHeaderTextClass = "subHeader";
        private string _toggleViewSelector = ".toggleView";
        private string _changeBackgroundClass = "changeBackground";
        private string _roomViewerIframeXpath = "//iframe[contains(@src,'/viewer/SceneView/')]";
        private string _roomOptionsXpath = "//*[@id='arFooter']//*[contains(text(),'Room Options')]";
        private string _arBodyContainerSelector = ".arContainer #arBody";

        protected override string _addSelectedCartClass => "addtoCartBtn";

        private IElement SavedRoomHeader => Browser.Locate.ElementByClassName(_savedRoomHeaderTextClass);
        private IElement StartNewRoomBtn => Browser.Locate.ElementByClassName(_startNewRoomBtnClass);
        private IElement ProductInRoom => Browser.Locate.ElementByClassName(_productInRoomLinkClass);
        private IElement SavedRoomIframe => Browser.Locate.ElementByXpath(_savedRoomIframeXpath);
        private IElement OpenSavedRoomButton => Browser.Locate.ElementByClassName(_openSavedRoomButtonClass);
        private IElement DuplicateRoomButton => Browser.Locate.ElementByClassName(_duplicateRoomButtonClass);
        private IElement PdpArIframe => Browser.Locate.ElementByXpath(_pdpArIframeXpath);
        private IElement ArViewerElement(int index) => Browser.Locate.ElementsBySelector(_arKitBtnClass)[index];
        private IElement ArEditingTools(int index) => Browser.Locate.ElementsByClassName(_darkroomButtonDefaultClass)[index];
        private IElement SampleRoomImage(int index) => Browser.Locate.ElementsByClassName(_sampleRoomImageClass)[index];
        private IElement SampleRoomFromRoomOptions(int index) => Browser.Locate.ElementsByClassName(_sampleRoomImageClass)[index];
        private IElement ArEditingCancelButton => Browser.Locate.ElementByClassName(_cancelErase);
        private IElement GetStartedButton => Browser.Locate.ElementByClassName(_arKitGetStartedClass);
        private IElement ArCropCancelButton => Browser.Locate.ElementByClassName(_darkroomButtonDangerClass);
        private IElement ArProceedButton => Browser.Locate.ElementById(_uploadBtnId);
        private IElement SampleRoomButton => Browser.Locate.ElementByClassName(_sampleRoomBtnClass);
        private IElement RoomOption => Browser.Locate.ElementByClassName(_roomOptionsButtonClass);
        private IElement CreateRoomBtn => Browser.Locate.ElementByClassName(_createRoomId);
        private IElement FirstSampleRoom => Browser.Locate.ElementByXpath(_arSampleImageXpath);
        private IElement ShowButton => Browser.Locate.ElementByClassName(_hideShowBtnClass);
        private IElement ArProductListHeader => Browser.Locate.ElementByXpath(_arProductListHeaderXpath);
        private IElement ChangeRoomPhotoButton => Browser.Locate.ElementByClassName(_changeBackgroundClass);

        private bool IsSampleRoomImagesDisplayed(int timeToWait)
        {
            return Browser.Wait.IsVisibleElement(By.ClassName(_sampleRoomImageClass));
        }

        public RoomViewerMobile(IBrowser browser, IModalMobile modal, IAssert assert, SessionSettings settings) : base(browser, modal, assert, settings) { }

        //Interface implementation
        public bool IsCurrentPage => Browser.Wait.IsVisibleElement(By.Id(_arTopHeaderId));
        public bool IsDuplicateRoomModalVisible() => Browser.Wait.IsVisibleElement(By.Id(_createRoomId));
        public bool IsImageInRoomEnabled() => Browser.Wait.IsVisibleElement(By.CssSelector(_arCanvasImageSelector));
        public bool IsAddToCartDisabled => Browser.Wait.ForCondition(() => Control(0).GetCssValue("opacity").Equals("0.7"));
        public bool IsHideDisabled => Browser.Wait.ForCondition(() => Control(1).GetCssValue("opacity").Equals("0.7"));
        public bool IsDeselectDisabled => Browser.Wait.ForCondition(() => Control(1).GetCssValue("opacity").Equals("0.7"));
        public bool IsDuplicateDisabled => Browser.Wait.ForCondition(() => Control(1).GetCssValue("opacity").Equals("0.7"));
        public bool IsRemoveDisabled => Browser.Wait.ForCondition(() => Control(1).GetCssValue("opacity").Equals("0.7"));
        public bool IsBringFwdDisabled => Browser.Wait.ForCondition(() => Control(1).GetCssValue("opacity").Equals("0.7"));
        public bool IsMoveBackDisabled => Browser.Wait.ForCondition(() => Control(1).GetCssValue("opacity").Equals("0.7"));
        public bool IsFlipHorizontallyDisabled => Browser.Wait.ForCondition(() => Control(1).GetCssValue("opacity").Equals("0.7"));
        public bool RoomContainsProducts => Browser.Wait.ForCondition(() => ArProductListHeader.Text.Contains("Products in this room"));
        public bool IsChooseSampleRoomVisible => Browser.Wait.IsVisibleElement(By.CssSelector(_arBodyContainerSelector));
        public bool IsArPageContentVisibleFor3d() => Browser.Wait.IsVisibleElement(By.XPath(_pdpArIframeXpath));

        public bool IsArPageContentVisible()
        {
            Browser.SwitchToDefaultContent();
            Browser.SwitchFocusToIframe(PdpArIframe);
            return Browser.Wait.IsVisibleElement(By.ClassName(_sampleRoomBtnClass));
        }

        private bool IsProductRemoved(int timeToWait)
        {
            return Browser.Wait.IsInvisibleElement(By.XPath(_arProductListHeaderXpath));
        }

        private bool IsSampleRoomSelected(int timeToWait)
        {
            Browser.SwitchToCurrentWindow();
            Browser.Wait.ForCondition(() => Browser.PageUrl.Contains("?fromPdp=1"));
            Browser.RefreshPage();
            return Browser.Wait.IsVisibleElement(By.ClassName(_roomOptionsButtonClass));
        }

        public void ChooseArViewType(int index)
        {
            Browser.SwitchToCurrentWindow();
            Browser.SwitchFocusToIframe(PdpArIframe);
            Browser.Wait.IsVisibleElement(By.CssSelector(_arKitBtnClass));
            ArViewerElement(index).Click();
        }

        public void OpenSampleRoom(int roomIndex)
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_sampleRoomBtnClass));
            Browser.ClickOnButtonMultipleTimes(SampleRoomButton, 10, IsSampleRoomImagesDisplayed);
            Browser.ClickOnButtonMultipleTimes(SampleRoomImage(roomIndex), 10, IsSampleRoomSelected);
        }

        public void UploadPhoto()
        {
            Browser.Wait.IsVisibleElement(By.XPath(_arSubHeaderXpath));
            Browser.Locate.ElementBySelector(_fileTypeSelector).SendKeys(FileUpload.ArUploadPath);
        }

        public void SelectEraseButton()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_darkroomButtonDefaultClass));
            ArEditingTools(0).Click();
        }

        public void SelectEraseCancelButton()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_cancelErase));
            ArEditingCancelButton.Click();
        }

        public void OpenSavedRoom()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_roomOptionsButtonClass));
            RoomOption.Click();
            Browser.Wait.IsVisibleElement(By.ClassName(_openSavedRoomButtonClass));
            OpenSavedRoomButton.Click();
        }

        public void SelectProductInRoom()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_roomOptionsButtonClass));
            ProductInRoom.Click();
        }

        public void StartNewRoom()
        {
            Browser.Wait.IsVisibleElement(By.XPath(_pdpArIframeXpath));
            Browser.SwitchFocusToIframe(PdpArIframe);
            Browser.Wait.IsVisibleElement(By.ClassName(_startNewRoomBtnClass));
            StartNewRoomBtn.Click();
        }

        public void SelectRotateButton()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_darkroomButtonDefaultClass));
            ArEditingTools(2).Click();
        }

        public void SelectDuplicateRoom()
        {
            Browser.Locate.ElementByXpath("//*[@id='arFooter']//*[contains(text(),'Room Options')]").Click();
            Browser.Wait.IsVisibleElement(By.ClassName(_duplicateRoomButtonClass));
            DuplicateRoomButton.Click();
        }

        public void SelectSavedRoom(int roomIndex)
        {
            Browser.SwitchToDefaultContent();
            Browser.Wait.IsVisibleElement(By.XPath(_savedRoomIframeXpath));
            Browser.SwitchFocusToIframe(SavedRoomIframe);
            Browser.Wait.IsVisibleElement(By.ClassName(_savedRoomImageClass));
            SampleRoomImage(roomIndex).Click();
            Browser.SwitchToDefaultContent();
            Browser.Wait.IsVisibleElement(By.ClassName(_roomOptionsButtonClass));
        }

        public string GetSavedRoomHeader()
        {
            Browser.Wait.IsVisibleElement(By.XPath(_savedRoomIframeXpath));
            Browser.SwitchFocusToIframe(SavedRoomIframe);
            var header = SavedRoomHeader.Text;
            return header;
        }

        public void SelectCropButton()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_darkroomButtonDefaultClass));
            ArEditingTools(3).Click();
        }

        public void SelectCropCancelButton()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_darkroomButtonDangerClass));
            ArCropCancelButton.Click();
        }

        public void SelectProceedButton()
        {
            Browser.Wait.IsVisibleElement(By.Id(_uploadBtnId));
            ArProceedButton.Click();

            Browser.Wait.IsVisibleElement(By.Id(_arFooterId));
        }

        public void Open3DViewer()
        {
            var viewType3DIndex = 0;
            ChooseArViewType(viewType3DIndex);
            Browser.SwitchToCurrentWindow();
            Browser.Wait.IsVisibleElement(By.ClassName(_arKitGetStartedClass));
            GetStartedButton.Click();
        }

        public void CreateDuplicate2dRoom()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_createRoomId));
            CreateRoomBtn.Click();
        }

        public string Get2dArProductHref()
        {
            return ArCanvasElement(0).GetAttribute("Href");
        }

        public void SelectDeselectButton()
        {
            DeselectSku.Click();
            Browser.Wait.ForCondition(() => Control(1).GetCssValue("opacity").Equals("0.7"));
        }

        public void SelectRemoveButton()
        {
            Browser.ScrollToTopOfWindow();
            Browser.ClickWithTapByElementCoordinates(Browser.Locate.ElementBySelector(_arCanvasImageSelector));

            Browser.ClickOnButtonMultipleTimes(RemoveSku, 10, IsProductRemoved);
            Browser.Wait.IsInvisibleElement(By.XPath(_arProductListHeaderXpath));
        }

        public void SelectDuplicateButton()
        {
            Browser.ClickWithTapByElementCoordinates(Browser.Locate.ElementBySelector(_arCanvasImageSelector));
            DuplicateSku.Click();
        }

        public void HideProduct()
        {
            HideSku.Click();
        }

        public void ShowProduct()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(_hideShowBtnClass.ToCssClassSelector()));
            ShowButton.Click();
        }

        public void SelectUndoButton()
        {
            UndoSku.Click();
            Browser.Wait.ForCondition(() => ArProductListHeader.Text.Contains("Products in this room"));
        }

        public void ChangeRoomBackground()
        {
            Browser.Locate.ElementByXpath(_roomOptionsXpath).Click();
            Browser.Wait.IsVisibleElement(By.ClassName(_changeBackgroundClass));
            ChangeRoomPhotoButton.Click();
        }

        public override void ChooseSampleImageFromChangeRoomImageSection()
        {
            Browser.SwitchFocusToIframe(Browser.Locate.ElementByXpath(_roomViewerIframeXpath));
            Browser.Wait.IsVisibleElement(By.CssSelector(_sampleRoomFromRoomOptionsSelector));
            SampleRoomFromRoomOptions(2).Click();
            Browser.SwitchToCurrentWindow();
            Browser.Wait.ForCondition(() => Browser.PageUrl.Contains("?fromPdp=1"));
            Browser.RefreshPage();
            Browser.Wait.IsVisibleElement(By.ClassName(_roomOptionsButtonClass));
        }
    }
}