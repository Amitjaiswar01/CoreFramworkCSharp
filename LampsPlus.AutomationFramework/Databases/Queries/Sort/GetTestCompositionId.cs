namespace LampsPlus.AutomationFramework.Databases.Queries.Sort
{
    public class GetTestCompositionId
    {
        public static string Query() => @"
                                        SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
										
										SELECT ATC.id AS TestCompositionId
                                        FROM products.dbo.tblabtestgroups AS ATG 
                                        INNER JOIN products.dbo.tblabtestcompositions AS ATC 
                                        ON ATC.abtestgroupid = ATG.id
                                        where FilterId = @filterId
                                        and ATC.isactive = 1
                                        and ATC.ABTestStatusId = 1
                                        ";
    }
}
