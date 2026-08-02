
namespace LampsPlus.AutomationFramework.Databases.Queries.Sort
{
    /// <summary>
    /// Query to get cart items with path and position
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7478
    /// </summary>
    public class GetImageSearchItem
    {
        public const string  Query =        @"
                                                      SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
													  
													  SELECT TOP 1 p.shortsku
                                                      FROM   carteasy.dbo.tblprducts p  
                                                      INNER JOIN carteasy.dbo.tblprductsextra px  
                                                      ON px.shortsku = p.shortsku 
                                                      WHERE  p.listable = 1 
                                                      AND p.instock = 1 
                                                       AND ( retailpriceinternet > 0 
                                                       OR saleprice1internet > 0 ) 
                                                         AND intranetonly = 0 
                                                       AND groupingsku IS NULL 
                                                        ORDER  BY Newid()";
    }
}
