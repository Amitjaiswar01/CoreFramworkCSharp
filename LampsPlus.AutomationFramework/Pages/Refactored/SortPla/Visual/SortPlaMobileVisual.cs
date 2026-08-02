using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.SortPla.Visual
{

    public class SortPlaMobileVisual : SortPlaMobile, ISortPlaMobileVisual
    {
        public SortPlaMobileVisual(IBrowser browser) : base(browser)
        {
        }

        public IElement IgnoreShipsTodayQLElement()
        {
            Browser.Wait.ForClickableElement(ShipsTodayQlElement);
            return ShipsTodayQlElement;
        }
    }
}