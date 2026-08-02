namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify a SKU that is part of a finish family. The groupingsku must be NULL or empty. The finishfamily can NOT be NULL.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T235
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1212
    /// </summary>
    public class SkuThatHasFinishFamily
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

                                     SELECT TOP 1 p.finishfamily ,p.Category
                                     FROM Carteasy.dbo.tblPrducts p
                                     INNER JOIN carteasy.dbo.tblprductsextra pe ON p.shortsku = pe.shortsku
                                     LEFT JOIN Carteasy.dbo.tblPrducts fp ON p.FinishFamily = fp.SHORTSKU
                                     WHERE ( p.Category Not like '%Chandeliers%'
                                     AND p.Category Not like '%Close to Ceiling Lights%'
                                     AND p.Category Not like '%Pendant Lighting%'
                                     AND p.Category Not like '%Sconces%'
                                     AND p.Category Not like '%Bathroom Lighting%' )
                                     AND p.listable = 1
                                     AND p.instock = 1
                                     AND Charindex('-', p.shortsku) = 0
                                     AND (
                                     pe.groupingsku IS NULL
                                     OR pe.groupingsku = ''
                                     )
                                     AND isBopusEligible = 0
                                     AND p.intranetonly = 0
                                     AND Isnull(p.finishfamily, '') <> ''
                                     AND fp.Listable = 1
                                     AND fp.instock = 1
                                     AND IsButtonEligible = 1
                                     And FirstShipDays < 57 -- (2/9/22) To ensure PDP has an Add to Cart button.
                                     GROUP BY p.Finishfamily , p.Category
                                     HAVING Count(p.finishfamily) > 2
                                     ORDER BY NewID()
                                    ";
    }
}
