namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify a Finial SKU that has multiple shipping options.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T111
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T663
    /// </summary>
    public class FinialWithMultipleShippingOptionsSku
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
                                    SELECT TOP 1 p.shortsku
                                    FROM   carteasy.dbo.tblprducts p
                                           INNER JOIN carteasy.dbo.tblprductsextra pe
                                           ON p.shortsku = pe.shortsku
                                    WHERE  p.instock = 1
                                           AND pe.isbuttoneligible = 1
                                           AND p.listable = 1
                                           AND ( pe.groupingsku IS NULL
                                                  OR pe.groupingsku = p.shortsku )
                                           AND p.retailpriceinternet <= 49
                                           AND firstshipdays < 57
                                    ORDER  BY Newid() 
                                    ";
    }
}
