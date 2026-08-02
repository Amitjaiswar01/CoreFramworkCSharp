namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify any SKU that has a PDP. 
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T256
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1234
    /// </summary>
    public class SingleSku
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 p.shortsku,
	                                    pa.FirstShipDays,
	                                    pa.LastShipDays,
	                                    category
                                    FROM carteasy..tblprducts p 
                                    INNER JOIN carteasy..tblproductsavailability pa 
	                                    ON pa.shortsku = p.shortsku
                                    INNER JOIN carteasy..tblprductsextra pe 
	                                    ON pe.shortsku = p.shortsku
                                    WHERE listable = 1
	                                    AND instock = 1
	                                    AND pe.isbopuseligible = 0
	                                    AND retailpriceinternet > 0
	                                    AND qtyavilablelampsplus > 0
                                        AND GroupingSKU IS NULL
	                                    AND category NOT LIKE 'Bathroom Lighting, %'
	                                    AND category NOT LIKE 'Outdoor Lighting, %'
	                                    AND category NOT LIKE 'Outdoor Lighting'
	                                    AND category NOT LIKE 'Bathroom Lighting'
	                                    AND pa.qtyavail0399 < 3
                                        AND pa.FirstShipDays <= 3
                                    ORDER BY Newid()
                                    ";
    }
}
