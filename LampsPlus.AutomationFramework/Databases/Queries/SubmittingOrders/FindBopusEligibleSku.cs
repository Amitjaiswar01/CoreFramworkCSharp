namespace LampsPlus.AutomationFramework.Databases.Queries.SubmittingOrders
{
    /// <summary>
    /// Query to identify a Bopus Eligible Sku.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T139
    /// </summary>
    public class FindBopusEligibleSku
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 p.shortsku
                                    FROM carteasy.dbo.tblprducts p 
                                    INNER JOIN carteasy.dbo.tblprductsextra pe 
	                                    ON p.shortsku = pe.shortsku
                                    INNER JOIN carteasy.dbo.tblproductsavailability pa 
	                                    ON pa.shortsku = p.shortsku
                                    INNER JOIN carteasy.dbo.tblfreightcharges b 
	                                    ON b.shortsku = p.shortsku
                                    WHERE pe.isbopuseligible = 0
	                                    AND p.vendornum NOT IN (
		                                    '2198',
		                                    '2298',
		                                    '2398',
		                                    '2498',
		                                    '2598'
		                                    )
	                                    AND p.listable = 1
	                                    AND p.instock = 1
                                        AND ISNULL(groupingsku, '') = '' -- (10/30/23) Added to avoid selecting multiproduct SKUs which will break tests using this query.
	                                    AND (
		                                    retailpriceinternet BETWEEN 10
			                                    AND 20
		                                    AND saleprice1internet = 0 -- (12/16/21) Exclude Sale items because they use a different callout that can alter the results of the test.
		                                    )
	                                    AND (
		                                    (
			                                    PA.qtyavail0399 > 0
			                                    AND PE.deliverypolicyid = 'LTL'
			                                    )
		                                    OR EXISTS (
			                                    SELECT 1
			                                    FROM carteasy.dbo.tblfreightcharges AS FC 
			                                    WHERE FC.shortsku = PE.shortsku
				                                    AND Isnull(FC.servicelevel, 0) = 888
			                                    )
		                                    )
                                    ORDER BY Newid()
                                    ";
    }
}
