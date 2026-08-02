namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify SKUs on Sale. The sale price must be greater than '0'. The end date for the sale must be greater than the current date.
    /// The retail price minus the sale price must be greater than or equal to 5% of the retail price.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T245
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1223
    /// </summary>
    public class ProductSalesData
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 pl.shortsku,
	                                    pl.retailprice,
	                                    p.saleprice,
	                                    Floor(pl.retailprice - p.saleprice) AS Savings,
	                                    productname,
	                                    p.saleenddate AS SaleEndDate
                                    FROM carteasy.dbo.tblprodlist pl 
                                    INNER JOIN carteasy.dbo.tblprductspricing p 
	                                    ON pl.shortsku = p.shortsku
                                    INNER JOIN carteasy.dbo.tblprductsextra pe 
	                                    ON pe.shortsku = pl.shortsku
									INNER JOIN carteasy..tblFirstDisplayedInSort fd
								        ON pl.Shortsku = fd.shortsku
                                    WHERE 0.10 >= Cast(Checksum(Newid(), p.shortsku) & 0x7FFFFFFF AS FLOAT) / Cast(0x7FFFFFFF AS INT)
	                                    AND p.saleprice > 0.00
	                                    AND p.sublocation = '9003'
	                                    AND p.saleenddate > Getdate()
	                                    AND isbopuseligible = 0
	                                    AND pl.listable = 1
	                                    AND pl.instock = 1
										AND IsButtonEligible = 1
                                        AND FirstShipDays < 57 -- (1/23/23) Added to ensure PDP has Add to Cart button.
	                                    AND (retailprice - saleprice) >= (0.05 * retailprice)
	                                    AND pl.shortSKU NOT LIKE '00%'
                                        AND fd.sublocationcode = 9003 -- (3/20/23) Added to exclude Employee only SKUs.
                                        AND Floor(pl.retailprice - p.saleprice) >= 5 -- (6/17/21) In order for the Save callout to appear saved amount must be greater than or equal to $5
                                        ";
    }
}
