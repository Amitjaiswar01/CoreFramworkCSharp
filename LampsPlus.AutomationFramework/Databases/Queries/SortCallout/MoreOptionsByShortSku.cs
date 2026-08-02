

namespace LampsPlus.AutomationFramework.Databases.Queries.SortCallout
{
    /// <summary>
    /// Query to verify that a SKU is a Clearance item. 
    /// Automated Test Case:https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T207
    /// </summary>
    class MoreOptionsByShortSku
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 ShortSKU, 
                                    Callout 
                                    FROM products.dbo.tblproductsearchcallouts 
                                    WHERE ShortSKU = @shortsku
                                    ";
    }
}
