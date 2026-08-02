namespace LampsPlus.AutomationFramework.Databases.Queries.SubmittingOrders
{
    /// <summary>
    /// Query to find a Linc Compatible product
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T158
    /// </summary>
    public class FindLincCompatibleProduct
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 dd.shortsku 
                                    FROM   carteasy.dbo.tblshareditems si 
                                           INNER JOIN carteasy.dbo.tblcarriercodes cc  
                                                   ON cc.shipviacode = si.shipvia 
                                                       OR cc.shipviahomedeliverycode = si.shipvia 
                                           INNER JOIN carteasy.dbo.tblprducts dd  
                                                   ON dd.shortsku = si.shortsku 
										   INNER JOIN carteasy.dbo.tblPrductsExtra px 
												   ON si.shortsku = px.shortsku
                                    WHERE  0.10 >= Cast(Checksum(Newid(), dd.shortsku) & 0x7FFFFFFF AS FLOAT) / Cast(0x7FFFFFFF AS INT)
									AND shiptocountry = 'US' 
                                           AND linccompatible = 1 
                                           AND dd.instock = 1 
                                           AND dd.listable = 1 
                                           AND ( pickupfromstore = 0 
                                                  OR pickupfromstore = NULL ) 
                                           AND ( retailpriceinternet BETWEEN 225 AND 500
                                                 AND saleprice1internet BETWEEN 225 AND 500 ) -- (12/13/21) Certain products with high prices trigger an additional shipping warning that breaks the test.
										   AND IsButtonEligible = 1
                                           AND FirstShipDays < 57
                                           AND (
		                                    px.groupingsku IS NULL
		                                    OR px.groupingsku = ''
		                                    )
                                        ";
    }
}
