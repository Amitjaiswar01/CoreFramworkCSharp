using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.UI;
using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Utilities;

namespace LampsPlus.AutomationFramework.Pages.Refactored.ProductDetailDimmers
{
    public class ProductDetailDimmersMobile : ProductDetailDimmersDesktop, IProductDetailDimmersMobile
    {
        //Class members
        private string _buildFullSystemOptionsClass = "unstyledList";
        private string _buildFullSystemContainerId  = "buildFullSystemContainer";
        private string _buildFullSystemDrawerXpath  = "//*[@id='pdBuildFullSystemCollapsibleButton']";
        private IElement BuildFullSystemDrawer => Browser.Locate.ElementByXpath(_buildFullSystemDrawerXpath);
        protected override string PdViewFullTrackSystemId => "pdViewFullTrackSystemBtn";
        protected override string BuildFullSystemId => "pdBuildFullSystemCollapsibleButton";
        protected override IElement BuildFullSystemButton => Browser.Locate.ElementById(PdViewFullTrackSystemId);
        protected override IElement BuildFullSystemContainer => Browser.Locate.ElementById(BuildFullSystemId);
        protected override IElement BuildFullSystemOptions => Browser.Locate.ElementBySelector($"{_buildFullSystemContainerId.ToCssIdSelector()} {_buildFullSystemOptionsClass.ToCssClassSelector()}");
        protected override ReadOnlyCollection<IElement> ListOfFullSystemSkus => Browser.Locate.ElementsBySelector($"{HtmlTextWriterTag.Li}", BuildFullSystemOptions);

        public ProductDetailDimmersMobile(IBrowser browser) : base(browser)
        {
        }

        //Interface implementation
        public override string GetBuildFullSystemTableFirstSku => Browser.Locate.ElementBySelector($"{HtmlTextWriterTag.Li} {HtmlTextWriterTag.Div} {HtmlTextWriterTag.Div} {HtmlTextWriterTag.Img}",
            (Browser.Locate.ElementBySelector($"{_buildFullSystemContainerId.ToCssIdSelector()} {_buildFullSystemOptionsClass.ToCssClassSelector()}"))).GetAttribute("data-sku");
        public override string GetBuildFullSystemSectionTitle => Browser.Locate.ElementBySelector($"{HtmlTextWriterTag.Span}", Browser.Locate.ElementById(BuildFullSystemId)).Text;
        public override List<string> GetListOfFullSystemSkus => ListOfFullSystemSkus.Select(item => item.GetAttribute("data-shortsku")).ToList();

        public override void NavigateToBuildFullSystemSection()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(BuildFullSystemId.ToCssIdSelector()));

            Browser.ScrollToBottomOfPageJs();
            Browser.ScrollIntoView(BuildFullSystemDrawer, true);
            Browser.Wait.IsVisibleElement(By.XPath(_buildFullSystemDrawerXpath));

            Browser.ClickByJs(BuildFullSystemDrawer);
            Browser.Wait.IsVisibleElement(By.Id(BuildFullSystemId));
        }
    }
}