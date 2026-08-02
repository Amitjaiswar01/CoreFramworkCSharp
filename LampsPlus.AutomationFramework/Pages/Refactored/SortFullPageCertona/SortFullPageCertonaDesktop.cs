using System.Collections.ObjectModel;
using OpenQA.Selenium;
using Automation.Framework;
using LampsPlus.AutomationFramework.Utilities;

namespace LampsPlus.AutomationFramework.Pages.Refactored.SortFullPageCertona
{
    public class SortFullPageCertonaDesktop : ISortFullPageCertonaDesktop
    {
        //Class members
        private string _certonaItemsXpath = "//*[@id='certonaItems']";
        private string _sortAddToCartClass = "ProductSortItemAddToCartButton";
        private string _ProductSortImageClass = "ProductSortImage";

        private IElement SortAddToCart => Browser.Locate.ElementByClassName(_sortAddToCartClass);
        private IElement ProdInfo (int index) => Browser.Locate.ElementsByClassName(_ProductSortImageClass)[index];
        protected IElement FullPageCertonaSimilarDesignsContainer => Browser.Locate.ElementByXpath(_certonaItemsXpath);
        private ReadOnlyCollection<IElement> ProductSortImage => Browser.Locate.ElementsByClassName(_ProductSortImageClass);

        //Instances
        protected IBrowser Browser;

        public SortFullPageCertonaDesktop(IBrowser browser)
        {
            Browser = browser;
        }

        public bool IsCurrentPage => Browser.Wait.IsVisibleElement(By.ClassName(_ProductSortImageClass));

        //Interface implementation
        public string PageTitle { get; }
        public string PageUrl { get; }

        public void AddToCartOnCertonaSortPage()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(_sortAddToCartClass));
            SortAddToCart.Click();
            Browser.Wait.ForDomReady();
        }

        public int GetNumberOfProductsOnPage()
        {
            return ProductSortImage.Count;
        }

        public ProductModel GetProductContentsOnPage(int index)
        {
            ProductModel randomSortProduct = null;

            randomSortProduct = new ProductModel
            {
                Sku = ProdInfo(index).GetAttribute("data-sku"),
                Price = ProdInfo(index).GetAttribute("data-price"),
                Name = ProdInfo(index).GetAttribute("data-name")
            };

            return randomSortProduct;
        }
    }
}