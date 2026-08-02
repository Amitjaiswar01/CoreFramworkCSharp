namespace LampsPlus.AutomationFramework.Databases.Queries.Orders
{
    /// <summary>
    /// Query to get order details for global order
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T338
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T887
    /// </summary>
    public class GlobalOrderDetails
    {
        public static string Query(string shippingEmail) => $@"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT goh.emailaddress,
                                           up.firstname,
                                           up.lastname,
                                           gp.billtozipcode
                                    FROM   [UserProfile]..[aspnet_users] au 
                                           INNER JOIN userprofile..tbluserprofile up 
                                                   ON au.userid = up.userid
                                           INNER JOIN assets.dbo.tblglobalorderheader goh 
                                                   ON goh.emailaddress = au.username
                                           INNER JOIN assets.dbo.tblglobalpayment gp 
                                                   ON gp.orderid = goh.orderid
                                    WHERE  au.username = '{shippingEmail}'
                                    ";
    }
}
