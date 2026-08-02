using System.Collections.ObjectModel;
using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages.Refactored.ProductDetailMcp
{
    public class ProductDetailMcpDesktop : IProductDetailMcpDesktop
    {
        //Class members
        private string _topClass  = "top";
        protected string PopularColorsId => "popularColors";
        protected string GicleeShadeOptionsThumbClass => "pdGicleeShadeOptionsThumb";

        protected IElement PopularColors => Browser.Locate.ElementById(PopularColorsId);
        protected IElement PopularColorsTop => Browser.Locate.ElementByClassName(_topClass, PopularColors);
        protected ReadOnlyCollection<IElement> ListOfPopularColors => Browser.Locate.ElementsByClassName(GicleeShadeOptionsThumbClass, PopularColors);

        //Instances
        protected IBrowser Browser;

        public ProductDetailMcpDesktop(IBrowser browser)
        {
            Browser = browser;
        }

        //Interface implementation
        public string PageTitle { get; }
        public string PageUrl { get; }
        public bool IsCurrentPage { get; }

        public virtual int GetPopularColorsCount()
        {
            Browser.Wait.ForClickableElement(PopularColorsTop).Click();
            return ListOfPopularColors.Count;
        }
    }
}