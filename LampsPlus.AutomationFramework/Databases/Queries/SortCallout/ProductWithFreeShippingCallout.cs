namespace LampsPlus.AutomationFramework.Databases.Queries.SortCallout
{
    /// <summary>
    /// Query to get a random sku what has "free shipping" callout
    /// </summary>
    public class ProductWithFreeShippingCallout
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
                                    INNER JOIN tblfreightcharges fc  ON p.shortsku = fc.shortsku
									INNER JOIN tblPrductsExtra pe  on p.shortsku = pe.shortsku
									INNER JOIN tblFreeShippingReturn FSR  ON p.shortsku = fsr.shortsku and fsr.SubLocationCode = 9003
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
	                                    AND p.retailprice > 50
                                        AND isnull(pe.groupingsku, '') = ''
										AND FSR.Freeshipping = 1 AND FSR.FreeReturn = 0
	                                ORDER BY NewID()
                                    ";
    }
}
