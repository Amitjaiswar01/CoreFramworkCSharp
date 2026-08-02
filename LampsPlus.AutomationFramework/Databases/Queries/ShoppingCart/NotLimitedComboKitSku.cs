namespace LampsPlus.AutomationFramework.Databases.Queries.ShoppingCart
{
    /// <summary>
    /// Query to identify a ComboKitSku that does NOT have limited inventory. The inventory must be more than 21. The BaseSku of the 
    /// ComboKitSku must not be the same as sku1. The lpbrand cannot be 'CPLUS'.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T123
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T675
    /// </summary>
    public class NotLimitedComboKitSku
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 p.shortsku 
                                    FROM   carteasy.dbo.tblprductsextra px 
                                           INNER JOIN carteasy.dbo.tblprducts p 
                                                   ON p.shortsku = px.shortsku 
                                    WHERE  px.shortsku NOT LIKE '00%' 
                                           AND px.shortsku NOT LIKE '%0%' 
                                           AND instock = 1 
                                           AND listable = 1 
                                           AND kitskutype = 2 
                                    ORDER  BY Newid()
                                    ";
    }
}
