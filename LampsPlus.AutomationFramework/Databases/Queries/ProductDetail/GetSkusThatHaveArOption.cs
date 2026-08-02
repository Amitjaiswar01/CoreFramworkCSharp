namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify SKUs that have AR
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T725
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7856
    /// </summary>
    public class GetSkusThatHaveArOption
    {
        public const string Query = @"
                                   USE Carteasy 

                                   SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED 

                                   SELECT TOP 2 PA.ShortSKU,PA.SalePrice1Internet,PA.RetailPriceInternet, PA.ProductName
                                   FROM tblPrducts AS PA
                                   INNER JOIN carteasy.dbo.tblPrductsExtra PX ON PA.ShortSKU = PX.ShortSKU
                                   WHERE PA.InStock = 1
                                   and PA.Listable = 1 
                                   and PX.IsClippable = 1 
                                   and PX.IsButtonEligible = 1
                                   and PA.SalePrice1Internet = 0
                                   and PX.FirstShipDays < 57  --Added to ensure PDP has Add to Cart button.
                                   ORDER BY NEWID()
                                    ";
    }
}