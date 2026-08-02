namespace LampsPlus.AutomationFramework.Databases.Queries.ShoppingCart
{
    /// <summary>
    /// Query to identify a SKU that has a shipping charge. The freightcharge column must have a value greater than '0'.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T107
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T659
    /// </summary>
    public class ShortSkuWithShippingCharge
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 p.ShortSKU
                                    FROM carteasy.dbo.tblfreightcharges fc 
                                    INNER JOIN carteasy.dbo.tblprducts p 
	                                    ON fc.shortsku = p.shortsku
                                    INNER Join carteasy.dbo.tblprductsextra px 
										ON px.shortsku = fc.shortsku
                                    WHERE  listable = 1
	                                    AND instock = 1
	                                    AND freightcharge > 0
	                                    AND zone = 2
	                                    AND sublocationcode = @sublocationcode
										AND IsButtonEligible = 1 -- (6/16/21) Added so Add to Cart button is always present.
										AND FirstShipDays < 57
										AND (
		                                    px.groupingsku IS NULL
		                                    OR px.groupingsku = ''
		                                    ) -- (10/19/21) Added because tests that use this query should not select multi-products which is controlled by GroupingSKU. 
										ORDER BY NEWID()
                                    ";
    }
}
