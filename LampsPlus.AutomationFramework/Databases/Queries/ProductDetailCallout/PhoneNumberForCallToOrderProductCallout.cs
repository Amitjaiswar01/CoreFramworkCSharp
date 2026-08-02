namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetailCallout
{
    /// <summary>
    /// Query to identify a SKU with callout and phone number for call to order product.
    /// Automated Desktop Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7563
    /// Automated Mobile Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7565
    /// Manual Test Case: 
    /// </summary>
    public class ShortSkuWithPhoneNumberCallToOrderCallout
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 shortsku 
                                    FROM   [Carteasy].[dbo].[vwpdptemplates] 
                                    WHERE  isstandard = 1 
                                           AND isintranetonly = 0 
                                           AND ishospitalityproduct = 0 
                                           AND isbuttoneligible = 1 
                                           AND isinstock = 1 
                                           AND NOT( isclearance = 1 
                                                     OR isdailysale = 1 ) 
                                           AND quantityavailable = 0 
                                           AND firstshipdays >= 57 
                                    ORDER BY Newid() 
                                    ";
    }
}
