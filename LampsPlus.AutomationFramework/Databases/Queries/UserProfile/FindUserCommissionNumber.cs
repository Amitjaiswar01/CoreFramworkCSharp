namespace LampsPlus.AutomationFramework.Databases.Queries.SubmittingOrders
{
    /// <summary>
    /// Query to get EmployeeId by Email.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T294
    /// </summary> https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1047
    public class FindUserCommissionNumber
    {
        public static string Query(string email) => $@"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT commissionemployeenumber
                                    FROM   [UserProfile]..[aspnet_users] au 
                                           INNER JOIN userprofile..tbluserprofile up 
                                                   ON au.userid = up.userid 
                                    WHERE  au.username = '{email}'
                                    ";
    }
}
