namespace LampsPlus.AutomationFramework.Databases.Queries.SortCallout
{
    /// <summary>
    /// Query to find the Sixteen Plus Colors SKU.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7834
    /// </summary>
    public class SixteenPlusColorsCallout
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
                                           AND pe.firstshipdays < 57
                                           AND p.intranetonly = 0
                                           AND psc.callout = '16+ Colors'
                                    ORDER  BY Newid() 
                                                ";
    }
}