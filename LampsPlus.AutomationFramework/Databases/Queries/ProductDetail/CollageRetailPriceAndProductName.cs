namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    class CollageRetailPriceAndProductName
    {
            public const string Query = @"
                                    SET TRANSACTION isolation level READ uncommitted;

                                    WITH cte_collage(sceneid, iscollage) AS (
                                    SELECT SSR.sceneid, S.iscollage
                                    FROM carteasy..tblsceneskurelationship SSR
                                    INNER JOIN carteasy..tblprducts P ON P.shortsku = SSR.shortsku
                                    INNER JOIN carteasy..tblscenes S ON S.sceneid = SSR.sceneid
                                    WHERE P.listable = 1
                                    AND P.instock = 1
                                    AND SSR.issimilaritem = 0
                                    AND S.iscollage = 1
                                    GROUP BY SSR.sceneid, S.iscollage
                                    HAVING Count(1) > 3
                                    )
                                    SELECT top 1 SSR.sceneid AS CollageID, P.shortsku
                                    FROM carteasy..tblsceneskurelationship SSR
                                    INNER JOIN carteasy..tblprducts P ON P.shortsku = SSR.shortsku
                                    INNER JOIN cte_collage C ON C.sceneid = SSR.sceneid
                                    WHERE P.listable = 1
                                    AND P.instock = 1
                                    AND SSR.issimilaritem = 0
									Order by NEWID()
                                    ";
    }
}