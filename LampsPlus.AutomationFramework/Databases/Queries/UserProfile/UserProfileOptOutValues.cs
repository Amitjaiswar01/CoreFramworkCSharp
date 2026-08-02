namespace LampsPlus.AutomationFramework.Databases.Queries.UserProfile
{
    /// <summary>
    /// Query to verify that a user profile has been created into the database but is not an account. 
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T291
    /// </summary>
    public class UserProfileOptOutValues
    {
        public const string Query = @"
                                SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
								SELECT o.optoutemail,
                                    o.sublocation,
                                    up.rewardnumber
                                FROM   userprofile.dbo.tbluserprofile up 
                                    INNER JOIN userprofile.dbo.aspnet_users u 
                                            ON up.userid = u.userid
                                    INNER JOIN userprofile.dbo.tbluseroptout o 
                                            ON o.rewardnumber = up.rewardnumber
                                WHERE  u.loweredusername = @Email
                                ";
    }
}
