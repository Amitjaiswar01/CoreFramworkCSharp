namespace LampsPlus.AutomationFramework.Databases.Queries.ManageAccount
{
    /// <summary>
    /// Query to verify that the database columns for phone numbers get updated correctly when adjusting account information. 
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T306
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T858
    /// </summary>
    public class UserPhoneInfo
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT up.phonenumber,
	                                    up.cellphonenumber,
	                                    up.fax
                                    FROM UserProfile.dbo.tbluserprofile up
                                    INNER JOIN UserProfile.dbo.aspnet_membership m
	                                    ON up.userid = m.userid
                                    WHERE m.email = @Email
                                    ";
    }
}
