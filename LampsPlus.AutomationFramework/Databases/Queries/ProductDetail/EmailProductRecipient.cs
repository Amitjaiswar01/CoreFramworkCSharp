namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify that an email has been entered in the database.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T241
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1219
    /// </summary>
    public class EmailProductRecipient
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT *
                                    FROM userprofile.dbo.tblemailrecepients er 
                                    WHERE er.dateadded BETWEEN DATEADD(dd, DATEDIFF(dd, 0, Getdate()), 0)
		                                    AND DATEADD(dd, DATEDIFF(dd, 0, DATEADD(day, 1, Getdate())), 0)
	                                    AND er.sublocationcode = 9003
	                                    AND er.emailaddress = @email
                                    ";
    }
}
