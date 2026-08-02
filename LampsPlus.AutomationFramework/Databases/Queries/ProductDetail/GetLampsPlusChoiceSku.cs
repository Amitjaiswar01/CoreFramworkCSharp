namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail

{
    /// <summary>
    /// Query to identify LampsPlusChoiceSKU
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7745
    public class GetLampsPlusChoiceSku
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									Use Products
                                    SELECT TOP 1 lampspluschoicesku,
                                    ProductName,
                                    m.category,
                                    m.finish,
                                    m.style,
                                    m.usage,
                                    m.type
                                    FROM products.dbo.tblmerchandizerfilters AS m
                                    JOIN carteasy.dbo.tblprducts AS p
                                    ON m.lampspluschoicesku = p.shortsku
									JOIN carteasy.dbo.tblPrductsExtra pe
									ON m.lampspluschoicesku = pe.ShortSKU
                                    WHERE Len(Isnull(lampspluschoicesku, '')) > 0
                                    AND listable = 1
                                    AND instock = 1
									AND p.intranetOnly=0
									AND pe.IsButtonEligible = 1
									AND pe.FirstShipDays < 57
                                    ORDER BY Newid()
                                    ";
    }
}
