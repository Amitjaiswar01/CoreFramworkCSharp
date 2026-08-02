namespace LampsPlus.AutomationFramework.Databases.Queries.Orders
{
	/// <summary>
	/// Query to get PayPal order details
	/// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T286
	/// </summary>
	public class OrderWithPayPal
	{
		public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 g.orderid, 
                                                 Count(g.orderid)   AS NumberOfRows, 
                                                 d.createddate, 
                                                 d.emailaddress, 
                                                 gp.paymentmethod, 
                                                 s.shiptofirstname, 
                                                 s.shiptolastname, 
                                                 s.shiptoaddressline1, 
                                                 s.shiptoaddressline2, 
                                                 s.shiptocity, 
                                                 s.shiptostate, 
                                                 s.shiptozipcode, 
                                                 s.shiptocountry, 
                                                 s.productname, 
                                                 s.shortsku, 
                                                 d.orderstatus, 
                                                 s.origitemshiptype AS TrackingType, 
                                                 s.trackingnumber, 
                                                 s.quantity, 
                                                 S.price            AS UnitPrice, 
                                                 g.itemtotal, 
                                                 d.manualdiscount   AS PriceAdjustment, 
                                                 d.freighttotal     AS 'S&P', 
                                                 g.taxtotal, 
                                                 d.ordertotal 
                                    FROM   assets.dbo.tblglobalorderheader g  
                                           INNER JOIN domexportorder.dbo.tbldomexportorderheader d  
                                                   ON g.orderid = d.orderid 
                                           INNER JOIN assets.dbo.tblglobalpayment gp  
                                                   ON gp.orderid = g.orderid 
                                           INNER JOIN carteasy.dbo.tblshareditems s  
                                                   ON s.orderid = g.orderid 
                                    WHERE  0.10 >= Cast(Checksum(Newid(), g.orderid) & 0x7FFFFFFF AS FLOAT) / Cast( 
                                                          0x7FFFFFFF AS INT) 
                                           AND paymentmethod = 'PayPal' 
                                           AND orderstatus != 'Canceled' 
                                           AND g.rewardnumber IS NOT NULL 
                                           AND IsOpenBox = 0 -- (6/16/21) Added to avoid selecting Open Box orders because We test those orders in a different test.
                                    GROUP  BY g.orderid, 
                                              d.createddate, 
                                              d.emailaddress, 
                                              gp.paymentmethod, 
                                              s.shiptofirstname, 
                                              s.shiptolastname, 
                                              s.shiptoaddressline1, 
                                              s.shiptoaddressline2, 
                                              s.shiptocity, 
                                              s.shiptostate, 
                                              s.shiptozipcode, 
                                              s.shiptocountry, 
                                              s.productname, 
                                              s.shortsku, 
                                              d.orderstatus, 
                                              s.origitemshiptype, 
                                              s.trackingnumber, 
                                              s.quantity, 
                                              S.price, 
                                              g.itemtotal, 
                                              d.manualdiscount, 
                                              d.freighttotal, 
                                              g.taxtotal, 
                                              d.ordertotal 
                                    HAVING Count(g.orderid) = 1 
                                    ORDER  BY createddate DESC 
                                     ";
	}
}
