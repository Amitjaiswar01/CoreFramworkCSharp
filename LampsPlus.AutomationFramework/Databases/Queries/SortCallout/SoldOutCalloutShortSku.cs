namespace LampsPlus.AutomationFramework.Databases.Queries.SortCallout
{
    /// <summary>
    /// Query to find a Sold out Callout SKU 
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7832
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7836
    /// </summary>
    public class SoldOutCalloutShortSku
    {
        public const string Query = @"
                                    SET TRANSACTION isolation level READ uncommitted;

                                    SELECT TOP 1 p.shortsku, ProductName, pa.FirstShipDays
                                    FROM Carteasy.dbo.tblPrducts p  
                                    INNER JOIN Carteasy.dbo.tblPrductsExtra pe  ON p.ShortSKU = pe.ShortSKU
                                    LEFT JOIN Carteasy.dbo.tblProductsAvailability pa  ON pa.shortsku = p.shortsku
                                    WHERE p.instock = 0
                                    AND p.department NOT BETWEEN 80 AND 89
                                    AND pe.IsButtonEligible = 1
                                    AND pa.FirstShipDays > 57
                                    AND p.IntranetOnly = 0
                                    AND DATEADD(d, 3, outofstockdate) > getdate()
                                    AND (pa.isdecrementable = 1 OR clearanceflag = 1)
                                    ORDER BY NewId()
                                    ";
    }
}