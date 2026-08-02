using System.Collections.Generic;

namespace LampsPlus.AutomationFramework.Pages.Refactored.ProductDetailDimmers
{
    public interface IProductDetailDimmersDesktop : IPageObjectModel 
    {
        void NavigateToBuildFullSystemSection();
        bool IsBuildFullSystemDisplayed();
        string GetBuildFullSystemSectionTitle { get; }
        string GetBuildFullSystemTableFirstSku { get; }
        List<string> GetListOfFullSystemSkus { get; }
    }
}