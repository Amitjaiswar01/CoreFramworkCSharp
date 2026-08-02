namespace LampsPlus.AutomationFramework.Databases.Queries.Sort
{
    /// <summary>
    /// Query to find a Sale Product SKU and Product Name
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7837
    ///  Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7838
    /// </summary>
    public class GetSaleProductSkuAndProductName
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

                                    SELECT TOP 1 p.shortsku, ProductName
                                    FROM carteasy..tblprducts p
                                    INNER JOIN carteasy..tblproductsavailability pa ON p.shortsku = pa.shortsku
                                    INNER JOIN carteasy..tblprductsextra pe ON pe.shortsku = p.shortsku
                                    WHERE pe.isbuttoneligible = 1 AND instock = 1
                                    AND p.department NOT BETWEEN 80 AND 89
                                    AND p.IntranetOnly = 0
                                    AND IsDecrementable = 0 AND saleprice1internet > 0 AND saleprice1internet < retailpriceinternet
                                    AND (pe.GroupingSku is null OR pe.GroupingSku=p.ShortSKU)
                                    AND p.endsale1 >= CONVERT(DATE, Dateadd(day, 0, Getdate()))
                                    ORDER BY Newid()
                                    ";
    }
}
