namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify a SKU as a Build Full System SKU.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T352
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T355
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1208
    /// </summary>
    public class BuildFullSystemSku
    {
        public const string Query = @"
                                      USE carteasy

                                            SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
											DECLARE @ShortSku NVARCHAR(30),
		                                            @TmpCategory NVARCHAR(30),
		                                            @ComponentSku NVARCHAR(30),
		                                            @Manufacturer NVARCHAR(25),
		                                            @TrackColorFinish NVARCHAR(25)
		                                            
                                            ; WITH CTE AS
                                                (
                                                SELECT shortsku, TmpCategory, ComponentSku, [group], color
                                                FROM (
                                                        SELECT TOP 10 shortsku,'DIMMER' AS TmpCategory,'' AS ComponentSku,'' AS[group], '' AS color                                                                                 
                                                        FROM carteasy.dbo.tblprducts sp 
                                                        WHERE category LIKE '%Dimmer%'
                                                            AND accessories LIKE '%Dimmer%'
                                                            AND instock = 1                                                                                 
                                                            AND listable = 1                                                                                 
                                                            AND EXISTS (
                                                                        SELECT 1
                                                                        FROM[Carteasy].[dbo].[tblprducts] P 
                                                                        LEFT JOIN tblprductsextra PP 
                                                                                        ON PP.shortsku = P.shortsku                                                                                 
                                                                        LEFT JOIN categories C 
                                                                                        ON P.category LIKE C.cat + '%'
                                                                        WHERE category LIKE '%Dimmer%'
                                                                            AND listable = 1
                                                                            AND IsButtonEligible = 1     
                                                                            AND instock = 1
                                                                            AND FirstShipDays < 57     
                                                                            AND p.color = sp.color                                                                                 
                                                                            AND accessories <> 'Dimmer'
                                                                            AND accessories <> 'DimerWithRemote'
                                                                            AND accessories IS NOT NULL
                                                                            AND accessories<> ''
                                                                            AND retailpriceinternet <> 0.00
                                                                            AND p.manufacturer = sp.manufacturer                                                                                 
                                                                            AND p.shortsku<> sp.shortsku
                                                                            AND EXISTS (
                                                                                            SELECT 1

                                                                                            FROM carteasy.dbo.tblprducts sp1 
                                                                                            WHERE p.dimmernames LIKE '%' + sp1.dimmernames + '%'
                                                                                                            AND sp1.shortsku = sp.shortsku
                                                                                            )
							                                            )
                                                    UNION ALL

                                                    SELECT TOP 10 P.shortsku,'under cabinet' AS TmpCategory,'' AS ComponentSku,'' AS[group], '' AS color
                                                                    FROM tblprducts P 
																	INNER JOIN tblPrductsExtra px 
																	ON p.Shortsku = px.Shortsku
                                                                    WHERE P.category LIKE '%under cabinet%'
                                                                    AND usage IN ( 'Light bars', 'puck lights', 'tape light' )
                                                                    AND p.instock = 1
					                                                AND listable = 1
																	AND IsButtonEligible = 1
                                                                    AND retailpriceinternet <> 0.00
																	AND FirstShipDays < 57
                                                                    AND EXISTS (SELECT 1
                                                                    FROM carteasy.dbo.tblcoordinatingproduct 
                                                                    WHERE p.shortsku = shortsku)

                                                    UNION ALL

                                                    SELECT TOP 10 p.Shortsku, 'Landscape Lighting 2' AS TmpCategory,'' AS ComponentSku,'' AS[group], '' AS color
                                                    FROM[Carteasy].[dbo].[TblPrducts]
                                                    P 
													INNER JOIN tblPrductsExtra px 
																	ON p.Shortsku = px.Shortsku
                                                    LEFT JOIN[Products].[dbo].[tblKitDetails] K  ON P.ShortSku = K.KitSku
													WHERE P.[Category] LIKE 'Landscape Lighting%'AND P.[Class] NOT IN (4,5,8)
                                                    AND K.KitSku IS NULL AND P.InStock = 1
                                                    AND P.Listable = 1
													AND IsButtonEligible = 1                                                  
													AND FirstShipDays < 57 -- (4/7/2023) Added to ensure PDP has an Add to Cart button.
                                                    AND Type not like '%Line Voltage%'

                                                    UNION ALL

                                                    SELECT TOP 10 P.[ShortSKU],'Landscape Lighting 1' AS TmpCategory,'' AS ComponentSku,'' AS[group], '' AS color
                                                    FROM[Carteasy].[dbo].[TblPrducts] P 
                                                    LEFT JOIN[Products].[dbo].[tblKitDetails] K  ON P.ShortSku = K.KitSku
                                                    LEFT JOIN [Carteasy].[dbo].[tblEAAttributes] ea  on ea.[ShortSku] = p.shortsku
													INNER JOIN tblPrductsExtra px 
																	ON p.Shortsku = px.Shortsku
                                                    WHERE P.[Category] LIKE 'Landscape Lighting%'
                                                    AND P.[Class] NOT IN (4,5,8)
                                                    AND K.KitSku IS NOT NULL AND P.InStock = 1 AND P.Listable = 1
													AND IsButtonEligible = 1                                                  
													AND FirstShipDays < 57 -- (4/7/2023) Added to ensure PDP has an Add to Cart button.
                                                    AND NOT EXISTS (Select 1 from carteasy..[tblEAAttributes] ea 
                                                                where ea.[ShortSku] = p.shortsku and AttributeName = 'Usage' and AttributeValue = 'Complete Kits')

                                                    UNION ALL

                                                    SELECT TOP 10 p.shortsku,'Track Lighting' AS TmpCategory,'' AS ComponentSku, p.[group], p.color
                                            FROM tblprducts p 
                                                    INNER JOIN carteasy.dbo.tblprductsextra pe 
                                                        ON pe.shortsku = p.shortsku
                                                    WHERE (Abs(Cast((Binary_checksum(*) * Rand()) AS INT)) % 100) < 10
                                                        AND category LIKE '%Track%'
			                                            AND(
                                                            usage LIKE '%pendant%'
                                                            OR usage LIKE 'track heads'
				                                            )
                                                        AND isbopuseligible = 0
														AND IsButtonEligible = 1
                                                        AND listable = 1
														AND FirstShipDays < 57
                                                        AND instock = 1
                                                        AND NOT EXISTS(
                                                            SELECT TOP 1 1 FROM Products.dbo.tblKitDetails 
                                                            WHERE ComponentSku = p.ShortSKU
                                                            )
                                                        AND EXISTS(
                                                            SELECT 1
                                                            FROM tblbyotracklightingusage U 
                                                            INNER JOIN tblprducts SP 
                                                                ON SP.ShortSku = U.ShortSku
                                                            WHERE SP.[Group] = P.[Group]
                                                            AND SP.Color = p.Color
                                                            )

                                                    UNION ALL

                                                    SELECT TOP 10 p.shortsku,'Track Lighting Case 2' AS TmpCategory, kd.ComponentSku,'' AS[group], '' AS color
                                                    FROM carteasy.dbo.tblprducts p 
                                                    INNER JOIN Products.dbo.tblKitDetails kd  ON kd.KitSku = p.ShortSKU
													INNER JOIN carteasy.dbo.tblprductsextra pe 
                                                        ON pe.shortsku = p.shortsku
                                                    WHERE p.category LIKE '%Track%'
                                                        AND kd.ComponentSku IN ('82960','32299')
                                                        AND p.listable = 1
			                                            AND p.instock = 1
														AND IsButtonEligible = 1                                                        
														AND FirstShipDays < 57 -- (4/7/2023) Added to ensure PDP has an Add to Cart button.
                                                    ) AS Tbl
                                            )
                                            SELECT TOP 1 @ShortSku = shortsku
		                                            ,@TmpCategory = TmpCategory
		                                            ,@ComponentSku = ComponentSku
		                                            ,@Manufacturer = [group]
		                                            ,@TrackColorFinish = color
                                            FROM CTE c
											where not exists (select kitsku from [Products].[dbo].[tblKitDetails] K  
											inner join [Carteasy].[dbo].[tblprductsextra] pe on pe.shortsku = K.KitSKU
											where c.shortsku=K.KitSKU AND IsButtonEligible = 1 AND FirstShipDays < 57  )
                                            ORDER BY NEWID()

                                            SELECT PrimarySKU, BuildFullSystemSKUs, ProductName,[SizeOrDisplayOrder],[Data From below Category]
                                            FROM(
                                                SELECT @ShortSKU AS PrimarySKU, P.[shortsku] AS BuildFullSystemSKUs, P.productname,0 AS 'SizeOrDisplayOrder'
			                                            , @TmpCategory AS 'Data From below Category'
                                                FROM[Carteasy].[dbo].[tblprducts] P 
                                                WHERE @TmpCategory = 'DIMMER'
                                                    AND category LIKE '%Dimmer%'
                                                                AND listable = 1
                                                                AND instock = 1
                                                                AND EXISTS (
                                                                                SELECT 1
                                                                                FROM[Carteasy].[dbo].[tblprducts] pc 
                                                                                WHERE shortsku = @ShortSku
                                                                                                AND p.color = pc.color
                                                                                )
                                                                AND accessories<> 'Dimmer'
					                                            AND accessories <> 'DimerWithRemote'
                                                                AND accessories IS NOT NULL
                                                                AND accessories<> ''
                                                                AND retailpriceinternet <> 0.00
                                                                AND EXISTS (
                                                                                SELECT 1

                                                                                FROM[Carteasy].[dbo].[tblprducts] pm 
                                                                                WHERE shortsku = @ShortSku

                                                                                                AND p.manufacturer = pm.manufacturer
                                                                                )
                                                                AND EXISTS(
                                                                                SELECT 1
                                                                                FROM carteasy.dbo.tblprducts sp1 
                                                                                WHERE p.dimmernames LIKE '%' + sp1.dimmernames + '%'
                                                                                                AND sp1.shortsku = @ShortSku
                                                                                )
                                                UNION
                                                SELECT @ShortSku AS PrimarySKU,P.ShortSKU AS BuildFullSystemSKUs,P.productname,0 AS 'SizeOrDisplayOrder'
			                                            ,@TmpCategory AS 'Data From below Category'

                                                FROM tblCoordinatingProduct  CP 

                                                INNER JOIN tblPrducts P  ON P.ShortSKU = CP.CoordinatingSKU
                                                WHERE @TmpCategory = 'under cabinet'

                                                    AND CP.ShortSku = @ShortSku AND P.Sellable = 1
		                                            AND P.InStock = 1 --Added as per DBADMIN-3077

                                                UNION
                                                SELECT @ShortSku AS PrimarySKU, P.ShortSKU AS BuildFullSystemSKUs, P.productname,0 AS 'SizeOrDisplayOrder'
			                                            , @TmpCategory AS 'Data From below Category'
                                                FROM tblPrducts P 
                                                INNER JOIN tblPrductsExtra PE  ON PE.ShortSKU = P.ShortSKU
                                                WHERE @TmpCategory = 'Landscape Lighting 2'
                                                    AND P.ShortSKU in ('2N754','39549', '5G986', '88F56', '88F57', '88F58')
                                                AND P.InStock = 1
	                                            UNION
                                                SELECT @ShortSku AS PrimarySKU, P.ShortSKU AS BuildFullSystemSKUs, P.productname,0 AS 'SizeOrDisplayOrder'
			                                            , @TmpCategory AS 'Data From below Category'
                                                FROM tblPrducts P 
                                                INNER JOIN tblPrductsExtra PE  ON PE.ShortSKU = P.ShortSKU
                                                WHERE @TmpCategory = 'Landscape Lighting 1'
                                                    AND P.ShortSKU in ('2N754','39549', '5G986', '88F56', '88F57', '88F58')
                                                AND P.InStock = 1
												AND IsButtonEligible = 1
												AND FirstShipDays < 57
	                                            UNION
                                                SELECT @ShortSKU AS PrimarySKU,
                                                    U.shortsku AS BuildFullSystemSKU,
                                                    P.productname,
                                                    ( CASE LTrim(TrackUsage)
                                                       WHEN '2 ft track' THEN 2 WHEN '2'' track' THEN 2
                                                       WHEN '4 ft track' THEN 3 WHEN '4'' track' THEN 3
                                                       WHEN '6 ft track' THEN 4 WHEN '6'' track' THEN 4
                                                       WHEN '8 ft track' THEN 5 WHEN '8'' track' THEN 5
                                                       WHEN 'Straight Connector' THEN 6 WHEN 'Mini Connector' THEN 6 WHEN 'Straight Line Connector' THEN 6
                                                       WHEN 'L Connector' THEN 7 WHEN 'L-Connector' THEN 7
                                                       WHEN 'T Connector' THEN 8 WHEN 'T-Connector' THEN 8 WHEN 'T-Bar' THEN 8
                                                       WHEN 'Live end feed' THEN 9 WHEN 'Live End' THEN 9 WHEN 'End Feed Cord and Plug' THEN 9
                                                       WHEN 'Live end connector with cover' THEN 10 WHEN 'End connector' THEN 10
                                                       WHEN 'Floating canopy' THEN 11 WHEN 'canopy' THEN 11 WHEN 'Floating feed' THEN 11
                                                       WHEN 'Conduit adapter' THEN 12 WHEN 'Conduit End Feed' THEN 12
                                                       WHEN 'Power feed with cord' THEN 13
		                                               WHEN '3-Way Dimmer With Faceplate' THEN 101
		                                               WHEN 'Single Pole Dimmer With Faceplate' THEN 102
		                                               WHEN 'CFL/LED Dimmer' THEN 103
		                                               WHEN '1 Gang Screwless Faceplate' THEN 104
                                                       ELSE 100
                                                     END ) AS 'Size/Display Order'--DisplayOrder
			                                            ,@TmpCategory AS 'Data From below Category'
                                                FROM tblbyotracklightingusage U 
                                                INNER JOIN tblprducts P 
                                                    ON U.shortsku = P.shortsku
                                                LEFT JOIN tblprductsextra PP 
                                                    ON PP.shortsku = P.shortsku
                                                LEFT JOIN categories C 
                                                    ON P.category LIKE C.cat + '%'
                                                WHERE @TmpCategory = 'Track Lighting'
		                                            AND 
			                                            (
				                                            (
					                                            P.[Group] = @manufacturer
					                                            AND (
							                                            (Charindex('silver', @trackColorFinish) <> 0 AND P.color LIKE '%silver%')

						                                            OR(Charindex('brown', @trackColorFinish) <> 0 AND P.color LIKE '%brown%')

						                                            OR(Charindex('black', @trackColorFinish) <> 0 AND P.color LIKE '%black%')

						                                            OR(Charindex('White - Ivory', @trackColorFinish) <> 0 AND P.color LIKE '%White - Ivory%')
						                                            )    
				                                            )   
				                                            OR P.ShortSKU in ('78T82','67X87','R4081','49525') --Added as per DBADMIN-3077
			                                            )                  
		                                            AND P.listable = 1
		                                            AND P.InStock = 1 --Added as per DBADMIN-3077
													AND IsButtonEligible = 1
													AND FirstShipDays < 57
	                                            UNION
                                                SELECT @ShortSku AS PrimarySKU,
                                                        p.[ShortSKU] AS BuildFullSystemSKU,
                                                        p.[ProductName],
                                                        (
                                                            CASE WHEN @ComponentSku = '82960' THEN (CASE WHEN p.[ShortSku] = @ShortSku THEN '3'

                                                                                        WHEN p.[ShortSku] IN ('55615','85289') THEN '1'
											                                            WHEN p.[ShortSku] IN ('87440','37162') THEN '2'
											                                            ELSE ''
											                                            END)
				                                            WHEN @ComponentSku = '32299' AND p.[ShortSku] IN ('29838','29677','41109','29712') THEN '1' 
				                                            ELSE '' END
				                                            ) AS 'SizeOrDisplayOrder'--SIZE
			                                            ,@TmpCategory AS 'Data From below Category'

                                                    FROM[Carteasy].[dbo].[TblPrducts]
                                                    p 
                                                    LEFT JOIN tblprductsextra pp 
                                                        ON pp.shortsku = p.shortsku
                                                    LEFT JOIN categories c 
                                                        ON p.category LIKE c.cat + '%'
                                                    WHERE @TmpCategory = 'Track Lighting Case 2'
                                                        AND category IN ('Track Lighting','Accessories')
                                                        AND(CASE WHEN (@ComponentSku = '82960' AND p.[ShortSku] IN ('55615','85289','87440','37162') )
						                                            OR(@ComponentSku = '32299' AND p.[ShortSku] IN ('29838','29677','41109','29712')) THEN 1
				                                            END) = 1
			                                            AND retailpriceinternet<> 0.00
			                                            AND intranetonly = 0
														AND IsButtonEligible = 1
														AND FirstShipDays < 57
			                                            AND P.InStock = 1 --Added as per DBADMIN-3077
                                            ) AS Data
                                           ORDER BY[SizeOrDisplayOrder], NEWID()
                                    ";
    }
}
