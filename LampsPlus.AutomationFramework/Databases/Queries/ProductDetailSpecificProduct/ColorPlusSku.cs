namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetailSpecificProduct
{
    /// <summary>
    /// Query to identify a Combokit Color Plus SKU. Only grabs skus that have multiple shade options, because if there is only one shade
    /// option, then the second slider doesnt show on the pdp.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T226
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1203
    /// </summary>
    public class ColorPlusSku
    {
        public const string Query = @"
                                        SET TRANSACTION isolation level READ uncommitted;                                     

                                        IF OBJECT_ID('tempdb..#colorplusskus') IS NOT NULL
                                            DROP TABLE #colorplusskus;

                                        IF OBJECT_ID('tempdb..#Temp1') IS NOT NULL
                                            DROP TABLE #Temp1;

                                        SELECT DISTINCT
                                            KD.[kitsku] AS ShortSku,
                                            KD.[componentsku],
                                            Isnull(   CASE
                                                          WHEN CP.isgiclee = 1 THEN
                                                              CP.basesku1
                                                          ELSE
                                                              CP.basesku2
                                                      END,
                                                      Kd.componentsku
                                                  ) AS BaseSku1,
                                            Isnull(   CASE
                                                          WHEN CP.isgiclee = 1 THEN
                                                              CP.basesku2
                                                          ELSE
                                                              CP.basesku1
                                                      END,
                                                      Kd.componentsku
                                                  ) AS BaseSku2,
                                            C.[shortsku] AS ColorMeBad,
                                            S.[shortsku] AS ShadeMeBad
                                        INTO #colorplusskus
                                        FROM [Products].[dbo].[tblkitdetails] KD
                                            INNER JOIN [Products].[dbo].[tblkitdetails] KDC
                                                ON KDC.kitsku = KD.kitsku
                                            INNER JOIN [Products].[dbo].[tblkitdetails] KDS
                                                ON KDS.kitsku = KD.kitsku
                                            INNER JOIN carteasy.dbo.tblprductsextra B
                                                ON KD.sequencenumber = 1
                                                   AND KD.componentsku = B.shortsku
                                            INNER JOIN carteasy.dbo.tblprductsextra C
                                                ON KDC.sequencenumber = 2
                                                   AND KDC.componentsku = C.shortsku
                                            INNER JOIN carteasy.dbo.tblprductsextra S
                                                ON KDS.sequencenumber = 3
                                                   AND KDS.componentsku = S.shortsku
                                            INNER JOIN carteasy.dbo.tblprducts P
                                                ON P.shortsku = KD.kitsku
                                            LEFT JOIN products.dbo.tblcolorplus CP
                                                ON CP.basesku1 = Kd.componentsku
                                                   OR Cp.basesku2 = Kd.componentsku
                                            INNER JOIN carteasy.dbo.tblprducts PP
                                                ON KDS.componentsku = PP.shortsku
                                        WHERE 0.10 >= Cast(Checksum(Newid(), p.shortsku) & 0x7FFFFFFF AS FLOAT) / Cast(0x7FFFFFFF AS INT)
                                              AND B.lpbrand IN ( 'CPLUS', 'CPLPH', 'LPHCR' )
                                              AND B.skutype IN ( '*STCK', 'PKITC' )
                                              AND C.lpbrand IN ( 'CPLUS', 'LPHCR' )
                                              AND C.skutype = 'CCOLR'
                                              AND P.listable = 1
                                              AND P.instock = 1
                                              AND PP.category LIKE 'lamp shades%'

                                            SELECT pm.shortsku 
	                                        INTO #Temp1
                                            FROM productmicroservices.relationship.tblrelationshiptype t
                                                INNER JOIN productmicroservices.relationship.tblrelationshipgroup g
                                                    ON g.relationshipid = t.relationshipid
                                                INNER JOIN productmicroservices.relationship.tblrelationshipitem i
                                                    ON i.relationshipgroupid = g.relationshipgroupid
                                                INNER JOIN productmicroservices.relationship.tblrelationshipgroupmap m
                                                    ON m.relationshipgroupid = g.relationshipgroupid
                                                INNER JOIN carteasy.dbo.tblprducts pm
                                                    ON pm.shortsku = m.shortsku
                                                INNER JOIN carteasy.dbo.tblprducts pp
                                                    ON pp.shortsku = i.shortsku
                                            WHERE t.relationshipname = 'Parts'
                                                  AND pm.listable = 1
                                                  AND pm.instock = 1
                                                  AND pp.instock = 1

                                        SELECT TOP 1
                                            A.shortsku
                                        FROM #colorplusskus A
                                            INNER JOIN
                                            (
                                                SELECT basesku1,
                                                       basesku2,
                                                       Count(colormebad) AS cnt
                                                FROM #colorplusskus A
                                                GROUP BY A.colormebad,
                                                         A.basesku1,
                                                         A.basesku2
                                                HAVING Count(A.colormebad) > 1
                                            ) color
                                                ON A.basesku1 = color.basesku1
                                                   OR A.basesku2 = color.basesku2
                                        LEFT JOIN #temp1 B ON A.shortsku=B.shortsku
                                        WHERE B.shortsku IS NULL
                                        ORDER BY Newid()
                                             ";                                                       
                                            }
                                        }