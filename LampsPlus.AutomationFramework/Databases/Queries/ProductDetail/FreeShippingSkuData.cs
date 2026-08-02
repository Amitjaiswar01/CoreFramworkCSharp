namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify a SKU that qualifies for Free Shipping. SKU must be in a certain category. There must be no freight charge for
    /// a Zone 1 ZIP code. There must be a manufacturer - it cannot be empty or NULL. The retail price must be greater than '0'.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T221
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1198
    /// </summary>
    public class FreeShippingSkuData
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 p.shortsku,
	                                    CASE 
		                                    WHEN NOT p.endsale1 IS NULL
			                                    AND p.endsale1 > Getdate()
			                                    THEN p.saleprice1internet
		                                    ELSE p.retailpriceinternet
		                                    END AS Price,
	                                    p.productname
                                    FROM tblprducts p 
                                    INNER JOIN tblfreightcharges fc 
	                                    ON p.shortsku = fc.shortsku
                                    WHERE p.category NOT IN (
		                                    'Bathroom Lighting',
		                                    'Close to Ceiling Lights',
		                                    'Desk Lamps',
		                                    'Floor Lamps',
		                                    'Landscape Lighting',
		                                    'Sconces',
		                                    'Wall Lamps',
		                                    'Table Lamps',
		                                    'Ceiling Fans',
		                                    'Outdoor Lighting'
		                                    )
	                                    AND p.listable = 1
	                                    AND p.instock = 1
	                                    AND fc.zone = 1
	                                    AND fc.freightcharge = 0.00
	                                    AND fc.sublocationcode = 9003
	                                    AND p.manufacturer != ''
	                                    AND p.retailprice > 0.00
	                                    AND p.shortsku = @shortsku
                                    ";
    }
}
