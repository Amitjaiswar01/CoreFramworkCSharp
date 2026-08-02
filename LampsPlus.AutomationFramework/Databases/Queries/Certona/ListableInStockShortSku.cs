namespace LampsPlus.AutomationFramework.Databases.Queries.Certona
{
    /// <summary>
    /// Query to find a SKU that is listable on the site.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T324
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T876
    /// </summary>
    public class ListableInStockShortSku
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP @Amount p.shortsku
                                    FROM carteasy.dbo.tblprducts p
                                    INNER JOIN Carteasy.dbo.tblPrductsExtra px
									ON p.ShortSKU = px.shortsku
								    INNER JOIN carteasy..tblFirstDisplayedInSort fd
									ON p.Shortsku = fd.shortsku
                                    WHERE 0.10 >= Cast(Checksum(Newid(), p.shortsku) & 0x7FFFFFFF AS FLOAT) / Cast(0x7FFFFFFF AS INT)
	                                    AND p.listable = 1
	                                    AND p.instock = 1
                                        AND px.IsButtonEligible = 1
                                        AND fd.SublocationCode = 9003 -- (3/20/23) Added to exclude Employee only SKUs.
	                                    AND (
		                                    P.saleprice1internet > 10
		                                    OR p.retailpriceinternet > 10
		                                    )
	                                    AND px.FirstShipDays < 57
                                        AND p.shortsku NOT LIKE '000%' -- (2/13/23) Added to avoid pulling pseudo-skus.
										AND (
		                                    px.groupingsku IS NULL
		                                    OR px.groupingsku = ''
		                                    ) -- (5/4/21) Added because tests that use this query should not select multi-products which is controlled by GroupingSKU. 
                                    ";
    }
}
