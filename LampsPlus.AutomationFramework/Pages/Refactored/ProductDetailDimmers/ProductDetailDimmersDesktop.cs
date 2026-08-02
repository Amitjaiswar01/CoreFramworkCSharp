using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.UI;
using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Utilities;


namespace LampsPlus.AutomationFramework.Pages.Refactored.ProductDetailDimmers
{
    public class ProductDetailDimmersDesktop : IProductDetailDimmersDesktop
    {
        //Class members
        private string _buildFullSystemOptionsId = "pdFullSystemOptions";
        protected virtual string PdViewFullTrackSystemId  => "pdViewFullTrackSystem";
        protected virtual string BuildFullSystemId => "build-full-system";
        protected virtual string BuildFullSystemSectionTitleClass => "pdSectionTitle";
        protected virtual string BuildFullSystemOptionsId  => "pdFullSystemOptions";
        protected virtual IElement BuildFullSystemOptions => Browser.Locate.ElementById(BuildFullSystemOptionsId);
        protected virtual IElement BuildFullSystemButton => Browser.Locate.ElementById(PdViewFullTrackSystemId);
        protected virtual IElement BuildFullSystemContainer => Browser.Locate.ElementById(BuildFullSystemId);
        protected virtual ReadOnlyCollection<IElement> ListOfFullSystemData(int nthIndex) => Browser.Locate.ElementsBySelector($"{HtmlTextWriterTag.Td.ToNthChildSelector(nthIndex)}", BuildFullSystemOptions);
        protected virtual ReadOnlyCollection<IElement> ListOfFullSystemSkus => ListOfFullSystemData(3);


        //Instances
        protected IBrowser Browser;

        public ProductDetailDimmersDesktop(IBrowser browser)
        {
            Browser = browser;
        }

        //Interface implementation
        public string PageTitle { get; }
        public string PageUrl { get; }
        public bool IsCurrentPage { get; }

        public virtual List<string> GetListOfFullSystemSkus => ListOfFullSystemSkus.Select(item => item.Text).ToList();
        public virtual string GetBuildFullSystemSectionTitle => Browser.Locate.ElementByClassName(BuildFullSystemSectionTitleClass, Browser.Locate.ElementById(BuildFullSystemId)).Text;
        public virtual string GetBuildFullSystemTableFirstSku => Browser.Locate.ElementBySelector($"{HtmlTextWriterTag.Tr.ToNthChildSelector(3)} {HtmlTextWriterTag.Td.ToNthChildSelector(3)}", Browser.Locate.ElementById(_buildFullSystemOptionsId)).Text;

        public virtual void NavigateToBuildFullSystemSection()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(PdViewFullTrackSystemId.ToCssIdSelector()));

            Browser.ScrollIntoView(BuildFullSystemButton, true);
            Browser.Wait.ForDomReady();

            BuildFullSystemButton.Click();
            Browser.Wait.ForDomReady();
        }

        public virtual bool IsBuildFullSystemDisplayed()
        {
            return BuildFullSystemContainer.Displayed;
        }
    }
}