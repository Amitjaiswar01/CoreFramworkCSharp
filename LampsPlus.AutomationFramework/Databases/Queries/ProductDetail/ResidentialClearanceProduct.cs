namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to find a Residential Product on Clearance, Not Eligible Member Special, No Company in Session.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7775
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7779
    /// </summary>
    public class ResidentialClearanceProduct
    {
        public const string Query = @"
                                    SET TRANSACTION isolation level READ uncommitted;

                                    SELECT TOP 1 p.shortsku,
                                                 initialretailprice,
                                                 retailprice,
                                                 retailpriceinternet,
                                                 Round(Floor(initialretailprice - retailpriceinternet), 0) AS SAVING
                                    FROM   carteasy.dbo.tblprductsextra pe
                                           INNER JOIN carteasy.dbo.tblprducts p
                                                   ON p.shortsku = pe.shortsku
                                    WHERE  pe.isbuttoneligible = 1
                                           AND instock = 1
                                           AND p.department NOT BETWEEN 80 AND 89
                                           AND clearanceflag = 1
                                           AND ( saleprice1internet = 0
                                                  OR saleprice1internet >= retailpriceinternet )
                                           AND ( Round(Floor(initialretailprice - retailpriceinternet), 0) > 5.00 )
                                    ORDER  BY Newid() 
                                    ";
    }
}
