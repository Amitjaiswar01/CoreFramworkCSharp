namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify SKU that has an inventory Sale and Comparable Value.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7620
    /// </summary>
    public class ProductWithSaleAndComparableValue
    {
        public const string Query = @"
                                  SELECT TOP 1 p.shortsku, p.endsale1,
                                  Round(Floor( retailpriceinternet - saleprice1internet ), 0) AS Saving, p.retailpriceinternet, p.saleprice1internet, *
                                  FROM carteasy..tblprducts p (nolock)
                                  INNER JOIN carteasy..tblproductsavailability pa (nolock) ON p.shortsku = pa.shortsku
                                  INNER JOIN carteasy..tblprductsextra pe (nolock) ON pe.shortsku = p.shortsku
                                  WHERE IntranetOnly = 0
                                  AND pe.isbuttoneligible = 1 AND instock = 1
                                  AND PA.firstshipdays < 57
                                  AND p.department NOT BETWEEN 80 AND 89
                                  AND IsDecrementable = 0 AND saleprice1internet > 0 AND saleprice1internet < retailpriceinternet
                                  AND (pe.GroupingSku is null OR pe.GroupingSku=p.ShortSKU)
                                  AND ( Round(Floor( p.retailpriceinternet - p.saleprice1internet ), 0) > 5.00 )
                                  AND p.endsale1 >= CONVERT(DATE, Dateadd(day, 0, Getdate()))
                                  ORDER BY Newid()
                                    ";
    }
}
