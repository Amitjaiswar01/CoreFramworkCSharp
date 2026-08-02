using Automation.Framework;

namespace LampsPlus.AutomationFramework.Utilities
{
    public class SortPageProduct
    {
        private readonly IElement _element;

        /// <summary>
        /// The SKU associated with the product.
        /// </summary>
        public string ProductSku => _element.GetAttribute("data-sku");

        /// <summary>
        /// The product's name.
        /// </summary>
        public string ProductName => _element.GetAttribute("title");

        /// <summary>
        /// The product's price.
        /// </summary>
        public decimal ProductPrice => decimal.Parse(_element.GetAttribute("data-price"));

        /// <summary>
        /// The URL of the image associated with the product.
        /// </summary>
        public string ProductImageUrl => _element.GetAttribute("data-src") ?? _element.GetAttribute("src"); // Sort page uses lazy loading and the actual image URL in data-src may not have been loaded yet to the src

        /// <summary>
        /// Create a <see cref="SortPageProduct"/> page object.
        /// </summary>
        public SortPageProduct(IElement element)
        {
            _element = element;
        }

        /// <summary>
        /// Returns the underlying <see cref="IElement"/>.
        /// </summary>
        public IElement ToElement()
        {
            return _element;
        }
    }
}
