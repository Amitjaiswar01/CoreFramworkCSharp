namespace LampsPlus.AutomationFramework.Databases.Queries.UserProfile
{
    /// <summary>
    /// Query to verify that an account has been entered into the database but is not verified. The IsApproved column value should be '0'.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T294
    /// </summary> https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1047
    public class UserProfile
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT IsApproved,
	                                    Email
                                    FROM userprofile.dbo.aspnet_Membership am 
                                    INNER JOIN userprofile.dbo.tblUserProfile up 
	                                    ON up.UserId = am.UserId
                                    INNER JOIN userprofile.dbo.aspnet_Users au 
	                                    ON au.UserId = up.UserId
                                    WHERE am.Email = @Email
                                    ";
    }
}
