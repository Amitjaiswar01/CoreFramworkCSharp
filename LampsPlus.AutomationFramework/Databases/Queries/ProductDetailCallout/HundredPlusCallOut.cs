namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetailCallout
{
    /// <summary>
    /// Query to identify One Hundred Plus callout eligible products
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7833
    /// </summary>
    public class HundredPlusCallOut
    {
        public const string Query = @"
                                    SET TRANSACTION isolation level READ uncommitted;

                                    SELECT TOP 1 p.shortsku,
                                                 productname
                                    FROM   carteasy..tblprducts p 
                                           INNER JOIN carteasy..tblprductsextra pe 
                                                   ON p.shortsku = pe.shortsku
                                           LEFT JOIN products.dbo.tblproductsearchcallouts psc 
                                                  ON psc.shortsku = pe.shortsku
                                    WHERE  pe.isbuttoneligible = 1
                                           AND p.instock = 1
                                           AND p.intranetonly = 0
                                           AND psc.callout = '100+ Colors'
                                           AND pe.FirstShipDays < 57
                                    ORDER  BY Newid() 
                                    ";
    }
}