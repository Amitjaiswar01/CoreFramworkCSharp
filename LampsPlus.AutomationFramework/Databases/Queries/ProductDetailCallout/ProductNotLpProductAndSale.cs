namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetailCallout
{
    /// <summary>
    /// Query to identify a SKU that is NOT an LPPRODUCT and it is on sale. This will result in NO 'Compare' callout on the PDP.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T253
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1231
    /// </summary>
    public class ProductNotLpProductAndSale
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 p.shortsku
                                    FROM carteasy.dbo.tblprducts p 
                                    INNER JOIN carteasy.dbo.tblprductsextra pe 
	                                    ON p.shortsku = pe.shortsku
                                    WHERE pe.islpproduct = 0	                                    
	                                    AND p.listable = 1
	                                    AND isbopuseligible = 0
	                                    AND p.instock = 1
	                                    AND p.saleprice1 > 0.00
                                    ORDER BY Newid()
                                    ";
    }
}
