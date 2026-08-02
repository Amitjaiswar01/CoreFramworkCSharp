using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.Home.Visual
{
    public class HomeMobileVisual : HomeMobile, IHomeMobileVisual
    {
        public HomeMobileVisual(IBrowser browser) : base(browser) { }

        public IElement IgnoreInstagramPixleeElement()
        {
            return PixleeElement;
        }

        public IElement IgnoreInstagramFeed()
        {
            return InstagramFeed;
        }
    }
}
