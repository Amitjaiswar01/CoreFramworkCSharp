namespace LampsPlus.AutomationFramework.Workflow
{
    /// <summary>
    /// Workflow to provide common actions for submitting orders.
    /// </summary>
    public interface ISubmittingOrdersWorkflow
    {
        /// <summary>
        /// Submit Order with PaymentMethod Check with items in current cart.
        /// </summary>
        /// <param name="poNumber"></param>
        /// <returns>Order Id</returns>
        string EmployeePlacesOrderForCurrentCartWithPoPayment(string poNumber = "123");

        /// <summary>
        /// Submit Order with PaymentMethod Check with items in current cart.
        /// </summary>
        /// <param name="poNumber"></param>
        /// <returns>Order Id</returns>
        string EmployeePlacesOrderForCurrentCartWithPurchaseOrderPaymentMethod(string poNumber = "123");

        /// <summary>
        /// Employee places an order using the P.O. Payment type for a SKU searched on the Lamps Plus site.
        /// </summary>
        /// <param name="poNumber">String to enter into the PO field.</param>
        /// <param name="searchedSku">Sku to search for. If no Sku is provided a random short sku will be found in the database.</param>
        void EmployeePlacesOrderForSearchedSkuWithPoPayment(string poNumber = "123", string searchedSku = "");
        
        /// <summary>
        /// User enters credit card information into the fields.
        /// </summary>
        void FillCcInfo();
    }
}
