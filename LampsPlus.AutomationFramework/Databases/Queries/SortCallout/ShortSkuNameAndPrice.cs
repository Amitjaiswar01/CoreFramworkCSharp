namespace LampsPlus.AutomationFramework.Databases.Queries.SortCallout
{
    /// <summary>
    /// Query to identify an item that has a quantity less than 20. Inventory is less than 20. The SKU is not part of a grouping SKU.
    /// The item is on Clearance or a Daily Sale item.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T198
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1175
    /// </summary>
    class ShortSkuNameAndPrice
    {
        public const string Query = @"
	                                SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT p.shortsku,
		                                    p.ProductName,
		                                    p.SalePrice1Internet
	                                    FROM carteasy.dbo.tblprducts p 
	                                    INNER JOIN carteasy.dbo.tblproductsavailability pa 
		                                    ON p.shortsku = pa.shortsku
	                                    INNER JOIN carteasy.dbo.tblprductsextra pe 
		                                    ON p.shortsku = pe.shortsku
	                                    WHERE p.instock = 1
		                                    AND p.inventory <= 20
		                                    AND p.listable = 1
		                                    AND intranetonly = 0
		                                    AND (
			                                    groupingsku = ''
			                                    OR GroupingSku IS NULL
			                                    )
		                                    AND (
			                                    p.clearanceflag = 1
			                                    OR pa.isdecrementable = 1
			                                    )
												AND p.ShortSKU = @shortsku
                                    ";
    }
}
