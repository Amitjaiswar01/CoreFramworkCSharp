namespace LampsPlus.AutomationFramework.Databases.Queries.ShoppingCart
{
    /// <summary>
    /// Query to find a White Glove Item
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T140
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T692
    /// </summary>
    public class WhiteGloveItem
    {
        public const string Query = @"
                                        SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
										SELECT TOP 1 p.shortsku
                                            FROM   carteasy.dbo.tblfreightcharges fc 
                                        INNER JOIN carteasy.dbo.tblcarriercodes cc 
                                        ON CC.servicelevelcode = FC.servicelevel
                                            INNER JOIN carteasy.dbo.tblprducts p 
                                        ON p.shortsku = fc.shortsku
                                            WHERE servicelevel IN ( 104, 105, 108 )
                                        AND sublocationcode = 9003
                                        AND zone = 1
                                        AND listable = 1
                                        AND instock = 1
                                        AND[service type] LIKE '%white glove%' 
                                        AND retailpriceinternet < 250
                                        AND p.clearanceflag = 0
                                        ORDER BY newid()
                                    ";
    }
}
