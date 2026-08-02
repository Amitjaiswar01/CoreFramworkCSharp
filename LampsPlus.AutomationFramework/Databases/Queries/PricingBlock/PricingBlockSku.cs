namespace LampsPlus.AutomationFramework.Databases.Queries.PricingBlock
{
    /// <summary>
    /// Query to identify an sku for the Pricing Block
    /// Automated Desktop Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7630
    /// Automated Mobile Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7631
    /// </summary> 
    public class PricingBlockSku
    {
    public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 p.shortsku,
                                    retailpriceinternet,
                                    saleprice1internet,
                                    Round(Floor(retailpriceinternet - saleprice1internet), 2) AS
                                    Savings,
                                    CASE
                                    WHEN ( ( Floor(initialretailprice * 1.5) % 10 ) >= 5 ) THEN
                                    ( Floor(( initialretailprice * 1.5 ) / 10) * 10 ) + 9.99
                                    WHEN ( ( Floor(initialretailprice * 1.5) % 10 ) < 5 ) THEN
                                    ( Floor(( initialretailprice * 1.5 ) / 10) * 10 ) + 4.99
                                    END AS
                                    compareprice,
                                    endsale1
                                    FROM carteasy.dbo.tblprducts p 
                                    INNER JOIN carteasy.dbo.tblprductsextra px 
                                    ON p.shortsku = px.shortsku
                                    INNER JOIN carteasy.dbo.tblproductsavailability pa 
                                    ON p.shortsku = pa.shortsku
                                    WHERE specialdiscount = 0.00
                                    AND ( ( saleprice1internet > 0
                                    AND saleprice1internet < retailpriceinternet )
                                    AND pa.isdecrementable = 1 )
                                    AND islpproduct = 1
                                         AND InStock = 1
                                    AND ( umrp = NULL
                                    OR umrp = '' )
                                    AND Round(Floor(retailpriceinternet - saleprice1internet), 2) > 5.00
                                    ORDER BY Newid()  
                                        ";
    }
}
