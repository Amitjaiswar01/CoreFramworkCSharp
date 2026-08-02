namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify a Tiffany Color Plus SKU. The manufacturer must be 'Tiffany Color Plus'. The lpbrand must be 'CPLUS'.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T225
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1202
    /// </summary>
    public class TiffanyColorPlusSku
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 kd.KitSku AS shortsku
                                    FROM 
	                                    Products.dbo.tblkitdetails as kd
	                                    INNER JOIN Carteasy.dbo.tblPrducts cp  ON kd.ComponentSku = cp.ShortSKU and kd.SequenceNumber = 3
	                                    INNER JOIN Carteasy.dbo.tblPrducts p  ON kd.KitSku = p.ShortSKU
	                                    INNER JOIN Carteasy.dbo.tblPrductsExtra px  ON px.ShortSKU = p.SHortSKU
                                    WHERE p.manufacturer = 'Tiffany Color Plus'
	                                    AND p.listable = 1	                                    
	                                    AND p.instock = 1
	                                    AND px.isBopusEligible = 0
	                                    AND ComponentSku in 
		                                    (SELECT CP.ShortSku
		                                    FROM 
			                                    Products.dbo.tblkitdetails as kd
			                                    INNER JOIN Carteasy.dbo.tblPrducts cp  ON kd.ComponentSku = cp.ShortSKU and kd.SequenceNumber = 3
			                                    INNER JOIN Carteasy.dbo.tblPrducts p  ON kd.KitSku = p.ShortSKU
			                                    INNER JOIN Carteasy.dbo.tblPrductsExtra px  ON px.ShortSKU = p.SHortSKU
		                                    WHERE p.manufacturer = 'Tiffany Color Plus'
			                                    AND p.listable = 1	                                    
			                                    AND p.instock = 1
			                                    AND px.isBopusEligible = 0
		                                    GROUP BY CP.ShortSKU
		                                    HAVING COUNT(1) > 1)
                                    ORDER BY NEWID()
                                    ";
    }
}
