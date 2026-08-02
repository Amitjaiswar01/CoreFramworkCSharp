namespace LampsPlus.AutomationFramework.Databases.Queries.Sort
{
    /// <summary>
    /// Query to find a Sort Path and Postion for All Users
    /// </summary>
    public class SortPathPositionAllUsersRoles
    {
        public static string Query(string orderId) => $@"
                                    USE assets 
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT linenumber,
	                                       sortpathid,
                                           sortpath,
                                           sortposition                                           
                                    FROM   tblcartshareditems csi  
                                        INNER JOIN carteasy.dbo.tblsortpath sp   ON sp.id = csi.sortpathid 
                                    WHERE  orderid = '{orderId}' 
                                    order by LineNumber
                                    ";
    }
}
