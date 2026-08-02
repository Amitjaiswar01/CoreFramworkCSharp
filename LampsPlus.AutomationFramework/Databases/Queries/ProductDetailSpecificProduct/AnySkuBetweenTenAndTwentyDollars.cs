namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetailSpecificProduct
{
    /// <summary>
    /// Query to identify a SKU between the prices 10 and 20 dollars
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T425
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T176
    /// </summary>
    public class AnySkuBetweenTenAndTwentyDollars
    {
        public const string Query = @"
                                    USE carteasy 

                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 shortsku 
                                    FROM   carteasy.dbo.tblprducts 
                                    WHERE  0.10 >= Cast(Checksum(Newid(), shortsku) & 0x7fffffff AS FLOAT) / Cast ( 
                                                          0x7fffffff AS INT) 
                                           AND instock = 1 
                                           AND listable = 1 
                                           AND ( retailpriceinternet BETWEEN 10 AND 20 
                                                  OR saleprice1internet BETWEEN 10 AND 20 ) 
                                           AND Charindex('-', shortsku) = 0 
                                           AND shortsku NOT LIKE '00%' 
                                           AND shortsku NOT LIKE '%0%' 
                                    ";
    }
}