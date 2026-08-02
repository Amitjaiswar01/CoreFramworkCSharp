namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify any SKU that has 2D Room Option
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T940
    /// </summary>
    public class SkuFor2DRoom
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT top 1 p.ShortSKU, c.Id, p.Category
                                    FROM carteasy..tblPrducts P
                                    INNER JOIN Carteasy..tblPrductsExtra pe ON p.ShortSku=pe.ShortSku
                                    LEFT JOIN carteasy.dbo.Categories c
                                    ON c.Cat = (CASE WHEN CHARINDEX(',', p.Category) = 0
                                    THEN p.Category ELSE Left(p.Category, CHARINDEX(',', P.Category) - 1 )
                                    END
                                    )
                                    WHERE p.InStock=1 and pe.isButtonEligible=1
                                    and pe.IsClippable=1
                                    and pe.FirstShipDays < 57
                                    and c.id not in (7,14,26,27,44,46,60,64)
                                    ORDER BY NEWID()
                                    ";
    }
}