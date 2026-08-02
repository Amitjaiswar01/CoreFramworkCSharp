namespace LampsPlus.AutomationFramework.Databases.Queries.SortCallout
{
    /// <summary>
    /// Query to verify that a SKU is a Clearance item. 
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T204
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1181
    /// </summary>
    public class ClearancePriceByShortSku
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 clearanceflag,
	                                    retailpriceinternet,
	                                    shortsku
                                    FROM carteasy.dbo.tblprducts 
                                    WHERE shortsku = @shortsku
                                    ";
    }
}
