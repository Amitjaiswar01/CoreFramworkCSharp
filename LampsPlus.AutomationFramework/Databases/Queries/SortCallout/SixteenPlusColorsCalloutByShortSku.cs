namespace LampsPlus.AutomationFramework.Databases.Queries.SortCallout
{
    /// <summary>
    /// Query to verify that a SKU has the 16+ Color badge.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T206
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1183
    /// </summary>
    public class SixteenPlusColorsCalloutByShortSku
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT shortsku,
	                                    callout
                                    FROM products.dbo.tblproductsearchcallouts 
                                    WHERE shortsku = @ShortSku
                                    ";
    }
}
