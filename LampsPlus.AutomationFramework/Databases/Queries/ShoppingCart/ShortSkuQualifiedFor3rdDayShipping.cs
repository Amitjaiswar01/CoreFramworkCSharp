namespace LampsPlus.AutomationFramework.Databases.Queries.ShoppingCart
{
    /// <summary>
    /// Query to identify an item that has 3rd Day Shipping. The service level must be '3'.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T124
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T676
    /// </summary>
    public class ShortSkuQualifiedFor3rdDayShipping
    {
        public const string Query = @"
                                    SET TRANSACTION isolation level READ uncommitted;

                                    SELECT TOP 1 FC.shortsku
                                    FROM   carteasy.dbo.tblfreightcharges FC
                                           INNER JOIN carteasy.dbo.tblprducts PR
                                                   ON FC.shortsku = PR.shortsku
                                           INNER JOIN carteasy.dbo.tblprductsextra pe
                                                   ON pe.shortsku = pr.shortsku
                                    WHERE  listable = 1
                                           AND instock = 1
                                           AND sublocationcode = '9003'
                                           AND zone = '2'
                                           AND servicelevel = '3'
                                           AND freightcharge > 0
                                           AND (
		                                    pe.groupingsku IS NULL
		                                    OR pe.groupingsku = ''
		                                    ) -- (5/20/22) Added to avoid selecting multi-products.
                                           AND NOT EXISTS (SELECT 1
                                                           FROM   carteasy.dbo.tblcombosku
                                                           WHERE  fc.shortsku = basesku)
                                    ORDER  BY Newid() 
                                    ";
    }
}
