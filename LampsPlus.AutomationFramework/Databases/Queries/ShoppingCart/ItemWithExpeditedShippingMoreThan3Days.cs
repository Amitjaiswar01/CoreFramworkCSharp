namespace LampsPlus.AutomationFramework.Databases.Queries.ShoppingCart
{
    /// <summary>
    /// Query to find SKUs that will NOT have Expedited Processing. The inventory in the warehouse must be greater than 0. Expedited 
    /// Processing must be greater than 0 and equal to or more than $5. The Expedited Processing charge is NOT equal to or greater than
    /// 3rd Day shipping. The charge is calculated in the following way: if 5% of the retail price is less than $5, then Expedited Processing
    /// will be $5. Otherwise it will be Standard shipping charge + 5% of the retail price.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T125
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T677
    /// </summary>
    public class ItemWithExpeditedShippingMoreThan3Days
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									WITH p
                                    AS (
	                                    SELECT Row_number() OVER (
			                                    PARTITION BY shortsku ORDER BY shortsku
			                                    ) AS S,
		                                    *
	                                    FROM carteasy.dbo.tblprducts 
	                                    )
                                    SELECT TOP 1 CONVERT(MONEY, retailpriceinternet * .05) AS [Calculated Expedited],
	                                    CASE 
		                                    WHEN CONVERT(MONEY, retailpriceinternet * .05) <= 5
			                                    THEN 5
		                                    ELSE CONVERT(MONEY, retailpriceinternet * .05) + (
				                                    SELECT freightcharge
				                                    FROM carteasy..tblfreightcharges 
				                                    WHERE shortsku = p.shortsku
					                                    AND zone = f.zone
					                                    AND servicelevel = 888
					                                    AND sublocationcode = f.sublocationcode
				                                    )
		                                    END AS [Expedited Processing],
	                                    p.shortsku,
	                                    f.freightcharge
                                    FROM p 
                                    INNER JOIN carteasy..tblprductsextra pe 
	                                    ON p.shortsku = pe.shortsku
                                    INNER JOIN carteasy..tblfreightcharges f 
	                                    ON p.shortsku = f.shortsku
                                    INNER JOIN carteasy..tblproductsavailability pa 
	                                    ON pa.shortsku = p.shortsku
                                    WHERE p.listable = 1
	                                    AND p.instock = 1
	                                    AND f.sublocationcode = 9003
	                                    AND f.zone = 2
	                                    AND servicelevel = 3
	                                    AND qtyavail0399 > 0
	                                    AND (
		                                    CONVERT(MONEY, retailpriceinternet * .05) + (
			                                    SELECT freightcharge
			                                    FROM carteasy..tblfreightcharges 
			                                    WHERE shortsku = p.shortsku
				                                    AND zone = f.zone
				                                    AND servicelevel = 888
				                                    AND sublocationcode = f.sublocationcode
			                                    )
		                                    ) >= f.freightcharge
                                    ";
    }
}
