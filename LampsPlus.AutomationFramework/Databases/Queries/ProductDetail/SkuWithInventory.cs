namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify SKU that has an inventory. 
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T133
    /// </summary>
    public class SkuWithInventory
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									DECLARE @ShortSKU NVARCHAR(30) 

                                    SET @ShortSKU = (SELECT TOP 1 shortsku 
                                                     FROM   carteasy.dbo.tblprducts 
                                                     WHERE  0.10 >= Cast(Checksum(Newid(), shortsku) & 0x7fffffff AS 
                                                                         FLOAT) / Cast ( 
                                                                            0x7fffffff AS INT) 
                                                            AND instock = 1 
                                                            AND listable = 1                                                            
                                                            AND ( retailpriceinternet > 1 
                                                                   OR saleprice1internet > 1 ) 
                                                      ) 

                                    SELECT TOP 1 p.shortsku, 
                                                 p.inventory AS 'warehouseinventory' 
                                    FROM   carteasy.dbo.tblprducts p 
                                           INNER JOIN carteasy.dbo.tblprductsextra pe 
                                                   ON pe.shortsku = p.shortsku 
                                           INNER JOIN carteasy.dbo.tblstoreinventory si 
                                                   ON si.shortsku = p.shortsku 
                                    WHERE  p.shortsku = @ShortSKU 
                                           AND si.storenumber = '-1' 
                                    ";
    }
}
