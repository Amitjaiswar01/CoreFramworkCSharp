namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetailCallout
{
    /// <summary>
    /// Query to find an LPPRODUCT SKU on sale which will have a 'Compare' callout. The item cannot be on Clearance or have a Special Discount. 
    /// The 'Compare' price ends in 9.99 or 4.99 depending on whether the remainder of (initialretailprice * 1.5) is greater than or less than 5.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T252
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1230
    /// </summary>
    public class LPProductOnSaleWithComparePrice
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 p.shortsku,
	                                    CASE 
		                                    WHEN ((Floor(initialretailprice * 1.5) % 10) >= 5)
			                                    THEN (Floor((initialretailprice * 1.5) / 10) * 10) + 9.99
		                                    WHEN ((Floor(initialretailprice * 1.5) % 10) < 5)
			                                    THEN (Floor((initialretailprice * 1.5) / 10) * 10) + 4.99
		                                    END AS compareprice
                                    FROM tblproductsavailability pa 
                                    INNER JOIN carteasy.dbo.tblprducts p 
	                                    ON pa.shortsku = p.shortsku
                                    INNER JOIN carteasy.dbo.tblprductsextra pe 
	                                    ON p.shortsku = pe.shortsku
                                    WHERE isdecrementable = 1
	                                    AND listable = 1
	                                    AND instock = 1
	                                    AND pa.isbopuseligible = 0
	                                    AND islpproduct = 1
	                                    AND clearanceflag = 0
	                                    AND specialdiscount = 0
	                                    AND retailprice = retailpriceinternet
	                                    AND (
		                                    saleprice1 > 0
		                                    OR saleprice1 = 0
		                                    )	                                    
                                    ORDER BY Newid()
                                    ";
    }
}
