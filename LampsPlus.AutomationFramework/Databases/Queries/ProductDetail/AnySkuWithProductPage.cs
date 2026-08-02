namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify literally any SKU that has a PDP. The retail price OR sale price must be greater than 0. The item cannot be 'intranetonly'.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T242
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1220
    /// </summary>
    public class AnySkuWithProductPage
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 p.shortsku 
                                    FROM   carteasy.dbo.tblprducts p  
                                           INNER JOIN carteasy.dbo.tblprductsextra px  
                                                   ON px.shortsku = p.shortsku 
                                    WHERE  p.listable = 1 
                                           AND p.instock = 1 
                                           AND ( retailpriceinternet > 0 
                                                  AND saleprice1internet = 0 ) 
                                           AND intranetonly = 0 
										   AND FirstShipDays < 57 -- (6/5/21) Added to ensure PDP has Add to Cart button.
										   AND IsButtonEligible = 1
                                           AND groupingsku IS NULL 
                                           AND ( category IS NOT NULL 
                                                  OR category = '' ) 
										   AND (FinishFamily IS NULL 
												OR FinishFamily = '') -- (11/3/23) Added to remove SKUs that go to a Sort page instead of a PDP.
                                           AND px.canadashippable = 1 
                                           AND ( category NOT LIKE '%Dimmer%' 
                                                  OR category NOT LIKE '%Track%' ) 
                                           AND isbopuseligible = 0 
                                           AND (umrp IS NULL OR UMRP = '')
                                           AND retailpriceinternet BETWEEN '100.00' AND '700.00' -- (4/30/21) Added by request from PM so this query can be used for multiple tests.
                                           AND (Moreviews >= 3 OR HasCroppedImage = 1) -- (6/14/22) Added so there are more than 1 thumbnail image below main image on the PDP.
                                            ORDER  BY Newid() 
                                    ";
    }
}
