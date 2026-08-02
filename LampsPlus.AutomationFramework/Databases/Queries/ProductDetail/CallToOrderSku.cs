namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    public class CallToOrderSku
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
