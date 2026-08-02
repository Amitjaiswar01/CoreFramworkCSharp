namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify a SKU that has the 'Check Store Availability' link on the PDP. The SKU must have a value in the freight charges
    /// table with a service level of '888' or '111' for Zone 2.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T258
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1236
    /// </summary>
    public class SkuWithCheckStoreAvailabilityLinkOnPdp
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 p.shortsku
                                    FROM carteasy..tblprducts p 
                                    INNER JOIN carteasy.dbo.tblprductsextra pe 
	                                    ON p.ShortSKu = pe.ShortSKU
                                    WHERE 0.10 >= Cast(Checksum(Newid(), p.shortsku) & 0x7FFFFFFF AS FLOAT) / Cast(0x7FFFFFFF AS INT)
	                                    AND (
		                                    p.shortsku IN (
			                                    SELECT shortsku
			                                    FROM carteasy..tblfreightcharges 
			                                    WHERE servicelevel = '888'
				                                    AND sublocationcode = '9003'
				                                    AND zone = 2
			                                    )
		                                    OR p.shortsku IN (
			                                    SELECT shortsku
			                                    FROM carteasy..tblfreightcharges 
			                                    WHERE servicelevel = '111'
				                                    AND sublocationcode = '9003'
				                                    AND zone = 2
			                                    )
		                                    )
	                                    AND p.listable = 1
	                                    AND p.instock = 1                                  
	                                    AND pe.IsBopusEligible = 0
                                        AND ( pe.groupingsku IS NULL
                                                  OR pe.groupingsku = '' ) -- (1/14/22) Removing multi-products since they break the test using this query
                                        AND IsButtonEligible = 1
                                        AND FirstShipDays < 57 -- (1/14/22) Added to ensure the PDP has an Add to Cart button.
                                    ";
    }
}
