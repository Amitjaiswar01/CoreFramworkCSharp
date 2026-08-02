namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify two short skus
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T160
    /// </summary>
    public class GetTwoSkusWithNullUmrp
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 2 p.shortsku 
                                    FROM   carteasy.dbo.tblprductsextra px 
                                           INNER JOIN products.dbo.tblpricingpolicy pr 
                                                   ON px.umrpvendornumber = pr.vendornum 
                                           INNER JOIN carteasy.dbo.tblprducts p 
                                                   ON px.shortsku = p.shortsku 
                                           INNER JOIN carteasy.dbo.tblfreightcharges fc 
                                                   ON fc.shortsku = px.shortsku 
                                    WHERE  instock = 1 
                                           AND listable = 1 
                                           AND p.Category NOT LIKE '%Dimmer%'
                                           AND Isnull(umrp, 0.0) = 0.0 
                                           AND quantityrestriction = 1 
                                           AND discountrequirement = 1 
                                           AND internetdiscounting = 1 
                                           AND fc.sublocationcode = 9003 
                                           AND servicelevel = 888 
                                           AND zone = 2 
                                           AND IsButtonEligible = 1
                                           AND FirstShipDays < 57 -- (8/10/21) Added to ensure PDP has Add to Cart button.
                                           AND freightcharge = 0 
                                           AND ( retailpriceinternet > 50 
                                                  OR saleprice1internet > 50 ) 
                                           AND (
		                                    px.groupingsku IS NULL
		                                    OR px.groupingsku = ''
		                                    ) -- (8/11/21) Added because tests that use this query should not select multi-products which is controlled by GroupingSKU.
                                    ORDER  BY Newid() 
                                    ";
    }
}