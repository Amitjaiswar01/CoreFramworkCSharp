namespace LampsPlus.AutomationFramework.Databases.Queries.ShoppingCart
{
    /// <summary>
    /// Query to get cart summary values by given CartId
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T589
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1143
    /// </summary>
    public class GetCartTotalValues
    {
        public static string Query(string cartId) => $@"
                                                        SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
														SELECT  ItemTotal, FreightTotal, TaxTotal, OrderTotal
                                                        FROM assets.dbo.tblglobalcart 
                                                        WHERE id = '{cartId}'";
    }
}
