namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify a SKU that qualifies as a Mapped to a Colorable Pattern SKU.
    /// The pattern must have at least 2 popular color combinations. It is restricted to two types of products: decorative pillows and art shade.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T227
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1204
    /// </summary>
    public class McpArtShadeItemSkus
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 pa.shortsku, 
                                                 kd.componentsku  AS BaseSku,
                                                 Count(patternid) AS PatternIDTotal, 
                                                 Count(a.trim)    AS TrimTotal
                                    FROM   [Products].[dbo].[tblproductassociation] pa  
                                           INNER JOIN carteasy.dbo.tblprducts p  
                                                   ON p.shortsku = pa.shortsku 
                                           INNER JOIN [Products].[dbo].[tblpatterns] a  
                                                   ON a.id = pa.patternguid 
                                           INNER JOIN [Products].[dbo].[tblpatterncolorcombo] pcc  
                                                   ON pcc.patternid = pa.patternguid 
                                           INNER JOIN tblprductsextra pe  
                                                   ON p.shortsku = pe.shortsku 
                                           INNER JOIN products.dbo.tblkitdetails kd  
                                                   ON kd.kitsku = pa.shortsku
                                           INNER JOIN products.dbo.tblPatternableProducts pp  
                                                   ON kd.componentsku = pp.ProductSKU
                                    WHERE  p.type LIKE '%Art Shade%' 
                                           AND p.listable = 1 
                                           AND a.iscolorable = 1 
                                           AND P.instock = 1 
                                           AND pe.isbopuseligible = 0 
                                           AND sequencenumber = 1
                                           AND pp.HasMetalBand = 0
                                    GROUP  BY pa.shortsku, 
                                              kd.componentsku
                                    HAVING Count(patternid) >= 2 
                                           AND Count(a.trim) > 1 
                                    ORDER  BY Newid() 
                                    ";
    }
}
