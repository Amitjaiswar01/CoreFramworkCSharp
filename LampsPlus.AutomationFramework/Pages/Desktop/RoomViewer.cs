using Automation.Framework;
using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Pages.Desktop
{
    /// <summary>
    /// https://www.lampsplus.com/viewer/
    /// </summary>
    public class RoomViewer : RoomViewerBase
    {
        /// <inheritdoc />      
        public RoomViewer(IBrowser browser) : base(browser) { }
    }
}
