namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetailCallout
{
    /// <summary>
    /// Query to identify a SKU that qualifies for Free Shipping and Returns. SKU must be part of a certain category. Item must not be on Clearance or a Daily Sale item.
    /// The retail price must be greater than $49. The SKU type can not be an 'Art Shade'. The lpbrand value can be NULL but NOT 'CPLUS'.
    /// There must be no freight charge for service level 888 to Zone 1 for SubLocation '9003'.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T249
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1227
    /// </summary>
    public class FreeShippingAndReturnShortSkus
    {
        public const string Query = @"
                                    USE carteasy;

                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									WITH altcat
                                    AS (
	                                    SELECT TOP 1000 id,
		                                    cat
	                                    FROM categories 
	                                    )
                                    SELECT TOP 1 pl.shortsku,
	                                    p.productname,
	                                    a.cat,
	                                    pe.islpproduct,
	                                    pl.category,
	                                    p.instock,
	                                    p.listable,
	                                    p.clearanceflag,
	                                    p.retailpriceinternet,
	                                    pa.isdecrementable,
	                                    pl.type,
	                                    pe.lpbrand,
	                                    fc.zone,
	                                    fc.freightcharge,
	                                    fc.servicelevel,
	                                    fc.sublocationcode
                                    FROM tblprodlist pl 
                                    INNER JOIN tblprducts p 
	                                    ON p.shortsku = pl.shortsku
                                    INNER JOIN tblprductsextra pe 
	                                    ON pl.shortsku = pe.shortsku
                                    INNER JOIN carteasy.dbo.tblproductsavailability pa 
	                                    ON pa.shortsku = pl.shortsku
                                    INNER JOIN tblfreightcharges fc 
	                                    ON pl.shortsku = fc.shortsku
                                    INNER JOIN categories c 
	                                    ON c.cat = pl.category
                                    INNER JOIN altcat a
	                                    ON CASE 
			                                    WHEN pl.category LIKE '%,%'
				                                    THEN Substring(pl.category, 0, Charindex(',', pl.category, 0))
			                                    ELSE pl.category
			                                    END = a.cat
                                    WHERE (
		                                    EXISTS (
			                                    SELECT TOP 1000 pl.category
			                                    WHERE pl.category IN (
					                                    'Ceiling Fans',
					                                    'Outdoor Lighting'
					                                    )
			                                    )
		                                    )
	                                    AND p.listable = 1
	                                    AND p.instock = 1
	                                    AND pe.isBopusEligible = 0
	                                    AND p.clearanceflag = 0
	                                    AND isdecrementable = 0
	                                    AND p.retailpriceinternet > 49
                                        AND p.saleprice1 = 0
	                                    AND pl.type NOT LIKE '%Art Shade%'
	                                    AND (
		                                    pe.lpbrand IS NULL
		                                    OR pe.lpbrand <> 'CPLUS'
		                                    )
	                                    AND fc.zone = 1
	                                    AND fc.freightcharge = 0.00
	                                    AND fc.servicelevel = 888
	                                    AND fc.sublocationcode = 9003
										AND IsButtonEligible = 1 -- (6/5/21) Add to ensure PDP always has Add to Cart button.
										AND pe.FirstShipDays < 57
                                        ORDER BY Newid()
                                    ";
    }
}
