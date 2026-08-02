namespace LampsPlus.AutomationFramework.Databases.Queries.PricingBlock
{
    /// <summary>
    /// Query to identify any SKU that has a Brand.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7773
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7774
    /// </summary>
    public class ResidentialProductSkuOnRegularPrice
    {
        public const string Query = @"
                                        SET TRANSACTION isolation level READ uncommitted;
                                        SELECT TOP 1 CASE
                                        WHEN ( ( Floor(initialretailprice * 1.5) % 10 ) >= 5 ) THEN
                                        ( Floor(( initialretailprice * 1.5 ) / 10) * 10 ) + 9.99
                                        WHEN ( ( Floor(initialretailprice * 1.5) % 10 ) < 5 ) THEN
                                        ( Floor(( initialretailprice * 1.5 ) / 10) * 10 ) + 4.99
                                        END AS ComparePrice,
                                        *
                                        FROM   carteasy..tblprducts p
                                               INNER JOIN carteasy..tblprductsextra pe
                                                       ON pe.shortsku = p.shortsku
                                        WHERE  pe.isbuttoneligible = 1
                                               AND instock = 1
                                               AND p.department NOT BETWEEN 80 AND 89
                                               AND islpproduct = 1
                                               AND clearanceflag = 0
                                               AND ( saleprice1internet = 0
                                                      OR saleprice1internet >= retailpriceinternet )
										       AND FirstShipDays < 57 -- (7/8/22) Added to PDP has Add to Cart button.
											   AND ISNULL(groupingsku, '') = '' -- (7/8/22) Added to avoid selecting multiproduct SKUs which will break tests using this query.
                                        ORDER  BY Newid()
                                    ";
    }
}
