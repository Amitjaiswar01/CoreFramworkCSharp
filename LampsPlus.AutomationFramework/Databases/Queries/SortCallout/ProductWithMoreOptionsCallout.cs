namespace LampsPlus.AutomationFramework.Databases.Queries.SortCallout
{
	/// <summary>
	/// Query to identify a SKU that has More Options callout. 
	/// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7835
	/// </summary>
	public class ProductWithMoreOptionsCallout
    {
		public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 p.shortsku, ProductName
                                    FROM Carteasy..tblPrducts p
                                    INNER JOIN Carteasy..tblPrductsextra pe
                                                ON p.shortsku = pe.shortsku
                                    LEFT JOIN products.dbo.tblProductSearchcallouts psc 
                                                ON psc.shortsku = pe.shortsku
                                    WHERE pe.IsbuttonEligible=1 
                                                AND p.InStock=1
                                                AND p.IntranetOnly=0
                                                AND psc.Callout='More Options'
                                                AND pe.FirstShipDays < 57
                                    ORDER BY Newid()                                   
                                    ";
    }
}