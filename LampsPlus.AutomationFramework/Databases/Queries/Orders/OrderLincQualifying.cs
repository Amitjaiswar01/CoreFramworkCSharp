namespace LampsPlus.AutomationFramework.Databases.Queries.Orders
{
    /// <summary>
    /// Query to get Linc qualifying orders.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T287
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T839
    /// </summary>
    public class OrderLincQualifying
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT top 1 si.orderid, 
                                     dd.emailaddress, 
                                     dd.createddate, 
                                     si.shortsku, 
                                     si.pickupfromstore, 
                                     cc.linccompatible, 
                                     si.shiptocountry 
                                     FROM   carteasy.dbo.tblshareditems si  
                                     INNER JOIN carteasy.dbo.tblcarriercodes cc  
                                             ON cc.shipviacode = si.shipvia
                                                 OR cc.shipviahomedeliverycode = si.shipvia
                                     INNER JOIN domexportorder.dbo.tbldomexportorderheader dd  
                                             ON dd.orderid = si.orderid 
                                     INNER JOIN assets.dbo.tblglobalpayment gpp  
                                             ON gpp.orderid = si.orderid 
              		   		         INNER Join assets.dbo.tblglobalorderheader g  
                  		                     ON g.orderid = si.orderid 
                                     WHERE  0.10 >= Cast(Checksum(Newid(), si.orderid) & 0x7fffffff AS FLOAT) / Cast 
                                             ( 
                                                    0x7fffffff AS INT) 
                                     AND shiptocountry = 'US' 
                                     AND ( pickupfromstore = 0 
                                     OR pickupfromstore = NULL ) 
                                     AND gpp.paymentmethod != 'PayPal' 
                                     AND dd.orderstatus != 'Canceled' 
                                     AND dd.cashieremployee != '7777' 
                                     AND dd.createddate > '2017-10-25' 
                                     AND g.ManualDiscount = '0'
                                     AND g.ProfessionalDiscount = '0' 
                                     AND cc.linccompatible = 1
                                    ";
    }
}
