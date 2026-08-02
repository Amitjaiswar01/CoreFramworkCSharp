namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetailCallout
{
    /// <summary>
    /// Query to identify a SKU that qualifies for the "Ships Free With Orders Over $49" callout. The SKU must be in a certain category and the retailprice less than
    /// or equal to $49. The freight charge for the item must be '0' for Zone 1 for SubLocation Code '9003'. 
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T250
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1228
    /// </summary>
    public class ShipsFreeOnOrdersOver49CallOutShortSku
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 p.shortsku
                                    FROM carteasy.dbo.tblprducts p 
                                    INNER JOIN carteasy.dbo.tblfreightcharges fc 
	                                    ON fc.shortsku = p.shortsku
                                    INNER JOIN carteasy.dbo.categories c 
	                                    ON c.catname = p.category
                                    INNER JOIN carteasy.dbo.tblprductsextra pe 
	                                    ON pe.shortsku = p.shortsku
                                    WHERE fc.sublocationcode = '9003'
	                                    AND fc.zone = 1
	                                    AND freightcharge = 0
	                                    AND isbopuseligible = 0
	                                    AND listable = 1
	                                    AND instock = 1
										AND IsButtonEligible = 1
                                        AND pe.FirstShipDays < 57 -- (4/21/23) Added to ensure PDP has Add to Cart button.	
	                                    AND p.category NOT IN (
		                                    'Bathroom Lighting',
		                                    'Ceiling Fans',
		                                    'Close to Ceiling Lights',
		                                    'Desk Lamps',
		                                    'Floor Lamps',
		                                    'Landscape Lighting',
		                                    'Outdoor Lighting',
		                                    'Sconces',
		                                    'Wall Lamps',
		                                    'Table Lamps'
		                                    )
	                                    AND p.retailpriceinternet <= 49
	                                    AND saleprice1internet = 0	
                                        AND ISNULL(PE.GroupingSKU,'') = ''
                                    ORDER BY Newid()
                                    ";
    }
}
