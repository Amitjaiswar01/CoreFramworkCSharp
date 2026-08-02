namespace LampsPlus.Automation.Tests.Databases.Queries.Sort
{
    /// <summary>
    /// Query to find a generic SKU to place at the end of the PLA3 URL.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T220
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1197
    /// </summary>
    public class Pla3ShortSku
    {
#pragma warning disable 1591
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 p.shortsku
                                    FROM carteasy.dbo.tblprducts p 
                                    INNER JOIN carteasy.dbo.tblprductsextra e 
	                                    ON e.shortsku = p.shortsku
                                    WHERE 0.10 >= Cast(Checksum(Newid(), p.shortsku) & 0x7FFFFFFF AS FLOAT) / Cast(0x7FFFFFFF AS INT)
	                                    AND instock = 1
	                                    AND listable = 1
	                                    AND (
		                                    retailpriceinternet > 0
		                                    OR saleprice1internet > 0
		                                    )
	                                    AND groupingsku IS NULL
                                    ORDER BY Newid()
                                    ";
#pragma warning restore 1591
    }
}
