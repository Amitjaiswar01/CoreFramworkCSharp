namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify the Trade Pricing for a Professional. The Special Discount is '0'. The SKU must not be part of a groupingsku.
    /// The UMRP must be NULL or empty. The sale price must be greater than '0' OR the sale price is greater than 0 AND less than the
    /// Special Discount price AND less than the retail price, OR the sale price is less than the retail price, equal to the Special
    /// Discount price when both the Special Discount and Sale price are not '0'. This only applies for certain vendors.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T254
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1232
    /// </summary>
    public class TradePriceInfo
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									DECLARE @CurrentDiscountRate DECIMAL(18, 2) 
                                    DECLARE @OrderTotal DECIMAL 
                                    DECLARE @DiscountStructure DECIMAL(18, 2) 
                                    DECLARE @DiscountRate DECIMAL(18, 2) 

                                    SET @CurrentDiscountRate = (SELECT TOP 1 currentdiscountrate 
                                                                FROM   userprofile.dbo.tblcompany  
                                                                WHERE  accountnumber = 1000000246) 
                                    SET @orderTotal = (SELECT TOP 1 ordertotal 
                                                       FROM   userprofile.dbo.tblcompany  
                                                       WHERE  accountnumber = 1000000246) 

                                    SELECT @DiscountStructure = Max(Isnull(discountrate, 0.00)) 
                                    FROM   userprofile.dbo.tblcompanydiscountstructure  
                                    WHERE  startingtierprice <= @OrderTotal 

                                    IF ( @DiscountStructure > @CurrentDiscountRate ) 
                                      BEGIN 
                                          SET @DiscountRate = @DiscountStructure 
                                      END 
                                    ELSE 
                                      BEGIN 
                                          SET @DiscountRate = @CurrentDiscountRate 
                                      END 

                                    SELECT TOP 1 p.shortsku, 
                                                 specialdiscount, 
                                                 umrp, 
                                                 retailpriceinternet, 
                                                 saleprice1internet, 
                                                 saleprice1, 
                                                 vendornum, 
                                                 clearanceflag, 
                                                 productname, 
                                                 groupingsku, 
                                                 Round(Floor(retailpriceinternet), 2) - ( 
                                                 Round(Floor(( saleprice1internet - ( 
                                                               saleprice1internet * 
                                                               @DiscountRate ) / 
                                                               100 
                                                             )), 2) ) AS YourSavings, 
                                                 saleprice1internet - ( Round(Floor(Rtrim(saleprice1internet * 
                                                                                          @DiscountRate)) / 
                                                                              100, 2) )                        AS 
                                                 TradePrice 
                                    FROM   carteasy.dbo.tblprducts p  
                                           INNER JOIN carteasy.dbo.tblprductsextra pe  
                                                   ON p.shortsku = pe.shortsku 
                                    WHERE  listable = 1 
                                           AND specialdiscount = 0 
                                           AND groupingsku IS NULL 
                                           AND isbopuseligible = 0 
                                           AND instock = 1 
                                           AND ( pe.umrp = '' 
                                                  OR pe.umrp IS NULL ) 
                                           AND ( saleprice1internet > 0 
                                                  OR ( saleprice1internet > 0 
                                                       AND saleprice1internet < specialdiscount 
                                                       AND saleprice1internet < retailpriceinternet ) 
                                                  OR ( saleprice1internet < retailpriceinternet 
                                                       AND specialdiscount = saleprice1internet 
                                                       AND specialdiscount <> 0 
                                                       AND saleprice1internet <> 0 ) ) 
                                           AND vendornum NOT IN ( '1860', '1420', '3705', '484', 
                                                                  '0370', '370', '1486', '693', 
                                                                  '921', '1135', '3706', '75', 
                                                                  '780', '1412', '1425', '3435', 
                                                                  '3705', '702', '721', '953', 
                                                                  '1359', '453', '691', '695', 
                                                                  '1390', '1735', '1930', '2090', 
                                                                  '1143', '426', '1470', '3707', 
                                                                  '1820', '3703', '1072', '3708', 
                                                                  '2090', '711', '1468', '462', 
                                                                  '244', '518', '2137', '831', 
                                                                  '2129', '32', '1992', '0699', 
                                                                  '699', '0915', '915', '1189' ) 
                                    GROUP  BY p.shortsku, 
                                              specialdiscount, 
                                              umrp, 
                                              saleprice1internet, 
                                              saleprice1, 
                                              vendornum, 
                                              clearanceflag, 
                                              retailpriceinternet, 
                                              productname, 
                                              groupingsku 
                                    ORDER  BY Newid() 
                                    ";
    }
}
