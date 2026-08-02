namespace LampsPlus.AutomationFramework.Databases.Queries.Orders
{
    /// <summary>
    /// Query to identify an LP(9003) order that was placed within 90 days
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T275
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T827
    /// </summary>
    public class OrderPlacedInLast60Days
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 g.orderid, 
                                                 d.emailaddress 
                                    FROM   assets.dbo.tblglobalorderheader g
                                           INNER JOIN domexportorder.dbo.tbldomexportorderheader d  
                                                   ON g.orderid = d.orderid 
                                    WHERE  g.orderdate >= Getdate() - 60 --  (7/13/22) - Changed value to 60 to accommodate Request RMA status change
                                           AND g.sublocation = 9003 
                                           AND Len(g.orderid) >= 21 
                                           AND Orderstatus = 'Shipped' -- (7/18/22) - Changed to 'Shipped' to accommodate Request RMA status change.
                                    ORDER  BY Newid() 
                                    ";
    }
}
