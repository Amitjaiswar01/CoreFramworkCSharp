namespace LampsPlus.AutomationFramework.Databases.Queries.SubmittingOrders
{
    /// <summary>
    /// Query to find an item that is greater than two hundred dollars.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T152
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T704
    /// </summary>   
    public class FindSkuGreaterThanTwoHundredDollars
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 p.shortsku 
                                    FROM   carteasy.dbo.tblprducts p  
                                           INNER JOIN carteasy.dbo.tblfreightcharges fc  
                                                   ON fc.shortsku = p.shortsku 
                                           INNER JOIN carteasy.dbo.tblcarriercodes cc  
                                                   ON cc.servicelevelcode = fc.servicelevel 
                                           INNER JOIN carteasy.dbo.tblproductsavailability pa  
                                                   ON pa.shortsku = p.shortsku 
                                           INNER JOIN carteasy.dbo.tblprductsextra pe  
                                                   ON pe.shortsku = p.shortsku 
										   INNER JOIN carteasy..tblFirstDisplayedInSort fd
												   ON p.Shortsku = fd.shortsku
                                    WHERE  0.10 >= Cast(Checksum(Newid(), p.shortsku) & 0x7fffffff AS FLOAT) / Cast( 
                                                          0x7fffffff AS INT) 
                                           AND listable = 1 
                                           AND instock = 1 
                                           AND ( retailpriceinternet BETWEEN 200 AND 225 
                                                  AND saleprice1internet = 0 ) 
                                           AND servicelevel NOT IN ( 104, 105, 108 ) 
                                           AND fc.sublocationcode = 9003 
                                           AND isdecrementable = 0 
                                           AND clearanceflag = 0 
                                           AND intranetOnly = 0
                                           AND pa.FirstShipDays < 57
                                           AND pe.IsButtonEligible = 1 -- (11/7/23) Added to ensure PDP has Add to Cart button.
                                           AND ISNULL(pa.QtyAvilablelampsplus, 0) - ISNULL(pa.NumberSoldToday, 0) = 0
                                           AND ( pe.groupingsku IS NULL 
                                                  OR pe.groupingsku = '' ) 
                                           AND [service type] NOT LIKE '%glove%' 
                                           AND fd.sublocationcode = 9003 -- (3/20/23) Added to avoid selecting Employee Only SKUS.
                                           GROUP BY p.shortsku
                                           ORDER BY NEWID()                                         
                                    ";
    }
}
