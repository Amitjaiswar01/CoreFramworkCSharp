namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetailCallout
{
    /// <summary>
    /// Query to retrieve a SKU for product that qualifies for 'Ships Free Within State' designation.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T251
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-17160
    /// </summary>
    public class ShipsFreeWitinState
    {
        public const string Query = @"
                                    USE carteasy 
                                     
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 p.ShortSku
                                    FROM   carteasy..tblprducts p 
                                           JOIN carteasy..tblfreightcharges f 
                                             ON f.shortsku = p.shortsku 
                                           INNER JOIN carteasy.dbo.tblprductsextra pe  
                                                   ON pe.shortsku = p.shortsku 
                                    WHERE  p.listable = 1 
                                           AND p.instock = 1 
                                           AND pe.isbopuseligible = 0 
                                           AND sublocationcode = '9003' 
                                           AND zone = 2 
                                           AND servicelevel = '888' 
                                           AND freightcharge > 0 
                                           AND f.freightcharge - Round(p.retailprice / 10, 0) <= 0 
                                           AND saleprice1internet = 0 
                                           AND retailpriceinternet > 49
                                           AND p.shortSku NOT LIKE '0%'   
                                    Order By NewID()
                                    ";
    }
}
