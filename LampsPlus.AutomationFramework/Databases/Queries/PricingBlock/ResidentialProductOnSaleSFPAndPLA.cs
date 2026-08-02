namespace LampsPlus.AutomationFramework.Databases.Queries.PricingBlock
{
    /// <summary>
    /// Query to identify SKU Residential Product on Sale, Not Eligible Member Special, No Company in Session
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7778
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7782
    /// </summary>
    public class ResidentialProductSkuOnSaleSfpAndPla
    {
        public const string Query = @"
                                    SET TRANSACTION isolation level READ uncommitted;

                                    SELECT TOP 1 pl.shortsku,
                                                 pl.retailprice,
                                                 p.saleprice,
                                                 Floor(pl.retailprice - p.saleprice) AS Savings,
                                                 pl.productname,
                                                 p.saleenddate                       AS SaleEndDate
                                    FROM   carteasy.dbo.tblprodlist pl
                                           INNER JOIN carteasy.dbo.tblprductspricing p
                                                   ON pl.shortsku = p.shortsku
                                           INNER JOIN carteasy.dbo.tblprductsextra pe
                                                   ON pe.shortsku = pl.shortsku
                                           INNER JOIN carteasy..tblprducts x
                                                   ON pl.shortsku = x.shortsku
                                           INNER JOIN carteasy..tblproductsavailability pa
                                                   ON pl.shortsku = pa.shortsku
                                    WHERE  p.saleprice > 0.00
                                           AND p.sublocation = '9003'
                                           AND p.saleenddate > Getdate()
                                           AND pa.isbopuseligible = 0
                                           AND isdecrementable = 0
                                           AND x.department NOT BETWEEN 80 AND 89
                                           AND pl.listable = 1
                                           AND pl.instock = 1
                                           AND pe.FirstShipDays < 57 -- (2/6/23) Added to ensure PDP has Add to Cart button.
										   AND IsButtonEligible = 1
                                           AND ( pl.retailprice - p.saleprice ) >= ( 0.05 * pl.retailprice )
                                           AND pl.shortsku NOT LIKE '00%'
                                           AND Floor(pl.retailprice - p.saleprice) >= 5 -- (6/17/21) In order for the Save callout to appear saved amount must be greater than or equal to $5
                                    ORDER  BY Newid() 
                                    ";
    }
}