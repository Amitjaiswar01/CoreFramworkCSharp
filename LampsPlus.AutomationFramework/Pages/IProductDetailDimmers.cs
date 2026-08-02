using System.Collections.Generic;
using System.Collections.ObjectModel;
using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages
{
    /// <summary>
    /// Interface for common PDP Color Plus behavior between desktop and mobile views.
    /// </summary>
    public interface IProductDetailDimmers
    {
        #region Page Elements
        string BuildFullSystemId { get; }
        string BuildFullSystemContainerId { get; }
        string BuildFullSystemDrawerXpath { get; }
        string BuildFullSystemOptionsClass { get; }
        string BuildFullSystemOptionsId { get; }
        string BuildFullSystemSectionId { get; }
        string BuildFullSystemSectionTitle { get; }
        string BuildFullSystemSectionTitleClass { get; }
        string BuildFullSystemTableFirstSku { get; }
        string MultiOptionMenuOpenId { get; }

        IElement BuildFullSystemDrawer { get; }
        IElement BuildFullSystemOptions { get; }
        IElement SelectedMultiProductDropdownOption { get; }
        
        ReadOnlyCollection<IElement> ListOfFullSystemData(int nthIndex);
        ReadOnlyCollection<IElement> ListOfFullSystemSkus { get; }

        List<string> GetListOfFullSystemSkus { get; }
        #endregion
    }
}
