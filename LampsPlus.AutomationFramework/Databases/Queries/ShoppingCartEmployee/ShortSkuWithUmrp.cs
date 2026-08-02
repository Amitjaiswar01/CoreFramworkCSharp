namespace LampsPlus.AutomationFramework.Databases.Queries.ShoppingCartEmployee
{
    /// <summary>
    /// Query to identify a SKU that has a UMRP value. UMRP value cannot be NULL or ''. Store discounting must be '0'.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T116
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T668
    /// </summary>
    public class ShortSkuWithUmrp
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 p.shortsku
                                    FROM carteasy.dbo.tblprducts p
                                    INNER JOIN carteasy.dbo.tblprductsextra pe
	                                    ON p.shortsku = pe.shortsku
                                    INNER JOIN products.dbo.tblpricingpolicy po
	                                    ON po.vendornum = p.vendornum
                                    WHERE Isnull(umrp, '') <> ''
	                                    AND p.listable = 1
	                                    AND instock = 1
	                                    AND saleprice1internet = 0
	                                    AND storediscounting = 0
	                                    AND umrp = retailpriceinternet
	                                    AND groupingsku IS NULL
	                                    AND FirstShipDays <= 56
                                    ORDER BY newid()
                                    ";
    }
}
