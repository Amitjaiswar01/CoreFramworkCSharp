namespace LampsPlus.AutomationFramework.Databases.Queries.SortCallout
{
    /// <summary>
    /// Query to identify a SKU that is eligible for Free Shipping and Free Returns. SKU must be part of a particular category.
    /// The item cannot be on Clearance or a Daily Sale item. It cannot be an Art Shade. The LPBRAND is NULL and cannot be 'CPLUS'. 
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T210
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1187
    /// </summary>
    public class FreeShippingFreeReturnsInformation
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 pl.shortsku, 
                                                 CASE 
                                                   WHEN NOT p.endsale1 IS NULL 
                                                        AND p.endsale1 > Getdate() THEN p.saleprice1internet 
                                                   ELSE p.retailpriceinternet 
                                                 END AS Price, 
                                                 p.productname 
                                    FROM   tblprodlist pl  
                                           INNER JOIN tblprducts p  
                                                   ON p.shortsku = pl.shortsku 
                                           INNER JOIN tblprductsextra pe  
                                                   ON pl.shortsku = pe.shortsku 
                                           INNER JOIN carteasy.dbo.tblproductsavailability pa  
                                                   ON pa.shortsku = pl.shortsku 
                                           INNER JOIN tblfreightcharges fc  
                                                   ON pl.shortsku = fc.shortsku 
                                    WHERE  ( EXISTS (SELECT TOP 1 pl.category -- (2/3/23) Updated categories that allow Free Returns.
                                                     WHERE  pl.category LIKE '%Sconces%' 
                                                            AND pe.islpproduct = 1 
                                                             OR pl.category LIKE '%Close to Ceiling Lights%' 
                                                                AND pe.islpproduct = 1 
                                                             OR pl.category LIKE '%Wall Lamps%' 
                                                                AND pe.islpproduct = 1 
                                                             OR pl.category LIKE '%Bathroom lighting%' 
                                                                AND pe.islpproduct = 1) 
                                              OR EXISTS (SELECT TOP 1 pl.category 
                                                         WHERE  pl.category LIKE '%Ceiling Fans%' 
                                                                 OR pl.category LIKE '%Outdoor Lighting%') ) 
                                           AND p.listable = 1 
                                           AND p.instock = 1 
                                           AND p.clearanceflag = 0 
                                           AND isdecrementable = 0                                           
                                           AND ( pe.lpbrand IS NULL 
                                                  OR pe.lpbrand <> 'CPLUS' ) 
                                           AND fc.zone = 1 
                                           AND fc.freightcharge = 0.00 
                                           AND fc.servicelevel = 888 
                                           AND fc.sublocationcode = 9003 
                                           AND p.shortsku = @shortsku 
                                    ";
    }
}
