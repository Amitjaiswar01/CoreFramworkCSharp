namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify a SKU that is part of a finish family and has More Finishes Slider on PDP.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7864
    /// </summary>
    public class SkuThatHasFinishFamilyWithMoreFinishesSlider
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

                                    SELECT TOP 1 p.finishfamily
                                    FROM Carteasy.dbo.tblPrducts p
                                    INNER JOIN carteasy.dbo.tblprductsextra pe ON p.shortsku = pe.shortsku
                                    LEFT JOIN Carteasy.dbo.tblPrducts fp ON p.FinishFamily = fp.SHORTSKU
                                    WHERE ( p.Category like '%Chandeliers%'
                                            AND p.Category like '%Close to Ceiling Lights%'
                                            OR p.Category like '%Pendant Lighting%'
                                            OR p.Category like '%Sconces%'
                                            OR p.Category like '%Bathroom Lighting%' )
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
