namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetailCallout
{
    /// <summary>
    /// Query to determine a 'Ships In' verbiage on the PDP page. 
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/browse/LP-16925
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T256
    /// </summary>
    public class ShipsInVerbiage
    {
        public const string Query = @"
                                        SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
										SELECT TOP 1 [Copy]
                                        FROM tblProductsShipsInContent 
                                        WHERE FirstShipDays = @firstshipdays AND SubLocationCode = @sublocationcode 
                                    ";
    }
}
