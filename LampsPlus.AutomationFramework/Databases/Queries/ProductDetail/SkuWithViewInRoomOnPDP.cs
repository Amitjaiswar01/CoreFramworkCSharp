namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify a SKU that has the 'View In Room' link on the PDP. 
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T173
    /// </summary>
    public class FindSkuWithViewInRoomOnPDP
    {
        public const string Query = @"
                              SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
							SELECT TOP 1 P.shortsku, 
                                 C.catname 
                              FROM   carteasy.dbo.tblprducts P 
                              INNER JOIN carteasy.dbo.tblprductsextra PP 
                                  ON PP.shortsku = P.shortsku 
                              INNER JOIN carteasy.dbo.categories C  
                                  ON C.cat = ( CASE 
                              WHEN Charindex(',', P.category) = 0 THEN 
                                  P.category 
                              ELSE LEFT(P.category, Charindex(',', P.category) 
                                                    - 1) 
                                 END ) 
                              INNER JOIN [Carteasy].[dbo].[tblfreightcharges] fc  
                                  ON fc.shortsku = p.shortsku 
                             WHERE  zone = 2 
                             AND [sublocationcode] = 9003 
                             AND ( servicelevel = 888 
                                     OR servicelevel = 111 ) 
                             AND C.id IN ( 1, 2, 3, 4, 
                                5, 6, 7, 8, 
                                9, 12, 14, 
                                15, 16, 17, 18, 
                                19, 20, 21, 22, 
                                23, 25, 26, 27, 
                                28, 30, 31, 36, 
                                37, 38, 39, 40, 
                                41, 42, 43, 44, 
                                46, 47, 49, 50, 
                                51, 52, 53, 54, 
                                55, 56, 57, 58, 
                                59, 60, 61, 62, 
                                64, 65, 66, 67, 
                                68, 69, 70, 71, 
                                72, 73, 75, 83, 
                                84, 85, 86 ) 
                            AND PP.isclippable = 1 
                            AND IsButtonEligible = 1
                            AND FirstShipDays < 57 -- (7/7/22) Added to PDP has Add to Cart button.
                            AND listable = 1 
                            AND instock = 1 
                            AND ISNULL(PP.groupingsku, '') = ''
                            ORDER BY Newid()
                            ";
    }
}

