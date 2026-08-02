using LampsPlus.AutomationFramework.Utilities;

namespace LampsPlus.AutomationFramework.Pages.Refactored.SortFullPageCertona
{
    public interface ISortFullPageCertonaDesktop : IPageObjectModel
    {
        ProductModel GetProductContentsOnPage(int index);
        int GetNumberOfProductsOnPage();
        void AddToCartOnCertonaSortPage();
    }
}