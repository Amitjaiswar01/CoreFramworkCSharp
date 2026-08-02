using System.Collections.Generic;
using LampsPlus.AutomationFramework.Pages.Base;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.AutomationFramework.Workflow
{
    /// <summary>
    /// Common behavior for the Shopping Cart workflow.
    /// </summary>
    public interface IShoppingCartWorkflow
    {
        /// <summary>
        /// Add multiple items to the cart with the specified list of CartProductAddItem objects. 
        /// NOTE: Only call this method if adding to cart is not your main test.
        /// </summary>
        void AddItemsToCartBySku(List<ProductModel> cartProductAddItems);

        /// <summary>
        /// Employee adds searched sku to the cart and checks out.
        /// </summary>
        /// <param name="searchedSku"></param>
        void AddItemToCartBySearchedSkuAndCheckOut(string searchedSku);

        /// <summary>
        /// Add a single item to the cart with the specified ProductModel object. 
        /// NOTE: Only call this method if adding to cart is not your main test.
        /// </summary>
        void AddItemToCartBySku(ProductModel cartProductAddItem);

        /// <summary>
        /// Add multiple products to the cart.
        /// </summary>
        /// <param name="url">URL to navigate to to add products from.</param>
        /// <param name="numberOfProducts">Number of products to add to the cart.</param>
        void AddMultipleItemsToCart(string url, int numberOfProducts);

        /// <summary>
        /// Add multiple Skus that have a price over $200 to the cart.
        /// </summary>
        /// <param name="numberOfProducts">Number of products to add to the cart.</param>
        void AddMultipleSkuWithPriceOverTwoHundredDollarsToCart(int numberOfProducts);

        /// <summary>
        /// Add a single item to the cart from the Contemporary Floor Lamps section.
        /// </summary>
        void AddSingleItemToCart();

        /// <summary>
        /// Add item to the cart from the given sort page url.
        /// </summary>
        /// <param name="url"></param>
        void AddSingleItemToCart(string url);

        /// <summary>
        /// Apply a discount to the cart.
        /// </summary>
        /// <param name="cartItemIndex">Cart item index to add a discount to.</param>
        /// <param name="percentDiscount">Percentage discount to add to a product.</param>
        void ApplyCartItemDiscount(int cartItemIndex, decimal percentDiscount);

        /// <summary>
        /// Add a single item to the cart and go checkout => go to the shipping page.
        /// </summary>
        void CheckoutWithSingleItem(string shortSku = "");

        /// <summary>
        /// Create a new saved address for logged-in customer using the inline shipping address form on Shipping page.
        /// </summary>
        /// <param name="address">Shipping address object. If not specified or null, default address will be used. (Optional)</param>
        /// <param name="goBackToShippingPage">Boolean flag to indicate if it needs to go back to Shipping page. Defaults to false. (Optional)</param>
        /// <returns></returns>
        Address CreateNewSavedAddress(UserRole userRole, Address address = null, bool goBackToShippingPage = false);

        /// <summary>
        /// Create a new saved address for logged-in customer using the Add New Address modal on Shipping page.
        /// </summary>
        /// <param name="address">Shipping address object. If not specified or null, default address will be used. (Optional)</param>
        /// <param name="shippingNameSuffix">Suffix to add to Shipping First and Last Name to make them unique. Defaults to "FromAutomation". (Optional)</param>
        /// <returns></returns>
        Address CreateNewSavedAddressFromModal(Address address = null, string shippingNameSuffix = "FromAutomation");

        /// <summary>
        /// Fill out check number information and place the order.
        /// </summary>
        void EmployeePlaceOrderViaCheck();

        /// <summary>
        /// Fill out PO information and place the order.
        /// </summary>
        void EmployeePlaceOrderViaPo();

        /// <summary>
        /// Pick wire transfer for payment with default CA address and place the order.
        /// </summary>
        void EmployeePlaceOrderWithDefaultAddressViaWireTransfer();

        /// <summary>
        /// Navigate to the shopping cart and remove all items in the cart.
        /// </summary>
        void EmptyCart();

        /// <summary>
        /// Enter Country, ZIP Code and Shipping Type (optional) for Shipping.
        /// </summary>
        /// <param name="countryCode">Country code to enter in the field.</param>
        /// <param name="zipCode">Zip code to enter in the field.</param>
        /// <param name="shippingType">Shipping type to enter in the field.</param>
        /// <param name="clickUpdateButton">Boolean indicating if the update button should be clicked after entering zip code in modal.</param>
        void EnterCartZipCodeForShipping(string countryCode, string zipCode, string shippingType = null, bool clickUpdateButton = true);

        /// <summary>
        /// Quickly fill out shipping page with default shipping information.
        /// </summary>
        void EnterDefaultShippingAddress(UserRole userRole = default);

        /// <summary>
        /// Is the PLA SKU added to the cart?
        /// </summary>
        /// <param name="url">URL of a PLA sku to navigate to.</param>
        /// <param name="sku">PLA SKU to navigate to.</param>
        /// <returns></returns>
        bool IsPlaSkuAddedToCart(string url, string sku);

        /// <summary>
        /// CLose shipping options.
        /// </summary>
        void CloseShippingOptions();

        /// <summary>
        /// Click the proceed to payment button, wait for the Payment page to load and the global spinner to close.
        /// </summary>
        void ProceedToPayment();

        /// <summary>
        /// Add a single product to the cart, use the default shipping address, wait for the payment page to load, and the global spinner to close.
        /// </summary>
        void ProceedToPaymentWithSingleProduct(string shortSku = "");

        /// <summary>
        /// Show FexEx validation model.
        /// </summary>
        void ShowFedExValidationModal();

        void WaitForNavigation(int index);
    }
}