using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Pages.Base;

namespace LampsPlus.AutomationFramework.Pages.Desktop
{
    /// <summary>
    /// Example page: https://www.lampsplus.com/products/samba-china-red-silk-shade-apothecary-table-lamp__29j32.html
    /// </summary>
    public class ProductDetailDimmers : ProductDetailDimmersBase
    {
        public ProductDetailDimmers(IBrowser browser) : base(browser) { }

        #region CSS Selector Strings
        public override string BuildFullSystemOptionsId { get; } = "pdFullSystemOptions";
        public override string BuildFullSystemSectionId { get; } = "build-full-system";
        public override string BuildFullSystemSectionTitleClass { get; } = "pdSectionTitle";

        public override string BuildFullSystemId => throw new NotImplementedException();
        public override string BuildFullSystemContainerId => throw new NotImplementedException();
        public override string BuildFullSystemDrawerXpath => throw new NotImplementedException();
        public override string BuildFullSystemOptionsClass => throw new NotImplementedException();
        public override string MultiOptionMenuOpenId => throw new NotImplementedException();
        #endregion

        #region Page Elements
        public override IElement BuildFullSystemOptions => Browser.Locate.ElementById(BuildFullSystemOptionsId);
        public override string BuildFullSystemSectionTitle => Browser.Locate.ElementByClassName(BuildFullSystemSectionTitleClass, Browser.Locate.ElementById(BuildFullSystemSectionId)).Text;
        public override string BuildFullSystemTableFirstSku => Browser.Locate.ElementBySelector($"{HtmlTextWriterTag.Tr.ToNthChildSelector(3)} {HtmlTextWriterTag.Td.ToNthChildSelector(3)}", Browser.Locate.ElementById(BuildFullSystemOptionsId)).Text;
        public override IElement SelectedMultiProductDropdownOption => Browser.Locate.ElementByTagNameAndAttributeEquals(HtmlTextWriterTag.Label, HtmlTextWriterAttribute.For, "dimmerOnlyOption");

        public override IElement BuildFullSystemDrawer => throw new NotImplementedException();

        public override ReadOnlyCollection<IElement> ListOfFullSystemSkus => ListOfFullSystemData(3);

        public override List<string> GetListOfFullSystemSkus => ListOfFullSystemSkus.Select(item => item.Text).ToList();
        #endregion
    }
}
