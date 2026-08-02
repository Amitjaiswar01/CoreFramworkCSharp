using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.UI;
using Automation.Framework;
using Automation.Framework.Utilities;
using Page = Automation.Framework.Core.Page;

namespace LampsPlus.AutomationFramework.Pages.Base
{
    /// <summary>
    /// Base class for common PDP  Color Plus behavior between desktop and mobile views.
    /// </summary>
    public abstract class ProductDetailDimmersBase : Page, IProductDetailDimmers
    {
        protected ProductDetailDimmersBase(IBrowser browser) : base(browser) { }

        #region CSS Selectors
        private string BuildFullSystemTableTitleId { get; } = "pdFullSystemOptionsTitle";

        public abstract string BuildFullSystemId { get; }
        public abstract string BuildFullSystemContainerId { get; }
        public abstract string BuildFullSystemDrawerXpath { get; }
        public abstract string BuildFullSystemOptionsClass { get; }
        public abstract string BuildFullSystemOptionsId { get; }
        public abstract string BuildFullSystemSectionId { get; }
        public abstract string BuildFullSystemSectionTitle { get; }
        public abstract string BuildFullSystemSectionTitleClass { get; }
        public abstract string BuildFullSystemTableFirstSku { get; }
        public abstract string MultiOptionMenuOpenId { get; }
        #endregion

        #region Page Elements
        public string BuildFullSystemTableTitle => Browser.Locate.ElementById(BuildFullSystemTableTitleId).Text;

        public ReadOnlyCollection<IElement> ListOfFullSystemData(int nthIndex) => Browser.Locate.ElementsBySelector($"{HtmlTextWriterTag.Td.ToNthChildSelector(nthIndex)}", BuildFullSystemOptions);
        public abstract IElement BuildFullSystemDrawer { get; }
        public abstract IElement BuildFullSystemOptions { get; }
        public abstract IElement SelectedMultiProductDropdownOption { get; }

        public abstract ReadOnlyCollection<IElement> ListOfFullSystemSkus { get; }

        public abstract List<string> GetListOfFullSystemSkus { get; }
        #endregion
    }
}