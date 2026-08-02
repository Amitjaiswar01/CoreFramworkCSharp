namespace LampsPlus.AutomationFramework.Databases.Queries.SortCallout
{
    /// <summary>
    /// Query to find a shortsku that has both the Daily Sale and Quantity Left callout.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7803
    /// </summary>
    public class DailySaleQuantityLeftCalloutsShortSku
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
                                    SELECT TOP 1 p.ShortSKU, ProductName -- (4/8/23) ProductName column value needed to match test case requirements in Zephyr.
                                    FROM carteasy..tblprducts p
                                    INNER JOIN carteasy..tblproductsavailability pa ON p.shortsku = pa.shortsku
                                    INNER JOIN carteasy..tblprductsextra pe ON pe.shortsku = p.shortsku
                                    WHERE pe.isbuttoneligible = 1 AND instock = 1
                                    AND p.department NOT BETWEEN 80 AND 89
                                    AND p.IntranetOnly = 0   -- (8/24/22) Set to 0 so the query selects SKUs that are visible to all users, not just employees.
                                    AND pa.isdecrementable = 1 AND saleprice1internet > 0 AND saleprice1internet < retailpriceinternet
                                                AND (pe.GroupingSku is null OR pe.GroupingSku=p.ShortSKU)
                                                AND p.Inventory < 20
                                    ORDER BY Newid()
                                    ";
    }
}
