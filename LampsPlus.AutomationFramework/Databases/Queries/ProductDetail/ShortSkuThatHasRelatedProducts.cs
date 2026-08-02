namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify a SKU that has Related Items. The category cannot be 'Recessed Lighting'. The SKU cannot have a value in the
    /// callout table. There must be less than 10 coordinating SKUs for the main SKU.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T223
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1200
    /// </summary>
    public class ShortSkuThatHasRelatedProducts
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
							        SELECT TOP 1 c.shortsku,
                                                 Count(coordinatingsku)
                                    FROM   carteasy.dbo.tblcoordinatingproduct c
                                           INNER JOIN carteasy.dbo.tblprducts sp
                                                   ON c.shortsku = sp.shortsku
                                           INNER JOIN carteasy.dbo.tblprductsextra spe
                                                   ON c.shortsku = spe.shortsku
                                           INNER JOIN carteasy.dbo.tblprducts cp
                                                   ON c.coordinatingsku = cp.shortsku
                                           INNER JOIN carteasy.dbo.tblprductsextra cpe
                                                   ON c.coordinatingsku = cpe.shortsku
                                    WHERE  spe.isbuttoneligible = 1
                                           AND sp.instock = 1
                                           AND cpe.isbuttoneligible = 1
                                           AND cp.instock = 1
                                    GROUP  BY c.shortsku
                                    HAVING Count(coordinatingsku) > 0
                                    ORDER  BY Newid()";
    }
}
