namespace LampsPlus.AutomationFramework.Databases.Queries.Orders
{
    /// <summary>
    /// Query that returns 4 orders, one for each status.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T283
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T835
    /// </summary>
    public class OrderForEachStatus
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT orderid, 
                                           orderstatus, 
                                           emailaddress 
                                    FROM   (SELECT TOP 1 domoh.orderid, 
                                                         domoh.orderstatus, 
                                                         emailaddress, 
                                                         Count(*) thecount 
                                            FROM   domexportorder.dbo.tbldomexportorderheader domoh  
                                                   INNER JOIN carteasy.dbo.tblshareditems cesi  
                                                           ON domoh.orderid = cesi.orderid 
                                            WHERE  domoh.sublocation = 9003 
                                                   AND domoh.orderdate >= Getdate() - 30 
                                                   AND domoh.orderstatus = 'Shipped' 
                                                   AND Len(domoh.orderid) = 21 
                                            GROUP  BY domoh.orderid, 
                                                      domoh.orderstatus, 
                                                      domoh.emailaddress 
                                            HAVING Count(*) = 1 
                                            ) ship 
                                    UNION ALL
                                    SELECT orderid, 
                                           orderstatus, 
                                           emailaddress 
                                    FROM   (SELECT TOP 1 domoh.orderid, 
                                                         domoh.orderstatus, 
                                                         emailaddress, 
                                                         Count(*) thecount 
                                            FROM   domexportorder.dbo.tbldomexportorderheader domoh  
                                                   INNER JOIN carteasy.dbo.tblshareditems cesi  
                                                           ON domoh.orderid = cesi.orderid 
                                            WHERE  domoh.sublocation = 9003 
                                                   AND domoh.orderdate >= Getdate() - 30 
                                                   AND domoh.orderstatus = 'Canceled' 
                                                   AND Len(domoh.orderid) = 21 
                                            GROUP  BY domoh.orderid, 
                                                      domoh.orderstatus, 
                                                      domoh.emailaddress 
                                            HAVING Count(*) = 1 
                                            ) canc 
                                    UNION ALL
                                    SELECT orderid, 
                                           orderstatus, 
                                           emailaddress 
                                    FROM   (SELECT TOP 1 domoh.orderid, 
                                                         domoh.orderstatus, 
                                                         emailaddress, 
                                                         Count(*) thecount 
                                            FROM   domexportorder.dbo.tbldomexportorderheader domoh  
                                                   INNER JOIN carteasy.dbo.tblshareditems cesi  
                                                           ON domoh.orderid = cesi.orderid 
                                            WHERE  domoh.sublocation = 9003 
                                                   AND domoh.orderdate >= Getdate() - 30 
                                                   AND domoh.orderstatus = 'Pickedup' 
                                                   AND Len(domoh.orderid) = 21 
                                            GROUP  BY domoh.orderid, 
                                                      domoh.orderstatus, 
                                                      domoh.emailaddress 
                                            HAVING Count(*) = 1 
                                            ) pick 
                                    UNION ALL
                                    SELECT orderid, 
                                           orderstatus, 
                                           emailaddress 
                                    FROM   (SELECT TOP 1 domoh.orderid, 
                                                         domoh.orderstatus, 
                                                         emailaddress, 
                                                         Count(*) thecount 
                                            FROM   domexportorder.dbo.tbldomexportorderheader domoh  
                                                   INNER JOIN carteasy.dbo.tblshareditems cesi  
                                                           ON domoh.orderid = cesi.orderid 
                                            WHERE  domoh.sublocation = 9003 
                                                   AND domoh.orderdate >= Getdate() - 30 
                                                   AND domoh.orderstatus IN ( 'Backorder', 'Pending' ) 
                                                   AND Len(domoh.orderid) = 21 
                                            GROUP  BY domoh.orderid, 
                                                      domoh.orderstatus, 
                                                      domoh.emailaddress 
                                            HAVING Count(*) = 1 
                                            ) pendbackord 
                                    ";
    }
}
