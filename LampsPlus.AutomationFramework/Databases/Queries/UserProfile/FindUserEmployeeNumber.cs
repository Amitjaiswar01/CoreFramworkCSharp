namespace LampsPlus.AutomationFramework.Databases.Queries.UserProfile
{
    /// <summary>
    /// Query to get Account Employee Number by Email.
    /// </summary>
    public class FindUserEmployeeNumber
    {
        public static string Query(string email) => $@"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT employeenumber
                                    FROM   [UserProfile]..[aspnet_users]  au 
                                           INNER JOIN userprofile..tbluserprofile  up 
                                                   ON au.userid = up.userid 
                                    WHERE  au.username = '{email}'
                                    ";
    }
}
