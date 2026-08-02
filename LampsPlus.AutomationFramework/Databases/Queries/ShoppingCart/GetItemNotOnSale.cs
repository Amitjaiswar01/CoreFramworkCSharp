namespace LampsPlus.AutomationFramework.Databases.Queries.ShoppingCart
{
    /// <summary>
    /// Query to identify items NOT on sale.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T109
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T661
    /// </summary>
    public class GetItemNotOnSale
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 p.shortsku
                                    FROM carteasy.dbo.tblprducts p 
									INNER JOIN Carteasy.dbo.tblPrductsExtra px 
									ON p.shortsku = px.shortsku
									INNER JOIN carteasy..tblFirstDisplayedInSort fd
									ON p.Shortsku = fd.shortsku
                                    WHERE 0.10 >= Cast(Checksum(Newid(), p.shortsku) & 0x7FFFFFFF AS FLOAT) / cast(0x7FFFFFFF AS INT)
	                                    AND listable = 1
	                                    AND instock = 1
	                                    AND saleprice1Internet = 0
	                                    AND retailpriceinternet > 0
										AND IsButtonEligible = 1
                                        AND FirstShipDays < 57
                                        AND fd.sublocationcode = 9003 -- (3-20-23) Added to avoid selecting Employee only SKUs.
                                        AND ( px.groupingsku IS NULL OR px.groupingsku = '') -- (01-06-22) Multi-products should be avoided for this query.
                                    ";
    }
}
