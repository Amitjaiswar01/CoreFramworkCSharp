namespace LampsPlus.AutomationFramework.Databases.Queries.SortCallout
{
    /// <summary>
    /// Query to identify SKUs that are Sold Out. The item has an InStock value of 0. The item is eligible for both Clearance or Daily Sale.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T199
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1176
    /// </summary>
    public class RandomSoldOutItemShortSku
    {
        public const string Query = @"
                                    USE carteasy

                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT Top 1 t.shortsku
                                    FROM carteasy.dbo.tblprducts t 
                                    INNER JOIN tblprductsextra PE 
	                                    ON PE.shortsku = T.shortsku
                                    LEFT JOIN dbo.tblproductsavailability pa 
	                                    ON pa.shortsku = t.shortsku
                                    WHERE listable = 1
										AND groupingSKU IS NULL	
										AND IntranetOnly = 0	
	                                    AND T.instock = 0
	                                    AND (
		                                    clearanceflag = 1
		                                    OR pa.isdecrementable = 1
		                                    )
										AND T.OutofStockDate BETWEEN DATEADD(DAY, -2, GETDATE()) AND DATEADD(HOUR, -8, GETDATE())
										Order By NewID()
                                    ";
    }
}
