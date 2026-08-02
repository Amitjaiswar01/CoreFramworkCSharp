namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to verify parts of  SKU with Replacement Part
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7786
    /// </summary>
    public class GetReplacementBulbDetail
    {
        public const string Query = @"
        SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
		SELECT DISTINCT TOP 5 pe.ShortSKU, b.BulbSKU
		FROM carteasy..tblprducts p
		INNER JOIN carteasy..tblprductsextra pe
		ON pe.shortsku = p.shortsku
		INNER JOIN carteasy..tblproductsavailability pa
		ON pe.shortsku = pa.shortsku
		INNER JOIN Carteasy.dbo.tblBulbs b
		ON pe.shortsku = b.ShortSku
		WHERE pe.isbuttoneligible = 1 AND instock = 1
		AND clearanceflag = 0 AND (saleprice1internet = 0 OR saleprice1internet >= retailpriceinternet)
		AND Len(p.shortsku) = 5
		AND p.Bulbs = '1'
		AND PE.ShortSku = @parentsku";
    }
}