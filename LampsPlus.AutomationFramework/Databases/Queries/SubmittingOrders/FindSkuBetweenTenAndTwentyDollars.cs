namespace LampsPlus.AutomationFramework.Databases.Queries.SubmittingOrders
{   
    /// <summary>
    /// Query to identify a SKU that is between ten and twenty dollars.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T134
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T686
    /// </summary>    
    public class FindSkuBetweenTenAndTwentyDollars
    {
        public const string Query = @"
                                     USE carteasy 

                                     SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									 SELECT TOP 1 p.shortsku
                                     FROM   carteasy.dbo.tblprductsextra px 
                                            INNER JOIN carteasy.dbo.tblprducts p 
                                                    ON p.shortsku = px.shortsku 
                                     WHERE  (kitskutype = 0 or kitskutype = 1)
                                            AND instock = 1 
                                            AND listable = 1 
											AND intranetonly = 0
                                            AND GroupingSKU IS NULL
											AND IsButtonEligible = 1
											And FirstShipDays < 57 -- (2/2/23) Added to ensure PDP has Add to Cart button.
                                            AND Category NOT LIKE '%Dimmers%'
                                            AND ( ( retailpriceinternet BETWEEN 10 AND 20 ) 
                                                   AND ( saleprice1internet = 0 ) ) -- (12/3/21) Exclude Sale items because they use a different callout that can alter the results of the test.
                                     ORDER  BY Newid()                                            
                                    ";
    }
}
