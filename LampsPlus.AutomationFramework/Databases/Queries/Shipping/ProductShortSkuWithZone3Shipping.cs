namespace LampsPlus.AutomationFramework.Databases.Queries.Shipping
{
    /// <summary>
    /// Query to identify a SKU that has a shipping charge for Zone 3. The SKU cannot be a special order item. The service level cannot be
    /// 104, 105, or 108. The zone must be 3 for sublocation code 9003.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T164
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T716
    /// </summary>
    public class ProductShortSkuWithZone3Shipping
    {
        public const string Query = @"
									SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT DISTINCT TOP 1 p.shortsku
                                    FROM carteasy.dbo.tblprducts p 
                                    INNER JOIN carteasy.dbo.tblfreightcharges fc 
	                                    ON fc.shortsku = p.shortsku
							        INNER JOIN Carteasy.dbo.tblPrductsExtra px
									ON p.ShortSKU = px.shortsku
                                    WHERE 0.10 >= Cast(Checksum(Newid(), p.shortsku) & 0x7FFFFFFF AS FLOAT) / Cast(0x7FFFFFFF AS INT)
	                                    AND specialorder = 0
	                                    AND listable = 1
	                                    AND instock = 1
	                                    AND zone = 3
										AND IsButtonEligible = 1
										AND FirstShipDays < 57 -- (1/20/22) Added to ensure PDP has an Add to Cart button.
										AND (
		                                    px.groupingsku IS NULL
		                                    OR px.groupingsku = ''
		                                    ) -- (1/20/22) Added because tests that use this query should not select multi-products which is controlled by GroupingSKU. 
	                                    AND (
		                                    retailpriceinternet > 10
		                                    OR saleprice1internet > 10
		                                    )
	                                    AND servicelevel NOT IN (
		                                    104,
		                                    105,
		                                    108
		                                    )
	                                    AND sublocationcode = '9003'
	                                    AND Charindex('-', p.shortsku) = 0
	                                    AND NOT EXISTS (
		                                    SELECT 1
		                                    FROM carteasy.dbo.tblcombosku 
		                                    WHERE p.shortsku = basesku
		                                    )                                    
                                    ";
    }
}
