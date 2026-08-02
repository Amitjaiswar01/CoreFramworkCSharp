// ReSharper disable StringLiteralTypo
namespace LampsPlus.AutomationFramework.Databases.Queries.SubmittingOrders
{
    /// <summary>
    /// Query to find hold reasons on an order.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T135
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T687
    /// </summary>
    public class FindOrderHoldReasons
    {
        public static string Query(string orderId) => $@"
                                                        SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
														SELECT Description
                                                        FROM   assets.dbo.tblglobalorderholdreasons ohr 
                                                               INNER JOIN carteasy.dbo.tblholdreasoncodes hr 
                                                                       ON ohr.holdreasoncodeid = hr.holdreasoncodeid
                                                        WHERE  ohr.orderid = '{orderId}' 
                                                        ";
    }
}
