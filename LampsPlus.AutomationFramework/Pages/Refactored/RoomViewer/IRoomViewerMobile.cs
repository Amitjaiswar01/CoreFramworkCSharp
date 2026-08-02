namespace LampsPlus.AutomationFramework.Pages.Refactored.RoomViewer
{
    public interface IRoomViewerMobile : IRoomViewerDesktop
    {
        void ChooseArViewType(int index);
        void UploadPhoto();
        void SelectEraseButton();
        void SelectEraseCancelButton();
        void SelectRotateButton();
        void SelectCropButton();
        void SelectCropCancelButton();
        void SelectProceedButton();
        bool IsArPageContentVisible();
        bool IsArPageContentVisibleFor3d();
        void Open3DViewer();
        void OpenSampleRoom(int roomIndex);
        void ChangeRoomBackground();
        void SelectDuplicateRoom();
        void OpenSavedRoom();
        void CreateDuplicate2dRoom();
        bool IsDuplicateRoomModalVisible();
        void ShowProduct();
        void HideProduct();
        void SelectSavedRoom(int roomIndex);
        void SelectProductInRoom();
        void StartNewRoom();
        bool IsImageInRoomEnabled();
        bool IsAddToCartDisabled { get; }
        bool RoomContainsProducts { get; }
        string Get2dArProductHref();
        string GetSavedRoomHeader();
    }
}