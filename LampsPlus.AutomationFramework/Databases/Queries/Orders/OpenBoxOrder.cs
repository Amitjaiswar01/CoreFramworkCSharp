namespace LampsPlus.AutomationFramework.Databases.Queries.Orders
{
    /// <summary>    /// Query to get open box order    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7646    /// </summary>    public class OpenBoxOrder    {        public const string Query = @"                                    SET TRANSACTION isolation level READ uncommitted;

                                    SELECT TOP 1 goh.orderid,
                                                 d.emailaddress
                                    FROM   carteasy.dbo.tblshareditems si
                                           LEFT OUTER JOIN domexportorder.dbo.tbldomexportorderheader d
                                                        ON si.orderid = d.orderid
                                           LEFT OUTER JOIN assets.dbo.tblglobalorderheader goh
                                                        ON goh.orderid = si.orderid
										   INNER JOIN carteasy.dbo.tblprductsextra px
														ON px.shortsku = si.shortsku
                                    WHERE  goh.orderdate >= Getdate() - 60 -- (7/13/22) - Changed value to 60 to accommodate Request RMA status change.
                                           AND goh.sublocation = 9003
                                           AND Len(goh.orderid) >= 21
                                           AND d.orderstatus = 'Shipped' -- (7/18/22) - Changed to 'Shipped' to accommodate Request RMA status change.
                                           AND si.isopenbox = 1
		 							       AND px.IsLpProduct = 0 -- (3/29/23) - Added to avoid selecting LP Product SKUs which are grouped differently on the Order History page.
                                    ";    }
}
