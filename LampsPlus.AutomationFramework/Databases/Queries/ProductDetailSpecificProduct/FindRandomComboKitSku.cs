namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetailSpecificProduct
{
    /// <summary>
    /// Query to identify a listable and instock KitSKU with KitSkuType=2.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/browse/LP-17238
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T122
    /// </summary>
    public class FindRandomComboKitSku
    {
        public const string Query = @"
                                    USE CartEasy

                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 p.shortsku 
                                    FROM   carteasy.dbo.tblprductsextra px 
                                    INNER JOIN carteasy.dbo.tblprducts p  ON p.shortsku = px.shortsku 
                                    WHERE  kitskutype = 2 
                                    AND instock = 1 
                                    AND listable = 1 
                                    AND px.UMRP = 0
                                    AND IsButtonEligible = 1 --(Added 11/18/21) To avoid selecting SKUs that do not have an Add to Cart button.
                                    AND FirstShipDays < 57
                                    AND retailpriceinternet > 250 
                                    AND saleprice1internet = 0 
                                    ORDER  BY Newid()  
                                    ";
    }
}
