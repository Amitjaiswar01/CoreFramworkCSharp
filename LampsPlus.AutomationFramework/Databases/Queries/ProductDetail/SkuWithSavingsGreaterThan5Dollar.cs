namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify any SKU with Savings greater than $5.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7970
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7973
    /// </summary>
    public class SkuWithSavingsGreaterThan5Dollar
    {
        public const string Query = @"
                                    SET TRANSACTION isolation level READ uncommitted;
                                    SELECT TOP 1 p.shortsku,
                                                 p.retailpriceinternet,
                                                 p.retailprice,
                                                 p.saleprice1,
                                                 pe.retailprice58,
                                                 ( Round(Floor(retailpriceinternet - retailprice58), 0) ) AS Saving
                                    FROM   carteasy..tblprducts p (nolock)
                                           INNER JOIN carteasy..tblprductsextra pe (nolock)
                                                   ON pe.shortsku = p.shortsku
                                    WHERE  intranetonly = 0
                                           AND listable58 = 1
                                           AND retailprice58 > 0
                                           AND inventory58 > 0
                                           AND clearanceflag = 0
                                           AND ( saleprice1internet = 0
                                                  OR saleprice1internet >= retailpriceinternet )
                                           AND ( Round(Floor(p.retailpriceinternet - pe.retailprice58), 0) > 5.00 )
                                    ORDER  BY Newid() 
                                    ";
    }
}
