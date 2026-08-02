namespace LampsPlus.AutomationFramework.Databases.Queries.ShoppingCart
{
    /// <summary>
    /// Query to fetch the Delivery days for a specific SKU
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T124
    /// </summary>
    class GetDeliveryDays
    {
        public const string Query = @"
                                    SET TRANSACTION isolation level READ uncommitted;

                                    SELECT TOP 1 p.shortsku,
                                                 firstdelvdays,
                                                 lastdelvdays
                                    FROM   carteasy.dbo.tblprducts P
                                           INNER JOIN carteasy.dbo.tblfreightcharges FC
                                                   ON FC.shortsku = P.shortsku
                                           INNER JOIN carteasy..tblproductsavailability pa
                                                   ON pa.shortsku = p.shortsku
                                    WHERE  listable = 1
                                           AND instock = 1
                                           AND firstshipdays = 1
                                           AND zone IN ( '1', '2' )
                                           AND qtyavail0399 > 0
                                    ORDER  BY Newid() 
                                    ";
    }
}
