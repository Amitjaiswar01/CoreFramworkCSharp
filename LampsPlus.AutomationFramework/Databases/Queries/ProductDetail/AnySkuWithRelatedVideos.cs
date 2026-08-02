namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify any SKU that has Related Videos. The retail price OR sale price must be greater than 0. The item cannot be 'intranetonly'.
    /// </summary>
    public class AnySkuWithRelatedVideos
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 p.shortsku
                                    FROM carteasy.dbo.tblVideoSkuRelationship v 
                                    INNER JOIN Carteasy.dbo.tblPrducts p 
	                                    ON p.ShortSKU = v.ShortSKU
                                    WHERE p.listable = 1
	                                    AND p.instock = 1
	                                    AND (
		                                    retailpriceinternet > 0
		                                    OR saleprice1internet > 0
		                                    )
	                                    AND intranetonly = 0
                                        AND category LIKE '%Chandeliers%'
                                    ORDER BY NewID()
                                    ";
    }
}