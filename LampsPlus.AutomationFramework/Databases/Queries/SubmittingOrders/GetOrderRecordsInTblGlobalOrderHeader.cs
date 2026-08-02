namespace LampsPlus.AutomationFramework.Databases.Queries.SubmittingOrders
{
    /// <summary>
    /// Query to find an order in TblGlobalOrderHeader in the Assets database.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T145
    /// </summary>
    public class GetOrderRecordsInTblGlobalOrderHeader
    {
        public static string Query(string orderId) => $@"
                                                        SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
														SELECT goh.orderid,
                                                            shortsku,
                                                            productname,
                                                            goh.itemtotal AS 'ItemTotal',
                                                            freighttotal AS 'SAndP',
                                                            taxtotal AS 'TaxTotal',
                                                            ordertotal
                                                        FROM   [Assets].[dbo].[tblglobalcart] goh 
                                                        INNER JOIN assets.dbo.tblcartshareditems si 
                                                        ON si.orderid = goh.orderid
                                                        WHERE goh.orderid = '{orderId}'";
    }
}

