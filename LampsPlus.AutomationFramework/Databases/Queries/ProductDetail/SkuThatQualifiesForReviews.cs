namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify a SKU that qualifies for reviews.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T264
    /// </summary>
    public class SkuThatQualifiesForReviews
    {
        public const string Query = @"
                                    USE carteasy 

                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 r.shortsku, 
                                                   reviewcount, 
                                                   relatedreviewcount, 
                                                   commentcount, 
                                                   averagerate 
                                    FROM   tblprducts p 
                                           INNER JOIN tblturntoskuaveragerating r  
                                                   ON p.shortsku = r.shortsku 
                                           LEFT JOIN carteasy.dbo.tblprductsextra pe  
                                                  ON pe.shortsku = p.shortsku 
                                    WHERE  instock = 1 
                                           AND p.listable = 1 
                                           AND isbopuseligible = 0 
                                           AND reviewcount > 1 
                                           AND ( p.type NOT LIKE '%Art Shade%' 
                                                  OR p.type NOT LIKE '%Decorative Pillows%' ) 
                                           AND Charindex('-', p.shortsku) = 0 

                                    ORDER BY Newid()
                                    ";
    }
}