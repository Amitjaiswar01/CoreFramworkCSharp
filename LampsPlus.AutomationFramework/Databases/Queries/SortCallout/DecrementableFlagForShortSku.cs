namespace LampsPlus.AutomationFramework.Databases.Queries.SortCallout
{
    /// <summary>
    /// Query to verify a SKU qualifies for a Daily Sale callout.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T205
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1182
    /// </summary>
    public class DecrementableFlagForShortSku
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 shortsku,
	                                    isdecrementable
                                    FROM carteasy.dbo.tblproductsavailability 
                                    WHERE shortsku = @shortsku
                                    ";
    }
}
