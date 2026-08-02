namespace LampsPlus.AutomationFramework.Databases.Queries.Shipping
{
    /// <summary>
    /// Query to verify the address information of a person using their email address as an identifier.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T169
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T721
    /// </summary>
    public class LastSavedAddressByEmail
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 up.rewardnumber,
	                                    email,
	                                    sa.firstname,
	                                    sa.lastname,
	                                    sa.address1,
	                                    sa.address2,
	                                    sa.city,
	                                    sa.STATE,
	                                    sa.zip,
	                                    sa.country,
	                                    sa.phonenumber,
	                                    sa.createddate
                                    FROM userprofile.dbo.tbluserprofile up 
                                    INNER JOIN userprofile.dbo.aspnet_membership m 
	                                    ON up.userid = m.userid
                                    INNER JOIN userprofile.dbo.tblshippingaddress sa 
	                                    ON sa.rewardnumber = up.rewardnumber
                                    WHERE email = @emailAddress
                                    ORDER BY createddate DESC
                                    ";
    }
}
