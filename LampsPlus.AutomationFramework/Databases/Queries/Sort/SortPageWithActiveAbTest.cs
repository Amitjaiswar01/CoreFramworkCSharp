namespace LampsPlus.AutomationFramework.Databases.Queries.Sort
{
    /// <summary>
    /// Query to find a Sort Page with Active A/B Test
    /// </summary>
    public class SortPageWithActiveAbTest
    {
        public static string Query() => @"
                                        SET NOCOUNT ON;
                                        SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
   
                                        Select Id, M.Name,
                                                LOWER(  
                                                        COALESCE('www.lampsplus.com/products/' + REPLACE(M.Category, ' ', '-') + '/', '') +
                                                        CASE WHEN M.Type IS NOT NULL AND RTRIM(LTRIM(M.Type)) != '' THEN COALESCE('type_' + REPLACE(REPLACE(REPLACE(M.Type, '-', '@'), ' - ', '-@-'), ' ', '-') + '/', '') ELSE '' END +    
                                                        CASE WHEN M.Usage IS NOT NULL AND RTRIM(LTRIM(M.Usage)) != '' THEN COALESCE('usage_' + REPLACE(REPLACE(REPLACE(M.Usage, '-', '@'), ' - ', '-@-'), ' ', '-') + '/', '') ELSE '' END +
                                                        CASE WHEN M.Style IS NOT NULL AND RTRIM(LTRIM(M.Style)) != '' THEN COALESCE('style_' + REPLACE(REPLACE(REPLACE(M.Style, '-', '@'), ' - ', '-@-'), ' ', '-') + '/', '') ELSE '' END +
                                                        CASE WHEN M.Finish IS NOT NULL AND RTRIM(LTRIM(M.Finish)) != '' THEN COALESCE('finish_' + REPLACE(REPLACE(REPLACE(M.Finish, '-', '@'), ' - ', '-@-'), ' ', '-') + '/', '') ELSE '' END
                                                    )  as url
                                        INTO #MMCleanUrl
                                        from products.dbo.tblMerchandizerFilters M 
   
                                        SELECT MF.NAME, MM.Url,
                                                     FilterID,
                                                     ATG.isactive,
                                                     ATC.isactive,
                                                     ATG.id,
                                                     ATC.id,
                                                     ATS.NAME,
                                                     ATC.percentage,
                                                     ATC.easortnumber,
                                                     ATC.bucket,
                                                     *
                                        FROM   products.dbo.tblabtestgroups AS ATG 
                                               INNER JOIN products.dbo.tblabtestcompositions AS ATC 
                                                       ON ATC.abtestgroupid = ATG.id
                                               INNER JOIN products.dbo.tblabteststatus AS ATS 
                                                       ON ATS.id = ATC.abteststatusid
                                               INNER JOIN products.dbo.tblmerchandizerfilters AS MF 
                                                       ON MF.id = ATG.filterid
                                               INNER JOIN #MMCleanUrl MM 
                                                       ON MM.Id = ATG.FilterId
                                        WHERE  ATG.isactive = 1
                                               AND ATC.percentage <> 100
                                        ORDER  BY ATS.Name  

                                        DROP table #MMCleanUrl
                                    ";
    }
}