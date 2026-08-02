namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify Open box SKU.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7841
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7842
    /// </summary>

    class GetOpenBoxItemWithName
    {
        public const string Query = @"
                              SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
                              SELECT TOP 1 p.shortsku, ProductName
                              FROM carteasy..tblprducts p 
                              INNER JOIN carteasy..tblprductsextra pe ON pe.shortsku = p.shortsku
                              WHERE pe.Listable58=1 AND pe.RetailPrice58 > 0 AND Inventory58 > 0 AND InStock=1
                              ORDER BY Newid()";
    }
}
