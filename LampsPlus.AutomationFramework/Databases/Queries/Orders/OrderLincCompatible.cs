namespace LampsPlus.AutomationFramework.Databases.Queries.Orders
{
    /// <summary>
    /// Query to get order details
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T275
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T827
    /// </summary>
    public class OrderLincCompatible
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT si.orderid, 
                                           si.shortsku,
                                           si.pickupfromstore, 
                                           cc.linccompatible, 
                                           si.shiptocountry,
										   si.itemstatus
                                    FROM   carteasy.dbo.tblshareditems si 
                                        INNER JOIN carteasy.dbo.tblcarriercodes cc 
                                                ON cc.shipviacode = si.shipvia OR shipviahomedeliverycode = si.shipvia 
                                    WHERE  si.orderid = '<ORDERID>'
                                    ";
    }
}
