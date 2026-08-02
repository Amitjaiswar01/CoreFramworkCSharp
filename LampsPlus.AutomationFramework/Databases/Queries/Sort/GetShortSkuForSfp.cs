namespace LampsPlus.AutomationFramework.Databases.Queries.Sort
{ 
    /// <summary>
    /// Query to find a SKU that will display all the necessary callouts for an SFP page.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7626
    /// </summary>
    public class ShortSkuForSfp
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 p.shortsku,
                                                 retailpriceinternet,
                                                 saleprice1internet,
                                                 endsale1                                                  AS
                                                 SaleEndDate,
                                                 Round(Floor(retailpriceinternet - saleprice1internet), 2) AS
                                                 Savings,
                                                 CASE
                                                   WHEN ( ( Floor(initialretailprice * 1.5) % 10 ) >= 5 ) THEN
                                                   ( Floor(( initialretailprice * 1.5 ) / 10) * 10 ) + 9.99
                                                   WHEN ( ( Floor(initialretailprice * 1.5) % 10 ) < 5 ) THEN
                                                   ( Floor(( initialretailprice * 1.5 ) / 10) * 10 ) + 4.99
                                                 END                                                       AS
                                                 compareprice,
                                                 endsale1
                                    FROM   carteasy.dbo.tblprducts p 
                                           INNER JOIN carteasy.dbo.tblprductsextra px 
                                                   ON p.shortsku = px.shortsku
                                           INNER JOIN carteasy.dbo.tblproductsavailability pa 
                                                   ON p.shortsku = pa.shortsku
                                    WHERE  specialdiscount = 0.00
                                           AND ( ( saleprice1internet > 0
                                                   AND saleprice1internet < retailpriceinternet )
                                                 AND pa.isdecrementable = 1 )
                                           AND islpproduct = 1
                                           AND instock = 1
                                           AND ( umrp = NULL
                                                  OR umrp = '' )
                                           AND Round(Floor(retailpriceinternet - saleprice1internet), 2) > 5.00
                                    ORDER  BY Newid() 
                                    ";
    }
}
