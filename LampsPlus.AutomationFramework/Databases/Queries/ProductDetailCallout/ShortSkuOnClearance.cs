namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetailCallout
{
    /// <summary>
    /// Query to identify a SKU with limited inventory. The inventory must be between 1 and 20. The Clearance Flag in the database must be set to '1'.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T247
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1225
    /// </summary>
    public class ShortSkuOnClearance
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 p.shortsku,
	                                    inventory
                                    FROM carteasy.dbo.tblprducts p 
                                    INNER JOIN carteasy.dbo.tblprductsextra pe 
                                        ON p.shortsku = pe.shortsku
                                    INNER JOIN carteasy.dbo.tblproductsavailability pa 
	                                    ON p.shortsku = pa.shortsku
                                    WHERE 0.10 >= Cast(Checksum(Newid(), p.shortsku) & 0x7FFFFFFF AS FLOAT) / Cast(0x7FFFFFFF AS INT)
	                                    AND p.instock = 1
	                                    AND p.inventory BETWEEN 2 -- (9/13/21) Quantity dropdown only appears when the quantity left is between 2 and 20.
		                                    AND 20
	                                    AND p.listable = 1
	                                    AND specialorder = 0
	                                    AND intranetonly = 0
	                                    AND p.clearanceflag = 1
                                        AND ( pe.groupingsku IS NULL OR pe.groupingsku = '')
                                    ";
    }
}
