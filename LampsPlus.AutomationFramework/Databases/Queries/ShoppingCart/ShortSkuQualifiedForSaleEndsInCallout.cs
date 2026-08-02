namespace LampsPlus.AutomationFramework.Databases.Queries.ShoppingCart
{

    /// <summary>
    /// Query to get the Shot SKU for 'Sale Ends in' callout on Cart Overview page
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7736
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7737
    /// </summary>


    class ShortSkuQualifiedForSaleEndsInCallout
    {
        public const string Query = @"
                                    SET TRANSACTION isolation level READ uncommitted;

                                    SELECT TOP 1 p.[shortsku],
                                                 [begsale1],
                                                 [endsale1],
                                                 [retailprice],
                                                 [saleprice1]
                                    FROM   [Carteasy].[dbo].[tblprducts] p
                                           INNER JOIN carteasy.dbo.tblprductsextra px
                                                   ON p.shortsku = px.shortsku
                                    WHERE  saleprice1 > 0 AND saleprice1 < retailprice -- SKU is On Sale or On Daily Sale
                                           AND endsale1 >= CONVERT(DATE, Dateadd(day, 1, Getdate())) -- (7/7/23) Adding 1 day to avoid sales that end on the same day as the test execution.
                                           AND isbuttoneligible = 1 
	                                       AND instock = 1 -- has an Add to Cart button
	                                       AND FirstShipDays < 57
	                                       AND (
		                                        px.groupingsku IS NULL
		                                        OR px.groupingsku = ''
		                                        ) -- (6/2/21) Added because tests that use this query should not select multi-products which is controlled by GroupingSKU. 
                                    ORDER  BY Newid() 
                                    ";
    }
}