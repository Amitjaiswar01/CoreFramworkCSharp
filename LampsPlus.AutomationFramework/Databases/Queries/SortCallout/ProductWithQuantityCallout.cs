namespace LampsPlus.AutomationFramework.Databases.Queries.SortCallout
{
    /// <summary>
    /// Query to identify a SKU that has the Quantity left callout. The inventory must be below 20. The item must be on Clearance.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T203
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1180
    /// </summary>
    public class ProductWithQuantityCallout
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 p.shortsku,
	                                    (p.inventory - pa.numbersoldtoday) AS CurrentInventory
                                    FROM carteasy.dbo.tblprducts p 
                                    INNER JOIN carteasy.dbo.tblproductsavailability pa 
	                                    ON p.shortsku = pa.shortsku
                                    WHERE 0.10 >= Cast(Checksum(Newid(), p.shortsku) & 0x7FFFFFFF AS FLOAT) / Cast(0x7FFFFFFF AS INT)
	                                    AND Charindex('-', p.shortsku) = 0
	                                    AND p.instock = 1
	                                    AND p.inventory <= 20
	                                    AND p.listable = 1
	                                    AND intranetonly = 0
	                                    AND p.clearanceflag = 1
	                                    AND NOT EXISTS (
		                                    SELECT 1
		                                    FROM carteasy.dbo.tblcombosku 
		                                    WHERE p.shortsku = basesku
		                                    )	                                    
                                    ";
    }
}
