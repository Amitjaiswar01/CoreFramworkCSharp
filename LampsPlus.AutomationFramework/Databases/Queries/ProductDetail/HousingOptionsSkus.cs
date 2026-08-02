namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify the 'Housing Options' skus for a given SKU that is displayed on PDP
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T234
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1211
    /// </summary>
    public class HousingOptionsSkus
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT P.ShortSku
                                    FROM tblPrducts PP 
                                    INNER JOIN tblFamilies F 
	                                    ON F.FamilySku = PP.Family
                                    LEFT JOIN tblPrducts P 
	                                    ON P.ShortSKU = F.ItemSku
                                    LEFT JOIN tblPrductsExtra PE 
	                                    ON PE.ShortSKU = P.ShortSKU
                                    LEFT JOIN Categories C 
	                                    ON P.Category LIKE C.Cat + '%'
                                    WHERE PP.ShortSKU = @shortsku
	                                    AND P.Listable = 1
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
                                    ";
    }
}
