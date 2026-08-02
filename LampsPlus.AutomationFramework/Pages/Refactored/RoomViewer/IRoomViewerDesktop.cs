using System.Collections.Generic;
using LampsPlus.AutomationFramework.Databases.Entities;

namespace LampsPlus.AutomationFramework.Pages.Refactored.RoomViewer
{
    public interface IRoomViewerDesktop : IPageObjectModel
    {
        void SelectHideButton();
        void SelectShowButton();
        void SelectDeselectButton();
        void SelectRemoveButton();
        void SelectUndoButton();
        void SelectDuplicateButton();
        void OpenAndFocusEmailModal();
        void OpenShareRoomModal();
        void OpenPrintRoomModal();
        void NavigateToSavedRooms();
        void InputEmailRecipientsInForm(string[] recipientEmails);
        void RoomViewerEmail(params string[] recipientEmails);
        void DeleteSavedRooms();
        void ChangeRoomName(string RoomName);
        void AddingFirstProductToWishList();
        void WaitForSavedRoomsToDisplay();
        void AddToCart();
        void AddAllToCart();
        void AddingSecondProductToWishList();
        void ARPageLoad();
        void OpenActiveRoom();
        void OpenDuplicateRoom();
        void OpenNonActiveRoom();
        void OpenSavedRoomModal();
        void SelectChangeRoomBtn();
        void ChooseSampleImageFromChangeRoomImageSection();
        void SelectDuplicateRoomOption();
        void CreateDuplicateRoom();
        string GetProductNameByShortSkuFromDb(string productName);
        string GetArCanvasHref();
        string GetArProductHref();
        string GetSkuData();
        string GetProductListCount();
        string GetSavedRoomModalTitle();
        string GetArCanvasHref(int index);
        string GetThumbnailImageHref();
        string GetRoomContainsNoProductText();
        string GetRoomContainsProductText();
        string GetFirstProductHref(int index);
        string GetSecondProductHref(int index);
        string GetTitleOfArPage();
        string GetRoomName();
        bool IsNewUnknownRoom(string roomNo);
        bool IsChooseSampleRoomVisible { get; }
        bool IsEmailNotificationDisplayed { get; }
        bool IsShareRoomModalDisplayed { get; }
        bool IsPrintModalDisplayed { get; }
        bool IsSkuDisplayed { get; }
        bool IsAddToCartDisabled { get; }
        bool IsSaveDisabled { get; }
        bool IsHideDisabled { get; }
        bool IsDeselectDisabled { get; }
        bool IsDuplicateDisabled { get; }
        bool IsRemoveDisabled { get; }
        bool IsBringFwdDisabled { get; }
        bool IsMoveBackDisabled { get; }
        bool IsFlipHorizontallyDisabled { get; }
        bool RoomContainsNoProduct { get; }
        bool RoomContains1Product { get; }
        bool RoomContains2Product { get; }
        bool BackToProductEnabled { get; }
        bool BackToProductDisabled { get; }
        List<Utilities.ProductModel> GetListOfAllProductsOnRoomViewer();
        List<ArProductModel> dataBaseList(Databases.Entities.ProductModel shortSkus);
    }
}