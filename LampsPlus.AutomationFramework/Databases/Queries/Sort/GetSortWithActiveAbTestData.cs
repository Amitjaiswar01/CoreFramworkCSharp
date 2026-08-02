namespace LampsPlus.AutomationFramework.Databases.Queries.Sort
{
    /// <summary>
    /// Query to get data for sort with an active AB test
    /// </summary>
    public class GetSortWithActiveAbTestData
    {
        public static string Query() => @"
                                        SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
                                        SELECT id, 
                                        M.NAME, 
                                        Lower(COALESCE('www.lampsplus.com/products/' + Replace(M.category, ' ', 
                                                '-') + 
                                                '/', '' 
                                                ) 
                                                + CASE WHEN M.type IS NOT NULL AND Rtrim(Ltrim(M.type)) != '' THEN 
                                                COALESCE('type_' + Replace(Replace(Replace(M.type, '-', '@'), ' - ' 
                                                , '-@-' 
                                                ), ' ', '-') + '/', '') ELSE '' END + CASE WHEN M.usage IS NOT NULL 
                                                AND 
                                                Rtrim(Ltrim(M.usage)) != '' THEN COALESCE('usage_' + Replace( 
                                                Replace( 
                                                Replace(M.usage, '-', '@'), ' - ', '-@-'), ' ', '-') + '/', '') 
                                                ELSE '' 
                                                END + CASE WHEN M.style IS NOT NULL AND Rtrim(Ltrim(M.style)) != '' 
                                                THEN 
                                                COALESCE('style_' + Replace(Replace(Replace(M.style, '-', '@'), 
                                                ' - ', 
                                                '-@-'), ' ', '-') + '/', '') ELSE '' END + CASE WHEN M.finish IS 
                                                NOT NULL 
                                                AND Rtrim(Ltrim(M.finish)) != '' THEN COALESCE('finish_' + Replace( 
                                                Replace 
                                                (Replace(M.finish, '-', '@'), ' - ', '-@-'), ' ', '-') + '/', '') 
                                                ELSE '' 
                                                END) AS url 
                                    INTO   #mmcleanurl 
                                    FROM   products.dbo.tblmerchandizerfilters M 

                                    SELECT Top 1 MF.NAME, MF.isactive, ATG.IsActive,
                                            MM.url, 
                                            ATG.filterid, 
                                            ( Cast(ATG.id AS VARCHAR(10)) + '-' + MF.NAME )     AS TestId, 
                                            ( Cast(ATC.formulapinlinkid AS VARCHAR(10)) 
                                                + '-' + FV.tags + '-' + FP.NAME + '-' + MF.NAME ) AS MMId, 
                                            ( Cast(FPL.versionid AS VARCHAR(10)) + '-' 
                                                + FV.tags )                                       AS FormulaId, 
                                            ( Cast(FP.id AS VARCHAR(10)) + '-' + FP.NAME )      AS PinId, 
                                            ATC.id                                              AS TestCompositionId, 
                                            ATG.startdate, * 
                                    FROM   products.dbo.tblabtestgroups AS ATG  
                                            INNER JOIN products.dbo.tblabtestcompositions AS ATC 
                                                    ON ATC.abtestgroupid = ATG.id 
                                            INNER JOIN products.dbo.tblabteststatus AS ATS 
                                                    ON ATS.id = ATC.abteststatusid 
                                            INNER JOIN products.dbo.tblfilterpin FP 
                                                    ON FP.filterid = ATG.filterid 
                                            INNER JOIN products.dbo.tblformulapinlink FPL 
                                                    ON FPL.filterpinid = FP.id 
                                                        AND FPL.id = ATC.formulapinlinkid 
                                            INNER JOIN products.dbo.tblformulaversion FV 
                                                    ON FV.id = FPL.versionid 
                                            INNER JOIN products.dbo.tblmerchandizerfilters AS MF 
                                                    ON MF.id = ATG.filterid 
                                            INNER JOIN #mmcleanurl MM 
                                                    ON MM.id = ATG.filterid 
                                    WHERE  ATG.isactive = 1 
                                    and LEN(url) > 0 
                                    and MF.Isactive = 1
                                    and ( Cast(ATG.id AS VARCHAR(10)) + '-' + MF.NAME ) NOT LIKE '%lighting fixtures%'
                                    and ( Cast(ATG.id AS VARCHAR(10)) + '-' + MF.NAME ) NOT LIKE '%ceiling fans%'
                                    and ( Cast(ATG.id AS VARCHAR(10)) + '-' + MF.NAME ) NOT LIKE '%floor lamps%'
                                    ORDER  BY ATS.NAME, NEWID() 

                                    DROP TABLE #mmcleanurl
                                    ";
    }
}