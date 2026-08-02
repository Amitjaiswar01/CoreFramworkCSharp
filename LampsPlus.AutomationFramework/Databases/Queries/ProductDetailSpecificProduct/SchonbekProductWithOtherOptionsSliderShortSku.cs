namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetailSpecificProduct
{
    /// <summary>
    /// Query to identify a Schonbek SKU that has the 'OTHER OPTIONS' slider.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T230
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1207
    /// </summary>
    public class SchonbekProductWithOtherOptionsSliderShortSku
    {
#pragma warning disable 1591
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT finishfamily AS ShortSKU,
                                    shortsku AS MoreOptionsSKU
                                    FROM   carteasy.dbo.tblprducts p 
                                    WHERE  p.category NOT LIKE '%color plus%'
                                           AND p.category NOT LIKE '%Schonbek%'
                                           AND p.category NOT LIKE '%Giclee%'
                                           AND finishfamily != ''
                                           AND finishfamily IN (SELECT shortsku
                                                                FROM   carteasy..tblprducts 
                                                                WHERE  listable = 1
                                                                       AND instock = 1)
                                           AND p.shortsku IN (SELECT p.shortsku
                                                              FROM   carteasy..tblprducts p 
                                                                     LEFT JOIN products.dbo.tblschonbek s
                                                                            ON p.shortsku = s.sku
                                                              WHERE  p.listable = 1
                                                                     AND p.excludedimmer = 0                                
                                                                     AND p.instock = 1
                                                                     AND p.manufacturer LIKE 'schonbek'
                                                                     AND s.crystal IS NULL
                                                                     AND s.finish IS NULL
                                                                     AND Isnull(family, '') != '')  
                                        ";
#pragma warning restore 1591
    }
}
