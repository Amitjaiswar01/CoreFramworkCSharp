namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify a SKU that has a 'Housing Options' tab on the PDP. The category must include the word 'recessed'. The usage column
    /// must contain the word 'trim'. The usage column cannot be empty. The inventory must be greater than or equal to 2.
    /// The 'subcls' column must be certain values.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T234
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1211
    /// </summary>
    public class SkuThatHasHousingOptions
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 PP.SHORTSKU
                                    FROM carteasy..tblprducts PP 
                                    OUTER APPLY (
	                                    SELECT count(P.ShortSku) AS RecessedCount
	                                    FROM tblPrducts P 
	                                    INNER JOIN tblFamilies F 
		                                    ON P.ShortSKU = F.ItemSku
	                                    WHERE F.FamilySku = PP.Family
		                                    AND P.listable = 1
		                                    AND P.InStock = 1
		                                    AND P.IntranetOnly = 0
		                                    AND (
			                                     P.ShowPrice = 1
			                                    )
		                                    AND P.Usage NOT LIKE '%Trim%'
		                                    AND P.Usage <> ''
		                                    AND (			                                    
			                                     P.ShowPrice = 1
			                                    OR (
				                                    P.Inventory >= 2
				                                    AND (
					                                    (
						                                    P.SubCls = 6
						                                    OR P.SubCls = 11
						                                    OR P.SubCls = 12
						                                    )
					                                    OR (
						                                    P.Class = 92
						                                    AND P.SubCls = 9
						                                    )
					                                    )
				                                    )
			                                    )
	                                    ) RC
                                    WHERE pp.listable = 1
	                                    AND pp.instock = 1
	                                    AND pp.category LIKE '%recessed lighting%'
	                                    AND pp.usage LIKE 'TRIM'
	                                    AND RC.RecessedCount > 0
                                    ORDER BY NEWID()
                                    ";
    }
}
