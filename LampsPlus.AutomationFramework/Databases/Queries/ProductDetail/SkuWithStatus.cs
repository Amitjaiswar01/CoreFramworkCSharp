namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify literally any SKU that has a PDP. The retail price OR sale price must be greater than 0. The item cannot be 'intranetonly'.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T259
    /// </summary>
    public class SkuWithStatus
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 PE.skustatus, 
                                                   p.shortsku 
                                    FROM   carteasy.dbo.tblprductsextra PE 
                                           JOIN carteasy.dbo.tblprducts P 
                                             ON P.shortsku = PE.shortsku 
                                    WHERE  P.listable = 1                                           
                                           AND P.instock = 1 
                                           AND isbopuseligible = 0 
                                           AND skustatus <> '' 
                                           
                                    ";
    }
}
