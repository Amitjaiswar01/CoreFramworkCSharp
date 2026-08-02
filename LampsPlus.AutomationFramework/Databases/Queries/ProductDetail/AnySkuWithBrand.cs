namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify any SKU that has a Brand.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7457
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T242
    /// </summary>
    public class AnySkuWithBrand
    {
        public const string Query = @"
                                    USE carteasy 

                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1   pr.ShortSku, 
                                                     Mn.Manufacturer, 
                                                     'lampsplus.com' + Mn.linkto AS Url 
                                    FROM   carteasy.dbo.tblmanufacturer AS Mn 
                                           INNER JOIN carteasy.dbo.tblprducts AS Pr  										   
                                                   ON Mn.manufacturer = Pr.manufacturer 
												   INNER JOIN Carteasy.dbo.tblPrductsExtra px 
												   ON pr.ShortSku = px.ShortSKU
                                    WHERE  islinktoactive = 1 
                                           AND instock = 1 
                                           AND listable = 1 
										   AND IsButtonEligible = 1
                                           AND mn.linkto NOT IN ('/color-plus/') -- (3/31/22) Color Plus PDPs do not have a manufacturer link.
                                    ORDER BY Newid()
                                    ";
    }
}