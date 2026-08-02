namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify any SKU that has a PDP. 
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T256
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1234
    /// </summary>
    public class SingleSkuBathroomLighting
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
									INNER JOIN carteasy.dbo.tblprductsextra px  
                                        ON px.shortsku = p.shortsku 
                                    WHERE listable = 1
	                                    AND instock = 1
	                                    AND px.isbopuseligible = 0
	                                    AND retailpriceinternet > 0
	                                    AND p.shortsku NOT LIKE '%-%'
	                                    AND qtyavilablelampsplus > 0
	                                    AND p.shortsku NOT LIKE '00%'
										AND category = 'Bathroom Lighting'
                                        AND px.firstshipdays = 1 
                                        AND (pa.qtyavail0399 >= 3 OR pa.QtyAvail0394 >= 3) -- (4/4/22) Added to support the addition of East Coast warehouses.
                                        AND px.FirstShipDays < 57 -- (10/20/21) Added to ensure PDP has Add to Cart button.
										AND IsButtonEligible = 1
                                    ORDER BY Newid()
                                    ";
    }
}
