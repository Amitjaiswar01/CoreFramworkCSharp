namespace LampsPlus.AutomationFramework.Databases.Queries.ShoppingCart
{
    /// <summary>
    /// Query to identify the freight charges for a specific SKU for Zone 2.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T124
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T676
    /// </summary>
    public class ProductFreightCharges
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT *
                                    FROM carteasy.dbo.tblfreightcharges 
                                    WHERE sublocationcode = '9003'
	                                    AND zone = '2'
	                                    AND shortsku = @shortsku
                                    ";
    }
}
