namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify a SKU that has the Energy Guide icon on the PDP. Energyinfo column must have a value of '1'. The category must be
    /// 'Ceiling Fans'. The attribute column must have one of three values: AirFlow, ElectricityUse, and AirflowEfficiency.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T263
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1030
    /// </summary>
    public class FanWithEnergyGuideIconShortSku
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									WITH t
                                    AS (
	                                    SELECT Row_number() OVER (
			                                    PARTITION BY shortsku ORDER BY shortsku
			                                    ) AS S,
		                                    shortsku,
		                                    attributeid
	                                    FROM carteasy.dbo.tblproductattributes 
	                                    )
                                    SELECT TOP 1 t.shortsku
                                    FROM t
                                    INNER JOIN carteasy.dbo.tblprducts A 
	                                    ON t.shortsku = A.shortsku
                                    INNER JOIN carteasy.dbo.tblproductattributevalues V 
	                                    ON V.[key1] = t.attributeid
                                    INNER JOIN carteasy.dbo.tblprductsextra pe 
	                                    ON pe.shortsku = t.shortsku
                                    WHERE s IN (
		                                    SELECT Max(s)
		                                    FROM t A
		                                    GROUP BY shortsku
		                                    HAVING a.shortsku = t.shortsku
		                                    )
	                                    AND listable = 1
	                                    AND instock = 1
	                                    AND isbopuseligible = 0
	                                    AND energyinfo = 1
                                        AND FirstShipDays < 57 -- (11/2/21) Added to ensure PDP has Add to Cart button.
										AND IsButtonEligible = 1
	                                    AND A.category = 'Ceiling fans'
	                                    AND V.attributetype IN (
		                                    'Airflow',
		                                    'ElectricityUse',
		                                    'AirflowEfficiency'
		                                    )
                                    ORDER BY NEWID()
                                    ";
    }
}
