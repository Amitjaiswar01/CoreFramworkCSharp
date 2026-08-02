namespace LampsPlus.AutomationFramework.Databases.Queries.Shipping
{
    /// <summary>
    /// Query to identify the specific freight charge for service level 888 for a specific SKU.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T164
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T716
    /// </summary>
    public class ProductFreightChargeWithZone3
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 shortsku,
	                                    freightcharge
                                    FROM carteasy.dbo.tblfreightcharges 
                                    WHERE sublocationcode = '9003'
	                                    AND zone = '3'
	                                    AND shortsku = @shortsku
	                                    AND servicelevel = '888'
                                    ";
    }
}
