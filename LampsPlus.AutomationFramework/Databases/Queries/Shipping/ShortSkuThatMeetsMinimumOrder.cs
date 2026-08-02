namespace LampsPlus.AutomationFramework.Databases.Queries.Shipping
{
    /// <summary>
    /// Query to identify a SKU that has a retail price or sale price above $10.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T167
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T719
    /// </summary>
    public class ShortSkuThatMeetsMinimumOrder
    {
        public const string Query = @"
									SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 p.shortsku
                                    FROM carteasy.dbo.tblprducts p 
									INNER JOIN carteasy.dbo.tblPrductsExtra px 
									ON p.ShortSKU = px.ShortSKU
									INNER JOIN carteasy..tblFirstDisplayedInSort fd
									ON p.Shortsku = fd.shortsku
                                    WHERE p.listable = 1
	                                    AND p.instock = 1
	                                    AND P.SalePrice1Internet = 0
	                                    AND retailpriceinternet > 10
										AND IsButtonEligible = 1
										AND FirstShipDays < 57
                                        AND ISNULL(px.GroupingSKU,'') = ''
                                        AND fd.sublocationcode = 9003 -- (3/20/23) Added to avoid selecting Employee Only SKUS.
										ORDER BY NEWID()
                                    ";
    }
}
