namespace LampsPlus.AutomationFramework.Databases.Queries.SubmittingOrders
{
    /// <summary>
    /// Query to identify a SKU that is less than $200
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T136
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T688
    /// </summary>
    public class AnySkuLessThanTwoHundredDollars
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 p.shortsku 
                                    FROM   carteasy.dbo.tblprducts p  
                                           INNER JOIN carteasy.dbo.tblprductsextra pe  
                                                   ON p.shortsku = pe.shortsku 
                                    WHERE  p.listable = 1 
                                           AND p.instock = 1 
                                           AND groupingsku IS NULL 
                                           AND IsButtonEligible = 1 -- (6/16/21) Added so PDP will have Add to Cart button.
                                           AND FirstShipDays < 57
                                           AND ( retailpriceinternet > 1 
                                                  OR saleprice1internet > 0 ) 
                                           AND retailprice BETWEEN 100 AND 150 
                                    ORDER  BY Newid()  
                                        ";
    }
}
