namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify SKUs that have AR
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7856
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7856
    /// </summary>
    public class GetSkuThatHasArOption 
    {
        public const string Query = @"
                                   USE carteasy

                                    SET TRANSACTION isolation level READ uncommitted;

                                    SELECT TOP 1 p.shortsku,
                                                   c.id,
                                                   p.category
                                    FROM   carteasy..tblprducts P
                                           INNER JOIN carteasy..tblprductsextra pe
                                                   ON p.shortsku = pe.shortsku
                                           LEFT JOIN carteasy.dbo.categories c
                                                  ON c.cat = ( CASE
                                                                 WHEN Charindex(',', p.category) = 0 THEN p.category
                                                                 ELSE LEFT(p.category, Charindex(',', P.category)
                                                                                       - 1)
                                                               END )
                                    WHERE  p.instock = 1
                                           AND pe.isbuttoneligible = 1
                                           AND pe.isclippable = 1
                                           AND pe.firstshipdays < 57
                                           AND c.id NOT IN ( 7, 14, 26, 27, 44, 46, 60, 64 )
                                           AND p.category != 'Lamp Shades'
                                    ORDER  BY Newid() 
                                    ";
    }
}