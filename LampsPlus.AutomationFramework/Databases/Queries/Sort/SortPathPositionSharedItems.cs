namespace LampsPlus.AutomationFramework.Databases.Queries.Sort
{
    /// <summary>
    /// Query to find a Sort Path and Postion for All Users
    /// </summary>
    public class SortPathPositionSharedItems
    {
        public static string Query(string orderId) => $@"
                                                        USE carteasy 

                                                        SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
														
														SELECT sortpathid, 
                                                               sortposition, 
                                                               sortpath,
                                                               linenumber
                                                        FROM   tblshareditems si  
                                                            INNER JOIN carteasy.dbo.tblsortpath sp  ON sp.id = si.sortpathid 
                                                        WHERE  orderid = '{orderId}' 
                                                       ";
    }
}
