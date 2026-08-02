namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify a product that qualifies for the call-out "Product Not Available".
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7560
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7556
    /// </summary>
    class ProductNotAvailableSku
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 shortsku
                                    FROM   [Carteasy].[dbo].[vwpdptemplates] 
                                    WHERE  isstandard = 1
                                           AND isintranetonly = 0
                                           AND ishospitalityproduct = 0
                                           AND isbuttoneligible = 1
                                           AND isinstock = 0
                                           AND isclearance = 0
                                           AND isdailysale = 0
                                    ORDER BY Newid()                               
                                    ";
    }
}
