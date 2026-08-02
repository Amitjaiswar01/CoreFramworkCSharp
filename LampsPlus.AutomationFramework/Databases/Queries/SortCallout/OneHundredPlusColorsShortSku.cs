namespace LampsPlus.AutomationFramework.Databases.Queries.SortCallout
{
    /// <summary>
    /// Query to verify that a SKU qualifies for 100+ color badge.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T218
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1195
    /// </summary>
    public class OneHundredPlusColorsShortSku
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT psc.shortsku,
	                                    psc.callout
                                    FROM products.dbo.tblproductsearchcallouts psc 
                                    WHERE psc.shortsku = @shortsku
	                                    AND psc.callout = '100+ Colors'
                                    ";
    }
}
