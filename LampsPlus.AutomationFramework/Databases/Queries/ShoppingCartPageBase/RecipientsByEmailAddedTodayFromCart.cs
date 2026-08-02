namespace LampsPlus.AutomationFramework.Databases.Queries.ShoppingCartPageBase
{
    /// <summary>
    /// Query to identify email records added for the current date to the database using the "CartOverview" source.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T106 and https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T112
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T658 and https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T664
    /// </summary>
    public class RecipientsByEmailAddedTodayFromCart
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									DECLARE @bod DATETIME = Cast(Getdate() AS DATE)
                                    DECLARE @eod DATETIME = @bod + '23:59:59'

                                    SELECT DISTINCT er.emailaddress
                                    FROM userprofile..tblemailrecepients er
                                    INNER JOIN userprofile.dbo.tblsource ts
	                                    ON er.sourceid = ts.id
                                    WHERE er.dateadded BETWEEN @bod
		                                    AND @eod
	                                    AND er.sublocationcode = 9003
	                                    AND ts.source = 'ShoppingCart' -- (1/6/23) Changed to 'ShoppingCart' as tblsource does not return value for CartOverview 
	                                    AND (er.emailaddress IN (@RecipientEmails))
                                    ";
    }
}
