namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify any SKU with a price between $30 and $1,350.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7937
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7938
    /// </summary>
    public class GetSkuWithPriceBetween30And1350
    {
        public const string Query = @"
                                    SET TRANSACTION isolation level READ uncommitted;

                                    SELECT TOP 1 p.shortsku
                                    FROM   carteasy.dbo.tblprducts p
                                           INNER JOIN carteasy.dbo.tblprductsextra px
                                                   ON px.shortsku = p.shortsku
                                    WHERE  p.instock = 1
                                           AND px.isbuttoneligible = 1
                                           AND p.department NOT BETWEEN 80 AND 89
                                           AND ( retailpriceinternet > 0
                                                 AND saleprice1internet = 0 )
                                           AND intranetonly = 0
                                           AND groupingsku IS NULL
                                           AND ( category IS NOT NULL
                                                  OR category = '' )
                                           AND px.canadashippable = 1
                                           AND ( category NOT LIKE '%Dimmer%'
                                                  OR category NOT LIKE '%Track%' )
                                           AND isbopuseligible = 0
                                           AND ( umrp IS NULL
                                                  OR umrp = '' )
                                           AND retailpriceinternet BETWEEN '30.00' AND '1350.00'
                                    ORDER  BY Newid() 
                                    ";
    }
}
