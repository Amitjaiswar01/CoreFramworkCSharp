namespace LampsPlus.AutomationFramework.Databases.Queries.ShoppingCartEmployeeCan
{
    /// <summary>
    /// Query to identify a SKU that is less than $10 when considering a $5 shipping charge.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T128
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T680
    /// </summary>
    public class LessThanTenDollarItem
    {
        public const string Query = @"
                                    SET TRANSACTION isolation level READ uncommitted;

                                    SELECT TOP 1 p.shortsku
                                    FROM   tblprducts p
									INNER JOIN carteasy..tblprductsextra pe
                                                       ON pe.shortsku = p.shortsku
                                    WHERE ( ( retailpriceinternet BETWEEN 1 AND 2.99 )
                                            AND ( saleprice1internet = 0 ) )
                                          -- (4/11/23) Exclude Sale items because they use a different callout that can alter the results of the test.                         
                                          AND listable = 1
                                          AND instock = 1
										  AND ISNULL(groupingsku, '') = '' -- (10/30/23) Added to avoid selecting multiproduct SKUs which will break tests using this query.
                                    ORDER  BY Newid() 
                                    ";
    }
}
