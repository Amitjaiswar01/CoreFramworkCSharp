namespace LampsPlus.AutomationFramework.Databases.Queries.Orders
{
    /// <summary>
    /// Query that returns Manual Discount and Freight Totals.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T140
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T692
    /// </summary>
    public class OrderDiscountAndFreightTotals
    {
        public static string Query(string orderId) => $@"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT OrderID, ManualDiscount, FreightTotal
                                    FROM   assets.dbo.tblglobalorderheader 
                                    WHERE orderid = '{orderId}'
                                    ";
    }
}
