using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Pages.Mobile
{
    /// <summary>
    /// Example page: https://www.lampsplus.com/products/samba-china-red-silk-shade-apothecary-table-lamp__29j32.html
    /// </summary>
    public class MobileProductDetailDimmers : ProductDetailDimmersBase
    {
        public MobileProductDetailDimmers(IBrowser browser) : base(browser) { }

        #region CSS Selectors
        public override string BuildFullSystemDrawerXpath { get; } = "//*[@id='pdBuildFullSystemCollapsibleButton']";
        public override string BuildFullSystemId { get; } = "pdBuildFullSystemCollapsibleButton";
        public override string BuildFullSystemContainerId { get; } = "buildFullSystemContainer";
        public override string BuildFullSystemOptionsClass { get; } = "unstyledList";
        public override string MultiOptionMenuOpenId { get; } = "prodOptionsMenuOpen";

        public override string BuildFullSystemOptionsId => throw new System.NotImplementedException();
        public override string BuildFullSystemSectionId => throw new System.NotImplementedException();
        public override string BuildFullSystemSectionTitleClass => throw new System.NotImplementedException();
        #endregion

        #region Page Elements

        public override IElement BuildFullSystemDrawer => Browser.Locate.ElementByXpath(BuildFullSystemDrawerXpath);
        public override IElement BuildFullSystemOptions => Browser.Locate.ElementBySelector($"{BuildFullSystemContainerId.ToCssIdSelector()} {BuildFullSystemOptionsClass.ToCssClassSelector()}");

        public override string BuildFullSystemSectionTitle => Browser.Locate.ElementBySelector($"{HtmlTextWriterTag.Span}", Browser.Locate.ElementById(BuildFullSystemId)).Text;
        public override string BuildFullSystemTableFirstSku => Browser.Locate.ElementBySelector($"{HtmlTextWriterTag.Li} {HtmlTextWriterTag.Div} {HtmlTextWriterTag.Div} {HtmlTextWriterTag.Img}", (Browser.Locate.ElementBySelector($"{BuildFullSystemContainerId.ToCssIdSelector()} {BuildFullSystemOptionsClass.ToCssClassSelector()}"))).GetAttribute("data-sku");

        public override IElement SelectedMultiProductDropdownOption => Browser.Locate.ElementById(MultiOptionMenuOpenId);

        public override ReadOnlyCollection<IElement> ListOfFullSystemSkus => Browser.Locate.ElementsBySelector($"{HtmlTextWriterTag.Li}", BuildFullSystemOptions);

        public override List<string> GetListOfFullSystemSkus => ListOfFullSystemSkus.Select(item => item.GetAttribute("data-shortsku")).ToList();
        #endregion
    }
}
