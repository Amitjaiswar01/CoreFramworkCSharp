namespace LampsPlus.AutomationFramework.Databases.Queries.ShoppingCart
{
    public class GetManualDiscountableSku
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 2 p.shortsku
                                    FROM carteasy.dbo.tblprductsextra px 
                                    INNER JOIN products.dbo.tblpricingpolicy pr 
                                            ON px.UMRPVendorNumber = pr.vendornum
                                    INNER JOIN carteasy.dbo.tblprducts p 
                                            ON px.shortsku = p.shortsku
									INNER JOIN carteasy.dbo.tblproductsavailability pa  
                                            ON px.shortsku = pa.shortsku 
                                    WHERE 0.10 >= Cast(Checksum(Newid(), p.shortsku) & 0x7FFFFFFF AS FLOAT) / cast(0x7FFFFFFF AS INT)
	                                    AND listable = 1
	                                    AND instock = 1
	                                    AND saleprice1 = 0
	                                    AND retailpriceinternet > 0
                                        AND internetdiscounting = 1
	                                    AND p.clearanceflag = 0
		                                AND pa.isdecrementable = 0
                                        AND IsButtonEligible = 1
                                        AND (
		                                    px.groupingsku IS NULL
		                                    OR px.groupingsku = ''
		                                    )
                                        AND px.FirstShipDays < 57 --Indicates the number of days it could take for an item to be in stock
                                    ";
    }
}
