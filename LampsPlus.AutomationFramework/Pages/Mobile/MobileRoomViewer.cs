using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Pages.Mobile
{
    /// <summary>
    /// https://www.lampsplus.com/viewer/
    /// </summary>
    public class MobileRoomViewer : RoomViewerBase
    {
        /// <inheritdoc />       
        public MobileRoomViewer(IBrowser browser) : base(browser) { }
    }
}
