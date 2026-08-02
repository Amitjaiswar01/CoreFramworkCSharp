namespace LampsPlus.AutomationFramework.Databases.Queries.SortCallout
{
    /// <summary>
    /// Query to find a SKU that qualifies for Free Shipping. SKU must NOT be part of certain categories. The manufacturer cannot be empty.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T209
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1186
    /// </summary>
    public class FreeShippingProduct
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 p.shortsku,
	                                    p.ProductName,
	                                    CASE 
		                                    WHEN p.saleprice1internet > 0.00
			                                    THEN p.saleprice1internet
		                                    ELSE p.retailpriceinternet
		                                    END AS Price,
	                                    p.productname
                                    FROM CartEasy.dbo.tblprducts p 
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
	                                    AND p.retailprice > 0.00
	                                    AND p.productname NOT LIKE '%" + "\"" + "%' AND p.shortsku = @shortsku";
    }
}
