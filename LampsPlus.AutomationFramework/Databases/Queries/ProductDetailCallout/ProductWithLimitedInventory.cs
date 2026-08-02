namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetailCallout
{
    /// <summary>
    /// Query to find a SKU with an inventory less than 20.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T246
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1224
    /// </summary>
    public class ProductWithLimitedInventory
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 p.shortsku,
	                                    inventory,
	                                    (p.inventory - pa.numbersoldtoday) AS CurrentInventory
                                    FROM carteasy.dbo.tblprducts p 
                                    INNER JOIN carteasy.dbo.tblproductsavailability pa 
	                                    ON p.shortsku = pa.shortsku
                                    INNER JOIN carteasy.dbo.tblprductsextra pe 
	                                    ON pe.shortsku = p.shortsku
                                    WHERE 0.10 >= Cast(Checksum(Newid(), p.shortsku) & 0x7FFFFFFF AS FLOAT) / Cast(0x7FFFFFFF AS INT)
	                                    AND p.instock = 1
	                                    AND p.inventory BETWEEN 2 -- (Updated 8/30/21) Dropdown menu for quantity no longer appears for quantity of only 1 - LP-44936
		                                    AND 20
	                                    AND p.listable = 1
	                                    AND pe.isbopuseligible = 0
                                        AND p.clearanceflag = 1
	                                    AND specialorder = 0
	                                    AND (
		                                    p.clearanceflag = 1
		                                    OR pa.isdecrementable = 1
		                                    )
                                        AND GroupingSKU IS NULL
										ANd pa.FirstShipDays = 1
										AND QtyAvail0399 >= 3
                                    ";
    }
}
