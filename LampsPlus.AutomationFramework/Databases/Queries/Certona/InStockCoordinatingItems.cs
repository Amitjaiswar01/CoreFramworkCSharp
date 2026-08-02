namespace LampsPlus.AutomationFramework.Databases.Queries.Certona
{
    /// <summary>
    /// Dynamic Query to get in stock coordinating items of a sku
    /// </summary>
    public class InStockCoordinatingItems
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT P.ShortSku
                                    FROM tblCoordinatingProduct CP
                                    INNER JOIN tblCoordinatingProductType CPT
	                                    ON CPT.TypeID = CP.TypeID
                                    INNER JOIN tblPrducts P
	                                    ON P.ShortSku = CP.CoordinatingSKU
                                    INNER JOIN tblPrductsExtra PX
	                                    ON PX.ShortSku = P.ShortSku
                                    LEFT JOIN TblSearchLinkSKUs SLS
	                                    ON SLS.ShortSku = P.ShortSku
                                    LEFT JOIN Products.dbo.tblPricingPolicy PY
	                                    ON P.VendorNum = PY.VendorNum
                                    WHERE CP.ShortSku = '@shortsku'
	                                    AND P.InStock = 1
	                                    AND P.Listable = 1
                                    ORDER BY cp.Sort
                                    ";
    }
}
