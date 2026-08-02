namespace LampsPlus.AutomationFramework.Databases.Queries.Orders
{
    /// <summary>
    /// Query to get order detail values used on Order History page. Does not return cancelled or PayPal orders.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T281
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T833
    /// </summary>
    public class OrderHistoryOrderDetails
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT d.orderid, 
                                       d.rewardnumber, 
                                       d.createddate, 
                                       d.emailaddress, 
                                       d.commissionemployee AS SalesAssociate, 
                                       gp.billtofirstname, 
                                       gp.billtolastname, 
                                       gp.billtoaddressline1, 
                                       gp.billtoaddressline2, 
                                       gp.billtocity, 
                                       gp.billtostate, 
                                       gp.billtozipcode, 
                                       gp.billtocountry, 
                                       gp.billtophonenumber, 
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
                                       s.origitemshiptype   AS TrackingType, 
                                       s.trackingnumber, 
                                       s.quantity, 
                                       S.price              AS UnitPrice, 
                                       d.itemtotal, 
                                       d.manualdiscount     AS PriceAdjustment, 
                                       d.freighttotal       AS 'S&P', 
                                       d.taxtotal, 
                                       d.ordertotal, 
                                       paymentmethod  
                                FROM   domexportorder.dbo.tbldomexportorderheader d                 
                                       INNER JOIN carteasy.dbo.tblshareditems s 
                                               ON d.orderid = s.orderid 
                                       INNER JOIN assets.dbo.tblglobalpayment gp 
                                               ON gp.orderid = d.orderid 
                                WHERE  d.orderid IN (SELECT TOP 1 dd.orderid 
                                                     FROM   domexportorder.dbo.tbldomexportorderheader dd 
                                                            INNER JOIN assets.dbo.tblglobalpayment gpp 
                                                                    ON gpp.orderid = dd.orderid                            
                                                            INNER JOIN userprofile.dbo.tbluserprofile P 
                                                                    ON P.employeenumber = dd.commissionemployee 
                                                            INNER JOIN carteasy.dbo.tblshareditems ss 
                                                                    ON ss.orderid = dd.orderid 
                                                            INNER JOIN carteasy.dbo.tblcarriercodes cc 
                                                                    ON cc.shiptype = ss.origitemshiptype 
																	   AND cc.shiptype IS NOT NULL 
                                                                       AND Len(cc.shiptype) > 0 
                                                     WHERE 	0.10 >= Cast(Checksum(Newid(), dd.orderid) & 0x7fffffff AS 
                                                                         FLOAT) / Cast ( 
                                                                            0x7fffffff AS INT)												  
															AND cc.linccompatible = 1 
                                                            AND gpp.paymentmethod != 'PayPal' 
                                                            AND dd.orderstatus != 'Canceled' 
                                                            AND dd.commissionemployee != 7777 
                                                            AND dd.orderid != 'AA0106181231197919005'
                                                            AND dd.sublocation = 9003
                                                            AND shiptolastname != ''
															AND shiptofirstname NOT LIKE 'S0%') 		
                                    ";
    }
}
