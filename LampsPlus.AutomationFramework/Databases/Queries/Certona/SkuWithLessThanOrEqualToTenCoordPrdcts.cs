namespace LampsPlus.AutomationFramework.Databases.Queries.Certona
{
    /// <summary>
    /// Query to find a SKU that has less than or equal to 10 coordinating products so the widgets on the page will be populated with Certona SKUs as well as SKUs from the database.
    /// The category can not have the word 'Dimmer', 'Track', or 'Recessed Lighting' in it. The manufacturer cannot be Schonbek.
    /// The usage cannot contain the words 'pendant' or 'head'. The inventory must be greater than 1 including the number sold for the current date.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T315
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T872
    /// </summary>
    public class SkuWithLessThanOrEqualToTenCoordPrdcts
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 cp.shortsku,
	                                    Count(cp.coordinatingsku) AS TotalNumberofCoordinatingProducts
                                    FROM tblcoordinatingproduct cp
                                    INNER JOIN carteasy.dbo.tblprducts p
	                                    ON p.shortsku = cp.shortsku
                                    INNER JOIN carteasy.dbo.tblproductsavailability pa
	                                    ON p.shortsku = pa.shortsku
                                    WHERE cp.shortsku NOT IN (
		                                    SELECT shortsku
		                                    FROM products.dbo.tblproductsearchcallouts
		                                    )
	                                    AND p.manufacturer <> 'Schonbek'
	                                    AND p.category NOT LIKE '%Dimmer%'
	                                    AND p.category NOT LIKE '%Track%'
	                                    AND p.category NOT LIKE '%Recessed Lighting%'
                                        AND p.category NOT LIKE '%Landscape Lighting%' -- (Added 10/13/21) Landscape Lighting has been added to BFS template and must be ignored.
	                                    AND p.usage NOT LIKE '%pendant%'
	                                    AND p.usage NOT LIKE '%head%'
	                                    AND p.instock = 1
                                        AND p.listable = 1
	                                    AND p.inventory > 0
	                                    AND pa.isdecrementable = 0
	                                    AND pa.qtyavilablelampsplus - pa.numbersoldtoday > 0
                                    GROUP BY cp.shortsku
                                    HAVING Count(cp.coordinatingsku) <= 10
                                    ORDER BY Newid()
                                    ";
    }
}
