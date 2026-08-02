using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages
{
    public class OrderPreview
    {
        #region Page Elements
        public IElement OrderDateElement { get; set; }
        public IElement OrderIdElement { get; set; }
        public IElement WebSiteElement { get; set; }
        public IElement OrderTotalElement { get; set; }
        public IElement OrderStatusElement { get; set; }
        #endregion
    }
}
