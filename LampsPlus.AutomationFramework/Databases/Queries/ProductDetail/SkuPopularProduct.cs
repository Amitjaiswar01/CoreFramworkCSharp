namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify a SKU that has the 'Popular Colors' slider. Test case is verifying that the correct number of Art Shades are present
    /// in the slider so 'type' must be 'Art Shade'. The SKU must have 2 or more patterns associated with it.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T228
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1205
    /// </summary>
    public class SkuPopularProduct
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 pa.shortsku,
	                                    Count(patternid) AS PatternIDTotal
                                    FROM [Products].[dbo].[tblproductassociation] pa 
                                    INNER JOIN carteasy.dbo.tblprducts p 
	                                    ON p.shortsku = pa.shortsku
                                    INNER JOIN [Products].[dbo].[tblpatterns] a 
	                                    ON a.id = pa.patternguid
                                    INNER JOIN [Products].[dbo].[tblpatterncolorcombo] pcc 
	                                    ON pcc.patternid = pa.patternguid
                                    INNER JOIN carteasy.dbo.tblprductsextra pe 
	                                    ON pe.shortsku = p.shortsku
                                    WHERE 0.10 >= Cast(Checksum(Newid(), p.shortsku) & 0x7FFFFFFF AS FLOAT) / Cast(0x7FFFFFFF AS INT)
                                        AND type = 'Art Shade'
	                                    AND p.listable = 1
	                                    AND iscolorable = 1
	                                    AND pe.isbopuseligible = 0
	                                    AND (
		                                    P.instock = 1
		                                    OR P.instock = 0
		                                    AND p.outofstockdate >= Dateadd(day, 0, Datediff(day, 0, Getdate()) - 2)
		                                    )
                                        AND IsButtonEligible = 1
                                        AND FirstShipDays < 57 -- (01-19-22) Adding to ensure PDP has an Add to cart Button
                                    GROUP BY pa.shortsku,
	                                    patternableproductsku,
	                                    iscolorable,
	                                    productname
                                    HAVING Count(patternid) <= 20
                                    ";
    }
}
