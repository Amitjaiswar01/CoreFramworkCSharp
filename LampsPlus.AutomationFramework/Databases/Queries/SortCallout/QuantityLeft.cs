namespace LampsPlus.AutomationFramework.Databases.Queries.SortCallout
{
    /// <summary>
    /// Query to verify the quantity left for a SKU.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T201
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1178
    /// </summary>
    public class QuantityLeft
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT p.shortsku,
	                                    p.inventory,
	                                    manufacturer,
	                                    productname,
	                                    (p.inventory - pa.numbersoldtoday) AS CurrentInventory
                                    FROM carteasy.dbo.tblprducts p 
                                    INNER JOIN carteasy.dbo.tblproductsavailability pa 
	                                    ON p.shortsku = pa.shortsku
                                    WHERE p.shortsku = @shortsku
                                    ";
    }
}
