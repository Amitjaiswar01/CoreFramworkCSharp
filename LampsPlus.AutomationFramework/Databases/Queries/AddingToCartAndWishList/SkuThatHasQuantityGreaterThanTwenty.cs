namespace LampsPlus.AutomationFramework.Databases.Queries.AddingToCartAndWishList
{
    /// <summary>
    /// Query to identify an item that has an inventory greater than 20 so the QTY field on the PDP will be a free-form field and not a drop-down.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T351
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T903
    /// </summary> 
    public class SkuThatHasQuantityGreaterThanTwenty
    {
        public const string Query = @"
									SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
                                    SELECT TOP 1 p.shortsku,
	                                    (p.inventory - pa.numbersoldtoday) AS CurrentInventory
                                    FROM carteasy.dbo.tblprducts p
                                    INNER JOIN carteasy.dbo.tblproductsavailability pa
	                                    ON p.shortsku = pa.shortsku
                                    INNER JOIN carteasy.dbo.tblprductsextra px
	                                    ON px.shortsku = p.shortsku
                                    WHERE p.instock = 1
	                                    AND p.inventory > 20
	                                    AND p.listable = 1
                                        AND px.IsButtonEligible = 1
                                        AND px.FirstShipDays < 57 -- (3/3/22) Added to ensure PDP has Add to Cart button.
	                                    AND px.isbopuseligible = 0
	                                    AND p.specialorder = 0
										AND (
		                                    px.groupingsku IS NULL
		                                    OR px.groupingsku = ''
		                                    )
                                    ORDER BY Newid()
                                        ";
    }
}
