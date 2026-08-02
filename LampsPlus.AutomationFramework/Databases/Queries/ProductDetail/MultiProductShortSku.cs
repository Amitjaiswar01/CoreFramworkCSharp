namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify a SKU that is a multi-product. The groupingsku can NOT be NULL.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T224
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1201
    /// </summary>
    public class MultiProductShortSku
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 p.shortsku
                                    FROM (
	                                    SELECT *,
		                                    Count(*) OVER (PARTITION BY groupingsku) AS 'Total'
	                                    FROM carteasy..tblprductsextra 
                                        WHERE FirstShipdays < 57 --Indicates the number of days it could take for an item to be in stock
	                                    ) pe
                                    INNER JOIN carteasy..tblprducts p 
	                                    ON p.shortsku = pe.shortsku
                                    WHERE 0.10 >= Cast(Checksum(Newid(), p.shortsku) & 0x7fffffff AS 
                                                                         FLOAT) / Cast ( 
                                                                            0x7fffffff AS INT)
                                        AND total > 1
	                                    AND Isnull(pe.groupingsku, ' ') <> ' '
                                        AND pe.GroupingSku = pe.ShortSKU
	                                    AND listable = 1	                                    
	                                    AND instock = 1
	                                    AND isBopusEligible = 0
                                        AND Total >= 3
                                    ORDER BY pe.groupingsku
                                    ";
    }
}
