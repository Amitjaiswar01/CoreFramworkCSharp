namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify SKUs for Pros Customer Trade Price.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7759
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T6993
    /// </summary>
    public class ProsProductTrade
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

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

                                    SELECT TOP 1 p.shortsku, specialdiscount, umrp, retailpriceinternet,RetailPrice, saleprice1internet, saleprice1,
                                    CAST(retailpriceinternet - ( Round(Floor(Rtrim(retailpriceinternet * @DiscountRate)) / 100, 2)) AS DECIMAL(12,2)) AS TradePrice,
                                    Round(Floor( retailpriceinternet - specialdiscount ), 0) AS Saving
                                    FROM carteasy..tblprducts p
                                    INNER JOIN carteasy..tblprductsextra pe (nolock) ON pe.shortsku = p.shortsku
                                    WHERE IntranetOnly = 0
                                    AND isbuttoneligible = 1 AND instock = 1
                                    AND p.department NOT BETWEEN 80 AND 89
                                    AND specialdiscount > 0 AND specialdiscount < retailpriceinternet
                                    AND clearanceflag = 0 AND (SalePrice1Internet = 0 OR SalePrice1Internet >= RetailPriceInternet)
                                    AND (pe.GroupingSku is null OR pe.GroupingSku=p.ShortSKU)
                                    AND UMRP = 0
                                    AND Round(Floor(retailpriceinternet - SpecialDiscount), 2) > 5--Saving > 5
                                    ORDER BY Newid()
                                    ";
    }
}
