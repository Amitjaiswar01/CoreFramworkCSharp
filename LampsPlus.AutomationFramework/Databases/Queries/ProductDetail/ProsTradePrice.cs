namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify any SKU that has a Brand.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7457
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T242
    /// </summary>
    public class ProsTradePrice
    {
        public const string Query = @"
                                    DECLARE @CurrentDiscountRate DECIMAL(18, 2)
                                    DECLARE @OrderTotal DECIMAL
                                    DECLARE @DiscountStructure DECIMAL(18, 2)
                                    DECLARE @DiscountRate DECIMAL(18, 2)

                                    SET @CurrentDiscountRate = (SELECT TOP 1 currentdiscountrate
                                    FROM userprofile.dbo.tblcompany
                                    WHERE accountnumber = 1000000246)
                                    SET @orderTotal = (SELECT TOP 1 ordertotal
                                    FROM userprofile.dbo.tblcompany
                                    WHERE accountnumber = 1000000246)

                                    SELECT @DiscountStructure = Max(Isnull(discountrate, 0.00))
                                    FROM userprofile.dbo.tblcompanydiscountstructure
                                    WHERE startingtierprice <= @OrderTotal

                                    IF( @DiscountStructure > @CurrentDiscountRate )
                                    BEGIN
                                    SET @DiscountRate = @DiscountStructure
                                    END
                                    ELSE
                                    BEGIN
                                    SET @DiscountRate = @CurrentDiscountRate
                                    END

                                    SELECT TOP 1 p.shortsku, specialdiscount, umrp, retailpriceinternet, saleprice1internet, saleprice1,
                                    CAST(retailpriceinternet - ( Round(Floor(Rtrim(retailpriceinternet * @DiscountRate)) / 100, 2) ) AS DECIMAL(12,2)) AS TradePrice,
                                    Round(Floor(retailpriceinternet - ( retailpriceinternet - ( Round( Floor(Rtrim(retailpriceinternet * @DiscountRate) ) / 100, 2)))),0) AS SAVING
                                    FROM carteasy..tblprducts p (nolock)
                                    INNER JOIN carteasy..tblprductsextra pe (nolock) ON pe.shortsku = p.shortsku
                                    INNER JOIN carteasy..tblproductsavailability pa (nolock) ON pe.shortsku = pa.shortsku
                                    WHERE pe.isbuttoneligible = 1 AND instock = 1
                                    AND p.department NOT BETWEEN 80 AND 89
                                    AND specialdiscount = 0
                                    AND (SalePrice1Internet = 0 OR SalePrice1Internet >= RetailPriceInternet)
                                    AND clearanceflag = 0
                                    AND UMRP = 0
                                    AND retailpriceinternet - ( retailpriceinternet - ( Round( Floor(Rtrim(retailpriceinternet * @DiscountRate) ) / 100, 2) ) ) > 5 --Saving > 5
                                    ORDER BY Newid()
                                    ";
    }
}